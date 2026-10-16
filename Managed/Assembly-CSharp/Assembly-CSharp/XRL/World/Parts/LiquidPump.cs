using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000FA9 RID: 4009
	[Serializable]
	public class LiquidPump : IPoweredPart
	{
		// Token: 0x06009F58 RID: 40792 RVA: 0x0039E8C8 File Offset: 0x0039CAC8
		public LiquidPump()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x06009F59 RID: 40793 RVA: 0x0039E8E4 File Offset: 0x0039CAE4
		public override bool SameAs(IPart p)
		{
			LiquidPump liquidPump = p as LiquidPump;
			return liquidPump.Rate == this.Rate && !(liquidPump.Liquid != this.Liquid) && !(liquidPump.VariableRate != this.VariableRate) && !(liquidPump.PerTick != this.PerTick) && !(liquidPump.FromDirection != this.FromDirection) && !(liquidPump.ToDirection != this.ToDirection) && liquidPump.PreferCollectors == this.PreferCollectors && liquidPump.PureOnFloor == this.PureOnFloor && liquidPump.FillSelfOnly == this.FillSelfOnly && liquidPump.StickyFromDirection == this.StickyFromDirection && liquidPump.StickyToDirection == this.StickyToDirection && base.SameAs(p);
		}

		// Token: 0x06009F5A RID: 40794 RVA: 0x0039E9C8 File Offset: 0x0039CBC8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == AllowLiquidCollectionEvent.ID && !string.IsNullOrEmpty(this.Liquid)) || (ID == GetPreferredLiquidEvent.ID && !string.IsNullOrEmpty(this.Liquid)) || ID == ObjectCreatedEvent.ID || (ID == WantsLiquidCollectionEvent.ID && !string.IsNullOrEmpty(this.Liquid));
		}

		// Token: 0x06009F5B RID: 40795 RVA: 0x0039EA2B File Offset: 0x0039CC2B
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x06009F5C RID: 40796 RVA: 0x0039EA44 File Offset: 0x0039CC44
		public override bool HandleEvent(GetPreferredLiquidEvent E)
		{
			if (E.Liquid == null)
			{
				E.Liquid = this.Liquid;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F5D RID: 40797 RVA: 0x0039EA61 File Offset: 0x0039CC61
		public override bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return !this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x06009F5E RID: 40798 RVA: 0x0039EA7A File Offset: 0x0039CC7A
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (!string.IsNullOrEmpty(this.VariableRate))
			{
				this.Rate = this.VariableRate.RollCached();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F5F RID: 40799 RVA: 0x0039EAA1 File Offset: 0x0039CCA1
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009F60 RID: 40800 RVA: 0x0039EAA4 File Offset: 0x0039CCA4
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.IsNeeded() && base.IsReady(true, false, false, false, false, false, false, false, false, false, Amount, null, false, 0L, null))
			{
				if (string.IsNullOrEmpty(this.PerTick))
				{
					this.Tick += Amount;
				}
				else
				{
					DieRoll cachedDieRoll = this.PerTick.GetCachedDieRoll();
					for (int i = 0; i < Amount; i++)
					{
						this.Tick += cachedDieRoll.Resolve();
					}
				}
				while (this.Tick >= this.Rate)
				{
					this.Tick -= this.Rate;
					if (!this.DistributeLiquid())
					{
						this.Tick = 0;
						return;
					}
					if (!string.IsNullOrEmpty(this.VariableRate))
					{
						this.Rate = this.VariableRate.RollCached();
					}
				}
			}
		}

		// Token: 0x06009F61 RID: 40801 RVA: 0x0039EB80 File Offset: 0x0039CD80
		private LiquidVolume GetPumpableLiquidVolume(Cell C, int Phase)
		{
			if (C.IsSolid(true, Phase))
			{
				return null;
			}
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				LiquidVolume liquidVolume = C.Objects[i].LiquidVolume;
				if (liquidVolume != null && liquidVolume.MaxVolume == -1 && (string.IsNullOrEmpty(this.Liquid) || liquidVolume.IsPureLiquid(this.Liquid, false)))
				{
					return liquidVolume;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06009F62 RID: 40802 RVA: 0x0039EBEE File Offset: 0x0039CDEE
		private LiquidVolume GetPumpableLiquidVolume(Cell C)
		{
			return this.GetPumpableLiquidVolume(C, this.ParentObject.GetPhase());
		}

		// Token: 0x06009F63 RID: 40803 RVA: 0x0039EC02 File Offset: 0x0039CE02
		private bool HasPumpableLiquid(Cell C, int Phase)
		{
			return this.GetPumpableLiquidVolume(C, Phase) != null;
		}

		// Token: 0x06009F64 RID: 40804 RVA: 0x0039EC0F File Offset: 0x0039CE0F
		private bool HasPumpableLiquid(Cell C)
		{
			return this.HasPumpableLiquid(C, this.ParentObject.GetPhase());
		}

		// Token: 0x06009F65 RID: 40805 RVA: 0x0039EC24 File Offset: 0x0039CE24
		private LiquidVolume GetLiquidVolumeToPumpFrom(ref Cell CC, int Phase)
		{
			if (CC == null)
			{
				CC = this.ParentObject.GetCurrentCell();
				if (CC == null)
				{
					return null;
				}
			}
			if (!string.IsNullOrEmpty(this.FromDirection))
			{
				Cell cellFromDirection = CC.GetCellFromDirection(this.FromDirection, true);
				if (cellFromDirection != null)
				{
					LiquidVolume pumpableLiquidVolume = this.GetPumpableLiquidVolume(cellFromDirection, Phase);
					if (pumpableLiquidVolume != null)
					{
						return pumpableLiquidVolume;
					}
				}
			}
			else
			{
				List<Cell> localAdjacentCells = CC.GetLocalAdjacentCells();
				List<Cell> list = Event.NewCellList();
				int i = 0;
				int count = localAdjacentCells.Count;
				while (i < count)
				{
					if (this.HasPumpableLiquid(localAdjacentCells[i], Phase))
					{
						list.Add(localAdjacentCells[i]);
					}
					i++;
				}
				Cell randomElement = list.GetRandomElement<Cell>(null);
				if (randomElement != null)
				{
					LiquidVolume pumpableLiquidVolume2 = this.GetPumpableLiquidVolume(randomElement, Phase);
					if (pumpableLiquidVolume2 != null)
					{
						if (this.StickyFromDirection)
						{
							this.FromDirection = CC.GetDirectionFromCell(randomElement, false);
						}
						return pumpableLiquidVolume2;
					}
				}
			}
			return null;
		}

		// Token: 0x06009F66 RID: 40806 RVA: 0x0039ECF6 File Offset: 0x0039CEF6
		private LiquidVolume GetLiquidVolumeToPumpFrom(Cell CC, int Phase)
		{
			return this.GetLiquidVolumeToPumpFrom(ref CC, Phase);
		}

		// Token: 0x06009F67 RID: 40807 RVA: 0x0039ED04 File Offset: 0x0039CF04
		private bool HasSomeplaceToPumpFrom(ref Cell CC, int Phase)
		{
			if (CC == null)
			{
				CC = this.ParentObject.GetCurrentCell();
				if (CC == null)
				{
					return false;
				}
			}
			if (!string.IsNullOrEmpty(this.FromDirection))
			{
				Cell cellFromDirection = CC.GetCellFromDirection(this.FromDirection, true);
				if (cellFromDirection != null && this.HasPumpableLiquid(cellFromDirection, Phase))
				{
					return true;
				}
			}
			else
			{
				List<Cell> localAdjacentCells = CC.GetLocalAdjacentCells();
				int i = 0;
				int count = localAdjacentCells.Count;
				while (i < count)
				{
					if (this.HasPumpableLiquid(localAdjacentCells[i], Phase))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06009F68 RID: 40808 RVA: 0x0039ED81 File Offset: 0x0039CF81
		private bool HasSomeplaceToPumpFrom(Cell CC, int Phase)
		{
			return this.HasSomeplaceToPumpFrom(ref CC, Phase);
		}

		// Token: 0x06009F69 RID: 40809 RVA: 0x0039ED8C File Offset: 0x0039CF8C
		private bool HasSomeplaceToPumpFrom(Cell CC)
		{
			return this.HasSomeplaceToPumpFrom(ref CC, this.ParentObject.GetPhase());
		}

		// Token: 0x06009F6A RID: 40810 RVA: 0x0039EDA4 File Offset: 0x0039CFA4
		private bool HasSomeplaceToPumpFrom()
		{
			Cell cell = null;
			return this.HasSomeplaceToPumpFrom(cell);
		}

		// Token: 0x06009F6B RID: 40811 RVA: 0x0039EDBC File Offset: 0x0039CFBC
		private bool CanPumpTo(Cell C, int Phase)
		{
			if (C == null)
			{
				return false;
			}
			bool flag = false;
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = C.Objects[i];
				if (gameObject.ConsiderSolid(true, Phase))
				{
					LiquidVolume liquidVolume = gameObject.LiquidVolume;
					if (liquidVolume != null && liquidVolume.Collector && !liquidVolume.EffectivelySealed())
					{
						return true;
					}
					flag = true;
				}
				i++;
			}
			return !flag;
		}

		// Token: 0x06009F6C RID: 40812 RVA: 0x0039EE25 File Offset: 0x0039D025
		private bool CanPumpTo(Cell C)
		{
			return this.CanPumpTo(C, this.ParentObject.GetPhase());
		}

		// Token: 0x06009F6D RID: 40813 RVA: 0x0039EE3C File Offset: 0x0039D03C
		private LiquidVolume GetDestinationLiquidVolume(Cell C, int Phase)
		{
			bool flag = false;
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = C.Objects[i];
				if (gameObject.ConsiderSolid(true, Phase))
				{
					LiquidVolume liquidVolume = gameObject.LiquidVolume;
					if (liquidVolume != null && liquidVolume.Collector && !liquidVolume.EffectivelySealed())
					{
						return liquidVolume;
					}
					flag = true;
				}
				i++;
			}
			if (flag)
			{
				return null;
			}
			int j = 0;
			int count2 = C.Objects.Count;
			while (j < count2)
			{
				LiquidVolume liquidVolume2 = C.Objects[j].LiquidVolume;
				if (liquidVolume2 != null && liquidVolume2.Collector && !liquidVolume2.EffectivelySealed())
				{
					return liquidVolume2;
				}
				j++;
			}
			int k = 0;
			int count3 = C.Objects.Count;
			while (k < count3)
			{
				LiquidVolume liquidVolume3 = C.Objects[k].LiquidVolume;
				if (liquidVolume3 != null && liquidVolume3.MaxVolume == -1)
				{
					return liquidVolume3;
				}
				k++;
			}
			return null;
		}

		// Token: 0x06009F6E RID: 40814 RVA: 0x0039EF31 File Offset: 0x0039D131
		private LiquidVolume GetDestinationLiquidVolume(Cell C)
		{
			return this.GetDestinationLiquidVolume(C, this.ParentObject.GetPhase());
		}

		// Token: 0x06009F6F RID: 40815 RVA: 0x0039EF48 File Offset: 0x0039D148
		private LiquidVolume GetDestinationCollectorLiquidVolume(Cell C, int Phase)
		{
			bool flag = false;
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = C.Objects[i];
				if (gameObject.ConsiderSolid(true, Phase))
				{
					LiquidVolume liquidVolume = gameObject.LiquidVolume;
					if (liquidVolume != null && liquidVolume.Collector && !liquidVolume.EffectivelySealed())
					{
						return liquidVolume;
					}
					flag = true;
				}
				i++;
			}
			if (flag)
			{
				return null;
			}
			int j = 0;
			int count2 = C.Objects.Count;
			while (j < count2)
			{
				LiquidVolume liquidVolume2 = C.Objects[j].LiquidVolume;
				if (liquidVolume2 != null && liquidVolume2.Collector && !liquidVolume2.EffectivelySealed())
				{
					return liquidVolume2;
				}
				j++;
			}
			return null;
		}

		// Token: 0x06009F70 RID: 40816 RVA: 0x0039EFFA File Offset: 0x0039D1FA
		private LiquidVolume GetDestinationCollectorLiquidVolume(Cell C)
		{
			return this.GetDestinationCollectorLiquidVolume(C, this.ParentObject.GetPhase());
		}

		// Token: 0x06009F71 RID: 40817 RVA: 0x0039F010 File Offset: 0x0039D210
		private LiquidVolume GetLiquidVolumeToPumpTo(ref Cell CC, int Phase)
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume != null && (liquidVolume.MaxVolume == -1 || liquidVolume.Volume < liquidVolume.MaxVolume) && (string.IsNullOrEmpty(this.Liquid) || liquidVolume.IsPureLiquid(this.Liquid, false)))
			{
				return liquidVolume;
			}
			if (this.FillSelfOnly)
			{
				return null;
			}
			if (CC == null)
			{
				CC = this.ParentObject.GetCurrentCell();
				if (CC == null)
				{
					return null;
				}
			}
			if (!string.IsNullOrEmpty(this.ToDirection))
			{
				Cell cellFromDirection = CC.GetCellFromDirection(this.ToDirection, true);
				if (cellFromDirection != null)
				{
					LiquidVolume destinationLiquidVolume = this.GetDestinationLiquidVolume(cellFromDirection, Phase);
					if (destinationLiquidVolume != null)
					{
						return destinationLiquidVolume;
					}
				}
			}
			else
			{
				List<Cell> localAdjacentCells = CC.GetLocalAdjacentCells();
				if (this.PreferCollectors)
				{
					List<Cell> list = Event.NewCellList();
					int i = 0;
					int count = localAdjacentCells.Count;
					while (i < count)
					{
						if (this.GetDestinationCollectorLiquidVolume(localAdjacentCells[i], Phase) != null)
						{
							list.Add(localAdjacentCells[i]);
						}
						i++;
					}
					Cell randomElement = list.GetRandomElement<Cell>(null);
					if (randomElement != null)
					{
						LiquidVolume destinationCollectorLiquidVolume = this.GetDestinationCollectorLiquidVolume(randomElement);
						if (destinationCollectorLiquidVolume != null)
						{
							if (this.StickyToDirection)
							{
								this.ToDirection = CC.GetDirectionFromCell(randomElement, false);
							}
							return destinationCollectorLiquidVolume;
						}
					}
				}
				List<Cell> list2 = Event.NewCellList();
				int j = 0;
				int count2 = localAdjacentCells.Count;
				while (j < count2)
				{
					if (this.CanPumpTo(localAdjacentCells[j], Phase))
					{
						list2.Add(localAdjacentCells[j]);
					}
					j++;
				}
				Cell randomElement2 = list2.GetRandomElement<Cell>(null);
				if (randomElement2 != null)
				{
					if (this.StickyToDirection)
					{
						this.ToDirection = CC.GetDirectionFromCell(randomElement2, false);
					}
					return this.GetDestinationLiquidVolume(randomElement2, Phase);
				}
			}
			return null;
		}

		// Token: 0x06009F72 RID: 40818 RVA: 0x0039F1AD File Offset: 0x0039D3AD
		private LiquidVolume GetLiquidVolumeToPumpTo(Cell CC, int Phase)
		{
			return this.GetLiquidVolumeToPumpTo(ref CC, Phase);
		}

		// Token: 0x06009F73 RID: 40819 RVA: 0x0039F1B8 File Offset: 0x0039D3B8
		private Cell GetCellToPumpTo(ref Cell CC, int Phase)
		{
			if (this.FillSelfOnly)
			{
				return null;
			}
			if (CC == null)
			{
				CC = this.ParentObject.GetCurrentCell();
				if (CC == null)
				{
					return null;
				}
			}
			if (!string.IsNullOrEmpty(this.ToDirection))
			{
				Cell cellFromDirection = CC.GetCellFromDirection(this.ToDirection, true);
				if (cellFromDirection != null)
				{
					return cellFromDirection;
				}
			}
			else
			{
				List<Cell> localAdjacentCells = CC.GetLocalAdjacentCells();
				List<Cell> list = Event.NewCellList();
				int i = 0;
				int count = localAdjacentCells.Count;
				while (i < count)
				{
					if (!localAdjacentCells[i].IsSolid(true, Phase))
					{
						list.Add(localAdjacentCells[i]);
					}
					i++;
				}
				Cell randomElement = list.GetRandomElement<Cell>(null);
				if (randomElement != null)
				{
					if (this.StickyToDirection)
					{
						this.ToDirection = CC.GetDirectionFromCell(randomElement, false);
					}
					return randomElement;
				}
			}
			return null;
		}

		// Token: 0x06009F74 RID: 40820 RVA: 0x0039F272 File Offset: 0x0039D472
		private Cell GetCellToPumpTo(Cell CC, int Phase)
		{
			return this.GetCellToPumpTo(ref CC, Phase);
		}

		// Token: 0x06009F75 RID: 40821 RVA: 0x0039F280 File Offset: 0x0039D480
		private bool HasSomeplaceToPumpTo(ref Cell CC, int Phase)
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume != null && (liquidVolume.MaxVolume == -1 || liquidVolume.Volume < liquidVolume.MaxVolume) && (string.IsNullOrEmpty(this.Liquid) || liquidVolume.IsPureLiquid(this.Liquid, false)))
			{
				return true;
			}
			if (this.FillSelfOnly)
			{
				return false;
			}
			if (CC == null)
			{
				CC = this.ParentObject.GetCurrentCell();
				if (CC == null)
				{
					return false;
				}
			}
			if (!string.IsNullOrEmpty(this.ToDirection))
			{
				Cell cellFromDirection = CC.GetCellFromDirection(this.ToDirection, true);
				if (this.CanPumpTo(cellFromDirection, Phase))
				{
					return true;
				}
			}
			else
			{
				List<Cell> localAdjacentCells = CC.GetLocalAdjacentCells();
				int i = 0;
				int count = localAdjacentCells.Count;
				while (i < count)
				{
					if (this.CanPumpTo(localAdjacentCells[i], Phase))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06009F76 RID: 40822 RVA: 0x0039F34A File Offset: 0x0039D54A
		private bool HasSomeplaceToPumpTo(Cell CC, int Phase)
		{
			return this.HasSomeplaceToPumpTo(ref CC, Phase);
		}

		// Token: 0x06009F77 RID: 40823 RVA: 0x0039F355 File Offset: 0x0039D555
		private bool HasSomeplaceToPumpTo(Cell CC)
		{
			return this.HasSomeplaceToPumpTo(ref CC, this.ParentObject.GetPhase());
		}

		// Token: 0x06009F78 RID: 40824 RVA: 0x0039F36C File Offset: 0x0039D56C
		private bool HasSomeplaceToPumpTo()
		{
			Cell cell = null;
			return this.HasSomeplaceToPumpTo(cell);
		}

		// Token: 0x06009F79 RID: 40825 RVA: 0x0039F384 File Offset: 0x0039D584
		public bool IsNeeded()
		{
			Cell currentCell = this.ParentObject.GetCurrentCell();
			return currentCell != null && !currentCell.OnWorldMap() && this.HasSomeplaceToPumpTo(currentCell) && this.HasSomeplaceToPumpFrom(currentCell);
		}

		// Token: 0x06009F7A RID: 40826 RVA: 0x0039F3C3 File Offset: 0x0039D5C3
		public bool IsLiquidCollectionCompatible(string LiquidType)
		{
			return string.IsNullOrEmpty(this.Liquid) || this.Liquid == LiquidType;
		}

		// Token: 0x06009F7B RID: 40827 RVA: 0x0039F3E4 File Offset: 0x0039D5E4
		public bool DistributeLiquid()
		{
			Cell currentCell = this.ParentObject.GetCurrentCell();
			int phase = this.ParentObject.GetPhase();
			LiquidVolume liquidVolumeToPumpFrom = this.GetLiquidVolumeToPumpFrom(currentCell, phase);
			if (liquidVolumeToPumpFrom == null)
			{
				return false;
			}
			LiquidVolume liquidVolumeToPumpTo = this.GetLiquidVolumeToPumpTo(currentCell, phase);
			Cell cell = ((liquidVolumeToPumpTo == null) ? this.GetCellToPumpTo(currentCell, phase) : null);
			if (liquidVolumeToPumpTo == null && cell == null)
			{
				return false;
			}
			if (!liquidVolumeToPumpFrom.UseDram())
			{
				return false;
			}
			if (!LiquidPump.pumped.ComponentLiquids.ContainsKey(this.Liquid) || LiquidPump.pumped.ComponentLiquids.Count > 1)
			{
				if (LiquidPump.pumped.ComponentLiquids.Count > 0)
				{
					LiquidPump.pumped.ComponentLiquids.Clear();
				}
				LiquidPump.pumped.ComponentLiquids.Add(this.Liquid, 1000);
			}
			LiquidPump.pumped.MaxVolume = 1;
			LiquidPump.pumped.Volume = 1;
			if (liquidVolumeToPumpTo != null)
			{
				liquidVolumeToPumpTo.MixWith(LiquidPump.pumped, null, null, null, false);
				return true;
			}
			if (cell != null)
			{
				GameObject gameObject = GameObject.Create("Water");
				LiquidVolume liquidVolume = gameObject.LiquidVolume;
				liquidVolume.Volume = LiquidPump.pumped.Volume;
				if (this.PureOnFloor || string.IsNullOrEmpty(cell.GroundLiquid))
				{
					liquidVolume.InitialLiquid = this.Liquid + "-1000";
				}
				else
				{
					liquidVolume.InitialLiquid = cell.GroundLiquid;
					liquidVolume.MixWith(LiquidPump.pumped, null, null, null, false);
				}
				cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				return true;
			}
			return false;
		}

		// Token: 0x04004224 RID: 16932
		public int Tick;

		// Token: 0x04004225 RID: 16933
		public int Rate = 1000;

		// Token: 0x04004226 RID: 16934
		public string Liquid;

		// Token: 0x04004227 RID: 16935
		public string VariableRate;

		// Token: 0x04004228 RID: 16936
		public string PerTick;

		// Token: 0x04004229 RID: 16937
		public string FromDirection;

		// Token: 0x0400422A RID: 16938
		public string ToDirection;

		// Token: 0x0400422B RID: 16939
		public bool PreferCollectors;

		// Token: 0x0400422C RID: 16940
		public bool PureOnFloor;

		// Token: 0x0400422D RID: 16941
		public bool FillSelfOnly;

		// Token: 0x0400422E RID: 16942
		public bool StickyFromDirection;

		// Token: 0x0400422F RID: 16943
		public bool StickyToDirection;

		// Token: 0x04004230 RID: 16944
		[NonSerialized]
		private static LiquidVolume pumped = new LiquidVolume();
	}
}
