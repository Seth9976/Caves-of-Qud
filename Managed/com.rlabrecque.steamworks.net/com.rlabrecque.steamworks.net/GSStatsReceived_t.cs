using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000040 RID: 64
	[CallbackIdentity(1800)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSStatsReceived_t
	{
		// Token: 0x040000E5 RID: 229
		public const int k_iCallback = 1800;

		// Token: 0x040000E6 RID: 230
		public EResult m_eResult;

		// Token: 0x040000E7 RID: 231
		public CSteamID m_steamIDUser;
	}
}
