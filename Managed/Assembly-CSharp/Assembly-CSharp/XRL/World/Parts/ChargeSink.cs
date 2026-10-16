using System;

namespace XRL.World.Parts
{
	// Token: 0x020011BC RID: 4540
	[Serializable]
	public class ChargeSink : IPoweredPart
	{
		// Token: 0x0600B449 RID: 46153 RVA: 0x0041C165 File Offset: 0x0041A365
		public ChargeSink()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B44A RID: 46154 RVA: 0x0041C174 File Offset: 0x0041A374
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B44B RID: 46155 RVA: 0x0041C177 File Offset: 0x0041A377
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B44C RID: 46156 RVA: 0x0041C17C File Offset: 0x0041A37C
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
		}
	}
}
