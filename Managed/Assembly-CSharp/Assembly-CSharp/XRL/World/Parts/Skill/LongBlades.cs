using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001348 RID: 4936
	[Serializable]
	public class LongBlades : BaseSkill
	{
		// Token: 0x0600C08B RID: 49291 RVA: 0x0046812B File Offset: 0x0046632B
		public override string GetWeaponCriticalDescription()
		{
			return "Long Blades (increased penetration on critical hit)";
		}

		// Token: 0x0600C08C RID: 49292 RVA: 0x00468132 File Offset: 0x00466332
		public override int GetWeaponCriticalModifier(GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return 2;
		}
	}
}
