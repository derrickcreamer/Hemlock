using System;
using UtilityCollections;

namespace Hemlock {
	public class Source<TObject, TBaseStatus> : IHandlers<TObject, TBaseStatus> where TBaseStatus : struct {
		public readonly TBaseStatus Status;
		public readonly SourceType SourceType;
		internal event Action<Source<TObject, TBaseStatus>> OnValueChanged;
		private int internalValue;
		public int Value {
			get { return internalValue; }
			set {
				if(value != internalValue) {
					internalValue = value;
					OnValueChanged?.Invoke(this);
				}
			}
		}
		public int Priority { get; set; }
		//todo: xml/docs, explain this one
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
		public BaseStatusSystem<TObject, TBaseStatus>.StatusHandlers Overrides(TBaseStatus overridden) => new BaseStatusSystem<TObject, TBaseStatus>.StatusHandlers(this, Status, overridden);
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
		public Source(TBaseStatus status, int value = 1, int priority = 0, SourceType type = SourceType.Value) {
			Status = status;
			internalValue = value;
			Priority = priority;
			SourceType = type;
		}
		public Source(Source<TObject, TBaseStatus> copyFrom, int? value = null, int? priority = null, SourceType? type = null) {
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
		public Source(TStatus status, int value = 1, int priority = 0, SourceType type = SourceType.Value)
			: base(Convert(status), value, priority, type)
		{
			this.Status = status;
			this.BaseStatus = Convert(status);
		}
		new public readonly TStatus Status;
		public readonly TBaseStatus BaseStatus;
	}
}
