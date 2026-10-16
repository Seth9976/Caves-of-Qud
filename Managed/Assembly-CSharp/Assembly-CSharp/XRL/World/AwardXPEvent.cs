using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000682 RID: 1666
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AwardXPEvent : IXPEvent
	{
		// Token: 0x06004772 RID: 18290 RVA: 0x001698F2 File Offset: 0x00167AF2
		public AwardXPEvent()
		{
			this.ID = AwardXPEvent.ID;
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x00169905 File Offset: 0x00167B05
		public static int CountPool()
		{
			if (AwardXPEvent.Pool != null)
			{
				return AwardXPEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x0016991A File Offset: 0x00167B1A
		public static void ResetPool()
		{
			while (AwardXPEvent.PoolCounter > 0)
			{
				AwardXPEvent.Pool[--AwardXPEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x00169942 File Offset: 0x00167B42
		public static void ResetTo(ref AwardXPEvent E)
		{
			MinEvent.ResetTo<AwardXPEvent>(E, AwardXPEvent.Pool, ref AwardXPEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x00169958 File Offset: 0x00167B58
		public static AwardXPEvent FromPool()
		{
			return MinEvent.FromPool<AwardXPEvent>(ref AwardXPEvent.Pool, ref AwardXPEvent.PoolCounter, 8192);
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x0016996E File Offset: 0x00167B6E
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x00169984 File Offset: 0x00167B84
		public static int Send(GameObject Actor, int Amount, int Tier = -1, int Minimum = 0, int Maximum = 2147483647, GameObject Kill = null, GameObject InfluencedBy = null, GameObject PassedUpFrom = null, GameObject PassedDownFrom = null, string ZoneID = null, string Deed = null)
		{
			int num = Actor.Stat("XP", 0);
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(AwardXPEvent.ID, MinEvent.CascadeLevel))
			{
				AwardXPEvent awardXPEvent = AwardXPEvent.FromPool();
				awardXPEvent.Actor = Actor;
				awardXPEvent.Amount = Amount;
				awardXPEvent.AmountBefore = num;
				awardXPEvent.Tier = Tier;
				awardXPEvent.Minimum = Minimum;
				awardXPEvent.Maximum = Maximum;
				awardXPEvent.Kill = Kill;
				awardXPEvent.InfluencedBy = InfluencedBy;
				awardXPEvent.PassedUpFrom = PassedUpFrom;
				awardXPEvent.PassedDownFrom = PassedDownFrom;
				awardXPEvent.ZoneID = ZoneID;
				awardXPEvent.Deed = Deed;
				awardXPEvent.TierScaling = true;
				Actor.HandleEvent(awardXPEvent);
				Amount = awardXPEvent.Amount;
			}
			return Amount;
		}

		// Token: 0x04002216 RID: 8726
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AwardXPEvent), null, new MinEvent.EventPoolCount(AwardXPEvent.CountPool), new MinEvent.EventPoolReset(AwardXPEvent.ResetPool));

		// Token: 0x04002217 RID: 8727
		private static List<AwardXPEvent> Pool;

		// Token: 0x04002218 RID: 8728
		private static int PoolCounter;
	}
}
