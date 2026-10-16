using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x02000642 RID: 1602
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AIGetDefensiveAbilityListEvent : IAICommandListEvent
	{
		// Token: 0x06004592 RID: 17810 RVA: 0x0016401C File Offset: 0x0016221C
		public AIGetDefensiveAbilityListEvent()
		{
			this.ID = AIGetDefensiveAbilityListEvent.ID;
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x0016402F File Offset: 0x0016222F
		public static int CountPool()
		{
			if (AIGetDefensiveAbilityListEvent.Pool != null)
			{
				return AIGetDefensiveAbilityListEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x00164044 File Offset: 0x00162244
		public static void ResetPool()
		{
			while (AIGetDefensiveAbilityListEvent.PoolCounter > 0)
			{
				AIGetDefensiveAbilityListEvent.Pool[--AIGetDefensiveAbilityListEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x0016406C File Offset: 0x0016226C
		public static void ResetTo(ref AIGetDefensiveAbilityListEvent E)
		{
			MinEvent.ResetTo<AIGetDefensiveAbilityListEvent>(E, AIGetDefensiveAbilityListEvent.Pool, ref AIGetDefensiveAbilityListEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x00164082 File Offset: 0x00162282
		public static AIGetDefensiveAbilityListEvent FromPool()
		{
			return MinEvent.FromPool<AIGetDefensiveAbilityListEvent>(ref AIGetDefensiveAbilityListEvent.Pool, ref AIGetDefensiveAbilityListEvent.PoolCounter, 8192);
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x00164098 File Offset: 0x00162298
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x001640AC File Offset: 0x001622AC
		public static List<AICommandList> GetFor(GameObject Actor, GameObject Target = null, int Distance = -1)
		{
			AIGetDefensiveAbilityListEvent aigetDefensiveAbilityListEvent = AIGetDefensiveAbilityListEvent.FromPool();
			if (Target == null)
			{
				Target = ((Actor != null) ? Actor.Target : null);
			}
			if (Distance == -1 && GameObject.Validate(ref Actor) && GameObject.Validate(ref Target))
			{
				Distance = Actor.DistanceTo(Target);
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetDefensiveAbilityList"))
			{
				Event @event = Event.New("AIGetDefensiveAbilityList", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Distance", Distance);
				@event.SetParameter("List", aigetDefensiveAbilityListEvent.List);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetDefensiveMutationList"))
			{
				Event event2 = Event.New("AIGetDefensiveMutationList", 0, 0, 0);
				event2.SetParameter("User", Actor);
				event2.SetParameter("Target", Target);
				event2.SetParameter("Distance", Distance);
				event2.SetParameter("List", aigetDefensiveAbilityListEvent.List);
				flag = Actor.FireEvent(event2);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AIGetDefensiveAbilityListEvent.ID, IAICommandListEvent.CascadeLevel))
			{
				aigetDefensiveAbilityListEvent.Actor = Actor;
				aigetDefensiveAbilityListEvent.Target = Target;
				aigetDefensiveAbilityListEvent.Distance = Distance;
				flag = Actor.HandleEvent(aigetDefensiveAbilityListEvent);
			}
			return aigetDefensiveAbilityListEvent.List;
		}

		// Token: 0x04002108 RID: 8456
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AIGetDefensiveAbilityListEvent), null, new MinEvent.EventPoolCount(AIGetDefensiveAbilityListEvent.CountPool), new MinEvent.EventPoolReset(AIGetDefensiveAbilityListEvent.ResetPool));

		// Token: 0x04002109 RID: 8457
		private static List<AIGetDefensiveAbilityListEvent> Pool;

		// Token: 0x0400210A RID: 8458
		private static int PoolCounter;
	}
}
