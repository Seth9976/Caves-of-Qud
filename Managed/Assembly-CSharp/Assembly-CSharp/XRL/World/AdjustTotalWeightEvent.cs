using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200064E RID: 1614
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AdjustTotalWeightEvent : IWeightEvent
	{
		// Token: 0x060045EF RID: 17903 RVA: 0x00165659 File Offset: 0x00163859
		public AdjustTotalWeightEvent()
		{
			this.ID = AdjustTotalWeightEvent.ID;
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x0016566C File Offset: 0x0016386C
		public static int CountPool()
		{
			if (AdjustTotalWeightEvent.Pool != null)
			{
				return AdjustTotalWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x00165681 File Offset: 0x00163881
		public static void ResetPool()
		{
			while (AdjustTotalWeightEvent.PoolCounter > 0)
			{
				AdjustTotalWeightEvent.Pool[--AdjustTotalWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x001656A9 File Offset: 0x001638A9
		public static void ResetTo(ref AdjustTotalWeightEvent E)
		{
			MinEvent.ResetTo<AdjustTotalWeightEvent>(E, AdjustTotalWeightEvent.Pool, ref AdjustTotalWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x001656BF File Offset: 0x001638BF
		public static AdjustTotalWeightEvent FromPool()
		{
			return MinEvent.FromPool<AdjustTotalWeightEvent>(ref AdjustTotalWeightEvent.Pool, ref AdjustTotalWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x001656D5 File Offset: 0x001638D5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x001656E9 File Offset: 0x001638E9
		public static AdjustTotalWeightEvent FromPool(GameObject Object, double BaseWeight, double Weight)
		{
			AdjustTotalWeightEvent adjustTotalWeightEvent = AdjustTotalWeightEvent.FromPool();
			adjustTotalWeightEvent.Object = Object;
			adjustTotalWeightEvent.BaseWeight = BaseWeight;
			adjustTotalWeightEvent.Weight = Weight;
			return adjustTotalWeightEvent;
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x00165705 File Offset: 0x00163905
		public static AdjustTotalWeightEvent FromPool(IWeightEvent PE)
		{
			AdjustTotalWeightEvent adjustTotalWeightEvent = AdjustTotalWeightEvent.FromPool();
			adjustTotalWeightEvent.Object = PE.Object;
			adjustTotalWeightEvent.BaseWeight = PE.BaseWeight;
			adjustTotalWeightEvent.Weight = PE.Weight;
			return adjustTotalWeightEvent;
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x00165730 File Offset: 0x00163930
		public void AdjustWeight(double Factor)
		{
			this.Weight *= Factor;
		}

		// Token: 0x04002134 RID: 8500
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AdjustTotalWeightEvent), null, new MinEvent.EventPoolCount(AdjustTotalWeightEvent.CountPool), new MinEvent.EventPoolReset(AdjustTotalWeightEvent.ResetPool));

		// Token: 0x04002135 RID: 8501
		private static List<AdjustTotalWeightEvent> Pool;

		// Token: 0x04002136 RID: 8502
		private static int PoolCounter;
	}
}
