using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x02000648 RID: 1608
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AIGetPassiveItemListEvent : IAIItemCommandListEvent
	{
		// Token: 0x060045C2 RID: 17858 RVA: 0x00164C21 File Offset: 0x00162E21
		public AIGetPassiveItemListEvent()
		{
			this.ID = AIGetPassiveItemListEvent.ID;
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x00164C34 File Offset: 0x00162E34
		public static int CountPool()
		{
			if (AIGetPassiveItemListEvent.Pool != null)
			{
				return AIGetPassiveItemListEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x00164C49 File Offset: 0x00162E49
		public static void ResetPool()
		{
			while (AIGetPassiveItemListEvent.PoolCounter > 0)
			{
				AIGetPassiveItemListEvent.Pool[--AIGetPassiveItemListEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x00164C71 File Offset: 0x00162E71
		public static void ResetTo(ref AIGetPassiveItemListEvent E)
		{
			MinEvent.ResetTo<AIGetPassiveItemListEvent>(E, AIGetPassiveItemListEvent.Pool, ref AIGetPassiveItemListEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x00164C87 File Offset: 0x00162E87
		public static AIGetPassiveItemListEvent FromPool()
		{
			return MinEvent.FromPool<AIGetPassiveItemListEvent>(ref AIGetPassiveItemListEvent.Pool, ref AIGetPassiveItemListEvent.PoolCounter, 8192);
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x00164C9D File Offset: 0x00162E9D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x00164CB4 File Offset: 0x00162EB4
		public static List<AICommandList> GetFor(GameObject Actor, GameObject Target = null, int Distance = -1)
		{
			AIGetPassiveItemListEvent aigetPassiveItemListEvent = AIGetPassiveItemListEvent.FromPool();
			if (Target == null)
			{
				Target = ((Actor != null) ? Actor.Target : null);
			}
			if (Distance == -1 && GameObject.Validate(ref Actor) && GameObject.Validate(ref Target))
			{
				Distance = Actor.DistanceTo(Target);
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIGetPassiveItemList"))
			{
				Event @event = Event.New("AIGetPassiveItemList", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Distance", Distance);
				@event.SetParameter("List", aigetPassiveItemListEvent.List);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AIGetPassiveItemListEvent.ID, IAIItemCommandListEvent.CascadeLevel))
			{
				aigetPassiveItemListEvent.Actor = Actor;
				aigetPassiveItemListEvent.Target = Target;
				aigetPassiveItemListEvent.Distance = Distance;
				flag = Actor.HandleEvent(aigetPassiveItemListEvent);
			}
			return aigetPassiveItemListEvent.List;
		}

		// Token: 0x0400211A RID: 8474
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AIGetPassiveItemListEvent), null, new MinEvent.EventPoolCount(AIGetPassiveItemListEvent.CountPool), new MinEvent.EventPoolReset(AIGetPassiveItemListEvent.ResetPool));

		// Token: 0x0400211B RID: 8475
		private static List<AIGetPassiveItemListEvent> Pool;

		// Token: 0x0400211C RID: 8476
		private static int PoolCounter;
	}
}
