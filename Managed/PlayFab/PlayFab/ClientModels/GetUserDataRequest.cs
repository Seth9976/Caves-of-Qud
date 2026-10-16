using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200024D RID: 589
	[Serializable]
	public class GetUserDataRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C97 RID: 3223
		public uint? IfChangedFromDataVersion;

		// Token: 0x04000C98 RID: 3224
		public List<string> Keys;

		// Token: 0x04000C99 RID: 3225
		public string PlayFabId;
	}
}
