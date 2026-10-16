using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D5 RID: 213
	[CallbackIdentity(166)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MarketEligibilityResponse_t
	{
		// Token: 0x04000313 RID: 787
		public const int k_iCallback = 166;

		// Token: 0x04000314 RID: 788
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAllowed;

		// Token: 0x04000315 RID: 789
		public EMarketNotAllowedReasonFlags m_eNotAllowedReason;

		// Token: 0x04000316 RID: 790
		public RTime32 m_rtAllowedAtTime;

		// Token: 0x04000317 RID: 791
		public int m_cdaySteamGuardRequiredDays;

		// Token: 0x04000318 RID: 792
		public int m_cdayNewDeviceCooldown;
	}
}
