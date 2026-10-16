using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200074A RID: 1866
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetExtrinsicWeightEvent : IWeightEvent
	{
		// Token: 0x06004D38 RID: 19768 RVA: 0x0017C63C File Offset: 0x0017A83C
		public GetExtrinsicWeightEvent()
		{
			this.ID = GetExtrinsicWeightEvent.ID;
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x0017C64F File Offset: 0x0017A84F
		public static int CountPool()
		{
			if (GetExtrinsicWeightEvent.Pool != null)
			{
				return GetExtrinsicWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x0017C664 File Offset: 0x0017A864
		public static void ResetPool()
		{
			while (GetExtrinsicWeightEvent.PoolCounter > 0)
			{
				GetExtrinsicWeightEvent.Pool[--GetExtrinsicWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x0017C68C File Offset: 0x0017A88C
		public static void ResetTo(ref GetExtrinsicWeightEvent E)
		{
			MinEvent.ResetTo<GetExtrinsicWeightEvent>(E, GetExtrinsicWeightEvent.Pool, ref GetExtrinsicWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0017C6A2 File Offset: 0x0017A8A2
		public static GetExtrinsicWeightEvent FromPool()
		{
			return MinEvent.FromPool<GetExtrinsicWeightEvent>(ref GetExtrinsicWeightEvent.Pool, ref GetExtrinsicWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x0017C6B8 File Offset: 0x0017A8B8
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x0017C6CC File Offset: 0x0017A8CC
		public static GetExtrinsicWeightEvent FromPool(GameObject Object, double BaseWeight, double Weight)
		{
			GetExtrinsicWeightEvent getExtrinsicWeightEvent = GetExtrinsicWeightEvent.FromPool();
			getExtrinsicWeightEvent.Object = Object;
			getExtrinsicWeightEvent.BaseWeight = BaseWeight;
			getExtrinsicWeightEvent.Weight = Weight;
			return getExtrinsicWeightEvent;
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x0017C6E8 File Offset: 0x0017A8E8
		public static GetExtrinsicWeightEvent FromPool(IWeightEvent PE)
		{
			GetExtrinsicWeightEvent getExtrinsicWeightEvent = GetExtrinsicWeightEvent.FromPool();
			getExtrinsicWeightEvent.Object = PE.Object;
			getExtrinsicWeightEvent.BaseWeight = PE.BaseWeight;
			getExtrinsicWeightEvent.Weight = PE.Weight;
			return getExtrinsicWeightEvent;
		}

		// Token: 0x04002570 RID: 9584
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetExtrinsicWeightEvent), null, new MinEvent.EventPoolCount(GetExtrinsicWeightEvent.CountPool), new MinEvent.EventPoolReset(GetExtrinsicWeightEvent.ResetPool));

		// Token: 0x04002571 RID: 9585
		private static List<GetExtrinsicWeightEvent> Pool;

		// Token: 0x04002572 RID: 9586
		private static int PoolCounter;
	}
}
