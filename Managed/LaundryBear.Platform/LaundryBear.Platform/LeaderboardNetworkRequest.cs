using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000014 RID: 20
	public abstract class LeaderboardNetworkRequest
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000020FC File Offset: 0x000002FC
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002104 File Offset: 0x00000304
		public ILeaderboardId LeaderboardId { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000210D File Offset: 0x0000030D
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002115 File Offset: 0x00000315
		public RequestUserType UserType { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000211E File Offset: 0x0000031E
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002126 File Offset: 0x00000326
		public DateTime Timestamp { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000212F File Offset: 0x0000032F
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002137 File Offset: 0x00000337
		public NetworkRequestStatus Status { get; internal set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002140 File Offset: 0x00000340
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002148 File Offset: 0x00000348
		public int Season { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002151 File Offset: 0x00000351
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002159 File Offset: 0x00000359
		public int PageSize { get; private set; }

		// Token: 0x06000033 RID: 51 RVA: 0x00002162 File Offset: 0x00000362
		public LeaderboardNetworkRequest(ILeaderboardId category, RequestUserType userType, int pageSize)
			: this(category, userType, pageSize, -1)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000216E File Offset: 0x0000036E
		public LeaderboardNetworkRequest(ILeaderboardId category, RequestUserType userType, int pageSize, int season)
		{
			this.LeaderboardId = category;
			this.UserType = userType;
			this.Season = season;
			this.PageSize = pageSize;
			this.Timestamp = DateTime.UtcNow;
			this.Status = NetworkRequestStatus.Unsent;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000021A5 File Offset: 0x000003A5
		public bool HasSeason()
		{
			return this.Season > -1;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000021B0 File Offset: 0x000003B0
		public void SetStatus(NetworkRequestStatus status)
		{
			this.Status = status;
		}
	}
}
