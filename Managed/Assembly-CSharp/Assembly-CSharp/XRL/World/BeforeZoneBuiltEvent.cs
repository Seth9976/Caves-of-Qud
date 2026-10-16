using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006A7 RID: 1703
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class BeforeZoneBuiltEvent : IZoneEvent
	{
		// Token: 0x06004885 RID: 18565 RVA: 0x0016C90F File Offset: 0x0016AB0F
		public BeforeZoneBuiltEvent()
		{
			this.ID = BeforeZoneBuiltEvent.ID;
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x0016C922 File Offset: 0x0016AB22
		public override int GetCascadeLevel()
		{
			return BeforeZoneBuiltEvent.CascadeLevel;
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x0016C929 File Offset: 0x0016AB29
		public static int CountPool()
		{
			if (BeforeZoneBuiltEvent.Pool != null)
			{
				return BeforeZoneBuiltEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x0016C93E File Offset: 0x0016AB3E
		public static void ResetPool()
		{
			while (BeforeZoneBuiltEvent.PoolCounter > 0)
			{
				BeforeZoneBuiltEvent.Pool[--BeforeZoneBuiltEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x0016C966 File Offset: 0x0016AB66
		public static void ResetTo(ref BeforeZoneBuiltEvent E)
		{
			MinEvent.ResetTo<BeforeZoneBuiltEvent>(E, BeforeZoneBuiltEvent.Pool, ref BeforeZoneBuiltEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x0016C97C File Offset: 0x0016AB7C
		public static BeforeZoneBuiltEvent FromPool()
		{
			return MinEvent.FromPool<BeforeZoneBuiltEvent>(ref BeforeZoneBuiltEvent.Pool, ref BeforeZoneBuiltEvent.PoolCounter, 8192);
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x0016C992 File Offset: 0x0016AB92
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x0016C9A8 File Offset: 0x0016ABA8
		public static void Send(Zone Zone)
		{
			BeforeZoneBuiltEvent beforeZoneBuiltEvent = BeforeZoneBuiltEvent.FromPool();
			beforeZoneBuiltEvent.Zone = Zone;
			The.Game.HandleEvent(beforeZoneBuiltEvent);
			Zone.HandleEvent(beforeZoneBuiltEvent);
			BeforeZoneBuiltEvent.ResetTo(ref beforeZoneBuiltEvent);
		}

		// Token: 0x040022BA RID: 8890
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeZoneBuiltEvent), null, new MinEvent.EventPoolCount(BeforeZoneBuiltEvent.CountPool), new MinEvent.EventPoolReset(BeforeZoneBuiltEvent.ResetPool));

		// Token: 0x040022BB RID: 8891
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040022BC RID: 8892
		private static List<BeforeZoneBuiltEvent> Pool;

		// Token: 0x040022BD RID: 8893
		private static int PoolCounter;
	}
}
