using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020011D5 RID: 4565
	[Serializable]
	public class LiquidFueledPowerPlant : IPoweredPart
	{
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600B5D7 RID: 46551 RVA: 0x00425B1C File Offset: 0x00423D1C
		public Dictionary<string, int> LiquidMap
		{
			get
			{
				if (this._LiquidMap == null && !string.IsNullOrEmpty(this.Liquids))
				{
					this._LiquidMap = this.Liquids.CachedNumericDictionaryExpansion();
				}
				return this._LiquidMap;
			}
		}

		// Token: 0x0600B5D8 RID: 46552 RVA: 0x00425B4A File Offset: 0x00423D4A
		public LiquidFueledPowerPlant()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B5D9 RID: 46553 RVA: 0x00425B7C File Offset: 0x00423D7C
		public override void Initialize()
		{
			base.Initialize();
			if (this.Liquid == null)
			{
				if (this.LiquidMap != null && this.LiquidMap.Count > 0)
				{
					KeyValuePair<string, int> keyValuePair = this.LiquidMap.First<KeyValuePair<string, int>>();
					this.Liquid = keyValuePair.Key;
					this.ChargePerDram = keyValuePair.Value;
					return;
				}
				this.Liquid = "water";
			}
		}

		// Token: 0x0600B5DA RID: 46554 RVA: 0x00425BDF File Offset: 0x00423DDF
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.EnergyLiquid() == null || base.GetActivePartLocallyDefinedFailure();
		}

		// Token: 0x0600B5DB RID: 46555 RVA: 0x00425BF4 File Offset: 0x00423DF4
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
			if (!liquidVolume.ContainsLiquid(this.Liquid))
			{
				string primaryLiquidID = liquidVolume.GetPrimaryLiquidID();
				int num;
				if (this.LiquidMap != null && this.LiquidMap.TryGetValue(primaryLiquidID, out num))
				{
					this.Liquid = primaryLiquidID;
					this.ChargePerDram = num;
				}
			}
			if (liquidVolume.ContainsLiquid(this.Liquid) && !liquidVolume.IsPureLiquid(false))
			{
				return "ProcessInputInvalid";
			}
			return "ProcessInputMissing";
		}

		// Token: 0x0600B5DC RID: 46556 RVA: 0x00425C8C File Offset: 0x00423E8C
		public LiquidVolume EnergyLiquid()
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume == null)
			{
				return null;
			}
			if (!this.IsValidLiquid(liquidVolume))
			{
				return null;
			}
			if (liquidVolume.Volume <= 0)
			{
				return null;
			}
			return liquidVolume;
		}

		// Token: 0x0600B5DD RID: 46557 RVA: 0x00425CC4 File Offset: 0x00423EC4
		private bool IsValidLiquid(LiquidVolume Volume)
		{
			if (!Volume.IsPureLiquid(false))
			{
				return false;
			}
			if (!Volume.ContainsLiquid(this.Liquid))
			{
				string primaryLiquidID = Volume.GetPrimaryLiquidID();
				int num;
				if (this.LiquidMap == null || !this.LiquidMap.TryGetValue(primaryLiquidID, out num))
				{
					return false;
				}
				this.Liquid = primaryLiquidID;
				this.ChargePerDram = num;
			}
			return true;
		}

		// Token: 0x0600B5DE RID: 46558 RVA: 0x00425D1C File Offset: 0x00423F1C
		public bool HasCharge(int Amount)
		{
			return this.GetCharge() >= Amount;
		}

		// Token: 0x0600B5DF RID: 46559 RVA: 0x00425D2C File Offset: 0x00423F2C
		public int GetCharge()
		{
			LiquidVolume liquidVolume = this.EnergyLiquid();
			if (liquidVolume == null)
			{
				return 0;
			}
			return liquidVolume.Volume * this.ChargePerDram - this.ChargeCounter;
		}

		// Token: 0x0600B5E0 RID: 46560 RVA: 0x00425D5C File Offset: 0x00423F5C
		public LiquidFueledPowerPlant UseCharge(int Amount)
		{
			LiquidVolume liquidVolume = this.EnergyLiquid();
			if (liquidVolume != null)
			{
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
			}
			return this;
		}

		// Token: 0x0600B5E1 RID: 46561 RVA: 0x00425DBD File Offset: 0x00423FBD
		public LiquidFueledPowerPlant AddCharge(int Amount)
		{
			if (Amount < 0)
			{
				this.UseCharge(-Amount);
			}
			return this;
		}

		// Token: 0x0600B5E2 RID: 46562 RVA: 0x00425DD0 File Offset: 0x00423FD0
		public string ChargeStatus()
		{
			LiquidVolume liquidVolume = this.EnergyLiquid();
			if (liquidVolume == null)
			{
				return "{{K|-}}";
			}
			string primaryLiquidColor = liquidVolume.GetPrimaryLiquidColor();
			if (primaryLiquidColor != null)
			{
				return string.Concat(new string[]
				{
					"{{",
					primaryLiquidColor,
					"|",
					liquidVolume.Volume.ToString(),
					"}}"
				});
			}
			return liquidVolume.Volume.ToString();
		}

		// Token: 0x0600B5E3 RID: 46563 RVA: 0x00425E38 File Offset: 0x00424038
		public override bool SameAs(IPart p)
		{
			LiquidFueledPowerPlant liquidFueledPowerPlant = p as LiquidFueledPowerPlant;
			return !(liquidFueledPowerPlant.Liquid != this.Liquid) && liquidFueledPowerPlant.ChargePerDram == this.ChargePerDram && liquidFueledPowerPlant.ChargeCounter == this.ChargeCounter && liquidFueledPowerPlant.ChargeRate == this.ChargeRate && liquidFueledPowerPlant.ConsiderLive == this.ConsiderLive && base.SameAs(p);
		}

		// Token: 0x0600B5E4 RID: 46564 RVA: 0x00425EA8 File Offset: 0x004240A8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (this.ChancePerRoundToEmptyOneIn > 0 && ID == SingletonEvent<EndTurnEvent>.ID) || ID == AllowLiquidCollectionEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || (ID == PooledEvent<GetElectricalConductivityEvent>.ID && this.ConsiderLive) || ID == GetPreferredLiquidEvent.ID || ID == PooledEvent<LiquidMixedEvent>.ID || ID == QueryChargeEvent.ID || ID == QueryChargeProductionEvent.ID || ID == PrimePowerSystemsEvent.ID || ID == TestChargeEvent.ID || ID == UseChargeEvent.ID || ID == WantsLiquidCollectionEvent.ID;
		}

		// Token: 0x0600B5E5 RID: 46565 RVA: 0x00425F32 File Offset: 0x00424132
		public override bool HandleEvent(PrimePowerSystemsEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("Furniture"))
			{
				this.ProduceCharge(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5E6 RID: 46566 RVA: 0x00425F54 File Offset: 0x00424154
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "ChargePerDram", this.ChargePerDram);
			E.AddEntry(this, "ChargeCounter", this.ChargeCounter);
			E.AddEntry(this, "ChargeRate", this.ChargeRate);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5E7 RID: 46567 RVA: 0x00425F93 File Offset: 0x00424193
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject && this.ConsiderLive)
			{
				E.MinValue(95);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5E8 RID: 46568 RVA: 0x00425FC4 File Offset: 0x004241C4
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.ChancePerRoundToEmptyOneIn > 0 && Stat.Random(1, this.ChancePerRoundToEmptyOneIn) == 1 && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				LiquidVolume part = this.ParentObject.GetPart<LiquidVolume>();
				if (this.ParentObject.EquippedOn() != null && this.ParentObject.EquippedOn().ParentBody.IsPlayer())
				{
					Popup.Show(string.Concat(new string[]
					{
						"Your ",
						this.ParentObject.DisplayNameOnlyDirect,
						this.ParentObject.GetVerb("have", true, false, false, true, false),
						" consumed all of ",
						this.ParentObject.GetPronounProvider(false).PossessiveAdjective,
						" ",
						part.GetLiquidName(),
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				part.Empty(false, false);
			}
			return true;
		}

		// Token: 0x0600B5E9 RID: 46569 RVA: 0x004260D4 File Offset: 0x004242D4
		public override bool HandleEvent(LiquidMixedEvent E)
		{
			if (this.EnergyLiquid() != null)
			{
				this.ParentObject.FireEvent(LiquidFueledPowerPlant.eLiquidFueledPowerPlantFueled);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5EA RID: 46570 RVA: 0x004260F6 File Offset: 0x004242F6
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600B5EB RID: 46571 RVA: 0x0042610F File Offset: 0x0042430F
		public override bool HandleEvent(GetPreferredLiquidEvent E)
		{
			if (E.Liquid == null && !string.IsNullOrEmpty(this.Liquid))
			{
				E.Liquid = this.Liquid;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5EC RID: 46572 RVA: 0x00426139 File Offset: 0x00424339
		public override bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return !this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600B5ED RID: 46573 RVA: 0x00426154 File Offset: 0x00424354
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

		// Token: 0x0600B5EE RID: 46574 RVA: 0x004261CC File Offset: 0x004243CC
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

		// Token: 0x0600B5EF RID: 46575 RVA: 0x0042624C File Offset: 0x0042444C
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

		// Token: 0x0600B5F0 RID: 46576 RVA: 0x004262F0 File Offset: 0x004244F0
		public override bool HandleEvent(QueryChargeProductionEvent E)
		{
			if (this.ChargeRate > this.ChargeUse && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = Math.Min(this.ChargeRate * E.Multiple, this.GetCharge()) - this.ChargeUse * E.Multiple;
				if (num > 0)
				{
					E.Amount += num;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5F1 RID: 46577 RVA: 0x00426373 File Offset: 0x00424573
		public bool IsLiquidCollectionCompatible(string LiquidType)
		{
			return this.Liquid == LiquidType;
		}

		// Token: 0x0600B5F2 RID: 46578 RVA: 0x00426381 File Offset: 0x00424581
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B5F3 RID: 46579 RVA: 0x00426384 File Offset: 0x00424584
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProduceCharge(Amount);
		}

		// Token: 0x0600B5F4 RID: 46580 RVA: 0x00426390 File Offset: 0x00424590
		public void ProduceCharge(int Turns = 1)
		{
			if (this.ChargeRate > this.ChargeUse && base.IsReady(false, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				int num = Math.Min(this.ChargeRate, this.GetCharge()) - this.ChargeUse;
				if (num > 0)
				{
					for (int i = 0; i < Turns; i++)
					{
						this.UseCharge(this.ParentObject.ChargeAvailable(num, 0L, 1, false, null));
					}
				}
			}
		}

		// Token: 0x04004954 RID: 18772
		public string Liquid;

		// Token: 0x04004955 RID: 18773
		public string Liquids;

		// Token: 0x04004956 RID: 18774
		public int ChargePerDram = 10000;

		// Token: 0x04004957 RID: 18775
		public int ChargeCounter;

		// Token: 0x04004958 RID: 18776
		public int ChargeRate;

		// Token: 0x04004959 RID: 18777
		public bool ConsiderLive;

		// Token: 0x0400495A RID: 18778
		public int ChancePerRoundToEmptyOneIn;

		// Token: 0x0400495B RID: 18779
		[NonSerialized]
		public static Event eLiquidFueledPowerPlantFueled = new ImmutableEvent("LiquidFueledPowerPlantFueled");

		// Token: 0x0400495C RID: 18780
		[NonSerialized]
		private Dictionary<string, int> _LiquidMap;
	}
}
