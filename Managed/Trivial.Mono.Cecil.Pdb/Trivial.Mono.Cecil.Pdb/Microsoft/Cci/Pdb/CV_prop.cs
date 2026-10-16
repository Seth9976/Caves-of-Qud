using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000048 RID: 72
	[Flags]
	internal enum CV_prop : ushort
	{
		// Token: 0x04000113 RID: 275
		packed = 1,
		// Token: 0x04000114 RID: 276
		ctor = 2,
		// Token: 0x04000115 RID: 277
		ovlops = 4,
		// Token: 0x04000116 RID: 278
		isnested = 8,
		// Token: 0x04000117 RID: 279
		cnested = 16,
		// Token: 0x04000118 RID: 280
		opassign = 32,
		// Token: 0x04000119 RID: 281
		opcast = 64,
		// Token: 0x0400011A RID: 282
		fwdref = 128,
		// Token: 0x0400011B RID: 283
		scoped = 256
	}
}
