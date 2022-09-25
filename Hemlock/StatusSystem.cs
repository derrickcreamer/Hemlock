using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UtilityCollections;

namespace Hemlock {

	using Aggregator = Func<IEnumerable<int>, int>;
	using Converter = Func<int, int>;
	using TBaseStatus = System.Int32; // Using 'TBaseStatus' to clarify the status keys vs the int values

	public static class StatusConverter<T, TResult> {
		/// <summary>
		/// Define a conversion between two types. Hemlock will use this conversion internally.
		/// </summary>
		public static Func<T, TResult> Convert;
	}

	/// <summary>
	/// InstanceType determines whether a StatusInstance will feed, suppress, or prevent its status.
	/// (Feed is the default and most common.)
	/// </summary>
	public enum InstanceType { Feed, Suppress, Prevent };

	/// <summary>
	/// A method that'll be run when a status changes on "obj".
	/// </summary>
	public delegate void OnChangedHandler<TObject>(TObject obj, TBaseStatus status, int oldValue, int newValue);

	internal interface IHandlers<TObject> {
		OnChangedHandler<TObject> GetHandler(TBaseStatus status, TBaseStatus overridden, bool increased, bool effect);
		void SetHandler(TBaseStatus status, TBaseStatus overridden, bool increased, bool effect, OnChangedHandler<TObject> handler);
	}

	internal struct StatusPair : IEquatable<StatusPair> {
		public readonly TBaseStatus status1;
		public readonly TBaseStatus status2; //and now, some boilerplate:
		public StatusPair(TBaseStatus status1, TBaseStatus status2) {
			this.status1 = status1;
			this.status2 = status2;
		}
		public override int GetHashCode() { unchecked { return status1 * 5557 + status2; } }
		public override bool Equals(object other) {
			if(other is StatusPair) return Equals((StatusPair)other);
			else return false;
		}
		public bool Equals(StatusPair other) => status1.Equals(other.status1) && status2.Equals(other.status2);
	}

	internal struct StatusChange : IEquatable<StatusChange> {
		public readonly TBaseStatus status;
		public readonly bool increased;
		public readonly bool effect;
		public StatusChange(TBaseStatus status, bool increased, bool effect) {
			this.status = status;
			this.increased = increased;
			this.effect = effect;
		}
		public override int GetHashCode() {
			unchecked {
				int hash = status + 857;
				if(increased) hash *= 7919;
				if(effect) hash *= 523;
				return hash;
			}
		}
		public override bool Equals(object other) {
			if(other is StatusChange) return Equals((StatusChange)other);
			else return false;
		}
		public bool Equals(StatusChange other) => status.Equals(other.status) && increased == other.increased && effect == other.effect;
	}

	public class StatusSystem<TObject> : IHandlers<TObject> {
		public class HandlerRules {
			private IHandlers<TObject> handlers;
			private TBaseStatus status;
			private TBaseStatus overridden;
			private bool effect;
			/// <summary>
			/// The method that'll be invoked in response to an increased value for this status
			/// </summary>
			public OnChangedHandler<TObject> Increased {
				get { return handlers.GetHandler(status, overridden, true, effect); }
				set { handlers.SetHandler(status, overridden, true, effect, value); }
			}
			/// <summary>
			/// The method that'll be invoked in response to a decreased value for this status
			/// </summary>
			public OnChangedHandler<TObject> Decreased {
				get { return handlers.GetHandler(status, overridden, false, effect); }
				set { handlers.SetHandler(status, overridden, false, effect, value); }
			}
			/// <summary>
			/// This property is simply a convenient way to set both Increased and Decreased at the same time.
			/// (Therefore, if Changed is set, that method will be invoked in response to any change in value for this status.)
			/// </summary>
			public OnChangedHandler<TObject> Changed {
				set {
					handlers.SetHandler(status, overridden, true, effect, value);
					handlers.SetHandler(status, overridden, false, effect, value);
				}
			}
			internal HandlerRules(IHandlers<TObject> handlers, TBaseStatus status, TBaseStatus overridden, bool effect) {
				this.handlers = handlers;
				this.status = status;
				this.overridden = overridden;
				this.effect = effect;
			}
		}
		public class StatusHandlers {
			/// <summary>
			/// Add "message" methods to be invoked in response to status value changes.
			/// </summary>
			public readonly HandlerRules Messages;
			/// <summary>
			/// Add "effect" methods to be invoked in response to status value changes.
			/// </summary>
			public readonly HandlerRules Effects;
			internal StatusHandlers(IHandlers<TObject> handlers, TBaseStatus status, TBaseStatus overridden) {
				Messages = new HandlerRules(handlers, status, overridden, false);
				Effects = new HandlerRules(handlers, status, overridden, true);
			}
		}
		public class StatusRules : StatusHandlers {
			protected StatusSystem<TObject> rules;
			protected TBaseStatus status;
			protected static TBaseStatus Convert<TStatus>(TStatus status) where TStatus : struct {
				return StatusSystem<TObject>.Convert(status);
			}
			protected static TBaseStatus[] Convert<TStatus>(TStatus[] statuses) where TStatus : struct {
				if(statuses == null) return null;
				var result = new TBaseStatus[statuses.Length];
				for(int i=0;i<statuses.Length;++i) result[i] = StatusSystem<TObject>.Convert(statuses[i]);
				return result;
			}
			/// <summary>
			/// Override message or effect behavior whenever a change in *this* status causes a change in *another* status.
			/// </summary>
			/// <param name="overridden">The status whose message/effect behavior should be overridden</param>
			public StatusHandlers Overrides(TBaseStatus overridden) => new StatusHandlers(rules, status, overridden);
			/// <summary>
			/// Override message or effect behavior whenever a change in *this* status causes a change in *another* status.
			/// </summary>
			/// <param name="overridden">The status whose message/effect behavior should be overridden</param>
			public StatusHandlers Overrides<TStatus>(TStatus overridden) where TStatus : struct {
				return Overrides(Convert(overridden));
			}
			/// <summary>
			/// If set, this status will use the specified override set instead of any message/effect handlers defined on the status itself.
			/// If set to null, no override set will be used.
			/// </summary>
			public void UsesOverrideSet(int? overrideSetIndex) {
				if(overrideSetIndex == null) rules.overrideSetsForStatuses.Remove(status);
				else rules.overrideSetsForStatuses[status] = overrideSetIndex.Value;
			}
			/// <summary>
			/// The value aggregator for this status - if this property is null, the default aggregator is used instead.
			/// </summary>
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
			/// <summary>
			/// If a status is set to "single instance", it can only have 1 StatusInstance at a time.
			/// Whenever an instance is added, all other instances are removed. Additionally, a single-instance status
			/// can have its value set directly through its indexer.
			/// This is very useful when a status should behave more like a single number than a collection of numbers.
			/// </summary>
			public bool SingleInstance {
				get { return rules.SingleInstance[status]; }
				set { rules.SingleInstance[status] = value; }
			}
			/// <summary>
			/// If CalculateDerivedValue is set for this status, it becomes a 'derived' status. A derived status can't
			/// have instances added to it, and can't participate in status relationships (feed, suppress, or prevent).
			/// Instead, this calculation will always be used to find its value based on other values in the tracker.
			/// </summary>
			public Func<StatusTracker<TObject>, int> CalculateDerivedValue {
				get { return rules.getDerivedValue[status]; }
				set { rules.getDerivedValue[status] = value; }
			}
			private const string StatusExpected = "Expected one or more statuses";
			/// <summary>
			/// Declare that this status extends one or more other statuses
			/// (i.e., this status is a subtype of those other statuses).
			/// </summary>
			public void Extends(params TBaseStatus[] extendedStatuses) {
				if(extendedStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				foreach(TBaseStatus extended in extendedStatuses) {
					rules.statusesExtendedBy.AddUnique(status, extended);
					rules.statusesThatExtend.AddUnique(extended, status);
				}
			}
			/// <summary>
			/// Declare that this status extends one or more other statuses
			/// (i.e., this status is a subtype of those other statuses).
			/// </summary>
			public void Extends<TStatus>(params TStatus[] extendedStatuses) where TStatus : struct => Extends(Convert(extendedStatuses));
			/// <summary>
			/// Declare that this status cancels one or more other statuses
			/// (i.e., when the value of this status increases, try to set those other statuses to zero by removing their StatusInstances).
			/// </summary>
			public void Cancels(params TBaseStatus[] cancelledStatuses) {
				if(cancelledStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				foreach(TBaseStatus cancelled in cancelledStatuses) {
					rules.statusesCancelledBy.AddUnique(status, cancelled);
				}
			}
			/// <summary>
			/// Declare that this status cancels one or more other statuses
			/// (i.e., when the value of this status increases, try to set those other statuses to zero by removing their StatusInstances).
			/// </summary>
			public void Cancels<TStatus>(params TStatus[] cancelledStatuses) where TStatus : struct => Cancels(Convert(cancelledStatuses));
			/// <summary>
			/// Declare that this status conditionally cancels one or more other statuses
			/// (i.e., when the value of this status increases, if the condition is true,
			/// try to set those other statuses to zero by removing their StatusInstances).
			/// </summary>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Cancels(Func<int, bool> condition, params TBaseStatus[] cancelledStatuses) {
				if(cancelledStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				foreach(TBaseStatus cancelled in cancelledStatuses) {
					rules.statusesCancelledBy.AddUnique(status, cancelled);
					if(condition != null) rules.cancellationConditions[new StatusPair(status, cancelled)] = condition;
				}
			}
			/// <summary>
			/// Declare that this status conditionally cancels one or more other statuses
			/// (i.e., when the value of this status increases, if the condition is true,
			/// try to set those other statuses to zero by removing their StatusInstances).
			/// </summary>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Cancels<TStatus>(Func<int, bool> condition, params TStatus[] cancelledStatuses) where TStatus : struct => Cancels(condition, Convert(cancelledStatuses));
			/// <summary>
			/// Declare that this status CANCELS, SUPPRESSES, and PREVENTS one or more other statuses
			/// </summary>
			public void Foils(params TBaseStatus[] foiledStatuses) {
				if(foiledStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				Cancels(foiledStatuses);
				Suppresses(foiledStatuses);
				Prevents(foiledStatuses);
			}
			/// <summary>
			/// Declare that this status CANCELS, SUPPRESSES, and PREVENTS one or more other statuses
			/// </summary>
			public void Foils<TStatus>(params TStatus[] foiledStatuses) where TStatus : struct => Foils(Convert(foiledStatuses));
			/// <summary>
			/// Declare that this status conditionally CANCELS, SUPPRESSES, and PREVENTS one or more other statuses
			/// </summary>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Foils(Func<int, bool> condition, params TBaseStatus[] foiledStatuses) {
				if(foiledStatuses.Length == 0) throw new ArgumentException(StatusExpected);
				Cancels(condition, foiledStatuses);
				Suppresses(condition, foiledStatuses);
				Prevents(condition, foiledStatuses);
			}
			/// <summary>
			/// Declare that this status conditionally CANCELS, SUPPRESSES, and PREVENTS one or more other statuses
			/// </summary>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Foils<TStatus>(Func<int, bool> condition, params TStatus[] foiledStatuses) where TStatus : struct => Foils(condition, Convert(foiledStatuses));

			/// <summary>
			/// Declare that this status feeds one or more other statuses
			/// (i.e., this status's value will be added to those other statuses).
			/// </summary>
			public void Feeds(params TBaseStatus[] fedStatuses) => FeedsInternal(InstanceType.Feed, null, null, null, fedStatuses);
			/// <summary>
			/// Declare that this status feeds one or more other statuses
			/// (i.e., this status's value will be added to those other statuses).
			/// </summary>
			public void Feeds<TStatus>(params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(InstanceType.Feed, null, null, null, Convert(fedStatuses));
			/// <summary>
			/// Declare that this status conditionally feeds one or more other statuses
			/// (i.e., if this status's value meets the given condition, that value will be added to those other statuses).
			/// </summary>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Feeds(Func<int, bool> condition, params TBaseStatus[] fedStatuses) => FeedsInternal(InstanceType.Feed, null, null, condition, fedStatuses);
			/// <summary>
			/// Declare that this status conditionally feeds one or more other statuses
			/// (i.e., if this status's value meets the given condition, that value will be added to those other statuses).
			/// </summary>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Feeds<TStatus>(Func<int, bool> condition, params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(InstanceType.Feed, null, null, condition, Convert(fedStatuses));
			/// <summary>
			/// Declare that this status feeds one or more other statuses a specific value
			/// (i.e., if this status's value is greater than zero, a specific value will be added to those other statuses).
			/// </summary>
			/// <param name="fedValue">The specific value to add to each listed status if this status is greater than zero</param>
			public void Feeds(int fedValue, params TBaseStatus[] fedStatuses) => FeedsInternal(InstanceType.Feed, null, fedValue, null, fedStatuses);
			/// <summary>
			/// Declare that this status feeds one or more other statuses a specific value
			/// (i.e., if this status's value is greater than zero, a specific value will be added to those other statuses).
			/// </summary>
			/// <param name="fedValue">The specific value to add to each listed status if this status is greater than zero</param>
			public void Feeds<TStatus>(int fedValue, params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(InstanceType.Feed, null, fedValue, null, Convert(fedStatuses));
			/// <summary>
			/// Declare that this status conditionally feeds one or more other statuses a specific value
			/// (i.e., if this status's value meets the given condition, a specific value will be added to those other statuses).
			/// </summary>
			/// <param name="fedValue">The specific value to add to each listed status if this status is greater than zero</param>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Feeds(int fedValue, Func<int, bool> condition, params TBaseStatus[] fedStatuses) => FeedsInternal(InstanceType.Feed, null, fedValue, condition, fedStatuses);
			/// <summary>
			/// Declare that this status conditionally feeds one or more other statuses a specific value
			/// (i.e., if this status's value meets the given condition, a specific value will be added to those other statuses).
			/// </summary>
			/// <param name="fedValue">The specific value to add to each listed status if this status is greater than zero</param>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Feeds<TStatus>(int fedValue, Func<int, bool> condition, params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(InstanceType.Feed, null, fedValue, condition, Convert(fedStatuses));
			/// <summary>
			/// Declare that this status feeds one or more other statuses using a value converter
			/// (i.e., this status's value is put into the converter, and (if nonzero) the result is added to those other statuses).
			/// </summary>
			/// <param name="converter">Return an int based on the current value of this status</param>
			public void Feeds(Converter converter, params TBaseStatus[] fedStatuses) => FeedsInternal(InstanceType.Feed, converter, null, null, fedStatuses);
			/// <summary>
			/// Declare that this status feeds one or more other statuses using a value converter
			/// (i.e., this status's value is put into the converter, and (if nonzero) the result is added to those other statuses).
			/// </summary>
			/// <param name="converter">Return an int based on the current value of this status</param>
			public void Feeds<TStatus>(Converter converter, params TStatus[] fedStatuses) where TStatus : struct => FeedsInternal(InstanceType.Feed, converter, null, null, Convert(fedStatuses));

			/// <summary>
			/// Declare that this status suppresses one or more other statuses
			/// (i.e., while this status's value is greater than zero, the value of those other statuses will always be zero).
			/// </summary>
			public void Suppresses(params TBaseStatus[] suppressedStatuses) => FeedsInternal(InstanceType.Suppress, null, null, null, suppressedStatuses);
			/// <summary>
			/// Declare that this status suppresses one or more other statuses
			/// (i.e., while this status's value is greater than zero, the value of those other statuses will always be zero).
			/// </summary>
			public void Suppresses<TStatus>(params TStatus[] suppressedStatuses) where TStatus : struct => FeedsInternal(InstanceType.Suppress, null, null, null, Convert(suppressedStatuses));
			/// <summary>
			/// Declare that this status conditionally suppresses one or more other statuses
			/// (i.e., while this status's value meets the given condition, the value of those other statuses will always be zero).
			/// </summary>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Suppresses(Func<int, bool> condition, params TBaseStatus[] suppressedStatuses) => FeedsInternal(InstanceType.Suppress, null, null, condition, suppressedStatuses);
			/// <summary>
			/// Declare that this status conditionally suppresses one or more other statuses
			/// (i.e., while this status's value meets the given condition, the value of those other statuses will always be zero).
			/// </summary>
			/// <param name="condition">Return true or false based on the current value of this status</param>
			public void Suppresses<TStatus>(Func<int, bool> condition, params TStatus[] suppressedStatuses) where TStatus : struct => FeedsInternal(InstanceType.Suppress, null, null, condition, Convert(suppressedStatuses));

			/// <summary>
			/// Declare that this status prevents one or more other statuses
			/// (i.e., while this status's value is greater than zero, new "Feed" StatusInstances can't be added for those other statuses).
			/// </summary>
			public void Prevents(params TBaseStatus[] preventedStatuses) => FeedsInternal(InstanceType.Prevent, null, null, null, preventedStatuses);
			/// <summary>
			/// Declare that this status prevents one or more other statuses
			/// (i.e., while this status's value is greater than zero, new "Feed" StatusInstances can't be added for those other statuses).
			/// </summary>
			public void Prevents<TStatus>(params TStatus[] preventedStatuses) where TStatus : struct => FeedsInternal(InstanceType.Prevent, null, null, null, Convert(preventedStatuses));
			/// <summary>
			/// Declare that this status conditionally prevents one or more other statuses
			/// (i.e., while this status's value meets the given condition, new "Feed" StatusInstances can't be added for those other statuses).
			/// </summary>
			public void Prevents(Func<int, bool> condition, params TBaseStatus[] preventedStatuses) => FeedsInternal(InstanceType.Prevent, null, null, condition, preventedStatuses);
			/// <summary>
			/// Declare that this status conditionally prevents one or more other statuses
			/// (i.e., while this status's value meets the given condition, new "Feed" StatusInstances can't be added for those other statuses).
			/// </summary>
			public void Prevents<TStatus>(Func<int, bool> condition, params TStatus[] preventedStatuses) where TStatus : struct => FeedsInternal(InstanceType.Prevent, null, null, condition, Convert(preventedStatuses));

			protected void FeedsInternal(InstanceType type, Converter converter,
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
						var pair = new StatusPair(status, fedStatus);
						rules.converters[type][pair] = converter;
					}
				}
			}
			/// <summary>
			/// Declare that this status is prevented when a given condition is met
			/// (i.e., while that condition is met, new "Feed" StatusInstances can't be added for this status).
			/// </summary>
			/// <param name="preventionCondition">Return true or false based on the current object</param>
			public void PreventedWhen(Func<TObject, TBaseStatus, bool> preventionCondition) {
				rules.extraPreventionConditions.AddUnique(status, preventionCondition);
			}
			internal StatusRules(StatusSystem<TObject> rules, TBaseStatus status) : base(rules, status, status) {
				this.rules = rules;
				this.status = status;
			}
		}
		/// <summary>Define a new rule for the given status</summary>
		public StatusRules this[TBaseStatus status] => new StatusRules(this, status);
		protected Dictionary<InstanceType, Aggregator> defaultAggs;
		/// <summary>
		/// The default value aggregator will be used to calculate the value of each status,
		/// unless that status has its own value aggregator defined.
		/// </summary>
		public Aggregator DefaultValueAggregator {
			get { return defaultAggs[InstanceType.Feed]; }
			set {
				if(value == null) throw new ArgumentNullException("value", "Default aggregators cannot be null.");
				ValidateAggregator(value);
				defaultAggs[InstanceType.Feed] = value;
			}
		}
		internal void ValidateAggregator(Aggregator agg) {
			if(agg(Enumerable.Empty<int>()) != 0) throw new ArgumentException("Aggregators must have a base value of 0.");
		}

		internal DefaultValueDictionary<TBaseStatus, Aggregator> valueAggs;
		internal EasyHashSet<TBaseStatus> SingleInstance { get; private set; }
		internal DefaultValueDictionary<TBaseStatus, Func<StatusTracker<TObject>, int>> getDerivedValue { get; private set; }

		internal MultiValueDictionary<TBaseStatus, TBaseStatus> statusesCancelledBy;
		internal MultiValueDictionary<TBaseStatus, TBaseStatus> statusesExtendedBy;
		internal MultiValueDictionary<TBaseStatus, TBaseStatus> statusesThatExtend;

		internal Dictionary<InstanceType, MultiValueDictionary<TBaseStatus, TBaseStatus>> statusesFedBy;

		internal Dictionary<InstanceType, Dictionary<StatusPair, Converter>> converters;
		internal void ValidateConverter(Converter conv) {
			if(conv(0) != 0) throw new ArgumentException("Converters must output 0 when input is 0.");
		}
		internal DefaultValueDictionary<StatusPair, Func<int, bool>> cancellationConditions;

		/// <summary>
		/// This handler can be used with overrides to hide a default message or effect.
		/// </summary>
		public readonly OnChangedHandler<TObject> DoNothing;
		/// <summary>
		/// The default aggregator. Adds all the values together.
		/// </summary>
		public readonly Aggregator Total;
		/// <summary>
		/// Returns 1 for positive values.
		/// </summary>
		public readonly Aggregator Bool;
		/// <summary>
		/// Finds the maximum value (or zero, if all the values are negative).
		/// </summary>
		public readonly Aggregator MaximumOrZero;

		internal DefaultValueDictionary<TBaseStatus, DefaultValueDictionary<StatusChange, OnChangedHandler<TObject>>> onChangedHandlers;
		internal MultiValueDictionary<TBaseStatus, Func<TObject, TBaseStatus, bool>> extraPreventionConditions;

		internal Aggregator GetAggregator(TBaseStatus status, InstanceType type) {
			if(type == InstanceType.Feed) {
				Aggregator agg = valueAggs[status];
				if(agg != null) return agg;
			}
			return defaultAggs[type];
		}
		void IHandlers<TObject>.SetHandler(TBaseStatus status, TBaseStatus overridden, bool increased, bool effect, OnChangedHandler<TObject> handler) {
			if(!onChangedHandlers.ContainsKey(status)) {
				onChangedHandlers.Add(status, new DefaultValueDictionary<StatusChange, OnChangedHandler<TObject>>());
			}
			onChangedHandlers[status][new StatusChange(overridden, increased, effect)] = handler;
		}
		OnChangedHandler<TObject> IHandlers<TObject>.GetHandler(TBaseStatus status, TBaseStatus overridden, bool increased, bool effect) {
			if(!onChangedHandlers.ContainsKey(status)) return null;
			return onChangedHandlers[status][new StatusChange(overridden, increased, effect)];
		}

		internal DefaultValueDictionary<int, OverrideSet<TObject>> overrideSets;
		/// <summary>Returns the OverrideSet of the chosen index.</summary>
		/// <param name="index">Any integer value of your choice can be used to refer to an OverrideSet.</param>
		public OverrideSet<TObject> GetOverrideSet(int index) {
			if(!overrideSets.ContainsKey(index)) overrideSets[index] = new OverrideSet<TObject>();
			return overrideSets[index];
		}

		internal DefaultValueDictionary<TBaseStatus, int> overrideSetsForStatuses;

		/// <summary>
		/// If set to true, no analysis of rule errors will be performed.
		/// Because analysis can be slow, this setting is recommended for release builds.
		/// </summary>
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
				var ruleChecker = new RuleChecker<TObject>(this);
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
		/// <summary>
		/// Analyze the rules that have been defined, and get a list of potential problems.
		/// </summary>
		public List<string> GetRuleErrorsAndWarnings() {
			VerifyConversions();
			var ruleChecker = new RuleChecker<TObject>(this);
			return ruleChecker.GetErrors(true);
		}

		/// <summary>
		/// Create a status tracker that'll use the rules already defined.
		/// Rules will be checked for errors when the first tracker is created (unless IgnoreRuleErrors is set to true).
		/// </summary>
		/// <param name="obj">The object associated with the newly created tracker (i.e., the tracker's "owner")</param>
		public StatusTracker<TObject> CreateStatusTracker(TObject obj) {
			return new StatusTracker<TObject>(obj, this);
		}
		/// <summary>
		/// Conveniently create a StatusInstance compatible with all trackers spawned from this object.
		/// </summary>
		/// <param name="status">The status to which the instance will add its value</param>
		/// <param name="value">The amount by which the instance will increase its status</param>
		/// <param name="cancelPriority">An instance with lower cancel priority will be cancelled before an instance with
		/// higher priority when Cancel() is called on this status.</param>
		/// <param name="type">
		/// The InstanceType determines whether the instance will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" StatusInstances are removed.)
		/// </param>
		public StatusInstance<TObject> CreateStatusInstance(TBaseStatus status, int value = 1, int cancelPriority = 0,
			InstanceType type = InstanceType.Feed, int? overrideSetIndex = null)
		{
			return new StatusInstance<TObject>(status, value, cancelPriority, type, overrideSetIndex);
		}
		/// <summary>
		/// Conveniently create a StatusInstance compatible with all trackers spawned from this object.
		/// </summary>
		/// <param name="status">The status to which the instance will add its value</param>
		/// <param name="value">The amount by which the instance will increase its status</param>
		/// <param name="cancelPriority">An instance with lower cancel priority will be cancelled before an instance with
		/// higher priority when Cancel() is called on this status.</param>
		/// <param name="type">
		/// The InstanceType determines whether the instance will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" StatusInstances are removed.)
		/// </param>
		public StatusInstance<TObject> CreateStatusInstance<TStatus>(TStatus status, int value = 1, int cancelPriority = 0,
			InstanceType type = InstanceType.Feed, int? overrideSetIndex = null)
			where TStatus : struct
		{
			return new StatusInstance<TObject>(Convert(status), value, cancelPriority, type, overrideSetIndex);
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
		public StatusSystem() {
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
			defaultAggs = new Dictionary<InstanceType, Func<IEnumerable<int>, int>>();
			defaultAggs[InstanceType.Feed] = Total;
			defaultAggs[InstanceType.Suppress] = Bool;
			defaultAggs[InstanceType.Prevent] = Bool;
			valueAggs = new DefaultValueDictionary<TBaseStatus, Aggregator>();
			SingleInstance = new EasyHashSet<TBaseStatus>();
			getDerivedValue = new DefaultValueDictionary<TBaseStatus, Func<StatusTracker<TObject>, int>>();
			statusesCancelledBy = new MultiValueDictionary<TBaseStatus, TBaseStatus>();
			statusesExtendedBy = new MultiValueDictionary<TBaseStatus, TBaseStatus>();
			statusesThatExtend = new MultiValueDictionary<TBaseStatus, TBaseStatus>();
			statusesFedBy = new Dictionary<InstanceType, MultiValueDictionary<TBaseStatus, TBaseStatus>>();
			converters = new Dictionary<InstanceType, Dictionary<StatusPair, Func<int, int>>>();
			cancellationConditions = new DefaultValueDictionary<StatusPair, Func<int, bool>>();
			foreach(InstanceType type in Enum.GetValues(typeof(InstanceType))) {
				statusesFedBy[type] = new MultiValueDictionary<TBaseStatus, TBaseStatus>();
				converters[type] = new Dictionary<StatusPair, Func<int, int>>();
			}
			onChangedHandlers = new DefaultValueDictionary<TBaseStatus, DefaultValueDictionary<StatusChange, OnChangedHandler<TObject>>>();
			extraPreventionConditions = new MultiValueDictionary<TBaseStatus, Func<TObject, TBaseStatus, bool>>();
			overrideSets = new DefaultValueDictionary<int, OverrideSet<TObject>>();
			overrideSetsForStatuses = new DefaultValueDictionary<TBaseStatus, int>();
		}
	}
}
