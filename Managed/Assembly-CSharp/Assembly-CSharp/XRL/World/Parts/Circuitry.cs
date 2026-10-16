using System;
using System.Text;
using XRL.Core;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020011BD RID: 4541
	[Serializable]
	public class Circuitry : IPoweredPart
	{
		// Token: 0x17000B24 RID: 2852
		// (set) Token: 0x0600B44D RID: 46157 RVA: 0x0041C1AF File Offset: 0x0041A3AF
		public int StartCharge
		{
			set
			{
				this.Charge = value;
				this.IncomingCharge = value;
				this.ActiveTurn = XRLCore.CurrentTurn;
			}
		}

		// Token: 0x0600B44E RID: 46158 RVA: 0x0041C1CA File Offset: 0x0041A3CA
		public Circuitry()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B44F RID: 46159 RVA: 0x0041C205 File Offset: 0x0041A405
		public bool HasCharge(int Amount)
		{
			return this.Charge + this.IncomingCharge >= Amount;
		}

		// Token: 0x0600B450 RID: 46160 RVA: 0x0041C21A File Offset: 0x0041A41A
		public int GetCharge()
		{
			return this.Charge + this.IncomingCharge;
		}

		// Token: 0x0600B451 RID: 46161 RVA: 0x0041C22C File Offset: 0x0041A42C
		public void UseCharge(int Amount)
		{
			this.Charge -= Amount;
			if (this.Charge < 0)
			{
				this.IncomingCharge += this.Charge;
				this.Charge = 0;
				if (this.IncomingCharge < 0)
				{
					this.IncomingCharge = 0;
					return;
				}
			}
			else if (this.MaxCharge > 0 && this.Charge + this.IncomingCharge > this.MaxCharge)
			{
				this.Charge = this.MaxCharge - this.IncomingCharge;
				if (this.Charge < 0)
				{
					this.Charge = 0;
				}
				if (this.IncomingCharge > this.MaxCharge)
				{
					this.IncomingCharge = this.MaxCharge - this.Charge;
				}
			}
		}

		// Token: 0x0600B452 RID: 46162 RVA: 0x0041C2E0 File Offset: 0x0041A4E0
		public void AddCharge(int Amount)
		{
			this.IncomingCharge += Amount;
			if (this.MaxCharge > 0 && this.Charge + this.IncomingCharge > this.MaxCharge)
			{
				this.Charge = this.MaxCharge - this.IncomingCharge;
				if (this.Charge < 0)
				{
					this.Charge = 0;
				}
				if (this.IncomingCharge > this.MaxCharge)
				{
					this.IncomingCharge = this.MaxCharge - this.Charge;
					return;
				}
			}
			else if (this.IncomingCharge < 0)
			{
				this.IncomingCharge = 0;
			}
		}

		// Token: 0x0600B453 RID: 46163 RVA: 0x0041C36E File Offset: 0x0041A56E
		public int GetChargeLevel()
		{
			return EnergyStorage.GetChargeLevel(this.Charge + this.IncomingCharge, this.MaxCharge);
		}

		// Token: 0x0600B454 RID: 46164 RVA: 0x0041C388 File Offset: 0x0041A588
		public bool UseAltChargeDisplayStyle()
		{
			return IComponent<GameObject>.ThePlayer != null && !string.IsNullOrEmpty(this.AltChargeDisplayProperty) && IComponent<GameObject>.ThePlayer.GetIntProperty(this.AltChargeDisplayProperty, 0) > 0;
		}

		// Token: 0x0600B455 RID: 46165 RVA: 0x0041C3BC File Offset: 0x0041A5BC
		public string ChargeStatus()
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
			return EnergyStorage.GetChargeStatus(this.Charge + this.IncomingCharge, this.MaxCharge, text);
		}

		// Token: 0x0600B456 RID: 46166 RVA: 0x0041C430 File Offset: 0x0041A630
		public void CheckTurn(int Multiple = 1)
		{
			long num = XRLCore.CurrentTurn + (long)(Multiple - 1);
			if (this.ActiveTurn < num)
			{
				if (this.ActiveTurn < num - 1L)
				{
					this.Charge = 0;
				}
				else
				{
					this.Charge = this.IncomingCharge;
				}
				this.IncomingCharge = 0;
				this.ActiveTurn = num;
			}
		}

		// Token: 0x0600B457 RID: 46167 RVA: 0x0041C480 File Offset: 0x0041A680
		public override bool SameAs(IPart p)
		{
			Circuitry circuitry = p as Circuitry;
			return circuitry.MaxCharge == this.MaxCharge && !(circuitry.ChargeDisplayStyle != this.ChargeDisplayStyle) && !(circuitry.AltChargeDisplayStyle != this.AltChargeDisplayStyle) && !(circuitry.AltChargeDisplayProperty != this.AltChargeDisplayProperty) && !(circuitry.Description != this.Description) && circuitry.ConsiderLive == this.ConsiderLive && circuitry.ActiveTurn == this.ActiveTurn && circuitry.Charge == this.Charge && circuitry.IncomingCharge == this.IncomingCharge && base.SameAs(p);
		}

		// Token: 0x0600B458 RID: 46168 RVA: 0x0041C540 File Offset: 0x0041A740
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EffectAppliedEvent.ID || ID == FinishChargeAvailableEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || (ID == PooledEvent<GetElectricalConductivityEvent>.ID && this.ConsiderLive) || ID == QueryChargeEvent.ID || ID == QueryChargeStorageEvent.ID || ID == TestChargeEvent.ID || ID == UseChargeEvent.ID || ID == ZoneActivatedEvent.ID || ID == ZoneDeactivatedEvent.ID || ID == ZoneThawedEvent.ID;
		}

		// Token: 0x0600B459 RID: 46169 RVA: 0x0041C5C1 File Offset: 0x0041A7C1
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Charge", this.Charge);
			E.AddEntry(this, "IncomingCharge", this.IncomingCharge);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B45A RID: 46170 RVA: 0x0041C5EE File Offset: 0x0041A7EE
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject && this.ConsiderLive)
			{
				E.MinValue(95);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B45B RID: 46171 RVA: 0x0041C620 File Offset: 0x0041A820
		public override bool HandleEvent(QueryChargeEvent E)
		{
			if ((!E.LiveOnly || this.ConsiderLive) && E.IncludeTransient && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.CheckTurn(E.Multiple);
				int num = this.ChargeUse * E.Multiple;
				int num2 = this.GetCharge() - num;
				if (num2 > 0)
				{
					E.Amount += num2;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B45C RID: 46172 RVA: 0x0041C6AC File Offset: 0x0041A8AC
		public override bool HandleEvent(TestChargeEvent E)
		{
			if ((!E.LiveOnly || this.ConsiderLive) && E.IncludeTransient && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.CheckTurn(E.Multiple);
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

		// Token: 0x0600B45D RID: 46173 RVA: 0x0041C74C File Offset: 0x0041A94C
		public override bool HandleEvent(UseChargeEvent E)
		{
			if (E.Pass == 1 && (!E.LiveOnly || this.ConsiderLive) && E.IncludeTransient && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.CheckTurn(E.Multiple);
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

		// Token: 0x0600B45E RID: 46174 RVA: 0x0041C804 File Offset: 0x0041AA04
		public override bool HandleEvent(FinishChargeAvailableEvent E)
		{
			if ((!E.LiveOnly || this.ConsiderLive) && E.IncludeTransient && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.CheckTurn(E.Multiple);
				if (E.Amount > 0 && (this.MaxCharge <= 0 || this.Charge + this.IncomingCharge < this.MaxCharge))
				{
					int num = E.Amount;
					if (this.MaxCharge > 0 && num > this.MaxCharge - this.Charge - this.IncomingCharge)
					{
						num = this.MaxCharge - this.Charge - this.IncomingCharge;
					}
					if (num > 0)
					{
						this.AddCharge(num);
						E.Amount -= num;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B45F RID: 46175 RVA: 0x0041C8EC File Offset: 0x0041AAEC
		public override bool HandleEvent(QueryChargeStorageEvent E)
		{
			if ((!E.LiveOnly || this.ConsiderLive) && E.IncludeTransient && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.MaxCharge > 0)
				{
					this.CheckTurn(E.Multiple);
					int num = this.MaxCharge - this.GetCharge();
					if (num > 0)
					{
						E.Amount += num;
						E.Transient += num;
					}
				}
				else
				{
					E.UnlimitedTransient = true;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B460 RID: 46176 RVA: 0x0041C98C File Offset: 0x0041AB8C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Context != "Tinkering" && E.Understood())
			{
				string text = this.ChargeStatus();
				if (text != null)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("{{y|(").Append(this.Description).Append(": ")
						.Append(text)
						.Append(")}}");
					E.AddTag(stringBuilder.ToString(), 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B461 RID: 46177 RVA: 0x0041CA0C File Offset: 0x0041AC0C
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Charge = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B462 RID: 46178 RVA: 0x0041CA4F File Offset: 0x0041AC4F
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.ActiveTurn = XRLCore.Core.Game.Turns + 1L;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B463 RID: 46179 RVA: 0x0041CA70 File Offset: 0x0041AC70
		public override bool HandleEvent(ZoneDeactivatedEvent E)
		{
			this.ActiveTurn = XRLCore.Core.Game.Turns + 1L;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B464 RID: 46180 RVA: 0x0041CA91 File Offset: 0x0041AC91
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			this.ActiveTurn = XRLCore.Core.Game.Turns + 1L;
			return base.HandleEvent(E);
		}

		// Token: 0x040048AA RID: 18602
		public int MaxCharge;

		// Token: 0x040048AB RID: 18603
		public string ChargeDisplayStyle;

		// Token: 0x040048AC RID: 18604
		public string AltChargeDisplayStyle;

		// Token: 0x040048AD RID: 18605
		public string AltChargeDisplayProperty = Scanning.GetScanPropertyName(Scanning.Scan.Tech);

		// Token: 0x040048AE RID: 18606
		public string Description = "circuitry";

		// Token: 0x040048AF RID: 18607
		public bool ConsiderLive = true;

		// Token: 0x040048B0 RID: 18608
		public long ActiveTurn;

		// Token: 0x040048B1 RID: 18609
		public int Charge;

		// Token: 0x040048B2 RID: 18610
		public int IncomingCharge;
	}
}
