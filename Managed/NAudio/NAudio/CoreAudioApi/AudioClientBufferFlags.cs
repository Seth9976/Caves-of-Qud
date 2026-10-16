using System;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200000C RID: 12
	[Flags]
	public enum AudioClientBufferFlags
	{
		// Token: 0x0400003C RID: 60
		None = 0,
		// Token: 0x0400003D RID: 61
		DataDiscontinuity = 1,
		// Token: 0x0400003E RID: 62
		Silent = 2,
		// Token: 0x0400003F RID: 63
		TimestampError = 4
	}
}
