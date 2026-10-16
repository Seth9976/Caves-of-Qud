using System;

namespace Steamworks
{
	// Token: 0x02000113 RID: 275
	public enum EP2PSessionError
	{
		// Token: 0x040006CF RID: 1743
		k_EP2PSessionErrorNone,
		// Token: 0x040006D0 RID: 1744
		k_EP2PSessionErrorNoRightsToApp = 2,
		// Token: 0x040006D1 RID: 1745
		k_EP2PSessionErrorTimeout = 4,
		// Token: 0x040006D2 RID: 1746
		k_EP2PSessionErrorNotRunningApp_DELETED = 1,
		// Token: 0x040006D3 RID: 1747
		k_EP2PSessionErrorDestinationNotLoggedIn_DELETED = 3,
		// Token: 0x040006D4 RID: 1748
		k_EP2PSessionErrorMax = 5
	}
}
