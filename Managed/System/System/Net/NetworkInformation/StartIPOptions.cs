using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020006F3 RID: 1779
	[Flags]
	internal enum StartIPOptions
	{
		// Token: 0x040021AE RID: 8622
		Both = 3,
		// Token: 0x040021AF RID: 8623
		None = 0,
		// Token: 0x040021B0 RID: 8624
		StartIPv4 = 1,
		// Token: 0x040021B1 RID: 8625
		StartIPv6 = 2
	}
}
