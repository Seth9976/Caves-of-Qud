using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="F:XRL.World.Parts.IActivePart.IsPowerLoadSensitive" /> is true,
	/// charge consumption is adjusted treating the power load as a percentage
	/// (i.e. the standard overload power load of 400 multiplies charge
	/// consumption by 4).
	/// </remarks>
	// Token: 0x02000FE2 RID: 4066
	[Serializable]
	public class IActivePart : IPart
	{
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600A162 RID: 41314 RVA: 0x003AB67B File Offset: 0x003A987B
		// (set) Token: 0x0600A161 RID: 41313 RVA: 0x003AB661 File Offset: 0x003A9861
		public string RequiresBodyPartCategory
		{
			get
			{
				if (this.RequiresBodyPartCategoryCode == 0)
				{
					return null;
				}
				return BodyPartCategory.GetName(this.RequiresBodyPartCategoryCode);
			}
			set
			{
				if (value == null)
				{
					this.RequiresBodyPartCategoryCode = 0;
					return;
				}
				this.RequiresBodyPartCategoryCode = BodyPartCategory.GetCode(value, true);
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600A164 RID: 41316 RVA: 0x003AB6E1 File Offset: 0x003A98E1
		// (set) Token: 0x0600A163 RID: 41315 RVA: 0x003AB694 File Offset: 0x003A9894
		public bool IsTechScannable
		{
			get
			{
				return this.DescribeStatusForProperty == Scanning.GetScanPropertyName(Scanning.Scan.Tech);
			}
			set
			{
				if (value)
				{
					this.DescribeStatusForProperty = Scanning.GetScanPropertyName(Scanning.Scan.Tech);
					this.StatusStyle = "tech";
					return;
				}
				if (this.DescribeStatusForProperty == Scanning.GetScanPropertyName(Scanning.Scan.Tech))
				{
					this.DescribeStatusForProperty = null;
					this.StatusStyle = "plain";
				}
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600A166 RID: 41318 RVA: 0x003AB741 File Offset: 0x003A9941
		// (set) Token: 0x0600A165 RID: 41317 RVA: 0x003AB6F4 File Offset: 0x003A98F4
		public bool IsBioScannable
		{
			get
			{
				return this.DescribeStatusForProperty == Scanning.GetScanPropertyName(Scanning.Scan.Bio);
			}
			set
			{
				if (value)
				{
					this.DescribeStatusForProperty = Scanning.GetScanPropertyName(Scanning.Scan.Bio);
					this.StatusStyle = "bio";
					return;
				}
				if (this.DescribeStatusForProperty == Scanning.GetScanPropertyName(Scanning.Scan.Bio))
				{
					this.DescribeStatusForProperty = null;
					this.StatusStyle = "plain";
				}
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600A168 RID: 41320 RVA: 0x003AB7A1 File Offset: 0x003A99A1
		// (set) Token: 0x0600A167 RID: 41319 RVA: 0x003AB754 File Offset: 0x003A9954
		public bool IsStructureScannable
		{
			get
			{
				return this.DescribeStatusForProperty == Scanning.GetScanPropertyName(Scanning.Scan.Structure);
			}
			set
			{
				if (value)
				{
					this.DescribeStatusForProperty = Scanning.GetScanPropertyName(Scanning.Scan.Structure);
					this.StatusStyle = "structure";
					return;
				}
				if (this.DescribeStatusForProperty == Scanning.GetScanPropertyName(Scanning.Scan.Structure))
				{
					this.DescribeStatusForProperty = null;
					this.StatusStyle = "plain";
				}
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600A16A RID: 41322 RVA: 0x003AB807 File Offset: 0x003A9A07
		// (set) Token: 0x0600A169 RID: 41321 RVA: 0x003AB7B4 File Offset: 0x003A99B4
		public bool IsPowered
		{
			get
			{
				return this.ChargeUse > 0 || this.ChargeMinimum > 0;
			}
			set
			{
				if (value)
				{
					if (this.ChargeUse == 0 && this.ChargeMinimum == 0)
					{
						this.ChargeMinimum = 1;
					}
				}
				else
				{
					this.ChargeUse = 0;
					this.ChargeMinimum = 0;
				}
				this.IsBootSensitive = value;
				this.IsEMPSensitive = value;
				this.IsPowerSwitchSensitive = value;
				this.IsTechScannable = value;
			}
		}

		// Token: 0x0600A16B RID: 41323 RVA: 0x003AB820 File Offset: 0x003A9A20
		public override bool SameAs(IPart p)
		{
			IActivePart activePart = p as IActivePart;
			return activePart.ChargeUse == this.ChargeUse && activePart.ChargeMinimum == this.ChargeMinimum && activePart.RequiresBodyPartCategoryCode == this.RequiresBodyPartCategoryCode && activePart.IsBootSensitive == this.IsBootSensitive && activePart.IsBreakageSensitive == this.IsBreakageSensitive && activePart.IsEMPSensitive == this.IsEMPSensitive && activePart.IsPowerSwitchSensitive == this.IsPowerSwitchSensitive && activePart.IsRealityDistortionBased == this.IsRealityDistortionBased && activePart.IsRustSensitive == this.IsRustSensitive && activePart.MustBeUnderstood == this.MustBeUnderstood && activePart.WorksOnAdjacentCellContents == this.WorksOnAdjacentCellContents && activePart.WorksOnCarrier == this.WorksOnCarrier && activePart.WorksOnCellContents == this.WorksOnCellContents && activePart.WorksOnEnclosed == this.WorksOnEnclosed && activePart.WorksOnEquipper == this.WorksOnEquipper && activePart.WorksOnHolder == this.WorksOnHolder && activePart.WorksOnImplantee == this.WorksOnImplantee && activePart.WorksOnInventory == this.WorksOnInventory && activePart.WorksOnSelf == this.WorksOnSelf && activePart.WorksOnWearer == this.WorksOnWearer && !(activePart.NeedsOtherActivePartOperational != this.NeedsOtherActivePartOperational) && !(activePart.NeedsOtherActivePartEngaged != this.NeedsOtherActivePartEngaged) && !(activePart.ReadyColorString != this.ReadyColorString) && !(activePart.ReadyDetailColor != this.ReadyDetailColor) && !(activePart.DisabledColorString != this.DisabledColorString) && !(activePart.DisabledDetailColor != this.DisabledDetailColor);
		}

		// Token: 0x0600A16C RID: 41324 RVA: 0x003AB9F4 File Offset: 0x003A9BF4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == AllowLiquidCollectionEvent.ID && !this.ConsumesLiquid.IsNullOrEmpty()) || ID == BootSequenceAbortedEvent.ID || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == PooledEvent<CheckUsesChargeWhileEquippedEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || (ID == GetPreferredLiquidEvent.ID && !this.ConsumesLiquid.IsNullOrEmpty()) || ID == PooledEvent<GetScanTypeEvent>.ID || ID == GetShortDescriptionEvent.ID || (ID == PooledEvent<IsOverloadableEvent>.ID && this.IsPowerLoadSensitive) || (ID == PowerSwitchFlippedEvent.ID && this.IsPowerSwitchSensitive) || ID == PooledEvent<QueryDrawEvent>.ID || ID == PooledEvent<SyncRenderEvent>.ID || (ID == PooledEvent<TransparentToEMPEvent>.ID && this.IsEMPSensitive) || (ID == WantsLiquidCollectionEvent.ID && !this.ConsumesLiquid.IsNullOrEmpty());
		}

		// Token: 0x0600A16D RID: 41325 RVA: 0x003ABB05 File Offset: 0x003A9D05
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return (this.ConsumesLiquid.IsNullOrEmpty() || this.WantsLiquidCollection(E.Liquid)) && base.HandleEvent(E);
		}

		// Token: 0x0600A16E RID: 41326 RVA: 0x003ABB2B File Offset: 0x003A9D2B
		public override bool HandleEvent(GetPreferredLiquidEvent E)
		{
			if (E.Liquid == null && !this.ConsumesLiquid.IsNullOrEmpty())
			{
				E.Liquid = this.ConsumesLiquid;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A16F RID: 41327 RVA: 0x003ABB55 File Offset: 0x003A9D55
		public override bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return !this.WantsLiquidCollection(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600A170 RID: 41328 RVA: 0x003ABB6E File Offset: 0x003A9D6E
		public override bool HandleEvent(GetScanTypeEvent E)
		{
			if (E.ScanType == Scanning.Scan.Structure && E.Object == this.ParentObject)
			{
				if (this.IsBioScannable)
				{
					E.ScanType = Scanning.Scan.Bio;
				}
				else if (this.IsTechScannable)
				{
					E.ScanType = Scanning.Scan.Tech;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A171 RID: 41329 RVA: 0x003ABBAE File Offset: 0x003A9DAE
		public override bool HandleEvent(QueryDrawEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.Draw += this.GetDraw(E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A172 RID: 41330 RVA: 0x003ABBDC File Offset: 0x003A9DDC
		public override bool HandleEvent(CheckUsesChargeWhileEquippedEvent E)
		{
			if (this.ChargeUse > 0)
			{
				if (this.WorksOnEquipper || this.WorksOnHolder || this.WorksOnWearer)
				{
					return false;
				}
				if (this.WorksOnSelf && this.ParentObject.IsTakeable() && this.ParentObject.GetInventoryCategory(false) != "Ammo" && !this.ParentObject.HasPropertyOrTag("CannotEquip"))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A173 RID: 41331 RVA: 0x003ABC54 File Offset: 0x003A9E54
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.DescribeStatusForProperty.IsNullOrEmpty() && E.Context != "Tinkering" && The.Player != null && The.Player.GetIntProperty(this.DescribeStatusForProperty, 0) > 0)
			{
				E.Infix.Append('\n').Append(this.GetStatusDescription());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A174 RID: 41332 RVA: 0x003ABCBB File Offset: 0x003A9EBB
		public override bool HandleEvent(TransparentToEMPEvent E)
		{
			return !this.IsEMPSensitive && base.HandleEvent(E);
		}

		// Token: 0x0600A175 RID: 41333 RVA: 0x003ABCD0 File Offset: 0x003A9ED0
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A176 RID: 41334 RVA: 0x003ABD0C File Offset: 0x003A9F0C
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A177 RID: 41335 RVA: 0x003ABD48 File Offset: 0x003A9F48
		public override bool HandleEvent(BootSequenceAbortedEvent E)
		{
			this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A178 RID: 41336 RVA: 0x003ABD84 File Offset: 0x003A9F84
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A179 RID: 41337 RVA: 0x003ABDC0 File Offset: 0x003A9FC0
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A17A RID: 41338 RVA: 0x003ABDFC File Offset: 0x003A9FFC
		public override bool HandleEvent(SyncRenderEvent E)
		{
			this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A17B RID: 41339 RVA: 0x003ABE38 File Offset: 0x003AA038
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A17C RID: 41340 RVA: 0x003ABE74 File Offset: 0x003AA074
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A17D RID: 41341 RVA: 0x003ABEAF File Offset: 0x003AA0AF
		public override bool HandleEvent(IsOverloadableEvent E)
		{
			return !this.IsPowerLoadSensitive && base.HandleEvent(E);
		}

		// Token: 0x0600A17E RID: 41342 RVA: 0x003ABEC4 File Offset: 0x003AA0C4
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			ActivePartStatus activePartStatus = this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			E.AddEntry(this, "Status", activePartStatus.ToString());
			if (activePartStatus == ActivePartStatus.LocallyDefinedFailure)
			{
				E.AddEntry(this, "LocallyDefinedFailureDescription", this.GetActivePartLocallyDefinedFailureDescription());
			}
			E.AddEntry(this, "FirstSubject", this.GetActivePartFirstSubject());
			E.AddEntry(this, "ChargeUse", this.ChargeUse);
			E.AddEntry(this, "ChargeMinimum", this.ChargeMinimum);
			E.AddEntry(this, "RequiresBodyPartCategory", this.RequiresBodyPartCategory);
			E.AddEntry(this, "IsBootSensitive", this.IsBootSensitive);
			E.AddEntry(this, "IsBreakageSensitive", this.IsBreakageSensitive);
			E.AddEntry(this, "IsEMPSensitive", this.IsEMPSensitive);
			E.AddEntry(this, "IsHangingSensitive", this.IsHangingSensitive);
			E.AddEntry(this, "IsPowerSwitchSensitive", this.IsPowerSwitchSensitive);
			E.AddEntry(this, "IsRealityDistortionBased", this.IsRealityDistortionBased);
			E.AddEntry(this, "IsRustSensitive", this.IsRustSensitive);
			E.AddEntry(this, "MustBeUnderstood", this.MustBeUnderstood);
			E.AddEntry(this, "WorksOnAdjacentCellContents", this.WorksOnAdjacentCellContents);
			E.AddEntry(this, "WorksOnCarrier", this.WorksOnCarrier);
			E.AddEntry(this, "WorksOnCellContents", this.WorksOnCellContents);
			E.AddEntry(this, "WorksOnEnclosed", this.WorksOnEnclosed);
			E.AddEntry(this, "WorksOnEquipper", this.WorksOnEquipper);
			E.AddEntry(this, "WorksOnHolder", this.WorksOnHolder);
			E.AddEntry(this, "WorksOnImplantee", this.WorksOnImplantee);
			E.AddEntry(this, "WorksOnInventory", this.WorksOnInventory);
			E.AddEntry(this, "WorksOnSelf", this.WorksOnSelf);
			E.AddEntry(this, "WorksOnWearer", this.WorksOnWearer);
			E.AddEntry(this, "NeedsOtherActivePartOperational", this.NeedsOtherActivePartOperational);
			E.AddEntry(this, "NeedsOtherActivePartEngaged", this.NeedsOtherActivePartEngaged);
			E.AddEntry(this, "DescribeStatusForProperty", this.DescribeStatusForProperty);
			E.AddEntry(this, "NameForStatus", this.NameForStatus);
			E.AddEntry(this, "StatusStyle", this.StatusStyle);
			E.AddEntry(this, "ReadyColorString", this.ReadyColorString);
			E.AddEntry(this, "ReadyDetailColor", this.ReadyDetailColor);
			E.AddEntry(this, "DisabledColorString", this.DisabledColorString);
			E.AddEntry(this, "DisabledDetailColor", this.DisabledDetailColor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A17F RID: 41343 RVA: 0x003AC15D File Offset: 0x003AA35D
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject) && this.IsTechScannable)
			{
				E.Add("circuitry", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A180 RID: 41344 RVA: 0x003AC188 File Offset: 0x003AA388
		public void ResetSensitivity()
		{
			this.IsBootSensitive = false;
			this.IsBreakageSensitive = true;
			this.IsEMPSensitive = false;
			this.IsHangingSensitive = false;
			this.IsPowerSwitchSensitive = false;
			this.IsRealityDistortionBased = false;
			this.IsRustSensitive = true;
			this.IsPowerLoadSensitive = false;
		}

		// Token: 0x0600A181 RID: 41345 RVA: 0x003AC1C4 File Offset: 0x003AA3C4
		public void ResetWorksOn()
		{
			this.WorksOnAdjacentCellContents = false;
			this.WorksOnCarrier = false;
			this.WorksOnCellContents = false;
			this.WorksOnEnclosed = false;
			this.WorksOnEquipper = false;
			this.WorksOnHolder = false;
			this.WorksOnImplantee = false;
			this.WorksOnInventory = false;
			this.WorksOnSelf = false;
			this.WorksOnWearer = false;
		}

		// Token: 0x0600A182 RID: 41346 RVA: 0x003AC218 File Offset: 0x003AA418
		public void WorksOn(bool AdjacentCellContents = false, bool Carrier = false, bool CellContents = false, bool Enclosed = false, bool Equipper = false, bool Holder = false, bool Implantee = false, bool Inventory = false, bool Self = false, bool Wearer = false)
		{
			this.WorksOnAdjacentCellContents = AdjacentCellContents;
			this.WorksOnCarrier = Carrier;
			this.WorksOnCellContents = CellContents;
			this.WorksOnEnclosed = Enclosed;
			this.WorksOnEquipper = Equipper;
			this.WorksOnHolder = Holder;
			this.WorksOnImplantee = Implantee;
			this.WorksOnInventory = Inventory;
			this.WorksOnSelf = Self;
			this.WorksOnWearer = Wearer;
		}

		// Token: 0x0600A183 RID: 41347 RVA: 0x003AC274 File Offset: 0x003AA474
		public void SyncWorksOn(IActivePart p)
		{
			this.WorksOnAdjacentCellContents = p.WorksOnAdjacentCellContents;
			this.WorksOnCarrier = p.WorksOnCarrier;
			this.WorksOnCellContents = p.WorksOnCellContents;
			this.WorksOnEnclosed = p.WorksOnEnclosed;
			this.WorksOnEquipper = p.WorksOnEquipper;
			this.WorksOnHolder = p.WorksOnHolder;
			this.WorksOnImplantee = p.WorksOnImplantee;
			this.WorksOnInventory = p.WorksOnInventory;
			this.WorksOnSelf = p.WorksOnSelf;
			this.WorksOnWearer = p.WorksOnWearer;
		}

		// Token: 0x0600A184 RID: 41348 RVA: 0x003AC2FC File Offset: 0x003AA4FC
		public bool HasAnyWorksOn()
		{
			return this.WorksOnAdjacentCellContents || this.WorksOnCarrier || this.WorksOnCellContents || this.WorksOnEnclosed || this.WorksOnEquipper || this.WorksOnHolder || this.WorksOnImplantee || this.WorksOnInventory || this.WorksOnSelf || this.WorksOnWearer;
		}

		// Token: 0x0600A185 RID: 41349 RVA: 0x003AC35C File Offset: 0x003AA55C
		public virtual void SyncRender()
		{
			ActivePartStatus? lastStatus = this.LastStatus;
			ActivePartStatus activePartStatus = ActivePartStatus.Operational;
			if ((lastStatus.GetValueOrDefault() == activePartStatus) & (lastStatus != null))
			{
				if (!this.ReadyColorString.IsNullOrEmpty())
				{
					this.ParentObject.Render.ColorString = this.ReadyColorString;
				}
				if (!this.ReadyDetailColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.DetailColor = this.ReadyDetailColor;
					return;
				}
			}
			else
			{
				if (!this.DisabledColorString.IsNullOrEmpty())
				{
					this.ParentObject.Render.ColorString = this.DisabledColorString;
				}
				if (!this.DisabledDetailColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.DetailColor = this.DisabledDetailColor;
				}
			}
		}

		// Token: 0x0600A186 RID: 41350 RVA: 0x003AC414 File Offset: 0x003AA614
		private bool Encloses(GameObject obj)
		{
			Enclosed effect = obj.GetEffect<Enclosed>();
			return effect != null && effect.EnclosedBy == this.ParentObject;
		}

		// Token: 0x0600A187 RID: 41351 RVA: 0x003AC43B File Offset: 0x003AA63B
		public virtual bool WorksForEveryone()
		{
			return !this.MustBeUnderstood;
		}

		// Token: 0x0600A188 RID: 41352 RVA: 0x003AC446 File Offset: 0x003AA646
		public virtual bool WorksFor(GameObject obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this.MustBeUnderstood)
			{
				if (obj.IsPlayer())
				{
					if (!this.ParentObject.Understood())
					{
						return false;
					}
				}
				else if (!obj.HasStat("Intelligence"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600A189 RID: 41353 RVA: 0x003AC47C File Offset: 0x003AA67C
		private bool EnclosesAndWorksFor(GameObject obj)
		{
			return this.Encloses(obj) && this.WorksFor(obj);
		}

		// Token: 0x0600A18A RID: 41354 RVA: 0x003AC490 File Offset: 0x003AA690
		private bool NotSelf(GameObject obj)
		{
			return obj != this.ParentObject;
		}

		// Token: 0x0600A18B RID: 41355 RVA: 0x003AC49E File Offset: 0x003AA69E
		private bool WorksForAndNotSelf(GameObject obj)
		{
			return obj != this.ParentObject && this.WorksFor(obj);
		}

		// Token: 0x0600A18C RID: 41356 RVA: 0x003AC4B4 File Offset: 0x003AA6B4
		public virtual bool ActivePartNeedsSubject()
		{
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject))
			{
				return false;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped))
			{
				return false;
			}
			if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped))
			{
				return false;
			}
			if (this.WorksOnImplantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && this.WorksFor(implantee))
				{
					return false;
				}
			}
			if (this.WorksOnEquipper)
			{
				GameObject parentObject = this.ParentObject;
				if (this.WorksFor((parentObject != null) ? parentObject.EquippedProperlyBy() : null))
				{
					return false;
				}
			}
			GameObject parentObject2 = this.ParentObject;
			Cell cell = ((parentObject2 != null) ? parentObject2.CurrentCell : null);
			if (cell != null)
			{
				if (this.WorksOnCellContents)
				{
					int i = 0;
					int count = cell.Objects.Count;
					while (i < count)
					{
						if (this.WorksForAndNotSelf(cell.Objects[i]))
						{
							return false;
						}
						i++;
					}
				}
				if (this.WorksOnAdjacentCellContents)
				{
					foreach (Cell cell2 in cell.IterateAdjacent(1, false, false, true))
					{
						int j = 0;
						int count2 = cell2.Objects.Count;
						while (j < count2)
						{
							if (this.WorksFor(cell2.Objects[j]))
							{
								return false;
							}
							j++;
						}
					}
				}
				if (this.WorksOnEnclosed)
				{
					int k = 0;
					int count3 = cell.Objects.Count;
					while (k < count3)
					{
						if (this.EnclosesAndWorksFor(cell.Objects[k]))
						{
							return false;
						}
						k++;
					}
				}
			}
			return (!this.WorksOnCarrier || ((this.ParentObject.InInventory == null || !this.WorksFor(this.ParentObject.InInventory)) && (!this.ParentObject.IsEquippedAsThrownWeapon() || !this.WorksFor(this.ParentObject.Equipped)))) && (!this.WorksOnInventory || !this.ParentObject.HasPart<Inventory>() || !this.ParentObject.Inventory.HasObject(new Predicate<GameObject>(this.WorksFor)));
		}

		// Token: 0x0600A18D RID: 41357 RVA: 0x003AC6EC File Offset: 0x003AA8EC
		public virtual GameObject GetActivePartFirstSubject()
		{
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject))
			{
				return this.ParentObject;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped))
			{
				return this.ParentObject.Equipped;
			}
			if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped))
			{
				return this.ParentObject.Equipped;
			}
			if (this.WorksOnImplantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && this.WorksFor(implantee))
				{
					return implantee;
				}
			}
			if (this.WorksOnEquipper)
			{
				GameObject gameObject = this.ParentObject.EquippedProperlyBy();
				if (this.WorksFor(gameObject))
				{
					return gameObject;
				}
			}
			if (this.WorksOnCarrier)
			{
				if (this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory))
				{
					return this.ParentObject.InInventory;
				}
				if (this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped))
				{
					return this.ParentObject.Equipped;
				}
			}
			if (this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				GameObject gameObject2 = (this.WorksForEveryone() ? this.ParentObject.CurrentCell.GetFirstObject(new Predicate<GameObject>(this.NotSelf)) : this.ParentObject.CurrentCell.GetFirstObject(new Predicate<GameObject>(this.WorksForAndNotSelf)));
				if (gameObject2 != null)
				{
					return gameObject2;
				}
			}
			if (this.WorksOnAdjacentCellContents && this.ParentObject.CurrentCell != null)
			{
				if (this.WorksForEveryone())
				{
					using (List<Cell>.Enumerator enumerator = this.ParentObject.CurrentCell.GetAdjacentCells(true).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Cell cell = enumerator.Current;
							GameObject firstObject = cell.GetFirstObject();
							if (firstObject != null)
							{
								return firstObject;
							}
						}
						goto IL_024E;
					}
				}
				foreach (Cell cell2 in this.ParentObject.CurrentCell.GetAdjacentCells(true))
				{
					GameObject firstObject2 = cell2.GetFirstObject(new Predicate<GameObject>(this.WorksFor));
					if (firstObject2 != null)
					{
						return firstObject2;
					}
				}
			}
			IL_024E:
			if (this.WorksOnEnclosed && this.ParentObject.CurrentCell != null)
			{
				GameObject firstObject3 = this.ParentObject.CurrentCell.GetFirstObject(this.WorksForEveryone() ? new Predicate<GameObject>(this.Encloses) : new Predicate<GameObject>(this.EnclosesAndWorksFor));
				if (firstObject3 != null)
				{
					return firstObject3;
				}
			}
			if (this.WorksOnInventory && this.ParentObject.HasPart<Inventory>())
			{
				GameObject gameObject3 = (this.WorksForEveryone() ? this.ParentObject.Inventory.GetFirstObject() : this.ParentObject.Inventory.GetFirstObject(new Predicate<GameObject>(this.WorksFor)));
				if (gameObject3 != null)
				{
					return gameObject3;
				}
			}
			return null;
		}

		// Token: 0x0600A18E RID: 41358 RVA: 0x003ACA0C File Offset: 0x003AAC0C
		public virtual GameObject GetActivePartFirstSubject(Predicate<GameObject> Filter)
		{
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject) && Filter(this.ParentObject))
			{
				return this.ParentObject;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped) && Filter(this.ParentObject.Equipped))
			{
				return this.ParentObject.Equipped;
			}
			if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped) && Filter(this.ParentObject.Equipped))
			{
				return this.ParentObject.Equipped;
			}
			if (this.WorksOnImplantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && this.WorksFor(implantee) && Filter(implantee))
				{
					return implantee;
				}
			}
			if (this.WorksOnEquipper)
			{
				GameObject gameObject = this.ParentObject.EquippedProperlyBy();
				if (this.WorksFor(gameObject) && Filter(gameObject))
				{
					return gameObject;
				}
			}
			if (this.WorksOnCarrier)
			{
				if (this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory) && Filter(this.ParentObject.InInventory))
				{
					return this.ParentObject.InInventory;
				}
				if (this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped) && Filter(this.ParentObject.Equipped))
				{
					return this.ParentObject.Equipped;
				}
			}
			if (this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				foreach (GameObject gameObject2 in this.ParentObject.CurrentCell.Objects)
				{
					if (this.WorksFor(gameObject2) && this.NotSelf(gameObject2) && Filter(gameObject2))
					{
						return gameObject2;
					}
				}
			}
			if (this.WorksOnAdjacentCellContents && this.ParentObject.CurrentCell != null)
			{
				if (this.WorksForEveryone())
				{
					using (List<Cell>.Enumerator enumerator2 = this.ParentObject.CurrentCell.GetAdjacentCells(true).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Cell cell = enumerator2.Current;
							GameObject firstObject = cell.GetFirstObject(Filter);
							if (firstObject != null)
							{
								return firstObject;
							}
						}
						goto IL_02FF;
					}
				}
				foreach (Cell cell2 in this.ParentObject.CurrentCell.GetAdjacentCells(true))
				{
					foreach (GameObject gameObject3 in cell2.Objects)
					{
						if (this.WorksFor(gameObject3) && Filter(gameObject3))
						{
							return gameObject3;
						}
					}
				}
			}
			IL_02FF:
			if (this.WorksOnEnclosed && this.ParentObject.CurrentCell != null)
			{
				GameObject firstObject2 = this.ParentObject.CurrentCell.GetFirstObject(this.WorksForEveryone() ? new Predicate<GameObject>(this.Encloses) : new Predicate<GameObject>(this.EnclosesAndWorksFor));
				if (firstObject2 != null && Filter(firstObject2))
				{
					return firstObject2;
				}
			}
			if (this.WorksOnInventory && this.ParentObject.HasPart<Inventory>())
			{
				foreach (GameObject gameObject4 in this.ParentObject.Inventory.GetObjectsDirect())
				{
					if (this.WorksFor(gameObject4) && Filter(gameObject4))
					{
						return gameObject4;
					}
				}
			}
			return null;
		}

		// Token: 0x0600A18F RID: 41359 RVA: 0x003ACE20 File Offset: 0x003AB020
		public virtual List<GameObject> GetActivePartSubjects()
		{
			List<GameObject> list = Event.NewGameObjectList();
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject))
			{
				list.Add(this.ParentObject);
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped) && !list.Contains(this.ParentObject.Equipped))
			{
				list.Add(this.ParentObject.Equipped);
			}
			if (this.WorksOnWearer && !list.Contains(this.ParentObject.Equipped) && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped))
			{
				list.Add(this.ParentObject.Equipped);
			}
			if (this.WorksOnImplantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && this.WorksFor(implantee))
				{
					list.Add(implantee);
				}
			}
			if (this.WorksOnEquipper)
			{
				GameObject gameObject = this.ParentObject.EquippedProperlyBy();
				if (gameObject != null && !list.Contains(gameObject) && this.WorksFor(gameObject))
				{
					list.Add(gameObject);
				}
			}
			if (this.WorksOnCarrier)
			{
				if (this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory))
				{
					list.Add(this.ParentObject.InInventory);
				}
				if (!list.Contains(this.ParentObject.Equipped) && this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped))
				{
					list.Add(this.ParentObject.Equipped);
				}
			}
			if (this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				List<GameObject> objectsViaEventList = this.ParentObject.CurrentCell.GetObjectsViaEventList();
				if (this.WorksForEveryone())
				{
					for (int i = 0; i < objectsViaEventList.Count; i++)
					{
						if (this.NotSelf(objectsViaEventList[i]))
						{
							list.Add(objectsViaEventList[i]);
						}
					}
				}
				else
				{
					for (int j = 0; j < objectsViaEventList.Count; j++)
					{
						if (this.WorksForAndNotSelf(objectsViaEventList[j]))
						{
							list.Add(objectsViaEventList[j]);
						}
					}
				}
			}
			if (this.WorksOnAdjacentCellContents && this.ParentObject.CurrentCell != null)
			{
				List<Cell> adjacentCells = this.ParentObject.CurrentCell.GetAdjacentCells(true);
				for (int k = 0; k < adjacentCells.Count; k++)
				{
					Cell cell = adjacentCells[k];
					list.AddRange(this.WorksForEveryone() ? cell.GetObjectsViaEventList() : cell.GetObjectsViaEventList(new Predicate<GameObject>(this.WorksFor)));
				}
			}
			if (this.WorksOnEnclosed && this.ParentObject.CurrentCell != null)
			{
				List<GameObject> objectsViaEventList2 = this.ParentObject.CurrentCell.GetObjectsViaEventList();
				for (int l = 0; l < objectsViaEventList2.Count; l++)
				{
					GameObject gameObject2 = objectsViaEventList2[l];
					if (this.Encloses(gameObject2) && this.WorksFor(gameObject2) && !list.Contains(gameObject2))
					{
						list.Add(gameObject2);
					}
				}
			}
			if (this.WorksOnInventory && this.ParentObject.HasPart<Inventory>())
			{
				list.AddRange(this.WorksForEveryone() ? this.ParentObject.Inventory.GetObjects() : this.ParentObject.Inventory.GetObjectsViaEventList(new Predicate<GameObject>(this.WorksFor)));
			}
			return list;
		}

		// Token: 0x0600A190 RID: 41360 RVA: 0x003AD194 File Offset: 0x003AB394
		public virtual List<GameObject> GetActivePartSubjects(Predicate<GameObject> Filter)
		{
			List<GameObject> list = Event.NewGameObjectList();
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject) && Filter(this.ParentObject))
			{
				list.Add(this.ParentObject);
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped) && !list.Contains(this.ParentObject.Equipped) && Filter(this.ParentObject.Equipped))
			{
				list.Add(this.ParentObject.Equipped);
			}
			if (this.WorksOnWearer && !list.Contains(this.ParentObject.Equipped) && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped) && Filter(this.ParentObject.Equipped))
			{
				list.Add(this.ParentObject.Equipped);
			}
			if (this.WorksOnImplantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && this.WorksFor(implantee) && Filter(implantee))
				{
					list.Add(implantee);
				}
			}
			if (this.WorksOnEquipper)
			{
				GameObject gameObject = this.ParentObject.EquippedProperlyBy();
				if (gameObject != null && !list.Contains(gameObject) && this.WorksFor(gameObject) && Filter(gameObject))
				{
					list.Add(gameObject);
				}
			}
			if (this.WorksOnCarrier)
			{
				if (this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory) && Filter(this.ParentObject.InInventory))
				{
					list.Add(this.ParentObject.InInventory);
				}
				if (!list.Contains(this.ParentObject.Equipped) && this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped) && Filter(this.ParentObject.Equipped))
				{
					list.Add(this.ParentObject.Equipped);
				}
			}
			if (this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				list.AddRange(this.WorksForEveryone() ? this.ParentObject.CurrentCell.GetObjects((GameObject obj) => this.NotSelf(obj) && Filter(obj)) : this.ParentObject.CurrentCell.GetObjects((GameObject obj) => this.WorksForAndNotSelf(obj) && Filter(obj)));
			}
			if (this.WorksOnAdjacentCellContents && this.ParentObject.CurrentCell != null)
			{
				Predicate<GameObject> <>9__3;
				foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(true))
				{
					List<GameObject> list2 = list;
					IEnumerable<GameObject> enumerable;
					if (!this.WorksForEveryone())
					{
						Cell cell2 = cell;
						Predicate<GameObject> predicate;
						if ((predicate = <>9__3) == null)
						{
							predicate = (<>9__3 = (GameObject obj) => this.WorksFor(obj) && Filter(obj));
						}
						enumerable = cell2.GetObjects(predicate);
					}
					else
					{
						enumerable = cell.GetObjects(Filter);
					}
					list2.AddRange(enumerable);
				}
			}
			if (this.WorksOnEnclosed && this.ParentObject.CurrentCell != null)
			{
				foreach (GameObject gameObject2 in this.ParentObject.CurrentCell.GetObjectsViaEventList())
				{
					if (this.Encloses(gameObject2) && this.WorksFor(gameObject2) && !list.Contains(gameObject2) && Filter(gameObject2))
					{
						list.Add(gameObject2);
					}
				}
			}
			if (this.WorksOnInventory && this.ParentObject.HasPart<Inventory>())
			{
				list.AddRange(this.WorksForEveryone() ? this.ParentObject.Inventory.GetObjects(Filter) : this.ParentObject.Inventory.GetObjects((GameObject obj) => this.WorksFor(obj) && Filter(obj)));
			}
			return list;
		}

		// Token: 0x0600A191 RID: 41361 RVA: 0x003AD5D0 File Offset: 0x003AB7D0
		public virtual bool IsObjectActivePartSubject(GameObject Object)
		{
			if (Object == null)
			{
				return false;
			}
			if (!this.WorksFor(Object))
			{
				return false;
			}
			if (this.WorksOnSelf && this.ParentObject == Object)
			{
				return true;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && Object == this.ParentObject.Equipped)
			{
				return true;
			}
			if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && Object == this.ParentObject.Equipped)
			{
				return true;
			}
			if (this.WorksOnImplantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && implantee == Object)
				{
					return true;
				}
			}
			if (this.WorksOnEquipper && Object == this.ParentObject.EquippedProperlyBy())
			{
				return true;
			}
			if (this.WorksOnCarrier)
			{
				if (this.ParentObject.InInventory != null && this.ParentObject.InInventory == Object)
				{
					return true;
				}
				if (this.ParentObject.IsEquippedAsThrownWeapon() && this.ParentObject.Equipped == Object)
				{
					return true;
				}
			}
			if (this.WorksOnCellContents && Object != this.ParentObject)
			{
				Cell currentCell = Object.CurrentCell;
				if (currentCell != null && currentCell == this.ParentObject.CurrentCell)
				{
					return true;
				}
			}
			if (this.WorksOnAdjacentCellContents && this.ParentObject.CurrentCell != null)
			{
				using (List<Cell>.Enumerator enumerator = this.ParentObject.CurrentCell.GetAdjacentCells(true).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.HasObject(Object))
						{
							return true;
						}
					}
				}
			}
			return (this.WorksOnEnclosed && this.Encloses(Object)) || (this.WorksOnInventory && this.ParentObject.HasPart<Inventory>() && this.ParentObject.Inventory.HasObject(Object));
		}

		// Token: 0x0600A192 RID: 41362 RVA: 0x003AD798 File Offset: 0x003AB998
		public virtual bool ActivePartHasSingleSubject()
		{
			int num = 0;
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject))
			{
				num++;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped))
			{
				if (++num > 1)
				{
					return false;
				}
			}
			else if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped))
			{
				if (++num > 1)
				{
					return false;
				}
			}
			else if (this.WorksOnEquipper && this.WorksFor(this.ParentObject.EquippedProperlyBy()))
			{
				if (++num > 1)
				{
					return false;
				}
			}
			else if (this.WorksOnCarrier && this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped) && ++num > 1)
			{
				return false;
			}
			if (this.WorksOnCarrier && this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory) && ++num > 1)
			{
				return false;
			}
			if (this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				num += this.ParentObject.CurrentCell.GetObjectCount(new Predicate<GameObject>(this.WorksForAndNotSelf));
				if (num > 1)
				{
					return false;
				}
			}
			if (this.WorksOnAdjacentCellContents && this.ParentObject.CurrentCell != null)
			{
				foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(true))
				{
					num += cell.GetObjectCount(new Predicate<GameObject>(this.WorksFor));
					if (num > 1)
					{
						return false;
					}
				}
			}
			if (this.WorksOnEnclosed && !this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				foreach (GameObject gameObject in this.ParentObject.CurrentCell.GetObjectsViaEventList())
				{
					if (this.Encloses(gameObject) && this.WorksFor(gameObject) && this.NotSelf(gameObject) && ++num > 1)
					{
						return false;
					}
				}
			}
			if (this.WorksOnInventory && this.ParentObject.HasPart<Inventory>())
			{
				num += this.ParentObject.Inventory.GetObjectCount(new Predicate<GameObject>(this.WorksFor));
				if (num > 1)
				{
					return false;
				}
			}
			return num == 1;
		}

		// Token: 0x0600A193 RID: 41363 RVA: 0x003ADA3C File Offset: 0x003ABC3C
		public virtual bool ActivePartHasMultipleSubjects()
		{
			int num = 0;
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject))
			{
				num++;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped))
			{
				if (++num > 1)
				{
					return true;
				}
			}
			else if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped))
			{
				if (++num > 1)
				{
					return true;
				}
			}
			else if (this.WorksOnEquipper && this.WorksFor(this.ParentObject.EquippedProperlyBy()))
			{
				if (++num > 1)
				{
					return true;
				}
			}
			else if (this.WorksOnCarrier && this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped) && ++num > 1)
			{
				return true;
			}
			if (this.WorksOnCarrier && this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory) && ++num > 1)
			{
				return true;
			}
			if (this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				num += this.ParentObject.CurrentCell.GetObjectCount(new Predicate<GameObject>(this.WorksForAndNotSelf));
				if (num > 1)
				{
					return true;
				}
			}
			if (this.WorksOnAdjacentCellContents && this.ParentObject.CurrentCell != null)
			{
				foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(true))
				{
					num += cell.GetObjectCount(new Predicate<GameObject>(this.WorksFor));
					if (num > 1)
					{
						return true;
					}
				}
			}
			if (this.WorksOnEnclosed && !this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				foreach (GameObject gameObject in this.ParentObject.CurrentCell.GetObjectsViaEventList())
				{
					if (this.Encloses(gameObject) && this.WorksFor(gameObject) && this.NotSelf(gameObject) && ++num > 1)
					{
						return true;
					}
				}
			}
			if (this.WorksOnInventory && this.ParentObject.HasPart<Inventory>())
			{
				num += this.ParentObject.Inventory.GetObjectCount(new Predicate<GameObject>(this.WorksFor));
				if (num > 1)
				{
					return true;
				}
			}
			return num > 1;
		}

		// Token: 0x0600A194 RID: 41364 RVA: 0x003ADCE0 File Offset: 0x003ABEE0
		public virtual int GetActivePartSubjectCount()
		{
			int num = 0;
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject))
			{
				num++;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped))
			{
				num++;
			}
			else if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped))
			{
				num++;
			}
			else if (this.WorksOnEquipper && this.WorksFor(this.ParentObject.EquippedProperlyBy()))
			{
				num++;
			}
			else if (this.WorksOnCarrier && this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped))
			{
				num++;
			}
			if (this.WorksOnCarrier && this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory))
			{
				num++;
			}
			if (this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				num += this.ParentObject.CurrentCell.GetObjectCount(new Predicate<GameObject>(this.WorksForAndNotSelf));
			}
			if (this.WorksOnAdjacentCellContents && this.ParentObject.CurrentCell != null)
			{
				foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(true))
				{
					num += cell.GetObjectCount(new Predicate<GameObject>(this.WorksFor));
				}
			}
			if (this.WorksOnEnclosed && !this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				foreach (GameObject gameObject in this.ParentObject.CurrentCell.GetObjectsViaEventList())
				{
					if (this.Encloses(gameObject) && this.WorksFor(gameObject) && this.NotSelf(gameObject))
					{
						num++;
					}
				}
			}
			if (this.WorksOnInventory && this.ParentObject.HasPart<Inventory>())
			{
				num += this.ParentObject.Inventory.GetObjectCount(new Predicate<GameObject>(this.WorksFor));
			}
			return num;
		}

		/// <summary>
		/// A non-cancelable form of <c>ForeachActivePartSubjectWhile</c>, just for foreach over everthing.
		/// <b>PERFORMANCE NOTE:</b> If you are calling this from inside a tight loop on a bunch of things, please use
		/// <c>ForeachActivePartSubjectWhile</c> directly, this interface can create extra garbage.
		/// </summary>
		// Token: 0x0600A195 RID: 41365 RVA: 0x003ADF44 File Offset: 0x003AC144
		public virtual void ForeachActivePartSubject(Action<GameObject> Proc, bool MayMoveAddOrDestroy = false)
		{
			this.ForeachActivePartSubjectWhile(delegate(GameObject obj)
			{
				Proc(obj);
				return true;
			}, MayMoveAddOrDestroy);
		}

		/// <summary>
		/// The negated form of <c>ForeachActivePartSubjectWhile</c>, returning true will exit the foreach early,
		/// and cause the return value to be true.  It returns false on an empty set or if every predicate returns false.
		/// <b>PERFORMANCE NOTE:</b> If you are calling this from inside a tight loop on a bunch of things, please use
		/// <c>ForeachActivePartSubjectWhile</c> directly, this interface can create extra garbage.
		/// </summary>
		// Token: 0x0600A196 RID: 41366 RVA: 0x003ADF74 File Offset: 0x003AC174
		public bool ForeachActivePartSubjectUntil(Predicate<GameObject> Proc, bool MayMoveAddOrDestroy = false)
		{
			return !this.ForeachActivePartSubjectWhile((GameObject obj) => !Proc(obj), false);
		}

		// Token: 0x0600A197 RID: 41367 RVA: 0x003ADFA4 File Offset: 0x003AC1A4
		private bool ForeachWhileReturnsTrue(GameObject o)
		{
			return true;
		}

		/// <summary>
		/// Finds the GameObjects that this part <c>WorksFor(GameObject)</c> in the ParentObjects's context,
		/// passing each to your predicate.  Returning false will exit the foreach early, and cause the return value to be false.
		/// It returns true on an empty set or if every predicate returns true.
		/// </summary>
		// Token: 0x0600A198 RID: 41368 RVA: 0x003ADFA8 File Offset: 0x003AC1A8
		public virtual bool ForeachActivePartSubjectWhile(Predicate<GameObject> Proc, bool MayMoveAddOrDestroy = false)
		{
			if (Proc == null)
			{
				Proc = new Predicate<GameObject>(this.ForeachWhileReturnsTrue);
			}
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject) && !Proc(this.ParentObject))
			{
				return false;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped) && !Proc(this.ParentObject.Equipped))
			{
				return false;
			}
			if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped) && !Proc(this.ParentObject.Equipped))
			{
				return false;
			}
			if (this.WorksOnEquipper)
			{
				GameObject gameObject = this.ParentObject.EquippedProperlyBy();
				if (this.WorksFor(gameObject) && !Proc(gameObject))
				{
					return false;
				}
			}
			if (this.WorksOnCarrier && this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped) && !Proc(this.ParentObject.Equipped))
			{
				return false;
			}
			if (this.WorksOnCarrier && this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory) && !Proc(this.ParentObject.InInventory))
			{
				return false;
			}
			if (this.WorksOnImplantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && this.WorksFor(implantee) && !Proc(implantee))
				{
					return false;
				}
			}
			if (this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				if (this.WorksForEveryone())
				{
					if (MayMoveAddOrDestroy)
					{
						if (!this.ParentObject.CurrentCell.SafeForeachObject(Proc, new Predicate<GameObject>(this.NotSelf)))
						{
							return false;
						}
					}
					else if (!this.ParentObject.CurrentCell.ForeachObject(Proc, new Predicate<GameObject>(this.NotSelf)))
					{
						return false;
					}
				}
				else if (MayMoveAddOrDestroy)
				{
					if (!this.ParentObject.CurrentCell.SafeForeachObject(Proc, new Predicate<GameObject>(this.WorksForAndNotSelf)))
					{
						return false;
					}
				}
				else if (!this.ParentObject.CurrentCell.ForeachObject(Proc, new Predicate<GameObject>(this.WorksForAndNotSelf)))
				{
					return false;
				}
			}
			if (this.WorksOnAdjacentCellContents && this.ParentObject.CurrentCell != null)
			{
				if (this.WorksForEveryone())
				{
					if (MayMoveAddOrDestroy)
					{
						using (List<Cell>.Enumerator enumerator = this.ParentObject.CurrentCell.GetAdjacentCells(true).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (!enumerator.Current.SafeForeachObject(Proc))
								{
									return false;
								}
							}
							goto IL_0404;
						}
					}
					using (List<Cell>.Enumerator enumerator = this.ParentObject.CurrentCell.GetAdjacentCells(true).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!enumerator.Current.ForeachObject(Proc))
							{
								return false;
							}
						}
						goto IL_0404;
					}
				}
				if (MayMoveAddOrDestroy)
				{
					using (List<Cell>.Enumerator enumerator = this.ParentObject.CurrentCell.GetAdjacentCells(true).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!enumerator.Current.SafeForeachObject(Proc, new Predicate<GameObject>(this.WorksFor)))
							{
								return false;
							}
						}
						goto IL_0404;
					}
				}
				using (List<Cell>.Enumerator enumerator = this.ParentObject.CurrentCell.GetAdjacentCells(true).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.ForeachObject(Proc, new Predicate<GameObject>(this.WorksFor)))
						{
							return false;
						}
					}
				}
			}
			IL_0404:
			if (this.WorksOnEnclosed && !this.WorksOnCellContents && this.ParentObject.CurrentCell != null)
			{
				if (MayMoveAddOrDestroy)
				{
					if (!this.ParentObject.CurrentCell.SafeForeachObject((GameObject obj) => !this.Encloses(obj) || !this.WorksFor(obj) || !this.NotSelf(obj) || Proc(obj)))
					{
						return false;
					}
				}
				else
				{
					foreach (GameObject gameObject2 in this.ParentObject.CurrentCell.GetObjectsViaEventList())
					{
						if (this.Encloses(gameObject2) && this.WorksFor(gameObject2) && this.NotSelf(gameObject2) && !Proc(gameObject2))
						{
							return false;
						}
					}
				}
			}
			if (this.WorksOnInventory && this.ParentObject.HasPart<Inventory>())
			{
				if (this.WorksForEveryone())
				{
					if (MayMoveAddOrDestroy)
					{
						if (!this.ParentObject.Inventory.SafeForeachObject(Proc))
						{
							return false;
						}
					}
					else if (!this.ParentObject.Inventory.ForeachObject(Proc))
					{
						return false;
					}
				}
				else if (MayMoveAddOrDestroy)
				{
					if (!this.ParentObject.Inventory.SafeForeachObject(Proc, new Predicate<GameObject>(this.WorksFor)))
					{
						return false;
					}
				}
				else if (!this.ParentObject.Inventory.ForeachObject(Proc, new Predicate<GameObject>(this.WorksFor)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600A199 RID: 41369 RVA: 0x003AE564 File Offset: 0x003AC764
		public virtual bool AnyActivePartSubjectWantsEvent(int ID, int cascade)
		{
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject) && this.ParentObject.WantEvent(ID, cascade))
			{
				return true;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped) && this.ParentObject.Equipped.WantEvent(ID, cascade))
			{
				return true;
			}
			if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped) && this.ParentObject.Equipped.WantEvent(ID, cascade))
			{
				return true;
			}
			if (this.WorksOnEquipper)
			{
				GameObject gameObject = this.ParentObject.EquippedProperlyBy();
				if (this.WorksFor(gameObject) && gameObject.WantEvent(ID, cascade))
				{
					return true;
				}
			}
			if (this.WorksOnCarrier && this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped) && this.ParentObject.Equipped.WantEvent(ID, cascade))
			{
				return true;
			}
			if (this.WorksOnCarrier && this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory) && this.ParentObject.InInventory.WantEvent(ID, cascade))
			{
				return true;
			}
			if (this.WorksOnImplantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && this.WorksFor(implantee) && implantee.WantEvent(ID, cascade))
				{
					return true;
				}
			}
			if (this.WorksOnCellContents)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					if (this.WorksForEveryone())
					{
						int i = 0;
						int count = currentCell.Objects.Count;
						while (i < count)
						{
							if (currentCell.Objects[i] != this.ParentObject && currentCell.Objects[i].WantEvent(ID, cascade))
							{
								return true;
							}
							i++;
						}
					}
					else
					{
						int j = 0;
						int count2 = currentCell.Objects.Count;
						while (j < count2)
						{
							if (currentCell.Objects[j] != this.ParentObject && this.WorksFor(currentCell.Objects[j]) && currentCell.Objects[j].WantEvent(ID, cascade))
							{
								return true;
							}
							j++;
						}
					}
				}
			}
			if (this.WorksOnAdjacentCellContents)
			{
				Cell currentCell2 = this.ParentObject.CurrentCell;
				if (currentCell2 != null)
				{
					List<Cell> adjacentCells = currentCell2.GetAdjacentCells(true);
					if (this.WorksForEveryone())
					{
						int k = 0;
						int count3 = adjacentCells.Count;
						while (k < count3)
						{
							Cell cell = adjacentCells[k];
							int l = 0;
							int count4 = cell.Objects.Count;
							while (l < count4)
							{
								if (cell.Objects[l] != this.ParentObject && cell.Objects[l].WantEvent(ID, cascade))
								{
									return true;
								}
								l++;
							}
							k++;
						}
					}
					else
					{
						int m = 0;
						int count5 = adjacentCells.Count;
						while (m < count5)
						{
							Cell cell2 = adjacentCells[m];
							int n = 0;
							int count6 = cell2.Objects.Count;
							while (n < count6)
							{
								if (cell2.Objects[n] != this.ParentObject && this.WorksFor(cell2.Objects[n]) && cell2.Objects[n].WantEvent(ID, cascade))
								{
									return true;
								}
								n++;
							}
							m++;
						}
					}
				}
			}
			if (this.WorksOnEnclosed && !this.WorksOnCellContents)
			{
				Cell currentCell3 = this.ParentObject.CurrentCell;
				if (currentCell3 != null)
				{
					if (this.WorksForEveryone())
					{
						int num = 0;
						int count7 = currentCell3.Objects.Count;
						while (num < count7)
						{
							if (currentCell3.Objects[num] != this.ParentObject && this.Encloses(currentCell3.Objects[num]) && currentCell3.Objects[num].WantEvent(ID, cascade))
							{
								return true;
							}
							num++;
						}
					}
					else
					{
						int num2 = 0;
						int count8 = currentCell3.Objects.Count;
						while (num2 < count8)
						{
							if (currentCell3.Objects[num2] != this.ParentObject && this.WorksFor(currentCell3.Objects[num2]) && this.Encloses(currentCell3.Objects[num2]) && currentCell3.Objects[num2].WantEvent(ID, cascade))
							{
								return true;
							}
							num2++;
						}
					}
				}
			}
			if (this.WorksOnInventory)
			{
				Inventory inventory = this.ParentObject.Inventory;
				if (inventory != null)
				{
					List<GameObject> objects = inventory.GetObjects();
					if (this.WorksForEveryone())
					{
						int num3 = 0;
						int count9 = objects.Count;
						while (num3 < count9)
						{
							if (objects[num3] != this.ParentObject && objects[num3].WantEvent(ID, cascade))
							{
								return true;
							}
							num3++;
						}
					}
					else
					{
						int num4 = 0;
						int count10 = objects.Count;
						while (num4 < count10)
						{
							if (objects[num4] != this.ParentObject && this.WorksFor(objects[num4]) && objects[num4].WantEvent(ID, cascade))
							{
								return true;
							}
							num4++;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600A19A RID: 41370 RVA: 0x003AEAB8 File Offset: 0x003ACCB8
		public virtual bool ActivePartSubjectsHandleEvent(MinEvent E)
		{
			if (this.WorksOnSelf && this.WorksFor(this.ParentObject) && !this.ParentObject.HandleEvent(E))
			{
				return false;
			}
			if (this.WorksOnHolder && this.ParentObject.IsHeld() && this.WorksFor(this.ParentObject.Equipped) && !this.ParentObject.Equipped.HandleEvent(E))
			{
				return false;
			}
			if (this.WorksOnWearer && this.ParentObject.IsWorn(null) && this.WorksFor(this.ParentObject.Equipped) && !this.ParentObject.Equipped.HandleEvent(E))
			{
				return false;
			}
			if (this.WorksOnEquipper)
			{
				GameObject gameObject = this.ParentObject.EquippedProperlyBy();
				if (this.WorksFor(gameObject) && !gameObject.HandleEvent(E))
				{
					return false;
				}
			}
			if (this.WorksOnCarrier && this.ParentObject.IsEquippedAsThrownWeapon() && this.WorksFor(this.ParentObject.Equipped) && !this.ParentObject.Equipped.HandleEvent(E))
			{
				return false;
			}
			if (this.WorksOnCarrier && this.ParentObject.InInventory != null && this.WorksFor(this.ParentObject.InInventory) && !this.ParentObject.InInventory.HandleEvent(E))
			{
				return false;
			}
			if (this.WorksOnImplantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && this.WorksFor(implantee) && !implantee.HandleEvent(E))
				{
					return false;
				}
			}
			if (this.WorksOnCellContents)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					if (this.WorksForEveryone())
					{
						int i = 0;
						int count = currentCell.Objects.Count;
						while (i < count)
						{
							if (currentCell.Objects[i] != this.ParentObject && !currentCell.Objects[i].HandleEvent(E))
							{
								return false;
							}
							i++;
						}
					}
					else
					{
						int j = 0;
						int count2 = currentCell.Objects.Count;
						while (j < count2)
						{
							if (currentCell.Objects[j] != this.ParentObject && this.WorksFor(currentCell.Objects[j]) && !currentCell.Objects[j].HandleEvent(E))
							{
								return false;
							}
							j++;
						}
					}
				}
			}
			if (this.WorksOnAdjacentCellContents)
			{
				Cell currentCell2 = this.ParentObject.CurrentCell;
				if (currentCell2 != null)
				{
					List<Cell> adjacentCells = currentCell2.GetAdjacentCells(true);
					if (this.WorksForEveryone())
					{
						int k = 0;
						int count3 = adjacentCells.Count;
						while (k < count3)
						{
							Cell cell = adjacentCells[k];
							int l = 0;
							int count4 = cell.Objects.Count;
							while (l < count4)
							{
								if (cell.Objects[l] != this.ParentObject && !cell.Objects[l].HandleEvent(E))
								{
									return false;
								}
								l++;
							}
							k++;
						}
					}
					else
					{
						int m = 0;
						int count5 = adjacentCells.Count;
						while (m < count5)
						{
							Cell cell2 = adjacentCells[m];
							int n = 0;
							int count6 = cell2.Objects.Count;
							while (n < count6)
							{
								if (cell2.Objects[n] != this.ParentObject && this.WorksFor(cell2.Objects[n]) && !cell2.Objects[n].HandleEvent(E))
								{
									return false;
								}
								n++;
							}
							m++;
						}
					}
				}
			}
			if (this.WorksOnEnclosed && !this.WorksOnCellContents)
			{
				Cell currentCell3 = this.ParentObject.CurrentCell;
				if (currentCell3 != null)
				{
					if (this.WorksForEveryone())
					{
						int num = 0;
						int count7 = currentCell3.Objects.Count;
						while (num < count7)
						{
							if (currentCell3.Objects[num] != this.ParentObject && this.Encloses(currentCell3.Objects[num]) && !currentCell3.Objects[num].HandleEvent(E))
							{
								return false;
							}
							num++;
						}
					}
					else
					{
						int num2 = 0;
						int count8 = currentCell3.Objects.Count;
						while (num2 < count8)
						{
							if (currentCell3.Objects[num2] != this.ParentObject && this.WorksFor(currentCell3.Objects[num2]) && this.Encloses(currentCell3.Objects[num2]) && !currentCell3.Objects[num2].HandleEvent(E))
							{
								return false;
							}
							num2++;
						}
					}
				}
			}
			if (this.WorksOnInventory)
			{
				Inventory inventory = this.ParentObject.Inventory;
				if (inventory != null)
				{
					List<GameObject> objects = inventory.GetObjects();
					if (this.WorksForEveryone())
					{
						int num3 = 0;
						int count9 = objects.Count;
						while (num3 < count9)
						{
							if (objects[num3] != this.ParentObject && !objects[num3].HandleEvent(E))
							{
								return false;
							}
							num3++;
						}
					}
					else
					{
						int num4 = 0;
						int count10 = objects.Count;
						while (num4 < count10)
						{
							if (objects[num4] != this.ParentObject && this.WorksFor(objects[num4]) && !objects[num4].HandleEvent(E))
							{
								return false;
							}
							num4++;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600A19B RID: 41371 RVA: 0x003AEFFD File Offset: 0x003AD1FD
		public virtual bool GetActivePartLocallyDefinedFailure()
		{
			return false;
		}

		// Token: 0x0600A19C RID: 41372 RVA: 0x003AF000 File Offset: 0x003AD200
		public virtual string GetActivePartLocallyDefinedFailureDescription()
		{
			return null;
		}

		// Token: 0x0600A19D RID: 41373 RVA: 0x003AF004 File Offset: 0x003AD204
		private ActivePartStatus GetActivePartStatusCore(bool UseCharge = false, bool IgnoreCharge = false, bool IgnoreLiquid = false, bool IgnoreBootSequence = false, bool IgnoreBreakage = false, bool IgnoreRust = false, bool IgnoreEMP = false, bool IgnoreRealityStabilization = false, bool IgnoreSubject = false, bool IgnoreLocallyDefinedFailure = false, int MultipleCharge = 1, int? ChargeUse = null, bool UseChargeIfUnpowered = false, long GridMask = 0L, int? PowerLoadLevel = null)
		{
			if (!IgnoreSubject && this.ActivePartNeedsSubject())
			{
				return ActivePartStatus.NeedsSubject;
			}
			if (this.IsPowerSwitchSensitive)
			{
				PowerSwitch part = this.ParentObject.GetPart<PowerSwitch>();
				if (part != null && !part.Active)
				{
					return ActivePartStatus.SwitchedOff;
				}
			}
			if (this.IsEMPSensitive && !IgnoreEMP && base.IsEMPed())
			{
				return ActivePartStatus.EMP;
			}
			if (this.IsBreakageSensitive && !IgnoreBreakage && base.IsBroken())
			{
				return ActivePartStatus.Broken;
			}
			if (this.IsRustSensitive && !IgnoreRust && base.IsRusted())
			{
				return ActivePartStatus.Rusted;
			}
			if (this.IsBootSensitive && !IgnoreBootSequence)
			{
				BootSequence part2 = this.ParentObject.GetPart<BootSequence>();
				if (part2 != null && part2.BootTimeLeft > 0 && part2.WasReadyIfKnown())
				{
					return ActivePartStatus.Booting;
				}
			}
			if (this.IsHangingSensitive)
			{
				Hangable part3 = this.ParentObject.GetPart<Hangable>();
				if (part3 != null && !part3.Hanging)
				{
					return ActivePartStatus.NotHanging;
				}
			}
			if (this.RequiresBodyPartCategoryCode != 0 && !this.ParentObject.IsEquippedOnCategory(this.RequiresBodyPartCategoryCode))
			{
				return ActivePartStatus.LimbIncompatible;
			}
			if (this.IsRealityDistortionBased && !IgnoreRealityStabilization)
			{
				GameObject gameObject = this.ParentObject.Equipped ?? this.ParentObject.Implantee;
				if (!IComponent<GameObject>.CheckRealityDistortionUsability(this.ParentObject, null, gameObject, this.ParentObject, null, null, 0))
				{
					return ActivePartStatus.RealityStabilized;
				}
				GameObject holder = this.ParentObject.Holder;
				if (holder != null && !IComponent<GameObject>.CheckRealityDistortionUsability(holder, null, gameObject, this.ParentObject, null, null, 0))
				{
					return ActivePartStatus.RealityStabilized;
				}
			}
			if (!IgnoreLocallyDefinedFailure && this.GetActivePartLocallyDefinedFailure())
			{
				return ActivePartStatus.LocallyDefinedFailure;
			}
			if (this.NeedsOtherActivePartOperational != null)
			{
				IActivePart activePart = this.ParentObject.GetPart(this.NeedsOtherActivePartOperational) as IActivePart;
				if (activePart == null || activePart.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return ActivePartStatus.PrimarySystemOffline;
				}
			}
			if (this.NeedsOtherActivePartEngaged != null)
			{
				IActivePart activePart2 = this.ParentObject.GetPart(this.NeedsOtherActivePartEngaged) as IActivePart;
				if (activePart2 == null || !activePart2.IsActivePartEngaged())
				{
					return ActivePartStatus.PrimarySystemOffline;
				}
			}
			if (!IgnoreCharge)
			{
				if (!IgnoreLiquid && !this.ConsumesLiquid.IsNullOrEmpty())
				{
					LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
					if (liquidVolume == null || liquidVolume.Volume <= 0)
					{
						return ActivePartStatus.ProcessInputMissing;
					}
					if (this.LiquidConsumptionAmount > liquidVolume.Volume && (!this.LiquidMustBePure || liquidVolume.IsPureLiquid(this.ConsumesLiquid, false)))
					{
						if (UseCharge && (this.LiquidConsumptionChanceOneIn == 1 || Stat.Random(1, this.LiquidConsumptionChanceOneIn) == 1))
						{
							base.ConsumeLiquid(this.ConsumesLiquid, this.LiquidConsumptionAmount, !this.LiquidMustBePure);
						}
						return ActivePartStatus.ProcessInputMissing;
					}
					if (this.LiquidMustBePure && !liquidVolume.IsPureLiquid(this.ConsumesLiquid, false))
					{
						return ActivePartStatus.ProcessInputInvalid;
					}
				}
				int activeChargeUse = this.GetActiveChargeUse(ref PowerLoadLevel, ChargeUse);
				if (this.ChargeMinimum > activeChargeUse && !this.ParentObject.TestCharge(this.ChargeMinimum, false, GridMask, true, true))
				{
					if (UseChargeIfUnpowered)
					{
						this.ParentObject.UseCharge(activeChargeUse, false, GridMask, true, true, PowerLoadLevel);
					}
					return ActivePartStatus.Unpowered;
				}
				if (activeChargeUse > 0)
				{
					bool flag;
					if (!UseCharge)
					{
						flag = this.ParentObject.TestCharge(activeChargeUse * MultipleCharge, false, GridMask, true, true);
						if (!flag && UseChargeIfUnpowered)
						{
							for (int i = 0; i < MultipleCharge; i++)
							{
								this.ParentObject.UseCharge(activeChargeUse, false, GridMask, true, true, PowerLoadLevel);
							}
						}
					}
					else if (MultipleCharge == 1)
					{
						flag = this.ParentObject.UseCharge(activeChargeUse, false, GridMask, true, true, PowerLoadLevel);
					}
					else
					{
						flag = this.ParentObject.UseCharge(activeChargeUse * MultipleCharge, false, GridMask, true, true, PowerLoadLevel);
						if (!flag)
						{
							int j = 0;
							int num = MultipleCharge - 1;
							while (j < num)
							{
								this.ParentObject.UseCharge(activeChargeUse, false, GridMask, true, true, PowerLoadLevel);
								j++;
							}
						}
					}
					if (!flag)
					{
						return ActivePartStatus.Unpowered;
					}
				}
				if (UseCharge && !IgnoreLiquid && !this.ConsumesLiquid.IsNullOrEmpty() && this.LiquidConsumptionChanceOneIn > 0 && this.LiquidConsumptionAmount > 0 && (this.LiquidConsumptionChanceOneIn == 1 || Stat.Random(1, this.LiquidConsumptionChanceOneIn) == 1))
				{
					base.ConsumeLiquid(this.ConsumesLiquid, this.LiquidConsumptionAmount, !this.LiquidMustBePure);
				}
			}
			return ActivePartStatus.Operational;
		}

		// Token: 0x0600A19E RID: 41374 RVA: 0x003AF41C File Offset: 0x003AD61C
		public ActivePartStatus GetActivePartStatus(bool UseCharge = false, bool IgnoreCharge = false, bool IgnoreLiquid = false, bool IgnoreBootSequence = false, bool IgnoreBreakage = false, bool IgnoreRust = false, bool IgnoreEMP = false, bool IgnoreRealityStabilization = false, bool IgnoreSubject = false, bool IgnoreLocallyDefinedFailure = false, int MultipleCharge = 1, int? ChargeUse = null, bool UseChargeIfUnpowered = false, long GridMask = 0L, int? PowerLoadLevel = null)
		{
			if (IgnoreCharge || IgnoreLiquid || IgnoreBootSequence || IgnoreBreakage || IgnoreRust || IgnoreEMP || IgnoreRealityStabilization || IgnoreSubject)
			{
				return this.GetActivePartStatusCore(UseCharge, IgnoreCharge, IgnoreLiquid, IgnoreBootSequence, IgnoreBreakage, IgnoreRust, IgnoreEMP, IgnoreRealityStabilization, IgnoreSubject, IgnoreLocallyDefinedFailure, MultipleCharge, ChargeUse, UseChargeIfUnpowered, GridMask, PowerLoadLevel);
			}
			ActivePartStatus? lastStatus = this.LastStatus;
			ActivePartStatus activePartStatusCore = this.GetActivePartStatusCore(UseCharge, IgnoreCharge, IgnoreLiquid, IgnoreBootSequence, IgnoreBreakage, IgnoreRust, IgnoreEMP, IgnoreRealityStabilization, IgnoreSubject, IgnoreLocallyDefinedFailure, MultipleCharge, ChargeUse, UseChargeIfUnpowered, GridMask, PowerLoadLevel);
			this.LastStatus = new ActivePartStatus?(activePartStatusCore);
			ActivePartStatus? lastStatus2 = this.LastStatus;
			ActivePartStatus? activePartStatus = lastStatus;
			if (!((lastStatus2.GetValueOrDefault() == activePartStatus.GetValueOrDefault()) & (lastStatus2 != null == (activePartStatus != null))))
			{
				this.SyncRender();
			}
			return activePartStatusCore;
		}

		// Token: 0x0600A19F RID: 41375 RVA: 0x003AF4C8 File Offset: 0x003AD6C8
		public bool IsDisabled(bool UseCharge = false, bool IgnoreCharge = false, bool IgnoreLiquid = false, bool IgnoreBootSequence = false, bool IgnoreBreakage = false, bool IgnoreRust = false, bool IgnoreEMP = false, bool IgnoreRealityStabilization = false, bool IgnoreSubject = false, bool IgnoreLocallyDefinedFailure = false, int MultipleCharge = 1, int? ChargeUse = null, bool UseChargeIfUnpowered = false, long GridMask = 0L, int? PowerLoadLevel = null)
		{
			return this.GetActivePartStatus(UseCharge, IgnoreCharge, IgnoreLiquid, IgnoreBootSequence, IgnoreBreakage, IgnoreRust, IgnoreEMP, IgnoreRealityStabilization, IgnoreSubject, IgnoreLocallyDefinedFailure, MultipleCharge, ChargeUse, UseChargeIfUnpowered, GridMask, PowerLoadLevel) > ActivePartStatus.Operational;
		}

		// Token: 0x0600A1A0 RID: 41376 RVA: 0x003AF4FC File Offset: 0x003AD6FC
		public bool IsReady(bool UseCharge = false, bool IgnoreCharge = false, bool IgnoreLiquid = false, bool IgnoreBootSequence = false, bool IgnoreBreakage = false, bool IgnoreRust = false, bool IgnoreEMP = false, bool IgnoreRealityStabilization = false, bool IgnoreSubject = false, bool IgnoreLocallyDefinedFailure = false, int MultipleCharge = 1, int? ChargeUse = null, bool UseChargeIfUnpowered = false, long GridMask = 0L, int? PowerLoadLevel = null)
		{
			return !this.IsDisabled(UseCharge, IgnoreCharge, IgnoreLiquid, IgnoreBootSequence, IgnoreBreakage, IgnoreRust, IgnoreEMP, IgnoreRealityStabilization, IgnoreSubject, IgnoreLocallyDefinedFailure, MultipleCharge, ChargeUse, UseChargeIfUnpowered, GridMask, PowerLoadLevel);
		}

		// Token: 0x0600A1A1 RID: 41377 RVA: 0x003AF530 File Offset: 0x003AD730
		public ActivePartStatus GetLastActivePartStatus()
		{
			if (this.LastStatus == null)
			{
				return this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			}
			return this.LastStatus.Value;
		}

		// Token: 0x0600A1A2 RID: 41378 RVA: 0x003AF57C File Offset: 0x003AD77C
		public int GetLastPowerLoadLevel()
		{
			if (this.LastPowerLoadLevel == null)
			{
				this.LastPowerLoadLevel = new int?(this.ParentObject.GetPowerLoadLevel());
			}
			return this.LastPowerLoadLevel.Value;
		}

		// Token: 0x0600A1A3 RID: 41379 RVA: 0x003AF5AC File Offset: 0x003AD7AC
		public bool WasReady()
		{
			return this.GetLastActivePartStatus() == ActivePartStatus.Operational;
		}

		// Token: 0x0600A1A4 RID: 41380 RVA: 0x003AF5B8 File Offset: 0x003AD7B8
		public bool WasReadyIfKnown()
		{
			ActivePartStatus? lastStatus = this.LastStatus;
			ActivePartStatus activePartStatus = ActivePartStatus.Operational;
			return (lastStatus.GetValueOrDefault() == activePartStatus) & (lastStatus != null);
		}

		// Token: 0x0600A1A5 RID: 41381 RVA: 0x003AF5E0 File Offset: 0x003AD7E0
		public bool WasDisabled()
		{
			return !this.WasReady();
		}

		// Token: 0x0600A1A6 RID: 41382 RVA: 0x003AF5EC File Offset: 0x003AD7EC
		public int GetActiveChargeUse(ref int? PowerLoadLevel, int? ChargeUse = null)
		{
			int num = ChargeUse ?? this.ChargeUse;
			if (this.IsPowerLoadSensitive)
			{
				int num2 = PowerLoadLevel ?? this.ParentObject.GetPowerLoadLevel();
				if (num2 != 100)
				{
					num = num * num2 / 100;
				}
				this.LastPowerLoadLevel = new int?(num2);
			}
			return num;
		}

		// Token: 0x0600A1A7 RID: 41383 RVA: 0x003AF65A File Offset: 0x003AD85A
		public int GetActiveChargeUse(int? ChargeUse = null, int? PowerLoadLevel = null)
		{
			return this.GetActiveChargeUse(ref PowerLoadLevel, ChargeUse);
		}

		// Token: 0x0600A1A8 RID: 41384 RVA: 0x003AF668 File Offset: 0x003AD868
		public bool ConsumeCharge(int? ChargeUse = null, int? PowerLoadLevel = null)
		{
			int activeChargeUse = this.GetActiveChargeUse(ChargeUse, PowerLoadLevel);
			return activeChargeUse <= 0 || this.ParentObject.UseCharge(activeChargeUse, false, 0L, true, true, null);
		}

		// Token: 0x0600A1A9 RID: 41385 RVA: 0x003AF6A0 File Offset: 0x003AD8A0
		public bool ConsumeCharge(int MultipleCharge, int? ChargeUse = null, int? PowerLoadLevel = null)
		{
			int activeChargeUse = this.GetActiveChargeUse(ChargeUse, PowerLoadLevel);
			return activeChargeUse <= 0 || this.ParentObject.UseCharge(activeChargeUse * MultipleCharge, false, 0L, true, true, null);
		}

		// Token: 0x0600A1AA RID: 41386 RVA: 0x003AF6D8 File Offset: 0x003AD8D8
		public bool ConsumeChargeIfOperational(bool IgnoreLiquid = false, bool IgnoreBootSequence = false, bool IgnoreBreakage = false, bool IgnoreRust = false, bool IgnoreEMP = false, bool IgnoreRealityStabilization = false, bool IgnoreSubject = false, bool IgnoreLocallyDefinedFailure = false, bool IgnoreWorldMap = false, int MultipleCharge = 1, int? ChargeUse = null, bool UseChargeIfUnpowered = false, int GridMask = 0, bool NeedStatusUpdate = false, int? PowerLoadLevel = null)
		{
			return (IgnoreWorldMap || !base.IsWorldMapActive) && ((!NeedStatusUpdate && this.GetActiveChargeUse(ChargeUse, PowerLoadLevel) <= 0) || this.GetActivePartStatus(true, false, IgnoreLiquid, IgnoreBootSequence, IgnoreBreakage, IgnoreRust, IgnoreEMP, IgnoreRealityStabilization, IgnoreSubject, IgnoreLocallyDefinedFailure, MultipleCharge, ChargeUse, UseChargeIfUnpowered, (long)GridMask, PowerLoadLevel) == ActivePartStatus.Operational);
		}

		// Token: 0x0600A1AB RID: 41387 RVA: 0x003AF72C File Offset: 0x003AD92C
		public virtual int GetDraw(QueryDrawEvent E = null)
		{
			if (this.ChargeUse <= 0 || !this.IsReady(false, true, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return 0;
			}
			return this.ChargeUse;
		}

		// Token: 0x0600A1AC RID: 41388 RVA: 0x003AF774 File Offset: 0x003AD974
		public virtual bool IsActivePartEngaged()
		{
			return this.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
		}

		// Token: 0x0600A1AD RID: 41389 RVA: 0x003AF7A8 File Offset: 0x003AD9A8
		public string GetStatusDescription()
		{
			ActivePartStatus activePartStatus = this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			string text = null;
			if (activePartStatus == ActivePartStatus.LocallyDefinedFailure)
			{
				text = this.GetActivePartLocallyDefinedFailureDescription();
			}
			if (text == null)
			{
				text = activePartStatus.ToString();
			}
			return StyledStatus.Format(this.NameForStatus ?? base.Name, text, this.StatusStyle);
		}

		// Token: 0x0600A1AE RID: 41390 RVA: 0x003AF818 File Offset: 0x003ADA18
		public virtual string GetStatusSummary(ActivePartStatus Status)
		{
			if (Status == ActivePartStatus.EMP)
			{
				return "{{W|EMP}}";
			}
			if (Status == ActivePartStatus.Unpowered)
			{
				return "{{K|unpowered}}";
			}
			if (Status == ActivePartStatus.ProcessInputMissing)
			{
				return "{{K|unfueled}}";
			}
			if (Status == ActivePartStatus.ProcessInputInvalid)
			{
				return "{{g|fuel contaminated}}";
			}
			if (Status == ActivePartStatus.SwitchedOff)
			{
				return "{{K|switched off}}";
			}
			if (Status == ActivePartStatus.Booting)
			{
				BootSequence part = this.ParentObject.GetPart<BootSequence>();
				if (part == null || part.IsObvious())
				{
					return "{{b|warming up}}";
				}
			}
			if (Status != ActivePartStatus.Operational && Status != ActivePartStatus.NeedsSubject)
			{
				return "{{r|nonfunctional}}";
			}
			return null;
		}

		// Token: 0x0600A1AF RID: 41391 RVA: 0x003AF88C File Offset: 0x003ADA8C
		public string GetStatusSummary()
		{
			return this.GetStatusSummary(this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null));
		}

		// Token: 0x0600A1B0 RID: 41392 RVA: 0x003AF8C5 File Offset: 0x003ADAC5
		public void AddStatusSummary(StringBuilder SB, string Summary)
		{
			if (!Summary.IsNullOrEmpty())
			{
				SB.Append(" (").Append(Summary).Append(')');
			}
		}

		// Token: 0x0600A1B1 RID: 41393 RVA: 0x003AF8E8 File Offset: 0x003ADAE8
		public void AddStatusSummary(StringBuilder SB)
		{
			this.AddStatusSummary(SB, this.GetStatusSummary());
		}

		// Token: 0x0600A1B2 RID: 41394 RVA: 0x003AF8F8 File Offset: 0x003ADAF8
		public Action<StringBuilder> GetEventSensitiveAddStatusSummary(IEvent E)
		{
			GetShortDescriptionEvent getShortDescriptionEvent = E as GetShortDescriptionEvent;
			if (((getShortDescriptionEvent != null) ? getShortDescriptionEvent.Context : null) == "Tinkering")
			{
				return delegate(StringBuilder SB)
				{
				};
			}
			return new Action<StringBuilder>(this.AddStatusSummary);
		}

		// Token: 0x0600A1B3 RID: 41395 RVA: 0x003AF950 File Offset: 0x003ADB50
		public virtual string GetStatusPhrase(ActivePartStatus Status)
		{
			if (Status == ActivePartStatus.EMP)
			{
				return "{{W|disabled by electromagnetic pulse}}";
			}
			if (Status == ActivePartStatus.Unpowered)
			{
				return "{{K|unpowered}}";
			}
			if (Status == ActivePartStatus.ProcessInputMissing)
			{
				return "{{K|unfueled}}";
			}
			if (Status == ActivePartStatus.ProcessInputInvalid)
			{
				return "{{g|disabled by fuel contamination}}";
			}
			if (Status == ActivePartStatus.SwitchedOff)
			{
				return "{{K|switched off}}";
			}
			if (Status == ActivePartStatus.Booting)
			{
				BootSequence part = this.ParentObject.GetPart<BootSequence>();
				if (part == null || part.IsObvious())
				{
					return "{{b|still warming up}}";
				}
			}
			return "{{r|nonfunctional}}";
		}

		// Token: 0x0600A1B4 RID: 41396 RVA: 0x003AF9B8 File Offset: 0x003ADBB8
		public string GetStatusPhrase()
		{
			return this.GetStatusPhrase(this.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null));
		}

		// Token: 0x0600A1B5 RID: 41397 RVA: 0x003AF9F4 File Offset: 0x003ADBF4
		public string GetOperationalScopeDescription()
		{
			IActivePart.relationalItems.Clear();
			IActivePart.overallItems.Clear();
			if (this.WorksOnEquipper || this.WorksOnImplantee)
			{
				IActivePart.relationalItems.Add("user");
			}
			else
			{
				if (this.WorksOnHolder)
				{
					IActivePart.relationalItems.Add("wielder");
				}
				if (this.WorksOnWearer)
				{
					IActivePart.relationalItems.Add("wearer");
				}
			}
			if (this.WorksOnCarrier)
			{
				IActivePart.relationalItems.Add("carrier");
			}
			if (this.WorksOnInventory)
			{
				IActivePart.relationalItems.Add("contents");
			}
			if (this.WorksOnAdjacentCellContents && this.WorksOnCellContents)
			{
				IActivePart.relationalItems.Add("vicinity");
			}
			else if (this.WorksOnCellContents)
			{
				IActivePart.relationalItems.Add("immediate vicinity");
			}
			else if (this.WorksOnAdjacentCellContents)
			{
				IActivePart.relationalItems.Add("nearby vicinity");
			}
			if (IActivePart.relationalItems.Count > 0)
			{
				IActivePart.overallItems.Add(this.ParentObject.its + " " + Grammar.MakeOrList(IActivePart.relationalItems, true));
			}
			if (this.WorksOnSelf)
			{
				IActivePart.overallItems.Add(this.ParentObject.itself);
			}
			if (this.WorksOnEnclosed)
			{
				IActivePart.overallItems.Add("someone enclosed within " + this.ParentObject.them);
			}
			if (IActivePart.overallItems.Count <= 0)
			{
				return this.ParentObject.its + " operating area";
			}
			return Grammar.MakeOrList(IActivePart.overallItems, true);
		}

		// Token: 0x0600A1B6 RID: 41398 RVA: 0x003AFB8C File Offset: 0x003ADD8C
		public override int MyPowerLoadBonus(int Load = -2147483648, int Baseline = 100, int Divisor = 150)
		{
			if (!this.IsPowerLoadSensitive)
			{
				return 0;
			}
			return base.MyPowerLoadBonus(Load, Baseline, Divisor);
		}

		// Token: 0x0600A1B7 RID: 41399 RVA: 0x003AFBA1 File Offset: 0x003ADDA1
		public override int MyPowerLoadLevel()
		{
			if (!this.IsPowerLoadSensitive)
			{
				return 100;
			}
			return base.MyPowerLoadLevel();
		}

		// Token: 0x0600A1B8 RID: 41400 RVA: 0x003AFBB4 File Offset: 0x003ADDB4
		public virtual bool WantsLiquidCollection(string LiquidType)
		{
			if (this.ConsumesLiquid != null)
			{
				if (this.ConsumesLiquid == LiquidType)
				{
					return true;
				}
				if (!this.LiquidMustBePure && LiquidType.IndexOf(',') != -1 && LiquidType.IndexOf(this.ConsumesLiquid) != -1)
				{
					using (List<string>.Enumerator enumerator = LiquidType.CachedCommaExpansion().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Split('-', StringSplitOptions.None)[0] == this.ConsumesLiquid)
							{
								return true;
							}
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x0400430D RID: 17165
		public int ChargeUse;

		// Token: 0x0400430E RID: 17166
		public int ChargeMinimum;

		// Token: 0x0400430F RID: 17167
		public int RequiresBodyPartCategoryCode;

		// Token: 0x04004310 RID: 17168
		public int LiquidConsumptionAmount = 1;

		// Token: 0x04004311 RID: 17169
		public int LiquidConsumptionChanceOneIn = 1;

		// Token: 0x04004312 RID: 17170
		public bool IsBootSensitive;

		// Token: 0x04004313 RID: 17171
		public bool IsBreakageSensitive = true;

		// Token: 0x04004314 RID: 17172
		public bool IsEMPSensitive;

		// Token: 0x04004315 RID: 17173
		public bool IsHangingSensitive;

		// Token: 0x04004316 RID: 17174
		public bool IsPowerSwitchSensitive;

		// Token: 0x04004317 RID: 17175
		public bool IsRealityDistortionBased;

		// Token: 0x04004318 RID: 17176
		public bool IsRustSensitive = true;

		// Token: 0x04004319 RID: 17177
		public bool IsPowerLoadSensitive;

		// Token: 0x0400431A RID: 17178
		public bool MustBeUnderstood;

		// Token: 0x0400431B RID: 17179
		public bool WorksOnAdjacentCellContents;

		// Token: 0x0400431C RID: 17180
		public bool WorksOnCarrier;

		// Token: 0x0400431D RID: 17181
		public bool WorksOnCellContents;

		// Token: 0x0400431E RID: 17182
		public bool WorksOnEnclosed;

		// Token: 0x0400431F RID: 17183
		public bool WorksOnEquipper;

		// Token: 0x04004320 RID: 17184
		public bool WorksOnHolder;

		// Token: 0x04004321 RID: 17185
		public bool WorksOnImplantee;

		// Token: 0x04004322 RID: 17186
		public bool WorksOnInventory;

		// Token: 0x04004323 RID: 17187
		public bool WorksOnSelf;

		// Token: 0x04004324 RID: 17188
		public bool WorksOnWearer;

		// Token: 0x04004325 RID: 17189
		public bool LiquidMustBePure = true;

		// Token: 0x04004326 RID: 17190
		public string ConsumesLiquid;

		// Token: 0x04004327 RID: 17191
		public string NeedsOtherActivePartOperational;

		// Token: 0x04004328 RID: 17192
		public string NeedsOtherActivePartEngaged;

		// Token: 0x04004329 RID: 17193
		public string DescribeStatusForProperty;

		// Token: 0x0400432A RID: 17194
		public string NameForStatus;

		// Token: 0x0400432B RID: 17195
		public string StatusStyle = "plain";

		// Token: 0x0400432C RID: 17196
		public string ReadyColorString;

		// Token: 0x0400432D RID: 17197
		public string ReadyDetailColor;

		// Token: 0x0400432E RID: 17198
		public string DisabledColorString;

		// Token: 0x0400432F RID: 17199
		public string DisabledDetailColor;

		// Token: 0x04004330 RID: 17200
		[NonSerialized]
		public ActivePartStatus? LastStatus;

		// Token: 0x04004331 RID: 17201
		[NonSerialized]
		public int? LastPowerLoadLevel;

		// Token: 0x04004332 RID: 17202
		private static List<string> relationalItems = new List<string>();

		// Token: 0x04004333 RID: 17203
		private static List<string> overallItems = new List<string>();
	}
}
