using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010ED RID: 4333
	[Serializable]
	public class PoisonOnHit : IPart
	{
		// Token: 0x0600ABBB RID: 43963 RVA: 0x003EEABC File Offset: 0x003ECCBC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ABBC RID: 43964 RVA: 0x003EEABF File Offset: 0x003ECCBF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (Object.IsCreature)
			{
				Registrar.Register("AttackerHit");
				return;
			}
			if (Object.IsProjectile)
			{
				Registrar.Register("ProjectileHit");
				return;
			}
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
		}

		// Token: 0x0600ABBD RID: 43965 RVA: 0x003EEB00 File Offset: 0x003ECD00
		public override bool FireEvent(Event E)
		{
			if (E.ID.EndsWith("Hit"))
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
				if (GameObject.Validate(ref gameObjectParameter2))
				{
					GameObject gameObject = gameObjectParameter;
					GameObject parentObject = this.ParentObject;
					string text = "Part PoisonOnHit Activation";
					GameObject gameObject2 = gameObjectParameter2;
					GameObject gameObject3 = gameObjectParameter3;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, gameObject3, true, false).in100())
					{
						int num = this.Strength.RollCached();
						if (!gameObjectParameter2.MakeSave("Toughness", num, null, null, "Injected Damaging Poison", false, false, false, false, this.ParentObject))
						{
							gameObjectParameter2.ApplyEffect(new Poisoned(this.Duration.RollCached(), this.DamageIncrement, num, this.ParentObject.Equipped, false, false), null);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046F3 RID: 18163
		public int Chance = 100;

		// Token: 0x040046F4 RID: 18164
		public string Strength = "15";

		// Token: 0x040046F5 RID: 18165
		public string DamageIncrement = "3d3";

		// Token: 0x040046F6 RID: 18166
		public string Duration = "6-9";
	}
}
