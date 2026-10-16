using System;

namespace Unity.Profiling
{
	// Token: 0x0200006A RID: 106
	[Flags]
	public enum ProfilerCounterOptions : ushort
	{
		// Token: 0x0400017F RID: 383
		None = 0,
		// Token: 0x04000180 RID: 384
		FlushOnEndOfFrame = 2,
		// Token: 0x04000181 RID: 385
		ResetToZeroOnFlush = 4
	}
}
