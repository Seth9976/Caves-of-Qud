using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200131B RID: 4891
	[Serializable]
	public class Axe : BaseSkill
	{
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600BF40 RID: 48960 RVA: 0x00460E6E File Offset: 0x0045F06E
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0600BF41 RID: 48961 RVA: 0x00460E75 File Offset: 0x0045F075
		public override string GetWeaponCriticalDescription()
		{
			return "Axe (cleaves armor on critical hit)";
		}

		// Token: 0x0600BF42 RID: 48962 RVA: 0x00460E7C File Offset: 0x0045F07C
		public override void WeaponMadeCriticalHit(GameObject Attacker, GameObject Defender, GameObject Weapon, string Properties)
		{
			if (Attacker.HasSkill("Axe_Cleave"))
			{
				Axe_Cleave.PerformCleave(Attacker, Defender, Weapon, "Axe", Properties, 100, 1, null);
			}
			else
			{
				Axe_Cleave.PerformCleave(Attacker, Defender, Weapon, "Axe", Properties, 100, 0, new int?(1));
			}
			base.WeaponMadeCriticalHit(Attacker, Defender, Weapon, Properties);
		}
	}
}
