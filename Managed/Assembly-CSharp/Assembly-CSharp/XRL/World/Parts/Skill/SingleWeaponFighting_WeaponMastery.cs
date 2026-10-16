using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001396 RID: 5014
	[Serializable]
	public class SingleWeaponFighting_WeaponMastery : ISingleWeaponFightingSkill
	{
		// Token: 0x0600C223 RID: 49699 RVA: 0x0046EB2F File Offset: 0x0046CD2F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetWeaponMeleeAttacksEvent.ID;
		}

		// Token: 0x0600C224 RID: 49700 RVA: 0x0046EB48 File Offset: 0x0046CD48
		public override bool HandleEvent(GetWeaponMeleeAttacksEvent E)
		{
			if (this.AbilityEntry.ToggleState && E.Intrinsic)
			{
				int num = 100;
				int num2 = 0;
				int num3 = 0;
				bool? flag = new bool?(true);
				E.AddAttack(num, num2, num3, typeof(SingleWeaponFighting), "SingleWeaponFighting", null, null, null, null, flag, false, true, true);
			}
			return base.HandleEvent(E);
		}
	}
}
