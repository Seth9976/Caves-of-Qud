using System;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200000D RID: 13
	public struct AudioClientProperties
	{
		// Token: 0x04000040 RID: 64
		public uint cbSize;

		// Token: 0x04000041 RID: 65
		public int bIsOffload;

		// Token: 0x04000042 RID: 66
		public AudioStreamCategory eCategory;

		// Token: 0x04000043 RID: 67
		public AudioClientStreamOptions Options;
	}
}
