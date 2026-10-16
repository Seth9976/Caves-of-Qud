using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000673 RID: 1651
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class AfterZoneBuiltEvent : IZoneEvent
	{
		// Token: 0x060046FC RID: 18172 RVA: 0x00168264 File Offset: 0x00166464
		public AfterZoneBuiltEvent()
		{
			this.ID = AfterZoneBuiltEvent.ID;
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x00168277 File Offset: 0x00166477
		public override int GetCascadeLevel()
		{
			return AfterZoneBuiltEvent.CascadeLevel;
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x0016827E File Offset: 0x0016647E
		public static int CountPool()
		{
			if (AfterZoneBuiltEvent.Pool != null)
			{
				return AfterZoneBuiltEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x00168293 File Offset: 0x00166493
		public static void ResetPool()
		{
			while (AfterZoneBuiltEvent.PoolCounter > 0)
			{
				AfterZoneBuiltEvent.Pool[--AfterZoneBuiltEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x001682BB File Offset: 0x001664BB
		public static void ResetTo(ref AfterZoneBuiltEvent E)
		{
			MinEvent.ResetTo<AfterZoneBuiltEvent>(E, AfterZoneBuiltEvent.Pool, ref AfterZoneBuiltEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x001682D1 File Offset: 0x001664D1
		public static AfterZoneBuiltEvent FromPool()
		{
			return MinEvent.FromPool<AfterZoneBuiltEvent>(ref AfterZoneBuiltEvent.Pool, ref AfterZoneBuiltEvent.PoolCounter, 8192);
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x001682E7 File Offset: 0x001664E7
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x001682FC File Offset: 0x001664FC
		public static void Send(Zone Zone)
		{
			AfterZoneBuiltEvent afterZoneBuiltEvent = AfterZoneBuiltEvent.FromPool();
			afterZoneBuiltEvent.Zone = Zone;
			The.Game.HandleEvent(afterZoneBuiltEvent);
			Zone.HandleEvent(afterZoneBuiltEvent);
			AfterZoneBuiltEvent.ResetTo(ref afterZoneBuiltEvent);
		}

		// Token: 0x040021D4 RID: 8660
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterZoneBuiltEvent), null, new MinEvent.EventPoolCount(AfterZoneBuiltEvent.CountPool), new MinEvent.EventPoolReset(AfterZoneBuiltEvent.ResetPool));

		// Token: 0x040021D5 RID: 8661
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040021D6 RID: 8662
		private static List<AfterZoneBuiltEvent> Pool;

		// Token: 0x040021D7 RID: 8663
		private static int PoolCounter;
	}
}
