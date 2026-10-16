using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001391 RID: 5009
	[Serializable]
	public abstract class ISingleWeaponFightingSkill : ISupportAbilitySkill
	{
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600C212 RID: 49682 RVA: 0x0046E70B File Offset: 0x0046C90B
		public override string AbilityCommand
		{
			get
			{
				return "CommandSingleWeaponFighting";
			}
		}

		// Token: 0x0600C213 RID: 49683 RVA: 0x0046E712 File Offset: 0x0046C912
		public override ActivatedAbilityEntry RequireAbility()
		{
			return this.ParentObject.RequirePart<SingleWeaponFighting_Ability>(false).AbilityEntry;
		}

		// Token: 0x04004D8F RID: 19855
		public const string ABL_CMD = "CommandSingleWeaponFighting";
	}
}
