using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200007D RID: 125
	public class TimeSpanUnits
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000999D File Offset: 0x00007B9D
		public TimeSpanUnits(double d, int m, int y)
		{
			this.days = d;
			this.months = m;
			this.years = y;
		}

		// Token: 0x040001AA RID: 426
		public double days;

		// Token: 0x040001AB RID: 427
		public int months;

		// Token: 0x040001AC RID: 428
		public int years;
	}
}
