using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x02000647 RID: 1607
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AIGetPassiveAbilityListEvent : IAICommandListEvent
	{
		// Token: 0x060045BA RID: 17850 RVA: 0x00164A09 File Offset: 0x00162C09
		public AIGetPassiveAbilityListEvent()
		{
			this.ID = AIGetPassiveAbilityListEvent.ID;
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x00164A1C File Offset: 0x00162C1C
		public static int CountPool()
		{
			if (AIGetPassiveAbilityListEvent.Pool != null)
			{
				return AIGetPassiveAbilityListEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x00164A31 File Offset: 0x00162C31
		public static void ResetPool()
		{
			while (AIGetPassiveAbilityListEvent.PoolCounter > 0)
			{
				AIGetPassiveAbilityListEvent.Pool[--AIGetPassiveAbilityListEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x00164A59 File Offset: 0x00162C59
		public static void ResetTo(ref AIGetPassiveAbilityListEvent E)
		{
			MinEvent.ResetTo<AIGetPassiveAbilityListEvent>(E, AIGetPassiveAbilityListEvent.Pool, ref AIGetPassiveAbilityListEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x00164A6F File Offset: 0x00162C6F
		public static AIGetPassiveAbilityListEvent FromPool()
		{
			return MinEvent.FromPool<AIGetPassiveAbilityListEvent>(ref AIGetPassiveAbilityListEvent.Pool, ref AIGetPassiveAbilityListEvent.PoolCounter, 8192);
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x00164A85 File Offset: 0x00162C85
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x00164A9C File Offset: 0x00162C9C
		public static List<AICommandList> GetFor(GameObject Actor, GameObject Target = null, int Distance = -1)
		{
			AIGetPassiveAbilityListEvent aigetPassiveAbilityListEvent = AIGetPassiveAbilityListEvent.FromPool();
			if (Target == null)
			{
				Target = ((Actor != null) ? Actor.Target : null);
			}
			if (Distance == -1 && GameObject.Validate(ref Actor) && GameObject.Validate(ref Target))
			{
				Distance = Actor.DistanceTo(Target);
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetPassiveAbilityList"))
			{
				Event @event = Event.New("AIGetPassiveAbilityList", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Distance", Distance);
				@event.SetParameter("List", aigetPassiveAbilityListEvent.List);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetPassiveMutationList"))
			{
				Event event2 = Event.New("AIGetPassiveMutationList", 0, 0, 0);
				event2.SetParameter("User", Actor);
				event2.SetParameter("Target", Target);
				event2.SetParameter("Distance", Distance);
				event2.SetParameter("List", aigetPassiveAbilityListEvent.List);
				flag = Actor.FireEvent(event2);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AIGetPassiveAbilityListEvent.ID, IAICommandListEvent.CascadeLevel))
			{
				aigetPassiveAbilityListEvent.Actor = Actor;
				aigetPassiveAbilityListEvent.Target = Target;
				aigetPassiveAbilityListEvent.Distance = Distance;
				flag = Actor.HandleEvent(aigetPassiveAbilityListEvent);
			}
			return aigetPassiveAbilityListEvent.List;
		}

		// Token: 0x04002117 RID: 8471
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AIGetPassiveAbilityListEvent), null, new MinEvent.EventPoolCount(AIGetPassiveAbilityListEvent.CountPool), new MinEvent.EventPoolReset(AIGetPassiveAbilityListEvent.ResetPool));

		// Token: 0x04002118 RID: 8472
		private static List<AIGetPassiveAbilityListEvent> Pool;

		// Token: 0x04002119 RID: 8473
		private static int PoolCounter;
	}
}
