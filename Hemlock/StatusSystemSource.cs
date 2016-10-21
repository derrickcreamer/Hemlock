using System;
using UtilityCollections;

namespace Hemlock {
	public class Source<TObject, TBaseStatus> : IHandlers<TObject, TBaseStatus> where TBaseStatus : struct {
		/// <summary>
		/// The status to which this Source will add its value
		/// </summary>
		public readonly TBaseStatus Status;
		/// <summary>
		/// The SourceType determines whether this Source will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" Sources are removed.)
		/// </summary>
		public readonly SourceType SourceType;
		internal event Action<Source<TObject, TBaseStatus>> OnValueChanged;
		private int internalValue;
		/// <summary>
		/// The value added to this Source's status. If this property is changed after this Source has been added
		/// to a tracker, the status's value will update automatically.
		/// </summary>
		public int Value {
			get { return internalValue; }
			set {
				if(value != internalValue) {
					internalValue = value;
					OnValueChanged?.Invoke(this);
				}
			}
		}
		/// <summary>
		/// Priority is important only during cancellation.  When a status is cancelled, its Sources are removed
		/// one at a time, in order of priority - lowest first.
		/// </summary>
		public int Priority { get; set; }
		/// <summary>
		/// Test whether this Source's status is a valid value for the type of the "status" argument.
		/// If so, load its value into "status" and return true.
		/// (For enums, test whether this value is in the defined range for that enum.)
		/// </summary>
		public bool TryGetStatus<TStatus>(out TStatus status) where TStatus : struct {
			if(StatusConverter<TBaseStatus, TStatus>.Convert != null) {
				status = StatusConverter<TBaseStatus, TStatus>.Convert(this.Status);
				return true;
			}
			try {
				status = (TStatus)(object)this.Status;
			}
			catch(InvalidCastException) {
				status = default(TStatus);
				return false;
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
		internal DefaultValueDictionary<StatusChange<TBaseStatus>, OnChangedHandler<TObject, TBaseStatus>> onChangedOverrides;
		/// <summary>
		/// Allow this Source to override message or effect behavior for its status or any other.
		/// (If the overridden status changes because of this Source, then this Source's overrides will be applied.)
		/// </summary>
		/// <param name="overridden">The status whose message/effect behavior should be overridden</param>
		public BaseStatusSystem<TObject, TBaseStatus>.StatusHandlers Overrides(TBaseStatus overridden) => new BaseStatusSystem<TObject, TBaseStatus>.StatusHandlers(this, Status, overridden);
		/// <summary>
		/// Allow this Source to override message or effect behavior for its status or any other.
		/// (If the overridden status changes because of this Source, then this Source's overrides will be applied.)
		/// </summary>
		/// <param name="overridden">The status whose message/effect behavior should be overridden</param>
		public BaseStatusSystem<TObject, TBaseStatus>.StatusHandlers Overrides<TStatus>(TStatus overridden) where TStatus : struct
			=> new BaseStatusSystem<TObject, TBaseStatus>.StatusHandlers(this, Status, Convert(overridden));
		void IHandlers<TObject, TBaseStatus>.SetHandler(TBaseStatus ignored, TBaseStatus overridden, bool increased, bool effect, OnChangedHandler<TObject, TBaseStatus> handler) {
			if(onChangedOverrides == null) onChangedOverrides = new DefaultValueDictionary<StatusChange<TBaseStatus>, OnChangedHandler<TObject, TBaseStatus>>();
			onChangedOverrides[new StatusChange<TBaseStatus>(overridden, increased, effect)] = handler;
		}
		OnChangedHandler<TObject, TBaseStatus> IHandlers<TObject, TBaseStatus>.GetHandler(TBaseStatus status, TBaseStatus ignored, bool increased, bool effect) {
			if(onChangedOverrides == null) return null;
			return onChangedOverrides[new StatusChange<TBaseStatus>(status, increased, effect)];
		}
		protected static TBaseStatus Convert<TStatus>(TStatus status) where TStatus : struct {
			return StatusConverter<TStatus, TBaseStatus>.Convert(status);
		}
		/// <summary>
		/// Create a (shallow) copy of this Source. If any non-null arguments are provided to this method,
		/// those values will be used in the copy.
		/// </summary>
		/// <param name="value">If provided, the copy will be created with this value.</param>
		/// <param name="priority">If provided, the copy will be created with this priority.</param>
		/// <param name="type">If provided, the copy will be created with this SourceType.</param>
		public Source<TObject, TBaseStatus> Clone(int? value = null, int? priority = null, SourceType? type = null) {
			return new Source<TObject, TBaseStatus>(this, value, priority, type);
		}
		/// <param name="status">The status to which this Source will add its value</param>
		/// <param name="value">The amount by which this Source will increase its status</param>
		/// <param name="priority">A Source with lower priority will be cancelled before a Source with
		/// higher priority when Cancel() is called on this status.</param>
		/// <param name="type">
		/// The SourceType determines whether this Source will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" Sources are removed.)
		/// </param>
		public Source(TBaseStatus status, int value = 1, int priority = 0, SourceType type = SourceType.Feed) {
			Status = status;
			internalValue = value;
			Priority = priority;
			SourceType = type;
		}
		protected Source(Source<TObject, TBaseStatus> copyFrom, int? value = null, int? priority = null, SourceType? type = null) {
			if(copyFrom == null) throw new ArgumentNullException("copyFrom");
			Status = copyFrom.Status;
			onChangedOverrides = copyFrom.onChangedOverrides;
			if(value == null) internalValue = copyFrom.internalValue;
			else internalValue = value.Value;
			if(priority == null) Priority = copyFrom.Priority;
			else Priority = priority.Value;
			if(type == null) SourceType = copyFrom.SourceType;
			else SourceType = type.Value;
		}
	}
	public class Source<TObject, TBaseStatus, TStatus> : Source<TObject, TBaseStatus>
		where TBaseStatus : struct
		where TStatus : struct
	{
		/// <summary>
		/// Create a (shallow) copy of this Source. If any non-null arguments are provided to this method,
		/// those values will be used in the copy.
		/// </summary>
		/// <param name="value">If provided, the copy will be created with this value.</param>
		/// <param name="priority">If provided, the copy will be created with this priority.</param>
		/// <param name="type">If provided, the copy will be created with this SourceType.</param>
		new public Source<TObject, TBaseStatus, TStatus> Clone(int? value = null, int? priority = null, SourceType? type = null) {
			return new Source<TObject, TBaseStatus, TStatus>(this, value, priority, type);
		}
		/// <param name="status">The status to which this Source will add its value</param>
		/// <param name="value">The amount by which this Source will increase its status</param>
		/// <param name="priority">A Source with lower priority will be cancelled before a Source with
		/// higher priority when Cancel() is called on this status.</param>
		/// <param name="type">
		/// The SourceType determines whether this Source will feed, suppress, or prevent its status.
		/// (Feed is the default and most common. When a status is cancelled, its "Feed" Sources are removed.)
		/// </param>
		public Source(TStatus status, int value = 1, int priority = 0, SourceType type = SourceType.Feed)
			: base(Convert(status), value, priority, type)
		{
			this.Status = status;
			this.BaseStatus = Convert(status);
		}
		protected Source(Source<TObject, TBaseStatus, TStatus> copyFrom, int? value = null, int? priority = null, SourceType? type = null)
			: base(copyFrom, value, priority, type)
		{
			this.Status = copyFrom.Status;
			this.BaseStatus = copyFrom.BaseStatus;
		}
		/// <summary>
		/// The status to which this Source will add its value
		/// </summary>
		new public readonly TStatus Status;
		/// <summary>
		/// The underlying status to which this Source will add its value
		/// </summary>
		public readonly TBaseStatus BaseStatus;
	}
}
