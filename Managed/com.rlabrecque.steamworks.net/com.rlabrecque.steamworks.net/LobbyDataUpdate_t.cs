using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006A RID: 106
	[CallbackIdentity(505)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyDataUpdate_t
	{
		// Token: 0x04000198 RID: 408
		public const int k_iCallback = 505;

		// Token: 0x04000199 RID: 409
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400019A RID: 410
		public ulong m_ulSteamIDMember;

		// Token: 0x0400019B RID: 411
		public byte m_bSuccess;
	}
}
