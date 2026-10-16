using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002D RID: 45
	[CallbackIdentity(345)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendsIsFollowing_t
	{
		// Token: 0x0400009E RID: 158
		public const int k_iCallback = 345;

		// Token: 0x0400009F RID: 159
		public EResult m_eResult;

		// Token: 0x040000A0 RID: 160
		public CSteamID m_steamID;

		// Token: 0x040000A1 RID: 161
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bIsFollowing;
	}
}
