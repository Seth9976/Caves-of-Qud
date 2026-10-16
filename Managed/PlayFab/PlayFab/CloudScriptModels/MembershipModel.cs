using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x0200018D RID: 397
	[Serializable]
	public class MembershipModel : PlayFabBaseModel
	{
		// Token: 0x040008B1 RID: 2225
		public bool IsActive;

		// Token: 0x040008B2 RID: 2226
		public DateTime MembershipExpiration;

		// Token: 0x040008B3 RID: 2227
		public string MembershipId;

		// Token: 0x040008B4 RID: 2228
		public DateTime? OverrideExpiration;

		// Token: 0x040008B5 RID: 2229
		public List<SubscriptionModel> Subscriptions;
	}
}
