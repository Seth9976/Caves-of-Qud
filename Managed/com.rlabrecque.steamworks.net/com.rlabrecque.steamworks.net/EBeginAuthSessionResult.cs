using System;

namespace Steamworks
{
	// Token: 0x02000140 RID: 320
	public enum EBeginAuthSessionResult
	{
		// Token: 0x04000898 RID: 2200
		k_EBeginAuthSessionResultOK,
		// Token: 0x04000899 RID: 2201
		k_EBeginAuthSessionResultInvalidTicket,
		// Token: 0x0400089A RID: 2202
		k_EBeginAuthSessionResultDuplicateRequest,
		// Token: 0x0400089B RID: 2203
		k_EBeginAuthSessionResultInvalidVersion,
		// Token: 0x0400089C RID: 2204
		k_EBeginAuthSessionResultGameMismatch,
		// Token: 0x0400089D RID: 2205
		k_EBeginAuthSessionResultExpiredTicket
	}
}
