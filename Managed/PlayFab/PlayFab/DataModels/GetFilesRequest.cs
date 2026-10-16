using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000169 RID: 361
	[Serializable]
	public class GetFilesRequest : PlayFabRequestCommon
	{
		// Token: 0x0400072B RID: 1835
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400072C RID: 1836
		public EntityKey Entity;
	}
}
