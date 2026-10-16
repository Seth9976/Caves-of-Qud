using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200023F RID: 575
	[Serializable]
	public class GetSharedGroupDataRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C7F RID: 3199
		public bool? GetMembers;

		// Token: 0x04000C80 RID: 3200
		public List<string> Keys;

		// Token: 0x04000C81 RID: 3201
		public string SharedGroupId;
	}
}
