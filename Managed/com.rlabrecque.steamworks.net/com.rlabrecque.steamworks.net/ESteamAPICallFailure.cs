using System;

namespace Steamworks
{
	// Token: 0x02000134 RID: 308
	public enum ESteamAPICallFailure
	{
		// Token: 0x040007D0 RID: 2000
		k_ESteamAPICallFailureNone = -1,
		// Token: 0x040007D1 RID: 2001
		k_ESteamAPICallFailureSteamGone,
		// Token: 0x040007D2 RID: 2002
		k_ESteamAPICallFailureNetworkFailure,
		// Token: 0x040007D3 RID: 2003
		k_ESteamAPICallFailureInvalidHandle,
		// Token: 0x040007D4 RID: 2004
		k_ESteamAPICallFailureMismatchedCallback
	}
}
