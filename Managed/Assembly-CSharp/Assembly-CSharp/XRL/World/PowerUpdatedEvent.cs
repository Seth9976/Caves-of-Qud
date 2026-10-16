using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000828 RID: 2088
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 15)]
	public class PowerUpdatedEvent : MinEvent
	{
		// Token: 0x0600530F RID: 21263 RVA: 0x0018E841 File Offset: 0x0018CA41
		public PowerUpdatedEvent()
		{
			this.ID = PowerUpdatedEvent.ID;
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x0018E854 File Offset: 0x0018CA54
		public override int GetCascadeLevel()
		{
			return PowerUpdatedEvent.CascadeLevel;
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x0018E85B File Offset: 0x0018CA5B
		public static int CountPool()
		{
			if (PowerUpdatedEvent.Pool != null)
			{
				return PowerUpdatedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x0018E870 File Offset: 0x0018CA70
		public static void ResetPool()
		{
			while (PowerUpdatedEvent.PoolCounter > 0)
			{
				PowerUpdatedEvent.Pool[--PowerUpdatedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x0018E898 File Offset: 0x0018CA98
		public static void ResetTo(ref PowerUpdatedEvent E)
		{
			MinEvent.ResetTo<PowerUpdatedEvent>(E, PowerUpdatedEvent.Pool, ref PowerUpdatedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x0018E8AE File Offset: 0x0018CAAE
		public static PowerUpdatedEvent FromPool()
		{
			return MinEvent.FromPool<PowerUpdatedEvent>(ref PowerUpdatedEvent.Pool, ref PowerUpdatedEvent.PoolCounter, 8192);
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x0018E8C4 File Offset: 0x0018CAC4
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x0018E8CD File Offset: 0x0018CACD
		public override void Reset()
		{
			base.Reset();
			this.Zone = null;
			this.Object = null;
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x0018E8E4 File Offset: 0x0018CAE4
		public static void Send(Zone Zone, GameObject Object = null)
		{
			if (Zone != null && Zone.WantEvent(PowerUpdatedEvent.ID, PowerUpdatedEvent.CascadeLevel))
			{
				PowerUpdatedEvent powerUpdatedEvent = PowerUpdatedEvent.FromPool();
				powerUpdatedEvent.Zone = Zone;
				powerUpdatedEvent.Object = Object;
				Zone.HandleEvent(powerUpdatedEvent);
			}
		}

		// Token: 0x040029C9 RID: 10697
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(PowerUpdatedEvent), null, new MinEvent.EventPoolCount(PowerUpdatedEvent.CountPool), new MinEvent.EventPoolReset(PowerUpdatedEvent.ResetPool));

		// Token: 0x040029CA RID: 10698
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040029CB RID: 10699
		private static List<PowerUpdatedEvent> Pool;

		// Token: 0x040029CC RID: 10700
		private static int PoolCounter;

		// Token: 0x040029CD RID: 10701
		public Zone Zone;

		// Token: 0x040029CE RID: 10702
		public GameObject Object;
	}
}
