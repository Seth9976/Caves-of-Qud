using System;

namespace Mono.Cecil
{
	// Token: 0x02000234 RID: 564
	internal struct Range
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x0002A1CC File Offset: 0x000283CC
		public Range(uint index, uint length)
		{
			this.Start = index;
			this.Length = length;
		}

		// Token: 0x0400036F RID: 879
		public uint Start;

		// Token: 0x04000370 RID: 880
		public uint Length;
	}
}
