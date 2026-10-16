using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000092 RID: 146
	[CallbackIdentity(1251)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetworkingMessagesSessionRequest_t
	{
		// Token: 0x0400020D RID: 525
		public const int k_iCallback = 1251;

		// Token: 0x0400020E RID: 526
		public SteamNetworkingIdentity m_identityRemote;
	}
}
