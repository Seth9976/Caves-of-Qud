using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003D6 RID: 982
	[Flags]
	internal enum FRAMEPROCSYM_FLAGS : uint
	{
		// Token: 0x04000E72 RID: 3698
		fHasAlloca = 1U,
		// Token: 0x04000E73 RID: 3699
		fHasSetJmp = 2U,
		// Token: 0x04000E74 RID: 3700
		fHasLongJmp = 4U,
		// Token: 0x04000E75 RID: 3701
		fHasInlAsm = 8U,
		// Token: 0x04000E76 RID: 3702
		fHasEH = 16U,
		// Token: 0x04000E77 RID: 3703
		fInlSpec = 32U,
		// Token: 0x04000E78 RID: 3704
		fHasSEH = 64U,
		// Token: 0x04000E79 RID: 3705
		fNaked = 128U,
		// Token: 0x04000E7A RID: 3706
		fSecurityChecks = 256U,
		// Token: 0x04000E7B RID: 3707
		fAsyncEH = 512U,
		// Token: 0x04000E7C RID: 3708
		fGSNoStackOrdering = 1024U,
		// Token: 0x04000E7D RID: 3709
		fWasInlined = 2048U
	}
}
