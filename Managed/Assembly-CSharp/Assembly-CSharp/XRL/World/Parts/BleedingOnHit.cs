using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010CE RID: 4302
	[Serializable]
	public class BleedingOnHit : IActivePart
	{
		// Token: 0x0600AB19 RID: 43801 RVA: 0x003EA074 File Offset: 0x003E8274
		public BleedingOnHit()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AB1A RID: 43802 RVA: 0x003EA098 File Offset: 0x003E8298
		public override bool SameAs(IPart p)
		{
			BleedingOnHit bleedingOnHit = p as BleedingOnHit;
			return !(bleedingOnHit.Amount != this.Amount) && bleedingOnHit.SaveTarget == this.SaveTarget && !(bleedingOnHit.RequireDamageAttribute != this.RequireDamageAttribute) && bleedingOnHit.Holographic == this.Holographic && bleedingOnHit.Stack == this.Stack && base.SameAs(p);
		}

		// Token: 0x0600AB1B RID: 43803 RVA: 0x003EA10D File Offset: 0x003E830D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == ImplantedEvent.ID || ID == UnequippedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x0600AB1C RID: 43804 RVA: 0x003EA13B File Offset: 0x003E833B
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "WieldedWeaponHit");
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB1D RID: 43805 RVA: 0x003EA155 File Offset: 0x003E8355
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "WieldedWeaponHit");
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB1E RID: 43806 RVA: 0x003EA16F File Offset: 0x003E836F
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterPartEvent(this, "WieldedWeaponHit");
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB1F RID: 43807 RVA: 0x003EA189 File Offset: 0x003E8389
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, "WieldedWeaponHit");
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB20 RID: 43808 RVA: 0x003EA1A3 File Offset: 0x003E83A3
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600AB21 RID: 43809 RVA: 0x003EA1A6 File Offset: 0x003E83A6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB22 RID: 43810 RVA: 0x003EA1BC File Offset: 0x003E83BC
		public bool CheckApply(Event E)
		{
			if (!this.RequireDamageAttribute.IsNullOrEmpty())
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage == null || !damage.HasAttribute(this.RequireDamageAttribute))
				{
					return false;
				}
			}
			if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
			return E.GetGameObjectParameter("Defender").ApplyEffect(this.Holographic ? new HolographicBleeding(this.Amount, this.SaveTarget, gameObjectParameter, this.Stack, false, false, false) : new Bleeding(this.Amount, this.SaveTarget, gameObjectParameter, this.Stack, false, false, false), null);
		}

		// Token: 0x0600AB23 RID: 43811 RVA: 0x003EA284 File Offset: 0x003E8484
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WieldedWeaponHit")
			{
				if (this.IsObjectActivePartSubject(E.GetGameObjectParameter("Attacker")))
				{
					this.CheckApply(E);
				}
			}
			else if (E.ID == "WeaponHit" && this.IsObjectActivePartSubject(E.GetGameObjectParameter("Weapon")))
			{
				this.CheckApply(E);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004688 RID: 18056
		public string Amount = "1d2";

		// Token: 0x04004689 RID: 18057
		public int SaveTarget = 20;

		// Token: 0x0400468A RID: 18058
		public string RequireDamageAttribute;

		// Token: 0x0400468B RID: 18059
		public bool Holographic;

		// Token: 0x0400468C RID: 18060
		public bool Stack;
	}
}
