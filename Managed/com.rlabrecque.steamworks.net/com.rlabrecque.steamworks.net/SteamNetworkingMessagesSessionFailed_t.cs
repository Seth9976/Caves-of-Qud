using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000093 RID: 147
	[CallbackIdentity(1252)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetworkingMessagesSessionFailed_t
	{
		// Token: 0x0400020F RID: 527
		public const int k_iCallback = 1252;

		// Token: 0x04000210 RID: 528
		public SteamNetConnectionInfo_t m_info;
	}
}
