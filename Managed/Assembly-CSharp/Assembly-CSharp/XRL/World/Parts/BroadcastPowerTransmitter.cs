using System;

namespace XRL.World.Parts
{
	// Token: 0x020011BA RID: 4538
	[Serializable]
	public class BroadcastPowerTransmitter : IPoweredPart
	{
		// Token: 0x0600B425 RID: 46117 RVA: 0x0041B5A8 File Offset: 0x004197A8
		public BroadcastPowerTransmitter()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B426 RID: 46118 RVA: 0x0041B5BE File Offset: 0x004197BE
		public override bool SameAs(IPart p)
		{
			return (p as BroadcastPowerTransmitter).TransmitRate == this.TransmitRate && base.SameAs(p);
		}

		// Token: 0x0600B427 RID: 46119 RVA: 0x0041B5DC File Offset: 0x004197DC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CollectBroadcastChargeEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600B428 RID: 46120 RVA: 0x0041B5FC File Offset: 0x004197FC
		public override bool HandleEvent(EndTurnEvent E)
		{
			Zone basisZone = this.GetBasisZone();
			if (basisZone != null)
			{
				basisZone.RequirePart<BroadcastPowerField>(false).RegisterTransmitter(this.ParentObject);
			}
			return true;
		}

		// Token: 0x0600B429 RID: 46121 RVA: 0x0041B628 File Offset: 0x00419828
		public override bool HandleEvent(CollectBroadcastChargeEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = Math.Min(E.Charge, this.ParentObject.QueryCharge(false, 0L, true, true) - this.ChargeUse);
				if (this.TransmitRate != 0 && num > this.TransmitRate)
				{
					num = this.TransmitRate;
				}
				if (num > 0)
				{
					this.ParentObject.UseCharge(num + this.ChargeUse, false, 0L, true, true, null);
					E.Charge -= num;
					if (num >= E.Charge)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B42A RID: 46122 RVA: 0x0041B6E0 File Offset: 0x004198E0
		public override int GetDraw(QueryDrawEvent E = null)
		{
			int num = base.GetDraw(E);
			Zone anyBasisZone = this.GetAnyBasisZone();
			if (anyBasisZone != null && anyBasisZone.IsActive() && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				bool flag = false;
				int num2 = 0;
				int num3 = 0;
				if (E != null)
				{
					flag = E.BroadcastDrawDone;
					num2 = (flag ? E.BroadcastDraw : 0);
					num3 = E.HighTransmitRate;
				}
				if (!flag || (num3 > 0 && num3 < this.TransmitRate))
				{
					if (!flag)
					{
						num2 = QueryBroadcastDrawEvent.GetFor(anyBasisZone);
						if (E != null)
						{
							E.BroadcastDraw = num2;
						}
					}
					int num4 = num2 - num3;
					if (num4 > 0)
					{
						if (this.TransmitRate > 0 && num4 > this.TransmitRate)
						{
							num4 = this.TransmitRate;
						}
						num += num4;
						if (E != null && (this.TransmitRate <= 0 || this.TransmitRate > num3))
						{
							E.HighTransmitRate = num3;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0400489C RID: 18588
		public int TransmitRate;
	}
}
