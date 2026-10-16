using System;

namespace Microsoft.Cci
{
	// Token: 0x020000C5 RID: 197
	[Flags]
	internal enum TypeLibTypeFlags
	{
		// Token: 0x04000204 RID: 516
		FAppObject = 1,
		// Token: 0x04000205 RID: 517
		FCanCreate = 2,
		// Token: 0x04000206 RID: 518
		FLicensed = 4,
		// Token: 0x04000207 RID: 519
		FPreDeclId = 8,
		// Token: 0x04000208 RID: 520
		FHidden = 16,
		// Token: 0x04000209 RID: 521
		FControl = 32,
		// Token: 0x0400020A RID: 522
		FDual = 64,
		// Token: 0x0400020B RID: 523
		FNonExtensible = 128,
		// Token: 0x0400020C RID: 524
		FOleAutomation = 256,
		// Token: 0x0400020D RID: 525
		FRestricted = 512,
		// Token: 0x0400020E RID: 526
		FAggregatable = 1024,
		// Token: 0x0400020F RID: 527
		FReplaceable = 2048,
		// Token: 0x04000210 RID: 528
		FDispatchable = 4096,
		// Token: 0x04000211 RID: 529
		FReverseBind = 8192
	}
}
