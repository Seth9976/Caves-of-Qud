using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200137A RID: 4986
	[Serializable]
	public class Discipline_Conatus : BaseSkill
	{
		// Token: 0x0600C198 RID: 49560 RVA: 0x0046C741 File Offset: 0x0046A941
		public override void Initialize()
		{
			base.Initialize();
			Run.SyncAbility(this.ParentObject, false);
		}

		// Token: 0x0600C199 RID: 49561 RVA: 0x0046C755 File Offset: 0x0046A955
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSprintDurationEvent>.ID;
		}

		// Token: 0x0600C19A RID: 49562 RVA: 0x0046C76B File Offset: 0x0046A96B
		public override bool HandleEvent(GetSprintDurationEvent E)
		{
			E.LinearIncrease += 10;
			Templates.StatCollector stats = E.Stats;
			if (stats != null)
			{
				stats.AddLinearBonusModifier("Duration", 10, base.DisplayName + " skill");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C19B RID: 49563 RVA: 0x0046C7AB File Offset: 0x0046A9AB
		public override bool AddSkill(GameObject GO)
		{
			if (!GO.HasSkill("Tactics_Run"))
			{
				GO.AddSkill("Tactics_Run", null, null);
			}
			return base.AddSkill(GO);
		}
	}
}
