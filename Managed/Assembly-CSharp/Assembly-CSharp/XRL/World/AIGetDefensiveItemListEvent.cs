using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x02000643 RID: 1603
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AIGetDefensiveItemListEvent : IAIItemCommandListEvent
	{
		// Token: 0x0600459A RID: 17818 RVA: 0x00164231 File Offset: 0x00162431
		public AIGetDefensiveItemListEvent()
		{
			this.ID = AIGetDefensiveItemListEvent.ID;
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x00164244 File Offset: 0x00162444
		public static int CountPool()
		{
			if (AIGetDefensiveItemListEvent.Pool != null)
			{
				return AIGetDefensiveItemListEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x00164259 File Offset: 0x00162459
		public static void ResetPool()
		{
			while (AIGetDefensiveItemListEvent.PoolCounter > 0)
			{
				AIGetDefensiveItemListEvent.Pool[--AIGetDefensiveItemListEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x00164281 File Offset: 0x00162481
		public static void ResetTo(ref AIGetDefensiveItemListEvent E)
		{
			MinEvent.ResetTo<AIGetDefensiveItemListEvent>(E, AIGetDefensiveItemListEvent.Pool, ref AIGetDefensiveItemListEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x00164297 File Offset: 0x00162497
		public static AIGetDefensiveItemListEvent FromPool()
		{
			return MinEvent.FromPool<AIGetDefensiveItemListEvent>(ref AIGetDefensiveItemListEvent.Pool, ref AIGetDefensiveItemListEvent.PoolCounter, 8192);
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x001642AD File Offset: 0x001624AD
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x001642C4 File Offset: 0x001624C4
		public static List<AICommandList> GetFor(GameObject Actor, GameObject Target = null, int Distance = -1)
		{
			AIGetDefensiveItemListEvent aigetDefensiveItemListEvent = AIGetDefensiveItemListEvent.FromPool();
			if (Target == null)
			{
				Target = ((Actor != null) ? Actor.Target : null);
			}
			if (Distance == -1 && GameObject.Validate(ref Actor) && GameObject.Validate(ref Target))
			{
				Distance = Actor.DistanceTo(Target);
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetDefensiveItemList"))
			{
				Event @event = Event.New("AIGetDefensiveItemList", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Distance", Distance);
				@event.SetParameter("List", aigetDefensiveItemListEvent.List);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AIGetDefensiveItemListEvent.ID, IAIItemCommandListEvent.CascadeLevel))
			{
				aigetDefensiveItemListEvent.Actor = Actor;
				aigetDefensiveItemListEvent.Target = Target;
				aigetDefensiveItemListEvent.Distance = Distance;
				flag = Actor.HandleEvent(aigetDefensiveItemListEvent);
			}
			return aigetDefensiveItemListEvent.List;
		}

		// Token: 0x0400210B RID: 8459
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AIGetDefensiveItemListEvent), null, new MinEvent.EventPoolCount(AIGetDefensiveItemListEvent.CountPool), new MinEvent.EventPoolReset(AIGetDefensiveItemListEvent.ResetPool));

		// Token: 0x0400210C RID: 8460
		private static List<AIGetDefensiveItemListEvent> Pool;

		// Token: 0x0400210D RID: 8461
		private static int PoolCounter;
	}
}
