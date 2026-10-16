using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	public class MatchmakingQueueConfig : PlayFabBaseModel
	{
		// Token: 0x04000532 RID: 1330
		public string BuildId;

		// Token: 0x04000533 RID: 1331
		public List<DifferenceRule> DifferenceRules;

		// Token: 0x04000534 RID: 1332
		public List<MatchTotalRule> MatchTotalRules;

		// Token: 0x04000535 RID: 1333
		public uint MaxMatchSize;

		// Token: 0x04000536 RID: 1334
		public uint? MaxTicketSize;

		// Token: 0x04000537 RID: 1335
		public uint MinMatchSize;

		// Token: 0x04000538 RID: 1336
		public string Name;

		// Token: 0x04000539 RID: 1337
		public RegionSelectionRule RegionSelectionRule;

		// Token: 0x0400053A RID: 1338
		public bool ServerAllocationEnabled;

		// Token: 0x0400053B RID: 1339
		public List<SetIntersectionRule> SetIntersectionRules;

		// Token: 0x0400053C RID: 1340
		public StatisticsVisibilityToPlayers StatisticsVisibilityToPlayers;

		// Token: 0x0400053D RID: 1341
		public List<StringEqualityRule> StringEqualityRules;

		// Token: 0x0400053E RID: 1342
		public List<TeamDifferenceRule> TeamDifferenceRules;

		// Token: 0x0400053F RID: 1343
		public List<MatchmakingQueueTeam> Teams;

		// Token: 0x04000540 RID: 1344
		public TeamSizeBalanceRule TeamSizeBalanceRule;

		// Token: 0x04000541 RID: 1345
		public TeamTicketSizeSimilarityRule TeamTicketSizeSimilarityRule;
	}
}
