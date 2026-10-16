using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000023 RID: 35
	[Flags]
	internal enum CFLAGSYM_FLAGS : ushort
	{
		// Token: 0x0400005B RID: 91
		pcode = 1,
		// Token: 0x0400005C RID: 92
		floatprec = 6,
		// Token: 0x0400005D RID: 93
		floatpkg = 24,
		// Token: 0x0400005E RID: 94
		ambdata = 224,
		// Token: 0x0400005F RID: 95
		ambcode = 1792,
		// Token: 0x04000060 RID: 96
		mode32 = 2048
	}
}
