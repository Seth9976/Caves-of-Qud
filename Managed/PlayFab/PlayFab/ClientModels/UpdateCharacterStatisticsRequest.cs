using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000306 RID: 774
	[Serializable]
	public class UpdateCharacterStatisticsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F3D RID: 3901
		public string CharacterId;

		// Token: 0x04000F3E RID: 3902
		public Dictionary<string, int> CharacterStatistics;

		// Token: 0x04000F3F RID: 3903
		public Dictionary<string, string> CustomTags;
	}
}
