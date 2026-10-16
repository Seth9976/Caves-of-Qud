using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000055 RID: 85
	public struct JobRanges
	{
		// Token: 0x0400010C RID: 268
		internal int BatchSize;

		// Token: 0x0400010D RID: 269
		internal int NumJobs;

		// Token: 0x0400010E RID: 270
		public int TotalIterationCount;

		// Token: 0x0400010F RID: 271
		internal IntPtr StartEndIndex;
	}
}
