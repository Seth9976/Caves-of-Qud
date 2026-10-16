using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000098 RID: 152
	[CallbackIdentity(5701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamRemotePlaySessionConnected_t
	{
		// Token: 0x0400021F RID: 543
		public const int k_iCallback = 5701;

		// Token: 0x04000220 RID: 544
		public RemotePlaySessionID_t m_unSessionID;
	}
}
