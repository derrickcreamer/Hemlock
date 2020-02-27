using System;
using UtilityCollections;

namespace Hemlock {

	using TBaseStatus = System.Int32;

	public class OverrideSet<TObject> : IHandlers<TObject> {
		internal DefaultValueDictionary<StatusChange, OnChangedHandler<TObject>> onChangedOverrides;

		/// <summary>
		/// Override message or effect behavior whenever a change in *this* status or source (i.e., the status
		/// or source which is using this override set) causes a change in *another* status.
		/// </summary>
		/// <param name="overridden">The status whose message/effect behavior should be overridden</param>
		public StatusSystem<TObject>.StatusHandlers Overrides(TBaseStatus overridden)
			=> new StatusSystem<TObject>.StatusHandlers(this, default(TBaseStatus), overridden);
		/// <summary>
		/// Override message or effect behavior whenever a change in *this* status or source (i.e., the status
		/// or source which is using this override set) causes a change in *another* status.
		/// </summary>
		/// <param name="overridden">The status whose message/effect behavior should be overridden</param>
		public StatusSystem<TObject>.StatusHandlers Overrides<TStatus>(TStatus overridden) where TStatus : struct
			=> new StatusSystem<TObject>.StatusHandlers(this, default(TBaseStatus), Convert(overridden));
		void IHandlers<TObject>.SetHandler(TBaseStatus ignored, TBaseStatus overridden, bool increased, bool effect, OnChangedHandler<TObject> handler) {
			if(onChangedOverrides == null) onChangedOverrides = new DefaultValueDictionary<StatusChange, OnChangedHandler<TObject>>();
			onChangedOverrides[new StatusChange(overridden, increased, effect)] = handler;
		}
		OnChangedHandler<TObject> IHandlers<TObject>.GetHandler(TBaseStatus status, TBaseStatus ignored, bool increased, bool effect) {
			if(onChangedOverrides == null) return null;
			return onChangedOverrides[new StatusChange(status, increased, effect)];
		}
		protected static TBaseStatus Convert<TStatus>(TStatus status) where TStatus : struct {
			return StatusConverter<TStatus, TBaseStatus>.Convert(status);
		}
	}
}
