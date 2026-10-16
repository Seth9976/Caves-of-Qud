using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000683 RID: 1667
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AwardedXPEvent : IXPEvent
	{
		// Token: 0x0600477A RID: 18298 RVA: 0x00169A64 File Offset: 0x00167C64
		public AwardedXPEvent()
		{
			this.ID = AwardedXPEvent.ID;
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x00169A77 File Offset: 0x00167C77
		public static int CountPool()
		{
			if (AwardedXPEvent.Pool != null)
			{
				return AwardedXPEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x00169A8C File Offset: 0x00167C8C
		public static void ResetPool()
		{
			while (AwardedXPEvent.PoolCounter > 0)
			{
				AwardedXPEvent.Pool[--AwardedXPEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x00169AB4 File Offset: 0x00167CB4
		public static void ResetTo(ref AwardedXPEvent E)
		{
			MinEvent.ResetTo<AwardedXPEvent>(E, AwardedXPEvent.Pool, ref AwardedXPEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x00169ACA File Offset: 0x00167CCA
		public static AwardedXPEvent FromPool()
		{
			return MinEvent.FromPool<AwardedXPEvent>(ref AwardedXPEvent.Pool, ref AwardedXPEvent.PoolCounter, 8192);
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x00169AE0 File Offset: 0x00167CE0
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x00169AF4 File Offset: 0x00167CF4
		public static void Send(IXPEvent ParentEvent, int Amount)
		{
			if (ParentEvent.Actor.WantEvent(AwardedXPEvent.ID, MinEvent.CascadeLevel))
			{
				AwardedXPEvent awardedXPEvent = AwardedXPEvent.FromPool();
				ParentEvent.ApplyTo(awardedXPEvent);
				awardedXPEvent.Amount = Amount;
				awardedXPEvent.Actor.HandleEvent(awardedXPEvent);
			}
		}

		// Token: 0x04002219 RID: 8729
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AwardedXPEvent), null, new MinEvent.EventPoolCount(AwardedXPEvent.CountPool), new MinEvent.EventPoolReset(AwardedXPEvent.ResetPool));

		// Token: 0x0400221A RID: 8730
		private static List<AwardedXPEvent> Pool;

		// Token: 0x0400221B RID: 8731
		private static int PoolCounter;
	}
}
