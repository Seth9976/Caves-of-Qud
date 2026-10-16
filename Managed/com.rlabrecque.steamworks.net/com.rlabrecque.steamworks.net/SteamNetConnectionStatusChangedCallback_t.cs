using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000094 RID: 148
	[CallbackIdentity(1221)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionStatusChangedCallback_t
	{
		// Token: 0x04000211 RID: 529
		public const int k_iCallback = 1221;

		// Token: 0x04000212 RID: 530
		public HSteamNetConnection m_hConn;

		// Token: 0x04000213 RID: 531
		public SteamNetConnectionInfo_t m_info;

		// Token: 0x04000214 RID: 532
		public ESteamNetworkingConnectionState m_eOldState;
	}
}
