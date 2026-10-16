using System;

namespace XRL.World.Parts
{
	// Token: 0x020011D2 RID: 4562
	[Serializable]
	public class IntegralRecharger : IPoweredPart
	{
		// Token: 0x0600B5A0 RID: 46496 RVA: 0x00424883 File Offset: 0x00422A83
		public IntegralRecharger()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B5A1 RID: 46497 RVA: 0x00424899 File Offset: 0x00422A99
		public override bool SameAs(IPart p)
		{
			return (p as IntegralRecharger).ChargeRate == this.ChargeRate && base.SameAs(p);
		}

		// Token: 0x0600B5A2 RID: 46498 RVA: 0x004248B7 File Offset: 0x00422AB7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ChargeAvailableEvent.ID || ID == QueryChargeStorageEvent.ID;
		}

		// Token: 0x0600B5A3 RID: 46499 RVA: 0x004248D8 File Offset: 0x00422AD8
		public override bool HandleEvent(ChargeAvailableEvent E)
		{
			if (base.IsReady(false, true, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = E.Amount - this.ChargeUse * E.Multiple;
				if (this.ChargeRate != 0)
				{
					int num2 = this.ChargeRate * E.Multiple;
					if (num > num2)
					{
						num = num2;
					}
				}
				if (num > 0)
				{
					E.Amount -= RechargeAvailableEvent.Send(this.ParentObject, E, num);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5A4 RID: 46500 RVA: 0x00424964 File Offset: 0x00422B64
		public override bool HandleEvent(QueryChargeStorageEvent E)
		{
			if (base.IsReady(false, true, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = QueryRechargeStorageEvent.Retrieve(this.ParentObject, E);
				if (num > 0)
				{
					if (this.ChargeRate != 0)
					{
						E.Amount += Math.Min(this.ChargeRate, num);
					}
					else
					{
						E.Amount += num;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004948 RID: 18760
		public int ChargeRate;
	}
}
