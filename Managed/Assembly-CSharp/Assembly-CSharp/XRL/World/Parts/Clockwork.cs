using System;
using System.Text;
using XRL.Language;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020011BE RID: 4542
	[Serializable]
	public class Clockwork : IPoweredPart
	{
		// Token: 0x0600B465 RID: 46181 RVA: 0x0041CAB4 File Offset: 0x0041ACB4
		public Clockwork()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B466 RID: 46182 RVA: 0x0041CB31 File Offset: 0x0041AD31
		public bool HasCharge(int Amount)
		{
			return this.Charge >= Amount;
		}

		// Token: 0x0600B467 RID: 46183 RVA: 0x0041CB3F File Offset: 0x0041AD3F
		public int GetCharge()
		{
			return this.Charge;
		}

		// Token: 0x0600B468 RID: 46184 RVA: 0x0041CB47 File Offset: 0x0041AD47
		public Clockwork UseCharge(int Amount)
		{
			this.Charge -= Amount;
			if (this.Charge < 0)
			{
				this.Charge = 0;
			}
			else if (this.Charge > this.MaxCharge)
			{
				this.Charge = this.MaxCharge;
			}
			return this;
		}

		// Token: 0x0600B469 RID: 46185 RVA: 0x0041CB84 File Offset: 0x0041AD84
		public Clockwork AddCharge(int Amount)
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
			return this;
		}

		// Token: 0x0600B46A RID: 46186 RVA: 0x0041CBC1 File Offset: 0x0041ADC1
		public int GetChargeLevel()
		{
			return EnergyStorage.GetChargeLevel(this.Charge, this.MaxCharge);
		}

		// Token: 0x0600B46B RID: 46187 RVA: 0x0041CBD4 File Offset: 0x0041ADD4
		public bool UseAltChargeDisplayStyle()
		{
			return IComponent<GameObject>.ThePlayer != null && !string.IsNullOrEmpty(this.AltChargeDisplayProperty) && IComponent<GameObject>.ThePlayer.GetIntProperty(this.AltChargeDisplayProperty, 0) > 0;
		}

		// Token: 0x0600B46C RID: 46188 RVA: 0x0041CC08 File Offset: 0x0041AE08
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

		// Token: 0x0600B46D RID: 46189 RVA: 0x0041CC74 File Offset: 0x0041AE74
		public override bool SameAs(IPart p)
		{
			Clockwork clockwork = p as Clockwork;
			return clockwork.Charge == this.Charge && clockwork.ChargeRate == this.ChargeRate && clockwork.MaxCharge == this.MaxCharge && clockwork.MinimumChargeToExplode == this.MinimumChargeToExplode && !(clockwork.Description != this.Description) && !(clockwork.ChargeDisplayStyle != this.ChargeDisplayStyle) && !(clockwork.AltChargeDisplayStyle != this.AltChargeDisplayStyle) && !(clockwork.AltChargeDisplayProperty != this.AltChargeDisplayProperty) && !(clockwork.StartCharge != this.StartCharge) && clockwork.CatastrophicDisable == this.CatastrophicDisable && clockwork.ChargeLossDisable == this.ChargeLossDisable && clockwork.ConsiderLive == this.ConsiderLive && base.SameAs(p);
		}

		// Token: 0x0600B46E RID: 46190 RVA: 0x0041CD68 File Offset: 0x0041AF68
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ChargeAvailableEvent.ID || ID == EffectAppliedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || (ID == PooledEvent<GetElectricalConductivityEvent>.ID && this.ConsiderLive) || (ID == ObjectCreatedEvent.ID && !string.IsNullOrEmpty(this.StartCharge)) || ID == QueryChargeEvent.ID || ID == QueryChargeStorageEvent.ID || ID == TestChargeEvent.ID || ID == UseChargeEvent.ID;
		}

		// Token: 0x0600B46F RID: 46191 RVA: 0x0041CDDE File Offset: 0x0041AFDE
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject && this.ConsiderLive)
			{
				E.MinValue(95);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B470 RID: 46192 RVA: 0x0041CE10 File Offset: 0x0041B010
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

		// Token: 0x0600B471 RID: 46193 RVA: 0x0041CEF0 File Offset: 0x0041B0F0
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

		// Token: 0x0600B472 RID: 46194 RVA: 0x0041CF68 File Offset: 0x0041B168
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

		// Token: 0x0600B473 RID: 46195 RVA: 0x0041CFF4 File Offset: 0x0041B1F4
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

		// Token: 0x0600B474 RID: 46196 RVA: 0x0041D098 File Offset: 0x0041B298
		public override bool HandleEvent(ChargeAvailableEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && E.Amount > 0 && this.Charge < this.MaxCharge)
			{
				int num = E.Amount;
				if (!E.Forced && this.ChargeRate != 0)
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

		// Token: 0x0600B475 RID: 46197 RVA: 0x0041D150 File Offset: 0x0041B350
		public override bool HandleEvent(QueryChargeStorageEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.MaxCharge - this.GetCharge();
				if (num > 0)
				{
					E.Amount += num;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B476 RID: 46198 RVA: 0x0041D1AC File Offset: 0x0041B3AC
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

		// Token: 0x0600B477 RID: 46199 RVA: 0x0041D229 File Offset: 0x0041B429
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (!string.IsNullOrEmpty(this.StartCharge))
			{
				this.Charge = this.StartCharge.RollCached();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B478 RID: 46200 RVA: 0x0041D250 File Offset: 0x0041B450
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDeathRemoval");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B479 RID: 46201 RVA: 0x0041D268 File Offset: 0x0041B468
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeathRemoval" && this.MinimumChargeToExplode > 0 && this.Charge >= this.MinimumChargeToExplode && (this.CatastrophicDisable || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)))
			{
				if (base.Visible())
				{
					IComponent<GameObject>.AddPlayerMessage(Grammar.MakePossessive(this.ParentObject.The + this.ParentObject.ShortDisplayName) + " " + this.Description + " tears itself apart!", 'R', true);
				}
				this.ParentObject.Explode(this.Charge, this.ParentObject.IsCombat ? this.ParentObject : E.GetGameObjectParameter("Killer"), null, 1f, false, true, false, 0, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x040048B3 RID: 18611
		public int Charge;

		// Token: 0x040048B4 RID: 18612
		public int ChargeRate = 1;

		// Token: 0x040048B5 RID: 18613
		public int MaxCharge = 100;

		// Token: 0x040048B6 RID: 18614
		public int MinimumChargeToExplode = 1000;

		// Token: 0x040048B7 RID: 18615
		public string Description = "clockwork";

		// Token: 0x040048B8 RID: 18616
		public string ChargeDisplayStyle = "clockwork";

		// Token: 0x040048B9 RID: 18617
		public string AltChargeDisplayStyle = "percentage";

		// Token: 0x040048BA RID: 18618
		public string AltChargeDisplayProperty = Scanning.GetScanPropertyName(Scanning.Scan.Structure);

		// Token: 0x040048BB RID: 18619
		public string StartCharge;

		// Token: 0x040048BC RID: 18620
		public bool CatastrophicDisable;

		// Token: 0x040048BD RID: 18621
		public bool ChargeLossDisable = true;

		// Token: 0x040048BE RID: 18622
		public bool ConsiderLive;
	}
}
