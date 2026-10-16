using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001388 RID: 5000
	[Serializable]
	public class ShortBlades_Bloodletter : BaseSkill
	{
		// Token: 0x0600C1DA RID: 49626 RVA: 0x0046D6DC File Offset: 0x0046B8DC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerAfterDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C1DB RID: 49627 RVA: 0x0046D6F4 File Offset: 0x0046B8F4
		public static void Bloodlet(GameObject Defender, GameObject Attacker)
		{
			if (Defender.GetEffectCount(typeof(Bleeding)) < 1 + Attacker.StatMod("Agility", 0))
			{
				Defender.ApplyEffect(new Bleeding("1d2-1", 20 + Attacker.StatMod("Agility", 0), Attacker, false, false, false, false), null);
			}
		}

		// Token: 0x0600C1DC RID: 49628 RVA: 0x0046D748 File Offset: 0x0046B948
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerAfterDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Weapon");
				if (gameObjectParameter != null && gameObjectParameter.HasPart<MeleeWeapon>() && gameObjectParameter.GetPart<MeleeWeapon>().Skill == "ShortBlades")
				{
					int num = 75;
					if (E.GetStringParameter("Properties", "").Contains("Juking"))
					{
						num = 100;
					}
					if (Stat.Random(1, 100) <= num)
					{
						ShortBlades_Bloodletter.Bloodlet(E.GetGameObjectParameter("Defender"), this.ParentObject);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C1DD RID: 49629 RVA: 0x0046D7DF File Offset: 0x0046B9DF
		public override bool AddSkill(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600C1DE RID: 49630 RVA: 0x0046D7E2 File Offset: 0x0046B9E2
		public override bool RemoveSkill(GameObject GO)
		{
			return true;
		}
	}
}
