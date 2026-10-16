using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000015 RID: 21
	public class NearLeaderboardNetworkRequest : LeaderboardNetworkRequest
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000021B9 File Offset: 0x000003B9
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000021C1 File Offset: 0x000003C1
		public string CentreUser { get; private set; }

		// Token: 0x06000039 RID: 57 RVA: 0x000021CA File Offset: 0x000003CA
		public NearLeaderboardNetworkRequest(ILeaderboardId category, RequestUserType userType, int pageSize, int season, string centreUser)
			: base(category, userType, pageSize, season)
		{
			this.CentreUser = centreUser;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000021DF File Offset: 0x000003DF
		public NearLeaderboardNetworkRequest(ILeaderboardId category, RequestUserType userType, string centreUser, int pageSize)
			: this(category, userType, pageSize, -1, centreUser)
		{
			this.CentreUser = centreUser;
		}
	}
}
