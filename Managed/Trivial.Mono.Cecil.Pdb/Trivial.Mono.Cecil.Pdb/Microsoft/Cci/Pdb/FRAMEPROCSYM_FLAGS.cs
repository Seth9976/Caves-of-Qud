using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000068 RID: 104
	[Flags]
	internal enum FRAMEPROCSYM_FLAGS : uint
	{
		// Token: 0x040001E5 RID: 485
		fHasAlloca = 1U,
		// Token: 0x040001E6 RID: 486
		fHasSetJmp = 2U,
		// Token: 0x040001E7 RID: 487
		fHasLongJmp = 4U,
		// Token: 0x040001E8 RID: 488
		fHasInlAsm = 8U,
		// Token: 0x040001E9 RID: 489
		fHasEH = 16U,
		// Token: 0x040001EA RID: 490
		fInlSpec = 32U,
		// Token: 0x040001EB RID: 491
		fHasSEH = 64U,
		// Token: 0x040001EC RID: 492
		fNaked = 128U,
		// Token: 0x040001ED RID: 493
		fSecurityChecks = 256U,
		// Token: 0x040001EE RID: 494
		fAsyncEH = 512U,
		// Token: 0x040001EF RID: 495
		fGSNoStackOrdering = 1024U,
		// Token: 0x040001F0 RID: 496
		fWasInlined = 2048U
	}
}
