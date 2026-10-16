using System;
using Steamworks;

// Token: 0x02000063 RID: 99
public class LeaderboardScores
{
	// Token: 0x06000360 RID: 864 RVA: 0x000169F9 File Offset: 0x00014BF9
	public LeaderboardScores(LeaderboardScoresDownloaded_t result)
	{
		this.m_result = result;
	}

	// Token: 0x06000361 RID: 865 RVA: 0x00016A08 File Offset: 0x00014C08
	public LeaderboardScores.Result GetResult()
	{
		return LeaderboardScores.Result.Success;
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00016A0B File Offset: 0x00014C0B
	public int GetEntryCount()
	{
		return this.m_result.m_cEntryCount;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00016A18 File Offset: 0x00014C18
	public string GetLeaderboardID()
	{
		return this.m_result.m_hSteamLeaderboard.ToString();
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00016A30 File Offset: 0x00014C30
	public LeaderboardEntry GetLeaderboardEntry(int index)
	{
		if (index < 0 || index >= this.m_result.m_cEntryCount)
		{
			return null;
		}
		LeaderboardEntry_t leaderboardEntry_t = default(LeaderboardEntry_t);
		if (!SteamUserStats.GetDownloadedLeaderboardEntry(this.m_result.m_hSteamLeaderboardEntries, index, out leaderboardEntry_t, null, 0))
		{
			return null;
		}
		SteamFriends.RequestUserInformation(leaderboardEntry_t.m_steamIDUser, true);
		return new LeaderboardEntry
		{
			id = leaderboardEntry_t.m_steamIDUser.m_SteamID.ToString(),
			displayName = SteamFriends.GetFriendPersonaName(leaderboardEntry_t.m_steamIDUser),
			score = leaderboardEntry_t.m_nScore,
			rank = leaderboardEntry_t.m_nGlobalRank,
			isCurrentUser = (leaderboardEntry_t.m_steamIDUser == SteamUser.GetSteamID())
		};
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00016ADC File Offset: 0x00014CDC
	public static LeaderboardScores CreateEmptyLeaderboardScores()
	{
		return new LeaderboardScores(default(LeaderboardScoresDownloaded_t));
	}

	// Token: 0x06000366 RID: 870 RVA: 0x00016AF7 File Offset: 0x00014CF7
	public static LeaderboardScores CreateCancelledLeaderboardScores()
	{
		return null;
	}

	// Token: 0x040002AA RID: 682
	private LeaderboardScoresDownloaded_t m_result;

	// Token: 0x0200157A RID: 5498
	public enum Result
	{
		// Token: 0x04005673 RID: 22131
		Success,
		// Token: 0x04005674 RID: 22132
		Failure,
		// Token: 0x04005675 RID: 22133
		Cancelled
	}
}
