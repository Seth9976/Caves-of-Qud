using System;

namespace Steamworks
{
	// Token: 0x02000119 RID: 281
	[Flags]
	public enum ERemoteStoragePlatform
	{
		// Token: 0x04000704 RID: 1796
		k_ERemoteStoragePlatformNone = 0,
		// Token: 0x04000705 RID: 1797
		k_ERemoteStoragePlatformWindows = 1,
		// Token: 0x04000706 RID: 1798
		k_ERemoteStoragePlatformOSX = 2,
		// Token: 0x04000707 RID: 1799
		k_ERemoteStoragePlatformPS3 = 4,
		// Token: 0x04000708 RID: 1800
		k_ERemoteStoragePlatformLinux = 8,
		// Token: 0x04000709 RID: 1801
		k_ERemoteStoragePlatformSwitch = 16,
		// Token: 0x0400070A RID: 1802
		k_ERemoteStoragePlatformAndroid = 32,
		// Token: 0x0400070B RID: 1803
		k_ERemoteStoragePlatformIOS = 64,
		// Token: 0x0400070C RID: 1804
		k_ERemoteStoragePlatformAll = -1
	}
}
