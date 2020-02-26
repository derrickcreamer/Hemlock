using System;
using System.Collections.Generic;
using System.Linq;
using UtilityCollections;

namespace Hemlock {

	using Converter = Func<int, int>;
	using TBaseStatus = System.Int32;

	public class StatusTracker<TObject> {
		protected TObject obj;
		protected StatusSystem<TObject> rules;

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
				AddSource(new Source<TObject>(status, value)); // Otherwise, create a new one.
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

		private Dictionary<SourceType, MultiValueDictionary<TBaseStatus, Source<TObject>>> sources;

		private Dictionary<SourceType, Dictionary<TBaseStatus, Dictionary<TBaseStatus, int>>> internalFeeds;

		private List<DefaultValueDictionary<StatusChange, OnChangedHandler<TObject>>> changeStack;

		internal StatusTracker(TObject obj, StatusSystem<TObject> rules) {
			this.obj = obj;
			this.rules = rules;
			if(rules != null) rules.TrackerCreated = true;
			currentActualValues = new DefaultValueDictionary<TBaseStatus, int>();
			currentRaw = new Dictionary<SourceType, DefaultValueDictionary<TBaseStatus, int>>();
			sources = new Dictionary<SourceType, MultiValueDictionary<TBaseStatus, Source<TObject>>>();
			internalFeeds = new Dictionary<SourceType, Dictionary<TBaseStatus, Dictionary<TBaseStatus, int>>>();
			changeStack = new List<DefaultValueDictionary<StatusChange, OnChangedHandler<TObject>>>();
			foreach(SourceType type in Enum.GetValues(typeof(SourceType))) {
				currentRaw[type] = new DefaultValueDictionary<TBaseStatus, int>();
				sources[type] = new MultiValueDictionary<TBaseStatus, Source<TObject>>();
				internalFeeds[type] = new Dictionary<TBaseStatus, Dictionary<TBaseStatus, int>>();
			}
		}
		/// <param name="stream">Serialize to this stream. The stream will NOT be automatically closed.</param>
		/// <param name="statusInstanceCallback">
		/// This optional method will be called after a status instance inside this StatusTracker is serialized,
		/// so that the calling code can serialize any required additional data associated with it.
		/// (Note that the underlying stream is accessible in the callback through the BinaryWriter.BaseStream property.)
		/// </param>
		public void Serialize(System.IO.Stream stream, Action<System.IO.BinaryWriter, Source<TObject>, StatusTracker<TObject>> statusInstanceCallback = null){
			using(var writer = new System.IO.BinaryWriter(stream, System.Text.Encoding.UTF8, true)){
				Serialize(writer, statusInstanceCallback);
			}
		}
		/// <param name="writer">Serialize using this BinaryWriter. The underlying stream will NOT be automatically closed.</param>
		/// <param name="statusInstanceCallback">
		/// This optional method will be called after a status instance inside this StatusTracker is serialized,
		/// so that the calling code can serialize any required additional data associated with it.
		/// (Note that the underlying stream is accessible in the callback through the BinaryWriter.BaseStream property.)
		/// </param>
		public void Serialize(System.IO.BinaryWriter writer, Action<System.IO.BinaryWriter, Source<TObject>, StatusTracker<TObject>> statusInstanceCallback = null){
			if(changeStack.Count > 0) throw new InvalidOperationException("Cannot serialize while status values are being updated.");
			if(writer == null) throw new ArgumentNullException(nameof(writer));
			writer.Write(GenerateNoEffects);
			writer.Write(GenerateNoMessages);
			writer.Write(currentActualValues.Count);
			foreach(KeyValuePair<TBaseStatus, int> pair in currentActualValues){
				writer.Write(pair.Key);
				writer.Write(pair.Value);
			}
			SerializeCurrentRaw(SourceType.Feed, writer);
			SerializeCurrentRaw(SourceType.Prevent, writer);
			SerializeCurrentRaw(SourceType.Suppress, writer);
			SerializeStatusInstances(SourceType.Feed, writer, statusInstanceCallback);
			SerializeStatusInstances(SourceType.Prevent, writer, statusInstanceCallback);
			SerializeStatusInstances(SourceType.Suppress, writer, statusInstanceCallback);
			SerializeInternalFeeds(SourceType.Feed, writer);
			SerializeInternalFeeds(SourceType.Prevent, writer);
			SerializeInternalFeeds(SourceType.Suppress, writer);
		}
		/// <param name="stream">Deserialize from this stream. The stream will NOT be automatically closed.</param>
		/// <param name="statusInstanceCallback">
		/// This optional method will be called after a status instance inside this StatusTracker is deserialized,
		/// so that the calling code can deserialize any additional data it previously serialized.
		/// (Note that the underlying stream is accessible in the callback through the BinaryReader.BaseStream property.)
		/// </param>
		public void Deserialize(System.IO.Stream stream, Action<System.IO.BinaryReader, Source<TObject>, TObject> statusInstanceCallback = null){
			using(var reader = new System.IO.BinaryReader(stream, System.Text.Encoding.UTF8, true)){
				Deserialize(reader, statusInstanceCallback);
			}
		}
		/// <param name="reader">Deserialize from this BinaryReader. The underlying stream will NOT be automatically closed.</param>
		/// <param name="statusInstanceCallback">
		/// This optional method will be called after a status instance inside this StatusTracker is deserialized,
		/// so that the calling code can deserialize any additional data it previously serialized.
		/// (Note that the underlying stream is accessible in the callback through the BinaryReader.BaseStream property.)
		/// </param>
		public void Deserialize(System.IO.BinaryReader reader, Action<System.IO.BinaryReader, Source<TObject>, TObject> statusInstanceCallback = null){
			if(reader == null) throw new ArgumentNullException(nameof(reader));
			GenerateNoEffects = reader.ReadBoolean();
			GenerateNoMessages = reader.ReadBoolean();
			int currentActualCount = reader.ReadInt32();
			for(int i=0;i<currentActualCount;++i){
				int key = reader.ReadInt32();
				int value = reader.ReadInt32();
				currentActualValues.Add(key, value);
			}
			DeserializeCurrentRaw(currentRaw[SourceType.Feed], reader);
			DeserializeCurrentRaw(currentRaw[SourceType.Prevent], reader);
			DeserializeCurrentRaw(currentRaw[SourceType.Suppress], reader);
			DeserializeStatusInstances(sources[SourceType.Feed], reader, statusInstanceCallback);
			DeserializeStatusInstances(sources[SourceType.Prevent], reader, statusInstanceCallback);
			DeserializeStatusInstances(sources[SourceType.Suppress], reader, statusInstanceCallback);
			DeserializeInternalFeeds(internalFeeds[SourceType.Feed], reader);
			DeserializeInternalFeeds(internalFeeds[SourceType.Prevent], reader);
			DeserializeInternalFeeds(internalFeeds[SourceType.Suppress], reader);
		}
		private void SerializeCurrentRaw(SourceType type, System.IO.BinaryWriter writer){
			DefaultValueDictionary<TBaseStatus, int> dict = currentRaw[type];
			writer.Write(dict.Count);
			foreach(KeyValuePair<TBaseStatus, int> pair in dict){
				writer.Write(pair.Key);
				writer.Write(pair.Value);
			}
		}
		private static void DeserializeCurrentRaw(DefaultValueDictionary<TBaseStatus, int> dict, System.IO.BinaryReader reader){
			int count = reader.ReadInt32();
			for(int i=0;i<count;++i){
				int key = reader.ReadInt32();
				int value = reader.ReadInt32();
				dict.Add(key, value);
			}
		}
		private void SerializeStatusInstances(SourceType type, System.IO.BinaryWriter writer, Action<System.IO.BinaryWriter, Source<TObject>, StatusTracker<TObject>> statusInstanceCallback){
			MultiValueDictionary<TBaseStatus, Source<TObject>> dict = sources[type];
			writer.Write(dict.GetAllKeys().Count());
			foreach(KeyValuePair<TBaseStatus, IEnumerable<Source<TObject>>> pair in dict){
				writer.Write(pair.Key);
				List<Source<TObject>> values = pair.Value.ToList();
				writer.Write(values.Count);
				foreach(Source<TObject> instance in values){
					writer.Write(instance.Status);
					writer.Write((int)instance.SourceType);
					writer.Write(instance.Value);
					writer.Write(instance.Priority);
					if(instance.OverrideSetIndex == null){
						writer.Write(false);
					}
					else{
						writer.Write(true);
						writer.Write(instance.OverrideSetIndex.Value);
					}
					statusInstanceCallback?.Invoke(writer, instance, this);
				}
			}
		}
		private void DeserializeStatusInstances(MultiValueDictionary<TBaseStatus, Source<TObject>> dict, System.IO.BinaryReader reader, Action<System.IO.BinaryReader, Source<TObject>, TObject> statusInstanceCallback){
			int allKeysCount = reader.ReadInt32();
			for(int i=0;i<allKeysCount;++i){
				int key = reader.ReadInt32();
				int count = reader.ReadInt32();
				for(int j=0;j<count;++j){
					int status = reader.ReadInt32();
					int type = reader.ReadInt32();
					int value = reader.ReadInt32();
					int priority = reader.ReadInt32();
					int? overrideIdx = null;
					if(reader.ReadBoolean()){
						overrideIdx = reader.ReadInt32();
					}
					Source<TObject> instance = new Source<TObject>(status, value, priority, (SourceType)type, overrideIdx);
					instance.tracker = this;
					dict.Add(key, instance);
					statusInstanceCallback?.Invoke(reader, instance, obj);
				}
			}
		}
		private void SerializeInternalFeeds(SourceType type, System.IO.BinaryWriter writer){
			Dictionary<TBaseStatus, Dictionary<TBaseStatus, int>> dict = internalFeeds[type];
			writer.Write(dict.Count);
			foreach(KeyValuePair<TBaseStatus, Dictionary<TBaseStatus, int>> topLevelPair in dict){
				writer.Write(topLevelPair.Key);
				writer.Write(topLevelPair.Value.Count);
				foreach(KeyValuePair<TBaseStatus, int> pair in topLevelPair.Value){
					writer.Write(pair.Key);
					writer.Write(pair.Value);
				}
			}
		}
		private static void DeserializeInternalFeeds(Dictionary<TBaseStatus, Dictionary<TBaseStatus, int>> dict, System.IO.BinaryReader reader){
			int topLevelCount = reader.ReadInt32();
			for(int i=0;i<topLevelCount;++i){
				int topLevelKey = reader.ReadInt32();
				dict.Add(topLevelKey, new Dictionary<TBaseStatus, int>());
				int count = reader.ReadInt32();
				for(int j=0;j<count;++j){
					int key = reader.ReadInt32();
					int value = reader.ReadInt32();
					dict[topLevelKey].Add(key, value);
				}
			}
		}
		/// <summary>
		/// Conveniently create a Source compatible with this tracker. Does not add the Source to the tracker automatically.
		/// </summary>
		/// <param name="status">The status to which the Source will add its value</param>
		/// <param name="value">The amount by which the Source will increase its status</param>
		/// <param name="priority">A Source with lower priority will be cancelled before a Source with
		/// higher priority when Cancel() is called on this status.</param>
		/// <param name="type">
		/// The SourceType determines whether the Source will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" Sources are removed.)
		/// </param>
		public Source<TObject> CreateSource(TBaseStatus status, int value = 1, int priority = 0,
			SourceType type = SourceType.Feed, int? overrideSetIndex = null)
		{
			return new Source<TObject>(status, value, priority, type, overrideSetIndex);
		}
		/// <summary>
		/// Conveniently create a Source compatible with this tracker. Does not add the Source to the tracker automatically.
		/// </summary>
		/// <param name="status">The status to which the Source will add its value</param>
		/// <param name="value">The amount by which the Source will increase its status</param>
		/// <param name="priority">A Source with lower priority will be cancelled before a Source with
		/// higher priority when Cancel() is called on this status.</param>
		/// <param name="type">
		/// The SourceType determines whether the Source will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" Sources are removed.)
		/// </param>
		public Source<TObject> CreateSource<TStatus>(TStatus status, int value = 1, int priority = 0,
			SourceType type = SourceType.Feed, int? overrideSetIndex = null)
			where TStatus : struct
		{
			return new Source<TObject>(Convert(status), value, priority, type, overrideSetIndex);
		}
		/// <summary>
		/// Add a Source to this tracker, updating the value of the status associated with the given Source.
		/// </summary>
		public bool AddSource(Source<TObject> source) {
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
		public Source<TObject> Add(TBaseStatus status, int value = 1, int priority = 0,
			SourceType type = SourceType.Feed, int? overrideSetIndex = null)
		{
			var source = new Source<TObject>(status, value, priority, type, overrideSetIndex);
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
		public Source<TObject> Add<TStatus>(TStatus status, int value = 1, int priority = 0,
			SourceType type = SourceType.Feed, int? overrideSetIndex = null)
			where TStatus : struct
		{
			var source = new Source<TObject>(Convert(status), value, priority, type, overrideSetIndex);
			if(AddSource(source)) return source;
			else return null;
		}
		/// <summary>
		/// Remove a Source from this tracker, updating the value of the status associated with the given Source.
		/// Returns true if successful, or false if the Source wasn't in this tracker.
		/// </summary>
		public bool RemoveSource(Source<TObject> source) {
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
		private OnChangedHandler<TObject> GetHandler(TBaseStatus status, bool increased, bool effect) {
			var change = new StatusChange(status, increased, effect);
			OnChangedHandler<TObject> result;
			foreach(var dict in changeStack) {
				if(dict.TryGetValue(change, out result)) return result;
			}
			return null;
		}
		internal void CheckSourceChanged(Source<TObject> source) {
			bool stacked = source.overrideSetIndex != null;
			if(stacked) {
				OverrideSet<TObject> overrideSet = rules.overrideSets[source.overrideSetIndex.Value];
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
				OverrideSet<TObject> overrideSet = rules.overrideSets[overrideSetIndex];
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
						var pair = new StatusPair(status, cancelledStatus);
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
				var pair = new StatusPair(status, fedStatus);
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
}
