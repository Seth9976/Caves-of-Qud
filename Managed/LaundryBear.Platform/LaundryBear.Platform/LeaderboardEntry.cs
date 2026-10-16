using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000019 RID: 25
	public class LeaderboardEntry
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000228A File Offset: 0x0000048A
		public LeaderboardEntry()
		{
			this.Name = "";
			this.Score = int.MaxValue;
			this.IsPlayer = true;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000022AF File Offset: 0x000004AF
		public LeaderboardEntry(string id, string name, int score, int rank, bool isPlayer)
		{
			this.Id = id;
			this.Name = name;
			this.Score = score;
			this.Rank = rank;
			this.IsPlayer = isPlayer;
		}

		// Token: 0x0400002D RID: 45
		public readonly string Id;

		// Token: 0x0400002E RID: 46
		public readonly int Rank;

		// Token: 0x0400002F RID: 47
		public readonly string Name;

		// Token: 0x04000030 RID: 48
		public readonly int Score;

		// Token: 0x04000031 RID: 49
		public readonly bool IsPlayer;
	}
}
