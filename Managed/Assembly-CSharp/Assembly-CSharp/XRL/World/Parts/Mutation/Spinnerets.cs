using System;
using System.Text;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001455 RID: 5205
	[Serializable]
	public class Spinnerets : BaseMutation
	{
		// Token: 0x0600CB9E RID: 52126 RVA: 0x004A0CBE File Offset: 0x0049EEBE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetMovementAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == LeftCellEvent.ID || ID == ModifyDefendingSaveEvent.ID || ID == LeavingCellEvent.ID;
		}

		// Token: 0x0600CB9F RID: 52127 RVA: 0x004A0CF4 File Offset: 0x0049EEF4
		public override bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandSpinWeb", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBA0 RID: 52128 RVA: 0x004A0D1D File Offset: 0x0049EF1D
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance > 1 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandSpinWeb", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBA1 RID: 52129 RVA: 0x004A0D50 File Offset: 0x0049EF50
		public override bool HandleEvent(LeavingCellEvent E)
		{
			if (base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null) && !this.ParentObject.OnWorldMap() && this.SpinTimer > 0 && E != null)
			{
				Cell cell = E.Cell;
				if (cell != null)
				{
					cell.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_spinnerets_webDrop", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return true;
		}

		// Token: 0x0600CBA2 RID: 52130 RVA: 0x004A0DBC File Offset: 0x0049EFBC
		public override bool HandleEvent(LeftCellEvent E)
		{
			if (base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
			{
				if (E.Cell.OnWorldMap())
				{
					this.SpinTimer = -1;
				}
				else
				{
					this.SpinTimer--;
				}
				if (this.SpinTimer < 0)
				{
					base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
				}
				else
				{
					GameObject gameObject;
					if (!this.Phase && this.ParentObject.GetPhase() != 2)
					{
						gameObject = GameObject.Create("Web");
						Sticky part = gameObject.GetPart<Sticky>();
						if (part != null)
						{
							part.SaveTarget = 13 + 3 * base.Level;
						}
					}
					else
					{
						gameObject = GameObject.Create("PhaseWeb");
						gameObject.ApplyEffect(new Phased(), null);
						PhaseSticky part2 = gameObject.GetPart<PhaseSticky>();
						if (part2 != null)
						{
							part2.SaveTarget = 18 + 2 * base.Level;
						}
					}
					E.Cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBA3 RID: 52131 RVA: 0x004A0EB9 File Offset: 0x0049F0B9
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Move", E))
			{
				E.Roll += this.GetMoveSaveModifier();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBA4 RID: 52132 RVA: 0x004A0EE2 File Offset: 0x0049F0E2
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600CBA5 RID: 52133 RVA: 0x004A0EE5 File Offset: 0x0049F0E5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyStuck");
			Registrar.Register("CommandSpinWeb");
			Registrar.Register("VillageInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CBA6 RID: 52134 RVA: 0x004A0F10 File Offset: 0x0049F110
		public override string GetDescription()
		{
			return "You can spin sticky silk webs.";
		}

		// Token: 0x0600CBA7 RID: 52135 RVA: 0x004A0F17 File Offset: 0x0049F117
		public int GetMoveSaveModifier()
		{
			return 5 + base.Level;
		}

		// Token: 0x0600CBA8 RID: 52136 RVA: 0x004A0F24 File Offset: 0x0049F124
		public override string GetLevelText(int Level)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Compound("While spinning, you leave webs in your wake as you move.", '\n');
			if (Level != base.Level)
			{
				stringBuilder.Compound("{{rules|Increased web strength}}", '\n');
			}
			stringBuilder.Compound("Duration: {{rules|", '\n').Append(this.GetDuration(Level)).Append("}} move actions");
			SavingThrows.AppendSaveBonusDescription(stringBuilder, this.GetMoveSaveModifier(), "Move", true, false, true);
			stringBuilder.Compound("Cooldown: 80 rounds", '\n');
			stringBuilder.Compound("You are immune to getting stuck.", '\n');
			stringBuilder.Compound("+300 reputation with {{w|arachnids}}", '\n');
			return stringBuilder.ToString();
		}

		// Token: 0x0600CBA9 RID: 52137 RVA: 0x004A0FC5 File Offset: 0x0049F1C5
		public int GetDuration(int Level)
		{
			return 5 + Level;
		}

		// Token: 0x0600CBAA RID: 52138 RVA: 0x004A0FCA File Offset: 0x0049F1CA
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("SpinDuration", this.GetDuration(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 80, 0);
		}

		// Token: 0x0600CBAB RID: 52139 RVA: 0x004A1008 File Offset: 0x0049F208
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyStuck")
			{
				return false;
			}
			if (E.ID == "CommandSpinWeb")
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_physical_generic_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (this.ParentObject.OnWorldMap())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You cannot do that on the world map.", true, true, true);
					}
					return false;
				}
				base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
				if (base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
				{
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 80, null, null, false);
					this.SpinTimer = 5 + base.Level;
				}
			}
			else if (E.ID == "VillageInit")
			{
				this.Active = false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CBAC RID: 52140 RVA: 0x004A1105 File Offset: 0x0049F305
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CBAD RID: 52141 RVA: 0x004A1110 File Offset: 0x0049F310
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Spin Webs", "CommandSpinWeb", "Physical Mutations", null, "#", null, false, false, true, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			if (this.Phase)
			{
				GO.ApplyEffect(new Phased(9999), null);
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CBAE RID: 52142 RVA: 0x004A1173 File Offset: 0x0049F373
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004F25 RID: 20261
		public const string SAVE_BONUS_VS = "Move";

		// Token: 0x04004F26 RID: 20262
		public int SpinTimer;

		// Token: 0x04004F27 RID: 20263
		public bool Phase;

		// Token: 0x04004F28 RID: 20264
		public bool Active;
	}
}
