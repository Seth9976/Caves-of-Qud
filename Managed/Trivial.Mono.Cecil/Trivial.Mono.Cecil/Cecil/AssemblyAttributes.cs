using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200000E RID: 14
	[Flags]
	public enum AssemblyAttributes : uint
	{
		// Token: 0x04000012 RID: 18
		PublicKey = 1U,
		// Token: 0x04000013 RID: 19
		SideBySideCompatible = 0U,
		// Token: 0x04000014 RID: 20
		Retargetable = 256U,
		// Token: 0x04000015 RID: 21
		WindowsRuntime = 512U,
		// Token: 0x04000016 RID: 22
		DisableJITCompileOptimizer = 16384U,
		// Token: 0x04000017 RID: 23
		EnableJITCompileTracking = 32768U
	}
}
