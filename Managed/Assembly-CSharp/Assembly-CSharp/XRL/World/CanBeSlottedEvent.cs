using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006BD RID: 1725
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanBeSlottedEvent : IActOnItemEvent
	{
		// Token: 0x06004931 RID: 18737 RVA: 0x00170F2D File Offset: 0x0016F12D
		public CanBeSlottedEvent()
		{
			this.ID = CanBeSlottedEvent.ID;
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x00170F40 File Offset: 0x0016F140
		public static int CountPool()
		{
			if (CanBeSlottedEvent.Pool != null)
			{
				return CanBeSlottedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x00170F55 File Offset: 0x0016F155
		public static void ResetPool()
		{
			while (CanBeSlottedEvent.PoolCounter > 0)
			{
				CanBeSlottedEvent.Pool[--CanBeSlottedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x00170F7D File Offset: 0x0016F17D
		public static void ResetTo(ref CanBeSlottedEvent E)
		{
			MinEvent.ResetTo<CanBeSlottedEvent>(E, CanBeSlottedEvent.Pool, ref CanBeSlottedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x00170F93 File Offset: 0x0016F193
		public static CanBeSlottedEvent FromPool()
		{
			return MinEvent.FromPool<CanBeSlottedEvent>(ref CanBeSlottedEvent.Pool, ref CanBeSlottedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x00170FA9 File Offset: 0x0016F1A9
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x00170FBD File Offset: 0x0016F1BD
		public override void Reset()
		{
			base.Reset();
			this.Cell = null;
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x00170FCC File Offset: 0x0016F1CC
		public static bool Check(GameObject Actor, GameObject Item, IEnergyCell Cell)
		{
			bool flag = Actor != null && Actor.WantEvent(CanBeSlottedEvent.ID, MinEvent.CascadeLevel);
			bool flag2 = Item != null && Item.WantEvent(CanBeSlottedEvent.ID, MinEvent.CascadeLevel);
			if (flag || flag2)
			{
				CanBeSlottedEvent canBeSlottedEvent = CanBeSlottedEvent.FromPool();
				canBeSlottedEvent.Actor = Actor;
				canBeSlottedEvent.Item = Item;
				canBeSlottedEvent.Cell = Cell;
				if (flag && !Actor.HandleEvent(canBeSlottedEvent))
				{
					return false;
				}
				if (flag2 && !Item.HandleEvent(canBeSlottedEvent))
				{
					return false;
				}
			}
			bool flag3 = Actor != null && Actor.HasRegisteredEvent("CanBeSlotted");
			bool flag4 = Item != null && Item.HasRegisteredEvent("CanBeSlotted");
			if (flag3 || flag4)
			{
				Event @event = Event.New("CanBeSlotted", "Actor", Actor, "Item", Item, "Cell", Cell);
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

		// Token: 0x0400231A RID: 8986
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CanBeSlottedEvent), null, new MinEvent.EventPoolCount(CanBeSlottedEvent.CountPool), new MinEvent.EventPoolReset(CanBeSlottedEvent.ResetPool));

		// Token: 0x0400231B RID: 8987
		private static List<CanBeSlottedEvent> Pool;

		// Token: 0x0400231C RID: 8988
		private static int PoolCounter;

		// Token: 0x0400231D RID: 8989
		public IEnergyCell Cell;
	}
}
