using System;
using System.Text;
using XRL.Language;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020011C7 RID: 4551
	[Serializable]
	public class Flywheel : IPoweredPart
	{
		// Token: 0x0600B4E5 RID: 46309 RVA: 0x0042056C File Offset: 0x0041E76C
		public Flywheel()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B4E6 RID: 46310 RVA: 0x00420609 File Offset: 0x0041E809
		public bool HasCharge(int Amount)
		{
			return this.Charge >= Amount;
		}

		// Token: 0x0600B4E7 RID: 46311 RVA: 0x00420617 File Offset: 0x0041E817
		public int GetCharge()
		{
			return this.Charge;
		}

		// Token: 0x0600B4E8 RID: 46312 RVA: 0x0042061F File Offset: 0x0041E81F
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

		// Token: 0x0600B4E9 RID: 46313 RVA: 0x0042065A File Offset: 0x0041E85A
		public void AddCharge(int Amount)
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

		// Token: 0x0600B4EA RID: 46314 RVA: 0x00420695 File Offset: 0x0041E895
		public int GetChargeLevel()
		{
			return EnergyStorage.GetChargeLevel(this.Charge, this.MaxCharge);
		}

		// Token: 0x0600B4EB RID: 46315 RVA: 0x004206A8 File Offset: 0x0041E8A8
		public bool UseAltChargeDisplayStyle()
		{
			return IComponent<GameObject>.ThePlayer != null && !string.IsNullOrEmpty(this.AltChargeDisplayProperty) && IComponent<GameObject>.ThePlayer.GetIntProperty(this.AltChargeDisplayProperty, 0) > 0;
		}

		// Token: 0x0600B4EC RID: 46316 RVA: 0x004206DC File Offset: 0x0041E8DC
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

		// Token: 0x0600B4ED RID: 46317 RVA: 0x00420748 File Offset: 0x0041E948
		public override bool SameAs(IPart p)
		{
			Flywheel flywheel = p as Flywheel;
			return flywheel.Charge == this.Charge && flywheel.ChargeRate == this.ChargeRate && flywheel.MaxCharge == this.MaxCharge && flywheel.MinimumChargeToExplode == this.MinimumChargeToExplode && !(flywheel.Description != this.Description) && !(flywheel.ChargeDisplayStyle != this.ChargeDisplayStyle) && !(flywheel.AltChargeDisplayStyle != this.AltChargeDisplayStyle) && !(flywheel.AltChargeDisplayProperty != this.AltChargeDisplayProperty) && !(flywheel.SavesModified != this.SavesModified) && !(flywheel.StartCharge != this.StartCharge) && flywheel.SaveDivisor == this.SaveDivisor && flywheel.CatastrophicDisable == this.CatastrophicDisable && flywheel.ChargeLossDisable == this.ChargeLossDisable && flywheel.SavesAcknowledgeDisable == this.SavesAcknowledgeDisable && flywheel.ConsiderLive == this.ConsiderLive && base.SameAs(p);
		}

		// Token: 0x0600B4EE RID: 46318 RVA: 0x00420874 File Offset: 0x0041EA74
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ChargeAvailableEvent.ID || ID == EffectAppliedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || (ID == PooledEvent<GetElectricalConductivityEvent>.ID && this.ConsiderLive) || ID == ModifyDefendingSaveEvent.ID || ID == ObjectCreatedEvent.ID || ID == QueryChargeEvent.ID || ID == QueryChargeStorageEvent.ID || ID == TestChargeEvent.ID || ID == UseChargeEvent.ID;
		}

		// Token: 0x0600B4EF RID: 46319 RVA: 0x004208E5 File Offset: 0x0041EAE5
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject && this.ConsiderLive)
			{
				E.MinValue(95);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4F0 RID: 46320 RVA: 0x00420918 File Offset: 0x0041EB18
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

		// Token: 0x0600B4F1 RID: 46321 RVA: 0x00420990 File Offset: 0x0041EB90
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

		// Token: 0x0600B4F2 RID: 46322 RVA: 0x00420A1C File Offset: 0x0041EC1C
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

		// Token: 0x0600B4F3 RID: 46323 RVA: 0x00420ABC File Offset: 0x0041ECBC
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

		// Token: 0x0600B4F4 RID: 46324 RVA: 0x00420B74 File Offset: 0x0041ED74
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

		// Token: 0x0600B4F5 RID: 46325 RVA: 0x00420BD0 File Offset: 0x0041EDD0
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

		// Token: 0x0600B4F6 RID: 46326 RVA: 0x00420CB0 File Offset: 0x0041EEB0
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

		// Token: 0x0600B4F7 RID: 46327 RVA: 0x00420D2D File Offset: 0x0041EF2D
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (!string.IsNullOrEmpty(this.StartCharge))
			{
				this.Charge = this.StartCharge.RollCached();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4F8 RID: 46328 RVA: 0x00420D54 File Offset: 0x0041EF54
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (!string.IsNullOrEmpty(this.SavesModified) && this.IsObjectActivePartSubject(E.Defender) && SavingThrows.Applicable(this.SavesModified, E))
			{
				int num = this.Charge / this.SaveDivisor;
				if (num != 0)
				{
					E.Roll += num;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4F9 RID: 46329 RVA: 0x00420DB0 File Offset: 0x0041EFB0
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600B4FA RID: 46330 RVA: 0x00420DB3 File Offset: 0x0041EFB3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDeathRemoval");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B4FB RID: 46331 RVA: 0x00420DC8 File Offset: 0x0041EFC8
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

		// Token: 0x040048EC RID: 18668
		public int Charge;

		// Token: 0x040048ED RID: 18669
		public int ChargeRate = 5;

		// Token: 0x040048EE RID: 18670
		public int MaxCharge = 10000;

		// Token: 0x040048EF RID: 18671
		public int MinimumChargeToExplode = 1000;

		// Token: 0x040048F0 RID: 18672
		public string Description = "flywheel";

		// Token: 0x040048F1 RID: 18673
		public string ChargeDisplayStyle = "kinetic";

		// Token: 0x040048F2 RID: 18674
		public string AltChargeDisplayStyle = "percentage";

		// Token: 0x040048F3 RID: 18675
		public string AltChargeDisplayProperty = Scanning.GetScanPropertyName(Scanning.Scan.Tech);

		// Token: 0x040048F4 RID: 18676
		public string SavesModified = "Slip,Knockdown";

		// Token: 0x040048F5 RID: 18677
		public string StartCharge;

		// Token: 0x040048F6 RID: 18678
		public int SaveDivisor = 1000;

		// Token: 0x040048F7 RID: 18679
		public bool CatastrophicDisable = true;

		// Token: 0x040048F8 RID: 18680
		public bool ChargeLossDisable = true;

		// Token: 0x040048F9 RID: 18681
		public bool SavesAcknowledgeDisable;

		// Token: 0x040048FA RID: 18682
		public bool ConsiderLive;
	}
}
