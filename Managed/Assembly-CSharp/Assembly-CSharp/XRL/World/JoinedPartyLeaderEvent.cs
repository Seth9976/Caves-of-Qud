using System;

namespace XRL.World
{
	// Token: 0x020007F8 RID: 2040
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class JoinedPartyLeaderEvent : PooledEvent<JoinedPartyLeaderEvent>
	{
		// Token: 0x060051B9 RID: 20921 RVA: 0x0018A70B File Offset: 0x0018890B
		public override int GetCascadeLevel()
		{
			return JoinedPartyLeaderEvent.CascadeLevel;
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x0018A712 File Offset: 0x00188912
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x0018A71B File Offset: 0x0018891B
		public override void Reset()
		{
			base.Reset();
			this.Companion = null;
			this.Leader = null;
			this.PreviousCell = null;
			this.TargetCell = null;
			this.DistanceFromPreviousCell = 0;
			this.DistanceFromLeader = 0;
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x0018A750 File Offset: 0x00188950
		public static void Send(GameObject Companion, GameObject Leader, Cell PreviousCell, Cell TargetCell, int DistanceFromPreviousCell, int DistanceFromLeader)
		{
			bool flag = Companion.HasRegisteredEvent("JoinedPartyLeader");
			bool flag2 = Leader.HasRegisteredEvent("JoinedPartyLeader");
			if (flag || flag2)
			{
				Event @event = Event.New("JoinedPartyLeader", 0, 0, 0);
				@event.SetParameter("Companion", Companion);
				@event.SetParameter("Leader", Leader);
				@event.SetParameter("PreviousCell", PreviousCell);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetParameter("DistanceFromPreviousCell", DistanceFromPreviousCell);
				@event.SetParameter("DistanceFromLeader", DistanceFromLeader);
				if (flag)
				{
					Companion.FireEvent(@event);
				}
				if (flag2)
				{
					Leader.FireEvent(@event);
				}
			}
			bool flag3 = Companion.WantEvent(PooledEvent<JoinedPartyLeaderEvent>.ID, JoinedPartyLeaderEvent.CascadeLevel);
			bool flag4 = Leader.WantEvent(PooledEvent<JoinedPartyLeaderEvent>.ID, JoinedPartyLeaderEvent.CascadeLevel);
			if (flag3 || flag4)
			{
				JoinedPartyLeaderEvent joinedPartyLeaderEvent = PooledEvent<JoinedPartyLeaderEvent>.FromPool();
				joinedPartyLeaderEvent.Companion = Companion;
				joinedPartyLeaderEvent.Leader = Leader;
				joinedPartyLeaderEvent.PreviousCell = PreviousCell;
				joinedPartyLeaderEvent.TargetCell = TargetCell;
				joinedPartyLeaderEvent.DistanceFromPreviousCell = DistanceFromPreviousCell;
				joinedPartyLeaderEvent.DistanceFromLeader = DistanceFromLeader;
				if (flag)
				{
					Companion.HandleEvent(joinedPartyLeaderEvent);
				}
				if (flag2)
				{
					Leader.HandleEvent(joinedPartyLeaderEvent);
				}
			}
		}

		// Token: 0x040028FE RID: 10494
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028FF RID: 10495
		public GameObject Companion;

		// Token: 0x04002900 RID: 10496
		public GameObject Leader;

		// Token: 0x04002901 RID: 10497
		public Cell PreviousCell;

		// Token: 0x04002902 RID: 10498
		public Cell TargetCell;

		// Token: 0x04002903 RID: 10499
		public int DistanceFromPreviousCell;

		// Token: 0x04002904 RID: 10500
		public int DistanceFromLeader;
	}
}
