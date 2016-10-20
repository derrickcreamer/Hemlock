using System;
using System.Collections.Generic;
using System.Linq;
using UtilityCollections;

namespace Hemlock {

	using Converter = Func<int, int>;

	public class BaseStatusTracker<TObject, TBaseStatus> where TBaseStatus : struct {
		protected TObject obj;
		protected BaseStatusSystem<TObject, TBaseStatus> rules;

		public bool GenerateNoMessages { get; set; }
		public bool GenerateNoEffects { get; set; }

		private DefaultValueDictionary<TBaseStatus, int> currentActualValues;
		public int this[TBaseStatus status] {
			get { return currentActualValues[status]; }
			set {
				if(!rules.SingleSource[status]) throw new InvalidOperationException("'SingleSource' must be true in order to set a value directly.");
				foreach(var source in sources[SourceType.Value][status]) {
					source.Value = value;
					return; // If any sources exist, change the value of the first one, then return.
				}
				AddSource(new Source<TObject, TBaseStatus>(status, value)); // Otherwise, create a new one.
			}
		}
		protected static TBaseStatus Convert<TStatus>(TStatus status) where TStatus : struct {
			return StatusConverter<TStatus, TBaseStatus>.Convert(status);
		}
		public bool HasStatus(TBaseStatus status) => currentActualValues[status] > 0;
		public bool HasStatus<TStatus>(TStatus status) where TStatus : struct => HasStatus(Convert(status));

		private Dictionary<SourceType, DefaultValueDictionary<TBaseStatus, int>> currentRaw;
		//public bool IsSuppressed(TStatus status) => currentRaw[SourceType.Suppression][status] > 0;
		//public bool IsPrevented(TStatus status) => currentRaw[SourceType.Prevention][status] > 0;

		private Dictionary<SourceType, MultiValueDictionary<TBaseStatus, Source<TObject, TBaseStatus>>> sources;

		private Dictionary<SourceType, Dictionary<TBaseStatus, Dictionary<TBaseStatus, int>>> internalFeeds;

		private List<DefaultValueDictionary<StatusChange<TBaseStatus>, OnChangedHandler<TObject, TBaseStatus>>> changeStack;

		internal BaseStatusTracker(TObject obj, BaseStatusSystem<TObject, TBaseStatus> rules) {
			this.obj = obj;
			this.rules = rules;
			if(rules != null) rules.TrackerCreated = true;
			currentActualValues = new DefaultValueDictionary<TBaseStatus, int>();
			currentRaw = new Dictionary<SourceType, DefaultValueDictionary<TBaseStatus, int>>();
			sources = new Dictionary<SourceType, MultiValueDictionary<TBaseStatus, Source<TObject, TBaseStatus>>>();
			internalFeeds = new Dictionary<SourceType, Dictionary<TBaseStatus, Dictionary<TBaseStatus, int>>>();
			changeStack = new List<DefaultValueDictionary<StatusChange<TBaseStatus>, OnChangedHandler<TObject, TBaseStatus>>>();
			foreach(SourceType type in Enum.GetValues(typeof(SourceType))) {
				currentRaw[type] = new DefaultValueDictionary<TBaseStatus, int>();
				sources[type] = new MultiValueDictionary<TBaseStatus, Source<TObject, TBaseStatus>>();
				internalFeeds[type] = new Dictionary<TBaseStatus, Dictionary<TBaseStatus, int>>();
			}
		}
		public Source<TObject, TBaseStatus> CreateSource(TBaseStatus status, int value = 1, int priority = 0, SourceType type = SourceType.Value) {
			return new Source<TObject, TBaseStatus>(status, value, priority, type);
		}
		public Source<TObject, TBaseStatus, TStatus> CreateSource<TStatus>(
			TStatus status, int value = 1, int priority = 0, SourceType type = SourceType.Value)
			where TStatus : struct
		{
			return new Source<TObject, TBaseStatus, TStatus>(status, value, priority, type);
		}
		public bool AddSource(Source<TObject, TBaseStatus> source) {
			if(source == null) throw new ArgumentNullException();
			TBaseStatus status = source.Status;
			SourceType type = source.SourceType;
			if(type == SourceType.Value) {
				if(currentRaw[SourceType.Prevention][status] > 0) return false;
				var preventableStatuses = new List<TBaseStatus> { status }.Concat(rules.statusesExtendedBy[status]);
				foreach(var preventableStatus in preventableStatuses) {
					if(rules.extraPreventionConditions.AnyValues(preventableStatus)) {
						foreach(var condition in rules.extraPreventionConditions[preventableStatus]) {
							if(condition(obj, preventableStatus)) return false;
						}
					}
				}
				if(rules.SingleSource[status]) sources[SourceType.Value].Clear(status);
			}
			if(sources[type].AddUnique(status, source)) {
				source.OnValueChanged += CheckSourceChanged;
				CheckSourceChanged(source);
				return true;
			}
			else return false;
		}
		//todo: definitely need xml comments for these methods
		public Source<TObject, TBaseStatus> Add(TBaseStatus status, int value = 1, int priority = 0, SourceType type = SourceType.Value) {
			var source = new Source<TObject, TBaseStatus>(status, value, priority, type);
			if(AddSource(source)) return source;
			else return null;
		}
		public Source<TObject, TBaseStatus, TStatus> Add<TStatus>(
			TStatus status, int value = 1, int priority = 0, SourceType type = SourceType.Value)
			where TStatus : struct
		{
			var source = new Source<TObject, TBaseStatus, TStatus>(status, value, priority, type);
			if(AddSource(source)) return source;
			else return null;
		}
		public bool RemoveSource(Source<TObject, TBaseStatus> source) {
			if(source == null) throw new ArgumentNullException();
			TBaseStatus status = source.Status;
			SourceType type = source.SourceType;
			if(sources[type].Remove(status, source)) {
				source.OnValueChanged -= CheckSourceChanged;
				CheckSourceChanged(source);
				return true;
			}
			else return false;
		}
		public void Cancel(TBaseStatus status) {
			foreach(var source in sources[SourceType.Value][status].OrderBy(x => x.Priority)) {
				RemoveSource(source);
			}
			foreach(TBaseStatus extendingStatus in rules.statusesThatExtend[status]) Cancel(extendingStatus);
		}
		public void Cancel<TStatus>(TStatus status) where TStatus : struct => Cancel(Convert(status));
		private OnChangedHandler<TObject, TBaseStatus> GetHandler(TBaseStatus status, bool increased, bool effect) {
			var change = new StatusChange<TBaseStatus>(status, increased, effect);
			OnChangedHandler<TObject, TBaseStatus> result;
			foreach(var dict in changeStack) {
				if(dict.TryGetValue(change, out result)) return result;
			}
			return null;
		}
		private void CheckSourceChanged(Source<TObject, TBaseStatus> source) {
			bool stacked = source.onChangedOverrides != null;
			if(stacked) changeStack.Add(source.onChangedOverrides);
			CheckRawChanged(source.Status, source.SourceType);
			if(stacked) changeStack.RemoveAt(changeStack.Count - 1);
		}
		private void CheckRawChanged(TBaseStatus status, SourceType type) {
			bool stacked = rules.onChangedHandlers[status] != null;
			if(stacked) changeStack.Add(rules.onChangedHandlers[status]);
			var values = sources[type][status].Select(x => x.Value);
			if(internalFeeds[type].ContainsKey(status)) values = values.Concat(internalFeeds[type][status].Values);
			IEnumerable<TBaseStatus> upstreamStatuses; //todo: be sure to explain how this works...
			IEnumerable<TBaseStatus> downstreamStatuses;
			if(type == SourceType.Value) {
				upstreamStatuses = rules.statusesThatExtend[status];
				downstreamStatuses = rules.statusesExtendedBy[status];
			}
			else {
				upstreamStatuses = rules.statusesExtendedBy[status];
				downstreamStatuses = rules.statusesThatExtend[status];
			}
			foreach(TBaseStatus otherStatus in upstreamStatuses) {
				values = values.Concat(sources[type][otherStatus].Select(x => x.Value));
				if(internalFeeds[type].ContainsKey(otherStatus)) values = values.Concat(internalFeeds[type][otherStatus].Values);
			}
			int newValue = rules.GetAggregator(status, type)(values);
			int oldValue = currentRaw[type][status];
			if(newValue != oldValue) {
				currentRaw[type][status] = newValue;
				if(type == SourceType.Value || type == SourceType.Suppression) CheckActualValueChanged(status);
			}
			foreach(TBaseStatus otherStatus in downstreamStatuses) {
				CheckRawChanged(otherStatus, type);
			}
			if(stacked) changeStack.RemoveAt(changeStack.Count - 1);
		}
		/*
		todo: put this text into a better comment format.

so, STATUS CHANGED looks like this:
Using stack, handle message & effect, in whatever order is right.
Using the rules, for each status that is fed by this one (for value, suppression, or prevention):
calculate the NEW FED VALUE from here to there, using the converter from the rules, if it exists. Otherwise, it's the same as the value.
get the OLD FED VALUE from here to there. If no source exists, it is 0. If a source exists, it is that source's value.
Compare those 2 values to see whether a change has occurred. If it has:
if the source exists, update its value.
If not, create a new source with that value, then add it to the target status.
If the value of this status just increased, using the rules, for each status that is cancelled by this one:
call Cancel on it, yeah?
		*/
		private void CheckActualValueChanged(TBaseStatus status) {
			int newValue;
			if(currentRaw[SourceType.Suppression][status] > 0) newValue = 0;
			else newValue = currentRaw[SourceType.Value][status];
			int oldValue = currentActualValues[status];
			if(newValue != oldValue) {
				currentActualValues[status] = newValue;
				bool increased = newValue > oldValue;
				if(!GenerateNoMessages) GetHandler(status, increased, false)?.Invoke(obj, status, oldValue, newValue);
				if(!GenerateNoEffects) GetHandler(status, increased, true)?.Invoke(obj, status, oldValue, newValue);
				UpdateFeed(status, SourceType.Value, newValue);
				if(increased) {
					foreach(TBaseStatus cancelledStatus in rules.statusesCancelledBy[status]) {
						var pair = new StatusPair<TBaseStatus>(status, cancelledStatus);
						var condition = rules.cancellationConditions[pair]; // if a condition exists, it must return true for the
						if(condition == null || condition(newValue)) Cancel(cancelledStatus); // status to be cancelled.
					}
				}
				UpdateFeed(status, SourceType.Suppression, newValue); // Cancellations happen before suppression to prevent some infinite loops
				UpdateFeed(status, SourceType.Prevention, newValue);
			}
		}
		private void UpdateFeed(TBaseStatus status, SourceType type, int newValue) {
			foreach(TBaseStatus fedStatus in rules.statusesFedBy[type][status]) {
				int newFedValue = newValue;
				var pair = new StatusPair<TBaseStatus>(status, fedStatus);
				Converter conv;
				if(rules.converters[type].TryGetValue(pair, out conv)) newFedValue = conv(newFedValue);
				int oldFedValue;
				Dictionary<TBaseStatus, int> fedValues;
				if(internalFeeds[type].TryGetValue(fedStatus, out fedValues)) fedValues.TryGetValue(status, out oldFedValue);
				else oldFedValue = 0;
				if(newFedValue != oldFedValue) {
					if(fedValues == null) {
						fedValues = new Dictionary<TBaseStatus, int>();
						fedValues.Add(status, newFedValue);
						internalFeeds[type].Add(fedStatus, fedValues);
					}
					else fedValues[status] = newFedValue;
					CheckRawChanged(fedStatus, type);
				}
			}
		}
	}
	public class StatusTracker<TObject> : BaseStatusTracker<TObject, int> {
		internal StatusTracker(TObject obj, BaseStatusSystem<TObject, int> rules) : base(obj, rules) { }
	}
	public class StatusTracker<TObject, TStatus1> : StatusTracker<TObject> where TStatus1 : struct {
		internal StatusTracker(TObject obj, BaseStatusSystem<TObject, int> rules) : base(obj, rules) { }
		public int this[TStatus1 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}
	public class StatusTracker<TObject, TStatus1, TStatus2> : StatusTracker<TObject, TStatus1> where TStatus1 : struct where TStatus2 : struct {
		internal StatusTracker(TObject obj, BaseStatusSystem<TObject, int> rules) : base(obj, rules) { }
		public int this[TStatus2 status] {
			get { return this[Convert(status)]; }
			set { this[Convert(status)] = value; }
		}
	}
}
