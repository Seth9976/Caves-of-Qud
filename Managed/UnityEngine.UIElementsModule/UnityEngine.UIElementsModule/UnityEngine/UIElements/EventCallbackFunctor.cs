using System;
using UnityEngine.Pool;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000233 RID: 563
	internal class EventCallbackFunctor<TEventType> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x060010C2 RID: 4290 RVA: 0x000487B0 File Offset: 0x000469B0
		public static EventCallbackFunctor<TEventType> GetPooled(long eventTypeId, EventCallback<TEventType> callback, InvokePolicy invokePolicy = InvokePolicy.Default)
		{
			EventCallbackFunctor<TEventType> eventCallbackFunctor = GenericPool<EventCallbackFunctor<TEventType>>.Get();
			eventCallbackFunctor.eventTypeId = eventTypeId;
			eventCallbackFunctor.invokePolicy = invokePolicy;
			eventCallbackFunctor.m_Callback = callback;
			return eventCallbackFunctor;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000487DE File Offset: 0x000469DE
		public override void Dispose()
		{
			this.eventTypeId = 0L;
			this.invokePolicy = InvokePolicy.Default;
			this.m_Callback = null;
			GenericPool<EventCallbackFunctor<TEventType>>.Release(this);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00048800 File Offset: 0x00046A00
		public override void Invoke(EventBase evt)
		{
			using (new EventDebuggerLogCall(this.m_Callback, evt))
			{
				this.m_Callback(evt as TEventType);
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00048858 File Offset: 0x00046A58
		public override void UnregisterCallback(CallbackEventHandler target, TrickleDown useTrickleDown)
		{
			target.UnregisterCallback<TEventType>(this.m_Callback, useTrickleDown);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0004886C File Offset: 0x00046A6C
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback)
		{
			return this.eventTypeId == eventTypeId && this.m_Callback == callback;
		}

		// Token: 0x04000A02 RID: 2562
		private EventCallback<TEventType> m_Callback;
	}
}
