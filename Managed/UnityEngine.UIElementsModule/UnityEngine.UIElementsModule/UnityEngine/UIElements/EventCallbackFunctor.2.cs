using System;
using UnityEngine.Pool;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000234 RID: 564
	internal class EventCallbackFunctor<TEventType, TCallbackArgs> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0004889F File Offset: 0x00046A9F
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x000488A7 File Offset: 0x00046AA7
		internal TCallbackArgs userArgs { get; set; }

		// Token: 0x060010CA RID: 4298 RVA: 0x000488B0 File Offset: 0x00046AB0
		public static EventCallbackFunctor<TEventType, TCallbackArgs> GetPooled(long eventTypeId, EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, InvokePolicy invokePolicy = InvokePolicy.Default)
		{
			EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = GenericPool<EventCallbackFunctor<TEventType, TCallbackArgs>>.Get();
			eventCallbackFunctor.eventTypeId = eventTypeId;
			eventCallbackFunctor.invokePolicy = invokePolicy;
			eventCallbackFunctor.userArgs = userArgs;
			eventCallbackFunctor.m_Callback = callback;
			return eventCallbackFunctor;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000488E8 File Offset: 0x00046AE8
		public override void Dispose()
		{
			this.eventTypeId = 0L;
			this.invokePolicy = InvokePolicy.Default;
			this.userArgs = default(TCallbackArgs);
			this.m_Callback = null;
			GenericPool<EventCallbackFunctor<TEventType, TCallbackArgs>>.Release(this);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00048924 File Offset: 0x00046B24
		public override void Invoke(EventBase evt)
		{
			using (new EventDebuggerLogCall(this.m_Callback, evt))
			{
				this.m_Callback(evt as TEventType, this.userArgs);
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00048980 File Offset: 0x00046B80
		public override void UnregisterCallback(CallbackEventHandler target, TrickleDown useTrickleDown)
		{
			target.UnregisterCallback<TEventType, TCallbackArgs>(this.m_Callback, useTrickleDown);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00048994 File Offset: 0x00046B94
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback)
		{
			return this.eventTypeId == eventTypeId && this.m_Callback == callback;
		}

		// Token: 0x04000A03 RID: 2563
		private EventCallback<TEventType, TCallbackArgs> m_Callback;
	}
}
