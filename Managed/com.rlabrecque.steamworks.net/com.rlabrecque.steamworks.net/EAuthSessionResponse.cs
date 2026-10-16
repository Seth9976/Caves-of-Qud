using System;

namespace Steamworks
{
	// Token: 0x02000141 RID: 321
	public enum EAuthSessionResponse
	{
		// Token: 0x0400089F RID: 2207
		k_EAuthSessionResponseOK,
		// Token: 0x040008A0 RID: 2208
		k_EAuthSessionResponseUserNotConnectedToSteam,
		// Token: 0x040008A1 RID: 2209
		k_EAuthSessionResponseNoLicenseOrExpired,
		// Token: 0x040008A2 RID: 2210
		k_EAuthSessionResponseVACBanned,
		// Token: 0x040008A3 RID: 2211
		k_EAuthSessionResponseLoggedInElseWhere,
		// Token: 0x040008A4 RID: 2212
		k_EAuthSessionResponseVACCheckTimedOut,
		// Token: 0x040008A5 RID: 2213
		k_EAuthSessionResponseAuthTicketCanceled,
		// Token: 0x040008A6 RID: 2214
		k_EAuthSessionResponseAuthTicketInvalidAlreadyUsed,
		// Token: 0x040008A7 RID: 2215
		k_EAuthSessionResponseAuthTicketInvalid,
		// Token: 0x040008A8 RID: 2216
		k_EAuthSessionResponsePublisherIssuedBan,
		// Token: 0x040008A9 RID: 2217
		k_EAuthSessionResponseAuthTicketNetworkIdentityFailure
	}
}
