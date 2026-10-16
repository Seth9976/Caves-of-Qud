using System;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200000F RID: 15
	[Flags]
	public enum AudioClientStreamFlags
	{
		// Token: 0x04000048 RID: 72
		None = 0,
		// Token: 0x04000049 RID: 73
		CrossProcess = 65536,
		// Token: 0x0400004A RID: 74
		Loopback = 131072,
		// Token: 0x0400004B RID: 75
		EventCallback = 262144,
		// Token: 0x0400004C RID: 76
		NoPersist = 524288
	}
}
