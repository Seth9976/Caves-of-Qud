using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002AD RID: 685
	[Serializable]
	public class RegisterForIOSPushNotificationRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E4C RID: 3660
		public string ConfirmationMessage;

		// Token: 0x04000E4D RID: 3661
		public string DeviceToken;

		// Token: 0x04000E4E RID: 3662
		public bool? SendPushNotificationConfirmation;
	}
}
