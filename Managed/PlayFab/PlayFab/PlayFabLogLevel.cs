using System;

namespace PlayFab
{
	// Token: 0x02000027 RID: 39
	[Flags]
	public enum PlayFabLogLevel
	{
		// Token: 0x040002B4 RID: 692
		None = 0,
		// Token: 0x040002B5 RID: 693
		Debug = 1,
		// Token: 0x040002B6 RID: 694
		Info = 2,
		// Token: 0x040002B7 RID: 695
		Warning = 4,
		// Token: 0x040002B8 RID: 696
		Error = 8,
		// Token: 0x040002B9 RID: 697
		All = 15
	}
}
