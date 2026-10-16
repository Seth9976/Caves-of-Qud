using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020011CC RID: 4556
	[Serializable]
	public class HydroTurbine : IPoweredPart
	{
		// Token: 0x0600B516 RID: 46358 RVA: 0x00421608 File Offset: 0x0041F808
		public HydroTurbine()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B517 RID: 46359 RVA: 0x00421640 File Offset: 0x0041F840
		public override bool SameAs(IPart p)
		{
			HydroTurbine hydroTurbine = p as HydroTurbine;
			return hydroTurbine.MinimumEffectiveVolume == this.MinimumEffectiveVolume && hydroTurbine.MaximumEffectiveVolume == this.MaximumEffectiveVolume && hydroTurbine.MaximumChargeRate == this.MaximumChargeRate && base.SameAs(p);
		}

		// Token: 0x0600B518 RID: 46360 RVA: 0x0042168B File Offset: 0x0041F88B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == QueryChargeProductionEvent.ID || ID == PrimePowerSystemsEvent.ID;
		}

		// Token: 0x0600B519 RID: 46361 RVA: 0x004216A9 File Offset: 0x0041F8A9
		public override bool HandleEvent(PrimePowerSystemsEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("Furniture"))
			{
				this.ProduceCharge(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B51A RID: 46362 RVA: 0x004216CC File Offset: 0x0041F8CC
		public override bool HandleEvent(QueryChargeProductionEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int chargeRate = this.GetChargeRate();
				if (chargeRate > this.ChargeUse)
				{
					E.Amount += (chargeRate - this.ChargeUse) * E.Multiple;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B51B RID: 46363 RVA: 0x00421734 File Offset: 0x0041F934
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.GetChargeRate() <= 0;
		}

		// Token: 0x0600B51C RID: 46364 RVA: 0x00421742 File Offset: 0x0041F942
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "HydrodynamicForceInsufficient";
		}

		// Token: 0x0600B51D RID: 46365 RVA: 0x0042174C File Offset: 0x0041F94C
		public int GetChargeRate()
		{
			int num = this.FindEffectiveNearbyLiquidVolume();
			if (num < this.MinimumEffectiveVolume)
			{
				return 0;
			}
			if (num >= this.MaximumEffectiveVolume)
			{
				return this.MaximumChargeRate;
			}
			num -= this.MinimumEffectiveVolume;
			return this.MaximumChargeRate * num / (this.MaximumEffectiveVolume - this.MinimumEffectiveVolume);
		}

		// Token: 0x0600B51E RID: 46366 RVA: 0x0042179A File Offset: 0x0041F99A
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B51F RID: 46367 RVA: 0x0042179D File Offset: 0x0041F99D
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProduceCharge(Amount);
		}

		// Token: 0x0600B520 RID: 46368 RVA: 0x004217A8 File Offset: 0x0041F9A8
		private int FindEffectiveNearbyLiquidVolume()
		{
			this.FoundVolume = 0;
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null && this.ScanForLiquidVolumes(currentCell))
			{
				List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells();
				int num = 0;
				int count = localAdjacentCells.Count;
				while (num < count && this.ScanForLiquidVolumes(localAdjacentCells[num]))
				{
					num++;
				}
			}
			return this.FoundVolume;
		}

		// Token: 0x0600B521 RID: 46369 RVA: 0x00421804 File Offset: 0x0041FA04
		private bool ScanForLiquidVolumes(Cell C)
		{
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				LiquidVolume liquidVolume = C.Objects[i].LiquidVolume;
				if (liquidVolume != null)
				{
					this.FoundVolume += liquidVolume.Volume;
					if (this.FoundVolume >= this.MaximumEffectiveVolume)
					{
						return false;
					}
				}
				i++;
			}
			return true;
		}

		// Token: 0x0600B522 RID: 46370 RVA: 0x00421864 File Offset: 0x0041FA64
		public void ProduceCharge(int Turns = 1)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				int chargeRate = this.GetChargeRate();
				if (chargeRate > this.ChargeUse)
				{
					this.ParentObject.ChargeAvailable(chargeRate - this.ChargeUse, 0L, Turns, false, null);
				}
			}
		}

		// Token: 0x04004905 RID: 18693
		public int MinimumEffectiveVolume = 1000;

		// Token: 0x04004906 RID: 18694
		public int MaximumEffectiveVolume = 6000;

		// Token: 0x04004907 RID: 18695
		public int MaximumChargeRate = 500;

		// Token: 0x04004908 RID: 18696
		[NonSerialized]
		private int FoundVolume;
	}
}
