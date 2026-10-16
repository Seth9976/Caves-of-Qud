using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001D3 RID: 467
	[Serializable]
	public class ConsumeItemResult : PlayFabResultCommon
	{
		// Token: 0x040009C7 RID: 2503
		public string ItemInstanceId;

		// Token: 0x040009C8 RID: 2504
		public int RemainingUses;
	}
}
