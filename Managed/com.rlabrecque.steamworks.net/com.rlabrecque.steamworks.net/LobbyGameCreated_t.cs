using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006D RID: 109
	[CallbackIdentity(509)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyGameCreated_t
	{
		// Token: 0x040001A6 RID: 422
		public const int k_iCallback = 509;

		// Token: 0x040001A7 RID: 423
		public ulong m_ulSteamIDLobby;

		// Token: 0x040001A8 RID: 424
		public ulong m_ulSteamIDGameServer;

		// Token: 0x040001A9 RID: 425
		public uint m_unIP;

		// Token: 0x040001AA RID: 426
		public ushort m_usPort;
	}
}
