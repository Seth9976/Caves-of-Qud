using System;
using System.Text;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x020011C1 RID: 4545
	[Serializable]
	public class EnergyCell : IEnergyCell
	{
		// Token: 0x0600B486 RID: 46214 RVA: 0x0041D638 File Offset: 0x0041B838
		public override bool HasAnyCharge()
		{
			return this.Charge > 0;
		}

		// Token: 0x0600B487 RID: 46215 RVA: 0x0041D643 File Offset: 0x0041B843
		public override bool HasCharge(int Amount)
		{
			return this.Charge >= Amount;
		}

		// Token: 0x0600B488 RID: 46216 RVA: 0x0041D651 File Offset: 0x0041B851
		public override int GetCharge()
		{
			return this.Charge;
		}

		// Token: 0x0600B489 RID: 46217 RVA: 0x0041D659 File Offset: 0x0041B859
		public override int GetChargePercentage()
		{
			if (this.MaxCharge == 0)
			{
				return 0;
			}
			return this.Charge * 100 / this.MaxCharge;
		}

		// Token: 0x0600B48A RID: 46218 RVA: 0x0041D678 File Offset: 0x0041B878
		public override void UseCharge(int Amount)
		{
			bool flag = this.Charge > 0;
			this.Charge -= Amount;
			if (this.Charge < 0)
			{
				this.Charge = 0;
			}
			else if (this.Charge > this.MaxCharge)
			{
				this.Charge = this.MaxCharge;
			}
			if (flag && this.Charge == 0)
			{
				CellDepletedEvent.Send(this.ParentObject, this);
			}
		}

		// Token: 0x0600B48B RID: 46219 RVA: 0x0041D6E0 File Offset: 0x0041B8E0
		public override void AddCharge(int Amount)
		{
			this.Charge += Amount;
			if (this.Charge > this.MaxCharge)
			{
				this.Charge = this.MaxCharge;
			}
			else if (this.Charge < 0)
			{
				this.Charge = 0;
			}
			if (this.ParentObject.Physics.InInventory != null || this.ParentObject.Physics.CurrentCell != null)
			{
				this.ParentObject.CheckStack();
			}
		}

		// Token: 0x0600B48C RID: 46220 RVA: 0x0041D757 File Offset: 0x0041B957
		public override void TinkerInitialize()
		{
			this.Charge = 0;
		}

		// Token: 0x0600B48D RID: 46221 RVA: 0x0041D760 File Offset: 0x0041B960
		public override void SetChargePercentage(int Percentage)
		{
			this.Charge = Math.Max(0, Math.Min(this.MaxCharge * Percentage / 100, this.MaxCharge));
		}

		// Token: 0x0600B48E RID: 46222 RVA: 0x0041D784 File Offset: 0x0041B984
		public override void RandomizeCharge()
		{
			this.Charge = Stat.Random(1, this.MaxCharge);
		}

		// Token: 0x0600B48F RID: 46223 RVA: 0x0041D798 File Offset: 0x0041B998
		public override void MaximizeCharge()
		{
			this.Charge = this.MaxCharge;
		}

		// Token: 0x0600B490 RID: 46224 RVA: 0x0041D7A6 File Offset: 0x0041B9A6
		public int GetChargeLevel()
		{
			return EnergyStorage.GetChargeLevel(this.Charge, this.MaxCharge);
		}

		// Token: 0x0600B491 RID: 46225 RVA: 0x0041D7B9 File Offset: 0x0041B9B9
		public bool UseAltChargeDisplayStyle()
		{
			return The.Player != null && !string.IsNullOrEmpty(this.AltChargeDisplayProperty) && The.Player.GetIntProperty(this.AltChargeDisplayProperty, 0) > 0;
		}

		// Token: 0x0600B492 RID: 46226 RVA: 0x0041D7EC File Offset: 0x0041B9EC
		public override string ChargeStatus()
		{
			string text = (this.UseAltChargeDisplayStyle() ? this.AltChargeDisplayStyle : this.ChargeDisplayStyle);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return null;
			}
			return EnergyStorage.GetChargeStatus(this.Charge, this.MaxCharge, text);
		}

		// Token: 0x0600B493 RID: 46227 RVA: 0x0041D858 File Offset: 0x0041BA58
		public override bool CanBeRecharged()
		{
			return this.IsRechargeable && base.IsReady(false, true, true, false, false, false, false, true, true, false, 1, null, false, 0L, null);
		}

		// Token: 0x0600B494 RID: 46228 RVA: 0x0041D895 File Offset: 0x0041BA95
		public override int GetRechargeAmount()
		{
			return this.MaxCharge - this.Charge;
		}

		// Token: 0x0600B495 RID: 46229 RVA: 0x0041D8A4 File Offset: 0x0041BAA4
		public override int GetRechargeValue()
		{
			return this.RechargeValue;
		}

		// Token: 0x0600B496 RID: 46230 RVA: 0x0041D8AC File Offset: 0x0041BAAC
		public override char GetRechargeBit()
		{
			return this.RechargeBit;
		}

		// Token: 0x0600B497 RID: 46231 RVA: 0x0041D8B4 File Offset: 0x0041BAB4
		public override bool SameAs(IPart p)
		{
			EnergyCell energyCell = p as EnergyCell;
			return energyCell.Charge == this.Charge && energyCell.MaxCharge == this.MaxCharge && energyCell.ChargeRate == this.ChargeRate && energyCell.RechargeValue == this.RechargeValue && energyCell.RechargeBit == this.RechargeBit && !(energyCell.StartCharge != this.StartCharge) && !(energyCell.ChargeDisplayStyle != this.ChargeDisplayStyle) && !(energyCell.AltChargeDisplayStyle != this.AltChargeDisplayStyle) && !(energyCell.AltChargeDisplayProperty != this.AltChargeDisplayProperty) && energyCell.ConsiderLive == this.ConsiderLive && energyCell.IsRechargeable == this.IsRechargeable && base.SameAs(p);
		}

		// Token: 0x0600B498 RID: 46232 RVA: 0x0041D994 File Offset: 0x0041BB94
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == AfterObjectCreatedEvent.ID && !this.StartCharge.IsNullOrEmpty()) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || (ID == PooledEvent<GetElectricalConductivityEvent>.ID && this.ConsiderLive) || ID == GetInventoryActionsEvent.ID || ID == GetSlottedInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == QueryChargeEvent.ID || ID == QueryRechargeStorageEvent.ID || ID == RechargeAvailableEvent.ID || ID == TestChargeEvent.ID || ID == UseChargeEvent.ID || (Options.AutogetAmmo && ID == AutoexploreObjectEvent.ID);
		}

		// Token: 0x0600B499 RID: 46233 RVA: 0x0041DA36 File Offset: 0x0041BC36
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (!this.StartCharge.IsNullOrEmpty())
			{
				this.Charge = Math.Min(Math.Max(this.StartCharge.RollCached(), 0), this.MaxCharge);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B49A RID: 46234 RVA: 0x0041DA6E File Offset: 0x0041BC6E
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject && this.ConsiderLive)
			{
				E.MinValue(95);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B49B RID: 46235 RVA: 0x0041DA9E File Offset: 0x0041BC9E
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (E.Command == null && Options.AutogetAmmo)
			{
				E.Command = "Autoget";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B49C RID: 46236 RVA: 0x0041DAC1 File Offset: 0x0041BCC1
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Charge", this.Charge);
			E.AddEntry(this, "MaxCharge", this.MaxCharge);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B49D RID: 46237 RVA: 0x0041DAF0 File Offset: 0x0041BCF0
		public override bool HandleEvent(QueryChargeEvent E)
		{
			if ((!E.LiveOnly || this.ConsiderLive) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.ChargeUse * E.Multiple;
				int num2 = this.GetCharge() - num;
				if (num2 > 0)
				{
					E.Amount += num2;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B49E RID: 46238 RVA: 0x0041DB68 File Offset: 0x0041BD68
		public override bool HandleEvent(TestChargeEvent E)
		{
			if ((!E.LiveOnly || this.ConsiderLive) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.ChargeUse * E.Multiple;
				int num2 = Math.Min(E.Amount, this.GetCharge() - num);
				if (num2 > 0)
				{
					E.Amount -= num2;
					if (E.Amount <= 0)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B49F RID: 46239 RVA: 0x0041DBF4 File Offset: 0x0041BDF4
		public override bool HandleEvent(UseChargeEvent E)
		{
			if (E.Pass == 2 && (!E.LiveOnly || this.ConsiderLive) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.ChargeUse * E.Multiple;
				int num2 = Math.Min(E.Amount, this.GetCharge() - num);
				if (num2 > 0)
				{
					this.UseCharge(num2 + num);
					E.Amount -= num2;
					if (E.Amount <= 0)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4A0 RID: 46240 RVA: 0x0041DC94 File Offset: 0x0041BE94
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Context != "Tinkering" && E.Understood())
			{
				string text = this.ChargeStatus();
				if (text != null)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("{{y|(").Append(text).Append(")}}");
					E.AddTag(stringBuilder.ToString(), 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4A1 RID: 46241 RVA: 0x0041DCFC File Offset: 0x0041BEFC
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.CanBeRecharged() && The.Player.HasSkill("Tinkering_Tinker1") && this.Charge < this.MaxCharge && The.Player.CanMoveExtremities("Recharge", false, false, false))
			{
				E.AddAction("Recharge", "recharge", "RechargeEnergyCell", null, 'R', false, 100 - this.Charge * 100 / this.MaxCharge, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4A2 RID: 46242 RVA: 0x0041DD80 File Offset: 0x0041BF80
		public override bool HandleEvent(GetSlottedInventoryActionsEvent E)
		{
			if (this.CanBeRecharged() && The.Player.HasSkill("Tinkering_Tinker1") && this.Charge < this.MaxCharge && this.ParentObject.Understood() && The.Player.CanMoveExtremities("Recharge", false, false, false))
			{
				E.AddAction("RechargeSlotted", "recharge " + this.ParentObject.BaseDisplayNameStripped, "RechargeEnergyCell", null, 'R', false, 90 - this.Charge * 90 / this.MaxCharge, 0, false, false, false, false, true, this.ParentObject, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4A3 RID: 46243 RVA: 0x0041DE29 File Offset: 0x0041C029
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "RechargeEnergyCell" && this.CanBeRecharged())
			{
				E.Actor.GetPart<Tinkering_Tinker1>().Recharge(this.ParentObject, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4A4 RID: 46244 RVA: 0x0041DE64 File Offset: 0x0041C064
		public override bool HandleEvent(RechargeAvailableEvent E)
		{
			if (E.Amount > 0 && this.Charge < this.MaxCharge && base.IsReady(false, true, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = E.Amount;
				if (!E.Forced && this.ChargeRate >= 0)
				{
					int num2 = this.ChargeRate * E.Multiple;
					if (num > num2)
					{
						num = num2;
					}
				}
				if (num > this.MaxCharge - this.Charge)
				{
					num = this.MaxCharge - this.Charge;
				}
				if (num > 0)
				{
					this.AddCharge(num);
					E.Amount -= num;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4A5 RID: 46245 RVA: 0x0041DF20 File Offset: 0x0041C120
		public override bool HandleEvent(QueryRechargeStorageEvent E)
		{
			if (base.IsReady(false, true, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.MaxCharge - this.GetCharge();
				if (num > 0)
				{
					E.Amount += num;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040048C0 RID: 18624
		public int Charge = 100;

		// Token: 0x040048C1 RID: 18625
		public int MaxCharge = 100;

		// Token: 0x040048C2 RID: 18626
		public int ChargeRate = 10;

		// Token: 0x040048C3 RID: 18627
		public int RechargeValue = 3000;

		// Token: 0x040048C4 RID: 18628
		public char RechargeBit = 'R';

		// Token: 0x040048C5 RID: 18629
		public string StartCharge;

		// Token: 0x040048C6 RID: 18630
		public string ChargeDisplayStyle = "electrical";

		// Token: 0x040048C7 RID: 18631
		public string AltChargeDisplayStyle = "percentage";

		// Token: 0x040048C8 RID: 18632
		public string AltChargeDisplayProperty = Scanning.GetScanPropertyName(Scanning.Scan.Tech);

		// Token: 0x040048C9 RID: 18633
		public bool ConsiderLive = true;

		// Token: 0x040048CA RID: 18634
		public bool IsRechargeable = true;
	}
}
