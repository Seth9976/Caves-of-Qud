using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DF RID: 223
	[CallbackIdentity(1108)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsUnloaded_t
	{
		// Token: 0x04000345 RID: 837
		public const int k_iCallback = 1108;

		// Token: 0x04000346 RID: 838
		public CSteamID m_steamIDUser;
	}
}
