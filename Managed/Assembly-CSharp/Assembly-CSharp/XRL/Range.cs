using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004FB RID: 1275
	[Serializable]
	public class Range : IComposite
	{
		// Token: 0x060036FF RID: 14079 RVA: 0x0010A2BF File Offset: 0x001084BF
		public Range()
		{
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x0010A2C7 File Offset: 0x001084C7
		public Range(int Amount)
		{
			this.Min = Amount;
			this.Max = Amount;
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x0010A2DD File Offset: 0x001084DD
		public Range(int _Min, int _Max)
		{
			this.Min = _Min;
			this.Max = _Max;
		}

		// Token: 0x04001B61 RID: 7009
		public int Min;

		// Token: 0x04001B62 RID: 7010
		public int Max;
	}
}
