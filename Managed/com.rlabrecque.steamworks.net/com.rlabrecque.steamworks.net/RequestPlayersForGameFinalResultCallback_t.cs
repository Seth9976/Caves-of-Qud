using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000076 RID: 118
	[CallbackIdentity(5213)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RequestPlayersForGameFinalResultCallback_t
	{
		// Token: 0x040001D2 RID: 466
		public const int k_iCallback = 5213;

		// Token: 0x040001D3 RID: 467
		public EResult m_eResult;

		// Token: 0x040001D4 RID: 468
		public ulong m_ullSearchID;

		// Token: 0x040001D5 RID: 469
		public ulong m_ullUniqueGameID;
	}
}
