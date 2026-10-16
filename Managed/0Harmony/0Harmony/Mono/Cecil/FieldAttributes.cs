using System;

namespace Mono.Cecil
{
	// Token: 0x02000203 RID: 515
	[Flags]
	public enum FieldAttributes : ushort
	{
		// Token: 0x0400030C RID: 780
		FieldAccessMask = 7,
		// Token: 0x0400030D RID: 781
		CompilerControlled = 0,
		// Token: 0x0400030E RID: 782
		Private = 1,
		// Token: 0x0400030F RID: 783
		FamANDAssem = 2,
		// Token: 0x04000310 RID: 784
		Assembly = 3,
		// Token: 0x04000311 RID: 785
		Family = 4,
		// Token: 0x04000312 RID: 786
		FamORAssem = 5,
		// Token: 0x04000313 RID: 787
		Public = 6,
		// Token: 0x04000314 RID: 788
		Static = 16,
		// Token: 0x04000315 RID: 789
		InitOnly = 32,
		// Token: 0x04000316 RID: 790
		Literal = 64,
		// Token: 0x04000317 RID: 791
		NotSerialized = 128,
		// Token: 0x04000318 RID: 792
		SpecialName = 512,
		// Token: 0x04000319 RID: 793
		PInvokeImpl = 8192,
		// Token: 0x0400031A RID: 794
		RTSpecialName = 1024,
		// Token: 0x0400031B RID: 795
		HasFieldMarshal = 4096,
		// Token: 0x0400031C RID: 796
		HasDefault = 32768,
		// Token: 0x0400031D RID: 797
		HasFieldRVA = 256
	}
}
