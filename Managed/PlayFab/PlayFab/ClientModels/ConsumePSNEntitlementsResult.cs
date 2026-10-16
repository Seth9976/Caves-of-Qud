using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001D5 RID: 469
	[Serializable]
	public class ConsumePSNEntitlementsResult : PlayFabResultCommon
	{
		// Token: 0x040009CC RID: 2508
		public List<ItemInstance> ItemsGranted;
	}
}
