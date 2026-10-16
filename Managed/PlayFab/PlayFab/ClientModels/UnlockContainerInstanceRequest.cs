using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000300 RID: 768
	[Serializable]
	public class UnlockContainerInstanceRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F29 RID: 3881
		public string CatalogVersion;

		// Token: 0x04000F2A RID: 3882
		public string CharacterId;

		// Token: 0x04000F2B RID: 3883
		public string ContainerItemInstanceId;

		// Token: 0x04000F2C RID: 3884
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F2D RID: 3885
		public string KeyItemInstanceId;
	}
}
