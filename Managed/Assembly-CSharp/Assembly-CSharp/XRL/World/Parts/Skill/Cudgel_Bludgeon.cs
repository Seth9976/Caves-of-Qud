using System;
using XRL.Language;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200132E RID: 4910
	[Serializable]
	public class Cudgel_Bludgeon : BaseSkill
	{
		// Token: 0x0600BFEE RID: 49134 RVA: 0x00464DCB File Offset: 0x00462FCB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerAfterAttack");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BFEF RID: 49135 RVA: 0x00464DE0 File Offset: 0x00462FE0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerAfterAttack")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
				if (GameObject.Validate(ref gameObjectParameter2) && GameObject.Validate(ref gameObjectParameter3))
				{
					MeleeWeapon part = gameObjectParameter3.GetPart<MeleeWeapon>();
					if (part != null && part.Skill == "Cudgel")
					{
						string stringParameter = E.GetStringParameter("Properties", null);
						bool flag = stringParameter != null && stringParameter.HasDelimitedSubstring(',', "Conking", StringComparison.Ordinal);
						bool flag2 = this.ParentObject.HasSkill("Cudgel_Conk");
						if (flag && flag2 && gameObjectParameter2.HasEffect<Stun>())
						{
							gameObjectParameter2.ApplyEffect(new Asleep(Stat.Random(30, 40), false, false, false, true), null);
						}
						else
						{
							int num = 50;
							if (flag && flag2)
							{
								num = 100;
							}
							else if (stringParameter != null && stringParameter.HasDelimitedSubstring(',', "Charging", StringComparison.Ordinal) && this.ParentObject.HasSkill("Cudgel_ChargingStrike"))
							{
								num = 100;
							}
							else if (this.ParentObject.HasEffect<Cudgel_SmashingUp>())
							{
								num = 100;
							}
							else if (this.ParentObject.HasIntProperty("ImprovedBludgeon"))
							{
								num += num * this.ParentObject.GetIntProperty("ImprovedBludgeon", 0);
							}
							num = GetSpecialEffectChanceEvent.GetFor(gameObjectParameter, this.ParentObject, "Skill Bludgeon", num, null, null, true, false);
							if (num.in100() && gameObjectParameter2.ApplyEffect(new Dazed(Stat.Random(3, 4), true), null) && gameObjectParameter2.IsCombat)
							{
								IComponent<GameObject>.XDidY(gameObjectParameter2, "reel", "from the force of " + (this.ParentObject.IsPlayer() ? "your" : Grammar.MakePossessive(this.ParentObject.the + this.ParentObject.ShortDisplayName)) + " bludgeoning", null, null, null, null, gameObjectParameter2, false, false, null, null, false, false, false, false, false, null);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BFF0 RID: 49136 RVA: 0x00464FE6 File Offset: 0x004631E6
		public override bool AddSkill(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600BFF1 RID: 49137 RVA: 0x00464FE9 File Offset: 0x004631E9
		public override bool RemoveSkill(GameObject GO)
		{
			return true;
		}
	}
}
