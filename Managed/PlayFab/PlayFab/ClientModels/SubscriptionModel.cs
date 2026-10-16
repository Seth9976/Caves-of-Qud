using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002D6 RID: 726
	[Serializable]
	public class SubscriptionModel : PlayFabBaseModel
	{
		// Token: 0x04000EC4 RID: 3780
		public DateTime Expiration;

		// Token: 0x04000EC5 RID: 3781
		public DateTime InitialSubscriptionTime;

		// Token: 0x04000EC6 RID: 3782
		public bool IsActive;

		// Token: 0x04000EC7 RID: 3783
		public SubscriptionProviderStatus? Status;

		// Token: 0x04000EC8 RID: 3784
		public string SubscriptionId;

		// Token: 0x04000EC9 RID: 3785
		public string SubscriptionItemId;

		// Token: 0x04000ECA RID: 3786
		public string SubscriptionProvider;
	}
}
