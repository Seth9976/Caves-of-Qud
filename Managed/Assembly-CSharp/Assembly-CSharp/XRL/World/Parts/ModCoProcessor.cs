using System;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the item's tier for purposes of calculating
	/// bonuses is increased by the standard power load bonus, i.e. 2 for
	/// the standard overload power load of 400.
	/// </remarks>
	// Token: 0x0200112D RID: 4397
	[Serializable]
	public class ModCoProcessor : IModification
	{
		// Token: 0x0600ADFB RID: 44539 RVA: 0x0040565D File Offset: 0x0040385D
		public ModCoProcessor()
		{
		}

		// Token: 0x0600ADFC RID: 44540 RVA: 0x00405686 File Offset: 0x00403886
		public ModCoProcessor(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600ADFD RID: 44541 RVA: 0x004056B0 File Offset: 0x004038B0
		public override void Configure()
		{
			this.ChargeUse = 1;
			this.WorksOnWearer = true;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			this.IsPowerLoadSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "CoProcessor";
		}

		// Token: 0x0600ADFE RID: 44542 RVA: 0x004056E8 File Offset: 0x004038E8
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Head", null, null, null, false, true, true, false);
		}

		// Token: 0x0600ADFF RID: 44543 RVA: 0x00405708 File Offset: 0x00403908
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<EnergyCellSocket>(false);
			int num = Math.Max((this.Tier >= 5) ? (10 - this.Tier) : (this.Tier + 2), 2);
			BootSequence part = Object.GetPart<BootSequence>();
			if (part == null)
			{
				Object.AddPart<BootSequence>(new BootSequence
				{
					BootTime = num,
					ReadoutInName = true,
					ReadoutInDescription = true
				}, true, false);
			}
			else if (part.BootTime < num)
			{
				part.BootTime = num;
			}
			base.IncreaseDifficultyAndComplexityIfComplex(1, 2, null);
		}

		// Token: 0x0600AE00 RID: 44544 RVA: 0x0040578C File Offset: 0x0040398C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == EquippedEvent.ID || ID == SingletonEvent<GetAvailableComputePowerEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<ModificationAppliedEvent>.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600AE01 RID: 44545 RVA: 0x004057FD File Offset: 0x004039FD
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApplyBonus(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE02 RID: 44546 RVA: 0x00405813 File Offset: 0x00403A13
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.UnapplyBonus(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE03 RID: 44547 RVA: 0x00405828 File Offset: 0x00403A28
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.CheckApplyBonus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE04 RID: 44548 RVA: 0x00405839 File Offset: 0x00403A39
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckApplyBonus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE05 RID: 44549 RVA: 0x0040584A File Offset: 0x00403A4A
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("{{brainbrine|co-processor}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE06 RID: 44550 RVA: 0x00405873 File Offset: 0x00403A73
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription(), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE07 RID: 44551 RVA: 0x00405895 File Offset: 0x00403A95
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApplyBonus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE08 RID: 44552 RVA: 0x004058A6 File Offset: 0x00403AA6
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckApplyBonus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE09 RID: 44553 RVA: 0x004058B7 File Offset: 0x00403AB7
		public override bool HandleEvent(GetAvailableComputePowerEvent E)
		{
			if (base.WasReady())
			{
				E.Amount += this.GetComputePower();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE0A RID: 44554 RVA: 0x004058DB File Offset: 0x00403ADB
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("circuitry", 10);
				E.Add("scholarship", 5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE0B RID: 44555 RVA: 0x0040590B File Offset: 0x00403B0B
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AE0C RID: 44556 RVA: 0x0040590E File Offset: 0x00403B0E
		public static int GetAttributeBonus(int Tier, int PowerLoad = 100, float Factor = 0.25f)
		{
			return (int)Math.Ceiling((double)((float)(Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)) * Factor));
		}

		// Token: 0x0600AE0D RID: 44557 RVA: 0x00405929 File Offset: 0x00403B29
		public int GetAttributeBonus(int PowerLoad)
		{
			return ModCoProcessor.GetAttributeBonus(this.Tier, PowerLoad, this.AttributeBonusFactor);
		}

		// Token: 0x0600AE0E RID: 44558 RVA: 0x0040593D File Offset: 0x00403B3D
		public int GetAttributeBonus()
		{
			return this.GetAttributeBonus(this.MyPowerLoadLevel());
		}

		// Token: 0x0600AE0F RID: 44559 RVA: 0x0040594B File Offset: 0x00403B4B
		public static int GetComputePower(int Tier, int PowerLoad = 100, float Factor = 2.5f)
		{
			return (int)Math.Ceiling((double)((float)(Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)) * Factor));
		}

		// Token: 0x0600AE10 RID: 44560 RVA: 0x00405966 File Offset: 0x00403B66
		public int GetComputePower(int PowerLoad)
		{
			return ModCoProcessor.GetComputePower(this.Tier, PowerLoad, this.ComputePowerFactor);
		}

		// Token: 0x0600AE11 RID: 44561 RVA: 0x0040597A File Offset: 0x00403B7A
		public int GetComputePower()
		{
			return this.GetComputePower(this.MyPowerLoadLevel());
		}

		// Token: 0x0600AE12 RID: 44562 RVA: 0x00405988 File Offset: 0x00403B88
		public void ApplyBonus(GameObject who = null)
		{
			if (who == null)
			{
				who = this.ParentObject.Equipped;
				if (who == null)
				{
					return;
				}
			}
			if (this.AttributeBonusApplied != 0)
			{
				this.UnapplyBonus(null);
			}
			this.AttributeBonusApplied = this.GetAttributeBonus();
			base.StatShifter.DefaultDisplayName = "co-processor";
			base.StatShifter.SetStatShift(who, this.Attribute, this.AttributeBonusApplied, false);
		}

		// Token: 0x0600AE13 RID: 44563 RVA: 0x004059EE File Offset: 0x00403BEE
		public void UnapplyBonus(GameObject who = null)
		{
			if (this.AttributeBonusApplied == 0)
			{
				return;
			}
			if (who == null)
			{
				who = this.ParentObject.Equipped;
				if (who == null)
				{
					return;
				}
			}
			base.StatShifter.RemoveStatShifts(who);
			this.AttributeBonusApplied = 0;
		}

		// Token: 0x0600AE14 RID: 44564 RVA: 0x00405A20 File Offset: 0x00403C20
		public bool ShouldApplyBonus(GameObject who = null)
		{
			if (who == null)
			{
				who = this.ParentObject.Equipped;
				if (who == null)
				{
					return false;
				}
			}
			return base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.IsObjectActivePartSubject(who);
		}

		// Token: 0x0600AE15 RID: 44565 RVA: 0x00405A74 File Offset: 0x00403C74
		public bool CheckApplyBonus(GameObject who = null)
		{
			if (who == null)
			{
				who = this.ParentObject.Equipped;
				if (who == null)
				{
					return false;
				}
			}
			if (this.ShouldApplyBonus(null))
			{
				if (this.AttributeBonusApplied == 0)
				{
					this.ApplyBonus(who);
					return true;
				}
			}
			else if (this.AttributeBonusApplied != 0)
			{
				this.UnapplyBonus(who);
				return true;
			}
			return false;
		}

		// Token: 0x0600AE16 RID: 44566 RVA: 0x00405AC2 File Offset: 0x00403CC2
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600AE17 RID: 44567 RVA: 0x00405AC8 File Offset: 0x00403CC8
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
			this.CheckApplyBonus(null);
		}

		// Token: 0x0600AE18 RID: 44568 RVA: 0x00405B0C File Offset: 0x00403D0C
		public static string GetDescription(int Tier)
		{
			return string.Concat(new string[]
			{
				"Co-Processor: When powered, this item grants ",
				(Tier == 0) ? "bonus" : ModCoProcessor.GetAttributeBonus(Tier, 100, 0.25f).Signed(false),
				" Intelligence and provides ",
				(Tier == 0) ? "" : (ModCoProcessor.GetComputePower(Tier, 100, 2.5f).ToString() + " units of "),
				"compute power to the local lattice."
			});
		}

		// Token: 0x0600AE19 RID: 44569 RVA: 0x00405B88 File Offset: 0x00403D88
		public static string GetDescription(int Tier, GameObject obj)
		{
			int powerLoadLevel = obj.GetPowerLoadLevel();
			return string.Concat(new string[]
			{
				"Co-Processor: When powered, this item grants ",
				(Tier == 0) ? "bonus" : ModCoProcessor.GetAttributeBonus(Tier, powerLoadLevel, 0.25f).Signed(false),
				" Intelligence and provides ",
				(Tier == 0) ? "" : (ModCoProcessor.GetComputePower(Tier, powerLoadLevel, 2.5f).ToString() + " units of "),
				"compute power to the local lattice."
			});
		}

		// Token: 0x0600AE1A RID: 44570 RVA: 0x00405C0C File Offset: 0x00403E0C
		public string GetInstanceDescription()
		{
			int num = this.MyPowerLoadLevel();
			return string.Concat(new string[]
			{
				"Co-processor: When powered, this item grants +",
				this.GetAttributeBonus(num).ToString(),
				" ",
				this.Attribute,
				" and provides ",
				this.GetComputePower(num).ToString(),
				" units of compute power to the local lattice."
			});
		}

		// Token: 0x040047DF RID: 18399
		public const string DEFAULT_ATTRIBUTE = "Intelligence";

		// Token: 0x040047E0 RID: 18400
		public const float DEFAULT_ATTRIBUTE_BONUS_FACTOR = 0.25f;

		// Token: 0x040047E1 RID: 18401
		public const float DEFAULT_COMPUTE_POWER_FACTOR = 2.5f;

		// Token: 0x040047E2 RID: 18402
		public string Attribute = "Intelligence";

		// Token: 0x040047E3 RID: 18403
		public float AttributeBonusFactor = 0.25f;

		// Token: 0x040047E4 RID: 18404
		public int AttributeBonusApplied;

		// Token: 0x040047E5 RID: 18405
		public float ComputePowerFactor = 2.5f;
	}
}
