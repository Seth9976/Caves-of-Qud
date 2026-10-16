using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013AB RID: 5035
	[Serializable]
	public class Tactics_Throwing : BaseSkill
	{
		// Token: 0x0600C28F RID: 49807 RVA: 0x004717BD File Offset: 0x0046F9BD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C290 RID: 49808 RVA: 0x004717C7 File Offset: 0x0046F9C7
		public override bool AddSkill(GameObject GO)
		{
			GO.ModIntProperty("CloseThrowRangeAccuracySkillBonus", 50, false);
			GO.ModIntProperty("ThrowRangeSkillBonus", 3, false);
			GO.ModIntProperty("ThrowToHitSkillBonus", 2, false);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C291 RID: 49809 RVA: 0x004717FB File Offset: 0x0046F9FB
		public override bool RemoveSkill(GameObject GO)
		{
			GO.ModIntProperty("CloseThrowRangeAccuracySkillBonus", -50, true);
			GO.ModIntProperty("ThrowRangeSkillBonus", -3, true);
			GO.ModIntProperty("ThrowToHitSkillBonus", -2, true);
			return base.AddSkill(GO);
		}
	}
}
