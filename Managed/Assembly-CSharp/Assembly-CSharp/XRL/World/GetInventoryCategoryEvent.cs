using System;

namespace XRL.World
{
	// Token: 0x02000759 RID: 1881
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetInventoryCategoryEvent : PooledEvent<GetInventoryCategoryEvent>
	{
		// Token: 0x06004DAA RID: 19882 RVA: 0x0017D871 File Offset: 0x0017BA71
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x0017D87A File Offset: 0x0017BA7A
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Category = "";
			this.AsIfKnown = false;
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x0017D89B File Offset: 0x0017BA9B
		public static GetInventoryCategoryEvent FromPool(GameObject Object, string Category = "")
		{
			GetInventoryCategoryEvent getInventoryCategoryEvent = PooledEvent<GetInventoryCategoryEvent>.FromPool();
			getInventoryCategoryEvent.Object = Object;
			getInventoryCategoryEvent.Category = Category;
			getInventoryCategoryEvent.AsIfKnown = false;
			return getInventoryCategoryEvent;
		}

		// Token: 0x040025AF RID: 9647
		public GameObject Object;

		// Token: 0x040025B0 RID: 9648
		public string Category = "";

		// Token: 0x040025B1 RID: 9649
		public bool AsIfKnown;
	}
}
