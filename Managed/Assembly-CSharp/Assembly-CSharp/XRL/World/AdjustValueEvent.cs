using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200064F RID: 1615
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AdjustValueEvent : IValueEvent
	{
		// Token: 0x060045F9 RID: 17913 RVA: 0x0016576F File Offset: 0x0016396F
		public AdjustValueEvent()
		{
			this.ID = AdjustValueEvent.ID;
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x00165782 File Offset: 0x00163982
		public static int CountPool()
		{
			if (AdjustValueEvent.Pool != null)
			{
				return AdjustValueEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x00165797 File Offset: 0x00163997
		public static void ResetPool()
		{
			while (AdjustValueEvent.PoolCounter > 0)
			{
				AdjustValueEvent.Pool[--AdjustValueEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x001657BF File Offset: 0x001639BF
		public static void ResetTo(ref AdjustValueEvent E)
		{
			MinEvent.ResetTo<AdjustValueEvent>(E, AdjustValueEvent.Pool, ref AdjustValueEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x001657D5 File Offset: 0x001639D5
		public static AdjustValueEvent FromPool()
		{
			return MinEvent.FromPool<AdjustValueEvent>(ref AdjustValueEvent.Pool, ref AdjustValueEvent.PoolCounter, 8192);
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x001657EB File Offset: 0x001639EB
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x001657FF File Offset: 0x001639FF
		public static AdjustValueEvent FromPool(GameObject Object, double Value)
		{
			AdjustValueEvent adjustValueEvent = AdjustValueEvent.FromPool();
			adjustValueEvent.Object = Object;
			adjustValueEvent.Value = Value;
			return adjustValueEvent;
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x00165814 File Offset: 0x00163A14
		public static AdjustValueEvent FromPool(IValueEvent PE)
		{
			AdjustValueEvent adjustValueEvent = AdjustValueEvent.FromPool();
			adjustValueEvent.Object = PE.Object;
			adjustValueEvent.Value = PE.Value;
			return adjustValueEvent;
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x00165833 File Offset: 0x00163A33
		public void AdjustValue(double Factor)
		{
			this.Value *= Factor;
		}

		// Token: 0x04002137 RID: 8503
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AdjustValueEvent), null, new MinEvent.EventPoolCount(AdjustValueEvent.CountPool), new MinEvent.EventPoolReset(AdjustValueEvent.ResetPool));

		// Token: 0x04002138 RID: 8504
		private static List<AdjustValueEvent> Pool;

		// Token: 0x04002139 RID: 8505
		private static int PoolCounter;
	}
}
