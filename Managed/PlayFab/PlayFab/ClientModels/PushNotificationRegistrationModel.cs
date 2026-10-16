using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002A7 RID: 679
	[Serializable]
	public class PushNotificationRegistrationModel : PlayFabBaseModel
	{
		// Token: 0x04000E36 RID: 3638
		public string NotificationEndpointARN;

		// Token: 0x04000E37 RID: 3639
		public PushNotificationPlatform? Platform;
	}
}
