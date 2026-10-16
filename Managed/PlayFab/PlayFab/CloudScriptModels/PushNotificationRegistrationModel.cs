using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000196 RID: 406
	[Serializable]
	public class PushNotificationRegistrationModel : PlayFabBaseModel
	{
		// Token: 0x040008E4 RID: 2276
		public string NotificationEndpointARN;

		// Token: 0x040008E5 RID: 2277
		public PushNotificationPlatform? Platform;
	}
}
