using System;

namespace Unity.Profiling
{
	// Token: 0x0200006D RID: 109
	[Flags]
	public enum ProfilerRecorderOptions
	{
		// Token: 0x04000186 RID: 390
		None = 0,
		// Token: 0x04000187 RID: 391
		StartImmediately = 1,
		// Token: 0x04000188 RID: 392
		KeepAliveDuringDomainReload = 2,
		// Token: 0x04000189 RID: 393
		CollectOnlyOnCurrentThread = 4,
		// Token: 0x0400018A RID: 394
		WrapAroundWhenCapacityReached = 8,
		// Token: 0x0400018B RID: 395
		SumAllSamplesInFrame = 16,
		// Token: 0x0400018C RID: 396
		GpuRecorder = 64,
		// Token: 0x0400018D RID: 397
		Default = 24
	}
}
