using System;

namespace XRL.World
{
	// Token: 0x02000794 RID: 1940
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class GetSwimmingPerformanceEvent : PooledEvent<GetSwimmingPerformanceEvent>
	{
		// Token: 0x06004F45 RID: 20293 RVA: 0x001830EE File Offset: 0x001812EE
		public override int GetCascadeLevel()
		{
			return GetSwimmingPerformanceEvent.CascadeLevel;
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x001830F5 File Offset: 0x001812F5
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x001830FE File Offset: 0x001812FE
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.MoveSpeedPenalty = 0;
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x00183114 File Offset: 0x00181314
		public static GetSwimmingPerformanceEvent FromPool(GameObject Actor, int MoveSpeedPenalty)
		{
			GetSwimmingPerformanceEvent getSwimmingPerformanceEvent = PooledEvent<GetSwimmingPerformanceEvent>.FromPool();
			getSwimmingPerformanceEvent.Actor = Actor;
			getSwimmingPerformanceEvent.MoveSpeedPenalty = MoveSpeedPenalty;
			return getSwimmingPerformanceEvent;
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x0018312C File Offset: 0x0018132C
		public static bool GetFor(GameObject Actor, ref int MoveSpeedPenalty)
		{
			bool flag = GameObject.Validate(ref Actor);
			if (flag && Actor.HasRegisteredEvent("GetSwimmingPerformance"))
			{
				Event @event = Event.New("GetSwimmingPerformance", "Actor", Actor, "MoveSpeedPenalty", MoveSpeedPenalty);
				flag = Actor.FireEvent(@event);
				MoveSpeedPenalty = @event.GetIntParameter("MoveSpeedPenalty", 0);
			}
			if (flag && Actor.WantEvent(PooledEvent<GetSwimmingPerformanceEvent>.ID, GetSwimmingPerformanceEvent.CascadeLevel))
			{
				GetSwimmingPerformanceEvent getSwimmingPerformanceEvent = GetSwimmingPerformanceEvent.FromPool(Actor, MoveSpeedPenalty);
				flag = Actor.HandleEvent(getSwimmingPerformanceEvent);
				MoveSpeedPenalty = getSwimmingPerformanceEvent.MoveSpeedPenalty;
			}
			return flag;
		}

		// Token: 0x040026F4 RID: 9972
		public new static readonly int CascadeLevel = 1;

		// Token: 0x040026F5 RID: 9973
		public GameObject Actor;

		// Token: 0x040026F6 RID: 9974
		public int MoveSpeedPenalty;
	}
}
