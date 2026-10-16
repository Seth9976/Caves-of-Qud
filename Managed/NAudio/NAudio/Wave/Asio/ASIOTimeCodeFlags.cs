using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000150 RID: 336
	[Flags]
	internal enum ASIOTimeCodeFlags
	{
		// Token: 0x04000772 RID: 1906
		kTcValid = 1,
		// Token: 0x04000773 RID: 1907
		kTcRunning = 2,
		// Token: 0x04000774 RID: 1908
		kTcReverse = 4,
		// Token: 0x04000775 RID: 1909
		kTcOnspeed = 8,
		// Token: 0x04000776 RID: 1910
		kTcStill = 16,
		// Token: 0x04000777 RID: 1911
		kTcSpeedValid = 256
	}
}
