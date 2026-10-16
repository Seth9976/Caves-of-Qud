using System;

namespace NVorbis.Ogg
{
	// Token: 0x02000012 RID: 18
	[Flags]
	internal enum PageFlags
	{
		// Token: 0x0400005B RID: 91
		None = 0,
		// Token: 0x0400005C RID: 92
		ContinuesPacket = 1,
		// Token: 0x0400005D RID: 93
		BeginningOfStream = 2,
		// Token: 0x0400005E RID: 94
		EndOfStream = 4
	}
}
