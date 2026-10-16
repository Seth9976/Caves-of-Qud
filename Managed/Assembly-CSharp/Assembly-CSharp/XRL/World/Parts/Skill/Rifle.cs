using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001374 RID: 4980
	[Serializable]
	public class Rifle : BaseSkill
	{
		// Token: 0x0600C186 RID: 49542 RVA: 0x0046C334 File Offset: 0x0046A534
		public override int GetWeaponCriticalModifier(GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return 2;
		}
	}
}
