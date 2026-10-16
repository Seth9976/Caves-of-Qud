using System;

namespace XRL.World
{
	// Token: 0x02000863 RID: 2147
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class SyncRenderEvent : PooledEvent<SyncRenderEvent>
	{
		// Token: 0x0600557C RID: 21884 RVA: 0x00198ACA File Offset: 0x00196CCA
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x00198AD3 File Offset: 0x00196CD3
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x00198AE2 File Offset: 0x00196CE2
		public static SyncRenderEvent FromPool(GameObject Object)
		{
			SyncRenderEvent syncRenderEvent = PooledEvent<SyncRenderEvent>.FromPool();
			syncRenderEvent.Object = Object;
			return syncRenderEvent;
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x00198AF0 File Offset: 0x00196CF0
		public static void Send(GameObject Object)
		{
			if (Object.WantEvent(PooledEvent<SyncRenderEvent>.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(SyncRenderEvent.FromPool(Object));
			}
		}

		// Token: 0x04002B17 RID: 11031
		public GameObject Object;
	}
}
