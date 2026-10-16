using System;

namespace NAudio.Dmo
{
	// Token: 0x020001F8 RID: 504
	[Flags]
	public enum DmoInputDataBufferFlags
	{
		// Token: 0x04000BF3 RID: 3059
		None = 0,
		// Token: 0x04000BF4 RID: 3060
		SyncPoint = 1,
		// Token: 0x04000BF5 RID: 3061
		Time = 2,
		// Token: 0x04000BF6 RID: 3062
		TimeLength = 4
	}
}
