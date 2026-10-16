using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000651 RID: 1617
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AdjustWeightEvent : IWeightEvent
	{
		// Token: 0x0600460A RID: 17930 RVA: 0x001658F8 File Offset: 0x00163AF8
		public AdjustWeightEvent()
		{
			this.ID = AdjustWeightEvent.ID;
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x0016590B File Offset: 0x00163B0B
		public static int CountPool()
		{
			if (AdjustWeightEvent.Pool != null)
			{
				return AdjustWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x00165920 File Offset: 0x00163B20
		public static void ResetPool()
		{
			while (AdjustWeightEvent.PoolCounter > 0)
			{
				AdjustWeightEvent.Pool[--AdjustWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x00165948 File Offset: 0x00163B48
		public static void ResetTo(ref AdjustWeightEvent E)
		{
			MinEvent.ResetTo<AdjustWeightEvent>(E, AdjustWeightEvent.Pool, ref AdjustWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x0016595E File Offset: 0x00163B5E
		public static AdjustWeightEvent FromPool()
		{
			return MinEvent.FromPool<AdjustWeightEvent>(ref AdjustWeightEvent.Pool, ref AdjustWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x00165974 File Offset: 0x00163B74
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x00165988 File Offset: 0x00163B88
		public static AdjustWeightEvent FromPool(GameObject Object, double BaseWeight, double Weight)
		{
			AdjustWeightEvent adjustWeightEvent = AdjustWeightEvent.FromPool();
			adjustWeightEvent.Object = Object;
			adjustWeightEvent.BaseWeight = BaseWeight;
			adjustWeightEvent.Weight = Weight;
			return adjustWeightEvent;
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x001659A4 File Offset: 0x00163BA4
		public static AdjustWeightEvent FromPool(IWeightEvent PE)
		{
			AdjustWeightEvent adjustWeightEvent = AdjustWeightEvent.FromPool();
			adjustWeightEvent.Object = PE.Object;
			adjustWeightEvent.BaseWeight = PE.BaseWeight;
			adjustWeightEvent.Weight = PE.Weight;
			return adjustWeightEvent;
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x001659CF File Offset: 0x00163BCF
		public void AdjustWeight(double Factor)
		{
			this.Weight *= Factor;
		}

		// Token: 0x0400213D RID: 8509
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AdjustWeightEvent), null, new MinEvent.EventPoolCount(AdjustWeightEvent.CountPool), new MinEvent.EventPoolReset(AdjustWeightEvent.ResetPool));

		// Token: 0x0400213E RID: 8510
		private static List<AdjustWeightEvent> Pool;

		// Token: 0x0400213F RID: 8511
		private static int PoolCounter;
	}
}
