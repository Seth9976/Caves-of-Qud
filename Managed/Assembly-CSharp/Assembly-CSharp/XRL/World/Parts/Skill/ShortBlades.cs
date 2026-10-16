using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001390 RID: 5008
	[Serializable]
	public class ShortBlades : BaseSkill
	{
		// Token: 0x0600C20E RID: 49678 RVA: 0x0046E6C6 File Offset: 0x0046C8C6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C20F RID: 49679 RVA: 0x0046E6C9 File Offset: 0x0046C8C9
		public override string GetWeaponCriticalDescription()
		{
			return "Short Blades (causes bleeding on critical hit)";
		}

		// Token: 0x0600C210 RID: 49680 RVA: 0x0046E6D0 File Offset: 0x0046C8D0
		public override void WeaponMadeCriticalHit(GameObject Attacker, GameObject Defender, GameObject Weapon, string Properties)
		{
			Defender.ApplyEffect(new Bleeding("1d2-1", 20 + Attacker.StatMod("Agility", 0), Attacker, false, false, false, false), null);
			base.WeaponMadeCriticalHit(Attacker, Defender, Weapon, Properties);
		}
	}
}
