using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200009E RID: 158
	[Flags]
	public enum PInvokeAttributes : ushort
	{
		// Token: 0x0400021C RID: 540
		NoMangle = 1,
		// Token: 0x0400021D RID: 541
		CharSetMask = 6,
		// Token: 0x0400021E RID: 542
		CharSetNotSpec = 0,
		// Token: 0x0400021F RID: 543
		CharSetAnsi = 2,
		// Token: 0x04000220 RID: 544
		CharSetUnicode = 4,
		// Token: 0x04000221 RID: 545
		CharSetAuto = 6,
		// Token: 0x04000222 RID: 546
		SupportsLastError = 64,
		// Token: 0x04000223 RID: 547
		CallConvMask = 1792,
		// Token: 0x04000224 RID: 548
		CallConvWinapi = 256,
		// Token: 0x04000225 RID: 549
		CallConvCdecl = 512,
		// Token: 0x04000226 RID: 550
		CallConvStdCall = 768,
		// Token: 0x04000227 RID: 551
		CallConvThiscall = 1024,
		// Token: 0x04000228 RID: 552
		CallConvFastcall = 1280,
		// Token: 0x04000229 RID: 553
		BestFitMask = 48,
		// Token: 0x0400022A RID: 554
		BestFitEnabled = 16,
		// Token: 0x0400022B RID: 555
		BestFitDisabled = 32,
		// Token: 0x0400022C RID: 556
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x0400022D RID: 557
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x0400022E RID: 558
		ThrowOnUnmappableCharDisabled = 8192
	}
}
