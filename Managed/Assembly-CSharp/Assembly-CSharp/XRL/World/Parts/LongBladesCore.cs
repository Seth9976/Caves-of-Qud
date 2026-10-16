using System;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001230 RID: 4656
	[Serializable]
	public class LongBladesCore : IPart
	{
		// Token: 0x0600B8E4 RID: 47332 RVA: 0x00436899 File Offset: 0x00434A99
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetToHitModifierEvent>.ID || ID == PooledEvent<NeedPartSupportEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600B8E5 RID: 47333 RVA: 0x004368C8 File Offset: 0x00434AC8
		public void CollectStatsAgressive(Templates.StatCollector stats)
		{
			if (this.ParentObject.HasPart<LongBladesImprovedAggressiveStance>())
			{
				stats.Set("HitPenalty", 3, true, -1);
				stats.Set("PenBonus", 2, true, 1);
				stats.AddChangePostfix("Penetration bonus", 1, "Improved Agressive Stance");
				stats.AddChangePostfix("To-hit penalty", 1, "Improved Agressive Stance");
				return;
			}
			stats.Set("HitPenalty", 2, false, 0);
			stats.Set("PenBonus", 1, false, 0);
		}

		// Token: 0x0600B8E6 RID: 47334 RVA: 0x0043693F File Offset: 0x00434B3F
		public void CollectStatsDefensive(Templates.StatCollector stats)
		{
			if (this.ParentObject.HasPart<LongBladesImprovedDefensiveStance>())
			{
				stats.Set("DVBonus", 3, true, 1);
				stats.AddChangePostfix("DV bonus", 1, "Improved Defensive Stance");
				return;
			}
			stats.Set("DVBonus", 2, false, 0);
		}

		// Token: 0x0600B8E7 RID: 47335 RVA: 0x0043697D File Offset: 0x00434B7D
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.AggressiveStanceID, new Action<Templates.StatCollector>(this.CollectStatsAgressive), null);
			base.DescribeMyActivatedAbility(this.DefensiveStanceID, new Action<Templates.StatCollector>(this.CollectStatsDefensive), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8E8 RID: 47336 RVA: 0x004369BC File Offset: 0x00434BBC
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Actor == this.ParentObject && E.Checking == "Actor" && E.Melee && (E.Skill == "LongBlades" || E.Skill == "ShortBlades") && this.IsPrimaryBladeEquipped())
			{
				if (this.currentStance == "aggressive")
				{
					E.Modifier -= (this.ParentObject.HasPart<LongBladesImprovedAggressiveStance>() ? 3 : 2);
				}
				else if (this.currentStance == "dueling")
				{
					E.Modifier += (this.ParentObject.HasPart<LongBladesImprovedDuelistStance>() ? 3 : 2);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8E9 RID: 47337 RVA: 0x00436A90 File Offset: 0x00434C90
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.IsPrimaryBladeEquipped() && GameObject.Validate(E.Target))
			{
				string text = "aggressive";
				if (E.Actor.isDamaged(0.6, false))
				{
					text = "defensive";
				}
				else if (E.Actor.HasPart<LongBladesDuelingStance>() && (double)base.ConTarget(E.Target) >= 0.8)
				{
					text = "dueling";
				}
				if (this.currentStance != text)
				{
					if (text == "aggressive")
					{
						if (base.IsMyActivatedAbilityAIUsable(this.AggressiveStanceID, null))
						{
							E.Add("CommandAggressiveStance", 10, null, false, false, null, null);
						}
					}
					else if (text == "defensive")
					{
						if (base.IsMyActivatedAbilityAIUsable(this.DefensiveStanceID, null))
						{
							E.Add("CommandDefensiveStance", 10, null, false, false, null, null);
						}
					}
					else if (text == "dueling")
					{
						LongBladesDuelingStance part = E.Actor.GetPart<LongBladesDuelingStance>();
						if (part != null && part.IsMyActivatedAbilityAIUsable(part.ActivatedAbilityID, null))
						{
							E.Add("CommandDuelingStance", 10, null, false, false, null, null);
						}
					}
				}
				if (E.Distance == 1)
				{
					if ((double)base.ConTarget(E.Target) >= 0.8)
					{
						LongBladesDeathblow part2 = E.Actor.GetPart<LongBladesDeathblow>();
						if (part2 != null && part2.IsMyActivatedAbilityAIUsable(part2.ActivatedAbilityID, null))
						{
							E.Add("CommandDeathblow", 1, null, false, false, null, null);
							return true;
						}
					}
					if (this.currentStance != "aggressive")
					{
						LongBladesLunge part3 = E.Actor.GetPart<LongBladesLunge>();
						if (part3 != null && part3.IsMyActivatedAbilityAIUsable(part3.ActivatedAbilityID, null))
						{
							E.Add("CommandLunge", 1, null, false, false, null, null);
						}
					}
					LongBladesSwipe part4 = E.Actor.GetPart<LongBladesSwipe>();
					if (part4 != null && part4.IsMyActivatedAbilityAIUsable(part4.ActivatedAbilityID, null))
					{
						E.Add("CommandSwipe", 1, null, false, false, null, null);
					}
				}
				else if (E.Distance == 2 && this.currentStance == "aggressive")
				{
					LongBladesLunge part5 = E.Actor.GetPart<LongBladesLunge>();
					if (part5 != null && part5.IsMyActivatedAbilityAIUsable(part5.ActivatedAbilityID, null))
					{
						E.Add("CommandLunge", 1, null, false, false, null, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8EA RID: 47338 RVA: 0x00436CD9 File Offset: 0x00434ED9
		public override bool HandleEvent(NeedPartSupportEvent E)
		{
			if (E.Type == "LongBladesCore" && !PartSupportEvent.Check(E, this))
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8EB RID: 47339 RVA: 0x00436D09 File Offset: 0x00434F09
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B8EC RID: 47340 RVA: 0x00436D0C File Offset: 0x00434F0C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CommandAggressiveStance");
			Registrar.Register("CommandDeathblow");
			Registrar.Register("CommandDefensiveStance");
			Registrar.Register("CommandDuelingStance");
			Registrar.Register("CommandLunge");
			Registrar.Register("CommandSwipe");
			Registrar.Register("EquipperEquipped");
			Registrar.Register("EquipperUnequipped");
			Registrar.Register("GetAttackerHitDice");
			Registrar.Register("GetDefenderDV");
			Registrar.Register("LongBlades.UpdateStance");
			Registrar.Register("PrimaryLimbRecalculated");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B8ED RID: 47341 RVA: 0x00436DB0 File Offset: 0x00434FB0
		public override void Initialize()
		{
			base.Initialize();
			if (this.AggressiveStanceID == Guid.Empty)
			{
				this.AggressiveStanceID = base.AddMyActivatedAbility("Aggressive Stance", "CommandAggressiveStance", "Stances", "+1/2 to penetration rolls, -2/-3 to hit while wielding a long blade in your primary hand", "\u009f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			}
			if (this.DefensiveStanceID == Guid.Empty)
			{
				this.DefensiveStanceID = base.AddMyActivatedAbility("Defensive Stance", "CommandDefensiveStance", "Stances", "+2/3 DV while wielding a long blade in your primary hand", "\u009f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			}
			if (this.currentStance.IsNullOrEmpty())
			{
				this.ChangeStance(this.ParentObject.GetPropertyOrTag("InitialStance", null) ?? "defensive");
			}
		}

		// Token: 0x0600B8EE RID: 47342 RVA: 0x00436E84 File Offset: 0x00435084
		public override void Remove()
		{
			base.RemoveMyActivatedAbility(ref this.AggressiveStanceID, null);
			base.RemoveMyActivatedAbility(ref this.DefensiveStanceID, null);
			this.ParentObject.RemoveEffect(typeof(LongbladeStance_Aggressive));
			this.ParentObject.RemoveEffect(typeof(LongbladeStance_Defensive));
			this.ParentObject.RemoveEffect(typeof(LongbladeStance_Dueling));
			this.ParentObject.RemoveEffect(typeof(LongbladeEffect_EnGarde));
			base.Remove();
		}

		// Token: 0x0600B8EF RID: 47343 RVA: 0x00436F0B File Offset: 0x0043510B
		public GameObject GetPrimaryBlade()
		{
			return this.ParentObject.GetPrimaryWeaponOfType("LongBlades");
		}

		// Token: 0x0600B8F0 RID: 47344 RVA: 0x00436F1D File Offset: 0x0043511D
		public bool IsPrimaryBladeEquipped()
		{
			return this.ParentObject.HasPrimaryWeaponOfType("LongBlades");
		}

		// Token: 0x0600B8F1 RID: 47345 RVA: 0x00436F30 File Offset: 0x00435130
		public string GetCommandForStance(string stance)
		{
			string text;
			if (!(stance == "defensive"))
			{
				if (!(stance == "aggressive"))
				{
					if (!(stance == "dueling"))
					{
						text = null;
					}
					else
					{
						text = "CommandDuelingStance";
					}
				}
				else
				{
					text = "CommandAggressiveStance";
				}
			}
			else
			{
				text = "CommandDefensiveStance";
			}
			return text;
		}

		// Token: 0x0600B8F2 RID: 47346 RVA: 0x00436F81 File Offset: 0x00435181
		public ActivatedAbilityEntry GetAbilityForStance(string stance)
		{
			return this.ParentObject.ActivatedAbilities.GetAbilityByCommand(this.GetCommandForStance(stance));
		}

		// Token: 0x0600B8F3 RID: 47347 RVA: 0x00436F9C File Offset: 0x0043519C
		public void ChangeStance(string newStance)
		{
			if (this.currentStance == "defensive")
			{
				base.StatShifter.RemoveStatShifts();
			}
			ActivatedAbilityEntry abilityForStance = this.GetAbilityForStance(this.currentStance);
			if (abilityForStance != null)
			{
				abilityForStance.ToggleState = false;
			}
			if (newStance != this.currentStance)
			{
				this.currentStance = newStance;
				if (base.Visible())
				{
					if (!this.ParentObject.IsPlayer() || base.MyActivatedAbilities == null || !base.MyActivatedAbilities.Silent)
					{
						this.ParentObject.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_physicalStanceChange", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						base.DidX("switch", "to " + newStance + " stance", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
					}
					Cell currentCell = this.ParentObject.GetCurrentCell();
					if (currentCell != null)
					{
						int x = currentCell.X;
						int y = currentCell.Y;
						string text = "&W";
						if (this.currentStance == "dueling")
						{
							text = "&W";
						}
						else if (this.currentStance == "aggressive")
						{
							text = "&R";
						}
						else if (this.currentStance == "defensive")
						{
							text = "&G";
						}
						for (int i = 0; i < 8; i++)
						{
							The.ParticleManager.AddRadial(text + ".", (float)x, (float)y, (float)(i * 45) / 360f * 6.14f, (float)Stat.Random(2, 4), -0.035f * (float)Stat.Random(8, 12), -0.3f + -0.15f * (float)Stat.Random(1, 3), 40, 0L);
						}
					}
				}
			}
			ActivatedAbilityEntry abilityForStance2 = this.GetAbilityForStance(this.currentStance);
			if (abilityForStance2 != null)
			{
				abilityForStance2.ToggleState = true;
			}
			if (this.currentStance == "defensive" && this.IsPrimaryBladeEquipped())
			{
				int num = 2;
				if (this.ParentObject.HasPart<LongBladesImprovedDefensiveStance>())
				{
					num++;
				}
				base.StatShifter.SetStatShift("DV", num, false);
			}
			if (this.currentStance == "aggressive")
			{
				this.ParentObject.ApplyEffect(new LongbladeStance_Aggressive(), null);
				return;
			}
			if (this.currentStance == "dueling")
			{
				this.ParentObject.ApplyEffect(new LongbladeStance_Dueling(), null);
				return;
			}
			if (this.currentStance == "defensive")
			{
				this.ParentObject.ApplyEffect(new LongbladeStance_Defensive(), null);
			}
		}

		// Token: 0x0600B8F4 RID: 47348 RVA: 0x0043722C File Offset: 0x0043542C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.Ultmode > 0)
				{
					this.Ultmode--;
					if (this.Ultmode <= 0)
					{
						this.ParentObject.RemoveEffect<LongbladeEffect_EnGarde>();
					}
					else if (this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(this.Ultmode.Things("turn remains", "turns remain") + " until your guard is down.", null, true);
					}
				}
			}
			else if (E.ID == "GetAttackerHitDice")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Weapon");
				if (gameObjectParameter != null)
				{
					MeleeWeapon part = gameObjectParameter.GetPart<MeleeWeapon>();
					if (part != null && (part.Skill == "LongBlades" || part.Skill == "ShortBlades") && this.IsPrimaryBladeEquipped() && this.currentStance == "aggressive")
					{
						int num = (this.ParentObject.HasPart<LongBladesImprovedAggressiveStance>() ? 2 : 1);
						E.SetParameter("PenetrationBonus", E.GetIntParameter("PenetrationBonus", 0) + num);
					}
				}
			}
			else if (E.ID == "CommandAggressiveStance")
			{
				if (!this.IsPrimaryBladeEquipped())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You must have a long blade equipped to switch stances.", true, true, true);
					}
					return false;
				}
				if (!this.ParentObject.CanMoveExtremities(null, true, false, false))
				{
					return false;
				}
				this.ChangeStance("aggressive");
			}
			else if (E.ID == "CommandDefensiveStance")
			{
				if (!this.IsPrimaryBladeEquipped())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You must have a long blade equipped to switch stances.", true, true, true);
					}
					return false;
				}
				if (!this.ParentObject.CanMoveExtremities(null, true, false, false))
				{
					return false;
				}
				this.ChangeStance("defensive");
			}
			else if (E.ID == "CommandDuelingStance")
			{
				if (!this.IsPrimaryBladeEquipped())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You must have a long blade equipped to switch stances.", true, true, true);
					}
					return false;
				}
				if (!this.ParentObject.CanMoveExtremities(null, true, false, false))
				{
					return false;
				}
				this.ChangeStance("dueling");
			}
			else if (E.ID == "LongBlades.UpdateStance" || E.ID == "EquipperEquipped" || E.ID == "EquipperUnequipped" || E.ID == "PrimaryLimbRecalculated")
			{
				this.ChangeStance(this.currentStance);
			}
			else if (E.ID == "CommandLunge")
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell == null)
				{
					return false;
				}
				if (currentCell.OnWorldMap())
				{
					return this.ParentObject.ShowFailure("You cannot do that on the world map.");
				}
				if (!this.IsPrimaryBladeEquipped())
				{
					return this.ParentObject.ShowFailure("You must have a long blade equipped in your primary hand to lunge.");
				}
				if (!this.ParentObject.CanMoveExtremities(null, true, false, false))
				{
					return false;
				}
				if (this.currentStance == "aggressive")
				{
					string text = base.PickDirectionS("Lunge", null);
					Cell cellFromDirection = this.ParentObject.GetCurrentCell().GetCellFromDirection(text, true);
					Cell cellFromDirection2 = cellFromDirection.GetCellFromDirection(text, true);
					if (cellFromDirection == null || cellFromDirection2 == null)
					{
						return false;
					}
					GameObject combatTarget = cellFromDirection.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, true, null);
					if (combatTarget != null)
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.ShowFail("You can't aggressively lunge through " + combatTarget.the + combatTarget.ShortDisplayName + ".", true, true, true);
						}
						return false;
					}
					GameObject combatTarget2 = cellFromDirection2.GetCombatTarget(this.ParentObject, false, false, false, 5, null, null, null, null, null, true, false, null);
					if (combatTarget2 == null)
					{
						if (this.ParentObject.IsPlayer())
						{
							if (cellFromDirection2.GetCombatTarget(this.ParentObject, true, false, false, 5, null, null, null, null, null, true, false, null) == null)
							{
								Popup.ShowFail("There's nothing there to lunge at.", true, true, true);
							}
							else
							{
								Popup.ShowFail("There's nothing there you can lunge at.", true, true, true);
							}
						}
						return false;
					}
					GameObject parentObject = this.ParentObject;
					if (parentObject != null)
					{
						parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_longBlade_lunge", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					base.DidXToY("lunge", "at", combatTarget2, null, null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					E.RequestInterfaceExit();
					if (!this.ParentObject.DirectMoveTo(cellFromDirection, 0, false, false, false, null))
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.Show("Your lunge is interrupted.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else if (base.Visible())
						{
							IComponent<GameObject>.AddPlayerMessage(Grammar.MakePossessive(this.ParentObject.The + this.ParentObject.ShortDisplayName) + " lunge is interrupted.", null, true);
						}
						return false;
					}
					if (this.ParentObject.PhaseMatches(combatTarget2))
					{
						GameObject primaryBlade = this.GetPrimaryBlade();
						Combat.MeleeAttackWithWeapon(this.ParentObject, combatTarget2, primaryBlade, this.ParentObject.Body.FindDefaultOrEquippedItem(primaryBlade), "Lunging", 0, 2, 2, 0, 0, true, true);
						combatTarget2.Bloodsplatter();
						this.ParentObject.FireEvent(Event.New("LungedTarget", "Defender", combatTarget2));
					}
					else if (this.ParentObject.IsPlayer())
					{
						Popup.Show("Your lunge passes through " + combatTarget2.the + combatTarget2.ShortDisplayName + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (base.Visible())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							Grammar.MakePossessive(this.ParentObject.The + this.ParentObject.ShortDisplayName),
							" lunge passes through ",
							combatTarget2.the,
							combatTarget2.ShortDisplayName,
							"."
						}), null, true);
					}
					this.ParentObject.UseEnergy(1000, "Skill Lunge", null, null, false);
					if (this.Ultmode <= 0)
					{
						LongBladesLunge part2 = this.ParentObject.GetPart<LongBladesLunge>();
						if (part2 != null)
						{
							part2.CooldownMyActivatedAbility(part2.ActivatedAbilityID, LongBladesLunge.COOLDOWN, null, "Agility", false);
						}
					}
				}
				else if (this.currentStance == "defensive")
				{
					Cell cell = base.PickDirection("Lunge", null);
					if (cell == null)
					{
						return false;
					}
					GameObject combatTarget3 = cell.GetCombatTarget(this.ParentObject, true, false, false, 5, null, null, null, null, null, false, false, null);
					if (combatTarget3 == null)
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.ShowFail("There's nothing there to lunge away from.", true, true, true);
						}
						return false;
					}
					GameObject parentObject2 = this.ParentObject;
					if (parentObject2 != null)
					{
						parentObject2.PlayWorldSound("Sounds/Abilities/sfx_ability_longBlade_lunge", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					string directionFromCell = cell.GetDirectionFromCell(currentCell, false);
					base.DidXToY("lunge", "away from", combatTarget3, null, null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					E.RequestInterfaceExit();
					if (this.ParentObject.PhaseAndFlightMatches(combatTarget3))
					{
						GameObject primaryBlade2 = this.GetPrimaryBlade();
						Combat.MeleeAttackWithWeapon(this.ParentObject, combatTarget3, primaryBlade2, this.ParentObject.Body.FindDefaultOrEquippedItem(primaryBlade2), "Lunging", 0, 0, 0, 0, 0, true, true);
						this.ParentObject.FireEvent(Event.New("LungedTarget", "Defender", combatTarget3));
						combatTarget3.DustPuff("&y", 15);
					}
					int num2 = this.ParentObject.GetKineticResistance() * 3 / 2;
					int num3 = 0;
					while (num3 < 2 && this.ParentObject.Physics.Push(directionFromCell, num2, 1, true, false, this.ParentObject))
					{
						num3++;
					}
					this.ParentObject.UseEnergy(1000, "Skill Lunge", null, null, false);
					this.ParentObject.Gravitate();
					if (this.Ultmode <= 0)
					{
						LongBladesLunge part3 = this.ParentObject.GetPart<LongBladesLunge>();
						if (part3 != null)
						{
							part3.CooldownMyActivatedAbility(part3.ActivatedAbilityID, 15, null, "Agility", false);
						}
					}
				}
				else
				{
					if (!(this.currentStance == "dueling"))
					{
						if (this.ParentObject.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You must be in a long blade stance to use that ability.", null, true);
						}
						return false;
					}
					Cell cell2 = base.PickDirection("Lunge", null);
					if (cell2 == null)
					{
						return false;
					}
					GameObject combatTarget4 = cell2.GetCombatTarget(this.ParentObject, false, false, false, 5, null, null, null, null, null, true, false, null);
					if (combatTarget4 == null)
					{
						if (this.ParentObject.IsPlayer())
						{
							if (cell2.GetCombatTarget(this.ParentObject, true, false, false, 5, null, null, null, null, null, true, false, null) == null)
							{
								Popup.ShowFail("There's nothing there to lunge at.", true, true, true);
							}
							else
							{
								Popup.ShowFail("There's nothing there you can lunge at.", true, true, true);
							}
						}
						return false;
					}
					base.DidXToY("lunge", "at", combatTarget4, null, null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					E.RequestInterfaceExit();
					if (this.ParentObject.PhaseMatches(combatTarget4))
					{
						combatTarget4.Bloodsplatter();
						GameObject primaryBlade3 = this.GetPrimaryBlade();
						Combat.MeleeAttackWithWeapon(this.ParentObject, combatTarget4, primaryBlade3, this.ParentObject.Body.FindDefaultOrEquippedItem(primaryBlade3), "Autohit,Autopen,Lunging", 0, 1, 1, 0, 0, true, true);
						this.ParentObject.FireEvent(Event.New("LungedTarget", "Defender", combatTarget4));
					}
					else if (this.ParentObject.IsPlayer())
					{
						Popup.Show("Your lunge passes through " + combatTarget4.the + combatTarget4.ShortDisplayName + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (base.Visible())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							Grammar.MakePossessive(this.ParentObject.The + this.ParentObject.ShortDisplayName),
							" lunge passes through ",
							combatTarget4.the,
							combatTarget4.ShortDisplayName,
							"."
						}), null, true);
					}
					this.ParentObject.UseEnergy(1000, "Skill Lunge", null, null, false);
					if (this.Ultmode <= 0)
					{
						LongBladesLunge part4 = this.ParentObject.GetPart<LongBladesLunge>();
						if (part4 != null)
						{
							part4.CooldownMyActivatedAbility(part4.ActivatedAbilityID, 15, null, "Agility", false);
						}
					}
				}
			}
			else if (E.ID == "CommandSwipe")
			{
				Cell currentCell2 = this.ParentObject.GetCurrentCell();
				if (currentCell2 == null)
				{
					return false;
				}
				if (currentCell2.OnWorldMap())
				{
					return this.ParentObject.ShowFailure("You cannot do that on the world map.");
				}
				GameObject primaryBlade4 = this.GetPrimaryBlade();
				if (primaryBlade4 == null)
				{
					return this.ParentObject.ShowFailure("You must have a long blade equipped in your primary hand to swipe.");
				}
				if (!this.ParentObject.CanMoveExtremities(null, true, false, false))
				{
					return false;
				}
				GameObject parentObject3 = this.ParentObject;
				if (parentObject3 != null)
				{
					parentObject3.PlayWorldSound("Sounds/Abilities/sfx_ability_longBlade_swipe", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (this.currentStance == "aggressive")
				{
					for (int i = 0; i < 1; i++)
					{
						if (this.ParentObject.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You aggressively swipe your blade in the air.", 'G', true);
						}
						else if (base.Visible())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								this.ParentObject.The,
								this.ParentObject.ShortDisplayName,
								" aggressively",
								this.ParentObject.GetVerb("swipe", true, false, false, true, false),
								" ",
								this.ParentObject.its,
								" blade in the air."
							}), 'R', true);
						}
						foreach (string text2 in Directions.DirectionList)
						{
							Cell cellFromDirection3 = currentCell2.GetCellFromDirection(text2, true);
							GameObject gameObject = ((cellFromDirection3 != null) ? cellFromDirection3.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, null) : null);
							if (gameObject != null && (gameObject.Brain == null || gameObject.Brain.IsHostileTowards(this.ParentObject)))
							{
								gameObject.Bloodsplatter();
								Combat.MeleeAttackWithWeapon(this.ParentObject, gameObject, primaryBlade4, this.ParentObject.Body.FindDefaultOrEquippedItem(primaryBlade4), null, 0, 0, 0, 0, 0, true, true);
							}
						}
					}
					this.ParentObject.UseEnergy(1000, "Skill Aggressive Swipe", null, null, false);
					if (this.Ultmode <= 0)
					{
						LongBladesSwipe part5 = this.ParentObject.GetPart<LongBladesSwipe>();
						if (part5 != null)
						{
							part5.CooldownMyActivatedAbility(part5.ActivatedAbilityID, 15, null, null, false);
						}
					}
				}
				else if (this.currentStance == "defensive")
				{
					if (this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You swipe your blade in the air, pushing your enemies backward.", 'G', true);
					}
					else if (base.Visible())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							this.ParentObject.The,
							this.ParentObject.ShortDisplayName,
							this.ParentObject.GetVerb("swipe", true, false, false, true, false),
							" ",
							this.ParentObject.its,
							" blade in the air, pushing ",
							this.ParentObject.its,
							" foes backward."
						}), 'R', true);
					}
					foreach (string text3 in Directions.DirectionList)
					{
						Cell cellFromDirection4 = this.ParentObject.GetCurrentCell().GetCellFromDirection(text3, true);
						GameObject gameObject2 = ((cellFromDirection4 != null) ? cellFromDirection4.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, null) : null);
						if (gameObject2 != null && gameObject2.GetMatterPhase() == 1)
						{
							gameObject2.DustPuff("&y", 15);
							gameObject2.Physics.Push(text3, 1000, 4, false, true, this.ParentObject);
							if (!gameObject2.MakeSave("Agility,Strength", 30, null, null, "LongBlades Blade Swipe Knockdown", false, false, false, false, primaryBlade4))
							{
								gameObject2.ApplyEffect(new Prone(), null);
							}
						}
					}
					this.ParentObject.UseEnergy(1000, "Skill Defensive Swipe", null, null, false);
					if (this.Ultmode <= 0)
					{
						LongBladesSwipe part6 = this.ParentObject.GetPart<LongBladesSwipe>();
						if (part6 != null)
						{
							part6.CooldownMyActivatedAbility(part6.ActivatedAbilityID, LongBladesSwipe.COOLDOWN, null, null, false);
						}
					}
				}
				else
				{
					if (!(this.currentStance == "dueling"))
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.ShowFail("You must be in a long blade stance to use that ability.", true, true, true);
						}
						return false;
					}
					Cell cell3 = base.PickDirection("Swipe", null);
					if (cell3 == null)
					{
						return false;
					}
					GameObject combatTarget5 = cell3.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, null);
					if (combatTarget5 == null)
					{
						if (this.ParentObject.IsPlayer())
						{
							if (cell3.GetCombatTarget(this.ParentObject, true, false, false, 5, null, null, null, null, null, true, false, null) == null)
							{
								Popup.ShowFail("There's nothing there to swipe at.", true, true, true);
							}
							else
							{
								Popup.ShowFail("There's nothing there you can swipe at.", true, true, true);
							}
						}
						return false;
					}
					base.DidXToY("swipe", this.ParentObject.its + " blade at", combatTarget5, null, null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					Disarming.Disarm(combatTarget5, this.ParentObject, 25, "Strength", "Agility", null, primaryBlade4);
					Combat.MeleeAttackWithWeapon(this.ParentObject, combatTarget5, primaryBlade4, this.ParentObject.Body.FindDefaultOrEquippedItem(primaryBlade4), "Autohit,Autopen", 0, 0, 0, 0, 0, true, true);
					this.ParentObject.UseEnergy(1000, "Skill Duelist Swipe", null, null, false);
					if (this.Ultmode <= 0)
					{
						LongBladesSwipe part7 = this.ParentObject.GetPart<LongBladesSwipe>();
						if (part7 != null)
						{
							part7.CooldownMyActivatedAbility(part7.ActivatedAbilityID, 15, null, null, false);
						}
					}
				}
			}
			else if (E.ID == "CommandDeathblow")
			{
				if (!this.IsPrimaryBladeEquipped())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You must have a long blade equipped to effectively yell out 'En garde!'", true, true, true);
					}
					return false;
				}
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_longBlade_enGarde", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (this.ParentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("{{G|En garde!}}", null, true);
					this.ParentObject.ParticleText("En garde!", 'W', false, 1.5f, -8f);
				}
				else if (this.ParentObject.IsVisible())
				{
					this.ParentObject.ParticleText("En garde!", 'W', false, 1.5f, -8f);
				}
				this.ParentObject.ApplyEffect(new LongbladeEffect_EnGarde(), null);
				this.Ultmode = 10;
				LongBladesDeathblow part8 = this.ParentObject.GetPart<LongBladesDeathblow>();
				if (part8 != null)
				{
					part8.CooldownMyActivatedAbility(part8.ActivatedAbilityID, 100, null, "Agility", false);
				}
				LongBladesLunge part9 = this.ParentObject.GetPart<LongBladesLunge>();
				if (part9 != null && part9.IsMyActivatedAbilityCoolingDown(part9.ActivatedAbilityID, null))
				{
					part9.MyActivatedAbility(part9.ActivatedAbilityID, null).Cooldown = 0;
				}
				LongBladesSwipe part10 = this.ParentObject.GetPart<LongBladesSwipe>();
				if (part10 != null && part10.IsMyActivatedAbilityCoolingDown(part10.ActivatedAbilityID, null))
				{
					part10.MyActivatedAbility(part10.ActivatedAbilityID, null).Cooldown = 0;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004A6A RID: 19050
		public const string SUPPORT_TYPE = "LongBladesCore";

		// Token: 0x04004A6B RID: 19051
		public const string STR_DEFENSIVE = "defensive";

		// Token: 0x04004A6C RID: 19052
		public const string STR_AGGRESSIVE = "aggressive";

		// Token: 0x04004A6D RID: 19053
		public const string STR_DUELIST = "dueling";

		// Token: 0x04004A6E RID: 19054
		public int Ultmode;

		// Token: 0x04004A6F RID: 19055
		public string currentStance = "";

		// Token: 0x04004A70 RID: 19056
		public Guid AggressiveStanceID = Guid.Empty;

		// Token: 0x04004A71 RID: 19057
		public Guid DefensiveStanceID = Guid.Empty;
	}
}
