using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200030A RID: 778
	[Serializable]
	public class UpdateSharedGroupDataRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F42 RID: 3906
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F43 RID: 3907
		public Dictionary<string, string> Data;

		// Token: 0x04000F44 RID: 3908
		public List<string> KeysToRemove;

		// Token: 0x04000F45 RID: 3909
		public UserDataPermission? Permission;

		// Token: 0x04000F46 RID: 3910
		public string SharedGroupId;
	}
}
