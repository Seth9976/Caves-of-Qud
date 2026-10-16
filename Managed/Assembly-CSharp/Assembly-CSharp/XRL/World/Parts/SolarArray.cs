using System;

namespace XRL.World.Parts
{
	// Token: 0x020011DC RID: 4572
	[Serializable]
	public class SolarArray : IPoweredPart
	{
		// Token: 0x0600B645 RID: 46661 RVA: 0x0042889B File Offset: 0x00426A9B
		public SolarArray()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B646 RID: 46662 RVA: 0x004288B9 File Offset: 0x00426AB9
		public override bool SameAs(IPart p)
		{
			return (p as SolarArray).ChargeRate == this.ChargeRate && base.SameAs(p);
		}

		// Token: 0x0600B647 RID: 46663 RVA: 0x004288D7 File Offset: 0x00426AD7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == QueryChargeProductionEvent.ID || ID == PrimePowerSystemsEvent.ID;
		}

		// Token: 0x0600B648 RID: 46664 RVA: 0x004288FD File Offset: 0x00426AFD
		public override bool HandleEvent(PrimePowerSystemsEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("Furniture"))
			{
				this.ProduceCharge(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B649 RID: 46665 RVA: 0x0042891F File Offset: 0x00426B1F
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "ChargeRate", this.ChargeRate);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B64A RID: 46666 RVA: 0x0042893C File Offset: 0x00426B3C
		public override bool HandleEvent(QueryChargeProductionEvent E)
		{
			if (this.ChargeRate > this.ChargeUse && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Amount += (this.ChargeRate - this.ChargeUse) * E.Multiple;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B64B RID: 46667 RVA: 0x004289A8 File Offset: 0x00426BA8
		public override bool GetActivePartLocallyDefinedFailure()
		{
			Cell anyBasisCell = this.GetAnyBasisCell();
			if (anyBasisCell == null)
			{
				return true;
			}
			if (anyBasisCell.IsBlackedOut())
			{
				return true;
			}
			Zone parentZone = anyBasisCell.ParentZone;
			return parentZone == null || (!parentZone.IsWorldMap() && !parentZone.IsOutside()) || !base.IsDay();
		}

		// Token: 0x0600B64C RID: 46668 RVA: 0x004289F4 File Offset: 0x00426BF4
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "RadiationFluxInsufficient";
		}

		// Token: 0x0600B64D RID: 46669 RVA: 0x004289FB File Offset: 0x00426BFB
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B64E RID: 46670 RVA: 0x004289FE File Offset: 0x00426BFE
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProduceCharge(Amount);
		}

		// Token: 0x0600B64F RID: 46671 RVA: 0x00428A08 File Offset: 0x00426C08
		public void ProduceCharge(int Turns = 1)
		{
			if (this.ChargeRate > this.ChargeUse && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ParentObject.ChargeAvailable(this.ChargeRate - this.ChargeUse, 0L, Turns, false, null);
			}
		}

		// Token: 0x0400498E RID: 18830
		public int ChargeRate = 10;
	}
}
