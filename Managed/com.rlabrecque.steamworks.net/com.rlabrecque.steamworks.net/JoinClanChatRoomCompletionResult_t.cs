using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002A RID: 42
	[CallbackIdentity(342)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct JoinClanChatRoomCompletionResult_t
	{
		// Token: 0x04000094 RID: 148
		public const int k_iCallback = 342;

		// Token: 0x04000095 RID: 149
		public CSteamID m_steamIDClanChat;

		// Token: 0x04000096 RID: 150
		public EChatRoomEnterResponse m_eChatRoomEnterResponse;
	}
}
