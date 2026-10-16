using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002B RID: 43
	[CallbackIdentity(343)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameConnectedFriendChatMsg_t
	{
		// Token: 0x04000097 RID: 151
		public const int k_iCallback = 343;

		// Token: 0x04000098 RID: 152
		public CSteamID m_steamIDUser;

		// Token: 0x04000099 RID: 153
		public int m_iMessageID;
	}
}
