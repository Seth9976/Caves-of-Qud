using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x02000644 RID: 1604
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AIGetMovementAbilityListEvent : IAIMoveCommandListEvent
	{
		// Token: 0x060045A2 RID: 17826 RVA: 0x001643E1 File Offset: 0x001625E1
		public AIGetMovementAbilityListEvent()
		{
			this.ID = AIGetMovementAbilityListEvent.ID;
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x001643F4 File Offset: 0x001625F4
		public static int CountPool()
		{
			if (AIGetMovementAbilityListEvent.Pool != null)
			{
				return AIGetMovementAbilityListEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x00164409 File Offset: 0x00162609
		public static void ResetPool()
		{
			while (AIGetMovementAbilityListEvent.PoolCounter > 0)
			{
				AIGetMovementAbilityListEvent.Pool[--AIGetMovementAbilityListEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x00164431 File Offset: 0x00162631
		public static void ResetTo(ref AIGetMovementAbilityListEvent E)
		{
			MinEvent.ResetTo<AIGetMovementAbilityListEvent>(E, AIGetMovementAbilityListEvent.Pool, ref AIGetMovementAbilityListEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x00164447 File Offset: 0x00162647
		public static AIGetMovementAbilityListEvent FromPool()
		{
			return MinEvent.FromPool<AIGetMovementAbilityListEvent>(ref AIGetMovementAbilityListEvent.Pool, ref AIGetMovementAbilityListEvent.PoolCounter, 8192);
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x0016445D File Offset: 0x0016265D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x00164474 File Offset: 0x00162674
		public static List<AICommandList> GetFor(GameObject Actor, GameObject Target = null, Cell TargetCell = null, int Distance = -1, int StandoffDistance = 0)
		{
			AIGetMovementAbilityListEvent aigetMovementAbilityListEvent = AIGetMovementAbilityListEvent.FromPool();
			if (TargetCell == null)
			{
				TargetCell = ((Target != null) ? Target.CurrentCell : null);
			}
			if (Distance == -1)
			{
				if (GameObject.Validate(ref Actor) && TargetCell != null)
				{
					Distance = Actor.DistanceTo(TargetCell);
				}
				else
				{
					Distance = 0;
				}
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetMovementAbilityList"))
			{
				Event @event = Event.New("AIGetMovementAbilityList", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetParameter("Distance", Distance);
				@event.SetParameter("StandoffDistance", StandoffDistance);
				@event.SetParameter("List", aigetMovementAbilityListEvent.List);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetMovementMutationList"))
			{
				Event event2 = Event.New("AIGetMovementMutationList", 0, 0, 0);
				event2.SetParameter("User", Actor);
				event2.SetParameter("Target", Target);
				event2.SetParameter("TargetCell", TargetCell);
				event2.SetParameter("Distance", Distance);
				event2.SetParameter("StandoffDistance", StandoffDistance);
				event2.SetParameter("List", aigetMovementAbilityListEvent.List);
				flag = Actor.FireEvent(event2);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AIGetMovementAbilityListEvent.ID, IAICommandListEvent.CascadeLevel))
			{
				aigetMovementAbilityListEvent.Actor = Actor;
				aigetMovementAbilityListEvent.Target = Target;
				aigetMovementAbilityListEvent.TargetCell = TargetCell;
				aigetMovementAbilityListEvent.Distance = Distance;
				aigetMovementAbilityListEvent.StandoffDistance = StandoffDistance;
				flag = Actor.HandleEvent(aigetMovementAbilityListEvent);
			}
			return aigetMovementAbilityListEvent.List;
		}

		// Token: 0x0400210E RID: 8462
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AIGetMovementAbilityListEvent), null, new MinEvent.EventPoolCount(AIGetMovementAbilityListEvent.CountPool), new MinEvent.EventPoolReset(AIGetMovementAbilityListEvent.ResetPool));

		// Token: 0x0400210F RID: 8463
		private static List<AIGetMovementAbilityListEvent> Pool;

		// Token: 0x04002110 RID: 8464
		private static int PoolCounter;
	}
}
