using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000A6 RID: 166
	internal struct Range
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x00015123 File Offset: 0x00013323
		public Range(uint index, uint length)
		{
			this.Start = index;
			this.Length = length;
		}

		// Token: 0x04000240 RID: 576
		public uint Start;

		// Token: 0x04000241 RID: 577
		public uint Length;
	}
}
