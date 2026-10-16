using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E3 RID: 227
	[CallbackIdentity(1112)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalStatsReceived_t
	{
		// Token: 0x04000352 RID: 850
		public const int k_iCallback = 1112;

		// Token: 0x04000353 RID: 851
		public ulong m_nGameID;

		// Token: 0x04000354 RID: 852
		public EResult m_eResult;
	}
}
