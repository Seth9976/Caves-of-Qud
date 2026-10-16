using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200003F RID: 63
	[CallbackIdentity(211)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ComputeNewPlayerCompatibilityResult_t
	{
		// Token: 0x040000DF RID: 223
		public const int k_iCallback = 211;

		// Token: 0x040000E0 RID: 224
		public EResult m_eResult;

		// Token: 0x040000E1 RID: 225
		public int m_cPlayersThatDontLikeCandidate;

		// Token: 0x040000E2 RID: 226
		public int m_cPlayersThatCandidateDoesntLike;

		// Token: 0x040000E3 RID: 227
		public int m_cClanPlayersThatDontLikeCandidate;

		// Token: 0x040000E4 RID: 228
		public CSteamID m_SteamIDCandidate;
	}
}
