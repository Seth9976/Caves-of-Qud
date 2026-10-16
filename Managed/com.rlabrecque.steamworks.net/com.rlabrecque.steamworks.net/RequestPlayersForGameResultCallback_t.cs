using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000075 RID: 117
	[CallbackIdentity(5212)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RequestPlayersForGameResultCallback_t
	{
		// Token: 0x040001C7 RID: 455
		public const int k_iCallback = 5212;

		// Token: 0x040001C8 RID: 456
		public EResult m_eResult;

		// Token: 0x040001C9 RID: 457
		public ulong m_ullSearchID;

		// Token: 0x040001CA RID: 458
		public CSteamID m_SteamIDPlayerFound;

		// Token: 0x040001CB RID: 459
		public CSteamID m_SteamIDLobby;

		// Token: 0x040001CC RID: 460
		public PlayerAcceptState_t m_ePlayerAcceptState;

		// Token: 0x040001CD RID: 461
		public int m_nPlayerIndex;

		// Token: 0x040001CE RID: 462
		public int m_nTotalPlayersFound;

		// Token: 0x040001CF RID: 463
		public int m_nTotalPlayersAcceptedGame;

		// Token: 0x040001D0 RID: 464
		public int m_nSuggestedTeamIndex;

		// Token: 0x040001D1 RID: 465
		public ulong m_ullUniqueGameID;
	}
}
