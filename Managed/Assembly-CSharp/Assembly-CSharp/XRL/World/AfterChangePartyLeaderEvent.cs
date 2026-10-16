using System;

namespace XRL.World
{
	// Token: 0x02000657 RID: 1623
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class AfterChangePartyLeaderEvent : PooledEvent<AfterChangePartyLeaderEvent>
	{
		// Token: 0x06004637 RID: 17975 RVA: 0x00165F54 File Offset: 0x00164154
		public override int GetCascadeLevel()
		{
			return AfterChangePartyLeaderEvent.CascadeLevel;
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x00165F5B File Offset: 0x0016415B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x00165F64 File Offset: 0x00164164
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.NewLeader = null;
			this.OldLeader = null;
			this.Transient = false;
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x00165F88 File Offset: 0x00164188
		public static void Send(GameObject Actor, GameObject NewLeader, GameObject OldLeader, bool Transient = false)
		{
			if (NewLeader == OldLeader)
			{
				return;
			}
			if (!GameObject.Validate(ref Actor))
			{
				return;
			}
			bool flag = Actor.WantEvent(PooledEvent<AfterChangePartyLeaderEvent>.ID, AfterChangePartyLeaderEvent.CascadeLevel);
			bool flag2 = NewLeader != null && NewLeader.WantEvent(PooledEvent<AfterChangePartyLeaderEvent>.ID, AfterChangePartyLeaderEvent.CascadeLevel);
			bool flag3 = OldLeader != null && OldLeader.WantEvent(PooledEvent<AfterChangePartyLeaderEvent>.ID, AfterChangePartyLeaderEvent.CascadeLevel);
			if (flag || flag2 || flag3)
			{
				AfterChangePartyLeaderEvent afterChangePartyLeaderEvent = PooledEvent<AfterChangePartyLeaderEvent>.FromPool();
				afterChangePartyLeaderEvent.Actor = Actor;
				afterChangePartyLeaderEvent.NewLeader = NewLeader;
				afterChangePartyLeaderEvent.OldLeader = OldLeader;
				afterChangePartyLeaderEvent.Transient = Transient;
				if (flag)
				{
					Actor.HandleEvent(afterChangePartyLeaderEvent);
				}
				if (flag2)
				{
					NewLeader.HandleEvent(afterChangePartyLeaderEvent);
				}
				if (flag3)
				{
					OldLeader.HandleEvent(afterChangePartyLeaderEvent);
				}
			}
		}

		// Token: 0x04002156 RID: 8534
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002157 RID: 8535
		public GameObject Actor;

		// Token: 0x04002158 RID: 8536
		public GameObject NewLeader;

		// Token: 0x04002159 RID: 8537
		public GameObject OldLeader;

		// Token: 0x0400215A RID: 8538
		public bool Transient;
	}
}
