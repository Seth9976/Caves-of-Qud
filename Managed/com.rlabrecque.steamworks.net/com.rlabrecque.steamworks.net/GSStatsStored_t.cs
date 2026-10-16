using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000041 RID: 65
	[CallbackIdentity(1801)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSStatsStored_t
	{
		// Token: 0x040000E8 RID: 232
		public const int k_iCallback = 1801;

		// Token: 0x040000E9 RID: 233
		public EResult m_eResult;

		// Token: 0x040000EA RID: 234
		public CSteamID m_steamIDUser;
	}
}
