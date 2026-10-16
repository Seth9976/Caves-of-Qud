using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000047 RID: 71
	[Flags]
	internal enum CV_PROCFLAGS : byte
	{
		// Token: 0x0400010A RID: 266
		CV_PFLAG_NOFPO = 1,
		// Token: 0x0400010B RID: 267
		CV_PFLAG_INT = 2,
		// Token: 0x0400010C RID: 268
		CV_PFLAG_FAR = 4,
		// Token: 0x0400010D RID: 269
		CV_PFLAG_NEVER = 8,
		// Token: 0x0400010E RID: 270
		CV_PFLAG_NOTREACHED = 16,
		// Token: 0x0400010F RID: 271
		CV_PFLAG_CUST_CALL = 32,
		// Token: 0x04000110 RID: 272
		CV_PFLAG_NOINLINE = 64,
		// Token: 0x04000111 RID: 273
		CV_PFLAG_OPTDBGINFO = 128
	}
}
