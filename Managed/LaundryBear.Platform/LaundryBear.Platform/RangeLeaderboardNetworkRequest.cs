using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000016 RID: 22
	public class RangeLeaderboardNetworkRequest : LeaderboardNetworkRequest
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000021F4 File Offset: 0x000003F4
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000021FC File Offset: 0x000003FC
		public int Offset { get; private set; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002205 File Offset: 0x00000405
		public RangeLeaderboardNetworkRequest(ILeaderboardId category, RequestUserType userType, int pageSize, int season, int offset)
			: base(category, userType, pageSize, season)
		{
			this.Offset = offset;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000221A File Offset: 0x0000041A
		public RangeLeaderboardNetworkRequest(ILeaderboardId category, RequestUserType userType, int pageSize, int offset)
			: this(category, userType, pageSize, -1, offset)
		{
		}
	}
}
