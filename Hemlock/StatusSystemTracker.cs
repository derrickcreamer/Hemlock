using System;
using System.Collections.Generic;
using System.Linq;
using UtilityCollections;

namespace Hemlock {

	using Converter = Func<int, int>;

	public class BaseStatusTracker<TObject, TBaseStatus> where TBaseStatus : struct {
		protected TObject obj;
		protected BaseStatusSystem<TObject, TBaseStatus> rules;

		/// <summary>
		/// If set to true, message events (OnChanged handlers) will not happen when a status is changed.
		/// </summary>
		public bool GenerateNoMessages { get; set; }
		/// <summary>
		/// If set to true, effect events (OnChanged handlers) will not happen when a status is changed.
		/// </summary>
		public bool GenerateNoEffects { get; set; }

		private DefaultValueDictionary<TBaseStatus, int> currentActualValues;
		/// <summary>
		/// Retrieve the current int value of the given status.
		/// The status's value can also be directly set, but only if the SingleSource property is true for this status.
		/// </summary>
		public int this[TBaseStatus status] {
			get { return currentActualValues[status]; }
			set {
				if(!rules.SingleSource[status]) throw new InvalidOperationException("'SingleSource' must be true in order to set a value directly.");
				foreach(var source in sources[SourceType.Feed][status]) {
					source.Value = value;
					return; // If any sources exist, change the value of the first one, then return.
				}
				AddSource(new Source<TObject, TBaseStatus>(status, value)); // Otherwise, create a new one.
			}
		}
		protected static TBaseStatus Convert<TStatus>(TStatus status) where TStatus : struct {
			return StatusConverter<TStatus, TBaseStatus>.Convert(status);
		}
		/// <summary>
		/// Returns true if the current value of the given status is greater than zero.
		/// </summary>
		public bool HasStatus(TBaseStatus status) => currentActualValues[status] > 0;
		/// <summary>
		/// Returns true if the current value of the given status is greater than zero.
		/// </summary>
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
		/// <summary>
		/// Conveniently create a Source compatible with this tracker.
		/// </summary>
		/// <param name="status">The status to which the Source will add its value</param>
		/// <param name="value">The amount by which the Source will increase its status</param>
		/// <param name="priority">A Source with lower priority will be cancelled before a Source with
		/// higher priority when Cancel() is called on this status.</param>
		/// <param name="type">
		/// The SourceType determines whether the Source will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" Sources are removed.)
		/// </param>
		public Source<TObject, TBaseStatus> CreateSource(TBaseStatus status, int value = 1, int priority = 0,
			SourceType type = SourceType.Feed, int? overrideSetIndex = null) {
			return new Source<TObject, TBaseStatus>(status, value, priority, type, overrideSetIndex);
		}
		/// <summary>
		/// Conveniently create a Source compatible with this tracker.
		/// </summary>
		/// <param name="status">The status to which the Source will add its value</param>
		/// <param name="value">The amount by which the Source will increase its status</param>
		/// <param name="priority">A Source with lower priority will be cancelled before a Source with
		/// higher priority when Cancel() is called on this status.</param>
		/// <param name="type">
		/// The SourceType determines whether the Source will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" Sources are removed.)
		/// </param>
		public Source<TObject, TBaseStatus, TStatus> CreateSource<TStatus>(TStatus status, int value = 1,
			int priority = 0, SourceType type = SourceType.Feed, int? overrideSetIndex = null)
			where TStatus : struct
		{
			return new Source<TObject, TBaseStatus, TStatus>(status, value, priority, type, overrideSetIndex);
		}
		/// <summary>
		/// Add a Source to this tracker, updating the value of the status associated with the given Source.
		/// </summary>
		public bool AddSource(Source<TObject, TBaseStatus> source) {
			if(source == null) throw new ArgumentNullException();
			if(source.tracker != null && source.tracker != this) throw new InvalidOperationException("Already added to another tracker");
			TBaseStatus status = source.Status;
			SourceType type = source.SourceType;
			if(type == SourceType.Feed) {
				if(currentRaw[SourceType.Prevent][status] > 0) return false;
				var preventableStatuses = new List<TBaseStatus> { status }.Concat(rules.statusesExtendedBy[status]);
				foreach(var preventableStatus in preventableStatuses) {
					if(rules.extraPreventionConditions.AnyValues(preventableStatus)) {
						foreach(var condition in rules.extraPreventionConditions[preventableStatus]) {
							if(condition(obj, preventableStatus)) return false;
						}
					}
				}
				if(rules.SingleSource[status]) sources[SourceType.Feed].Clear(status);
			}
			if(sources[type].AddUnique(status, source)) {
				source.tracker = this;
				CheckSourceChanged(source);
				return true;
			}
			else return false;
		}
		/// <summary>
		/// Add a Source to this tracker, updating the value of the status associated with the given Source.
		/// </summary>
		public bool AddSource<TStatus>(Source<TObject, TBaseStatus, TStatus> source) where TStatus : struct {
			return AddSource(source as Source<TObject, TBaseStatus>);
		}
		/// <summary>
		/// Create a new Source and add it to this tracker, updating the value of the given status.
		/// Returns the newly created Source, if successfully added, or null, if not.
		/// </summary>
		/// <param name="status">The status to which the Source will add its value</param>
		/// <param name="value">The amount by which the Source will increase the given status</param>
		/// <param name="priority">A Source with lower priority will be cancelled before a Source with
		/// higher priority when Cancel() is called on its status.</param>
		/// <param name="type">
		/// The SourceType determines whether the Source will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" Sources are removed.)
		/// </param>
		public Source<TObject, TBaseStatus> Add(TBaseStatus status, int value = 1, int priority = 0,
			SourceType type = SourceType.Feed, int? overrideSetIndex = null)
		{
			var source = new Source<TObject, TBaseStatus>(status, value, priority, type, overrideSetIndex);
			if(AddSource(source)) return source;
			else return null;
		}
		/// <summary>
		/// Create a new Source and add it to this tracker, updating the value of the given status.
		/// Returns the newly created Source, if successfully added, or null, if not.
		/// </summary>
		/// <param name="status">The status to which the Source will add its value</param>
		/// <param name="value">The amount by which the Source will increase the given status</param>
		/// <param name="priority">A Source with lower priority will be cancelled before a Source with
		/// higher priority when Cancel() is called on its status.</param>
		/// <param name="type">
		/// The SourceType determines whether the Source will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" Sources are removed.)
		/// </param>
		public Source<TObject, TBaseStatus, TStatus> Add<TStatus>(TStatus status, int value = 1, int priority = 0,
			SourceType type = SourceType.Feed, int? overrideSetIndex = null)
			where TStatus : struct
		{
			var source = new Source<TObject, TBaseStatus, TStatus>(status, value, priority, type, overrideSetIndex);
			if(AddSource(source)) return source;
			else return null;
		}
		/// <summary>
		/// Remove a Source from this tracker, updating the value of the status associated with the given Source.
		/// Returns true if successful, or false if the Source wasn't in this tracker.
		/// </summary>
		public bool RemoveSource(Source<TObject, TBaseStatus> source) {
			if(source == null) throw new ArgumentNullException();
			TBaseStatus status = source.Status;
			SourceType type = source.SourceType;
			if(sources[type].Remove(status, source)) {
				source.tracker = null;
				CheckSourceChanged(source);
				return true;
			}
			else return false;
		}
		/// <summary>
		/// Cancel the given status, removing all "Feed" sources that have been added to this tracker for this status.
		/// (This will return the value of this status to zero, unless other statuses are feeding this one.)
		/// </summary>
		public void Cancel(TBaseStatus status) {
			foreach(var source in sources[SourceType.Feed][status].OrderBy(x => x.Priority)) {
				RemoveSource(source);
			}
			foreach(TBaseStatus extendingStatus in rules.statusesThatExtend[status]) Cancel(extendingStatus);
		}
		/// <summary>
		/// Cancel the given status, removing all "Feed" sources that have been added to this tracker for this status.
		/// (This will return the value of this status to zero, unless other statuses are feeding this one.)
		/// </summary>
		public void Cancel<TStatus>(TStatus status) where TStatus : struct => Cancel(Convert(status));
		private OnChangedHandler<TObject, TBaseStatus> GetHandler(TBaseStatus status, bool increased, bool effect) {
			var change = new StatusChange<TBaseStatus>(status, increased, effect);
			OnChangedHandler<TObject, TBaseStatus> result;
			foreach(var dict in changeStack) {
				if(dict.TryGetValue(change, out result)) return result;
			}
			return null;
		}
		internal void CheckSourceChanged(Source<TObject, TBaseStatus> source) {
			bool stacked = source.overrideSetIndex != null;
			if(stacked) {
				OverrideSet<TObject, TBaseStatus> overrideSet = rules.overrideSets[source.overrideSetIndex.Value];
				if(overrideSet == null) throw new InvalidOperationException($"Override set {source.overrideSetIndex.Value} does not exist");
				changeStack.Add(overrideSet.onChangedOverrides);
			}
			CheckRawChanged(source.Status, source.SourceType);
			if(stacked) changeStack.RemoveAt(changeStack.Count - 1);
		}
		private void CheckRawChanged(TBaseStatus status, SourceType type) {
			bool stacked = false;
			if(rules.overrideSetsForStatuses.TryGetValue(status, out int overrideSetIndex)) {
				stacked = true;
				OverrideSet<TObject, TBaseStatus> overrideSet = rules.overrideSets[overrideSetIndex];
				if(overrideSet == null) throw new InvalidOperationException($"Override set {overrideSetIndex} does not exist");
				changeStack.Add(overrideSet.onChangedOverrides);
			}
			else if(rules.onChangedHandlers[status] != null) {
				stacked = true;
				changeStack.Add(rules.onChangedHandlers[status]);
			}
			var values = sources[type][status].Select(x => x.Value);
			if(internalFeeds[type].ContainsKey(status)) values = values.Concat(internalFeeds[type][status].Values);
			IEnumerable<TBaseStatus> upstreamStatuses; // 'Upstream' and 'downstream' statuses change depending on the SourceType.
			IEnumerable<TBaseStatus> downstreamStatuses; // Value changes to a status are also applied to statuses that this one extends...
			if(type == SourceType.Feed) {
				upstreamStatuses = rules.statusesThatExtend[status];
				downstreamStatuses = rules.statusesExtendedBy[status];
			}
			else {
				upstreamStatuses = rules.statusesExtendedBy[status]; // ...while negative changes to a status go the other way,
				downstreamStatuses = rules.statusesThatExtend[status]; // being applied to statuses that extend this one.
			}
			foreach(TBaseStatus otherStatus in upstreamStatuses) {
				values = values.Concat(sources[type][otherStatus].Select(x => x.Value));
				if(internalFeeds[type].ContainsKey(otherStatus)) values = values.Concat(internalFeeds[type][otherStatus].Values);
			}
			int newValue = rules.GetAggregator(status, type)(values);
			int oldValue = currentRaw[type][status];
			if(newValue != oldValue) {
				currentRaw[type][status] = newValue;
				if(type == SourceType.Feed || type == SourceType.Suppress) CheckActualValueChanged(status);
			}
			foreach(TBaseStatus otherStatus in downstreamStatuses) {
				CheckRawChanged(otherStatus, type);
			}
			if(stacked) changeStack.RemoveAt(changeStack.Count - 1);
		}
		private void CheckActualValueChanged(TBaseStatus status) {
			int newValue;
			if(currentRaw[SourceType.Suppress][status] > 0) newValue = 0;
			else newValue = currentRaw[SourceType.Feed][status];
			int oldValue = currentActualValues[status];
			if(newValue != oldValue) {
				currentActualValues[status] = newValue;
				bool increased = newValue > oldValue;
				if(!GenerateNoMessages) GetHandler(status, increased, false)?.Invoke(obj, status, oldValue, newValue);
				if(!GenerateNoEffects) GetHandler(status, increased, true)?.Invoke(obj, status, oldValue, newValue);
				UpdateFeed(status, SourceType.Feed, newValue);
				if(increased) {
					foreach(TBaseStatus cancelledStatus in rules.statusesCancelledBy[status]) {
						var pair = new StatusPair<TBaseStatus>(status, cancelledStatus);
						var condition = rules.cancellationConditions[pair]; // if a condition exists, it must return true for the
						if(condition == null || condition(newValue)) Cancel(cancelledStatus); // status to be cancelled.
					}
				}
				UpdateFeed(status, SourceType.Suppress, newValue); // Cancellations happen before suppression to prevent some infinite loops
				UpdateFeed(status, SourceType.Prevent, newValue);
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
}
