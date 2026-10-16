using System;

namespace XRL.World.Parts
{
	// Token: 0x020011DF RID: 4575
	[Serializable]
	public class WindTurbine : IPoweredPart
	{
		// Token: 0x0600B65C RID: 46684 RVA: 0x00428DD9 File Offset: 0x00426FD9
		public WindTurbine()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B65D RID: 46685 RVA: 0x00428DFA File Offset: 0x00426FFA
		public override bool SameAs(IPart p)
		{
			return (p as WindTurbine).ChargeRateFactor == this.ChargeRateFactor && base.SameAs(p);
		}

		// Token: 0x0600B65E RID: 46686 RVA: 0x00428E18 File Offset: 0x00427018
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == QueryChargeProductionEvent.ID || ID == PrimePowerSystemsEvent.ID;
		}

		// Token: 0x0600B65F RID: 46687 RVA: 0x00428E36 File Offset: 0x00427036
		public override bool HandleEvent(PrimePowerSystemsEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("Furniture"))
			{
				this.ProduceCharge(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B660 RID: 46688 RVA: 0x00428E58 File Offset: 0x00427058
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

		// Token: 0x0600B661 RID: 46689 RVA: 0x00428EC0 File Offset: 0x004270C0
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.GetChargeRate() <= 0;
		}

		// Token: 0x0600B662 RID: 46690 RVA: 0x00428ED0 File Offset: 0x004270D0
		public int GetChargeRate()
		{
			Zone anyBasisZone = this.GetAnyBasisZone();
			if (anyBasisZone == null)
			{
				return 0;
			}
			return (int)((float)anyBasisZone.CurrentWindSpeed * this.ChargeRateFactor);
		}

		// Token: 0x0600B663 RID: 46691 RVA: 0x00428EF8 File Offset: 0x004270F8
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "WindSpeedInsufficient";
		}

		// Token: 0x0600B664 RID: 46692 RVA: 0x00428EFF File Offset: 0x004270FF
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B665 RID: 46693 RVA: 0x00428F02 File Offset: 0x00427102
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProduceCharge(Amount);
		}

		// Token: 0x0600B666 RID: 46694 RVA: 0x00428F0C File Offset: 0x0042710C
		public void ProduceCharge(int Turns = 1)
		{
			int chargeRate = this.GetChargeRate();
			if (chargeRate > this.ChargeUse && base.IsReady(false, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				this.ParentObject.ChargeAvailable(chargeRate - this.ChargeUse, 0L, Turns, false, null);
			}
		}

		// Token: 0x04004993 RID: 18835
		public float ChargeRateFactor = 0.2f;
	}
}
