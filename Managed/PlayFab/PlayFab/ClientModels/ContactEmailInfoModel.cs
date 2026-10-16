using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001D8 RID: 472
	[Serializable]
	public class ContactEmailInfoModel : PlayFabBaseModel
	{
		// Token: 0x040009D1 RID: 2513
		public string EmailAddress;

		// Token: 0x040009D2 RID: 2514
		public string Name;

		// Token: 0x040009D3 RID: 2515
		public EmailVerificationStatus? VerificationStatus;
	}
}
