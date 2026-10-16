using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000026 RID: 38
	[CallbackIdentity(338)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameConnectedClanChatMsg_t
	{
		// Token: 0x04000086 RID: 134
		public const int k_iCallback = 338;

		// Token: 0x04000087 RID: 135
		public CSteamID m_steamIDClanChat;

		// Token: 0x04000088 RID: 136
		public CSteamID m_steamIDUser;

		// Token: 0x04000089 RID: 137
		public int m_iMessageID;
	}
}
