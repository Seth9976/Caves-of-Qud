using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000232 RID: 562
	internal abstract class EventCallbackFunctorBase : IDisposable
	{
		// Token: 0x060010BD RID: 4285
		public abstract void Invoke(EventBase evt);

		// Token: 0x060010BE RID: 4286
		public abstract void UnregisterCallback(CallbackEventHandler target, TrickleDown useTrickleDown);

		// Token: 0x060010BF RID: 4287
		public abstract void Dispose();

		// Token: 0x060010C0 RID: 4288
		public abstract bool IsEquivalentTo(long eventTypeId, Delegate callback);

		// Token: 0x04000A00 RID: 2560
		public long eventTypeId;

		// Token: 0x04000A01 RID: 2561
		public InvokePolicy invokePolicy;
	}
}
