using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000070 RID: 112
	[CallbackIdentity(513)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyCreated_t
	{
		// Token: 0x040001B1 RID: 433
		public const int k_iCallback = 513;

		// Token: 0x040001B2 RID: 434
		public EResult m_eResult;

		// Token: 0x040001B3 RID: 435
		public ulong m_ulSteamIDLobby;
	}
}
