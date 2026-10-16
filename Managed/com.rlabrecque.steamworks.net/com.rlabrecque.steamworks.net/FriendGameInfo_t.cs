using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000160 RID: 352
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FriendGameInfo_t
	{
		// Token: 0x04000A30 RID: 2608
		public CGameID m_gameID;

		// Token: 0x04000A31 RID: 2609
		public uint m_unGameIP;

		// Token: 0x04000A32 RID: 2610
		public ushort m_usGamePort;

		// Token: 0x04000A33 RID: 2611
		public ushort m_usQueryPort;

		// Token: 0x04000A34 RID: 2612
		public CSteamID m_steamIDLobby;
	}
}
