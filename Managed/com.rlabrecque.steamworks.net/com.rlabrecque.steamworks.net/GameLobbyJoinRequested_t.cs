using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000021 RID: 33
	[CallbackIdentity(333)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameLobbyJoinRequested_t
	{
		// Token: 0x04000074 RID: 116
		public const int k_iCallback = 333;

		// Token: 0x04000075 RID: 117
		public CSteamID m_steamIDLobby;

		// Token: 0x04000076 RID: 118
		public CSteamID m_steamIDFriend;
	}
}
