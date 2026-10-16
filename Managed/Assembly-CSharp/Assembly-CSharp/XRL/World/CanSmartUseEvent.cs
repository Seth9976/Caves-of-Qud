using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006CC RID: 1740
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanSmartUseEvent : IActOnItemEvent
	{
		// Token: 0x0600498E RID: 18830 RVA: 0x00171F85 File Offset: 0x00170185
		public CanSmartUseEvent()
		{
			this.ID = CanSmartUseEvent.ID;
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x00171F98 File Offset: 0x00170198
		public static int CountPool()
		{
			if (CanSmartUseEvent.Pool != null)
			{
				return CanSmartUseEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x00171FAD File Offset: 0x001701AD
		public static void ResetPool()
		{
			while (CanSmartUseEvent.PoolCounter > 0)
			{
				CanSmartUseEvent.Pool[--CanSmartUseEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x00171FD5 File Offset: 0x001701D5
		public static void ResetTo(ref CanSmartUseEvent E)
		{
			MinEvent.ResetTo<CanSmartUseEvent>(E, CanSmartUseEvent.Pool, ref CanSmartUseEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x00171FEB File Offset: 0x001701EB
		public static CanSmartUseEvent FromPool()
		{
			return MinEvent.FromPool<CanSmartUseEvent>(ref CanSmartUseEvent.Pool, ref CanSmartUseEvent.PoolCounter, 8192);
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00172001 File Offset: 0x00170201
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x00172015 File Offset: 0x00170215
		public override void Reset()
		{
			base.Reset();
			this.MinPriority = 0;
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x00172024 File Offset: 0x00170224
		public static CanSmartUseEvent FromPool(GameObject Actor, GameObject Item, int MinPriority)
		{
			CanSmartUseEvent canSmartUseEvent = CanSmartUseEvent.FromPool();
			canSmartUseEvent.Actor = Actor;
			canSmartUseEvent.Item = Item;
			canSmartUseEvent.MinPriority = MinPriority;
			return canSmartUseEvent;
		}

		// Token: 0x04002359 RID: 9049
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CanSmartUseEvent), null, new MinEvent.EventPoolCount(CanSmartUseEvent.CountPool), new MinEvent.EventPoolReset(CanSmartUseEvent.ResetPool));

		// Token: 0x0400235A RID: 9050
		private static List<CanSmartUseEvent> Pool;

		// Token: 0x0400235B RID: 9051
		private static int PoolCounter;

		// Token: 0x0400235C RID: 9052
		public int MinPriority;
	}
}
