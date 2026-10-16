using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200028B RID: 651
	[Serializable]
	public class LoginWithPlayFabRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D8E RID: 3470
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D8F RID: 3471
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D90 RID: 3472
		public string Password;

		// Token: 0x04000D91 RID: 3473
		public string TitleId;

		// Token: 0x04000D92 RID: 3474
		public string Username;
	}
}
