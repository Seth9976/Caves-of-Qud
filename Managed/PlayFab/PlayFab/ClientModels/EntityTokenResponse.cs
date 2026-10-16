using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001E6 RID: 486
	[Serializable]
	public class EntityTokenResponse : PlayFabBaseModel
	{
		// Token: 0x04000B8B RID: 2955
		public EntityKey Entity;

		// Token: 0x04000B8C RID: 2956
		public string EntityToken;

		// Token: 0x04000B8D RID: 2957
		public DateTime? TokenExpiration;
	}
}
