using System;

namespace XRL.World.Parts
{
	// Token: 0x020011DB RID: 4571
	[Serializable]
	public class SlottedCellCharger : IPoweredPart
	{
		// Token: 0x0600B63C RID: 46652 RVA: 0x004285D2 File Offset: 0x004267D2
		public SlottedCellCharger()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
			this.NameForStatus = "ChargingSystem";
		}

		// Token: 0x0600B63D RID: 46653 RVA: 0x004285F4 File Offset: 0x004267F4
		public GameObject GetChargeableCell()
		{
			EnergyCellSocket energyCellSocket;
			EnergyCell energyCell;
			if (this.ParentObject.TryGetPart<EnergyCellSocket>(out energyCellSocket) && energyCellSocket.Cell != null && energyCellSocket.Cell.TryGetPart<EnergyCell>(out energyCell) && energyCell.Charge < energyCell.MaxCharge)
			{
				return energyCellSocket.Cell;
			}
			return null;
		}

		// Token: 0x0600B63E RID: 46654 RVA: 0x0042863D File Offset: 0x0042683D
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.GetChargeableCell() == null;
		}

		// Token: 0x0600B63F RID: 46655 RVA: 0x00428648 File Offset: 0x00426848
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			EnergyCellSocket part = this.ParentObject.GetPart<EnergyCellSocket>();
			if (part == null)
			{
				return "NoSocket";
			}
			if (part.Cell == null)
			{
				return "NoCell";
			}
			EnergyCell part2 = part.Cell.GetPart<EnergyCell>();
			if (part2 == null)
			{
				return "InappropriateCell";
			}
			if (part2.Charge >= part2.MaxCharge)
			{
				return "CellFull";
			}
			return "Error";
		}

		// Token: 0x0600B640 RID: 46656 RVA: 0x004286A6 File Offset: 0x004268A6
		public override bool SameAs(IPart p)
		{
			return (p as SlottedCellCharger).ChargeRate == this.ChargeRate && base.SameAs(p);
		}

		// Token: 0x0600B641 RID: 46657 RVA: 0x004286C4 File Offset: 0x004268C4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ChargeAvailableEvent.ID || ID == QueryChargeStorageEvent.ID;
		}

		// Token: 0x0600B642 RID: 46658 RVA: 0x004286E4 File Offset: 0x004268E4
		public override bool HandleEvent(ChargeAvailableEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.CheckTurn();
				GameObject chargeableCell = this.GetChargeableCell();
				if (chargeableCell != null)
				{
					int num;
					if (this.ChargeRate != 0 && !E.Forced)
					{
						num = Math.Min(this.ChargeRate * E.Multiple - this.UsedOnTurn, E.Amount - this.ChargeUse * E.Multiple);
					}
					else
					{
						num = E.Amount - this.ChargeUse * E.Multiple;
					}
					if (num > 0)
					{
						int num2 = RechargeAvailableEvent.Send(chargeableCell, E, num);
						if (num2 != 0)
						{
							if (E.Multiple > 1)
							{
								this.UsedOnTurn = num2 / E.Multiple;
							}
							else
							{
								this.UsedOnTurn += num2;
							}
							E.Amount -= num2 + this.ChargeUse * E.Multiple;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B643 RID: 46659 RVA: 0x004287E0 File Offset: 0x004269E0
		public override bool HandleEvent(QueryChargeStorageEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.CheckTurn();
				if (this.ChargeRate <= 0 || this.ChargeRate > this.UsedOnTurn)
				{
					GameObject chargeableCell = this.GetChargeableCell();
					if (chargeableCell != null)
					{
						int num = QueryRechargeStorageEvent.Retrieve(chargeableCell, E);
						if (num > 0)
						{
							if (this.ChargeRate > 0)
							{
								num = Math.Min(num, this.ChargeRate - this.UsedOnTurn);
							}
							E.Amount += num;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B644 RID: 46660 RVA: 0x0042887A File Offset: 0x00426A7A
		public void CheckTurn()
		{
			if (this.ActiveTurn < The.CurrentTurn)
			{
				this.UsedOnTurn = 0;
				this.ActiveTurn = The.CurrentTurn;
			}
		}

		// Token: 0x0400498B RID: 18827
		public int ChargeRate;

		// Token: 0x0400498C RID: 18828
		public long ActiveTurn;

		// Token: 0x0400498D RID: 18829
		public int UsedOnTurn;
	}
}
