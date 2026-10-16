using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001331 RID: 4913
	[Serializable]
	public class Cudgel : BaseSkill
	{
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x0600C003 RID: 49155 RVA: 0x004654A9 File Offset: 0x004636A9
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0600C004 RID: 49156 RVA: 0x004654B0 File Offset: 0x004636B0
		public override string GetWeaponCriticalDescription()
		{
			return "Cudgel (dazes on critical hit)";
		}

		// Token: 0x0600C005 RID: 49157 RVA: 0x004654B7 File Offset: 0x004636B7
		public override void WeaponMadeCriticalHit(GameObject Attacker, GameObject Defender, GameObject Weapon, string Properties)
		{
			Defender.ApplyEffect(new Dazed(Stat.Random(1, 4), true), null);
			base.WeaponMadeCriticalHit(Attacker, Defender, Weapon, Properties);
		}
	}
}
