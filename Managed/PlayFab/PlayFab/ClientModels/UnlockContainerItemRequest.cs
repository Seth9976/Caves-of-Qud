using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000301 RID: 769
	[Serializable]
	public class UnlockContainerItemRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F2E RID: 3886
		public string CatalogVersion;

		// Token: 0x04000F2F RID: 3887
		public string CharacterId;

		// Token: 0x04000F30 RID: 3888
		public string ContainerItemId;

		// Token: 0x04000F31 RID: 3889
		public Dictionary<string, string> CustomTags;
	}
}
