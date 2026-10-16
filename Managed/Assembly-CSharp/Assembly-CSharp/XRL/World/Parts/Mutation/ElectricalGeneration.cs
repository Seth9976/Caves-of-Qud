using System;
using System.Text;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001432 RID: 5170
	[Serializable]
	public class ElectricalGeneration : BaseMutation
	{
		// Token: 0x0600C9A3 RID: 51619 RVA: 0x004966A5 File Offset: 0x004948A5
		public static int GetBaseChargePerTurn(int Level, int Percent)
		{
			return Level * 100 * Percent / 100;
		}

		// Token: 0x0600C9A4 RID: 51620 RVA: 0x004966B0 File Offset: 0x004948B0
		public int GetBaseChargePerTurn()
		{
			return ElectricalGeneration.GetBaseChargePerTurn(base.Level, this.BaseChargePerTurnPercent);
		}

		// Token: 0x0600C9A5 RID: 51621 RVA: 0x004966C4 File Offset: 0x004948C4
		public static int GetChargePerTurn(int Level, int Willpower, int Percent)
		{
			int baseChargePerTurn = ElectricalGeneration.GetBaseChargePerTurn(Level, Percent);
			int num = baseChargePerTurn;
			int num2 = (Willpower - 16) * 5;
			if (num2 != 0)
			{
				num = num * (100 + num2) / 100;
			}
			return Math.Max(Math.Min(num, baseChargePerTurn * 5), baseChargePerTurn / 5);
		}

		// Token: 0x0600C9A6 RID: 51622 RVA: 0x00496700 File Offset: 0x00494900
		public int GetChargePerTurn(int Level)
		{
			return ElectricalGeneration.GetChargePerTurn(Level, this.ParentObject.Stat("Willpower", 0), this.BaseChargePerTurnPercent);
		}

		// Token: 0x0600C9A7 RID: 51623 RVA: 0x0049671F File Offset: 0x0049491F
		public int GetChargePerTurn()
		{
			return this.GetChargePerTurn(base.Level);
		}

		// Token: 0x0600C9A8 RID: 51624 RVA: 0x0049672D File Offset: 0x0049492D
		public static int GetMaxCharge(int Level)
		{
			return 2000 + Level * 2000;
		}

		// Token: 0x0600C9A9 RID: 51625 RVA: 0x0049673C File Offset: 0x0049493C
		public int GetMaxCharge()
		{
			return ElectricalGeneration.GetMaxCharge(base.Level);
		}

		// Token: 0x0600C9AA RID: 51626 RVA: 0x0049674C File Offset: 0x0049494C
		public static string GetDischargeDamageRoll(int Charge)
		{
			if (Charge < 1000)
			{
				return null;
			}
			return (Charge / 1000).ToString() + "d4";
		}

		// Token: 0x0600C9AB RID: 51627 RVA: 0x0049677C File Offset: 0x0049497C
		public string GetDischargeDamageRoll()
		{
			return ElectricalGeneration.GetDischargeDamageRoll(this.Charge);
		}

		// Token: 0x0600C9AC RID: 51628 RVA: 0x00496789 File Offset: 0x00494989
		public static int GetDischargeVoltage(int Charge)
		{
			return Charge / 1000;
		}

		// Token: 0x0600C9AD RID: 51629 RVA: 0x00496792 File Offset: 0x00494992
		public int GetDischargeVoltage()
		{
			return ElectricalGeneration.GetDischargeVoltage(this.Charge);
		}

		// Token: 0x0600C9AE RID: 51630 RVA: 0x0049679F File Offset: 0x0049499F
		public override string GetDescription()
		{
			return "You accrue electrical charge that you can use and discharge to deal damage.";
		}

		// Token: 0x0600C9AF RID: 51631 RVA: 0x004967A8 File Offset: 0x004949A8
		public override string GetLevelText(int Level)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Maximum charge: {{C|").Append(ElectricalGeneration.GetMaxCharge(Level)).Append("}}");
			stringBuilder.Append("\nAccrue base {{C|").Append(ElectricalGeneration.GetBaseChargePerTurn(Level, this.BaseChargePerTurnPercent)).Append("}} charge per turn");
			stringBuilder.Append("\nCan discharge all held charge for 1d4 damage per ").Append(1000).Append(" charge");
			stringBuilder.Append("\nDischarge can arc to adjacent targets dealing reduced damage, up to 1 target per ").Append(1000).Append(" charge");
			stringBuilder.Append("\nEMP causes involuntary discharge (difficulty 18 Willpower save)");
			if (this.CanDrinkTransient)
			{
				stringBuilder.Append("\nYou can drink change from electrical power sources.");
			}
			else
			{
				stringBuilder.Append("\nYou can drink charge from energy cells and capacitors.");
			}
			stringBuilder.Append("\nYou gain ").Append(100).Append(" charge per point of electrical damage taken.");
			stringBuilder.Append("\nYou can provide charge to equipped devices that have integrated power systems.");
			return stringBuilder.ToString();
		}

		// Token: 0x0600C9B0 RID: 51632 RVA: 0x004968A1 File Offset: 0x00494AA1
		public bool PlayerCanSeeChargeAmounts()
		{
			return this.ParentObject.IsPlayer() || (IComponent<GameObject>.ThePlayer != null && (IComponent<GameObject>.ThePlayer.HasPart<ElectricalGeneration>() || Scanning.HasScanningFor(IComponent<GameObject>.ThePlayer, this.ParentObject)));
		}

		// Token: 0x0600C9B1 RID: 51633 RVA: 0x004968E0 File Offset: 0x00494AE0
		private void DischargeMessage(int Amount)
		{
			if (this.PlayerCanSeeChargeAmounts())
			{
				base.DidX("discharge", Amount.ToString() + " units of electrical charge", "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				return;
			}
			base.DidX("discharge", "an electrical arc", "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
		}

		// Token: 0x0600C9B2 RID: 51634 RVA: 0x00496948 File Offset: 0x00494B48
		public bool Discharge(Cell TargetCell, bool Accidental = false)
		{
			string dischargeDamageRoll = this.GetDischargeDamageRoll();
			if (dischargeDamageRoll == null)
			{
				return false;
			}
			int dischargeVoltage = this.GetDischargeVoltage();
			int charge = this.Charge;
			this.Charge = 0;
			this.SyncDischargeAbility();
			this.DischargeMessage(charge);
			this.ParentObject.Discharge(TargetCell, dischargeVoltage, 0, dischargeDamageRoll, null, this.ParentObject, this.ParentObject, null, null, null, null, null, 0, Accidental, false, false);
			return true;
		}

		// Token: 0x0600C9B3 RID: 51635 RVA: 0x004969AC File Offset: 0x00494BAC
		public bool Discharge(GameObject Target, bool Accidental = false)
		{
			string dischargeDamageRoll = this.GetDischargeDamageRoll();
			if (dischargeDamageRoll == null)
			{
				return false;
			}
			int dischargeVoltage = this.GetDischargeVoltage();
			int charge = this.Charge;
			this.Charge = 0;
			this.SyncDischargeAbility();
			this.DischargeMessage(charge);
			this.ParentObject.Discharge(null, dischargeVoltage, 0, dischargeDamageRoll, null, this.ParentObject, this.ParentObject, Target, null, null, null, null, 0, Accidental, false, false);
			return true;
		}

		// Token: 0x0600C9B4 RID: 51636 RVA: 0x00496A10 File Offset: 0x00494C10
		public void SyncDischargeAbility()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Discharge [").Append(this.Charge).Append(" charge]");
			base.SetMyActivatedAbilityDisplayName(this.DischargeActivatedAbilityID, stringBuilder.ToString(), null);
			if (this.Charge < 1000)
			{
				base.DisableMyActivatedAbility(this.DischargeActivatedAbilityID, null);
				return;
			}
			base.EnableMyActivatedAbility(this.DischargeActivatedAbilityID, null);
		}

		// Token: 0x0600C9B5 RID: 51637 RVA: 0x00496A83 File Offset: 0x00494C83
		public int GetCharge()
		{
			return this.Charge + this.GetChargePerTurn() - this.AdvancedCharge;
		}

		// Token: 0x0600C9B6 RID: 51638 RVA: 0x00496A9C File Offset: 0x00494C9C
		public int AddCharge(int Amount)
		{
			int num = Math.Min(this.Charge + Amount, this.GetMaxCharge());
			if (num != this.Charge)
			{
				int num2 = num - this.Charge;
				this.Charge = num;
				this.SyncDischargeAbility();
				return num2;
			}
			return 0;
		}

		// Token: 0x0600C9B7 RID: 51639 RVA: 0x00496AE0 File Offset: 0x00494CE0
		public int UseCharge(int Amount)
		{
			if (Amount <= 0)
			{
				return 0;
			}
			int num = Math.Min(this.GetChargePerTurn() - this.AdvancedCharge, Amount);
			int num2 = 0;
			if (num > 0)
			{
				this.AdvancedCharge += num;
				Amount -= num;
			}
			if (Amount > 0)
			{
				this.Charge -= Amount;
				num2 = Amount;
				if (Amount < 0)
				{
					Amount = 0;
				}
			}
			this.SyncDischargeAbility();
			return num2;
		}

		// Token: 0x0600C9B8 RID: 51640 RVA: 0x00496B42 File Offset: 0x00494D42
		public bool IsDrinkable(GameObject obj)
		{
			return obj != this.ParentObject && obj.TestCharge(1, true, 0L, this.CanDrinkTransient, false) && !obj.HasPart(typeof(VehicleSocketSeal));
		}

		// Token: 0x0600C9B9 RID: 51641 RVA: 0x00496B75 File Offset: 0x00494D75
		public virtual bool InteractWithChargeEvent(IChargeEvent E)
		{
			return (this.ConsiderLive || !E.LiveOnly) && E.IncludeBiological && base.IsMyActivatedAbilityVoluntarilyUsable(this.ProvideChargeActivatedAbilityID, null);
		}

		// Token: 0x0600C9BA RID: 51642 RVA: 0x00496B9E File Offset: 0x00494D9E
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("DischargeChunk", 1000, false, 0);
			stats.Set("CurrentCharge", this.Charge, false, 0);
		}

		// Token: 0x0600C9BB RID: 51643 RVA: 0x00496BC8 File Offset: 0x00494DC8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<EarlyBeforeBeginTakeActionEvent>.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID || ID == OwnerGetInventoryActionsEvent.ID || ID == OwnerGetShortDescriptionEvent.ID || ID == QueryChargeEvent.ID || ID == TestChargeEvent.ID || ID == TookDamageEvent.ID || ID == UseChargeEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C9BC RID: 51644 RVA: 0x00496C41 File Offset: 0x00494E41
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("circuitry", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9BD RID: 51645 RVA: 0x00496C69 File Offset: 0x00494E69
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.DischargeActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9BE RID: 51646 RVA: 0x00496C8D File Offset: 0x00494E8D
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.Charge >= 1000 && E.Distance <= 1 && base.IsMyActivatedAbilityAIUsable(this.DischargeActivatedAbilityID, null))
			{
				E.Add("CommandDischarge", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9BF RID: 51647 RVA: 0x00496CCC File Offset: 0x00494ECC
		public override bool HandleEvent(QueryChargeEvent E)
		{
			if (this.InteractWithChargeEvent(E))
			{
				int charge = this.GetCharge();
				if (charge > 0)
				{
					E.Amount += charge;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9C0 RID: 51648 RVA: 0x00496D04 File Offset: 0x00494F04
		public override bool HandleEvent(TestChargeEvent E)
		{
			if (this.InteractWithChargeEvent(E))
			{
				int num = Math.Min(E.Amount, this.GetCharge());
				if (num > 0)
				{
					E.Amount -= num;
					if (E.Amount <= 0)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9C1 RID: 51649 RVA: 0x00496D50 File Offset: 0x00494F50
		public override bool HandleEvent(UseChargeEvent E)
		{
			if (E.Pass == 1 && this.InteractWithChargeEvent(E))
			{
				int num = Math.Min(E.Amount, this.GetCharge());
				if (num > 0)
				{
					E.Amount -= num;
					this.UseCharge(num);
					if (E.Amount <= 0)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9C2 RID: 51650 RVA: 0x00496DB0 File Offset: 0x00494FB0
		public override bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			if (this.IsDrinkable(E.Object))
			{
				E.AddAction("Drink Charge", "drink charge", "DrinkCharge", null, 'k', true, -1, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9C3 RID: 51651 RVA: 0x00496DF8 File Offset: 0x00494FF8
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "DrinkCharge")
			{
				E.Item.SplitFromStack();
				if (E.Item.IsHostileTowards(this.ParentObject) && Stat.Random(1, 20) + this.ParentObject.StatMod("Agility", 0) < Stats.GetCombatDV(E.Item))
				{
					base.DidXToY("try", "to touch", E.Item, ", but " + E.Item.does("evade", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " " + this.ParentObject.them, null, null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
				}
				else
				{
					int num = E.Item.QueryCharge(true, 0L, this.CanDrinkTransient, false);
					if (num > 0 && E.Item.UseCharge(num, true, 0L, this.CanDrinkTransient, false, null))
					{
						int num2 = this.AddCharge(num);
						if (this.PlayerCanSeeChargeAmounts())
						{
							if (num2 > 0)
							{
								base.DidXToY("drink", "the juice from", E.Item, string.Concat(new string[]
								{
									"and recharge ",
									num2.ToString(),
									" ",
									(num2 == 1) ? "unit" : "units",
									" of electrical charge"
								}), null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
							}
							else
							{
								base.DidXToY("drink", "the juice from", E.Item, "but don't seem to retain any of it", null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
							}
						}
						else if (E.Item.CurrentCell != null)
						{
							base.DidXToY("touch", E.Item, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						}
					}
					else if (this.ParentObject.IsPlayer())
					{
						Popup.Show("You can't seem to drink any of the juice from " + E.Item.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (E.Item.CurrentCell != null)
					{
						base.DidXToY("touch", E.Item, null, null, null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
					}
				}
				E.Actor.UseEnergy(1000, "Physical Mutation ElectricalGeneration Drink", null, null, false);
				E.RequestInterfaceExit();
				E.Item.CheckStack();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9C4 RID: 51652 RVA: 0x004970D8 File Offset: 0x004952D8
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("StartingCharge"))
			{
				this.AddCharge(this.ParentObject.GetPropertyOrTag("StartingCharge", null).Roll());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9C5 RID: 51653 RVA: 0x00497110 File Offset: 0x00495310
		public override bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			this.PerformCharging(1);
			this.ChargedThisTurn = true;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9C6 RID: 51654 RVA: 0x00497128 File Offset: 0x00495328
		public override bool HandleEvent(OwnerGetShortDescriptionEvent E)
		{
			IntegratedPowerSystems integratedPowerSystems;
			if (E.Object.TryGetPart<IntegratedPowerSystems>(out integratedPowerSystems) && integratedPowerSystems.RequiresEvent == "HasPowerConnectors")
			{
				E.Postfix.AppendRules("Integrated power systems: When equipped, you can power " + (E.Object.UseBareIndicative ? (E.Object.IsPlural ? "these devices" : "this device") : E.Object.them) + " via Electrical Generation.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9C7 RID: 51655 RVA: 0x004971AC File Offset: 0x004953AC
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == this.ParentObject && E.Damage.IsElectricDamage())
			{
				int num = this.AddCharge(E.Damage.Amount * 100);
				if (num > 0)
				{
					if (this.PlayerCanSeeChargeAmounts())
					{
						base.DidX("recharge", num.ToString() + " " + ((num == 1) ? "unit" : "units") + " of electrical charge from the damage", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
					}
					else
					{
						base.DidX("seem", "to have absorbed some of the electrical charge", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C9C8 RID: 51656 RVA: 0x00497263 File Offset: 0x00495463
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600C9C9 RID: 51657 RVA: 0x00497266 File Offset: 0x00495466
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.ChargedThisTurn)
			{
				Amount--;
				this.ChargedThisTurn = false;
			}
			if (Amount > 0)
			{
				this.PerformCharging(Amount);
			}
		}

		// Token: 0x0600C9CA RID: 51658 RVA: 0x00497287 File Offset: 0x00495487
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyEMP");
			Registrar.Register("CommandDischarge");
			Registrar.Register("CommandToggleProvideCharge");
			Registrar.Register("HasPowerConnectors");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C9CB RID: 51659 RVA: 0x004972C0 File Offset: 0x004954C0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "HasPowerConnectors")
			{
				if (base.IsMyActivatedAbilityVoluntarilyUsable(this.ProvideChargeActivatedAbilityID, null))
				{
					return false;
				}
			}
			else if (E.ID == "ApplyEMP")
			{
				if (this.Charge >= 1000 && base.IsMyActivatedAbilityUsable(this.DischargeActivatedAbilityID, null) && !this.ParentObject.MakeSave("Willpower", 18, null, null, "EMP ForcedDischarge", false, false, false, false, this.ParentObject))
				{
					this.Discharge(this.ParentObject.CurrentCell.GetAdjacentCells(true).GetRandomElement<Cell>(null), true);
				}
			}
			else if (E.ID == "CommandDischarge")
			{
				if (this.Charge < 1000)
				{
					return false;
				}
				if (!this.PerformDischarge(true))
				{
					return false;
				}
			}
			else if (E.ID == "CommandToggleProvideCharge")
			{
				base.ToggleMyActivatedAbility(this.ProvideChargeActivatedAbilityID, null, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C9CC RID: 51660 RVA: 0x004973CC File Offset: 0x004955CC
		public bool PerformDischarge(bool AllowCancel = true)
		{
			GameObject combatTarget;
			DialogResult dialogResult;
			for (;;)
			{
				Cell cell = base.PickDirection("Discharge", null);
				if (cell == null)
				{
					break;
				}
				combatTarget = cell.GetCombatTarget(this.ParentObject, true, false, true, 0, null, null, null, null, null, true, false, null);
				if (combatTarget == null)
				{
					goto Block_1;
				}
				if (combatTarget != this.ParentObject)
				{
					goto IL_007B;
				}
				string text = "Are you sure you want to target yourself?";
				dialogResult = (AllowCancel ? Popup.ShowYesNoCancel(text, "Sounds/UI/ui_notification", true, DialogResult.Cancel) : Popup.ShowYesNo(text, "Sounds/UI/ui_notification", true, DialogResult.No, null));
				if (dialogResult != DialogResult.No)
				{
					goto Block_4;
				}
			}
			return false;
			Block_1:
			this.ParentObject.ShowFailure("There is nothing there that your electrical discharge can ground into.");
			return false;
			Block_4:
			if (dialogResult == DialogResult.Cancel)
			{
				return false;
			}
			IL_007B:
			base.UseEnergy(1000, "Physical Mutation ElectricalGeneration Discharge");
			return this.Discharge(combatTarget, false);
		}

		// Token: 0x0600C9CD RID: 51661 RVA: 0x0049746C File Offset: 0x0049566C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C9CE RID: 51662 RVA: 0x00497478 File Offset: 0x00495678
		public override bool Mutate(GameObject GO, int Level)
		{
			this.DischargeActivatedAbilityID = base.AddMyActivatedAbility("Discharge", "CommandDischarge", "Physical Mutations", null, "û", "You need at least " + 1000.ToString() + " charge to generate a discharge.", false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			this.ProvideChargeActivatedAbilityID = base.AddMyActivatedAbility("Power Devices", "CommandToggleProvideCharge", "Physical Mutations", null, "ñ", null, true, true, false, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C9CF RID: 51663 RVA: 0x00497510 File Offset: 0x00495710
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.DischargeActivatedAbilityID, null);
			base.RemoveMyActivatedAbility(ref this.ProvideChargeActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C9D0 RID: 51664 RVA: 0x00497535 File Offset: 0x00495735
		public void PerformCharging(int Turns = 1)
		{
			this.AddCharge(this.GetChargePerTurn() * Turns - this.AdvancedCharge);
			this.AdvancedCharge = 0;
		}

		// Token: 0x04004EC1 RID: 20161
		public const int PER_TURN_PER_LEVEL_BASE = 100;

		// Token: 0x04004EC2 RID: 20162
		public const int BASE_MAX = 2000;

		// Token: 0x04004EC3 RID: 20163
		public const int PER_LEVEL_MAX = 2000;

		// Token: 0x04004EC4 RID: 20164
		public const int DISCHARGE_CHUNK = 1000;

		// Token: 0x04004EC5 RID: 20165
		public const int DAMAGE_ABSORB_FACTOR = 100;

		// Token: 0x04004EC6 RID: 20166
		public const int WILLPOWER_BASELINE = 16;

		// Token: 0x04004EC7 RID: 20167
		public const int WILLPOWER_FACTOR = 5;

		// Token: 0x04004EC8 RID: 20168
		public const int WILLPOWER_CEILING_FACTOR = 5;

		// Token: 0x04004EC9 RID: 20169
		public const int WILLPOWER_FLOOR_DIVISOR = 5;

		// Token: 0x04004ECA RID: 20170
		public int Charge;

		// Token: 0x04004ECB RID: 20171
		public int AdvancedCharge;

		// Token: 0x04004ECC RID: 20172
		public int BaseChargePerTurnPercent = 100;

		// Token: 0x04004ECD RID: 20173
		public bool ConsiderLive = true;

		// Token: 0x04004ECE RID: 20174
		public bool CanDrinkTransient;

		// Token: 0x04004ECF RID: 20175
		public Guid DischargeActivatedAbilityID = Guid.Empty;

		// Token: 0x04004ED0 RID: 20176
		public Guid ProvideChargeActivatedAbilityID = Guid.Empty;

		// Token: 0x04004ED1 RID: 20177
		[NonSerialized]
		private bool ChargedThisTurn;
	}
}
