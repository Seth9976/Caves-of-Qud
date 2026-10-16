using System;
using XRL.Collections;

namespace XRL.World
{
	// Token: 0x0200074C RID: 1868
	[GameEvent(Cascade = 64, Cache = MinEvent.Cache.Pool)]
	public class GetFeelingEvent : PooledEvent<GetFeelingEvent>
	{
		// Token: 0x06004D4B RID: 19787 RVA: 0x0017C876 File Offset: 0x0017AA76
		public override int GetCascadeLevel()
		{
			return GetFeelingEvent.CascadeLevel;
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x0017C87D File Offset: 0x0017AA7D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x0017C888 File Offset: 0x0017AA88
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.ActorLeader = null;
			this.Target = null;
			this.TargetLeader = null;
			this.BaseFeeling = 0;
			this.Feeling = 0;
			this.Combat = false;
			this.Personal = false;
			this.Faction = false;
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0017C8DC File Offset: 0x0017AADC
		public static int GetFor(GameObject Actor, GameObject Target, int BaseFeeling, GameObject ActorLeader = null, GameObject TargetLeader = null, bool Combat = false, bool Personal = false, bool Faction = false)
		{
			int num = BaseFeeling;
			EventRegistry.List list;
			if (Actor.RegisteredEvents != null && Actor.RegisteredEvents.TryGetValue(PooledEvent<GetFeelingEvent>.ID, out list))
			{
				GetFeelingEvent getFeelingEvent = PooledEvent<GetFeelingEvent>.FromPool();
				getFeelingEvent.Actor = Actor;
				getFeelingEvent.ActorLeader = ActorLeader;
				getFeelingEvent.Target = Target;
				getFeelingEvent.TargetLeader = TargetLeader;
				getFeelingEvent.BaseFeeling = BaseFeeling;
				getFeelingEvent.Feeling = BaseFeeling;
				getFeelingEvent.Combat = Combat;
				getFeelingEvent.Personal = Personal;
				getFeelingEvent.Faction = Faction;
				list.Dispatch(getFeelingEvent);
				num = getFeelingEvent.Feeling;
				PooledEvent<GetFeelingEvent>.ResetTo(ref getFeelingEvent);
			}
			return num;
		}

		// Token: 0x0400257A RID: 9594
		public new static readonly int CascadeLevel = 64;

		// Token: 0x0400257B RID: 9595
		public GameObject Actor;

		// Token: 0x0400257C RID: 9596
		public GameObject ActorLeader;

		// Token: 0x0400257D RID: 9597
		public GameObject Target;

		// Token: 0x0400257E RID: 9598
		public GameObject TargetLeader;

		// Token: 0x0400257F RID: 9599
		public int BaseFeeling;

		// Token: 0x04002580 RID: 9600
		public int Feeling;

		/// <summary>Actor and Target are engaged in combat.</summary>
		// Token: 0x04002581 RID: 9601
		public bool Combat;

		/// <summary>BaseFeeling was retrieved from personal memory.</summary>
		// Token: 0x04002582 RID: 9602
		public bool Personal;

		/// <summary>BaseFeeling is based on faction feeling.</summary>
		// Token: 0x04002583 RID: 9603
		public bool Faction;
	}
}
