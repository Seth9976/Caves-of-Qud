using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000844 RID: 2116
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class RepairCriticalFailureEvent : IActOnItemEvent
	{
		// Token: 0x060053FF RID: 21503 RVA: 0x00190E1E File Offset: 0x0018F01E
		public RepairCriticalFailureEvent()
		{
			this.ID = RepairCriticalFailureEvent.ID;
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x00190E31 File Offset: 0x0018F031
		public static int CountPool()
		{
			if (RepairCriticalFailureEvent.Pool != null)
			{
				return RepairCriticalFailureEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x00190E46 File Offset: 0x0018F046
		public static void ResetPool()
		{
			while (RepairCriticalFailureEvent.PoolCounter > 0)
			{
				RepairCriticalFailureEvent.Pool[--RepairCriticalFailureEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x00190E6E File Offset: 0x0018F06E
		public static void ResetTo(ref RepairCriticalFailureEvent E)
		{
			MinEvent.ResetTo<RepairCriticalFailureEvent>(E, RepairCriticalFailureEvent.Pool, ref RepairCriticalFailureEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x00190E84 File Offset: 0x0018F084
		public static RepairCriticalFailureEvent FromPool()
		{
			return MinEvent.FromPool<RepairCriticalFailureEvent>(ref RepairCriticalFailureEvent.Pool, ref RepairCriticalFailureEvent.PoolCounter, 8192);
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x00190E9A File Offset: 0x0018F09A
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x00190EAE File Offset: 0x0018F0AE
		public static RepairCriticalFailureEvent FromPool(GameObject Actor, GameObject Item)
		{
			RepairCriticalFailureEvent repairCriticalFailureEvent = RepairCriticalFailureEvent.FromPool();
			repairCriticalFailureEvent.Actor = Actor;
			repairCriticalFailureEvent.Item = Item;
			return repairCriticalFailureEvent;
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x00190EC4 File Offset: 0x0018F0C4
		public static bool Check(GameObject Actor, GameObject Item)
		{
			if (Actor.HasRegisteredEvent("RepairCriticalFailure") || Item.HasRegisteredEvent("RepairCriticalFailure"))
			{
				Event @event = Event.New("RepairCriticalFailure", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Item", Item);
				if (!Actor.FireEvent(@event) || !Item.FireEvent(@event))
				{
					return false;
				}
			}
			bool flag = Actor.WantEvent(RepairCriticalFailureEvent.ID, MinEvent.CascadeLevel);
			bool flag2 = Item.WantEvent(RepairCriticalFailureEvent.ID, MinEvent.CascadeLevel);
			if (flag || flag2)
			{
				RepairCriticalFailureEvent repairCriticalFailureEvent = RepairCriticalFailureEvent.FromPool(Actor, Item);
				if ((flag && !Actor.HandleEvent(repairCriticalFailureEvent)) || (flag2 && !Item.HandleEvent(repairCriticalFailureEvent)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002A33 RID: 10803
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(RepairCriticalFailureEvent), null, new MinEvent.EventPoolCount(RepairCriticalFailureEvent.CountPool), new MinEvent.EventPoolReset(RepairCriticalFailureEvent.ResetPool));

		// Token: 0x04002A34 RID: 10804
		private static List<RepairCriticalFailureEvent> Pool;

		// Token: 0x04002A35 RID: 10805
		private static int PoolCounter;
	}
}
