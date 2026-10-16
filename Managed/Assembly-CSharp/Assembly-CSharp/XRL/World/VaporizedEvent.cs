using System;

namespace XRL.World
{
	// Token: 0x02000877 RID: 2167
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class VaporizedEvent : PooledEvent<VaporizedEvent>
	{
		// Token: 0x0600560C RID: 22028 RVA: 0x0019A1E3 File Offset: 0x001983E3
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x0019A1EC File Offset: 0x001983EC
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.By = null;
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x0019A202 File Offset: 0x00198402
		public static VaporizedEvent FromPool(GameObject Object, GameObject By = null)
		{
			VaporizedEvent vaporizedEvent = PooledEvent<VaporizedEvent>.FromPool();
			vaporizedEvent.Object = Object;
			vaporizedEvent.By = By;
			return vaporizedEvent;
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x0019A218 File Offset: 0x00198418
		public static bool Check(GameObject Object, GameObject By = null)
		{
			return (!Object.WantEvent(PooledEvent<VaporizedEvent>.ID, MinEvent.CascadeLevel) || Object.HandleEvent(VaporizedEvent.FromPool(Object, By))) && (!Object.HasRegisteredEvent("Vaporized") || Object.FireEvent(Event.New("Vaporized", "Object", Object, "By", By)));
		}

		// Token: 0x04002B59 RID: 11097
		public GameObject Object;

		// Token: 0x04002B5A RID: 11098
		public GameObject By;
	}
}
