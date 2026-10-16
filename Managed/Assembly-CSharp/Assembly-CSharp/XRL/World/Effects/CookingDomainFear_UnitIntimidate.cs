using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000CA7 RID: 3239
	[Serializable]
	public class CookingDomainFear_UnitIntimidate : ProceduralCookingEffectUnitSkill<Persuasion_Intimidate>
	{
		// Token: 0x060081DF RID: 33247 RVA: 0x002F6DFA File Offset: 0x002F4FFA
		public override string GetDescription()
		{
			if (this.skillWasAdded)
			{
				return "Can use " + base.SkillDisplayName + ".";
			}
			return (this.Tier * 2).Signed(false) + " bonus on Ego roll when using Intimidate.";
		}

		// Token: 0x060081E0 RID: 33248 RVA: 0x002F6E34 File Offset: 0x002F5034
		public override string GetTemplatedDescription()
		{
			string text = "Can use " + base.SkillDisplayName + ".";
			if (this.BonusTier != "0")
			{
				text = string.Concat(new string[]
				{
					text,
					" If @they already have ",
					base.SkillDisplayName,
					", gain a ",
					(Convert.ToInt32(this.BonusTier) * 2).Signed(false),
					" bonus on the Ego roll when using ",
					base.SkillDisplayName,
					"."
				});
			}
			return text;
		}
	}
}
