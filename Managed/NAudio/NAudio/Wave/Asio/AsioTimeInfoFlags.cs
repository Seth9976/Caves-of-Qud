using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000152 RID: 338
	[Flags]
	internal enum AsioTimeInfoFlags
	{
		// Token: 0x0400077F RID: 1919
		kSystemTimeValid = 1,
		// Token: 0x04000780 RID: 1920
		kSamplePositionValid = 2,
		// Token: 0x04000781 RID: 1921
		kSampleRateValid = 4,
		// Token: 0x04000782 RID: 1922
		kSpeedValid = 8,
		// Token: 0x04000783 RID: 1923
		kSampleRateChanged = 16,
		// Token: 0x04000784 RID: 1924
		kClockSourceChanged = 32
	}
}
