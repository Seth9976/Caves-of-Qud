using System;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x020011D1 RID: 4561
	[Serializable]
	public class InductionCharger : IPoweredPart
	{
		// Token: 0x0600B59A RID: 46490 RVA: 0x00424646 File Offset: 0x00422846
		public InductionCharger()
		{
			this.ChargeUse = 0;
			this.WorksOnInventory = true;
		}

		// Token: 0x0600B59B RID: 46491 RVA: 0x00424664 File Offset: 0x00422864
		public override bool SameAs(IPart p)
		{
			return (p as InductionCharger).ChargeRate == this.ChargeRate && base.SameAs(p);
		}

		// Token: 0x0600B59C RID: 46492 RVA: 0x00424682 File Offset: 0x00422882
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ChargeAvailableEvent.ID || ID == QueryChargeStorageEvent.ID;
		}

		// Token: 0x0600B59D RID: 46493 RVA: 0x004246A0 File Offset: 0x004228A0
		public override bool HandleEvent(ChargeAvailableEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.CheckTurn();
				int num = Math.Min(this.ChargeRate * E.Multiple - this.UsedOnTurn, E.Amount - this.ChargeUse * E.Multiple);
				if (num > 0 && this.AnyActivePartSubjectWantsEvent(InductionChargeEvent.ID, IChargeEvent.CascadeLevel))
				{
					InductionChargeEvent inductionChargeEvent = InductionChargeEvent.FromPool(E);
					inductionChargeEvent.Amount = num;
					this.ActivePartSubjectsHandleEvent(inductionChargeEvent);
					int num2 = num - inductionChargeEvent.Amount;
					if (num2 != 0)
					{
						E.Amount -= num2;
						if (num2 < 0)
						{
							num2 = -num2;
						}
						if (E.Multiple > 1)
						{
							num2 /= E.Multiple;
							this.UsedOnTurn = 0;
						}
						this.UsedOnTurn += num2;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B59E RID: 46494 RVA: 0x00424788 File Offset: 0x00422988
		public override bool HandleEvent(QueryChargeStorageEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.AnyActivePartSubjectWantsEvent(QueryInductionChargeStorageEvent.ID, IChargeEvent.CascadeLevel))
			{
				this.CheckTurn();
				int num = this.ChargeRate - this.UsedOnTurn;
				if (num > 0)
				{
					if (this.ActivePartHasSingleSubject())
					{
						QueryInductionChargeStorageEvent.Subprocess(this.GetActivePartFirstSubject(), E, ref num);
					}
					else
					{
						foreach (GameObject gameObject in this.GetActivePartSubjects())
						{
							QueryInductionChargeStorageEvent.Subprocess(gameObject, E, ref num);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B59F RID: 46495 RVA: 0x0042484C File Offset: 0x00422A4C
		public void CheckTurn()
		{
			if (The.Game != null && this.ActiveTurn < The.Game.Turns)
			{
				this.UsedOnTurn = 0;
				this.ActiveTurn = XRLCore.Core.Game.Turns;
			}
		}

		// Token: 0x04004945 RID: 18757
		public int ChargeRate = 10;

		// Token: 0x04004946 RID: 18758
		public long ActiveTurn;

		// Token: 0x04004947 RID: 18759
		public int UsedOnTurn;
	}
}
