using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	public class SubscriptionModel : PlayFabBaseModel
	{
		// Token: 0x040008F6 RID: 2294
		public DateTime Expiration;

		// Token: 0x040008F7 RID: 2295
		public DateTime InitialSubscriptionTime;

		// Token: 0x040008F8 RID: 2296
		public bool IsActive;

		// Token: 0x040008F9 RID: 2297
		public SubscriptionProviderStatus? Status;

		// Token: 0x040008FA RID: 2298
		public string SubscriptionId;

		// Token: 0x040008FB RID: 2299
		public string SubscriptionItemId;

		// Token: 0x040008FC RID: 2300
		public string SubscriptionProvider;
	}
}
