using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200064C RID: 1612
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ActorGetNavigationWeightEvent : INavigationWeightEvent
	{
		// Token: 0x060045DD RID: 17885 RVA: 0x00165317 File Offset: 0x00163517
		public ActorGetNavigationWeightEvent()
		{
			this.ID = ActorGetNavigationWeightEvent.ID;
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x0016532A File Offset: 0x0016352A
		public static int CountPool()
		{
			if (ActorGetNavigationWeightEvent.Pool != null)
			{
				return ActorGetNavigationWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x0016533F File Offset: 0x0016353F
		public static void ResetPool()
		{
			while (ActorGetNavigationWeightEvent.PoolCounter > 0)
			{
				ActorGetNavigationWeightEvent.Pool[--ActorGetNavigationWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x00165367 File Offset: 0x00163567
		public static void ResetTo(ref ActorGetNavigationWeightEvent E)
		{
			MinEvent.ResetTo<ActorGetNavigationWeightEvent>(E, ActorGetNavigationWeightEvent.Pool, ref ActorGetNavigationWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x0016537D File Offset: 0x0016357D
		public static ActorGetNavigationWeightEvent FromPool()
		{
			return MinEvent.FromPool<ActorGetNavigationWeightEvent>(ref ActorGetNavigationWeightEvent.Pool, ref ActorGetNavigationWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x00165393 File Offset: 0x00163593
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x001653A8 File Offset: 0x001635A8
		public static void Process(INavigationWeightEvent Parent)
		{
			if (Parent.Weight >= 100 || !Parent.Actor.IsValid())
			{
				return;
			}
			ActorGetNavigationWeightEvent actorGetNavigationWeightEvent = ActorGetNavigationWeightEvent.FromPool();
			Parent.ApplyTo(actorGetNavigationWeightEvent);
			actorGetNavigationWeightEvent.PriorWeight = actorGetNavigationWeightEvent.Weight;
			actorGetNavigationWeightEvent.Actor.HandleEvent(actorGetNavigationWeightEvent);
			actorGetNavigationWeightEvent.ApplyTo(Parent);
			ActorGetNavigationWeightEvent.ResetTo(ref actorGetNavigationWeightEvent);
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x00165404 File Offset: 0x00163604
		public static int GetFor(Cell Cell, GameObject Actor, ref bool Uncacheable, int Weight = 0, int Nav = 0)
		{
			if (Weight >= 100)
			{
				return Weight;
			}
			if (Cell != null && GameObject.Validate(ref Actor) && Actor.WantEvent(ActorGetNavigationWeightEvent.ID, MinEvent.CascadeLevel))
			{
				ActorGetNavigationWeightEvent actorGetNavigationWeightEvent = ActorGetNavigationWeightEvent.FromPool();
				actorGetNavigationWeightEvent.Cell = Cell;
				actorGetNavigationWeightEvent.Actor = Actor;
				actorGetNavigationWeightEvent.Object = null;
				actorGetNavigationWeightEvent.Uncacheable = Uncacheable;
				actorGetNavigationWeightEvent.Weight = Weight;
				actorGetNavigationWeightEvent.PriorWeight = Weight;
				actorGetNavigationWeightEvent.Nav = Nav;
				Actor.HandleEvent(actorGetNavigationWeightEvent);
				Weight = actorGetNavigationWeightEvent.Weight;
				Uncacheable = actorGetNavigationWeightEvent.Uncacheable;
			}
			return Weight;
		}

		// Token: 0x0400212C RID: 8492
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ActorGetNavigationWeightEvent), null, new MinEvent.EventPoolCount(ActorGetNavigationWeightEvent.CountPool), new MinEvent.EventPoolReset(ActorGetNavigationWeightEvent.ResetPool));

		// Token: 0x0400212D RID: 8493
		private static List<ActorGetNavigationWeightEvent> Pool;

		// Token: 0x0400212E RID: 8494
		private static int PoolCounter;
	}
}
