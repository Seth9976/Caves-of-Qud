using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000046 RID: 70
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum MmapProts
	{
		// Token: 0x0400035D RID: 861
		PROT_READ = 1,
		// Token: 0x0400035E RID: 862
		PROT_WRITE = 2,
		// Token: 0x0400035F RID: 863
		PROT_EXEC = 4,
		// Token: 0x04000360 RID: 864
		PROT_NONE = 0,
		// Token: 0x04000361 RID: 865
		PROT_GROWSDOWN = 16777216,
		// Token: 0x04000362 RID: 866
		PROT_GROWSUP = 33554432
	}
}
