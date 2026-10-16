using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EEF RID: 3823
	[Serializable]
	public class PoisonMelee : IPart
	{
		// Token: 0x060098C8 RID: 39112 RVA: 0x003777B4 File Offset: 0x003759B4
		public override bool SameAs(IPart p)
		{
			PoisonMelee poisonMelee = p as PoisonMelee;
			return poisonMelee.Chance == this.Chance && poisonMelee.Strength == this.Strength && !(poisonMelee.DamageIncrement != this.DamageIncrement) && !(poisonMelee.Duration != this.Duration) && base.SameAs(p);
		}

		// Token: 0x060098C9 RID: 39113 RVA: 0x00377819 File Offset: 0x00375A19
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060098CA RID: 39114 RVA: 0x0037781C File Offset: 0x00375A1C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponDealDamage");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x060098CB RID: 39115 RVA: 0x0037783C File Offset: 0x00375A3C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage" && this.Chance.in100())
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null && !gameObjectParameter.MakeSave("Toughness", 10 + this.Strength, null, null, "Melee Injected Damaging Poison", false, false, false, false, this.ParentObject))
				{
					gameObjectParameter.ApplyEffect(new Poisoned(this.Duration.RollCached(), this.DamageIncrement, this.Strength, this.ParentObject.Physics.Equipped, false, false), null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003F9B RID: 16283
		public int Chance = 25;

		// Token: 0x04003F9C RID: 16284
		public int Strength = 10;

		// Token: 0x04003F9D RID: 16285
		public string DamageIncrement = "3d3";

		// Token: 0x04003F9E RID: 16286
		public string Duration = "3d3";
	}
}
