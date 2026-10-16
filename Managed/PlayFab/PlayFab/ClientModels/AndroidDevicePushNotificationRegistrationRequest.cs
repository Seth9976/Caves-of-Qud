using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public class AndroidDevicePushNotificationRegistrationRequest : PlayFabRequestCommon
	{
		// Token: 0x04000981 RID: 2433
		public string ConfirmationMessage;

		// Token: 0x04000982 RID: 2434
		public string DeviceToken;

		// Token: 0x04000983 RID: 2435
		public bool? SendPushNotificationConfirmation;
	}
}
