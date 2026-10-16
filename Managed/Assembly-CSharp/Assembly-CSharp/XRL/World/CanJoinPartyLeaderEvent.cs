using System;

namespace XRL.World
{
	// Token: 0x020006C7 RID: 1735
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanJoinPartyLeaderEvent : PooledEvent<CanJoinPartyLeaderEvent>
	{
		// Token: 0x06004974 RID: 18804 RVA: 0x00171C49 File Offset: 0x0016FE49
		public override int GetCascadeLevel()
		{
			return CanJoinPartyLeaderEvent.CascadeLevel;
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x00171C50 File Offset: 0x0016FE50
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x00171C59 File Offset: 0x0016FE59
		public override void Reset()
		{
			base.Reset();
			this.Companion = null;
			this.Leader = null;
			this.CurrentCell = null;
			this.TargetCell = null;
			this.DistanceFromCurrentCell = 0;
			this.DistanceFromLeader = 0;
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x00171C8C File Offset: 0x0016FE8C
		public static bool Check(GameObject Companion, GameObject Leader, Cell CurrentCell, Cell TargetCell, int DistanceFromCurrentCell, int DistanceFromLeader)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Companion) && Companion.HasRegisteredEvent("CanJoinPartyLeader"))
			{
				Event @event = Event.New("CanJoinPartyLeader", 0, 0, 0);
				@event.SetParameter("Companion", Companion);
				@event.SetParameter("Leader", Leader);
				@event.SetParameter("CurrentCell", CurrentCell);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetParameter("DistanceFromCurrentCell", DistanceFromCurrentCell);
				@event.SetParameter("DistanceFromLeader", DistanceFromLeader);
				flag = Companion.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Companion) && Companion.WantEvent(PooledEvent<CanJoinPartyLeaderEvent>.ID, CanJoinPartyLeaderEvent.CascadeLevel))
			{
				CanJoinPartyLeaderEvent canJoinPartyLeaderEvent = PooledEvent<CanJoinPartyLeaderEvent>.FromPool();
				canJoinPartyLeaderEvent.Companion = Companion;
				canJoinPartyLeaderEvent.Leader = Leader;
				canJoinPartyLeaderEvent.CurrentCell = CurrentCell;
				canJoinPartyLeaderEvent.TargetCell = TargetCell;
				canJoinPartyLeaderEvent.DistanceFromCurrentCell = DistanceFromCurrentCell;
				canJoinPartyLeaderEvent.DistanceFromLeader = DistanceFromLeader;
				flag = Companion.HandleEvent(canJoinPartyLeaderEvent);
			}
			return flag;
		}

		// Token: 0x04002349 RID: 9033
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400234A RID: 9034
		public GameObject Companion;

		// Token: 0x0400234B RID: 9035
		public GameObject Leader;

		// Token: 0x0400234C RID: 9036
		public Cell CurrentCell;

		// Token: 0x0400234D RID: 9037
		public Cell TargetCell;

		// Token: 0x0400234E RID: 9038
		public int DistanceFromCurrentCell;

		// Token: 0x0400234F RID: 9039
		public int DistanceFromLeader;
	}
}
