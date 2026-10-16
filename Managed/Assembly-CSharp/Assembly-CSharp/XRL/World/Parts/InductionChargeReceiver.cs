using System;

namespace XRL.World.Parts
{
	// Token: 0x020011D0 RID: 4560
	[Serializable]
	public class InductionChargeReceiver : IPoweredPart
	{
		// Token: 0x0600B595 RID: 46485 RVA: 0x004244D5 File Offset: 0x004226D5
		public InductionChargeReceiver()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B596 RID: 46486 RVA: 0x004244F3 File Offset: 0x004226F3
		public override bool SameAs(IPart p)
		{
			return (p as InductionChargeReceiver).ChargeRate == this.ChargeRate && base.SameAs(p);
		}

		// Token: 0x0600B597 RID: 46487 RVA: 0x00424511 File Offset: 0x00422711
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == InductionChargeEvent.ID || ID == QueryInductionChargeStorageEvent.ID;
		}

		// Token: 0x0600B598 RID: 46488 RVA: 0x00424530 File Offset: 0x00422730
		public override bool HandleEvent(InductionChargeEvent E)
		{
			if (E.Amount > 0 && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = Math.Min(E.Amount, this.ChargeRate * E.Multiple);
				if (num > 0)
				{
					int num2 = this.ParentObject.ChargeAvailable(num, 0L, 1, false, null);
					if (num2 > 0)
					{
						E.Amount -= num2;
						base.ConsumeCharge(null, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B599 RID: 46489 RVA: 0x004245D4 File Offset: 0x004227D4
		public override bool HandleEvent(QueryInductionChargeStorageEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num;
				bool flag;
				E.Amount += this.ParentObject.QueryChargeStorage(out num, out flag, null, true);
				E.Transient += num;
				if (flag)
				{
					E.UnlimitedTransient = true;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004944 RID: 18756
		public int ChargeRate = 10;
	}
}
