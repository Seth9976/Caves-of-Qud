using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000684 RID: 1668
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AwardingXPEvent : IXPEvent
	{
		// Token: 0x06004782 RID: 18306 RVA: 0x00169B68 File Offset: 0x00167D68
		public AwardingXPEvent()
		{
			this.ID = AwardingXPEvent.ID;
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x00169B7B File Offset: 0x00167D7B
		public static int CountPool()
		{
			if (AwardingXPEvent.Pool != null)
			{
				return AwardingXPEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x00169B90 File Offset: 0x00167D90
		public static void ResetPool()
		{
			while (AwardingXPEvent.PoolCounter > 0)
			{
				AwardingXPEvent.Pool[--AwardingXPEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x00169BB8 File Offset: 0x00167DB8
		public static void ResetTo(ref AwardingXPEvent E)
		{
			MinEvent.ResetTo<AwardingXPEvent>(E, AwardingXPEvent.Pool, ref AwardingXPEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x00169BCE File Offset: 0x00167DCE
		public static AwardingXPEvent FromPool()
		{
			return MinEvent.FromPool<AwardingXPEvent>(ref AwardingXPEvent.Pool, ref AwardingXPEvent.PoolCounter, 8192);
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x00169BE4 File Offset: 0x00167DE4
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x00169BF8 File Offset: 0x00167DF8
		public static bool Check(IXPEvent ParentEvent, IEventSource Source)
		{
			if (Source.WantEvent(AwardingXPEvent.ID, MinEvent.CascadeLevel))
			{
				AwardingXPEvent awardingXPEvent = AwardingXPEvent.FromPool();
				ParentEvent.ApplyTo(awardingXPEvent);
				if (!The.Game.HandleEvent(awardingXPEvent) || !Source.HandleEvent(awardingXPEvent))
				{
					return false;
				}
				awardingXPEvent.ApplyTo(ParentEvent);
			}
			return true;
		}

		// Token: 0x0400221C RID: 8732
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AwardingXPEvent), null, new MinEvent.EventPoolCount(AwardingXPEvent.CountPool), new MinEvent.EventPoolReset(AwardingXPEvent.ResetPool));

		// Token: 0x0400221D RID: 8733
		private static List<AwardingXPEvent> Pool;

		// Token: 0x0400221E RID: 8734
		private static int PoolCounter;
	}
}
