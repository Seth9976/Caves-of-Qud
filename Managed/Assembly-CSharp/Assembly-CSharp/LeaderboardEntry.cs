using System;

// Token: 0x02000065 RID: 101
public class LeaderboardEntry
{
	// Token: 0x0600037D RID: 893 RVA: 0x0001767A File Offset: 0x0001587A
	public static LeaderboardEntry GenerateEmpty()
	{
		return new LeaderboardEntry
		{
			displayName = string.Empty,
			id = string.Empty,
			rank = 0,
			score = 0,
			isCurrentUser = false
		};
	}

	// Token: 0x040002B7 RID: 695
	public string id;

	// Token: 0x040002B8 RID: 696
	public string displayName;

	// Token: 0x040002B9 RID: 697
	public int score;

	// Token: 0x040002BA RID: 698
	public int rank;

	// Token: 0x040002BB RID: 699
	public bool isCurrentUser;
}
