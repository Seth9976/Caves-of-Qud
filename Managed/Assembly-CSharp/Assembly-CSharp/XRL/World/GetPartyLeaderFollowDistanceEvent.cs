using System;

namespace XRL.World
{
	// Token: 0x02000773 RID: 1907
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetPartyLeaderFollowDistanceEvent : PooledEvent<GetPartyLeaderFollowDistanceEvent>
	{
		// Token: 0x06004E68 RID: 20072 RVA: 0x001805C5 File Offset: 0x0017E7C5
		public override int GetCascadeLevel()
		{
			return GetPartyLeaderFollowDistanceEvent.CascadeLevel;
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x001805CC File Offset: 0x0017E7CC
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x001805D5 File Offset: 0x0017E7D5
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.PartyLeader = null;
			this.Distance = 0;
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x001805F4 File Offset: 0x0017E7F4
		public static int GetFor(GameObject Object, GameObject PartyLeader = null, int? DefaultDistance = null)
		{
			bool flag = true;
			if (PartyLeader == null)
			{
				PartyLeader = Object.PartyLeader;
			}
			int num = DefaultDistance ?? Object.GetIntPropertyIfSet("PartyLeaderFollowDistance") ?? (PartyLeader.IsPlayer() ? GetPartyLeaderFollowDistanceEvent.DEFAULT_DISTANCE_PLAYER_LED : GetPartyLeaderFollowDistanceEvent.DEFAULT_DISTANCE_NON_PLAYER_LED);
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetPartyLeaderFollowDistance"))
			{
				Event @event = Event.New("GetPartyLeaderFollowDistance", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("PartyLeader", PartyLeader);
				@event.SetParameter("Distance", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Distance", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetPartyLeaderFollowDistanceEvent>.ID, GetPartyLeaderFollowDistanceEvent.CascadeLevel))
			{
				GetPartyLeaderFollowDistanceEvent getPartyLeaderFollowDistanceEvent = PooledEvent<GetPartyLeaderFollowDistanceEvent>.FromPool();
				getPartyLeaderFollowDistanceEvent.Object = Object;
				getPartyLeaderFollowDistanceEvent.PartyLeader = PartyLeader;
				getPartyLeaderFollowDistanceEvent.Distance = num;
				flag = Object.HandleEvent(getPartyLeaderFollowDistanceEvent);
				num = getPartyLeaderFollowDistanceEvent.Distance;
			}
			return num;
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x0018070D File Offset: 0x0017E90D
		public void MinDistance(int Value)
		{
			if (this.Distance < Value)
			{
				this.Distance = Value;
			}
		}

		// Token: 0x04002647 RID: 9799
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002648 RID: 9800
		public static readonly int DEFAULT_DISTANCE_PLAYER_LED = 1;

		// Token: 0x04002649 RID: 9801
		public static readonly int DEFAULT_DISTANCE_NON_PLAYER_LED = 5;

		// Token: 0x0400264A RID: 9802
		public GameObject Object;

		// Token: 0x0400264B RID: 9803
		public GameObject PartyLeader;

		// Token: 0x0400264C RID: 9804
		public int Distance;
	}
}
