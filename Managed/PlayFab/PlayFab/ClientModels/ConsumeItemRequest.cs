using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001D2 RID: 466
	[Serializable]
	public class ConsumeItemRequest : PlayFabRequestCommon
	{
		// Token: 0x040009C3 RID: 2499
		public string CharacterId;

		// Token: 0x040009C4 RID: 2500
		public int ConsumeCount;

		// Token: 0x040009C5 RID: 2501
		public Dictionary<string, string> CustomTags;

		// Token: 0x040009C6 RID: 2502
		public string ItemInstanceId;
	}
}
