using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000240 RID: 576
	[Serializable]
	public class GetSharedGroupDataResult : PlayFabResultCommon
	{
		// Token: 0x04000C82 RID: 3202
		public Dictionary<string, SharedGroupDataRecord> Data;

		// Token: 0x04000C83 RID: 3203
		public List<string> Members;
	}
}
