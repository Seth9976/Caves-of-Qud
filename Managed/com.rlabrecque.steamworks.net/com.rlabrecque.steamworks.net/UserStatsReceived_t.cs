using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D8 RID: 216
	[CallbackIdentity(1101)]
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	public struct UserStatsReceived_t
	{
		// Token: 0x04000327 RID: 807
		public const int k_iCallback = 1101;

		// Token: 0x04000328 RID: 808
		[FieldOffset(0)]
		public ulong m_nGameID;

		// Token: 0x04000329 RID: 809
		[FieldOffset(8)]
		public EResult m_eResult;

		// Token: 0x0400032A RID: 810
		[FieldOffset(12)]
		public CSteamID m_steamIDUser;
	}
}
