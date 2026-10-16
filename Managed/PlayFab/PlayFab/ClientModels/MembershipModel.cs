using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000295 RID: 661
	[Serializable]
	public class MembershipModel : PlayFabBaseModel
	{
		// Token: 0x04000DD1 RID: 3537
		public bool IsActive;

		// Token: 0x04000DD2 RID: 3538
		public DateTime MembershipExpiration;

		// Token: 0x04000DD3 RID: 3539
		public string MembershipId;

		// Token: 0x04000DD4 RID: 3540
		public DateTime? OverrideExpiration;

		// Token: 0x04000DD5 RID: 3541
		public List<SubscriptionModel> Subscriptions;
	}
}
