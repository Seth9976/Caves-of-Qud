using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200136B RID: 4971
	[Serializable]
	public class Pistol : BaseSkill
	{
		// Token: 0x0600C15E RID: 49502 RVA: 0x0046B980 File Offset: 0x00469B80
		public override int GetWeaponCriticalModifier(GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			int num = 2;
			if (Attacker != null && Attacker.HasSkill("Pistol_DeadShot"))
			{
				num += 2;
			}
			return num;
		}
	}
}
