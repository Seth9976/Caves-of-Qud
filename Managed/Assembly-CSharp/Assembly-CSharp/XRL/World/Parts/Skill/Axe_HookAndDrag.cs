using System;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001321 RID: 4897
	[Serializable]
	public class Axe_HookAndDrag : BaseSkill
	{
		// Token: 0x0600BF7C RID: 49020 RVA: 0x00462474 File Offset: 0x00460674
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("DurationSpecial", "9 rounds or until opponent is dismembered", false, 0);
			stats.Set("ResistSave", "Strength " + Axe_HookAndDrag.RESISTSAVE.ToString(), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Axe_HookAndDrag.COOLDOWN, 0);
		}

		// Token: 0x0600BF7D RID: 49021 RVA: 0x004624D0 File Offset: 0x004606D0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == BeforeDeathRemovalEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == EnteredCellEvent.ID || ID == LeftCellEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600BF7E RID: 49022 RVA: 0x0046250E File Offset: 0x0046070E
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF7F RID: 49023 RVA: 0x00462534 File Offset: 0x00460734
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance == 1 && !E.Actor.IsFrozen() && this.ParentObject.PhaseAndFlightMatches(E.Actor))
			{
				if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
				{
					E.Add("CommandAxeHookAndDrag", 1, null, false, false, null, null);
				}
				if (GameObject.Validate(ref this.HookedObject) && !this.HookedObject.IsNowhere())
				{
					Hooked hooked = this.HookedObject.GetEffect(typeof(Hooked), new Predicate<Effect>(this.WeaponMatch)) as Hooked;
					if (hooked != null && hooked.Duration > 0)
					{
						E.Add("MetaCommandMoveAway", 1, null, false, false, null, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF80 RID: 49024 RVA: 0x004625F5 File Offset: 0x004607F5
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			this.Validate(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF81 RID: 49025 RVA: 0x00462608 File Offset: 0x00460808
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (GameObject.Validate(ref this.HookedObject))
			{
				Hooked hooked = this.HookedObject.GetEffect(typeof(Hooked), new Predicate<Effect>(this.WeaponMatch)) as Hooked;
				if (hooked != null)
				{
					this.HookedObject.RemoveEffect(hooked, true);
				}
			}
			this.HookingWeapon = null;
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF82 RID: 49026 RVA: 0x00462668 File Offset: 0x00460868
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.HookedObject != null && this.HookedObject.IsInvalid())
			{
				this.HookedObject = null;
				this.HookingWeapon = null;
			}
			if (this.HookedObject != null && this.LeftCell.IsCell())
			{
				Hooked hooked = this.HookedObject.GetEffect(typeof(Hooked), new Predicate<Effect>(this.WeaponMatch)) as Hooked;
				if (hooked != null)
				{
					if (!this.HookedObject.PhaseAndFlightMatches(this.ParentObject))
					{
						this.HookedObject.RemoveEffect(hooked, true);
						this.HookedObject = null;
						this.HookingWeapon = null;
					}
					else if (hooked.Duration > 0)
					{
						int num = this.ParentObject.DistanceTo(this.HookedObject);
						if (num == 2)
						{
							string directionFromCell = this.HookedObject.CurrentCell.GetDirectionFromCell(this.LeftCell.ResolveCell(), false);
							GameObject parentObject = this.ParentObject;
							GameObject hookedObject = this.HookedObject;
							GameObject hookingWeapon = this.HookingWeapon;
							BodyPart bodyPart = this.ParentObject.Body.FindDefaultOrEquippedItem(this.HookingWeapon);
							string text = "Autohit";
							int num2 = 0;
							int num3 = 0;
							int num4 = 0;
							int num5 = 0;
							int num6 = 0;
							GameObject hookingWeapon2 = this.HookingWeapon;
							Combat.MeleeAttackWithWeapon(parentObject, hookedObject, hookingWeapon, bodyPart, text, num2, num3, num4, num5, num6, hookingWeapon2 != null && hookingWeapon2.IsEquippedOrDefaultOfPrimary(this.ParentObject), true);
							if (this.HookedObject.IsValid())
							{
								this.HookedObject.Move(directionFromCell, false, false, false, false, true, true, this.HookingWeapon, null, false, null, null, null, false, false, null, null, 0);
								this.HookedObject.PlayWorldSound("Sounds/Abilities/sfx_ability_drag", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							}
						}
						else if (num > 2)
						{
							this.HookedObject.RemoveEffect(hooked, true);
							this.HookedObject = null;
							this.HookingWeapon = null;
						}
					}
				}
				else
				{
					this.HookedObject = null;
					this.HookingWeapon = null;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF83 RID: 49027 RVA: 0x00462852 File Offset: 0x00460A52
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.Validate(E.Cell);
			if (this.HookedObject != null)
			{
				this.LeftCell.SetCell(E.Cell);
			}
			else
			{
				this.LeftCell.Clear();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF84 RID: 49028 RVA: 0x0046288E File Offset: 0x00460A8E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginMove");
			Registrar.Register("CommandAxeHookAndDrag");
			Registrar.Register("StopFighting");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BF85 RID: 49029 RVA: 0x004628BC File Offset: 0x00460ABC
		private bool WeaponMatch(Effect FX)
		{
			Hooked hooked = FX as Hooked;
			return hooked != null && hooked.HookingWeapon == this.HookingWeapon;
		}

		// Token: 0x0600BF86 RID: 49030 RVA: 0x004628E4 File Offset: 0x00460AE4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginMove")
			{
				if (this.ParentObject != null && GameObject.Validate(ref this.HookedObject) && this.HookedObject.DistanceTo(this.ParentObject) <= 1 && this.HookedObject.PhaseAndFlightMatches(this.ParentObject))
				{
					Hooked hooked = this.HookedObject.GetEffect(typeof(Hooked), new Predicate<Effect>(this.WeaponMatch)) as Hooked;
					if (hooked != null)
					{
						Cell cell = E.GetParameter("DestinationCell") as Cell;
						if (cell != null && this.HookedObject.DistanceTo(cell) == 2 && hooked.Duration > 0 && (!this.HookedObject.FireEvent("BeforeGrabbed") || this.HookedObject.MakeSave("Strength", Axe_HookAndDrag.RESISTSAVE, this.ParentObject, null, "HookAndDrag Move Grab Restraint", false, false, false, false, hooked.HookingWeapon)))
						{
							IComponent<GameObject>.XDidY(this.HookedObject, "stand", this.HookedObject.its + " ground", "!", null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
							this.ParentObject.UseEnergy(1000, "Movement Failure", null, null, false);
							return false;
						}
					}
					else
					{
						this.HookedObject = null;
						this.HookingWeapon = null;
					}
				}
				else
				{
					if (this.HookedObject != null)
					{
						if (this.HookedObject.IsValid())
						{
							Hooked hooked2 = this.HookedObject.GetEffect(typeof(Hooked), new Predicate<Effect>(this.WeaponMatch)) as Hooked;
							if (hooked2 != null)
							{
								this.HookedObject.RemoveEffect(hooked2, true);
							}
						}
						this.HookedObject = null;
					}
					this.HookingWeapon = null;
				}
			}
			else if (E.ID == "CommandAxeHookAndDrag")
			{
				GameObject primaryWeaponOfType = this.ParentObject.GetPrimaryWeaponOfType("Axe", true);
				if (primaryWeaponOfType == null)
				{
					this.ParentObject.ShowFailure("You must have an axe equipped in your primary hand to use Hook and Drag.");
					return false;
				}
				if (!this.ParentObject.CheckFrozen(false, false, false, null))
				{
					return false;
				}
				Cell cell2 = base.PickDirection("Hook and Drag", null);
				if (cell2 == null)
				{
					return false;
				}
				GameObject combatTarget = cell2.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, null);
				if (combatTarget == null || !combatTarget.PhaseAndFlightMatches(this.ParentObject))
				{
					this.ParentObject.ShowFailure("There's nothing there you can hook.");
					return false;
				}
				if (!combatTarget.FireEvent("BeforeGrabbed"))
				{
					return false;
				}
				if (!combatTarget.ApplyEffect(new Hooked(primaryWeaponOfType, 20, 9), null))
				{
					return false;
				}
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_drag", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				base.DidXToYWithZ("hook", combatTarget, "with", primaryWeaponOfType, null, "!", null, null, null, combatTarget, false, false, false, false, false, false, false, false, null, null, this.ParentObject, null, false, false, false, false, false, null);
				this.HookedObject = combatTarget;
				this.HookingWeapon = primaryWeaponOfType;
				combatTarget.Bloodsplatter();
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Axe_HookAndDrag.COOLDOWN, null, null, false);
			}
			else if (E.ID == "StopFighting")
			{
				if (this.HookedObject != null && !this.HookedObject.IsValid())
				{
					this.HookedObject = null;
					this.HookingWeapon = null;
				}
				if (this.HookedObject != null)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Target");
					if (gameObjectParameter == null || gameObjectParameter == this.HookedObject)
					{
						Hooked hooked3 = this.HookedObject.GetEffect(typeof(Hooked), new Predicate<Effect>(this.WeaponMatch)) as Hooked;
						if (hooked3 != null)
						{
							this.HookedObject.RemoveEffect(hooked3, true);
						}
						this.HookedObject = null;
						this.HookingWeapon = null;
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BF87 RID: 49031 RVA: 0x00462CD4 File Offset: 0x00460ED4
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Hook and Drag", "CommandAxeHookAndDrag", "Skills", "You grab an opponent's limb with the heel of your axe and pull them toward you. If successful, you pull your opponent with you as you move and make a free attack with your axe. Your opponent is forced to move with you but can attack you while moving. Your opponent gets a chance to resist the move (strength save; difficulty 20 + your strength modifier) and a chance to break free at the start of their turn (same save).\n\nThis effect lasts for 9 rounds or until you dismember the opponent.", "ô", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600BF88 RID: 49032 RVA: 0x00462D20 File Offset: 0x00460F20
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600BF89 RID: 49033 RVA: 0x00462D38 File Offset: 0x00460F38
		public bool Validate(Cell C = null)
		{
			Hooked hooked = null;
			if (GameObject.Validate(ref this.HookedObject))
			{
				hooked = this.HookedObject.GetEffect(typeof(Hooked), new Predicate<Effect>(this.WeaponMatch)) as Hooked;
				if (hooked != null)
				{
					if (C != null)
					{
						if (C.DistanceTo(this.HookedObject) > 1)
						{
							goto IL_0063;
						}
					}
					else if (this.ParentObject.DistanceTo(this.HookedObject) > 1)
					{
						goto IL_0063;
					}
					return true;
				}
			}
			IL_0063:
			if (hooked != null)
			{
				GameObject hookedObject = this.HookedObject;
				if (hookedObject != null)
				{
					hookedObject.RemoveEffect(hooked, true);
				}
			}
			this.HookedObject = null;
			this.HookingWeapon = null;
			return false;
		}

		// Token: 0x04004D1F RID: 19743
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D20 RID: 19744
		public GameObject HookedObject;

		// Token: 0x04004D21 RID: 19745
		public GlobalLocation LeftCell = new GlobalLocation();

		// Token: 0x04004D22 RID: 19746
		public GameObject HookingWeapon;

		// Token: 0x04004D23 RID: 19747
		public static readonly int COOLDOWN = 50;

		// Token: 0x04004D24 RID: 19748
		public static readonly int RESISTSAVE = 20;
	}
}
