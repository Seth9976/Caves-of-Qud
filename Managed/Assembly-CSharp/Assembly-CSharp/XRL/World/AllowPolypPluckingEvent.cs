using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000676 RID: 1654
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 32)]
	public class AllowPolypPluckingEvent : MinEvent
	{
		// Token: 0x06004718 RID: 18200 RVA: 0x00168616 File Offset: 0x00166816
		public AllowPolypPluckingEvent()
		{
			this.ID = AllowPolypPluckingEvent.ID;
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x00168629 File Offset: 0x00166829
		public override int GetCascadeLevel()
		{
			return AllowPolypPluckingEvent.CascadeLevel;
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x00168630 File Offset: 0x00166830
		public static int CountPool()
		{
			if (AllowPolypPluckingEvent.Pool != null)
			{
				return AllowPolypPluckingEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x00168645 File Offset: 0x00166845
		public static void ResetPool()
		{
			while (AllowPolypPluckingEvent.PoolCounter > 0)
			{
				AllowPolypPluckingEvent.Pool[--AllowPolypPluckingEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x0016866D File Offset: 0x0016686D
		public static void ResetTo(ref AllowPolypPluckingEvent E)
		{
			MinEvent.ResetTo<AllowPolypPluckingEvent>(E, AllowPolypPluckingEvent.Pool, ref AllowPolypPluckingEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x00168683 File Offset: 0x00166883
		public static AllowPolypPluckingEvent FromPool()
		{
			return MinEvent.FromPool<AllowPolypPluckingEvent>(ref AllowPolypPluckingEvent.Pool, ref AllowPolypPluckingEvent.PoolCounter, 8192);
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x00168699 File Offset: 0x00166899
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x001686A2 File Offset: 0x001668A2
		public override void Reset()
		{
			base.Reset();
			this.Zone = null;
			this.Object = null;
			this.Actor = null;
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x001686C0 File Offset: 0x001668C0
		public static bool Check(Zone Zone, GameObject Object, GameObject Actor = null)
		{
			bool flag = true;
			if (Zone != null && Zone.WantEvent(AllowPolypPluckingEvent.ID, AllowPolypPluckingEvent.CascadeLevel))
			{
				AllowPolypPluckingEvent allowPolypPluckingEvent = AllowPolypPluckingEvent.FromPool();
				allowPolypPluckingEvent.Zone = Zone;
				allowPolypPluckingEvent.Object = Object;
				allowPolypPluckingEvent.Actor = Actor;
				flag = Zone.HandleEvent(allowPolypPluckingEvent);
			}
			return flag;
		}

		// Token: 0x040021DF RID: 8671
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AllowPolypPluckingEvent), null, new MinEvent.EventPoolCount(AllowPolypPluckingEvent.CountPool), new MinEvent.EventPoolReset(AllowPolypPluckingEvent.ResetPool));

		// Token: 0x040021E0 RID: 8672
		public new static readonly int CascadeLevel = 32;

		// Token: 0x040021E1 RID: 8673
		private static List<AllowPolypPluckingEvent> Pool;

		// Token: 0x040021E2 RID: 8674
		private static int PoolCounter;

		// Token: 0x040021E3 RID: 8675
		public Zone Zone;

		// Token: 0x040021E4 RID: 8676
		public GameObject Object;

		// Token: 0x040021E5 RID: 8677
		public GameObject Actor;
	}
}
