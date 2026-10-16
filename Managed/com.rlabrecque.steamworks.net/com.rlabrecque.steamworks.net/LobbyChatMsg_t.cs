using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006C RID: 108
	[CallbackIdentity(507)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyChatMsg_t
	{
		// Token: 0x040001A1 RID: 417
		public const int k_iCallback = 507;

		// Token: 0x040001A2 RID: 418
		public ulong m_ulSteamIDLobby;

		// Token: 0x040001A3 RID: 419
		public ulong m_ulSteamIDUser;

		// Token: 0x040001A4 RID: 420
		public byte m_eChatEntryType;

		// Token: 0x040001A5 RID: 421
		public uint m_iChatID;
	}
}
