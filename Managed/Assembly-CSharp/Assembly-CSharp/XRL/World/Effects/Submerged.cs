using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D98 RID: 3480
	[Serializable]
	public class Submerged : Effect
	{
		// Token: 0x06008AB9 RID: 35513 RVA: 0x00317186 File Offset: 0x00315386
		public Submerged()
		{
			this.Duration = 1;
			this.DisplayName = "{{B|submerged}}";
		}

		// Token: 0x06008ABA RID: 35514 RVA: 0x003171B3 File Offset: 0x003153B3
		public Submerged(int MovePenalty = 0, int HiddenDifficulty = 20)
			: this()
		{
			this.MovePenalty = MovePenalty;
			this.HiddenDifficulty = HiddenDifficulty;
		}

		// Token: 0x06008ABB RID: 35515 RVA: 0x003171C9 File Offset: 0x003153C9
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x06008ABC RID: 35516 RVA: 0x003171D0 File Offset: 0x003153D0
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008ABD RID: 35517 RVA: 0x003171D3 File Offset: 0x003153D3
		public override string GetDetails()
		{
			return "Staying underwater.";
		}

		// Token: 0x06008ABE RID: 35518 RVA: 0x003171DC File Offset: 0x003153DC
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Submerged>())
			{
				return false;
			}
			Cell currentCell = Object.CurrentCell;
			GameObject gameObject = ((currentCell != null) ? currentCell.GetAquaticSupportFor(Object) : null);
			if (gameObject == null)
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/Abilities/sfx_ability_water_submerge", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			base.DidX("submerge", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
			this.EndAbilityID = base.AddMyActivatedAbility("Surface", "CommandEndSubmerged", "Maneuvers", null, "\u0018", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			LiquidVolume liquidVolume = gameObject.LiquidVolume;
			Object.LiquidSplash((liquidVolume != null) ? liquidVolume.GetPrimaryLiquid() : null);
			this.ApplyChanges();
			return true;
		}

		// Token: 0x06008ABF RID: 35519 RVA: 0x003172A8 File Offset: 0x003154A8
		public override void Remove(GameObject Object)
		{
			try
			{
				Hidden part = Object.GetPart<Hidden>();
				Cell currentCell = Object.CurrentCell;
				GameObject gameObject = ((currentCell != null) ? currentCell.GetAquaticSupportFor(Object) : null);
				if (gameObject != null)
				{
					if (part != null)
					{
						part.Reveal(true);
					}
					if (Object != null)
					{
						Object.PlayWorldSound("Sounds/Abilities/sfx_ability_water_emerge", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					base.DidXToY("emerge", "from", gameObject, null, null, null, null, null, null, false, true, false, false, false, null, null, null, false, true, false, false, false, null);
					LiquidVolume liquidVolume = gameObject.LiquidVolume;
					Object.LiquidSplash((liquidVolume != null) ? liquidVolume.GetPrimaryLiquid() : null);
				}
				else if (part != null)
				{
					part.Silent = false;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Submerged::Remove", ex, "game_exception");
			}
			base.RemoveMyActivatedAbility(ref this.EndAbilityID, null);
			this.UnapplyChanges();
		}

		// Token: 0x06008AC0 RID: 35520 RVA: 0x0031738C File Offset: 0x0031558C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == SingletonEvent<EarlyBeforeBeginTakeActionEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == LeavingCellEvent.ID;
		}

		// Token: 0x06008AC1 RID: 35521 RVA: 0x003173BC File Offset: 0x003155BC
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (this.Duration > 0 && !E.Involuntary && E.Object == base.Object && E.To != "Swimming" && E.To != "Wading")
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You cannot do that while submerged.");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008AC2 RID: 35522 RVA: 0x0031742E File Offset: 0x0031562E
		public override bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			this.Validate();
			return base.HandleEvent(E);
		}

		// Token: 0x06008AC3 RID: 35523 RVA: 0x0031743E File Offset: 0x0031563E
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.Validate();
			return base.HandleEvent(E);
		}

		// Token: 0x06008AC4 RID: 35524 RVA: 0x0031744E File Offset: 0x0031564E
		public override bool HandleEvent(LeavingCellEvent E)
		{
			this.Surface();
			return base.HandleEvent(E);
		}

		// Token: 0x06008AC5 RID: 35525 RVA: 0x00317460 File Offset: 0x00315660
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("AIAttackMelee");
			Registrar.Register("AIAttackRange");
			Registrar.Register("AICanAttackMelee");
			Registrar.Register("AICanAttackRange");
			Registrar.Register("AILookForTarget");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeforeDirectMove");
			Registrar.Register("BeforeTeleport");
			Registrar.Register("BodyPositionChanged");
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("CanMoveExtremities");
			Registrar.Register("CommandEndSubmerged");
			Registrar.Register("FiringMissile");
			Registrar.Register("LateBeforeApplyDamage");
			Registrar.Register("MovementModeChanged");
			Registrar.Register("BeginAttack");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008AC6 RID: 35526 RVA: 0x00317530 File Offset: 0x00315730
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AILookForTarget" || E.ID == "AIAttackMelee" || E.ID == "AIAttackRange" || E.ID == "AICanAttackMelee" || E.ID == "AICanAttackRange")
			{
				if (this.Duration > 0)
				{
					return false;
				}
			}
			else if (E.ID == "BeginAttack" || E.ID == "CanFireMissileWeapon")
			{
				if (this.Duration > 0)
				{
					if (base.Object.IsPlayer())
					{
						Popup.ShowFail("You cannot do that while submerged.", true, true, true);
					}
					return false;
				}
			}
			else if (E.ID == "LateBeforeApplyDamage")
			{
				if (this.Duration > 0)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Owner");
					Damage damage = E.GetParameter("Damage") as Damage;
					if (gameObjectParameter != null && gameObjectParameter != base.Object && damage != null && !damage.HasAttribute("Bleeding"))
					{
						base.DidX("are", "forced to the surface", null, null, null, null, base.Object, false, false, null, null, false, false, false, false, false, null);
						base.Object.RemoveEffect(this, true);
						return false;
					}
				}
			}
			else if (E.ID == "BeforeDirectMove" || E.ID == "BeforeTeleport" || E.ID == "BeginAttack" || E.ID == "FiringMissile")
			{
				if (this.Duration > 0)
				{
					base.Object.RemoveEffect(this, true);
				}
			}
			else if (E.ID == "CanChangeBodyPosition" || E.ID == "CanMoveExtremities")
			{
				if (this.Duration > 0 && !E.HasFlag("Involuntary"))
				{
					string stringParameter = E.GetStringParameter("To", null);
					if (stringParameter != "Swimming" && stringParameter != "Wading")
					{
						if (E.HasFlag("ShowMessage") && base.Object.IsPlayer())
						{
							Popup.ShowFail("You cannot do that while submerged.", true, true, true);
						}
						return false;
					}
				}
			}
			else if (E.ID == "CommandEndSubmerged")
			{
				this.Surface();
			}
			else if (E.ID == "MovementModeChanged" || E.ID == "BodyPositionChanged")
			{
				string stringParameter2 = E.GetStringParameter("To", null);
				if (stringParameter2 != "Swimming" && stringParameter2 != "Wading")
				{
					this.Surface();
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008AC7 RID: 35527 RVA: 0x00317834 File Offset: 0x00315A34
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift(base.Object, "MoveSpeed", this.MovePenalty, false);
			Hidden hidden = base.Object.GetPart<Hidden>();
			if (hidden == null)
			{
				hidden = new Hidden(this.HiddenDifficulty, true);
				base.Object.AddPart<Hidden>(hidden, true, false);
				return;
			}
			hidden.Difficulty = this.HiddenDifficulty;
			hidden.Silent = true;
			hidden.Found = false;
		}

		// Token: 0x06008AC8 RID: 35528 RVA: 0x003178A8 File Offset: 0x00315AA8
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
			Hidden part = base.Object.GetPart<Hidden>();
			if (part != null)
			{
				part.Reveal();
			}
		}

		// Token: 0x06008AC9 RID: 35529 RVA: 0x003178DB File Offset: 0x00315ADB
		private bool Validate()
		{
			if (this.CanSubmerge())
			{
				return true;
			}
			base.Object.RemoveEffect(this, true);
			return false;
		}

		// Token: 0x06008ACA RID: 35530 RVA: 0x003178F8 File Offset: 0x00315AF8
		public void Surface()
		{
			base.Object.PlayWorldOrUISound("Sounds/Abilities/sfx_ability_skill_generic", null);
			base.Object.RemoveEffect(this, true);
		}

		// Token: 0x06008ACB RID: 35531 RVA: 0x0031792C File Offset: 0x00315B2C
		public bool CanSubmerge()
		{
			return Submerged.CanSubmerge(base.Object);
		}

		// Token: 0x06008ACC RID: 35532 RVA: 0x00317939 File Offset: 0x00315B39
		public static bool CanSubmerge(GameObject Object)
		{
			return Submerged.CanSubmergeIn(Object, (Object != null) ? Object.CurrentCell : null);
		}

		// Token: 0x06008ACD RID: 35533 RVA: 0x0031794D File Offset: 0x00315B4D
		public static bool CanSubmergeIn(GameObject Object, Cell C)
		{
			return C != null && GameObject.Validate(ref Object) && C.HasAquaticSupportFor(Object);
		}

		// Token: 0x04003ABA RID: 15034
		public Guid EndAbilityID = Guid.Empty;

		// Token: 0x04003ABB RID: 15035
		public int MovePenalty;

		// Token: 0x04003ABC RID: 15036
		public int HiddenDifficulty = 20;
	}
}
