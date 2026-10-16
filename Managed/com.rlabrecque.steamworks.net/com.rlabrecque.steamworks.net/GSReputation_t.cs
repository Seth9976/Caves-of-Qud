using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200003D RID: 61
	[CallbackIdentity(209)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSReputation_t
	{
		// Token: 0x040000D5 RID: 213
		public const int k_iCallback = 209;

		// Token: 0x040000D6 RID: 214
		public EResult m_eResult;

		// Token: 0x040000D7 RID: 215
		public uint m_unReputationScore;

		// Token: 0x040000D8 RID: 216
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;

		// Token: 0x040000D9 RID: 217
		public uint m_unBannedIP;

		// Token: 0x040000DA RID: 218
		public ushort m_usBannedPort;

		// Token: 0x040000DB RID: 219
		public ulong m_ulBannedGameID;

		// Token: 0x040000DC RID: 220
		public uint m_unBanExpires;
	}
}
