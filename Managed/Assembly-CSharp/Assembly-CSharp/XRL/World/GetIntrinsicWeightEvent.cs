using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000756 RID: 1878
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetIntrinsicWeightEvent : IWeightEvent
	{
		// Token: 0x06004D8E RID: 19854 RVA: 0x0017D574 File Offset: 0x0017B774
		public GetIntrinsicWeightEvent()
		{
			this.ID = GetIntrinsicWeightEvent.ID;
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x0017D587 File Offset: 0x0017B787
		public static int CountPool()
		{
			if (GetIntrinsicWeightEvent.Pool != null)
			{
				return GetIntrinsicWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x0017D59C File Offset: 0x0017B79C
		public static void ResetPool()
		{
			while (GetIntrinsicWeightEvent.PoolCounter > 0)
			{
				GetIntrinsicWeightEvent.Pool[--GetIntrinsicWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x0017D5C4 File Offset: 0x0017B7C4
		public static void ResetTo(ref GetIntrinsicWeightEvent E)
		{
			MinEvent.ResetTo<GetIntrinsicWeightEvent>(E, GetIntrinsicWeightEvent.Pool, ref GetIntrinsicWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x0017D5DA File Offset: 0x0017B7DA
		public static GetIntrinsicWeightEvent FromPool()
		{
			return MinEvent.FromPool<GetIntrinsicWeightEvent>(ref GetIntrinsicWeightEvent.Pool, ref GetIntrinsicWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x0017D5F0 File Offset: 0x0017B7F0
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x0017D604 File Offset: 0x0017B804
		public static GetIntrinsicWeightEvent FromPool(GameObject Object, double BaseWeight, double Weight)
		{
			GetIntrinsicWeightEvent getIntrinsicWeightEvent = GetIntrinsicWeightEvent.FromPool();
			getIntrinsicWeightEvent.Object = Object;
			getIntrinsicWeightEvent.BaseWeight = BaseWeight;
			getIntrinsicWeightEvent.Weight = Weight;
			return getIntrinsicWeightEvent;
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x0017D620 File Offset: 0x0017B820
		public static GetIntrinsicWeightEvent FromPool(GameObject Object, double Weight)
		{
			GetIntrinsicWeightEvent getIntrinsicWeightEvent = GetIntrinsicWeightEvent.FromPool();
			getIntrinsicWeightEvent.Object = Object;
			getIntrinsicWeightEvent.BaseWeight = Weight;
			getIntrinsicWeightEvent.Weight = Weight;
			return getIntrinsicWeightEvent;
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x0017D63C File Offset: 0x0017B83C
		public static GetIntrinsicWeightEvent FromPool(GameObject Object, int Weight)
		{
			return GetIntrinsicWeightEvent.FromPool(Object, (double)Weight);
		}

		// Token: 0x040025A6 RID: 9638
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetIntrinsicWeightEvent), null, new MinEvent.EventPoolCount(GetIntrinsicWeightEvent.CountPool), new MinEvent.EventPoolReset(GetIntrinsicWeightEvent.ResetPool));

		// Token: 0x040025A7 RID: 9639
		private static List<GetIntrinsicWeightEvent> Pool;

		// Token: 0x040025A8 RID: 9640
		private static int PoolCounter;
	}
}
