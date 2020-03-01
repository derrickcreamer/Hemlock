using System;

namespace Hemlock {

	using TBaseStatus = System.Int32;

	public class StatusInstance<TObject> {
		/// <summary>
		/// The status to which this instance will add its value
		/// </summary>
		public readonly TBaseStatus Status;
		/// <summary>
		/// The InstanceType determines whether this instance will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" StatusInstances are removed.)
		/// </summary>
		public readonly InstanceType InstanceType;
		internal StatusTracker<TObject> tracker;
		private int internalValue;
		/// <summary>
		/// The value added to this instance's status. If this property is changed after this StatusInstance has been added
		/// to a tracker, the status's value will update automatically.
		/// </summary>
		public int Value {
			get { return internalValue; }
			set {
				if(value != internalValue) {
					internalValue = value;
					tracker?.CheckInstanceChanged(this);
				}
			}
		}
		/// <summary>
		/// When a status is cancelled, its StatusInstances are removed one at a time, in order of priority - lowest first.
		/// This can be useful to ensure a specific ordering for value changes during cancellation.
		/// </summary>
		public int CancelPriority { get; set; }
		/// <summary>
		/// Test whether this StatusInstance's status is a valid value for the type of the "status" argument.
		/// If so, load its value into "status" and return true.
		/// (For enums, test whether this value is in the defined range for that enum.)
		/// </summary>
		public bool TryGetStatus<TStatus>(out TStatus status) where TStatus : struct {
			if(StatusConverter<TBaseStatus, TStatus>.Convert != null) {
				status = StatusConverter<TBaseStatus, TStatus>.Convert(this.Status);
			}
			else {
				try {
					status = (TStatus)(object)this.Status;
				}
				catch(InvalidCastException) {
					status = default(TStatus);
					return false;
				}
			}
			if(typeof(TStatus).IsEnum) {
				try {
					return Enum.IsDefined(typeof(TStatus), this.Status);
				}
				catch(ArgumentException) {
					return false;
				}
			}
			return true; // I guess this should return true. If it isn't an enum, all we know is that the cast was successful.
		}
		/// <summary>
		/// Casts this instance's Status property to a chosen type, regardless of whether it falls into the defined range of enum types.
		/// </summary>
		public TStatus GetStatus<TStatus>() {
			if(StatusConverter<TBaseStatus, TStatus>.Convert != null) {
				return StatusConverter<TBaseStatus, TStatus>.Convert(this.Status);
			}
			try {
				return (TStatus)(object)this.Status;
			}
			catch(InvalidCastException) {
				throw new InvalidOperationException($"Couldn't convert value {this.Status} to type {typeof(TStatus).FullName}");
			}
		}
		internal int? overrideSetIndex;
		public int? OverrideSetIndex => overrideSetIndex;

		/// <summary>Serialize this StatusInstance, which must NOT currently belong to a StatusTracker.</summary>
		/// <param name="stream">Serialize to this stream. The stream will NOT be automatically closed.</param>
		public void Serialize(System.IO.Stream stream){
			if(tracker != null) throw new InvalidOperationException("Can't directly serialize a StatusInstance inside a StatusTracker. Serialize the entire StatusTracker instead.");
			using(var writer = new System.IO.BinaryWriter(stream, System.Text.Encoding.UTF8, true)){
				SerializeInternal(writer);
			}
		}
		/// <summary>Serialize this StatusInstance, which must NOT currently belong to a StatusTracker.</summary>
		/// <param name="writer">Serialize using this BinaryWriter. The underlying stream will NOT be automatically closed.</param>
		public void Serialize(System.IO.BinaryWriter writer){
			if(tracker != null) throw new InvalidOperationException("Can't directly serialize a StatusInstance inside a StatusTracker. Serialize the entire StatusTracker instead.");
			if(writer == null) throw new ArgumentNullException(nameof(writer));
			SerializeInternal(writer);
		}
		internal void SerializeInternal(System.IO.BinaryWriter writer){
			writer.Write(Status);
			writer.Write((int)InstanceType);
			writer.Write(Value);
			writer.Write(CancelPriority);
			if(OverrideSetIndex == null){
				writer.Write(false);
			}
			else{
				writer.Write(true);
				writer.Write(OverrideSetIndex.Value);
			}
		}
		/// <summary>Deserialize and return a StatusInstance that was serialized outside of a StatusTracker.</summary>
		/// <param name="stream">Deserialize from this stream. The stream will NOT be automatically closed.</param>
		public static StatusInstance<TObject> Deserialize(System.IO.Stream stream){
			using(var reader = new System.IO.BinaryReader(stream, System.Text.Encoding.UTF8, true)){
				return Deserialize(reader);
			}
		}
		/// <summary>Deserialize and return a StatusInstance that was serialized outside of a StatusTracker.</summary>
		/// <param name="reader">Deserialize from this BinaryReader. The underlying stream will NOT be automatically closed.</param>
		public static StatusInstance<TObject> Deserialize(System.IO.BinaryReader reader){
			if(reader == null) throw new ArgumentNullException(nameof(reader));
			int status = reader.ReadInt32();
			int type = reader.ReadInt32();
			int value = reader.ReadInt32();
			int priority = reader.ReadInt32();
			int? overrideIdx = null;
			if(reader.ReadBoolean()){
				overrideIdx = reader.ReadInt32();
			}
			StatusInstance<TObject> instance = new StatusInstance<TObject>(status, value, priority, (InstanceType)type, overrideIdx);
			return instance;
		}
		/// <summary>
		/// Create a (shallow) copy of this StatusInstance. If any non-null arguments are provided to this method,
		/// those values will be used in the copy.
		/// </summary>
		/// <param name="value">If provided, the copy will be created with this value.</param>
		/// <param name="cancelPriority">If provided, the copy will be created with this priority.</param>
		/// <param name="type">If provided, the copy will be created with this InstanceType.</param>
		/// <param name="overrideSetIndex">If provided, the copy will be created with this override set index.</param>
		public StatusInstance<TObject> Clone(int? value = null, int? cancelPriority = null, InstanceType? type = null, int? overrideSetIndex = null) {
			return new StatusInstance<TObject>(this, value, cancelPriority, type, overrideSetIndex);
		}
		/// <param name="status">The status to which this instance will add its value</param>
		/// <param name="value">The amount by which this instance will increase its status</param>
		/// <param name="cancelPriority">An instance with lower cancel priority will be cancelled before an instance with
		/// higher priority when Cancel() is called on this status.</param>
		/// <param name="type">
		/// The InstanceType determines whether this instance will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" StatusInstances are removed.)
		/// </param>
		public StatusInstance(TBaseStatus status, int value = 1, int cancelPriority = 0, InstanceType type = InstanceType.Feed, int? overrideSetIndex = null) {
			Status = status;
			internalValue = value;
			CancelPriority = cancelPriority;
			InstanceType = type;
			this.overrideSetIndex = overrideSetIndex;
		}
		protected StatusInstance(StatusInstance<TObject> copyFrom, int? value = null, int? cancelPriority = null, InstanceType? type = null, int? overrideSetIndex = null) {
			if(copyFrom == null) throw new ArgumentNullException("copyFrom");
			Status = copyFrom.Status;
			if(value == null) internalValue = copyFrom.internalValue;
			else internalValue = value.Value;
			if(cancelPriority == null) CancelPriority = copyFrom.CancelPriority;
			else CancelPriority = cancelPriority.Value;
			if(type == null) InstanceType = copyFrom.InstanceType;
			else InstanceType = type.Value;
			if(overrideSetIndex == null) this.overrideSetIndex = copyFrom.overrideSetIndex;
			else this.overrideSetIndex = overrideSetIndex;
		}
	}
}
