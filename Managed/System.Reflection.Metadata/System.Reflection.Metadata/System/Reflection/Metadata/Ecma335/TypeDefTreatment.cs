using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000158 RID: 344
	[Flags]
	internal enum TypeDefTreatment : byte
	{
		// Token: 0x0400089F RID: 2207
		None = 0,
		// Token: 0x040008A0 RID: 2208
		KindMask = 15,
		// Token: 0x040008A1 RID: 2209
		NormalNonAttribute = 1,
		// Token: 0x040008A2 RID: 2210
		NormalAttribute = 2,
		// Token: 0x040008A3 RID: 2211
		UnmangleWinRTName = 3,
		// Token: 0x040008A4 RID: 2212
		PrefixWinRTName = 4,
		// Token: 0x040008A5 RID: 2213
		RedirectedToClrType = 5,
		// Token: 0x040008A6 RID: 2214
		RedirectedToClrAttribute = 6,
		// Token: 0x040008A7 RID: 2215
		MarkAbstractFlag = 16,
		// Token: 0x040008A8 RID: 2216
		MarkInternalFlag = 32
	}
}
