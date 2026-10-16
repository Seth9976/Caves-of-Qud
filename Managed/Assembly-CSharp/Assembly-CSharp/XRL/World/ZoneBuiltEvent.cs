using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200087E RID: 2174
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class ZoneBuiltEvent : IZoneEvent
	{
		// Token: 0x06005643 RID: 22083 RVA: 0x0019AB5B File Offset: 0x00198D5B
		public ZoneBuiltEvent()
		{
			this.ID = ZoneBuiltEvent.ID;
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x0019AB6E File Offset: 0x00198D6E
		public override int GetCascadeLevel()
		{
			return ZoneBuiltEvent.CascadeLevel;
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x0019AB75 File Offset: 0x00198D75
		public static int CountPool()
		{
			if (ZoneBuiltEvent.Pool != null)
			{
				return ZoneBuiltEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x0019AB8A File Offset: 0x00198D8A
		public static void ResetPool()
		{
			while (ZoneBuiltEvent.PoolCounter > 0)
			{
				ZoneBuiltEvent.Pool[--ZoneBuiltEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x0019ABB2 File Offset: 0x00198DB2
		public static void ResetTo(ref ZoneBuiltEvent E)
		{
			MinEvent.ResetTo<ZoneBuiltEvent>(E, ZoneBuiltEvent.Pool, ref ZoneBuiltEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005648 RID: 22088 RVA: 0x0019ABC8 File Offset: 0x00198DC8
		public static ZoneBuiltEvent FromPool()
		{
			return MinEvent.FromPool<ZoneBuiltEvent>(ref ZoneBuiltEvent.Pool, ref ZoneBuiltEvent.PoolCounter, 8192);
		}

		// Token: 0x06005649 RID: 22089 RVA: 0x0019ABDE File Offset: 0x00198DDE
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x0019ABF4 File Offset: 0x00198DF4
		public static void Send(Zone Zone)
		{
			ZoneBuiltEvent zoneBuiltEvent = ZoneBuiltEvent.FromPool();
			zoneBuiltEvent.Zone = Zone;
			The.Game.HandleEvent(zoneBuiltEvent);
			Zone.HandleEvent(zoneBuiltEvent);
			ZoneBuiltEvent.ResetTo(ref zoneBuiltEvent);
		}

		// Token: 0x04002B78 RID: 11128
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ZoneBuiltEvent), null, new MinEvent.EventPoolCount(ZoneBuiltEvent.CountPool), new MinEvent.EventPoolReset(ZoneBuiltEvent.ResetPool));

		// Token: 0x04002B79 RID: 11129
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002B7A RID: 11130
		private static List<ZoneBuiltEvent> Pool;

		// Token: 0x04002B7B RID: 11131
		private static int PoolCounter;
	}
}
