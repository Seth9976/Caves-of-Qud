using System;

namespace XRL.World
{
	// Token: 0x020007A2 RID: 1954
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class GetWadingPerformanceEvent : PooledEvent<GetWadingPerformanceEvent>
	{
		// Token: 0x06004FA9 RID: 20393 RVA: 0x00184E0B File Offset: 0x0018300B
		public override int GetCascadeLevel()
		{
			return GetWadingPerformanceEvent.CascadeLevel;
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x00184E12 File Offset: 0x00183012
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x00184E1B File Offset: 0x0018301B
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.MoveSpeedPenalty = 0;
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x00184E31 File Offset: 0x00183031
		public static GetWadingPerformanceEvent FromPool(GameObject Actor, int MoveSpeedPenalty)
		{
			GetWadingPerformanceEvent getWadingPerformanceEvent = PooledEvent<GetWadingPerformanceEvent>.FromPool();
			getWadingPerformanceEvent.Actor = Actor;
			getWadingPerformanceEvent.MoveSpeedPenalty = MoveSpeedPenalty;
			return getWadingPerformanceEvent;
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x00184E48 File Offset: 0x00183048
		public static bool GetFor(GameObject Actor, ref int MoveSpeedPenalty)
		{
			if (Actor != null)
			{
				if (Actor.HasRegisteredEvent("GetWadingPerformance"))
				{
					Event @event = Event.New("GetWadingPerformance", "Actor", Actor, "MoveSpeedPenalty", MoveSpeedPenalty);
					bool flag = Actor.FireEvent(@event);
					MoveSpeedPenalty = @event.GetIntParameter("MoveSpeedPenalty", 0);
					if (!flag)
					{
						return false;
					}
				}
				if (Actor.WantEvent(PooledEvent<GetWadingPerformanceEvent>.ID, GetWadingPerformanceEvent.CascadeLevel))
				{
					GetWadingPerformanceEvent getWadingPerformanceEvent = GetWadingPerformanceEvent.FromPool(Actor, MoveSpeedPenalty);
					bool flag2 = Actor.HandleEvent(getWadingPerformanceEvent);
					MoveSpeedPenalty = getWadingPerformanceEvent.MoveSpeedPenalty;
					if (!flag2)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0400275B RID: 10075
		public new static readonly int CascadeLevel = 1;

		// Token: 0x0400275C RID: 10076
		public GameObject Actor;

		// Token: 0x0400275D RID: 10077
		public int MoveSpeedPenalty;
	}
}
