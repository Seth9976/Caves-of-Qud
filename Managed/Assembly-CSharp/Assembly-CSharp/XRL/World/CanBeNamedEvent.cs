using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006BB RID: 1723
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class CanBeNamedEvent : IActOnItemEvent
	{
		// Token: 0x0600491F RID: 18719 RVA: 0x00170C05 File Offset: 0x0016EE05
		public CanBeNamedEvent()
		{
			this.ID = CanBeNamedEvent.ID;
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x00170C18 File Offset: 0x0016EE18
		public override int GetCascadeLevel()
		{
			return CanBeNamedEvent.CascadeLevel;
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x00170C1F File Offset: 0x0016EE1F
		public static int CountPool()
		{
			if (CanBeNamedEvent.Pool != null)
			{
				return CanBeNamedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x00170C34 File Offset: 0x0016EE34
		public static void ResetPool()
		{
			while (CanBeNamedEvent.PoolCounter > 0)
			{
				CanBeNamedEvent.Pool[--CanBeNamedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x00170C5C File Offset: 0x0016EE5C
		public static void ResetTo(ref CanBeNamedEvent E)
		{
			MinEvent.ResetTo<CanBeNamedEvent>(E, CanBeNamedEvent.Pool, ref CanBeNamedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x00170C72 File Offset: 0x0016EE72
		public static CanBeNamedEvent FromPool()
		{
			return MinEvent.FromPool<CanBeNamedEvent>(ref CanBeNamedEvent.Pool, ref CanBeNamedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x00170C88 File Offset: 0x0016EE88
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x00170C9C File Offset: 0x0016EE9C
		public static CanBeNamedEvent FromPool(GameObject Actor, GameObject Item)
		{
			CanBeNamedEvent canBeNamedEvent = CanBeNamedEvent.FromPool();
			canBeNamedEvent.Actor = Actor;
			canBeNamedEvent.Item = Item;
			return canBeNamedEvent;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x00170CB4 File Offset: 0x0016EEB4
		public static bool Check(GameObject Actor, GameObject Item)
		{
			bool flag = Actor != null && Actor.WantEvent(CanBeNamedEvent.ID, CanBeNamedEvent.CascadeLevel);
			bool flag2 = Item != null && Item.WantEvent(CanBeNamedEvent.ID, CanBeNamedEvent.CascadeLevel);
			if (flag || flag2)
			{
				CanBeNamedEvent canBeNamedEvent = CanBeNamedEvent.FromPool(Actor, Item);
				if (flag && !Actor.HandleEvent(canBeNamedEvent))
				{
					return false;
				}
				if (flag2 && !Item.HandleEvent(canBeNamedEvent))
				{
					return false;
				}
			}
			bool flag3 = Actor != null && Actor.HasRegisteredEvent("CanBeNamed");
			bool flag4 = Item != null && Item.HasRegisteredEvent("CanBeNamed");
			if (flag3 || flag4)
			{
				Event @event = Event.New("CanBeNamed", "Actor", Actor, "Item", Item);
				if (flag3 && !Actor.FireEvent(@event))
				{
					return false;
				}
				if (flag4 && !Item.FireEvent(@event))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002313 RID: 8979
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CanBeNamedEvent), null, new MinEvent.EventPoolCount(CanBeNamedEvent.CountPool), new MinEvent.EventPoolReset(CanBeNamedEvent.ResetPool));

		// Token: 0x04002314 RID: 8980
		public new static readonly int CascadeLevel = 1;

		// Token: 0x04002315 RID: 8981
		private static List<CanBeNamedEvent> Pool;

		// Token: 0x04002316 RID: 8982
		private static int PoolCounter;
	}
}
