using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003B4 RID: 948
	[Flags]
	internal enum COMPILESYM_FLAGS : uint
	{
		// Token: 0x04000DAE RID: 3502
		iLanguage = 255U,
		// Token: 0x04000DAF RID: 3503
		fEC = 256U,
		// Token: 0x04000DB0 RID: 3504
		fNoDbgInfo = 512U,
		// Token: 0x04000DB1 RID: 3505
		fLTCG = 1024U,
		// Token: 0x04000DB2 RID: 3506
		fNoDataAlign = 2048U,
		// Token: 0x04000DB3 RID: 3507
		fManagedPresent = 4096U,
		// Token: 0x04000DB4 RID: 3508
		fSecurityChecks = 8192U,
		// Token: 0x04000DB5 RID: 3509
		fHotPatch = 16384U,
		// Token: 0x04000DB6 RID: 3510
		fCVTCIL = 32768U,
		// Token: 0x04000DB7 RID: 3511
		fMSILModule = 65536U
	}
}
