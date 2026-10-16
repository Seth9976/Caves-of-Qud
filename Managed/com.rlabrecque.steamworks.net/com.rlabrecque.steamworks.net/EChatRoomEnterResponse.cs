using System;

namespace Steamworks
{
	// Token: 0x02000145 RID: 325
	public enum EChatRoomEnterResponse
	{
		// Token: 0x040008C9 RID: 2249
		k_EChatRoomEnterResponseSuccess = 1,
		// Token: 0x040008CA RID: 2250
		k_EChatRoomEnterResponseDoesntExist,
		// Token: 0x040008CB RID: 2251
		k_EChatRoomEnterResponseNotAllowed,
		// Token: 0x040008CC RID: 2252
		k_EChatRoomEnterResponseFull,
		// Token: 0x040008CD RID: 2253
		k_EChatRoomEnterResponseError,
		// Token: 0x040008CE RID: 2254
		k_EChatRoomEnterResponseBanned,
		// Token: 0x040008CF RID: 2255
		k_EChatRoomEnterResponseLimited,
		// Token: 0x040008D0 RID: 2256
		k_EChatRoomEnterResponseClanDisabled,
		// Token: 0x040008D1 RID: 2257
		k_EChatRoomEnterResponseCommunityBan,
		// Token: 0x040008D2 RID: 2258
		k_EChatRoomEnterResponseMemberBlockedYou,
		// Token: 0x040008D3 RID: 2259
		k_EChatRoomEnterResponseYouBlockedMember,
		// Token: 0x040008D4 RID: 2260
		k_EChatRoomEnterResponseRatelimitExceeded = 15
	}
}
