using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000027 RID: 39
	[CallbackIdentity(339)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameConnectedChatJoin_t
	{
		// Token: 0x0400008A RID: 138
		public const int k_iCallback = 339;

		// Token: 0x0400008B RID: 139
		public CSteamID m_steamIDClanChat;

		// Token: 0x0400008C RID: 140
		public CSteamID m_steamIDUser;
	}
}
