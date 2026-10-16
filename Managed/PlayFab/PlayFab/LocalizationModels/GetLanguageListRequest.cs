using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.LocalizationModels
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	public class GetLanguageListRequest : PlayFabRequestCommon
	{
		// Token: 0x040005CD RID: 1485
		public Dictionary<string, string> CustomTags;
	}
}
