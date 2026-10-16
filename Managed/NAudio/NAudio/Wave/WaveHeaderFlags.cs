using System;

namespace NAudio.Wave
{
	// Token: 0x02000178 RID: 376
	[Flags]
	public enum WaveHeaderFlags
	{
		// Token: 0x04000903 RID: 2307
		BeginLoop = 4,
		// Token: 0x04000904 RID: 2308
		Done = 1,
		// Token: 0x04000905 RID: 2309
		EndLoop = 8,
		// Token: 0x04000906 RID: 2310
		InQueue = 16,
		// Token: 0x04000907 RID: 2311
		Prepared = 2
	}
}
