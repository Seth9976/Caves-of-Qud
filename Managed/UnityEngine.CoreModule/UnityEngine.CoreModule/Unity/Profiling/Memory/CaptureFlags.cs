using System;

namespace Unity.Profiling.Memory
{
	// Token: 0x0200007C RID: 124
	[Flags]
	public enum CaptureFlags : uint
	{
		// Token: 0x040001E5 RID: 485
		ManagedObjects = 1U,
		// Token: 0x040001E6 RID: 486
		NativeObjects = 2U,
		// Token: 0x040001E7 RID: 487
		NativeAllocations = 4U,
		// Token: 0x040001E8 RID: 488
		NativeAllocationSites = 8U,
		// Token: 0x040001E9 RID: 489
		NativeStackTraces = 16U
	}
}
