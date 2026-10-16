using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000073 RID: 115
	[CallbackIdentity(5202)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SearchForGameResultCallback_t
	{
		// Token: 0x040001BD RID: 445
		public const int k_iCallback = 5202;

		// Token: 0x040001BE RID: 446
		public ulong m_ullSearchID;

		// Token: 0x040001BF RID: 447
		public EResult m_eResult;

		// Token: 0x040001C0 RID: 448
		public int m_nCountPlayersInGame;

		// Token: 0x040001C1 RID: 449
		public int m_nCountAcceptedGame;

		// Token: 0x040001C2 RID: 450
		public CSteamID m_steamIDHost;

		// Token: 0x040001C3 RID: 451
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bFinalCallback;
	}
}
