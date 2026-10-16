using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200134A RID: 4938
	[Serializable]
	public class LongBladesDuelingStance : LongBladesSkillBase
	{
		// Token: 0x0600C096 RID: 49302 RVA: 0x0046826A File Offset: 0x0046646A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C097 RID: 49303 RVA: 0x00468280 File Offset: 0x00466480
		public void CollectStats(Templates.StatCollector stats)
		{
			if (this.ParentObject.HasPart<LongBladesImprovedDuelistStance>())
			{
				stats.Set("HitBonus", 3, true, 1);
				stats.AddChangePostfix("To-hit bonus", 1, "Improved Dueling Stance");
				return;
			}
			stats.Set("HitBonus", 2, false, 0);
		}

		// Token: 0x0600C098 RID: 49304 RVA: 0x004682BE File Offset: 0x004664BE
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C099 RID: 49305 RVA: 0x004682E4 File Offset: 0x004664E4
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Dueling Stance", "CommandDuelingStance", "Stances", "+2/3 to hit while wielding a long blade in your primary hand", "\u009f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C09A RID: 49306 RVA: 0x00468330 File Offset: 0x00466530
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D51 RID: 19793
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
