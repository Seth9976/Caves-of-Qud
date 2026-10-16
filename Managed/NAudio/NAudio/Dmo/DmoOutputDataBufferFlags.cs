using System;

namespace NAudio.Dmo
{
	// Token: 0x020001FC RID: 508
	[Flags]
	public enum DmoOutputDataBufferFlags
	{
		// Token: 0x04000C08 RID: 3080
		None = 0,
		// Token: 0x04000C09 RID: 3081
		SyncPoint = 1,
		// Token: 0x04000C0A RID: 3082
		Time = 2,
		// Token: 0x04000C0B RID: 3083
		TimeLength = 4,
		// Token: 0x04000C0C RID: 3084
		Incomplete = 16777216
	}
}
