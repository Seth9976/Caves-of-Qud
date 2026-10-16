using System;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000022 RID: 34
	public enum AudioStreamCategory
	{
		// Token: 0x0400006C RID: 108
		Other,
		// Token: 0x0400006D RID: 109
		ForegroundOnlyMedia,
		// Token: 0x0400006E RID: 110
		BackgroundCapableMedia,
		// Token: 0x0400006F RID: 111
		Communications,
		// Token: 0x04000070 RID: 112
		Alerts,
		// Token: 0x04000071 RID: 113
		SoundEffects,
		// Token: 0x04000072 RID: 114
		GameEffects,
		// Token: 0x04000073 RID: 115
		GameMedia
	}
}
