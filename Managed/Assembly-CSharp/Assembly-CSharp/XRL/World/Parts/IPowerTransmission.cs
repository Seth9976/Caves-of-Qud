using System;
using System.Collections.Generic;
using System.Text;
using XRL.Collections;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020011CE RID: 4558
	[Serializable]
	public abstract class IPowerTransmission : IPoweredPart
	{
		// Token: 0x0600B538 RID: 46392 RVA: 0x00421A1C File Offset: 0x0041FC1C
		public IPowerTransmission()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B539 RID: 46393
		public abstract string GetPowerTransmissionType();

		// Token: 0x0600B53A RID: 46394 RVA: 0x00421A7C File Offset: 0x0041FC7C
		public override bool SameAs(IPart p)
		{
			IPowerTransmission powerTransmission = p as IPowerTransmission;
			return !(powerTransmission.GetPowerTransmissionType() != this.GetPowerTransmissionType()) && powerTransmission.ChargeRate == this.ChargeRate && powerTransmission.ChanceBreakConnectedOnDestroy == this.ChanceBreakConnectedOnDestroy && powerTransmission.ChanceBreakConnectedOnMove == this.ChanceBreakConnectedOnMove && powerTransmission.ChanceBreakOnMove == this.ChanceBreakOnMove && !(powerTransmission.Substance != this.Substance) && !(powerTransmission.Activity != this.Activity) && !(powerTransmission.Constituent != this.Constituent) && !(powerTransmission.Assembly != this.Assembly) && !(powerTransmission.Unit != this.Unit) && powerTransmission.UnitFactor == this.UnitFactor && powerTransmission.DamageDegrades == this.DamageDegrades && powerTransmission.Readout == this.Readout && powerTransmission.IsProducer == this.IsProducer && powerTransmission.IsConsumer == this.IsConsumer && powerTransmission.IsConduit == this.IsConduit && powerTransmission.DischargeLiquidWhenBrokenAndPowered == this.DischargeLiquidWhenBrokenAndPowered && powerTransmission.DischargeLiquidWhenBrokenAndUnpowered == this.DischargeLiquidWhenBrokenAndUnpowered && powerTransmission.MingleLiquidsWhenPowered == this.MingleLiquidsWhenPowered && powerTransmission.MingleLiquidsWhenUnpowered == this.MingleLiquidsWhenUnpowered && powerTransmission.SparkWhenBrokenAndPowered == this.SparkWhenBrokenAndPowered && powerTransmission.TileEffects == this.TileEffects && !(powerTransmission.TileBaseFromTag != this.TileBaseFromTag) && !(powerTransmission.TileAppendWhenPowered != this.TileAppendWhenPowered) && !(powerTransmission.TileAppendWhenUnpowered != this.TileAppendWhenUnpowered) && !(powerTransmission.TileAppendWhenBroken != this.TileAppendWhenBroken) && !(powerTransmission.TileAppendWhenUnbroken != this.TileAppendWhenUnbroken) && !(powerTransmission.TileAppendWhenBrokenAndPowered != this.TileAppendWhenBrokenAndPowered) && !(powerTransmission.TileAppendWhenUnbrokenAndPowered != this.TileAppendWhenUnbrokenAndPowered) && !(powerTransmission.TileAppendWhenBrokenAndUnpowered != this.TileAppendWhenBrokenAndUnpowered) && !(powerTransmission.TileAppendWhenUnbrokenAndUnpowered != this.TileAppendWhenUnbrokenAndUnpowered) && powerTransmission.TileAnimateWhenPowered == this.TileAnimateWhenPowered && powerTransmission.TileAnimateWhenUnpowered == this.TileAnimateWhenUnpowered && powerTransmission.TileAnimateSuppressWhenBroken == this.TileAnimateSuppressWhenBroken && powerTransmission.TileAnimateSuppressWhenUnbroken == this.TileAnimateSuppressWhenUnbroken && powerTransmission.TileAnimatePoweredFrames == this.TileAnimatePoweredFrames && powerTransmission.TileAnimateUnpoweredFrames == this.TileAnimateUnpoweredFrames && base.SameAs(p);
		}

		// Token: 0x0600B53B RID: 46395 RVA: 0x00421D34 File Offset: 0x0041FF34
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == AllowLiquidCollectionEvent.ID && !this.DependsOnLiquid.IsNullOrEmpty()) || ID == PooledEvent<AnimateEvent>.ID || (ID == ChargeAvailableEvent.ID && this.IsProducer) || ID == EnteredCellEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || (ID == FinishChargeAvailableEvent.ID && this.IsProducer) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || (ID == GetPreferredLiquidEvent.ID && !this.DependsOnLiquid.IsNullOrEmpty()) || ID == GetShortDescriptionEvent.ID || ID == LeavingCellEvent.ID || (ID == PooledEvent<LiquidMixedEvent>.ID && !this.DependsOnLiquid.IsNullOrEmpty()) || ID == OnDestroyObjectEvent.ID || (ID == PowerSwitchFlippedEvent.ID && this.IsPowerSwitchSensitive) || (ID == SingletonEvent<RepaintedEvent>.ID && this.TileEffects && !this.TileBaseFromTag.IsNullOrEmpty()) || (ID == PooledEvent<StatChangeEvent>.ID && this.DamageDegrades) || (ID == QueryChargeEvent.ID && this.IsConsumer) || (ID == TestChargeEvent.ID && this.IsConsumer) || (ID == UseChargeEvent.ID && this.IsConsumer) || (ID == WantsLiquidCollectionEvent.ID && !this.DependsOnLiquid.IsNullOrEmpty());
		}

		// Token: 0x0600B53C RID: 46396 RVA: 0x00421E9B File Offset: 0x0042009B
		public override bool HandleEvent(AnimateEvent E)
		{
			this.ChanceBreakOnMove = 0;
			this.ChanceBreakConnectedOnDestroy = 0;
			this.ChanceBreakConnectedOnMove = 0;
			this.MingleLiquidsWhenPowered = false;
			this.MingleLiquidsWhenUnpowered = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B53D RID: 46397 RVA: 0x00421EC8 File Offset: 0x004200C8
		public override bool HandleEvent(QueryChargeEvent E)
		{
			if ((E.GridMask & this.GridBit) == 0L && this.IsConsumer)
			{
				bool flag = false;
				bool flag2 = true;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num = 1;
				long num2 = E.GridMask | this.GridBit;
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num, null, false, num2, null))
				{
					int num3 = this.GetCharge(E.IncludeTransient, E.IncludeBiological, E.GridMask, false) - this.ChargeUse * E.Multiple;
					if (num3 > 0)
					{
						E.Amount += num3;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B53E RID: 46398 RVA: 0x00421F64 File Offset: 0x00420164
		public override bool HandleEvent(TestChargeEvent E)
		{
			if ((E.GridMask & this.GridBit) == 0L && this.IsConsumer)
			{
				bool flag = false;
				bool flag2 = true;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num = 1;
				long num2 = E.GridMask | this.GridBit;
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num, null, false, num2, null))
				{
					int num3 = Math.Min(E.Amount, this.GetCharge(E.IncludeTransient, E.IncludeBiological, E.GridMask, false) - this.ChargeUse * E.Multiple);
					if (num3 > 0)
					{
						E.Amount -= num3;
						if (E.Amount <= 0)
						{
							return false;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B53F RID: 46399 RVA: 0x0042201C File Offset: 0x0042021C
		public override bool HandleEvent(UseChargeEvent E)
		{
			if (E.Pass == 1 && (E.GridMask & this.GridBit) == 0L && this.IsConsumer)
			{
				bool flag = false;
				bool flag2 = true;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num = 1;
				long num2 = E.GridMask | this.GridBit;
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num, null, false, num2, null))
				{
					int num3 = this.ChargeUse * E.Multiple;
					int num4 = Math.Min(E.Amount, this.GetCharge(E.IncludeTransient, E.IncludeBiological, E.GridMask, false) - num3);
					if (num4 > 0)
					{
						this.UseCharge(num4 + num3, E.GridMask);
						E.Amount -= num4;
						if (E.Amount <= 0)
						{
							return false;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B540 RID: 46400 RVA: 0x004220EF File Offset: 0x004202EF
		public override bool HandleEvent(ChargeAvailableEvent E)
		{
			this.Process(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B541 RID: 46401 RVA: 0x004220FF File Offset: 0x004202FF
		public override bool HandleEvent(FinishChargeAvailableEvent E)
		{
			this.Process(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B542 RID: 46402 RVA: 0x0042210F File Offset: 0x0042030F
		public override bool HandleEvent(LiquidMixedEvent E)
		{
			this.DependsOnLiquid.IsNullOrEmpty();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B543 RID: 46403 RVA: 0x00422124 File Offset: 0x00420324
		public override bool HandleEvent(RepaintedEvent E)
		{
			if (this.TileEffects && !this.TileBaseFromTag.IsNullOrEmpty())
			{
				string tag = this.ParentObject.GetTag(this.TileBaseFromTag, null);
				if (tag != null)
				{
					IPowerTransmission.TileSplitter[0] = tag;
					this.PaintedTileParts = this.ParentObject.Render.Tile.Split(IPowerTransmission.TileSplitter, 2, StringSplitOptions.None);
				}
				else
				{
					this.PaintedTileParts = null;
				}
				this.SyncTile();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B544 RID: 46404 RVA: 0x0042219C File Offset: 0x0042039C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.ResetGrid(false, true);
			Cell currentCell = this.ParentObject.CurrentCell;
			Zone zone = ((currentCell != null) ? currentCell.ParentZone : null);
			if (zone != null && zone.Built)
			{
				ZoneManager.PaintWalls(zone, currentCell.X - 1, currentCell.Y - 1, currentCell.X + 1, currentCell.Y + 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B545 RID: 46405 RVA: 0x00422202 File Offset: 0x00420402
		public override bool HandleEvent(LeavingCellEvent E)
		{
			this.ResetGrid(false, true);
			if (this.ChanceBreakOnMove.in100())
			{
				this.ParentObject.ApplyEffect(new Broken(), null);
			}
			if (this.ChanceBreakConnectedOnMove > 0)
			{
				this.ScanForBreakageOnMove();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B546 RID: 46406 RVA: 0x00422241 File Offset: 0x00420441
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600B547 RID: 46407 RVA: 0x0042225A File Offset: 0x0042045A
		public override bool HandleEvent(GetPreferredLiquidEvent E)
		{
			if (E.Liquid == null && !this.DependsOnLiquid.IsNullOrEmpty())
			{
				E.Liquid = this.DependsOnLiquid;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B548 RID: 46408 RVA: 0x00422284 File Offset: 0x00420484
		public override bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return !this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600B549 RID: 46409 RVA: 0x0042229D File Offset: 0x0042049D
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (E.Effect is IBusted)
			{
				if (The.Game != null)
				{
					this.ResetGrid(false, true);
				}
				if (The.Game != null)
				{
					this.CheckSparking();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B54A RID: 46410 RVA: 0x004222CF File Offset: 0x004204CF
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			if (E.Effect is IBusted)
			{
				this.ResetGrid(false, true);
				this.CheckSparking();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B54B RID: 46411 RVA: 0x004222F3 File Offset: 0x004204F3
		public override bool HandleEvent(StatChangeEvent E)
		{
			if (E.Name == "Hitpoints" && this.DamageDegrades)
			{
				this.ResetGrid(false, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B54C RID: 46412 RVA: 0x0042231E File Offset: 0x0042051E
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			if (this.IsPowerSwitchSensitive)
			{
				this.ResetGrid(false, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B54D RID: 46413 RVA: 0x00422338 File Offset: 0x00420538
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Power available", this.GetCharge(true, true, 0L, false));
			E.AddEntry(this, "Total production", this.GetTotalProduction());
			E.AddEntry(this, "Total availability", this.GetTotalAvailability(0L, false));
			E.AddEntry(this, "Total draw", this.GetTotalDraw());
			E.AddEntry(this, "Local capacity", this.GetEffectiveChargeRate());
			E.AddEntry(this, "Grid capacity", this.GridCapacity.ToString() + " " + ((this.GridCapacity == 1) ? this.Unit : Grammar.Pluralize(this.Unit)));
			E.AddEntry(this, "Grid bit", string.Format("0x{0:X}", this.GridBit));
			E.AddEntry(this, "Any charge activity", this.AnyChargeActivity(0L, false));
			E.AddEntry(this, "Should be sparking", this.ShouldBeSparking());
			E.AddEntry(this, "Any adjacent charge activity", this.AnyAdjacentChargeActivity());
			E.AddEntry(this, "Adjacent charge", this.GetAdjacentCharge(true, true, 0L, false, 0));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B54E RID: 46414 RVA: 0x00422460 File Offset: 0x00420660
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetBehaviorDescription());
			if (this.Readout || (!this.DescribeStatusForProperty.IsNullOrEmpty() && IComponent<GameObject>.ThePlayer != null && IComponent<GameObject>.ThePlayer.GetIntProperty(this.DescribeStatusForProperty, 0) > 0))
			{
				double charge = (double)this.GetCharge(true, true, 0L, false);
				int totalProduction = this.GetTotalProduction();
				int totalAvailability = this.GetTotalAvailability(0L, false);
				int totalDraw = this.GetTotalDraw();
				string text = "\n{{rules|";
				string text2 = (charge * this.UnitFactor).ToString("F").TrimEnd('0').TrimEnd('.');
				E.Postfix.Append(text).Append("Power available: ").Append(text2)
					.Append(' ')
					.Append((text2 == "1") ? this.Unit : Grammar.Pluralize(this.Unit));
				string text3 = ((double)totalProduction * this.UnitFactor).ToString("F").TrimEnd('0').TrimEnd('.');
				E.Postfix.Append(text).Append("Total ").Append(this.Assembly)
					.Append(" production: ")
					.Append(text3)
					.Append(' ')
					.Append((text3 == "1") ? this.Unit : Grammar.Pluralize(this.Unit));
				string text4 = ((double)totalAvailability * this.UnitFactor).ToString("F").TrimEnd('0').TrimEnd('.');
				E.Postfix.Append(text).Append("Total ").Append(this.Assembly)
					.Append(" availability: ")
					.Append(text4)
					.Append(' ')
					.Append((text4 == "1") ? this.Unit : Grammar.Pluralize(this.Unit));
				string text5 = ((double)totalDraw * this.UnitFactor).ToString("F").TrimEnd('0').TrimEnd('.');
				E.Postfix.Append(text).Append("Total ").Append(this.Assembly)
					.Append(" draw: ")
					.Append(text5)
					.Append(' ')
					.Append((text5 == "1") ? this.Unit : Grammar.Pluralize(this.Unit));
				string text6 = ((double)this.GetEffectiveChargeRate() * this.UnitFactor).ToString("F").TrimEnd('0').TrimEnd('.');
				E.Postfix.Append(text).Append("Local ").Append(this.Assembly)
					.Append(" capacity: ")
					.Append(text6)
					.Append(' ')
					.Append((text6 == "1") ? this.Unit : Grammar.Pluralize(this.Unit));
				E.Postfix.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B54F RID: 46415 RVA: 0x0042278B File Offset: 0x0042098B
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.ResetGrid(false, true);
			if (this.ChanceBreakConnectedOnDestroy > 0)
			{
				this.ScanForBreakageOnDestroy();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B550 RID: 46416 RVA: 0x004227AB File Offset: 0x004209AB
		public override bool Render(RenderEvent E)
		{
			if (this.TileEffects)
			{
				this.SyncTile();
			}
			if (this.SparkingActive)
			{
				this.MaybeEmitCosmeticSpark();
			}
			return true;
		}

		// Token: 0x0600B551 RID: 46417 RVA: 0x004227CA File Offset: 0x004209CA
		public void HadChargeActivity(long TurnNumber)
		{
			if (this.ChargeActivity != null)
			{
				this.ChargeActivity[0] = TurnNumber;
			}
		}

		// Token: 0x0600B552 RID: 46418 RVA: 0x004227E1 File Offset: 0x004209E1
		public void HadChargeActivity()
		{
			this.HadChargeActivity(XRLCore.CurrentTurn);
		}

		// Token: 0x0600B553 RID: 46419 RVA: 0x004227EE File Offset: 0x004209EE
		public bool AnyChargeActivity(long GridMask = 0L, bool IgnoreReady = false)
		{
			return (this.ChargeActivity != null && this.ChargeActivity[0] >= XRLCore.CurrentTurn - 1L) || this.AnyCharge(GridMask, IgnoreReady);
		}

		// Token: 0x0600B554 RID: 46420 RVA: 0x00422818 File Offset: 0x00420A18
		public bool AnyAdjacentChargeActivity()
		{
			if (this.AnyChargeActivity(0L, false))
			{
				return true;
			}
			if (this.AnyChargeActivity(this.ParentObject.CurrentCell))
			{
				return true;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			List<Cell> list = ((currentCell != null) ? currentCell.GetLocalAdjacentCells() : null);
			if (list != null)
			{
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					if (this.AnyChargeActivity(list[i]))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600B555 RID: 46421 RVA: 0x00422888 File Offset: 0x00420A88
		private bool AnyChargeActivity(Cell C)
		{
			if (C != null)
			{
				int i = 0;
				int count = C.Objects.Count;
				while (i < count)
				{
					GameObject gameObject = C.Objects[i];
					if (gameObject != this.ParentObject)
					{
						IPowerTransmission correspondingPart = this.GetCorrespondingPart(gameObject);
						if (correspondingPart != null && correspondingPart.AnyChargeActivity(0L, false))
						{
							return true;
						}
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600B556 RID: 46422 RVA: 0x004228E0 File Offset: 0x00420AE0
		public bool AnyCharge(long GridMask = 0L, bool IgnoreReady = false)
		{
			if ((GridMask & this.GridBit) == 0L)
			{
				if (!IgnoreReady)
				{
					bool flag = false;
					bool flag2 = true;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag10 = false;
					int num = 1;
					long num2 = GridMask | this.GridBit;
					if (!base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num, null, false, num2, null))
					{
						return false;
					}
				}
				this.CheckGrid();
				if (this.Producers != null)
				{
					int i = 0;
					int count = this.Producers.Count;
					while (i < count)
					{
						if (this.Producers[i].TestCharge(1, false, GridMask | this.GridBit, true, true))
						{
							return true;
						}
						i++;
					}
				}
			}
			return false;
		}

		// Token: 0x0600B557 RID: 46423 RVA: 0x00422972 File Offset: 0x00420B72
		public bool HasCharge(int Amount, bool IncludeTransient = true, bool IncludeBiological = true, bool IgnoreReady = false)
		{
			return this.GetCharge(IncludeTransient, IncludeBiological, 0L, IgnoreReady) >= Amount;
		}

		// Token: 0x0600B558 RID: 46424 RVA: 0x00422988 File Offset: 0x00420B88
		public int GetTotalAvailability(long GridMask = 0L, bool IgnoreReady = false)
		{
			int num = 0;
			if ((GridMask & this.GridBit) == 0L)
			{
				bool flag = false;
				bool flag2 = true;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num2 = 1;
				long num3 = GridMask | this.GridBit;
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, num3, null))
				{
					this.CheckGrid();
					if (this.Producers != null)
					{
						int i = 0;
						int count = this.Producers.Count;
						while (i < count)
						{
							num += this.Producers[i].QueryCharge(false, GridMask | this.GridBit, true, true);
							i++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600B559 RID: 46425 RVA: 0x00422A1C File Offset: 0x00420C1C
		public int GetTotalProduction()
		{
			int num = 0;
			this.CheckGrid();
			if (this.Producers != null)
			{
				for (int i = 0; i < this.Producers.Count; i++)
				{
					num += this.Producers[i].QueryChargeProduction();
				}
			}
			return num;
		}

		// Token: 0x0600B55A RID: 46426 RVA: 0x00422A64 File Offset: 0x00420C64
		public int GetCharge(bool IncludeTransient = true, bool IncludeBiological = true, long GridMask = 0L, bool IgnoreReady = false)
		{
			int num = 0;
			if ((GridMask & this.GridBit) == 0L)
			{
				if (!IgnoreReady)
				{
					bool flag = false;
					bool flag2 = true;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag10 = false;
					int num2 = 1;
					long num3 = GridMask | this.GridBit;
					if (!base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, num3, null))
					{
						return num;
					}
				}
				this.CheckGrid();
				if (this.Producers != null)
				{
					int num4 = 0;
					int i = 0;
					int count = this.Producers.Count;
					while (i < count)
					{
						int num5 = this.GridCapacity - num4;
						if (num5 > num)
						{
							int num6 = num5 - num;
							int num7 = this.Producers[i].QueryCharge(false, GridMask | this.GridBit, IncludeTransient, IncludeBiological);
							int num8 = Math.Min(num6, num7);
							if (num8 > 0)
							{
								num += num8;
								num4 += num8;
							}
						}
						i++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600B55B RID: 46427 RVA: 0x00422B30 File Offset: 0x00420D30
		public int GetAdjacentCharge(bool IncludeTransient = true, bool IncludeBiological = true, long GridMask = 0L, bool IgnoreReady = false, int MinIfActivity = 0)
		{
			int num = this.GetCharge(IncludeTransient, IncludeBiological, GridMask, IgnoreReady);
			int charge = this.GetCharge(this.ParentObject.CurrentCell, IncludeTransient, IncludeBiological, GridMask, IgnoreReady);
			if (charge > num)
			{
				num = charge;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			List<Cell> list = ((currentCell != null) ? currentCell.GetLocalAdjacentCells() : null);
			if (list != null)
			{
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					int charge2 = this.GetCharge(list[i], IncludeTransient, IncludeBiological, GridMask, IgnoreReady);
					if (charge2 > num)
					{
						num = charge2;
					}
					i++;
				}
			}
			if (num < MinIfActivity && (num > 0 || this.AnyAdjacentChargeActivity()))
			{
				num = MinIfActivity;
			}
			return num;
		}

		// Token: 0x0600B55C RID: 46428 RVA: 0x00422BC8 File Offset: 0x00420DC8
		private int GetCharge(Cell C, bool IncludeTransient = true, bool IncludeBiological = true, long GridMask = 0L, bool IgnoreReady = false)
		{
			int num = 0;
			if (C != null)
			{
				int i = 0;
				int count = C.Objects.Count;
				while (i < count)
				{
					GameObject gameObject = C.Objects[i];
					if (gameObject != this.ParentObject)
					{
						IPowerTransmission correspondingPart = this.GetCorrespondingPart(gameObject);
						if (correspondingPart != null)
						{
							int charge = correspondingPart.GetCharge(IncludeTransient, IncludeBiological, GridMask, IgnoreReady);
							if (charge > num)
							{
								num = charge;
							}
						}
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600B55D RID: 46429 RVA: 0x00422C2D File Offset: 0x00420E2D
		public int GetTotalDraw()
		{
			this.CheckGrid();
			return QueryDrawEvent.GetFor(this.Consumers);
		}

		// Token: 0x0600B55E RID: 46430 RVA: 0x00422C40 File Offset: 0x00420E40
		public void UseCharge(int Amount, long GridMask = 0L)
		{
			if ((GridMask & this.GridBit) == 0L)
			{
				this.CheckGrid();
				if (this.Producers != null)
				{
					int num = 0;
					int i = 0;
					int count = this.Producers.Count;
					while (i < count)
					{
						GameObject gameObject = this.Producers[i];
						int num2 = this.GridCapacity - num;
						if (num2 > 0)
						{
							int num3 = gameObject.QueryCharge(false, GridMask | this.GridBit, true, true);
							int num4 = Math.Min(Math.Min(num2, num3), Amount);
							if (num4 > 0)
							{
								gameObject.UseCharge(num4, false, GridMask | this.GridBit, true, true, null);
								Amount -= num4;
								num += num4;
								this.HadChargeActivity();
								if (Amount <= 0)
								{
									break;
								}
							}
						}
						i++;
					}
				}
			}
		}

		// Token: 0x0600B55F RID: 46431 RVA: 0x00422D00 File Offset: 0x00420F00
		public double GetPerformance()
		{
			if (!this.DamageDegrades && this.DependsOnLiquid.IsNullOrEmpty())
			{
				return 1.0;
			}
			double num = 1.0;
			if (this.DamageDegrades)
			{
				num *= this.ParentObject.Health();
			}
			if (!this.DependsOnLiquid.IsNullOrEmpty())
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume != null && liquidVolume.Volume > 0)
				{
					int num2 = 0;
					int num3 = 0;
					double? num4 = null;
					double? num5 = null;
					if (liquidVolume.ComponentLiquids.ContainsKey(this.DependsOnLiquid))
					{
						if (liquidVolume.ComponentLiquids.Count == 1)
						{
							num2 = liquidVolume.Volume;
						}
						else
						{
							num4 = new double?((double)(liquidVolume.Volume * liquidVolume.ComponentLiquids[this.DependsOnLiquid] / 1000));
							num5 = (double)liquidVolume.Volume - num4;
						}
					}
					else
					{
						num3 = liquidVolume.Volume;
					}
					if (num4 != null || num2 < liquidVolume.MaxVolume)
					{
						if (num4 == null)
						{
							num4 = new double?((double)num2);
						}
						double num6 = num4.Value / (double)liquidVolume.MaxVolume;
						if (this.WrongLiquidFactor > 0.0)
						{
							if (num5 == null)
							{
								num5 = new double?((double)num3);
							}
							num6 += num5.Value * this.WrongLiquidFactor / (double)liquidVolume.MaxVolume;
						}
						num *= num6;
					}
				}
				else
				{
					num = 0.0;
				}
			}
			return num;
		}

		// Token: 0x0600B560 RID: 46432 RVA: 0x00422EA4 File Offset: 0x004210A4
		public int GetEffectiveChargeRate()
		{
			int num = this.ChargeRate;
			double performance = this.GetPerformance();
			if (performance != 1.0)
			{
				num = (int)Math.Round((double)num * performance);
			}
			return num;
		}

		// Token: 0x0600B561 RID: 46433 RVA: 0x00422ED8 File Offset: 0x004210D8
		public void MaybeEmitCosmeticSpark()
		{
			if (this.AdjacentChargeTurn < XRLCore.CurrentTurn)
			{
				this.AdjacentChargeTurn = XRLCore.CurrentTurn;
				this.AdjacentCharge = this.GetAdjacentCharge(true, true, 0L, false, 100);
			}
			if (Stat.RandomCosmetic(1, 120) <= Math.Min(1 + this.AdjacentCharge / 200, 12))
			{
				this.EmitCosmeticSpark();
			}
		}

		// Token: 0x0600B562 RID: 46434 RVA: 0x00422F38 File Offset: 0x00421138
		public void EmitCosmeticSpark()
		{
			for (int i = 0; i < 2; i++)
			{
				this.ParentObject.ParticleText("&Y" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), 0.2f, 20);
			}
			for (int j = 0; j < 2; j++)
			{
				this.ParentObject.ParticleText("&W\u000f", 0.02f, 10);
			}
			base.PlayWorldSound("sfx_spark", 0.35f, 0.35f, false, null, 0f, 1f, 1f, int.MaxValue);
		}

		// Token: 0x0600B563 RID: 46435 RVA: 0x00422FD4 File Offset: 0x004211D4
		public void SyncTile()
		{
			if (!this.TileEffects)
			{
				return;
			}
			if (this.PaintedTileParts == null)
			{
				return;
			}
			if (this.TileBaseFromTag == null)
			{
				return;
			}
			IPowerTransmission.TileBuilder.Length = 0;
			IPowerTransmission.TileBuilder.Append(this.PaintedTileParts[0]);
			IPowerTransmission.TileBuilder.Append(this.ParentObject.GetTag(this.TileBaseFromTag, null));
			bool flag = false;
			bool flag2 = this.AnyChargeActivity(0L, false);
			if (flag2)
			{
				if (!this.TileAppendWhenBrokenAndPowered.IsNullOrEmpty() || !this.TileAppendWhenUnbrokenAndPowered.IsNullOrEmpty())
				{
					if (base.IsBroken())
					{
						if (!this.TileAppendWhenBrokenAndPowered.IsNullOrEmpty())
						{
							IPowerTransmission.TileBuilder.Append(this.TileAppendWhenBrokenAndPowered);
							flag = true;
						}
					}
					else if (!this.TileAppendWhenUnbrokenAndPowered.IsNullOrEmpty())
					{
						IPowerTransmission.TileBuilder.Append(this.TileAppendWhenUnbrokenAndPowered);
						flag = true;
					}
				}
				if (!flag && !this.TileAppendWhenPowered.IsNullOrEmpty())
				{
					IPowerTransmission.TileBuilder.Append(this.TileAppendWhenPowered);
				}
			}
			else
			{
				if (!this.TileAppendWhenBrokenAndUnpowered.IsNullOrEmpty() || !this.TileAppendWhenUnbrokenAndUnpowered.IsNullOrEmpty())
				{
					if (base.IsBroken())
					{
						if (!this.TileAppendWhenBrokenAndUnpowered.IsNullOrEmpty())
						{
							IPowerTransmission.TileBuilder.Append(this.TileAppendWhenBrokenAndUnpowered);
							flag = true;
						}
					}
					else if (!this.TileAppendWhenUnbrokenAndUnpowered.IsNullOrEmpty())
					{
						IPowerTransmission.TileBuilder.Append(this.TileAppendWhenUnbrokenAndUnpowered);
						flag = true;
					}
				}
				if (!flag && !this.TileAppendWhenUnpowered.IsNullOrEmpty())
				{
					IPowerTransmission.TileBuilder.Append(this.TileAppendWhenUnpowered);
				}
			}
			if (!flag && (!this.TileAppendWhenBroken.IsNullOrEmpty() || !this.TileAppendWhenUnbroken.IsNullOrEmpty()))
			{
				if (base.IsBroken())
				{
					if (!this.TileAppendWhenBroken.IsNullOrEmpty())
					{
						IPowerTransmission.TileBuilder.Append(this.TileAppendWhenBroken);
					}
				}
				else if (!this.TileAppendWhenUnbroken.IsNullOrEmpty())
				{
					IPowerTransmission.TileBuilder.Append(this.TileAppendWhenUnbroken);
				}
			}
			if ((flag2 ? this.TileAnimateWhenPowered : this.TileAnimateWhenUnpowered) && (!this.TileAnimateSuppressWhenBroken || !base.IsBroken()) && (!this.TileAnimateSuppressWhenUnbroken || base.IsBroken()))
			{
				int num = (flag2 ? this.TileAnimatePoweredFrames : this.TileAnimateUnpoweredFrames);
				if (num > 0)
				{
					long num2 = (long)XRLCore.CurrentFrameLong;
					if (this.TileAnimateDesyncFrame)
					{
						if (this.FrameOffset == -1)
						{
							this.FrameOffset = Stat.RandomCosmetic(0, 1000);
						}
						num2 = (num2 + (long)this.FrameOffset) % 1000L;
					}
					int num3 = 1000 / num;
					int num4 = (int)Math.Min(1L + num2 / (long)num3, (long)num);
					IPowerTransmission.TileBuilder.Append('_').Append(num4);
				}
			}
			if (this.PaintedTileParts.Length >= 2)
			{
				IPowerTransmission.TileBuilder.Append(this.PaintedTileParts[1]);
			}
			if (!IPowerTransmission.TileBuilder.CompareTo(this.ParentObject.Render.Tile))
			{
				this.ParentObject.Render.Tile = IPowerTransmission.TileBuilder.ToString();
			}
		}

		// Token: 0x0600B564 RID: 46436 RVA: 0x004232D0 File Offset: 0x004214D0
		private IPowerTransmission GetCorrespondingPart(GameObject obj)
		{
			string powerTransmissionType = this.GetPowerTransmissionType();
			int i = 0;
			int count = obj.PartsList.Count;
			while (i < count)
			{
				IPowerTransmission powerTransmission = obj.PartsList[i] as IPowerTransmission;
				if (((powerTransmission != null) ? powerTransmission.GetPowerTransmissionType() : null) == powerTransmissionType)
				{
					return obj.PartsList[i] as IPowerTransmission;
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600B565 RID: 46437 RVA: 0x00423334 File Offset: 0x00421534
		private bool TypeMatches(IPowerTransmission part)
		{
			return part.GetPowerTransmissionType() == this.GetPowerTransmissionType();
		}

		// Token: 0x0600B566 RID: 46438 RVA: 0x00423347 File Offset: 0x00421547
		private bool TypeMatches(GameObject obj)
		{
			return this.GetCorrespondingPart(obj) != null;
		}

		// Token: 0x0600B567 RID: 46439 RVA: 0x00423354 File Offset: 0x00421554
		public void FindGrid()
		{
			if (this.ParentObject.CurrentCell != null)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				bool? flag;
				if (currentCell == null)
				{
					flag = null;
				}
				else
				{
					Zone parentZone = currentCell.ParentZone;
					flag = ((parentZone != null) ? new bool?(parentZone.IsActive()) : null);
				}
				bool? flag2 = flag;
				if (flag2.GetValueOrDefault())
				{
					Cell currentCell2 = this.ParentObject.CurrentCell;
					bool? flag3;
					if (currentCell2 == null)
					{
						flag3 = null;
					}
					else
					{
						Zone parentZone2 = currentCell2.ParentZone;
						flag3 = ((parentZone2 != null) ? new bool?(parentZone2.IsWorldMap()) : null);
					}
					flag2 = flag3;
					if (!flag2.GetValueOrDefault())
					{
						if (this.Grid == null)
						{
							this.Grid = new List<GameObject> { this.ParentObject };
						}
						if (this.ChargeActivity == null)
						{
							this.ChargeActivity = new List<long> { 0L };
						}
						if (this.Producers == null)
						{
							this.Producers = new List<GameObject>();
						}
						if (this.Consumers == null)
						{
							this.Consumers = new List<GameObject>();
						}
						IPowerTransmission.Visited.Clear();
						IPowerTransmission.VisitNext.Clear();
						IPowerTransmission.VisitNext.Add(this.ParentObject.CurrentCell);
						this.GridCapacity = this.GetEffectiveChargeRate();
						while (IPowerTransmission.VisitNext.Count > 0)
						{
							IPowerTransmission.VisitNow.Clear();
							IPowerTransmission.VisitNow.AddRange(IPowerTransmission.VisitNext);
							IPowerTransmission.VisitNext.Clear();
							int i = 0;
							int count = IPowerTransmission.VisitNow.Count;
							while (i < count)
							{
								Cell cell = IPowerTransmission.VisitNow[i];
								IPowerTransmission.Visited.Add(cell);
								int j = 0;
								int count2 = cell.Objects.Count;
								while (j < count2)
								{
									GameObject gameObject = cell.Objects[j];
									IPowerTransmission correspondingPart = this.GetCorrespondingPart(gameObject);
									if (correspondingPart != null)
									{
										IActivePart activePart = correspondingPart;
										bool flag4 = false;
										bool flag5 = true;
										bool flag6 = false;
										bool flag7 = false;
										bool flag8 = false;
										bool flag9 = false;
										bool flag10 = false;
										bool flag11 = false;
										bool flag12 = false;
										bool flag13 = false;
										int num = 1;
										long gridBit = this.GridBit;
										if (activePart.IsReady(flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag12, flag13, num, null, false, gridBit, null))
										{
											int effectiveChargeRate = correspondingPart.GetEffectiveChargeRate();
											if (effectiveChargeRate > 0)
											{
												if (gameObject != this.ParentObject)
												{
													this.Grid.Add(gameObject);
													if (this.GridBit == 0L)
													{
														this.GridBit = IPowerTransmission.GridBitBase;
														IPowerTransmission.GridBitBase <<= 1;
														if (IPowerTransmission.GridBitBase == 0L)
														{
															IPowerTransmission.GridBitBase = 1L;
														}
													}
													correspondingPart.GridBit = this.GridBit;
													correspondingPart.Grid = this.Grid;
													correspondingPart.ChargeActivity = this.ChargeActivity;
													correspondingPart.Producers = this.Producers;
													correspondingPart.Consumers = this.Consumers;
												}
												if (this.GridCapacity > effectiveChargeRate)
												{
													this.GridCapacity = effectiveChargeRate;
												}
												if (correspondingPart.IsProducer && !this.Producers.Contains(gameObject))
												{
													this.Producers.Add(gameObject);
												}
												if (correspondingPart.IsConsumer && !this.Consumers.Contains(gameObject))
												{
													this.Consumers.Add(gameObject);
												}
												Cell currentCell3 = gameObject.CurrentCell;
												if (currentCell3 != null)
												{
													int k = 0;
													int num2 = Cell.DirectionListCardinalOnly.Length;
													while (k < num2)
													{
														Cell localCellFromDirection = currentCell3.GetLocalCellFromDirection(Cell.DirectionListCardinalOnly[k], true);
														if (localCellFromDirection != null && !IPowerTransmission.Visited.Contains(localCellFromDirection) && !IPowerTransmission.VisitNext.Contains(localCellFromDirection) && !IPowerTransmission.VisitNow.Contains(localCellFromDirection) && localCellFromDirection.HasObject(new Predicate<GameObject>(this.TypeMatches)))
														{
															IPowerTransmission.VisitNext.Add(localCellFromDirection);
														}
														k++;
													}
												}
											}
										}
									}
									j++;
								}
								i++;
							}
						}
						int l = 0;
						int count3 = this.Grid.Count;
						while (l < count3)
						{
							GameObject gameObject2 = this.Grid[l];
							IPowerTransmission correspondingPart2 = this.GetCorrespondingPart(gameObject2);
							if (correspondingPart2 != null)
							{
								correspondingPart2.GridCapacity = this.GridCapacity;
							}
							l++;
						}
						return;
					}
				}
			}
		}

		// Token: 0x0600B568 RID: 46440 RVA: 0x00423733 File Offset: 0x00421933
		public void CheckGrid()
		{
			if (this.GridBit == 0L)
			{
				this.FindGrid();
			}
		}

		// Token: 0x0600B569 RID: 46441 RVA: 0x00423744 File Offset: 0x00421944
		public void ResetGrid(bool Local = false, bool Initial = true)
		{
			if (this.ParentObject.GetLongProperty("LastReset", 0L) == The.Game.Turns)
			{
				return;
			}
			this.ParentObject.SetLongProperty("LastReset", The.Game.Turns);
			if (!Local)
			{
				if (this.Grid != null)
				{
					int i = 0;
					int count = this.Grid.Count;
					while (i < count)
					{
						GameObject gameObject = this.Grid[i];
						IPowerTransmission correspondingPart = this.GetCorrespondingPart(gameObject);
						if (correspondingPart != null && correspondingPart.GridBit == this.GridBit)
						{
							correspondingPart.ResetGrid(true, true);
						}
						i++;
					}
				}
				this.ResetGrid(this.ParentObject.CurrentCell);
				Cell currentCell = this.ParentObject.CurrentCell;
				List<Cell> list = ((currentCell != null) ? currentCell.GetLocalAdjacentCells() : null);
				if (list != null)
				{
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						this.ResetGrid(list[j]);
						j++;
					}
				}
			}
			this.GridBit = 0L;
			this.GridCapacity = 0;
			this.ChargeActivity = null;
			this.Producers = null;
			this.Consumers = null;
			if (Initial)
			{
				PowerUpdatedEvent.Send(this.ParentObject.CurrentZone, this.ParentObject);
			}
		}

		// Token: 0x0600B56A RID: 46442 RVA: 0x00423874 File Offset: 0x00421A74
		private void ResetGrid(Cell C)
		{
			if (C != null)
			{
				int i = 0;
				int count = C.Objects.Count;
				while (i < count)
				{
					GameObject gameObject = C.Objects[i];
					if (gameObject.GetLongProperty("LastReset", 0L) != The.Game.Turns && gameObject != this.ParentObject)
					{
						IPowerTransmission correspondingPart = this.GetCorrespondingPart(gameObject);
						if (correspondingPart != null && correspondingPart.GridBit != 0L)
						{
							correspondingPart.ResetGrid(false, false);
						}
					}
					i++;
				}
			}
		}

		// Token: 0x0600B56B RID: 46443 RVA: 0x004238E8 File Offset: 0x00421AE8
		private void ScanForBreakageOnDestroy(GameObject obj)
		{
			if (obj == this.ParentObject)
			{
				return;
			}
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone == null || !currentZone.Built)
			{
				return;
			}
			IPowerTransmission correspondingPart = this.GetCorrespondingPart(obj);
			if (correspondingPart == null || correspondingPart.IsBroken())
			{
				return;
			}
			if (this.ChanceBreakConnectedOnDestroy.in100())
			{
				obj.ApplyEffect(new Broken(), null);
			}
		}

		// Token: 0x0600B56C RID: 46444 RVA: 0x0042394A File Offset: 0x00421B4A
		private void ScanForBreakageOnDestroy(Cell C)
		{
			C.ForeachObject(new Action<GameObject>(this.ScanForBreakageOnDestroy));
		}

		// Token: 0x0600B56D RID: 46445 RVA: 0x0042395E File Offset: 0x00421B5E
		private void ScanForBreakageOnDestroyFrom(Cell C)
		{
			this.ScanForBreakageOnDestroy(C);
			C.ForeachCardinalAdjacentCell(new Action<Cell>(this.ScanForBreakageOnDestroy));
		}

		// Token: 0x0600B56E RID: 46446 RVA: 0x00423979 File Offset: 0x00421B79
		private void ScanForBreakageOnDestroy()
		{
			if (this.ParentObject.CurrentCell != null)
			{
				this.ScanForBreakageOnDestroyFrom(this.ParentObject.CurrentCell);
			}
		}

		// Token: 0x0600B56F RID: 46447 RVA: 0x0042399C File Offset: 0x00421B9C
		private void ScanForBreakageOnMove(GameObject obj)
		{
			if (obj == this.ParentObject)
			{
				return;
			}
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone == null || !currentZone.Built)
			{
				return;
			}
			IPowerTransmission correspondingPart = this.GetCorrespondingPart(obj);
			if (correspondingPart == null || correspondingPart.IsBroken())
			{
				return;
			}
			if (this.ChanceBreakConnectedOnMove.in100())
			{
				obj.ApplyEffect(new Broken(), null);
			}
		}

		// Token: 0x0600B570 RID: 46448 RVA: 0x004239FE File Offset: 0x00421BFE
		private void ScanForBreakageOnMove(Cell C)
		{
			C.ForeachObject(new Action<GameObject>(this.ScanForBreakageOnMove));
		}

		// Token: 0x0600B571 RID: 46449 RVA: 0x00423A12 File Offset: 0x00421C12
		private void ScanForBreakageOnMoveFrom(Cell C)
		{
			this.ScanForBreakageOnMove(C);
			C.ForeachCardinalAdjacentCell(new Action<Cell>(this.ScanForBreakageOnMove));
		}

		// Token: 0x0600B572 RID: 46450 RVA: 0x00423A2D File Offset: 0x00421C2D
		private void ScanForBreakageOnMove()
		{
			if (this.ParentObject.CurrentCell != null)
			{
				this.ScanForBreakageOnMoveFrom(this.ParentObject.CurrentCell);
			}
		}

		// Token: 0x0600B573 RID: 46451 RVA: 0x00423A4D File Offset: 0x00421C4D
		public bool ShouldBeSparking()
		{
			return this.SparkWhenBrokenAndPowered && base.IsBroken() && this.AnyAdjacentChargeActivity();
		}

		// Token: 0x0600B574 RID: 46452 RVA: 0x00423A6E File Offset: 0x00421C6E
		public bool SyncSparking()
		{
			if (this.SparkingActive)
			{
				if (!this.ShouldBeSparking())
				{
					this.SparkingActive = false;
				}
			}
			else if (this.ShouldBeSparking())
			{
				this.SparkingActive = true;
			}
			return this.SparkingActive;
		}

		// Token: 0x0600B575 RID: 46453 RVA: 0x00423AA0 File Offset: 0x00421CA0
		public void Spark()
		{
			if (this.ParentObject.CurrentCell == null)
			{
				return;
			}
			Cell randomLocalAdjacentCell = this.ParentObject.CurrentCell.GetRandomLocalAdjacentCell();
			if (randomLocalAdjacentCell != null)
			{
				int adjacentCharge = this.GetAdjacentCharge(true, true, 0L, false, 100);
				int num = Stat.Random(1, Math.Min(1 + adjacentCharge / 10, 5));
				string text = "1d" + Math.Min(4 + adjacentCharge / 50, 20).ToString();
				if (adjacentCharge >= 200)
				{
					text += Math.Min(adjacentCharge / 200, 10).Signed(false);
				}
				this.CheckGrid();
				this.ParentObject.Discharge(randomLocalAdjacentCell, num, 0, text, null, this.ParentObject, this.ParentObject, null, null, null, this.Grid, null, 0, false, false, false);
			}
			this.ParentObject.ParticleBlip("&K*", 10, 0L, false);
		}

		// Token: 0x0600B576 RID: 46454 RVA: 0x00423B80 File Offset: 0x00421D80
		public void MaybeSpark()
		{
			if (this.ParentObject.CurrentCell == null)
			{
				return;
			}
			if (IPowerTransmission.lastSparkTurn == (long)XRLCore.CurrentFrameLong)
			{
				return;
			}
			int adjacentCharge = this.GetAdjacentCharge(true, true, 0L, false, 0);
			if (adjacentCharge > 1000 || Stat.Random(1, 1000) < adjacentCharge)
			{
				IPowerTransmission.lastSparkTurn = (long)XRLCore.CurrentFrameLong;
				this.Spark();
			}
		}

		// Token: 0x0600B577 RID: 46455 RVA: 0x00423BDD File Offset: 0x00421DDD
		public void CheckSparking()
		{
			if (this.SyncSparking())
			{
				this.MaybeSpark();
			}
		}

		// Token: 0x0600B578 RID: 46456 RVA: 0x00423BF0 File Offset: 0x00421DF0
		public bool ShouldDischargeLiquid()
		{
			if (!this.DischargeLiquidWhenBrokenAndPowered && !this.DischargeLiquidWhenBrokenAndUnpowered)
			{
				return false;
			}
			if (this.DischargeLiquidWhenBrokenAndPowered && this.DischargeLiquidWhenBrokenAndUnpowered)
			{
				return base.IsBroken();
			}
			return (this.AnyChargeActivity(0L, false) ? this.DischargeLiquidWhenBrokenAndPowered : this.DischargeLiquidWhenBrokenAndUnpowered) && base.IsBroken();
		}

		// Token: 0x0600B579 RID: 46457 RVA: 0x00423C49 File Offset: 0x00421E49
		private int DischargeLiquidForceFrom(IPowerTransmission pt, LiquidVolume lv)
		{
			return Math.Min(Math.Max(this.GetCharge(true, true, 0L, true), Math.Max(lv.Volume / 8, 1)), lv.Volume);
		}

		// Token: 0x0600B57A RID: 46458 RVA: 0x00423C74 File Offset: 0x00421E74
		private int DischargeLiquidForceFrom(GameObject obj)
		{
			IPowerTransmission correspondingPart = this.GetCorrespondingPart(obj);
			if (correspondingPart == null)
			{
				return 0;
			}
			LiquidVolume liquidVolume = obj.LiquidVolume;
			if (liquidVolume == null || liquidVolume.Volume <= 1)
			{
				return 0;
			}
			return this.DischargeLiquidForceFrom(correspondingPart, liquidVolume);
		}

		// Token: 0x0600B57B RID: 46459 RVA: 0x00423CAC File Offset: 0x00421EAC
		private int DischargeLiquidForceFrom(Cell C)
		{
			if (C == null)
			{
				return 0;
			}
			GameObject firstObject = C.GetFirstObject(new Predicate<GameObject>(this.TypeMatches));
			if (firstObject == null)
			{
				return 0;
			}
			int num = this.DischargeLiquidForceFrom(firstObject);
			using (Container<GameObject>.Enumerator enumerator = C.Objects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != firstObject)
					{
						int num2 = this.DischargeLiquidForceFrom(firstObject);
						if (num2 > num)
						{
							num = num2;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600B57C RID: 46460 RVA: 0x00423D30 File Offset: 0x00421F30
		private int DischargeLiquidForceFrom(string Direction)
		{
			return this.DischargeLiquidForceFrom(this.ParentObject.CurrentCell.GetCellFromDirection(Direction, true));
		}

		// Token: 0x0600B57D RID: 46461 RVA: 0x00423D4A File Offset: 0x00421F4A
		private int Randomize(int num)
		{
			if (num <= 1)
			{
				return num;
			}
			return Stat.Random(1, num);
		}

		// Token: 0x0600B57E RID: 46462 RVA: 0x00423D59 File Offset: 0x00421F59
		private int CalmDown(int num)
		{
			if (num <= 0)
			{
				return 0;
			}
			if (num <= 2)
			{
				return num - 1;
			}
			return Stat.Random(1, num - 1);
		}

		// Token: 0x0600B57F RID: 46463 RVA: 0x00423D74 File Offset: 0x00421F74
		public void DischargeLiquid()
		{
			if (this.ParentObject.CurrentCell == null)
			{
				return;
			}
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume == null || liquidVolume.Volume <= 1)
			{
				return;
			}
			int num = this.Randomize(this.DischargeLiquidForceFrom("S"));
			int num2 = this.Randomize(this.DischargeLiquidForceFrom("N"));
			int num3 = this.Randomize(this.DischargeLiquidForceFrom("W"));
			int num4 = this.Randomize(this.DischargeLiquidForceFrom("E"));
			int num5 = this.Randomize(this.DischargeLiquidForceFrom(this, liquidVolume));
			while (num + num2 + num3 + num4 + num5 > liquidVolume.Volume)
			{
				num = this.CalmDown(num);
				num2 = this.CalmDown(num2);
				num3 = this.CalmDown(num3);
				num4 = this.CalmDown(num4);
				if (num + num2 + num3 + num4 + num5 > liquidVolume.Volume)
				{
					num5 = this.CalmDown(num5);
				}
			}
			if (num > 0)
			{
				liquidVolume.PourIntoCell(this.ParentObject, this.ParentObject.CurrentCell.GetCellFromDirection("N", true), num, true, false, false, false);
			}
			if (num2 > 0)
			{
				liquidVolume.PourIntoCell(this.ParentObject, this.ParentObject.CurrentCell.GetCellFromDirection("S", true), num2, true, false, false, false);
			}
			if (num3 > 0)
			{
				liquidVolume.PourIntoCell(this.ParentObject, this.ParentObject.CurrentCell.GetCellFromDirection("E", true), num3, true, false, false, false);
			}
			if (num4 > 0)
			{
				liquidVolume.PourIntoCell(this.ParentObject, this.ParentObject.CurrentCell.GetCellFromDirection("W", true), num4, true, false, false, false);
			}
			if (num5 > 0)
			{
				liquidVolume.PourIntoCell(this.ParentObject, this.ParentObject.CurrentCell, num5, true, false, false, false);
			}
		}

		// Token: 0x0600B580 RID: 46464 RVA: 0x00423F2A File Offset: 0x0042212A
		public void CheckDischargeLiquid()
		{
			if (this.ShouldDischargeLiquid())
			{
				this.DischargeLiquid();
			}
		}

		// Token: 0x0600B581 RID: 46465 RVA: 0x00423F3A File Offset: 0x0042213A
		public bool ShouldMingleLiquids()
		{
			if (!this.MingleLiquidsWhenPowered && !this.MingleLiquidsWhenUnpowered)
			{
				return false;
			}
			if (this.MingleLiquidsWhenPowered && this.MingleLiquidsWhenUnpowered)
			{
				return true;
			}
			if (!this.AnyChargeActivity(0L, false))
			{
				return this.MingleLiquidsWhenUnpowered;
			}
			return this.MingleLiquidsWhenPowered;
		}

		// Token: 0x0600B582 RID: 46466 RVA: 0x00423F78 File Offset: 0x00422178
		public void MingleScan(GameObject obj)
		{
			if (this.GetCorrespondingPart(obj) == null)
			{
				return;
			}
			LiquidVolume liquidVolume = obj.LiquidVolume;
			if (liquidVolume == null)
			{
				return;
			}
			LiquidVolume liquidVolume2 = this.ParentObject.LiquidVolume;
			if (liquidVolume2 == null)
			{
				return;
			}
			liquidVolume2.MingleAdjacent(liquidVolume);
		}

		// Token: 0x0600B583 RID: 46467 RVA: 0x00423FB4 File Offset: 0x004221B4
		public void MingleScan(Cell C)
		{
			int num = C.Objects.Count - 1;
			while (num >= 0 && num < C.Objects.Count)
			{
				this.MingleScan(C.Objects[num]);
				num--;
			}
		}

		// Token: 0x0600B584 RID: 46468 RVA: 0x00423FFC File Offset: 0x004221FC
		public void MingleLiquids()
		{
			if (!this.ParentObject.HasPart<LiquidVolume>())
			{
				return;
			}
			if (this.ParentObject.CurrentCell == null)
			{
				return;
			}
			List<Cell> adjacentCells = this.ParentObject.CurrentCell.GetAdjacentCells(true);
			for (int i = 0; i < adjacentCells.Count; i++)
			{
				this.MingleScan(adjacentCells[i]);
			}
		}

		// Token: 0x0600B585 RID: 46469 RVA: 0x00424055 File Offset: 0x00422255
		public void CheckMingleLiquids()
		{
			if (this.ShouldMingleLiquids())
			{
				this.MingleLiquids();
			}
		}

		// Token: 0x0600B586 RID: 46470 RVA: 0x00424068 File Offset: 0x00422268
		public string GetBehaviorDescription()
		{
			if (this.IsConduit)
			{
				return string.Concat(new string[]
				{
					"Functions as part of ",
					Grammar.A(this.Assembly, false),
					", ",
					this.Activity,
					" ",
					this.GetPowerTransmissionType(),
					" ",
					this.Substance,
					" between locations."
				});
			}
			if (this.IsProducer)
			{
				return string.Concat(new string[]
				{
					"Contains ",
					this.Constituent,
					" enabling ",
					this.ParentObject.them,
					" to function as part of ",
					Grammar.A(this.Assembly, false),
					", producing ",
					this.GetPowerTransmissionType(),
					" ",
					this.Substance,
					"."
				});
			}
			if (this.IsConsumer)
			{
				return string.Concat(new string[]
				{
					"Contains ",
					this.Constituent,
					" enabling ",
					this.ParentObject.them,
					" to function as part of ",
					Grammar.A(this.Assembly, false),
					", consuming ",
					this.GetPowerTransmissionType(),
					" ",
					this.Substance,
					"."
				});
			}
			return string.Concat(new string[]
			{
				"Contains ",
				this.Constituent,
				" enabling ",
				this.ParentObject.them,
				" to function as part of ",
				Grammar.A(this.Assembly, false),
				", ",
				this.Activity,
				" ",
				this.GetPowerTransmissionType(),
				" ",
				this.Substance,
				" between locations."
			});
		}

		// Token: 0x0600B587 RID: 46471 RVA: 0x00424263 File Offset: 0x00422463
		public bool IsLiquidCollectionCompatible(string LiquidType)
		{
			return this.DependsOnLiquid.IsNullOrEmpty() || this.DependsOnLiquid == LiquidType;
		}

		// Token: 0x0600B588 RID: 46472 RVA: 0x00424280 File Offset: 0x00422480
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B589 RID: 46473 RVA: 0x00424283 File Offset: 0x00422483
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.TurnProcessing();
		}

		// Token: 0x0600B58A RID: 46474 RVA: 0x0042428C File Offset: 0x0042248C
		private void TurnProcessing()
		{
			Cell cell = this.ParentObject.FastGetCurrentCell();
			if (cell != null && !cell.ParentZone.IsActive())
			{
				return;
			}
			this.CheckSparking();
			this.CheckMingleLiquids();
			this.CheckDischargeLiquid();
		}

		// Token: 0x0600B58B RID: 46475 RVA: 0x004242C8 File Offset: 0x004224C8
		public long GetGridBit()
		{
			return this.GridBit;
		}

		// Token: 0x0600B58C RID: 46476 RVA: 0x004242D0 File Offset: 0x004224D0
		public List<GameObject> GetGrid()
		{
			this.CheckGrid();
			return this.Grid;
		}

		// Token: 0x0600B58D RID: 46477 RVA: 0x004242E0 File Offset: 0x004224E0
		private void Process(IChargeProductionEvent E)
		{
			if (this.IsProducer)
			{
				long gridMask = E.GridMask;
				if ((gridMask & this.GridBit) == 0L)
				{
					bool flag = false;
					bool flag2 = true;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag10 = false;
					int num = 1;
					long num2 = E.GridMask | this.GridBit;
					if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num, null, false, num2, null))
					{
						this.CheckGrid();
						if (this.Consumers != null && this.Consumers.Count > 0)
						{
							int amount = E.Amount;
							if (amount > 0)
							{
								int num3 = amount;
								int num4 = 0;
								int id = E.GetID();
								int cascadeLevel = E.GetCascadeLevel();
								E.GridMask |= this.GridBit;
								try
								{
									int i = 0;
									int count = this.Consumers.Count;
									while (i < count)
									{
										GameObject gameObject = this.Consumers[i];
										if (gameObject.WantEvent(id, cascadeLevel))
										{
											int gridCapacity = this.GridCapacity;
											if (gridCapacity > num4)
											{
												int num5 = gridCapacity - num4;
												int num6 = ((num3 > num5) ? num5 : num3);
												E.Amount = num6;
												gameObject.HandleEvent(E);
												int amount2 = E.Amount;
												if (amount2 < num6)
												{
													int num7 = num6 - amount2;
													num4 += num7;
													num3 -= num7;
													this.HadChargeActivity();
													if (num3 <= 0)
													{
														break;
													}
												}
											}
										}
										i++;
									}
								}
								finally
								{
									E.Amount = num3;
									E.GridMask = gridMask;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0400490B RID: 18699
		public int ChargeRate = 100;

		// Token: 0x0400490C RID: 18700
		public int ChanceBreakConnectedOnDestroy = 50;

		// Token: 0x0400490D RID: 18701
		public int ChanceBreakConnectedOnMove = 50;

		// Token: 0x0400490E RID: 18702
		public int ChanceBreakOnMove = 100;

		// Token: 0x0400490F RID: 18703
		public string Substance;

		// Token: 0x04004910 RID: 18704
		public string Activity;

		// Token: 0x04004911 RID: 18705
		public string Constituent;

		// Token: 0x04004912 RID: 18706
		public string Assembly;

		// Token: 0x04004913 RID: 18707
		public string Unit;

		// Token: 0x04004914 RID: 18708
		public string DependsOnLiquid;

		// Token: 0x04004915 RID: 18709
		public double UnitFactor = 0.1;

		// Token: 0x04004916 RID: 18710
		public double WrongLiquidFactor;

		// Token: 0x04004917 RID: 18711
		public bool IsProducer;

		// Token: 0x04004918 RID: 18712
		public bool IsConsumer;

		// Token: 0x04004919 RID: 18713
		public bool IsConduit;

		// Token: 0x0400491A RID: 18714
		public bool DamageDegrades = true;

		// Token: 0x0400491B RID: 18715
		public bool Readout;

		// Token: 0x0400491C RID: 18716
		public bool DischargeLiquidWhenBrokenAndPowered;

		// Token: 0x0400491D RID: 18717
		public bool DischargeLiquidWhenBrokenAndUnpowered;

		// Token: 0x0400491E RID: 18718
		public bool MingleLiquidsWhenPowered;

		// Token: 0x0400491F RID: 18719
		public bool MingleLiquidsWhenUnpowered;

		// Token: 0x04004920 RID: 18720
		public bool SparkWhenBrokenAndPowered;

		// Token: 0x04004921 RID: 18721
		public bool TileEffects;

		// Token: 0x04004922 RID: 18722
		public string TileBaseFromTag;

		// Token: 0x04004923 RID: 18723
		public string TileAppendWhenPowered;

		// Token: 0x04004924 RID: 18724
		public string TileAppendWhenUnpowered;

		// Token: 0x04004925 RID: 18725
		public string TileAppendWhenBroken;

		// Token: 0x04004926 RID: 18726
		public string TileAppendWhenUnbroken;

		// Token: 0x04004927 RID: 18727
		public string TileAppendWhenBrokenAndPowered;

		// Token: 0x04004928 RID: 18728
		public string TileAppendWhenUnbrokenAndPowered;

		// Token: 0x04004929 RID: 18729
		public string TileAppendWhenBrokenAndUnpowered;

		// Token: 0x0400492A RID: 18730
		public string TileAppendWhenUnbrokenAndUnpowered;

		// Token: 0x0400492B RID: 18731
		public bool TileAnimateWhenPowered;

		// Token: 0x0400492C RID: 18732
		public bool TileAnimateWhenUnpowered;

		// Token: 0x0400492D RID: 18733
		public bool TileAnimateSuppressWhenBroken;

		// Token: 0x0400492E RID: 18734
		public bool TileAnimateSuppressWhenUnbroken;

		// Token: 0x0400492F RID: 18735
		public int TileAnimatePoweredFrames;

		// Token: 0x04004930 RID: 18736
		public int TileAnimateUnpoweredFrames;

		// Token: 0x04004931 RID: 18737
		public bool TileAnimateDesyncFrame;

		// Token: 0x04004932 RID: 18738
		public int FrameOffset = -1;

		// Token: 0x04004933 RID: 18739
		public bool SparkingActive;

		// Token: 0x04004934 RID: 18740
		public string[] PaintedTileParts;

		// Token: 0x04004935 RID: 18741
		[NonSerialized]
		private static long GridBitBase = 1L;

		// Token: 0x04004936 RID: 18742
		[NonSerialized]
		private long GridBit;

		// Token: 0x04004937 RID: 18743
		[NonSerialized]
		private List<GameObject> Grid;

		// Token: 0x04004938 RID: 18744
		[NonSerialized]
		private int GridCapacity;

		// Token: 0x04004939 RID: 18745
		[NonSerialized]
		private List<GameObject> Producers;

		// Token: 0x0400493A RID: 18746
		[NonSerialized]
		private List<GameObject> Consumers;

		// Token: 0x0400493B RID: 18747
		[NonSerialized]
		private List<long> ChargeActivity;

		// Token: 0x0400493C RID: 18748
		private static string[] TileSplitter = new string[1];

		// Token: 0x0400493D RID: 18749
		[NonSerialized]
		private long AdjacentChargeTurn;

		// Token: 0x0400493E RID: 18750
		[NonSerialized]
		private int AdjacentCharge;

		// Token: 0x0400493F RID: 18751
		private static StringBuilder TileBuilder = new StringBuilder(64);

		// Token: 0x04004940 RID: 18752
		private static List<Cell> Visited = new List<Cell>();

		// Token: 0x04004941 RID: 18753
		private static List<Cell> VisitNow = new List<Cell>();

		// Token: 0x04004942 RID: 18754
		private static List<Cell> VisitNext = new List<Cell>();

		// Token: 0x04004943 RID: 18755
		private static long lastSparkTurn = -1L;
	}
}
