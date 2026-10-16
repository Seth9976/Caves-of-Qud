using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006F RID: 111
	[CallbackIdentity(512)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyKicked_t
	{
		// Token: 0x040001AD RID: 429
		public const int k_iCallback = 512;

		// Token: 0x040001AE RID: 430
		public ulong m_ulSteamIDLobby;

		// Token: 0x040001AF RID: 431
		public ulong m_ulSteamIDAdmin;

		// Token: 0x040001B0 RID: 432
		public byte m_bKickedDueToDisconnect;
	}
}
