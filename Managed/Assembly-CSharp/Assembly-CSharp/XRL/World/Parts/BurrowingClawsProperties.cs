using System;
using XRL.World.Anatomy;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200119B RID: 4507
	public class BurrowingClawsProperties : IPart
	{
		// Token: 0x0600B349 RID: 45897 RVA: 0x00417265 File Offset: 0x00415465
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetWeaponHitDiceEvent.ID || ID == PooledEvent<PreferDefaultBehaviorEvent>.ID;
		}

		// Token: 0x0600B34A RID: 45898 RVA: 0x00417283 File Offset: 0x00415483
		public override bool HandleEvent(GetWeaponHitDiceEvent E)
		{
			if (E.Defender.IsDiggable())
			{
				E.PenetrationBonus += this.WallBonusPenetration;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B34B RID: 45899 RVA: 0x004172AC File Offset: 0x004154AC
		public override bool HandleEvent(PreferDefaultBehaviorEvent E)
		{
			BodyPart part = E.Part;
			if (((part != null) ? part.DefaultBehavior : null) == this.ParentObject)
			{
				GameObject target = E.Target;
				if (target != null && target.IsDiggable() && (this.Mutation != null || E.Actor.TryGetPart<BurrowingClaws>(out this.Mutation)) && this.Mutation.IsMyActivatedAbilityToggledOn(this.Mutation.EnableActivatedAbilityID, null))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B34C RID: 45900 RVA: 0x00417324 File Offset: 0x00415524
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponDealDamage");
		}

		// Token: 0x0600B34D RID: 45901 RVA: 0x00417334 File Offset: 0x00415534
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null && gameObjectParameter.IsDiggable())
				{
					Damage parameter = E.GetParameter<Damage>("Damage");
					int num = (int)Math.Floor((double)gameObjectParameter.baseHitpoints * this.WallBonusPercentage / 100.0);
					if (num >= parameter.Amount)
					{
						parameter.Amount = num;
						CombatJuice.playPrefabAnimation(gameObjectParameter, "Impacts/ImpactVFXDig", null, null, null, false);
						gameObjectParameter.PlayWorldSound("sfx_ability_mutation_burrowingClaws_burrow", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400484F RID: 18511
		public int WallBonusPenetration;

		// Token: 0x04004850 RID: 18512
		public double WallBonusPercentage;

		// Token: 0x04004851 RID: 18513
		[NonSerialized]
		private BurrowingClaws Mutation;
	}
}
