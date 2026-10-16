using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// <remarks>
	/// This part is not used in the base game.
	///
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is not by default, chance to activate is increased by a
	/// percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400, and save targets are
	/// increased by the standard power load bonus, i.e. 2 for the standard
	/// overload power load of 400.
	/// </remarks>
	// Token: 0x020010D1 RID: 4305
	[Serializable]
	public class CardiacArrestOnHit : IActivePart
	{
		// Token: 0x0600AB2A RID: 43818 RVA: 0x003EA468 File Offset: 0x003E8668
		public CardiacArrestOnHit()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AB2B RID: 43819 RVA: 0x003EA4A0 File Offset: 0x003E86A0
		public override bool SameAs(IPart p)
		{
			CardiacArrestOnHit cardiacArrestOnHit = p as CardiacArrestOnHit;
			return cardiacArrestOnHit.Chance == this.Chance && cardiacArrestOnHit.SaveTarget == this.SaveTarget && !(cardiacArrestOnHit.SaveAttribute != this.SaveAttribute) && !(cardiacArrestOnHit.SaveVs != this.SaveVs) && !(cardiacArrestOnHit.RequireDamageAttribute != this.RequireDamageAttribute) && base.SameAs(p);
		}

		// Token: 0x0600AB2C RID: 43820 RVA: 0x003EA51C File Offset: 0x003E871C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("LauncherProjectileHit");
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponDealDamage");
			Registrar.Register("WeaponPseudoThrowHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB2D RID: 43821 RVA: 0x003EA568 File Offset: 0x003E8768
		public int GetActivationChance(GameObject Attacker = null, GameObject Defender = null, GameObject Projectile = null, int? PowerLoadLevel = null)
		{
			int num = this.Chance;
			int num2 = IComponent<GameObject>.PowerLoadBonus(PowerLoadLevel ?? this.MyPowerLoadLevel(), 100, 10);
			if (num2 != 0)
			{
				num = num * (100 + num2) / 100;
			}
			return GetSpecialEffectChanceEvent.GetFor(Attacker, this.ParentObject, "Part CardiacArrestOnHit Activation", num, Defender, Projectile, true, false);
		}

		// Token: 0x0600AB2E RID: 43822 RVA: 0x003EA5CC File Offset: 0x003E87CC
		public int GetSaveTarget(int? PowerLoadLevel = null)
		{
			return this.SaveTarget + IComponent<GameObject>.PowerLoadBonus(PowerLoadLevel ?? this.MyPowerLoadLevel(), 100, 150);
		}

		// Token: 0x0600AB2F RID: 43823 RVA: 0x003EA608 File Offset: 0x003E8808
		public void CheckApply(Event E)
		{
			if (!this.RequireDamageAttribute.IsNullOrEmpty())
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage == null || !damage.HasAttribute(this.RequireDamageAttribute))
				{
					return;
				}
			}
			int num = this.MyPowerLoadLevel();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			if (base.IsDisabled(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
			{
				return;
			}
			GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
			GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
			GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
			if (!this.GetActivationChance(gameObjectParameter, gameObjectParameter2, gameObjectParameter3, new int?(num)).in100())
			{
				return;
			}
			num3 = new int?(num);
			base.ConsumeCharge(null, num3);
			if (CanApplyEffectEvent.Check<CardiacArrest>(gameObjectParameter2, 0) && (this.SaveAttribute.IsNullOrEmpty() || !gameObjectParameter2.MakeSave(this.SaveAttribute, this.GetSaveTarget(new int?(num)), null, null, this.SaveVs, false, false, false, false, this.ParentObject)))
			{
				gameObjectParameter2.ApplyEffect(new CardiacArrest(), null);
			}
		}

		// Token: 0x0600AB30 RID: 43824 RVA: 0x003EA720 File Offset: 0x003E8920
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage" || E.ID == "ProjectileHit" || E.ID == "LauncherProjectileHit" || E.ID == "WeaponThrowHit" || E.ID == "WeaponPseudoThrowHit")
			{
				this.CheckApply(E);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004690 RID: 18064
		public int Chance = 100;

		// Token: 0x04004691 RID: 18065
		public int SaveTarget = 20;

		// Token: 0x04004692 RID: 18066
		public string SaveAttribute = "Toughness";

		// Token: 0x04004693 RID: 18067
		public string SaveVs = "CardiacArrest CardiacArrestInduction";

		// Token: 0x04004694 RID: 18068
		public string RequireDamageAttribute;
	}
}
