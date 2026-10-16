using System;

namespace Mono.Cecil
{
	// Token: 0x020001B4 RID: 436
	[Flags]
	public enum AssemblyAttributes : uint
	{
		// Token: 0x04000275 RID: 629
		PublicKey = 1U,
		// Token: 0x04000276 RID: 630
		SideBySideCompatible = 0U,
		// Token: 0x04000277 RID: 631
		Retargetable = 256U,
		// Token: 0x04000278 RID: 632
		WindowsRuntime = 512U,
		// Token: 0x04000279 RID: 633
		DisableJITCompileOptimizer = 16384U,
		// Token: 0x0400027A RID: 634
		EnableJITCompileTracking = 32768U
	}
}
