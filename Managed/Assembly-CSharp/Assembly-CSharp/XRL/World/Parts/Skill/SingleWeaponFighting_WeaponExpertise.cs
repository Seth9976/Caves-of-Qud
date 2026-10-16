using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001395 RID: 5013
	[Serializable]
	public class SingleWeaponFighting_WeaponExpertise : ISingleWeaponFightingSkill
	{
		// Token: 0x0600C220 RID: 49696 RVA: 0x0046EABD File Offset: 0x0046CCBD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetWeaponMeleeAttacksEvent.ID;
		}

		// Token: 0x0600C221 RID: 49697 RVA: 0x0046EAD4 File Offset: 0x0046CCD4
		public override bool HandleEvent(GetWeaponMeleeAttacksEvent E)
		{
			if (this.AbilityEntry.ToggleState && E.Intrinsic)
			{
				int num = 50;
				int num2 = 0;
				int num3 = 0;
				bool? flag = new bool?(true);
				E.AddAttack(num, num2, num3, typeof(SingleWeaponFighting), "SingleWeaponFighting", null, null, null, null, flag, false, true, true);
			}
			return base.HandleEvent(E);
		}
	}
}
