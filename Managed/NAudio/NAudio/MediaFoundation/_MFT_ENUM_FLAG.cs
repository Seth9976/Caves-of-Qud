using System;

namespace NAudio.MediaFoundation
{
	// Token: 0x0200005E RID: 94
	[Flags]
	public enum _MFT_ENUM_FLAG
	{
		// Token: 0x04000315 RID: 789
		None = 0,
		// Token: 0x04000316 RID: 790
		MFT_ENUM_FLAG_SYNCMFT = 1,
		// Token: 0x04000317 RID: 791
		MFT_ENUM_FLAG_ASYNCMFT = 2,
		// Token: 0x04000318 RID: 792
		MFT_ENUM_FLAG_HARDWARE = 4,
		// Token: 0x04000319 RID: 793
		MFT_ENUM_FLAG_FIELDOFUSE = 8,
		// Token: 0x0400031A RID: 794
		MFT_ENUM_FLAG_LOCALMFT = 16,
		// Token: 0x0400031B RID: 795
		MFT_ENUM_FLAG_TRANSCODE_ONLY = 32,
		// Token: 0x0400031C RID: 796
		MFT_ENUM_FLAG_SORTANDFILTER = 64,
		// Token: 0x0400031D RID: 797
		MFT_ENUM_FLAG_ALL = 63
	}
}
