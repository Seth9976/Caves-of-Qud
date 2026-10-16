using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000C7 RID: 199
	public enum SFSampleLink : ushort
	{
		// Token: 0x04000529 RID: 1321
		MonoSample = 1,
		// Token: 0x0400052A RID: 1322
		RightSample,
		// Token: 0x0400052B RID: 1323
		LeftSample = 4,
		// Token: 0x0400052C RID: 1324
		LinkedSample = 8,
		// Token: 0x0400052D RID: 1325
		RomMonoSample = 32769,
		// Token: 0x0400052E RID: 1326
		RomRightSample,
		// Token: 0x0400052F RID: 1327
		RomLeftSample = 32772,
		// Token: 0x04000530 RID: 1328
		RomLinkedSample = 32776
	}
}
