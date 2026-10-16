using System;
using PlayFab.SharedModels;

namespace PlayFab.AuthenticationModels
{
	// Token: 0x0200033F RID: 831
	[Serializable]
	public class ValidateEntityTokenResponse : PlayFabResultCommon
	{
		// Token: 0x04001246 RID: 4678
		public EntityKey Entity;

		// Token: 0x04001247 RID: 4679
		public LoginIdentityProvider? IdentityProvider;

		// Token: 0x04001248 RID: 4680
		public EntityLineage Lineage;
	}
}
