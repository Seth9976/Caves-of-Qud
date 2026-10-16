using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000024 RID: 36
	[CallbackIdentity(336)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendRichPresenceUpdate_t
	{
		// Token: 0x04000080 RID: 128
		public const int k_iCallback = 336;

		// Token: 0x04000081 RID: 129
		public CSteamID m_steamIDFriend;

		// Token: 0x04000082 RID: 130
		public AppId_t m_nAppID;
	}
}
