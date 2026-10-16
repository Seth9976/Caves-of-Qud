using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003A0 RID: 928
	[Flags]
	internal enum CV_PROCFLAGS : byte
	{
		// Token: 0x04000D56 RID: 3414
		CV_PFLAG_NOFPO = 1,
		// Token: 0x04000D57 RID: 3415
		CV_PFLAG_INT = 2,
		// Token: 0x04000D58 RID: 3416
		CV_PFLAG_FAR = 4,
		// Token: 0x04000D59 RID: 3417
		CV_PFLAG_NEVER = 8,
		// Token: 0x04000D5A RID: 3418
		CV_PFLAG_NOTREACHED = 16,
		// Token: 0x04000D5B RID: 3419
		CV_PFLAG_CUST_CALL = 32,
		// Token: 0x04000D5C RID: 3420
		CV_PFLAG_NOINLINE = 64,
		// Token: 0x04000D5D RID: 3421
		CV_PFLAG_OPTDBGINFO = 128
	}
}
