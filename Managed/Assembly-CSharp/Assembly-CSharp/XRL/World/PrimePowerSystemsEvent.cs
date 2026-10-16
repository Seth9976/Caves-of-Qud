using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200082C RID: 2092
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class PrimePowerSystemsEvent : IZoneEvent
	{
		// Token: 0x0600532D RID: 21293 RVA: 0x0018EC1A File Offset: 0x0018CE1A
		public PrimePowerSystemsEvent()
		{
			this.ID = PrimePowerSystemsEvent.ID;
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x0018EC2D File Offset: 0x0018CE2D
		public static int CountPool()
		{
			if (PrimePowerSystemsEvent.Pool != null)
			{
				return PrimePowerSystemsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x0018EC42 File Offset: 0x0018CE42
		public static void ResetPool()
		{
			while (PrimePowerSystemsEvent.PoolCounter > 0)
			{
				PrimePowerSystemsEvent.Pool[--PrimePowerSystemsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x0018EC6A File Offset: 0x0018CE6A
		public static void ResetTo(ref PrimePowerSystemsEvent E)
		{
			MinEvent.ResetTo<PrimePowerSystemsEvent>(E, PrimePowerSystemsEvent.Pool, ref PrimePowerSystemsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x0018EC80 File Offset: 0x0018CE80
		public static PrimePowerSystemsEvent FromPool()
		{
			return MinEvent.FromPool<PrimePowerSystemsEvent>(ref PrimePowerSystemsEvent.Pool, ref PrimePowerSystemsEvent.PoolCounter, 8192);
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x0018EC96 File Offset: 0x0018CE96
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0018ECAC File Offset: 0x0018CEAC
		public static void Send(Zone Zone)
		{
			if (Zone != null && Zone.WantEvent(PrimePowerSystemsEvent.ID, MinEvent.CascadeLevel))
			{
				PrimePowerSystemsEvent primePowerSystemsEvent = PrimePowerSystemsEvent.FromPool();
				primePowerSystemsEvent.Zone = Zone;
				Zone.HandleEvent(primePowerSystemsEvent);
			}
		}

		// Token: 0x040029DB RID: 10715
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(PrimePowerSystemsEvent), null, new MinEvent.EventPoolCount(PrimePowerSystemsEvent.CountPool), new MinEvent.EventPoolReset(PrimePowerSystemsEvent.ResetPool));

		// Token: 0x040029DC RID: 10716
		private static List<PrimePowerSystemsEvent> Pool;

		// Token: 0x040029DD RID: 10717
		private static int PoolCounter;
	}
}
