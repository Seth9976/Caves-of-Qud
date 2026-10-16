using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000248 RID: 584
	[Serializable]
	public class GetTitleNewsResult : PlayFabResultCommon
	{
		// Token: 0x04000C90 RID: 3216
		public List<TitleNewsItem> News;
	}
}
