using System;
using System.Collections;
using System.Collections.Generic;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200001A RID: 26
	public interface INetworkService : IService
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71
		string CurrentUserNetworkId { get; }

		// Token: 0x06000048 RID: 72
		bool IsBusy();

		// Token: 0x06000049 RID: 73
		bool IsConnected();

		// Token: 0x0600004A RID: 74
		void CancelRequest();

		// Token: 0x0600004B RID: 75
		IEnumerator SetupNetwork(IUser user, string gameID, Action<LeaderboardResult> onComplete = null);

		// Token: 0x0600004C RID: 76
		IEnumerator SendScoreAsync(ILeaderboardId leaderboardID, int score, int season, Action<LeaderboardResult> onComplete = null);

		// Token: 0x0600004D RID: 77
		void SendLeaderboardRequest<T>(T request, Action<LeaderboardResult, List<LeaderboardEntry>> onComplete) where T : LeaderboardNetworkRequest;

		// Token: 0x0600004E RID: 78
		void SendQuantileRequest(QuantileNetworkRequest request, Action<LeaderboardResult, long> onComplete);

		// Token: 0x0600004F RID: 79
		ILeaderboardId ConstructLeaderboardId(params string[] ids);
	}
}
