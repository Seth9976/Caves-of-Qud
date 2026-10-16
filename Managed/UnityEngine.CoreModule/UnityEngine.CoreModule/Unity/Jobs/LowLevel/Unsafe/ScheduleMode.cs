using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000056 RID: 86
	public enum ScheduleMode
	{
		// Token: 0x04000111 RID: 273
		Run,
		// Token: 0x04000112 RID: 274
		[Obsolete("Batched is obsolete, use Parallel or Single depending on job type. (UnityUpgradable) -> Parallel", false)]
		Batched,
		// Token: 0x04000113 RID: 275
		Parallel = 1,
		// Token: 0x04000114 RID: 276
		Single
	}
}
