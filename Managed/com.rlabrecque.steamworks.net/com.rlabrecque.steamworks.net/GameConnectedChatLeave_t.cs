using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000028 RID: 40
	[CallbackIdentity(340)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GameConnectedChatLeave_t
	{
		// Token: 0x0400008D RID: 141
		public const int k_iCallback = 340;

		// Token: 0x0400008E RID: 142
		public CSteamID m_steamIDClanChat;

		// Token: 0x0400008F RID: 143
		public CSteamID m_steamIDUser;

		// Token: 0x04000090 RID: 144
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bKicked;

		// Token: 0x04000091 RID: 145
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDropped;
	}
}
