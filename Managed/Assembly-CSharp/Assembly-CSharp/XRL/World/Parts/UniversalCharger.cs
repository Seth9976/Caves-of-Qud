using System;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x020011DD RID: 4573
	[Serializable]
	public class UniversalCharger : IPoweredPart
	{
		// Token: 0x0600B650 RID: 46672 RVA: 0x00428A69 File Offset: 0x00426C69
		public UniversalCharger()
		{
			this.ChargeUse = 0;
			this.WorksOnInventory = true;
		}

		// Token: 0x0600B651 RID: 46673 RVA: 0x00428A87 File Offset: 0x00426C87
		public override bool SameAs(IPart p)
		{
			return (p as UniversalCharger).ChargeRate == this.ChargeRate && base.SameAs(p);
		}

		// Token: 0x0600B652 RID: 46674 RVA: 0x00428AA5 File Offset: 0x00426CA5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ChargeAvailableEvent.ID || ID == QueryChargeStorageEvent.ID;
		}

		// Token: 0x0600B653 RID: 46675 RVA: 0x00428AC4 File Offset: 0x00426CC4
		public override bool HandleEvent(ChargeAvailableEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.AnyActivePartSubjectWantsEvent(RechargeAvailableEvent.ID, IChargeEvent.CascadeLevel))
			{
				this.CheckTurn();
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
					RechargeAvailableEvent rechargeAvailableEvent = RechargeAvailableEvent.FromPool(E);
					rechargeAvailableEvent.Amount = num;
					this.ActivePartSubjectsHandleEvent(rechargeAvailableEvent);
					if (rechargeAvailableEvent.Amount < num)
					{
						int num2 = num - rechargeAvailableEvent.Amount;
						if (E.Multiple > 1)
						{
							this.UsedOnTurn = num2 / E.Multiple;
						}
						else
						{
							this.UsedOnTurn += num2;
						}
						E.Amount -= num2 + this.ChargeUse;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B654 RID: 46676 RVA: 0x00428BDC File Offset: 0x00426DDC
		public override bool HandleEvent(QueryChargeStorageEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.AnyActivePartSubjectWantsEvent(QueryRechargeStorageEvent.ID, IChargeEvent.CascadeLevel))
			{
				this.CheckTurn();
				int num = this.ChargeRate * E.Multiple - this.UsedOnTurn;
				if (num > 0)
				{
					QueryRechargeStorageEvent queryRechargeStorageEvent = QueryRechargeStorageEvent.FromPool(E);
					this.ActivePartSubjectsHandleEvent(queryRechargeStorageEvent);
					if (queryRechargeStorageEvent.Amount > E.Amount)
					{
						E.Amount += Math.Min(queryRechargeStorageEvent.Amount - E.Amount, num);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B655 RID: 46677 RVA: 0x00428C86 File Offset: 0x00426E86
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B656 RID: 46678 RVA: 0x00428C8C File Offset: 0x00426E8C
		public void CheckTurn()
		{
			if (XRLCore.Core != null && XRLCore.Core.Game != null && this.ActiveTurn < XRLCore.Core.Game.Turns)
			{
				this.UsedOnTurn = 0;
				this.ActiveTurn = XRLCore.Core.Game.Turns;
			}
		}

		// Token: 0x0400498F RID: 18831
		public int ChargeRate = 10;

		// Token: 0x04004990 RID: 18832
		public long ActiveTurn;

		// Token: 0x04004991 RID: 18833
		public int UsedOnTurn;
	}
}
