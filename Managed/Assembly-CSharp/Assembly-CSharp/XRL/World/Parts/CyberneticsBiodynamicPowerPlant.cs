using System;
using System.Text;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000F1A RID: 3866
	[Serializable]
	public class CyberneticsBiodynamicPowerPlant : IPoweredPart
	{
		// Token: 0x06009A18 RID: 39448 RVA: 0x0037EE5E File Offset: 0x0037D05E
		public CyberneticsBiodynamicPowerPlant()
		{
			this.WorksOnImplantee = true;
			this.IsBootSensitive = false;
			this.ChargeUse = 0;
			this.NameForStatus = "BiodynamicPowerPlant";
		}

		// Token: 0x06009A19 RID: 39449 RVA: 0x0037EE98 File Offset: 0x0037D098
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009A1A RID: 39450 RVA: 0x0037EE9B File Offset: 0x0037D09B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == EffectAppliedEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009A1B RID: 39451 RVA: 0x0037EED4 File Offset: 0x0037D0D4
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterPartEvent(this, "HasPowerConnectors");
			E.Implantee.RegisterPartEvent(this, "OwnerGetShortDescription");
			E.Implantee.RegisterPartEvent(this, "QueryCharge");
			E.Implantee.RegisterPartEvent(this, "QueryChargeProduction");
			E.Implantee.RegisterPartEvent(this, "TestCharge");
			E.Implantee.RegisterPartEvent(this, "UseCharge");
			return base.HandleEvent(E);
		}

		// Token: 0x06009A1C RID: 39452 RVA: 0x0037EF50 File Offset: 0x0037D150
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, "HasPowerConnectors");
			E.Implantee.UnregisterPartEvent(this, "OwnerGetShortDescription");
			E.Implantee.UnregisterPartEvent(this, "QueryCharge");
			E.Implantee.UnregisterPartEvent(this, "QueryChargeProduction");
			E.Implantee.UnregisterPartEvent(this, "TestCharge");
			E.Implantee.UnregisterPartEvent(this, "UseCharge");
			return base.HandleEvent(E);
		}

		// Token: 0x06009A1D RID: 39453 RVA: 0x0037EFCC File Offset: 0x0037D1CC
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (E.Object == this.ParentObject.Implantee)
			{
				this.LastCharge = this.Charge;
				this.Charge = 0;
				if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					this.AddCharge(this.ChargeRate);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A1E RID: 39454 RVA: 0x0037F03A File Offset: 0x0037D23A
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Current charge", this.Charge);
			E.AddEntry(this, "Charge left after last turn", this.LastCharge);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A1F RID: 39455 RVA: 0x0037F068 File Offset: 0x0037D268
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Charge = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A20 RID: 39456 RVA: 0x0037F0AB File Offset: 0x0037D2AB
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x06009A21 RID: 39457 RVA: 0x0037F0B0 File Offset: 0x0037D2B0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "HasPowerConnectors")
			{
				return false;
			}
			if (E.ID == "QueryCharge")
			{
				if ((this.ConsiderLive || !E.HasFlag("LiveOnly")) && E.HasFlag("IncludeTransient") && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					int num = this.GetCharge() - this.ChargeUse;
					if (num > 0)
					{
						E.SetParameter("Charge", E.GetIntParameter("Charge", 0) + num);
					}
				}
			}
			else if (E.ID == "TestCharge")
			{
				if ((this.ConsiderLive || !E.HasFlag("LiveOnly")) && E.HasFlag("IncludeTransient") && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					int intParameter = E.GetIntParameter("Charge", 0);
					int num2 = Math.Min(intParameter, this.GetCharge() - this.ChargeUse);
					if (num2 > 0)
					{
						E.SetParameter("Charge", intParameter - num2);
						if (num2 >= intParameter)
						{
							return false;
						}
					}
				}
			}
			else if (E.ID == "UseCharge")
			{
				if ((this.ConsiderLive || !E.HasFlag("LiveOnly")) && E.HasFlag("IncludeTransient") && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					int intParameter2 = E.GetIntParameter("Charge", 0);
					int num3 = Math.Min(intParameter2, this.GetCharge() - this.ChargeUse);
					if (num3 > 0)
					{
						E.SetParameter("Charge", intParameter2 - num3);
						this.UseCharge(num3 + this.ChargeUse);
						if (num3 >= intParameter2)
						{
							return false;
						}
					}
				}
			}
			else if (E.ID == "OwnerGetShortDescription")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				IntegratedPowerSystems part = gameObjectParameter.GetPart<IntegratedPowerSystems>();
				if (part != null && part.RequiresEvent == "HasPowerConnectors" && !gameObjectParameter.HasPart<ModJacked>())
				{
					string text;
					if (gameObjectParameter.UseBareIndicative)
					{
						text = (gameObjectParameter.IsPlural ? "these devices" : "this device");
					}
					else
					{
						text = gameObjectParameter.it;
					}
					StringBuilder stringBuilder = E.GetParameter("PostfixBuilder") as StringBuilder;
					if (stringBuilder != null)
					{
						stringBuilder.AppendRules(string.Concat(new string[]
						{
							"Integrated power systems: When equipped, ",
							text,
							" can be powered by ",
							this.ParentObject.HasProperName ? this.ParentObject.DisplayNameOnlyStripped : ("your " + this.ParentObject.DisplayNameOnlyDirectAndStripped),
							"."
						}));
					}
				}
			}
			else if (E.ID == "QueryChargeProduction" && (this.ConsiderLive || !E.HasFlag("LiveOnly")) && E.HasFlag("IncludeTransient") && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.SetParameter("Charge", E.GetIntParameter("Charge", 0) + this.ChargeRate);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009A22 RID: 39458 RVA: 0x0037F452 File Offset: 0x0037D652
		public bool HasCharge(int Amount)
		{
			return this.Charge >= Amount;
		}

		// Token: 0x06009A23 RID: 39459 RVA: 0x0037F460 File Offset: 0x0037D660
		public int GetCharge()
		{
			return this.Charge;
		}

		// Token: 0x06009A24 RID: 39460 RVA: 0x0037F468 File Offset: 0x0037D668
		public void UseCharge(int Amount)
		{
			this.Charge -= Amount;
			if (this.Charge < 0)
			{
				this.Charge = 0;
			}
		}

		// Token: 0x06009A25 RID: 39461 RVA: 0x0037F488 File Offset: 0x0037D688
		public void AddCharge(int Amount)
		{
			this.Charge += Amount;
			if (this.Charge < 0)
			{
				this.Charge = 0;
			}
		}

		// Token: 0x06009A26 RID: 39462 RVA: 0x0037F4A8 File Offset: 0x0037D6A8
		public int GetChargeLevel()
		{
			return EnergyStorage.GetChargeLevel(this.GetCharge(), this.ChargeRate);
		}

		// Token: 0x04004030 RID: 16432
		public int ChargeRate = 5000;

		// Token: 0x04004031 RID: 16433
		public bool ConsiderLive = true;

		// Token: 0x04004032 RID: 16434
		public int Charge;

		// Token: 0x04004033 RID: 16435
		public int LastCharge;
	}
}
