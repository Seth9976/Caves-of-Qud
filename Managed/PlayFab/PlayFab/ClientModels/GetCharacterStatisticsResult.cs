using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000200 RID: 512
	[Serializable]
	public class GetCharacterStatisticsResult : PlayFabResultCommon
	{
		// Token: 0x04000BE8 RID: 3048
		public Dictionary<string, int> CharacterStatistics;
	}
}
