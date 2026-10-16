using System;

namespace Mono.Cecil
{
	// Token: 0x020001B5 RID: 437
	public enum AssemblyHashAlgorithm : uint
	{
		// Token: 0x0400027C RID: 636
		None,
		// Token: 0x0400027D RID: 637
		MD5 = 32771U,
		// Token: 0x0400027E RID: 638
		SHA1,
		// Token: 0x0400027F RID: 639
		SHA256 = 32780U,
		// Token: 0x04000280 RID: 640
		SHA384,
		// Token: 0x04000281 RID: 641
		SHA512,
		// Token: 0x04000282 RID: 642
		Reserved = 32771U
	}
}
