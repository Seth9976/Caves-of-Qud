using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x02000646 RID: 1606
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AIGetOffensiveItemListEvent : IAIItemCommandListEvent
	{
		// Token: 0x060045B2 RID: 17842 RVA: 0x00164859 File Offset: 0x00162A59
		public AIGetOffensiveItemListEvent()
		{
			this.ID = AIGetOffensiveItemListEvent.ID;
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x0016486C File Offset: 0x00162A6C
		public static int CountPool()
		{
			if (AIGetOffensiveItemListEvent.Pool != null)
			{
				return AIGetOffensiveItemListEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x00164881 File Offset: 0x00162A81
		public static void ResetPool()
		{
			while (AIGetOffensiveItemListEvent.PoolCounter > 0)
			{
				AIGetOffensiveItemListEvent.Pool[--AIGetOffensiveItemListEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x001648A9 File Offset: 0x00162AA9
		public static void ResetTo(ref AIGetOffensiveItemListEvent E)
		{
			MinEvent.ResetTo<AIGetOffensiveItemListEvent>(E, AIGetOffensiveItemListEvent.Pool, ref AIGetOffensiveItemListEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x001648BF File Offset: 0x00162ABF
		public static AIGetOffensiveItemListEvent FromPool()
		{
			return MinEvent.FromPool<AIGetOffensiveItemListEvent>(ref AIGetOffensiveItemListEvent.Pool, ref AIGetOffensiveItemListEvent.PoolCounter, 8192);
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x001648D5 File Offset: 0x00162AD5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x001648EC File Offset: 0x00162AEC
		public static List<AICommandList> GetFor(GameObject Actor, GameObject Target = null, int Distance = -1)
		{
			AIGetOffensiveItemListEvent aigetOffensiveItemListEvent = AIGetOffensiveItemListEvent.FromPool();
			if (Target == null)
			{
				Target = ((Actor != null) ? Actor.Target : null);
			}
			if (Distance == -1 && GameObject.Validate(ref Actor) && GameObject.Validate(ref Target))
			{
				Distance = Actor.DistanceTo(Target);
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetOffensiveItemList"))
			{
				Event @event = Event.New("AIGetOffensiveItemList", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Distance", Distance);
				@event.SetParameter("List", aigetOffensiveItemListEvent.List);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AIGetOffensiveItemListEvent.ID, IAIItemCommandListEvent.CascadeLevel))
			{
				aigetOffensiveItemListEvent.Actor = Actor;
				aigetOffensiveItemListEvent.Target = Target;
				aigetOffensiveItemListEvent.Distance = Distance;
				flag = Actor.HandleEvent(aigetOffensiveItemListEvent);
			}
			return aigetOffensiveItemListEvent.List;
		}

		// Token: 0x04002114 RID: 8468
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AIGetOffensiveItemListEvent), null, new MinEvent.EventPoolCount(AIGetOffensiveItemListEvent.CountPool), new MinEvent.EventPoolReset(AIGetOffensiveItemListEvent.ResetPool));

		// Token: 0x04002115 RID: 8469
		private static List<AIGetOffensiveItemListEvent> Pool;

		// Token: 0x04002116 RID: 8470
		private static int PoolCounter;
	}
}
