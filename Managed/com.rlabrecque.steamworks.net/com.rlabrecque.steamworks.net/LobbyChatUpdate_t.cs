using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006B RID: 107
	[CallbackIdentity(506)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyChatUpdate_t
	{
		// Token: 0x0400019C RID: 412
		public const int k_iCallback = 506;

		// Token: 0x0400019D RID: 413
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400019E RID: 414
		public ulong m_ulSteamIDUserChanged;

		// Token: 0x0400019F RID: 415
		public ulong m_ulSteamIDMakingChange;

		// Token: 0x040001A0 RID: 416
		public uint m_rgfChatMemberStateChange;
	}
}
