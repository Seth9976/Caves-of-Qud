using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CF RID: 207
	[CallbackIdentity(143)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ValidateAuthTicketResponse_t
	{
		// Token: 0x04000302 RID: 770
		public const int k_iCallback = 143;

		// Token: 0x04000303 RID: 771
		public CSteamID m_SteamID;

		// Token: 0x04000304 RID: 772
		public EAuthSessionResponse m_eAuthSessionResponse;

		// Token: 0x04000305 RID: 773
		public CSteamID m_OwnerSteamID;
	}
}
