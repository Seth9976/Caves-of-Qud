using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000018 RID: 24
	public class QuantileNetworkRequest : LeaderboardNetworkRequest
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002266 File Offset: 0x00000466
		public QuantileNetworkRequest(ILeaderboardId leaderboardId, RequestUserType userType, float quantile, int season)
			: base(leaderboardId, userType, 0, season)
		{
			this.Quantile = quantile;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000227A File Offset: 0x0000047A
		public QuantileNetworkRequest(ILeaderboardId leaderboardId, RequestUserType userType, float quantile)
			: this(leaderboardId, userType, 0f, -1)
		{
		}

		// Token: 0x0400002C RID: 44
		public float Quantile;
	}
}
