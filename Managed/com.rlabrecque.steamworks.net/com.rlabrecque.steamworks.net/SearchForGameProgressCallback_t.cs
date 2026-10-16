using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000072 RID: 114
	[CallbackIdentity(5201)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SearchForGameProgressCallback_t
	{
		// Token: 0x040001B6 RID: 438
		public const int k_iCallback = 5201;

		// Token: 0x040001B7 RID: 439
		public ulong m_ullSearchID;

		// Token: 0x040001B8 RID: 440
		public EResult m_eResult;

		// Token: 0x040001B9 RID: 441
		public CSteamID m_lobbyID;

		// Token: 0x040001BA RID: 442
		public CSteamID m_steamIDEndedSearch;

		// Token: 0x040001BB RID: 443
		public int m_nSecondsRemainingEstimate;

		// Token: 0x040001BC RID: 444
		public int m_cPlayersSearching;
	}
}
