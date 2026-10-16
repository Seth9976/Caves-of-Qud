using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000068 RID: 104
	[CallbackIdentity(503)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyInvite_t
	{
		// Token: 0x0400018F RID: 399
		public const int k_iCallback = 503;

		// Token: 0x04000190 RID: 400
		public ulong m_ulSteamIDUser;

		// Token: 0x04000191 RID: 401
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000192 RID: 402
		public ulong m_ulGameID;
	}
}
