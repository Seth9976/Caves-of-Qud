using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x02000649 RID: 1609
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AIGetRetreatAbilityListEvent : IAIMoveCommandListEvent
	{
		// Token: 0x060045CA RID: 17866 RVA: 0x00164DD1 File Offset: 0x00162FD1
		public AIGetRetreatAbilityListEvent()
		{
			this.ID = AIGetRetreatAbilityListEvent.ID;
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x00164DE4 File Offset: 0x00162FE4
		public static int CountPool()
		{
			if (AIGetRetreatAbilityListEvent.Pool != null)
			{
				return AIGetRetreatAbilityListEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x00164DF9 File Offset: 0x00162FF9
		public static void ResetPool()
		{
			while (AIGetRetreatAbilityListEvent.PoolCounter > 0)
			{
				AIGetRetreatAbilityListEvent.Pool[--AIGetRetreatAbilityListEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x00164E21 File Offset: 0x00163021
		public static void ResetTo(ref AIGetRetreatAbilityListEvent E)
		{
			MinEvent.ResetTo<AIGetRetreatAbilityListEvent>(E, AIGetRetreatAbilityListEvent.Pool, ref AIGetRetreatAbilityListEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x00164E37 File Offset: 0x00163037
		public static AIGetRetreatAbilityListEvent FromPool()
		{
			return MinEvent.FromPool<AIGetRetreatAbilityListEvent>(ref AIGetRetreatAbilityListEvent.Pool, ref AIGetRetreatAbilityListEvent.PoolCounter, 8192);
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x00164E4D File Offset: 0x0016304D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045D0 RID: 17872 RVA: 0x00164E61 File Offset: 0x00163061
		public override void Reset()
		{
			base.Reset();
			this.AvoidCell = null;
		}

		// Token: 0x060045D1 RID: 17873 RVA: 0x00164E70 File Offset: 0x00163070
		public static List<AICommandList> GetFor(GameObject Actor, GameObject Target = null, Cell TargetCell = null, Cell AvoidCell = null, int Distance = -1, int StandoffDistance = 0)
		{
			AIGetRetreatAbilityListEvent aigetRetreatAbilityListEvent = AIGetRetreatAbilityListEvent.FromPool();
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
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetRetreatAbilityList"))
			{
				Event @event = Event.New("AIGetRetreatAbilityList", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetParameter("AvoidCell", AvoidCell);
				@event.SetParameter("Distance", Distance);
				@event.SetParameter("StandoffDistance", StandoffDistance);
				@event.SetParameter("List", aigetRetreatAbilityListEvent.List);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AIGetRetreatAbilityListEvent.ID, IAICommandListEvent.CascadeLevel))
			{
				aigetRetreatAbilityListEvent.Actor = Actor;
				aigetRetreatAbilityListEvent.Target = Target;
				aigetRetreatAbilityListEvent.TargetCell = TargetCell;
				aigetRetreatAbilityListEvent.AvoidCell = AvoidCell;
				aigetRetreatAbilityListEvent.Distance = Distance;
				aigetRetreatAbilityListEvent.StandoffDistance = StandoffDistance;
				flag = Actor.HandleEvent(aigetRetreatAbilityListEvent);
			}
			return aigetRetreatAbilityListEvent.List;
		}

		// Token: 0x0400211D RID: 8477
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AIGetRetreatAbilityListEvent), null, new MinEvent.EventPoolCount(AIGetRetreatAbilityListEvent.CountPool), new MinEvent.EventPoolReset(AIGetRetreatAbilityListEvent.ResetPool));

		// Token: 0x0400211E RID: 8478
		private static List<AIGetRetreatAbilityListEvent> Pool;

		// Token: 0x0400211F RID: 8479
		private static int PoolCounter;

		// Token: 0x04002120 RID: 8480
		public Cell AvoidCell;
	}
}
