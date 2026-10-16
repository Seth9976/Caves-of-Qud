using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000023 RID: 35
	[CallbackIdentity(335)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ClanOfficerListResponse_t
	{
		// Token: 0x0400007C RID: 124
		public const int k_iCallback = 335;

		// Token: 0x0400007D RID: 125
		public CSteamID m_steamIDClan;

		// Token: 0x0400007E RID: 126
		public int m_cOfficers;

		// Token: 0x0400007F RID: 127
		public byte m_bSuccess;
	}
}
