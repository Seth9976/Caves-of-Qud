using System;
using PlayFab.SharedModels;

namespace PlayFab.AuthenticationModels
{
	// Token: 0x0200033C RID: 828
	[Serializable]
	public class GetEntityTokenResponse : PlayFabResultCommon
	{
		// Token: 0x0400122B RID: 4651
		public EntityKey Entity;

		// Token: 0x0400122C RID: 4652
		public string EntityToken;

		// Token: 0x0400122D RID: 4653
		public DateTime? TokenExpiration;
	}
}
