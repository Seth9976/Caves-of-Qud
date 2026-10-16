using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000026 RID: 38
	[Flags]
	internal enum COMPILESYM_FLAGS : uint
	{
		// Token: 0x04000071 RID: 113
		iLanguage = 255U,
		// Token: 0x04000072 RID: 114
		fEC = 256U,
		// Token: 0x04000073 RID: 115
		fNoDbgInfo = 512U,
		// Token: 0x04000074 RID: 116
		fLTCG = 1024U,
		// Token: 0x04000075 RID: 117
		fNoDataAlign = 2048U,
		// Token: 0x04000076 RID: 118
		fManagedPresent = 4096U,
		// Token: 0x04000077 RID: 119
		fSecurityChecks = 8192U,
		// Token: 0x04000078 RID: 120
		fHotPatch = 16384U,
		// Token: 0x04000079 RID: 121
		fCVTCIL = 32768U,
		// Token: 0x0400007A RID: 122
		fMSILModule = 65536U
	}
}
