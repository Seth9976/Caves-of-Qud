using System;

namespace Steamworks
{
	// Token: 0x0200012B RID: 299
	[Flags]
	public enum EItemState
	{
		// Token: 0x04000796 RID: 1942
		k_EItemStateNone = 0,
		// Token: 0x04000797 RID: 1943
		k_EItemStateSubscribed = 1,
		// Token: 0x04000798 RID: 1944
		k_EItemStateLegacyItem = 2,
		// Token: 0x04000799 RID: 1945
		k_EItemStateInstalled = 4,
		// Token: 0x0400079A RID: 1946
		k_EItemStateNeedsUpdate = 8,
		// Token: 0x0400079B RID: 1947
		k_EItemStateDownloading = 16,
		// Token: 0x0400079C RID: 1948
		k_EItemStateDownloadPending = 32,
		// Token: 0x0400079D RID: 1949
		k_EItemStateDisabledLocally = 64
	}
}
