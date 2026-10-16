using System;

namespace XRL.World.Parts
{
	// Token: 0x020010FA RID: 4346
	[Serializable]
	public class ThirstOnHit : IPart
	{
		// Token: 0x0600AC04 RID: 44036 RVA: 0x003F0D14 File Offset: 0x003EEF14
		public override bool SameAs(IPart p)
		{
			ThirstOnHit thirstOnHit = p as ThirstOnHit;
			return thirstOnHit.Chance == this.Chance && !(thirstOnHit.Amount != this.Amount) && thirstOnHit.RequiresOrganic == this.RequiresOrganic && base.SameAs(p);
		}

		// Token: 0x0600AC05 RID: 44037 RVA: 0x003F0D64 File Offset: 0x003EEF64
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AC06 RID: 44038 RVA: 0x003F0D67 File Offset: 0x003EEF67
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AC07 RID: 44039 RVA: 0x003F0D94 File Offset: 0x003EEF94
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "WeaponThrowHit" || E.ID == "ProjectileHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
				if (GameObject.Validate(ref gameObjectParameter2))
				{
					GameObject gameObject = gameObjectParameter;
					GameObject gameObject2 = gameObjectParameter3;
					string text = "Part ThirstOnHit Activation";
					GameObject gameObject3 = gameObjectParameter2;
					GameObject gameObject4 = gameObjectParameter4;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false).in100() && (!this.RequiresOrganic || gameObjectParameter2.IsOrganic) && !gameObjectParameter2.FireEvent(new Event("AddWater", "Amount", -this.Amount.RollCached(), "Forced", 1, "External", 1)))
					{
						return false;
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400472E RID: 18222
		public int Chance = 100;

		// Token: 0x0400472F RID: 18223
		public string Amount = "60000";

		// Token: 0x04004730 RID: 18224
		public bool RequiresOrganic = true;
	}
}
