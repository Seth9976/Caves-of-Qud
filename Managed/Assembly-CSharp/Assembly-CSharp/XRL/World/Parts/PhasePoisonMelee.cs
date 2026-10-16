using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EF0 RID: 3824
	[Serializable]
	public class PhasePoisonMelee : IPart
	{
		// Token: 0x060098CD RID: 39117 RVA: 0x0037790C File Offset: 0x00375B0C
		public override bool SameAs(IPart p)
		{
			PoisonMelee poisonMelee = p as PoisonMelee;
			return poisonMelee.Chance == this.Chance && poisonMelee.Strength == this.Strength && !(poisonMelee.DamageIncrement != this.DamageIncrement) && !(poisonMelee.Duration != this.Duration) && base.SameAs(p);
		}

		// Token: 0x060098CE RID: 39118 RVA: 0x00377971 File Offset: 0x00375B71
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060098CF RID: 39119 RVA: 0x00377974 File Offset: 0x00375B74
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponDealDamage");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x060098D0 RID: 39120 RVA: 0x00377994 File Offset: 0x00375B94
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage" && this.Chance.in100())
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null && !gameObjectParameter.MakeSave("Toughness", 10 + this.Strength, null, null, "Melee Injected Phase Damaging Poison", false, false, false, false, this.ParentObject))
				{
					gameObjectParameter.ApplyEffect(new PhasePoisoned(this.Duration.RollCached(), this.DamageIncrement, this.Strength, this.ParentObject.Equipped), null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003F9F RID: 16287
		public int Chance = 25;

		// Token: 0x04003FA0 RID: 16288
		public int Strength = 10;

		// Token: 0x04003FA1 RID: 16289
		public string DamageIncrement = "3d3";

		// Token: 0x04003FA2 RID: 16290
		public string Duration = "1d4+4";
	}
}
