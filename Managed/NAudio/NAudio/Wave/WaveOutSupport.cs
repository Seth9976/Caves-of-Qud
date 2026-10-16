using System;

namespace NAudio.Wave
{
	// Token: 0x02000184 RID: 388
	[Flags]
	internal enum WaveOutSupport
	{
		// Token: 0x04000958 RID: 2392
		Pitch = 1,
		// Token: 0x04000959 RID: 2393
		PlaybackRate = 2,
		// Token: 0x0400095A RID: 2394
		Volume = 4,
		// Token: 0x0400095B RID: 2395
		LRVolume = 8,
		// Token: 0x0400095C RID: 2396
		Sync = 16,
		// Token: 0x0400095D RID: 2397
		SampleAccurate = 32
	}
}
