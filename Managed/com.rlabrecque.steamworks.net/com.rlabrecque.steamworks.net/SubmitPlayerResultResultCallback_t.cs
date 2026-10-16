using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000077 RID: 119
	[CallbackIdentity(5214)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SubmitPlayerResultResultCallback_t
	{
		// Token: 0x040001D6 RID: 470
		public const int k_iCallback = 5214;

		// Token: 0x040001D7 RID: 471
		public EResult m_eResult;

		// Token: 0x040001D8 RID: 472
		public ulong ullUniqueGameID;

		// Token: 0x040001D9 RID: 473
		public CSteamID steamIDPlayer;
	}
}
