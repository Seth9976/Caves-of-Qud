using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020011D4 RID: 4564
	[Serializable]
	public class LiquidFueledEnergyCell : IEnergyCell
	{
		// Token: 0x0600B5BA RID: 46522 RVA: 0x004254A8 File Offset: 0x004236A8
		public LiquidVolume EnergyLiquid()
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume == null)
			{
				return null;
			}
			if (liquidVolume.Volume <= 0)
			{
				return null;
			}
			if (!liquidVolume.IsPureLiquid(this.Liquid, false))
			{
				return null;
			}
			return liquidVolume;
		}

		// Token: 0x0600B5BB RID: 46523 RVA: 0x004254E3 File Offset: 0x004236E3
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.EnergyLiquid() == null || base.GetActivePartLocallyDefinedFailure();
		}

		// Token: 0x0600B5BC RID: 46524 RVA: 0x004254F8 File Offset: 0x004236F8
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			if (this.EnergyLiquid() != null)
			{
				return base.GetActivePartLocallyDefinedFailureDescription();
			}
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume == null || liquidVolume.Volume <= 0)
			{
				return "ProcessInputMissing";
			}
			if (liquidVolume.ContainsLiquid(this.Liquid) && !liquidVolume.IsPureLiquid(false))
			{
				return "ProcessInputInvalid";
			}
			return "ProcessInputMissing";
		}

		// Token: 0x0600B5BD RID: 46525 RVA: 0x00425554 File Offset: 0x00423754
		public override bool HasAnyCharge()
		{
			return this.EnergyLiquid() != null;
		}

		// Token: 0x0600B5BE RID: 46526 RVA: 0x0042555F File Offset: 0x0042375F
		public override bool HasCharge(int Amount)
		{
			return this.GetCharge() >= Amount;
		}

		// Token: 0x0600B5BF RID: 46527 RVA: 0x00425570 File Offset: 0x00423770
		public override int GetCharge()
		{
			LiquidVolume liquidVolume = this.EnergyLiquid();
			if (liquidVolume == null)
			{
				return 0;
			}
			return liquidVolume.Volume * this.ChargePerDram - this.ChargeCounter;
		}

		// Token: 0x0600B5C0 RID: 46528 RVA: 0x004255A0 File Offset: 0x004237A0
		public override int GetChargePercentage()
		{
			LiquidVolume liquidVolume = this.EnergyLiquid();
			if (liquidVolume == null)
			{
				return 0;
			}
			return (liquidVolume.Volume * this.ChargePerDram - this.ChargeCounter) * 100 / (liquidVolume.MaxVolume * this.ChargePerDram);
		}

		// Token: 0x0600B5C1 RID: 46529 RVA: 0x004255E0 File Offset: 0x004237E0
		public override void UseCharge(int Amount)
		{
			LiquidVolume liquidVolume = this.EnergyLiquid();
			if (liquidVolume != null)
			{
				bool flag = liquidVolume.Volume > 0;
				this.ChargeCounter += Amount;
				while (this.ChargeCounter >= this.ChargePerDram)
				{
					liquidVolume.UseDram();
					if (liquidVolume.Volume == 0)
					{
						this.ChargeCounter = 0;
						break;
					}
					this.ChargeCounter -= this.ChargePerDram;
				}
				if (flag && liquidVolume.Volume == 0)
				{
					CellDepletedEvent.Send(this.ParentObject, this);
				}
			}
		}

		// Token: 0x0600B5C2 RID: 46530 RVA: 0x00425661 File Offset: 0x00423861
		public override void AddCharge(int Amount)
		{
			if (Amount < 0)
			{
				this.UseCharge(-Amount);
			}
		}

		// Token: 0x0600B5C3 RID: 46531 RVA: 0x00425670 File Offset: 0x00423870
		public override string ChargeStatus()
		{
			LiquidVolume liquidVolume = this.EnergyLiquid();
			if (liquidVolume == null)
			{
				return "&K-";
			}
			string primaryLiquidColor = liquidVolume.GetPrimaryLiquidColor();
			if (primaryLiquidColor != null)
			{
				return "&" + primaryLiquidColor + liquidVolume.Volume.ToString() + "&y";
			}
			return liquidVolume.Volume.ToString();
		}

		// Token: 0x0600B5C4 RID: 46532 RVA: 0x004256BE File Offset: 0x004238BE
		public override void TinkerInitialize()
		{
		}

		// Token: 0x0600B5C5 RID: 46533 RVA: 0x004256C0 File Offset: 0x004238C0
		public override void SetChargePercentage(int Percentage)
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			liquidVolume.Empty(false, false);
			if (liquidVolume.MaxVolume > 0)
			{
				int num = Math.Max(0, Math.Min(liquidVolume.MaxVolume * Percentage / 100, liquidVolume.MaxVolume));
				if (num > 0)
				{
					liquidVolume.Fill(this.Liquid, num);
				}
			}
		}

		// Token: 0x0600B5C6 RID: 46534 RVA: 0x00425718 File Offset: 0x00423918
		public override void RandomizeCharge()
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			liquidVolume.Empty(false, false);
			if (liquidVolume.MaxVolume > 0)
			{
				int num = Stat.Random(0, liquidVolume.MaxVolume);
				if (num > 0)
				{
					liquidVolume.Fill(this.Liquid, num);
				}
			}
		}

		// Token: 0x0600B5C7 RID: 46535 RVA: 0x00425760 File Offset: 0x00423960
		public override void MaximizeCharge()
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			liquidVolume.Empty(false, false);
			if (liquidVolume.MaxVolume > 0)
			{
				liquidVolume.Fill(this.Liquid, liquidVolume.MaxVolume);
			}
		}

		// Token: 0x0600B5C8 RID: 46536 RVA: 0x0042579C File Offset: 0x0042399C
		public override bool CanBeRecharged()
		{
			return false;
		}

		// Token: 0x0600B5C9 RID: 46537 RVA: 0x0042579F File Offset: 0x0042399F
		public override int GetRechargeAmount()
		{
			return 0;
		}

		// Token: 0x0600B5CA RID: 46538 RVA: 0x004257A4 File Offset: 0x004239A4
		public override bool SameAs(IPart p)
		{
			LiquidFueledEnergyCell liquidFueledEnergyCell = p as LiquidFueledEnergyCell;
			return !(liquidFueledEnergyCell.Liquid != this.Liquid) && liquidFueledEnergyCell.ChargePerDram == this.ChargePerDram && liquidFueledEnergyCell.ChargeCounter == this.ChargeCounter && liquidFueledEnergyCell.ConsiderLive == this.ConsiderLive && base.SameAs(p);
		}

		// Token: 0x0600B5CB RID: 46539 RVA: 0x00425804 File Offset: 0x00423A04
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AllowLiquidCollectionEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || (ID == PooledEvent<GetElectricalConductivityEvent>.ID && this.ConsiderLive) || ID == GetPreferredLiquidEvent.ID || ID == QueryChargeEvent.ID || ID == RemoveFromContextEvent.ID || ID == TestChargeEvent.ID || ID == TryRemoveFromContextEvent.ID || ID == UseChargeEvent.ID || ID == WantsLiquidCollectionEvent.ID || (Options.AutogetAmmo && ID == AutoexploreObjectEvent.ID);
		}

		// Token: 0x0600B5CC RID: 46540 RVA: 0x00425886 File Offset: 0x00423A86
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject && this.ConsiderLive)
			{
				E.MinValue(95);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5CD RID: 46541 RVA: 0x004258B6 File Offset: 0x00423AB6
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (E.Command == null && Options.AutogetAmmo)
			{
				E.Command = "Autoget";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5CE RID: 46542 RVA: 0x004258D9 File Offset: 0x00423AD9
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "ChargePerDram", this.ChargePerDram);
			E.AddEntry(this, "ChargeCounter", this.ChargeCounter);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5CF RID: 46543 RVA: 0x00425906 File Offset: 0x00423B06
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600B5D0 RID: 46544 RVA: 0x0042591F File Offset: 0x00423B1F
		public override bool HandleEvent(GetPreferredLiquidEvent E)
		{
			if (E.Liquid == null)
			{
				E.Liquid = this.Liquid;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5D1 RID: 46545 RVA: 0x0042593C File Offset: 0x00423B3C
		public override bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return !this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600B5D2 RID: 46546 RVA: 0x00425958 File Offset: 0x00423B58
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

		// Token: 0x0600B5D3 RID: 46547 RVA: 0x004259D0 File Offset: 0x00423BD0
		public override bool HandleEvent(TestChargeEvent E)
		{
			if ((!E.LiveOnly || this.ConsiderLive) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.ChargeUse * E.Multiple;
				int num2 = this.GetCharge() - num;
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

		// Token: 0x0600B5D4 RID: 46548 RVA: 0x00425A50 File Offset: 0x00423C50
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

		// Token: 0x0600B5D5 RID: 46549 RVA: 0x00425AF0 File Offset: 0x00423CF0
		public bool IsLiquidCollectionCompatible(string LiquidType)
		{
			return this.Liquid == LiquidType;
		}

		// Token: 0x04004950 RID: 18768
		public string Liquid = "water";

		// Token: 0x04004951 RID: 18769
		public int ChargePerDram = 10000;

		// Token: 0x04004952 RID: 18770
		public int ChargeCounter;

		// Token: 0x04004953 RID: 18771
		public bool ConsiderLive;
	}
}
