using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010E7 RID: 4327
	[Serializable]
	public class MonochromePoisonOnDamage : IPart
	{
		// Token: 0x0600ABA9 RID: 43945 RVA: 0x003EE3B2 File Offset: 0x003EC5B2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("AttackerHit");
			Registrar.Register("WeaponAfterDamage");
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABAA RID: 43946 RVA: 0x003EE3E8 File Offset: 0x003EC5E8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerHit" || E.ID == "WeaponHit")
			{
				if (E.GetIntParameter("Penetrations", 0) > 0)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
					GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
					GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
					if (GameObject.Validate(ref gameObjectParameter2) && GameObject.Validate(ref gameObjectParameter3) && gameObjectParameter3.HasTag("NaturalGear"))
					{
						GameObject gameObject = gameObjectParameter;
						GameObject gameObject2 = gameObjectParameter3;
						string text = "Part MonochromePoisonOnHit Activation";
						GameObject gameObject3 = gameObjectParameter2;
						GameObject gameObject4 = gameObjectParameter4;
						if (GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false).in100())
						{
							E.SetParameter("DidSpecialEffect", 1);
							string text2 = E.GetStringParameter("Properties", "") ?? "";
							if (!text2.Contains("Monochromed"))
							{
								E.SetParameter("Properties", (text2 == "") ? "Monochromed" : (text2 + ",Monochromed"));
							}
						}
					}
				}
			}
			else if (E.ID == "AttackerAfterDamage" || E.ID == "WeaponAfterDamage")
			{
				GameObject parentObject = this.ParentObject;
				if (this.ParentObject.Equipped != null)
				{
					GameObject equipped = this.ParentObject.Equipped;
				}
				GameObject gameObjectParameter5 = E.GetGameObjectParameter("Defender");
				if ((E.GetStringParameter("Properties", "") ?? "").Contains("Monochromed") && gameObjectParameter5.ApplyEffect(new MonochromeOnset(), null))
				{
					if (gameObjectParameter5.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("Your vision blurs.", null, true);
					}
					E.SetParameter("DidSpecialEffect", 1);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046E9 RID: 18153
		public int Chance = 100;

		// Token: 0x040046EA RID: 18154
		public string Duration = "50-100";
	}
}
