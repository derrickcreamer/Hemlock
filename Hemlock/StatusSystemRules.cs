using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UtilityCollections;

namespace Hemlock {

	using Aggregator = Func<IEnumerable<int>, int>;
	using Converter = Func<int, int>;

	public static class StatusConverter<T, TResult> {
		public static Func<T, TResult> Convert;
	}

	public enum SourceType { Value, Suppression, Prevention };

	public delegate void OnChangedHandler<TObject, TStatus>(TObject obj, TStatus status, int oldValue, int newValue);

	internal interface IHandlers<TObject, TStatus> {
		OnChangedHandler<TObject, TStatus> GetHandler(TStatus status, TStatus overridden, bool increased, bool effect);
		void SetHandler(TStatus status, TStatus overridden, bool increased, bool effect, OnChangedHandler<TObject, TStatus> handler);
	}

	internal struct StatusPair<TStatus> : IEquatable<StatusPair<TStatus>> where TStatus : struct {
		public readonly TStatus status1;
		public readonly TStatus status2; //and now, some boilerplate:
		public StatusPair(TStatus status1, TStatus status2) {
			this.status1 = status1;
			this.status2 = status2;
		}
		public override int GetHashCode() { unchecked { return status1.GetHashCode() * 5557 + status2.GetHashCode(); } }
		public override bool Equals(object other) {
			if(other is StatusPair<TStatus>) return Equals((StatusPair<TStatus>)other);
			else return false;
		}
		public bool Equals(StatusPair<TStatus> other) => status1.Equals(other.status1) && status2.Equals(other.status2);
	}

	internal struct StatusChange<TStatus> : IEquatable<StatusChange<TStatus>> where TStatus : struct {
		public readonly TStatus status;
		public readonly bool increased;
		public readonly bool effect;
		public StatusChange(TStatus status, bool increased, bool effect) {
			this.status = status;
			this.increased = increased;
			this.effect = effect;
		}
		public override int GetHashCode() {
			unchecked {
				int hash = status.GetHashCode() + 857;
				if(increased) hash *= 7919;
				if(effect) hash *= 523;
				return hash;
			}
		}
		public override bool Equals(object other) {
			if(other is StatusChange<TStatus>) return Equals((StatusChange<TStatus>)other);
			else return false;
		}
		public bool Equals(StatusChange<TStatus> other) => status.Equals(other.status) && increased == other.increased && effect == other.effect;
	}

	public class BaseStatusSystem<TObject, TBaseStatus> : IHandlers<TObject, TBaseStatus> where TBaseStatus : struct {
		public class HandlerRules {
			private IHandlers<TObject, TBaseStatus> handlers;
			private TBaseStatus status;
			private TBaseStatus overridden;
			private bool effect;
			public OnChangedHandler<TObject, TBaseStatus> Increased {
				get { return handlers.GetHandler(status, overridden, true, effect); }
				set { handlers.SetHandler(status, overridden, true, effect, value); }
			}
			public OnChangedHandler<TObject, TBaseStatus> Decreased {
				get { return handlers.GetHandler(status, overridden, false, effect); }
				set { handlers.SetHandler(status, overridden, false, effect, value); }
			}
			//todo: xml comments here to explain
			public OnChangedHandler<TObject, TBaseStatus> Changed {
				set {
					handlers.SetHandler(status, overridden, true, effect, value);
					handlers.SetHandler(status, overridden, false, effect, value);
				}
			}
			internal HandlerRules(IHandlers<TObject, TBaseStatus> handlers, TBaseStatus status, TBaseStatus overridden, bool effect) {
				this.handlers = handlers;
				this.status = status;
				this.overridden = overridden;
				this.effect = effect;
			}
		}
		public class StatusHandlers {
			public readonly HandlerRules Messages, Effects;
			internal StatusHandlers(IHandlers<TObject, TBaseStatus> handlers, TBaseStatus status, TBaseStatus overridden) {
				Messages = new HandlerRules(handlers, status, overridden, false);
				Effects = new HandlerRules(handlers, status, overridden, true);
			}
		}
		public class StatusRules : StatusHandlers {
			protected BaseStatusSystem<TObject, TBaseStatus> rules;
			protected TBaseStatus status;
			protected static TBaseStatus Convert<TStatus>(TStatus status) where TStatus : struct {
				return BaseStatusSystem<TObject, TBaseStatus>.Convert(status);
			}
			protected static TBaseStatus[] Convert<TStatus>(TStatus[] statuses) where TStatus : struct {
				if(statuses == null) return null;
				var result = new TBaseStatus[statuses.Length];
				for(int i=0;i<statuses.Length;++i) result[i] = BaseStatusSystem<TObject, TBaseStatus>.Convert(statuses[i]);
				return result;
			}
			public StatusHandlers Overrides(TBaseStatus overridden) => new StatusHandlers(rules, status, overridden);
			public StatusHandlers Overrides<TStatus>(TStatus overridden) where TStatus : struct {
				return Overrides(Convert(overridden));
			}
			public Aggregator Aggregator {
				get { return rules.valueAggs[status]; }
				set {
					if(value == null) rules.valueAggs.Remove(status);
					else {
						rules.ValidateAggregator(value);
						rules.valueAggs[status] = value;
					}
				}
			}
			public bool SingleSource {
				get { return rules.SingleSource[status]; }
				set { rules.SingleSource[status] = value; }
			}
			private const string StatusExpected = "Expected one or more statuses";
			public void Extends(params TBaseStatus[] extendedStatuses) {
				if(extendedStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				foreach(TBaseStatus extended in extendedStatuses) {
					rules.statusesExtendedBy.AddUnique(status, extended);
					rules.statusesThatExtend.AddUnique(extended, status);
				}
			}
			public void Extends<TStatus>(params TStatus[] extendedStatuses) where TStatus : struct => Extends(Convert(extendedStatuses));
			public void Cancels(params TBaseStatus[] cancelledStatuses) {
				if(cancelledStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				foreach(TBaseStatus cancelled in cancelledStatuses) {
					rules.statusesCancelledBy.AddUnique(status, cancelled);
				}
			}
			public void Cancels<TStatus>(params TStatus[] cancelledStatuses) where TStatus : struct => Cancels(Convert(cancelledStatuses));
			public void Cancels(Func<int, bool> condition, params TBaseStatus[] cancelledStatuses) {
				if(cancelledStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				foreach(TBaseStatus cancelled in cancelledStatuses) {
					rules.statusesCancelledBy.AddUnique(status, cancelled);
					if(condition != null) rules.cancellationConditions[new StatusPair<TBaseStatus>(status, cancelled)] = condition;
				}
			}
			public void Cancels<TStatus>(Func<int, bool> condition, params TStatus[] cancelledStatuses) where TStatus : struct => Cancels(condition, Convert(cancelledStatuses));
			//todo: gotta explain this one, certainly
			public void Foils(params TBaseStatus[] foiledStatuses) {
				if(foiledStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				Cancels(foiledStatuses);
				Suppresses(foiledStatuses);
				Prevents(foiledStatuses);
			}
			public void Foils<TStatus>(params TStatus[] foiledStatuses) where TStatus : struct => Foils(Convert(foiledStatuses));
			public void Foils(Func<int, bool> condition, params TBaseStatus[] foiledStatuses) {
				if(foiledStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				Cancels(condition, foiledStatuses);
				Suppresses(condition, foiledStatuses);
				Prevents(condition, foiledStatuses);
			}
			public void Foils<TStatus>(Func<int, bool> condition, params TStatus[] foiledStatuses) where TStatus : struct => Foils(condition, Convert(foiledStatuses));

			public void Feeds(params TBaseStatus[] fedStatuses) => FeedsInternal(SourceType.Value, null, null, null, fedStatuses);
			public void Feeds(int fedValue, params TBaseStatus[] fedStatuses) => FeedsInternal(SourceType.Value, null, fedValue, null, fedStatuses);
			public void Feeds(int fedValue, Func<int, bool> condition, params TBaseStatus[] fedStatuses) => FeedsInternal(SourceType.Value, null, fedValue, condition, fedStatuses);
			public void Feeds(Func<int, bool> condition, params TBaseStatus[] fedStatuses) => FeedsInternal(SourceType.Value, null, null, condition, fedStatuses);
			public void Feeds(Converter converter, params TBaseStatus[] fedStatuses) => FeedsInternal(SourceType.Value, converter, null, null, fedStatuses);

			public void Feeds<TStatus>(params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(SourceType.Value, null, null, null, Convert(fedStatuses));
			public void Feeds<TStatus>(int fedValue, params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(SourceType.Value, null, fedValue, null, Convert(fedStatuses));
			public void Feeds<TStatus>(int fedValue, Func<int, bool> condition, params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(SourceType.Value, null, fedValue, condition, Convert(fedStatuses));
			public void Feeds<TStatus>(Func<int, bool> condition, params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(SourceType.Value, null, null, condition, Convert(fedStatuses));
			public void Feeds<TStatus>(Converter converter, params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(SourceType.Value, converter, null, null, Convert(fedStatuses));

			// Many of these are disabled because there's currently no valid reason to use them.
			public void Suppresses(params TBaseStatus[] suppressedStatuses) => FeedsInternal(SourceType.Suppression, null, null, null, suppressedStatuses);
			//public void Suppresses(int fedValue, params TBaseStatus[] suppressedStatuses) => FeedsInternal(SourceType.Suppression, null, fedValue, null, suppressedStatuses);
			//public void Suppresses(int fedValue, Func<int, bool> condition, params TBaseStatus[] suppressedStatuses) => FeedsInternal(SourceType.Suppression, null, fedValue, condition, suppressedStatuses);
			public void Suppresses(Func<int, bool> condition, params TBaseStatus[] suppressedStatuses) => FeedsInternal(SourceType.Suppression, null, null, condition, suppressedStatuses);
			//public void Suppresses(Converter converter, params TBaseStatus[] suppressedStatuses) => FeedsInternal(SourceType.Suppression, converter, null, null, suppressedStatuses);

			public void Suppresses<TStatus>(params TStatus[] suppressedStatuses) where TStatus : struct => FeedsInternal(SourceType.Suppression, null, null, null, Convert(suppressedStatuses));
			//public void Suppresses<TStatus>(int fedValue, params TStatus[] suppressedStatuses) where TStatus : struct => FeedsInternal(SourceType.Suppression, null, fedValue, null, Convert(suppressedStatuses));
			//public void Suppresses<TStatus>(int fedValue, Func<int, bool> condition, params TStatus[] suppressedStatuses) where TStatus : struct => FeedsInternal(SourceType.Suppression, null, fedValue, condition, Convert(suppressedStatuses));
			public void Suppresses<TStatus>(Func<int, bool> condition, params TStatus[] suppressedStatuses) where TStatus : struct => FeedsInternal(SourceType.Suppression, null, null, condition, Convert(suppressedStatuses));
			//public void Suppresses<TStatus>(Converter converter, params TStatus[] suppressedStatuses) where TStatus : struct => FeedsInternal(SourceType.Suppression, converter, null, null, Convert(suppressedStatuses));

			public void Prevents(params TBaseStatus[] preventedStatuses) => FeedsInternal(SourceType.Prevention, null, null, null, preventedStatuses);
			//public void Prevents(int fedValue, params TBaseStatus[] preventedStatuses) => FeedsInternal(SourceType.Prevention, null, fedValue, null, preventedStatuses);
			//public void Prevents(int fedValue, Func<int, bool> condition, params TBaseStatus[] preventedStatuses) => FeedsInternal(SourceType.Prevention, null, fedValue, condition, preventedStatuses);
			public void Prevents(Func<int, bool> condition, params TBaseStatus[] preventedStatuses) => FeedsInternal(SourceType.Prevention, null, null, condition, preventedStatuses);
			//public void Prevents(Converter converter, params TBaseStatus[] preventedStatuses) => FeedsInternal(SourceType.Prevention, converter, null, null, preventedStatuses);

			public void Prevents<TStatus>(params TStatus[] preventedStatuses) where TStatus : struct => FeedsInternal(SourceType.Prevention, null, null, null, Convert(preventedStatuses));
			//public void Prevents<TStatus>(int fedValue, params TStatus[] preventedStatuses) where TStatus : struct => FeedsInternal(SourceType.Prevention, null, fedValue, null, Convert(preventedStatuses));
			//public void Prevents<TStatus>(int fedValue, Func<int, bool> condition, params TStatus[] preventedStatuses) where TStatus : struct => FeedsInternal(SourceType.Prevention, null, fedValue, condition, Convert(preventedStatuses));
			public void Prevents<TStatus>(Func<int, bool> condition, params TStatus[] preventedStatuses) where TStatus : struct => FeedsInternal(SourceType.Prevention, null, null, condition, Convert(preventedStatuses));
			//public void Prevents<TStatus>(Converter converter, params TStatus[] preventedStatuses) where TStatus : struct => FeedsInternal(SourceType.Prevention, converter, null, null, Convert(preventedStatuses));

			protected void FeedsInternal(SourceType type, Converter converter,
				int? fedValue, Func<int, bool> condition, params TBaseStatus[] fedStatuses)
			{
				if(fedStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				if(condition != null) {
					if(fedValue != null) {
						int fed = fedValue.Value;
						converter = i => {
							if(condition(i)) return fed; //todo: cache this?
							else return 0;
						};
					}
					else {
						converter = i => {
							if(condition(i)) return i; //todo: cache?
							else return 0;
						};
					}
				}
				else {
					if(fedValue != null) {
						int fed = fedValue.Value;
						converter = i => {
							if(i > 0) return fed; //todo: cache?
							else return 0;
						};
					}
				}
				if(converter != null) rules.ValidateConverter(converter);
				foreach(TBaseStatus fedStatus in fedStatuses) {
					rules.statusesFedBy[type].AddUnique(status, fedStatus);
					if(converter != null) {
						var pair = new StatusPair<TBaseStatus>(status, fedStatus);
						rules.converters[type][pair] = converter;
					}
				}
			}
			public void PreventedWhen(Func<TObject, TBaseStatus, bool> preventionCondition) {
				rules.extraPreventionConditions.AddUnique(status, preventionCondition);
			}
			internal StatusRules(BaseStatusSystem<TObject, TBaseStatus> rules, TBaseStatus status) : base(rules, status, status) {
				this.rules = rules;
				this.status = status;
			}
		}
		public StatusRules this[TBaseStatus status] => new StatusRules(this, status);
		protected Dictionary<SourceType, Aggregator> defaultAggs;
		public Aggregator DefaultValueAggregator {
			get { return defaultAggs[SourceType.Value]; }
			set {
				if(value == null) throw new ArgumentNullException("value", "Default aggregators cannot be null.");
				ValidateAggregator(value);
				defaultAggs[SourceType.Value] = value;
			}
		}
		internal void ValidateAggregator(Aggregator agg) {
			if(agg(Enumerable.Empty<int>()) != 0) throw new ArgumentException("Aggregators must have a base value of 0.");
		}

		internal DefaultValueDictionary<TBaseStatus, Aggregator> valueAggs;
		internal DefaultHashSet<TBaseStatus> SingleSource { get; private set; }

		internal MultiValueDictionary<TBaseStatus, TBaseStatus> statusesCancelledBy;
		internal MultiValueDictionary<TBaseStatus, TBaseStatus> statusesExtendedBy;
		internal MultiValueDictionary<TBaseStatus, TBaseStatus> statusesThatExtend;

		internal Dictionary<SourceType, MultiValueDictionary<TBaseStatus, TBaseStatus>> statusesFedBy;

		internal Dictionary<SourceType, Dictionary<StatusPair<TBaseStatus>, Converter>> converters;
		internal void ValidateConverter(Converter conv) {
			if(conv(0) != 0) throw new ArgumentException("Converters must output 0 when input is 0.");
		}
		internal DefaultValueDictionary<StatusPair<TBaseStatus>, Func<int, bool>> cancellationConditions;

		public readonly OnChangedHandler<TObject, TBaseStatus> DoNothing;
		public readonly Aggregator Total;
		public readonly Aggregator Bool;
		public readonly Aggregator MaximumOrZero;

		internal DefaultValueDictionary<TBaseStatus, DefaultValueDictionary<StatusChange<TBaseStatus>, OnChangedHandler<TObject, TBaseStatus>>> onChangedHandlers;
		internal MultiValueDictionary<TBaseStatus, Func<TObject, TBaseStatus, bool>> extraPreventionConditions;

		internal Aggregator GetAggregator(TBaseStatus status, SourceType type) {
			if(type == SourceType.Value) {
				Aggregator agg = valueAggs[status];
				if(agg != null) return agg;
			}
			return defaultAggs[type];
		}
		void IHandlers<TObject, TBaseStatus>.SetHandler(TBaseStatus status, TBaseStatus overridden, bool increased, bool effect, OnChangedHandler<TObject, TBaseStatus> handler) {
			if(!onChangedHandlers.ContainsKey(status)) {
				onChangedHandlers.Add(status, new DefaultValueDictionary<StatusChange<TBaseStatus>, OnChangedHandler<TObject, TBaseStatus>>());
			}
			onChangedHandlers[status][new StatusChange<TBaseStatus>(overridden, increased, effect)] = handler;
		}
		OnChangedHandler<TObject, TBaseStatus> IHandlers<TObject, TBaseStatus>.GetHandler(TBaseStatus status, TBaseStatus overridden, bool increased, bool effect) {
			if(!onChangedHandlers.ContainsKey(status)) return null;
			return onChangedHandlers[status][new StatusChange<TBaseStatus>(overridden, increased, effect)];
		}

		//todo xml: For performance. If true, it doesn't look for rule errors at all.
		public bool IgnoreRuleErrors { get; set; }

		protected bool trackerCreated;
		internal bool TrackerCreated {
			get { return trackerCreated; }
			set {
				if(trackerCreated) return; // Once true, it stays true and does nothing else.
				trackerCreated = value; // (One possible safety feature: After a tracker has been created, throw on further rule changes.)
				if(value) CheckRuleErrors();
			}
		}
		protected void CheckRuleErrors() {
			if(!IgnoreRuleErrors) {
				VerifyConversions();
				var ruleChecker = new RuleChecker<TObject, TBaseStatus>(this);
				var errorList = ruleChecker.GetErrors(false);
				if(errorList.Count > 0) {
					throw new InvalidDataException("Illegal rules detected:     \r\n" + string.Join("     \r\n", errorList));
				}
			}
		}
		private void VerifyConversions() {
			if(requiredConversionChecks != null) {
				foreach(var verify in requiredConversionChecks) verify();
			}
		}
		public List<string> GetRuleErrorsAndWarnings() {
			VerifyConversions();
			var ruleChecker = new RuleChecker<TObject, TBaseStatus>(this);
			return ruleChecker.GetErrors(true);
		}

		//todo: xml note: null is a legal value here, but the user is responsible for ensuring that no OnChanged handlers make use of the 'obj' parameter.
		public BaseStatusTracker<TObject, TBaseStatus> CreateStatusTracker(TObject obj) {
			return new BaseStatusTracker<TObject, TBaseStatus>(obj, this);
		}
		protected static TBaseStatus Convert<TStatus>(TStatus status) where TStatus : struct {
			try {
				return StatusConverter<TStatus, TBaseStatus>.Convert(status);
			}
			catch(NullReferenceException) {
				string tName = typeof(TStatus).Name;
				string baseName = typeof(TBaseStatus).Name;
				throw new InvalidOperationException($"No converter found for {tName}. (StatusConverter<{tName}, {baseName}>.Convert must be given a value before defining status rules.)");
			}
		}
		protected List<Action> requiredConversionChecks;
		internal List<Type> extraEnumTypes;
		protected void AddConversionCheck<T>() {
			if(typeof(T).IsEnum) extraEnumTypes.Add(typeof(T));
			string tName = typeof(T).Name;
			string baseName = typeof(TBaseStatus).Name;
			requiredConversionChecks.Add(() => {
				if(StatusConverter<T, TBaseStatus>.Convert == null) {
					throw new InvalidOperationException($"No converter found for {tName}. (StatusConverter<{tName}, {baseName}>.Convert must be given a value before creating a status tracker.)");
				}
			});
		}
		public BaseStatusSystem() {
			requiredConversionChecks = new List<Action>();
			extraEnumTypes = new List<Type>();
			DoNothing = (obj, status, ov, nv) => { };
			Total = ints => {
				int total = 0;
				foreach(int i in ints) total += i;
				return total;
			};
			Bool = ints => {
				int total = 0;
				foreach(int i in ints) total += i;
				if(total > 0) return 1;
				else return 0;
			};
			MaximumOrZero = ints => {
				int max = 0;
				foreach(int i in ints) if(i > max) max = i;
				return max;
			};
			defaultAggs = new Dictionary<SourceType, Func<IEnumerable<int>, int>>();
			defaultAggs[SourceType.Value] = Total;
			defaultAggs[SourceType.Suppression] = Bool;
			defaultAggs[SourceType.Prevention] = Bool;
			valueAggs = new DefaultValueDictionary<TBaseStatus, Aggregator>();
			SingleSource = new DefaultHashSet<TBaseStatus>();
			statusesCancelledBy = new MultiValueDictionary<TBaseStatus, TBaseStatus>();
			statusesExtendedBy = new MultiValueDictionary<TBaseStatus, TBaseStatus>();
			statusesThatExtend = new MultiValueDictionary<TBaseStatus, TBaseStatus>();
			statusesFedBy = new Dictionary<SourceType, MultiValueDictionary<TBaseStatus, TBaseStatus>>();
			converters = new Dictionary<SourceType, Dictionary<StatusPair<TBaseStatus>, Func<int, int>>>();
			cancellationConditions = new DefaultValueDictionary<StatusPair<TBaseStatus>, Func<int, bool>>();
			foreach(SourceType type in Enum.GetValues(typeof(SourceType))) {
				statusesFedBy[type] = new MultiValueDictionary<TBaseStatus, TBaseStatus>();
				converters[type] = new Dictionary<StatusPair<TBaseStatus>, Func<int, int>>();
			}
			onChangedHandlers = new DefaultValueDictionary<TBaseStatus, DefaultValueDictionary<StatusChange<TBaseStatus>, OnChangedHandler<TObject, TBaseStatus>>>();
			extraPreventionConditions = new MultiValueDictionary<TBaseStatus, Func<TObject, TBaseStatus, bool>>();
		}
	}
	public class StatusSystem<TObject> : BaseStatusSystem<TObject, int> {
		new public StatusTracker<TObject> CreateStatusTracker(TObject obj) => new StatusTracker<TObject>(obj, this);
	}
	public class StatusSystem<TObject, TStatus1> : StatusSystem<TObject> where TStatus1 : struct {
		public StatusSystem() { AddConversionCheck<TStatus1>(); }
		public StatusRules this[TStatus1 status] => new StatusRules(this, Convert(status));
		new public StatusTracker<TObject, TStatus1> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1>(obj, this);
	}
	public class StatusSystem<TObject, TStatus1, TStatus2> : StatusSystem<TObject, TStatus1> where TStatus1 : struct where TStatus2 : struct{
		public StatusSystem() { AddConversionCheck<TStatus2>(); }
		public StatusRules this[TStatus2 status] => new StatusRules(this, Convert(status));
		new public StatusTracker<TObject, TStatus1, TStatus2> CreateStatusTracker(TObject obj) => new StatusTracker<TObject, TStatus1, TStatus2>(obj, this);
	}
}
