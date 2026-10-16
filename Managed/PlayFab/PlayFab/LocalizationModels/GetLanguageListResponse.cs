using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.LocalizationModels
{
	// Token: 0x02000103 RID: 259
	[Serializable]
	public class GetLanguageListResponse : PlayFabResultCommon
	{
		// Token: 0x040005CE RID: 1486
		public List<string> LanguageList;
	}
}
