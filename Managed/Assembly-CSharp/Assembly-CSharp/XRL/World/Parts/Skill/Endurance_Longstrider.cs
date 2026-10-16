using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200133E RID: 4926
	[Serializable]
	public class Endurance_Longstrider : BaseSkill
	{
		// Token: 0x0600C053 RID: 49235 RVA: 0x0046762E File Offset: 0x0046582E
		public override void Initialize()
		{
			base.Initialize();
			Run.SyncAbility(this.ParentObject, false);
		}

		// Token: 0x0600C054 RID: 49236 RVA: 0x00467642 File Offset: 0x00465842
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSprintDurationEvent>.ID;
		}

		// Token: 0x0600C055 RID: 49237 RVA: 0x00467658 File Offset: 0x00465858
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

		// Token: 0x0600C056 RID: 49238 RVA: 0x00467698 File Offset: 0x00465898
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
