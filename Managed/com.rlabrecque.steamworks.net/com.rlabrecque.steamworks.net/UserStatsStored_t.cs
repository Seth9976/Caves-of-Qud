using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D9 RID: 217
	[CallbackIdentity(1102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsStored_t
	{
		// Token: 0x0400032B RID: 811
		public const int k_iCallback = 1102;

		// Token: 0x0400032C RID: 812
		public ulong m_nGameID;

		// Token: 0x0400032D RID: 813
		public EResult m_eResult;
	}
}
