using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x02000645 RID: 1605
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AIGetOffensiveAbilityListEvent : IAICommandListEvent
	{
		// Token: 0x060045AA RID: 17834 RVA: 0x00164643 File Offset: 0x00162843
		public AIGetOffensiveAbilityListEvent()
		{
			this.ID = AIGetOffensiveAbilityListEvent.ID;
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x00164656 File Offset: 0x00162856
		public static int CountPool()
		{
			if (AIGetOffensiveAbilityListEvent.Pool != null)
			{
				return AIGetOffensiveAbilityListEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x0016466B File Offset: 0x0016286B
		public static void ResetPool()
		{
			while (AIGetOffensiveAbilityListEvent.PoolCounter > 0)
			{
				AIGetOffensiveAbilityListEvent.Pool[--AIGetOffensiveAbilityListEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x00164693 File Offset: 0x00162893
		public static void ResetTo(ref AIGetOffensiveAbilityListEvent E)
		{
			MinEvent.ResetTo<AIGetOffensiveAbilityListEvent>(E, AIGetOffensiveAbilityListEvent.Pool, ref AIGetOffensiveAbilityListEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x001646A9 File Offset: 0x001628A9
		public static AIGetOffensiveAbilityListEvent FromPool()
		{
			return MinEvent.FromPool<AIGetOffensiveAbilityListEvent>(ref AIGetOffensiveAbilityListEvent.Pool, ref AIGetOffensiveAbilityListEvent.PoolCounter, 8192);
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x001646BF File Offset: 0x001628BF
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x001646D4 File Offset: 0x001628D4
		public static List<AICommandList> GetFor(GameObject Actor, GameObject Target = null, int Distance = -1)
		{
			AIGetOffensiveAbilityListEvent aigetOffensiveAbilityListEvent = AIGetOffensiveAbilityListEvent.FromPool();
			if (Target == null)
			{
				Target = ((Actor != null) ? Actor.Target : null);
			}
			if (Distance == -1 && GameObject.Validate(ref Actor) && GameObject.Validate(ref Target))
			{
				Distance = Actor.DistanceTo(Target);
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetOffensiveAbilityList"))
			{
				Event @event = Event.New("AIGetOffensiveAbilityList", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Distance", Distance);
				@event.SetParameter("List", aigetOffensiveAbilityListEvent.List);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetOffensiveMutationList"))
			{
				Event event2 = Event.New("AIGetOffensiveMutationList", 0, 0, 0);
				event2.SetParameter("User", Actor);
				event2.SetParameter("Target", Target);
				event2.SetParameter("Distance", Distance);
				event2.SetParameter("List", aigetOffensiveAbilityListEvent.List);
				flag = Actor.FireEvent(event2);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AIGetOffensiveAbilityListEvent.ID, IAICommandListEvent.CascadeLevel))
			{
				aigetOffensiveAbilityListEvent.Actor = Actor;
				aigetOffensiveAbilityListEvent.Target = Target;
				aigetOffensiveAbilityListEvent.Distance = Distance;
				flag = Actor.HandleEvent(aigetOffensiveAbilityListEvent);
			}
			return aigetOffensiveAbilityListEvent.List;
		}

		// Token: 0x04002111 RID: 8465
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AIGetOffensiveAbilityListEvent), null, new MinEvent.EventPoolCount(AIGetOffensiveAbilityListEvent.CountPool), new MinEvent.EventPoolReset(AIGetOffensiveAbilityListEvent.ResetPool));

		// Token: 0x04002112 RID: 8466
		private static List<AIGetOffensiveAbilityListEvent> Pool;

		// Token: 0x04002113 RID: 8467
		private static int PoolCounter;
	}
}
