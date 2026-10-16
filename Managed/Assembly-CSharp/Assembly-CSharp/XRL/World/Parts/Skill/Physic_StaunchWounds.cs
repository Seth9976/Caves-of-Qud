using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001365 RID: 4965
	[Serializable]
	public class Physic_StaunchWounds : BaseSkill
	{
		// Token: 0x0600C13F RID: 49471 RVA: 0x0046B4BE File Offset: 0x004696BE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetBandagePerformanceEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C140 RID: 49472 RVA: 0x0046B4DC File Offset: 0x004696DC
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C141 RID: 49473 RVA: 0x0046B500 File Offset: 0x00469700
		public override bool HandleEvent(GetBandagePerformanceEvent E)
		{
			if (E.Pass == 1 && E.Actor == this.ParentObject && E.Checking == "Actor")
			{
				E.Performance += GlobalConfig.GetIntSetting("StaunchWoundsBandagePerformanceBonus", Physic_StaunchWounds.DEFAULT_BANDAGE_PERFORMANCE_BONUS);
			}
			else if (E.Pass == 2 && E.Actor == this.ParentObject && E.Checking == "Actor")
			{
				E.Performance *= GlobalConfig.GetIntSetting("StaunchWoundsBandagePerformanceFactor", Physic_StaunchWounds.DEFAULT_BANDAGE_PERFORMANCE_FACTOR);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004D6F RID: 19823
		public static readonly int DEFAULT_BANDAGE_PERFORMANCE_BONUS = 1;

		// Token: 0x04004D70 RID: 19824
		public static readonly int DEFAULT_BANDAGE_PERFORMANCE_FACTOR = 2;
	}
}
