using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000178 RID: 376
	[Serializable]
	public class ContactEmailInfoModel : PlayFabBaseModel
	{
		// Token: 0x0400075B RID: 1883
		public string EmailAddress;

		// Token: 0x0400075C RID: 1884
		public string Name;

		// Token: 0x0400075D RID: 1885
		public EmailVerificationStatus? VerificationStatus;
	}
}
