using System;

namespace Steamworks
{
	// Token: 0x020000F4 RID: 244
	public enum EUserRestriction
	{
		// Token: 0x04000436 RID: 1078
		k_nUserRestrictionNone,
		// Token: 0x04000437 RID: 1079
		k_nUserRestrictionUnknown,
		// Token: 0x04000438 RID: 1080
		k_nUserRestrictionAnyChat,
		// Token: 0x04000439 RID: 1081
		k_nUserRestrictionVoiceChat = 4,
		// Token: 0x0400043A RID: 1082
		k_nUserRestrictionGroupChat = 8,
		// Token: 0x0400043B RID: 1083
		k_nUserRestrictionRating = 16,
		// Token: 0x0400043C RID: 1084
		k_nUserRestrictionGameInvites = 32,
		// Token: 0x0400043D RID: 1085
		k_nUserRestrictionTrading = 64
	}
}
