using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001343 RID: 4931
	[Serializable]
	public class HeavyWeapons : BaseSkill
	{
		// Token: 0x0600C06B RID: 49259 RVA: 0x00467B9F File Offset: 0x00465D9F
		public override int GetWeaponCriticalModifier(GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return 2;
		}
	}
}
