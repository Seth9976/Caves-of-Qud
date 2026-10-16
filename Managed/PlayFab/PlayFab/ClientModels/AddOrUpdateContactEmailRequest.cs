using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	public class AddOrUpdateContactEmailRequest : PlayFabRequestCommon
	{
		// Token: 0x04000966 RID: 2406
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000967 RID: 2407
		public string EmailAddress;
	}
}
