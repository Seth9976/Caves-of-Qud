using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the damage roll used when the device is charged
	/// is increased by the standard power load bonus, i.e. 2 for the standard
	/// overload power load of 400.
	/// </remarks>
	// Token: 0x0200103C RID: 4156
	[Serializable]
	public class Gaslight : IPoweredPart
	{
		// Token: 0x0600A578 RID: 42360 RVA: 0x003C8544 File Offset: 0x003C6744
		public Gaslight()
		{
			this.ChargeUse = 10;
			this.WorksOnSelf = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A579 RID: 42361 RVA: 0x003C85B8 File Offset: 0x003C67B8
		public override bool SameAs(IPart p)
		{
			Gaslight gaslight = p as Gaslight;
			return !(gaslight.ChargedName != this.ChargedName) && !(gaslight.UnchargedName != this.UnchargedName) && gaslight.ChargedPenetrationBonus == this.ChargedPenetrationBonus && gaslight.UnchargedPenetrationBonus == this.UnchargedPenetrationBonus && !(gaslight.ChargedDamage != this.ChargedDamage) && !(gaslight.UnchargedDamage != this.UnchargedDamage) && !(gaslight.ChargedSkill != this.ChargedSkill) && !(gaslight.UnchargedSkill != this.UnchargedSkill) && base.SameAs(p);
		}

		// Token: 0x0600A57A RID: 42362 RVA: 0x003C8671 File Offset: 0x003C6871
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A57B RID: 42363 RVA: 0x003C8674 File Offset: 0x003C6874
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.SyncActiveState();
		}

		// Token: 0x0600A57C RID: 42364 RVA: 0x003C867C File Offset: 0x003C687C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CellChangedEvent.ID || ID == PooledEvent<DamageConstantAdjustedEvent>.ID || ID == PooledEvent<DamageDieSizeAdjustedEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetWeaponMeleePenetrationEvent.ID || ID == PooledEvent<ModificationAppliedEvent>.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A57D RID: 42365 RVA: 0x003C86E0 File Offset: 0x003C68E0
		public override bool HandleEvent(DamageConstantAdjustedEvent E)
		{
			if (E.Part is MeleeWeapon)
			{
				this.ChargedDamage = DieRoll.AdjustResult(this.ChargedDamage, E.Amount);
				if (this.Active)
				{
					MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
					if (part != null)
					{
						part.BaseDamage = this.ChargedDamage;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A57E RID: 42366 RVA: 0x003C873C File Offset: 0x003C693C
		public override bool HandleEvent(DamageDieSizeAdjustedEvent E)
		{
			if (E.Part is MeleeWeapon)
			{
				this.ChargedDamage = DieRoll.AdjustDieSize(this.ChargedDamage, E.Amount);
				if (this.Active)
				{
					MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
					if (part != null)
					{
						part.BaseDamage = this.ChargedDamage;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A57F RID: 42367 RVA: 0x003C8797 File Offset: 0x003C6997
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.SyncActiveState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A580 RID: 42368 RVA: 0x003C87A6 File Offset: 0x003C69A6
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.SyncActiveState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A581 RID: 42369 RVA: 0x003C87B5 File Offset: 0x003C69B5
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.SyncActiveState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A582 RID: 42370 RVA: 0x003C87C4 File Offset: 0x003C69C4
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.AsIfKnown && E.DB.PrimaryBase == this.UnchargedName)
			{
				E.ReplacePrimaryBase(this.ChargedName, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A583 RID: 42371 RVA: 0x003C87FC File Offset: 0x003C69FC
		public override bool HandleEvent(GetWeaponMeleePenetrationEvent E)
		{
			this.ParentObject.UseCharge(this.ChargeUse, false, 0L, true, true, null);
			this.SyncActiveState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A584 RID: 42372 RVA: 0x003C8836 File Offset: 0x003C6A36
		public override bool HandleEvent(ModificationAppliedEvent E)
		{
			if (this.Active)
			{
				this.Deactivate();
				this.Activate();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A585 RID: 42373 RVA: 0x003C8854 File Offset: 0x003C6A54
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			Render render = this.ParentObject.Render;
			if (((render != null) ? render.DisplayName : null) == this.ChargedName)
			{
				this.ParentObject.Render.DisplayName = this.UnchargedName;
			}
			MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
			if (part != null)
			{
				part.PenBonus = this.UnchargedPenetrationBonus;
				part.BaseDamage = this.UnchargedDamage;
				part.Skill = this.UnchargedSkill;
			}
			this.Active = false;
			this.SyncActiveState();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A586 RID: 42374 RVA: 0x003C88E4 File Offset: 0x003C6AE4
		public void Activate()
		{
			if (this.Active)
			{
				return;
			}
			Render render = this.ParentObject.Render;
			if (((render != null) ? render.DisplayName : null) == this.UnchargedName)
			{
				this.ParentObject.Render.DisplayName = this.ChargedName;
			}
			MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
			if (part != null)
			{
				part.PenBonus += this.ChargedPenetrationBonus - this.UnchargedPenetrationBonus;
				part.BaseDamage = this.ChargedDamage;
				int num = this.MyPowerLoadBonus(int.MinValue, 100, 150);
				if (num != 0)
				{
					part.BaseDamage = DieRoll.AdjustResult(part.BaseDamage, num);
				}
				part.Skill = this.ChargedSkill;
			}
			this.Active = true;
		}

		// Token: 0x0600A587 RID: 42375 RVA: 0x003C89A4 File Offset: 0x003C6BA4
		public void Deactivate()
		{
			if (!this.Active)
			{
				return;
			}
			Render render = this.ParentObject.Render;
			if (((render != null) ? render.DisplayName : null) == this.ChargedName)
			{
				this.ParentObject.Render.DisplayName = this.UnchargedName;
			}
			MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
			if (part != null)
			{
				part.PenBonus -= this.ChargedPenetrationBonus - this.UnchargedPenetrationBonus;
				part.BaseDamage = this.UnchargedDamage;
				part.Skill = this.UnchargedSkill;
			}
			this.Active = false;
		}

		// Token: 0x0600A588 RID: 42376 RVA: 0x003C8A3C File Offset: 0x003C6C3C
		public void SyncActiveState()
		{
			if (this.Active)
			{
				if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					this.Deactivate();
					return;
				}
			}
			else if (!base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Activate();
			}
		}

		// Token: 0x0400445E RID: 17502
		public string ChargedName = "1";

		// Token: 0x0400445F RID: 17503
		public string UnchargedName = "0";

		// Token: 0x04004460 RID: 17504
		public int ChargedPenetrationBonus = 4;

		// Token: 0x04004461 RID: 17505
		public int UnchargedPenetrationBonus;

		// Token: 0x04004462 RID: 17506
		public string ChargedDamage = "1d6";

		// Token: 0x04004463 RID: 17507
		public string UnchargedDamage = "1d2";

		// Token: 0x04004464 RID: 17508
		public string ChargedSkill = "ShortBlades";

		// Token: 0x04004465 RID: 17509
		public string UnchargedSkill = "Cudgel";

		// Token: 0x04004466 RID: 17510
		public bool Active;
	}
}
