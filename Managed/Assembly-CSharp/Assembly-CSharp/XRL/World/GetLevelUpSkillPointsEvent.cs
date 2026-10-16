using System;

namespace XRL.World
{
	// Token: 0x0200075F RID: 1887
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetLevelUpSkillPointsEvent : PooledEvent<GetLevelUpSkillPointsEvent>
	{
		// Token: 0x06004DCF RID: 19919 RVA: 0x0017E0D1 File Offset: 0x0017C2D1
		public override int GetCascadeLevel()
		{
			return GetLevelUpSkillPointsEvent.CascadeLevel;
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0017E0D8 File Offset: 0x0017C2D8
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0017E0E1 File Offset: 0x0017C2E1
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.BaseAmount = 0;
			this.Amount = 0;
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0017E0FE File Offset: 0x0017C2FE
		public static GetLevelUpSkillPointsEvent FromPool(GameObject Actor, int BaseAmount, int Amount)
		{
			GetLevelUpSkillPointsEvent getLevelUpSkillPointsEvent = PooledEvent<GetLevelUpSkillPointsEvent>.FromPool();
			getLevelUpSkillPointsEvent.Actor = Actor;
			getLevelUpSkillPointsEvent.BaseAmount = BaseAmount;
			getLevelUpSkillPointsEvent.Amount = Amount;
			return getLevelUpSkillPointsEvent;
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x0017E11C File Offset: 0x0017C31C
		public static int GetFor(GameObject Actor, int BaseAmount)
		{
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetLevelUpSkillPointsEvent>.ID, GetLevelUpSkillPointsEvent.CascadeLevel))
			{
				GetLevelUpSkillPointsEvent getLevelUpSkillPointsEvent = GetLevelUpSkillPointsEvent.FromPool(Actor, BaseAmount, BaseAmount);
				Actor.HandleEvent(getLevelUpSkillPointsEvent);
				return getLevelUpSkillPointsEvent.Amount;
			}
			return BaseAmount;
		}

		// Token: 0x040025D4 RID: 9684
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040025D5 RID: 9685
		public GameObject Actor;

		// Token: 0x040025D6 RID: 9686
		public int BaseAmount;

		// Token: 0x040025D7 RID: 9687
		public int Amount;
	}
}
