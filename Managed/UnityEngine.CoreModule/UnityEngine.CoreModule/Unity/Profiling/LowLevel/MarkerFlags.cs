using System;

namespace Unity.Profiling.LowLevel
{
	// Token: 0x02000074 RID: 116
	[Flags]
	public enum MarkerFlags : ushort
	{
		// Token: 0x040001A3 RID: 419
		Default = 0,
		// Token: 0x040001A4 RID: 420
		Script = 2,
		// Token: 0x040001A5 RID: 421
		ScriptInvoke = 32,
		// Token: 0x040001A6 RID: 422
		ScriptDeepProfiler = 64,
		// Token: 0x040001A7 RID: 423
		AvailabilityEditor = 4,
		// Token: 0x040001A8 RID: 424
		AvailabilityNonDevelopment = 8,
		// Token: 0x040001A9 RID: 425
		Warning = 16,
		// Token: 0x040001AA RID: 426
		Counter = 128,
		// Token: 0x040001AB RID: 427
		SampleGPU = 256
	}
}
