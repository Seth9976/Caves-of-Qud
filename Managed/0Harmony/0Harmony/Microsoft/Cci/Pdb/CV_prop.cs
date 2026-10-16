using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200034C RID: 844
	[Flags]
	internal enum CV_prop : ushort
	{
		// Token: 0x04000C0A RID: 3082
		packed = 1,
		// Token: 0x04000C0B RID: 3083
		ctor = 2,
		// Token: 0x04000C0C RID: 3084
		ovlops = 4,
		// Token: 0x04000C0D RID: 3085
		isnested = 8,
		// Token: 0x04000C0E RID: 3086
		cnested = 16,
		// Token: 0x04000C0F RID: 3087
		opassign = 32,
		// Token: 0x04000C10 RID: 3088
		opcast = 64,
		// Token: 0x04000C11 RID: 3089
		fwdref = 128,
		// Token: 0x04000C12 RID: 3090
		scoped = 256
	}
}
