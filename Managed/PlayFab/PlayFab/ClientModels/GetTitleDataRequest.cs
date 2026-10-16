using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000245 RID: 581
	[Serializable]
	public class GetTitleDataRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C8C RID: 3212
		public List<string> Keys;

		// Token: 0x04000C8D RID: 3213
		public string OverrideLabel;
	}
}
