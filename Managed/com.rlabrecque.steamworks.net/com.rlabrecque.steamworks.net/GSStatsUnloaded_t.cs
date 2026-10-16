using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000042 RID: 66
	[CallbackIdentity(1108)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSStatsUnloaded_t
	{
		// Token: 0x040000EB RID: 235
		public const int k_iCallback = 1108;

		// Token: 0x040000EC RID: 236
		public CSteamID m_steamIDUser;
	}
}
