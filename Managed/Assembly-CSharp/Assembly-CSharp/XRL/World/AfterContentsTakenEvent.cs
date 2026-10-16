using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000659 RID: 1625
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterContentsTakenEvent : IActOnItemEvent
	{
		// Token: 0x06004645 RID: 17989 RVA: 0x00166164 File Offset: 0x00164364
		public AfterContentsTakenEvent()
		{
			this.ID = AfterContentsTakenEvent.ID;
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x00166177 File Offset: 0x00164377
		public static int CountPool()
		{
			if (AfterContentsTakenEvent.Pool != null)
			{
				return AfterContentsTakenEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x0016618C File Offset: 0x0016438C
		public static void ResetPool()
		{
			while (AfterContentsTakenEvent.PoolCounter > 0)
			{
				AfterContentsTakenEvent.Pool[--AfterContentsTakenEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x001661B4 File Offset: 0x001643B4
		public static void ResetTo(ref AfterContentsTakenEvent E)
		{
			MinEvent.ResetTo<AfterContentsTakenEvent>(E, AfterContentsTakenEvent.Pool, ref AfterContentsTakenEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x001661CA File Offset: 0x001643CA
		public static AfterContentsTakenEvent FromPool()
		{
			return MinEvent.FromPool<AfterContentsTakenEvent>(ref AfterContentsTakenEvent.Pool, ref AfterContentsTakenEvent.PoolCounter, 8192);
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x001661E0 File Offset: 0x001643E0
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x001661F4 File Offset: 0x001643F4
		public override void Reset()
		{
			base.Reset();
			this.Container = null;
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x00166204 File Offset: 0x00164404
		public static void Send(GameObject Actor, GameObject Container, GameObject Item)
		{
			AfterContentsTakenEvent afterContentsTakenEvent = AfterContentsTakenEvent.FromPool();
			afterContentsTakenEvent.Actor = Actor;
			afterContentsTakenEvent.Container = Container;
			afterContentsTakenEvent.Item = Item;
			Container.HandleEvent(afterContentsTakenEvent);
			AfterContentsTakenEvent.ResetTo(ref afterContentsTakenEvent);
		}

		// Token: 0x0400215E RID: 8542
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterContentsTakenEvent), null, new MinEvent.EventPoolCount(AfterContentsTakenEvent.CountPool), new MinEvent.EventPoolReset(AfterContentsTakenEvent.ResetPool));

		// Token: 0x0400215F RID: 8543
		private static List<AfterContentsTakenEvent> Pool;

		// Token: 0x04002160 RID: 8544
		private static int PoolCounter;

		// Token: 0x04002161 RID: 8545
		public GameObject Container;
	}
}
