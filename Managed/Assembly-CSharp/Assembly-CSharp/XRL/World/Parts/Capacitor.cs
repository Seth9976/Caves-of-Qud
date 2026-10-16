using System;
using System.Text;
using XRL.World.Capabilities;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x020011BB RID: 4539
	[Serializable]
	public class Capacitor : IRechargeable
	{
		// Token: 0x0600B42B RID: 46123 RVA: 0x0041B7D4 File Offset: 0x004199D4
		public Capacitor()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
		}

		// Token: 0x0600B42C RID: 46124 RVA: 0x0041B854 File Offset: 0x00419A54
		public bool HasCharge(int Amount)
		{
			return this.Charge >= Amount;
		}

		// Token: 0x0600B42D RID: 46125 RVA: 0x0041B862 File Offset: 0x00419A62
		public int GetCharge()
		{
			return this.Charge;
		}

		// Token: 0x0600B42E RID: 46126 RVA: 0x0041B86A File Offset: 0x00419A6A
		public void UseCharge(int Amount)
		{
			this.Charge -= Amount;
			if (this.Charge < 0)
			{
				this.Charge = 0;
				return;
			}
			if (this.Charge > this.MaxCharge)
			{
				this.Charge = this.MaxCharge;
			}
		}

		// Token: 0x0600B42F RID: 46127 RVA: 0x0041B8A5 File Offset: 0x00419AA5
		public override void AddCharge(int Amount)
		{
			this.Charge += Amount;
			if (this.Charge > this.MaxCharge)
			{
				this.Charge = this.MaxCharge;
				return;
			}
			if (this.Charge < 0)
			{
				this.Charge = 0;
			}
		}

		// Token: 0x0600B430 RID: 46128 RVA: 0x0041B8E0 File Offset: 0x00419AE0
		public override bool CanBeRecharged()
		{
			return this.IsRechargeable && base.IsReady(false, true, true, false, false, false, false, true, true, false, 1, null, false, 0L, null);
		}

		// Token: 0x0600B431 RID: 46129 RVA: 0x0041B91D File Offset: 0x00419B1D
		public int GetChargeLevel()
		{
			return EnergyStorage.GetChargeLevel(this.Charge, this.MaxCharge);
		}

		// Token: 0x0600B432 RID: 46130 RVA: 0x0041B930 File Offset: 0x00419B30
		public bool UseAltChargeDisplayStyle()
		{
			return The.Player != null && !string.IsNullOrEmpty(this.AltChargeDisplayProperty) && The.Player.GetIntProperty(this.AltChargeDisplayProperty, 0) > 0;
		}

		// Token: 0x0600B433 RID: 46131 RVA: 0x0041B964 File Offset: 0x00419B64
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
			return EnergyStorage.GetChargeStatus(this.Charge, this.MaxCharge, text);
		}

		// Token: 0x0600B434 RID: 46132 RVA: 0x0041B9CE File Offset: 0x00419BCE
		public override int GetRechargeAmount()
		{
			return this.MaxCharge - this.Charge;
		}

		// Token: 0x0600B435 RID: 46133 RVA: 0x0041B9E0 File Offset: 0x00419BE0
		public override bool SameAs(IPart p)
		{
			Capacitor capacitor = p as Capacitor;
			return capacitor.Charge == this.Charge && capacitor.ChargeRate == this.ChargeRate && capacitor.MaxCharge == this.MaxCharge && capacitor.MinimumChargeToExplode == this.MinimumChargeToExplode && !(capacitor.Description != this.Description) && !(capacitor.ChargeDisplayStyle != this.ChargeDisplayStyle) && !(capacitor.AltChargeDisplayStyle != this.AltChargeDisplayStyle) && !(capacitor.AltChargeDisplayProperty != this.AltChargeDisplayProperty) && !(capacitor.StartCharge != this.StartCharge) && capacitor.CatastrophicDisable == this.CatastrophicDisable && capacitor.ChargeLossDisable == this.ChargeLossDisable && capacitor.IsRechargeable == this.IsRechargeable && capacitor.ConsiderLive == this.ConsiderLive && base.SameAs(p);
		}

		// Token: 0x0600B436 RID: 46134 RVA: 0x0041BAE4 File Offset: 0x00419CE4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID || ID == ChargeAvailableEvent.ID || ID == EffectAppliedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || (ID == PooledEvent<GetElectricalConductivityEvent>.ID && this.ConsiderLive) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || (ID == ObjectCreatedEvent.ID && !string.IsNullOrEmpty(this.StartCharge)) || ID == QueryChargeEvent.ID || ID == QueryChargeStorageEvent.ID || ID == QueryRechargeStorageEvent.ID || ID == RechargeAvailableEvent.ID || ID == TestChargeEvent.ID || ID == UseChargeEvent.ID;
		}

		// Token: 0x0600B437 RID: 46135 RVA: 0x0041BB85 File Offset: 0x00419D85
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject && this.ConsiderLive)
			{
				E.MinValue(95);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B438 RID: 46136 RVA: 0x0041BBB8 File Offset: 0x00419DB8
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if ((this.CatastrophicDisable || this.ChargeLossDisable) && base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.CatastrophicDisable && this.MinimumChargeToExplode > 0 && this.Charge >= this.MinimumChargeToExplode)
				{
					this.ParentObject.Die(null, "catastrophic " + this.Description + " failure", "Your " + this.Description + " failed catastrophically.", this.ParentObject.Its + " " + this.Description + " failed catastrophically.", true, null, null, false, false, null, null, null);
				}
				if (this.ChargeLossDisable)
				{
					this.Charge = 0;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B439 RID: 46137 RVA: 0x0041BC98 File Offset: 0x00419E98
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference && E.Understood())
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

		// Token: 0x0600B43A RID: 46138 RVA: 0x0041BD0C File Offset: 0x00419F0C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.CanBeRecharged() && IComponent<GameObject>.ThePlayer.HasSkill("Tinkering_Tinker1") && this.Charge < this.MaxCharge && this.ParentObject.Understood() && The.Player.CanMoveExtremities("Recharge", false, false, false))
			{
				E.AddAction("Recharge", "recharge", "RechargeCapacitor", null, 'R', false, 100 - this.Charge * 100 / this.MaxCharge, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B43B RID: 46139 RVA: 0x0041BD9D File Offset: 0x00419F9D
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "RechargeCapacitor" && this.CanBeRecharged())
			{
				E.Actor.GetPart<Tinkering_Tinker1>().Recharge(this.ParentObject, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B43C RID: 46140 RVA: 0x0041BDD8 File Offset: 0x00419FD8
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (!this.StartCharge.IsNullOrEmpty())
			{
				this.Charge = this.StartCharge.RollCached();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B43D RID: 46141 RVA: 0x0041BDFF File Offset: 0x00419FFF
		public override bool HandleEvent(ChargeAvailableEvent E)
		{
			this.Process(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B43E RID: 46142 RVA: 0x0041BE0F File Offset: 0x0041A00F
		public override bool HandleEvent(RechargeAvailableEvent E)
		{
			this.Process(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B43F RID: 46143 RVA: 0x0041BE20 File Offset: 0x0041A020
		public override bool HandleEvent(QueryChargeEvent E)
		{
			if (this.InteractWithChargeEvent(E))
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

		// Token: 0x0600B440 RID: 46144 RVA: 0x0041BE68 File Offset: 0x0041A068
		public override bool HandleEvent(TestChargeEvent E)
		{
			if (this.InteractWithChargeEvent(E))
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

		// Token: 0x0600B441 RID: 46145 RVA: 0x0041BEC4 File Offset: 0x0041A0C4
		public override bool HandleEvent(UseChargeEvent E)
		{
			if (E.Pass == 2 && this.InteractWithChargeEvent(E))
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

		// Token: 0x0600B442 RID: 46146 RVA: 0x0041BF32 File Offset: 0x0041A132
		public override bool HandleEvent(QueryChargeStorageEvent E)
		{
			this.Process(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B443 RID: 46147 RVA: 0x0041BF42 File Offset: 0x0041A142
		public override bool HandleEvent(QueryRechargeStorageEvent E)
		{
			this.Process(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B444 RID: 46148 RVA: 0x0041BF54 File Offset: 0x0041A154
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (this.MinimumChargeToExplode > 0 && this.Charge >= this.MinimumChargeToExplode && (this.CatastrophicDisable || base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)))
			{
				base.DidX("explode", null, "!", null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
				this.ParentObject.Explode(this.Charge, this.ParentObject.IsCombat ? this.ParentObject : E.Killer, null, 1f, false, true, false, 0, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B445 RID: 46149 RVA: 0x0041C011 File Offset: 0x0041A211
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B446 RID: 46150 RVA: 0x0041C014 File Offset: 0x0041A214
		public virtual bool InteractWithChargeEvent(IChargeEvent E)
		{
			return (!E.LiveOnly || this.ConsiderLive) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
		}

		// Token: 0x0600B447 RID: 46151 RVA: 0x0041C05C File Offset: 0x0041A25C
		private void Process(IInitialChargeProductionEvent E)
		{
			if (this.Charge < this.MaxCharge && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && E.Amount > 0)
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
		}

		// Token: 0x0600B448 RID: 46152 RVA: 0x0041C110 File Offset: 0x0041A310
		private void Process(IChargeStorageEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.MaxCharge - this.GetCharge();
				if (num > 0)
				{
					E.Amount += num;
				}
			}
		}

		// Token: 0x0400489D RID: 18589
		public int Charge;

		// Token: 0x0400489E RID: 18590
		public int ChargeRate = 5;

		// Token: 0x0400489F RID: 18591
		public int MaxCharge = 10000;

		// Token: 0x040048A0 RID: 18592
		public int MinimumChargeToExplode = 1000;

		// Token: 0x040048A1 RID: 18593
		public string Description = "capacitor";

		// Token: 0x040048A2 RID: 18594
		public string ChargeDisplayStyle = "electrical";

		// Token: 0x040048A3 RID: 18595
		public string AltChargeDisplayStyle = "percentage";

		// Token: 0x040048A4 RID: 18596
		public string AltChargeDisplayProperty = Scanning.GetScanPropertyName(Scanning.Scan.Tech);

		// Token: 0x040048A5 RID: 18597
		public string StartCharge;

		// Token: 0x040048A6 RID: 18598
		public bool CatastrophicDisable;

		// Token: 0x040048A7 RID: 18599
		public bool ChargeLossDisable = true;

		// Token: 0x040048A8 RID: 18600
		public bool IsRechargeable = true;

		// Token: 0x040048A9 RID: 18601
		public bool ConsiderLive = true;
	}
}
