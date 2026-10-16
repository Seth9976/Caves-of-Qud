using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000017 RID: 23
	public class UsersLeaderboardNetworkRequest : LeaderboardNetworkRequest
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002228 File Offset: 0x00000428
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002230 File Offset: 0x00000430
		public int Offset { get; private set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002239 File Offset: 0x00000439
		public UsersLeaderboardNetworkRequest(ILeaderboardId leaderboardId, RequestUserType userType, int pageSize, int season, int offset, IUniqueID[] users)
			: base(leaderboardId, userType, pageSize, season)
		{
			this.Offset = offset;
			this.UserIDs = users;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002256 File Offset: 0x00000456
		public UsersLeaderboardNetworkRequest(ILeaderboardId leaderboardId, RequestUserType userType, int pageSize, int offset, IUniqueID[] users)
			: this(leaderboardId, userType, pageSize, -1, offset, users)
		{
		}

		// Token: 0x0400002A RID: 42
		public IUniqueID[] UserIDs;
	}
}
