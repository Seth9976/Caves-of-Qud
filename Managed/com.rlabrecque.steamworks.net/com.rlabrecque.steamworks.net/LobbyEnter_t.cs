using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000069 RID: 105
	[CallbackIdentity(504)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyEnter_t
	{
		// Token: 0x04000193 RID: 403
		public const int k_iCallback = 504;

		// Token: 0x04000194 RID: 404
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000195 RID: 405
		public uint m_rgfChatPermissions;

		// Token: 0x04000196 RID: 406
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLocked;

		// Token: 0x04000197 RID: 407
		public uint m_EChatRoomEnterResponse;
	}
}
