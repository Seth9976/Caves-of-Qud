using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000099 RID: 153
	[CallbackIdentity(5702)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamRemotePlaySessionDisconnected_t
	{
		// Token: 0x04000221 RID: 545
		public const int k_iCallback = 5702;

		// Token: 0x04000222 RID: 546
		public RemotePlaySessionID_t m_unSessionID;
	}
}
