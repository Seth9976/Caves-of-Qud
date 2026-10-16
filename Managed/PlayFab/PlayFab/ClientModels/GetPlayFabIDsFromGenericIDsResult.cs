using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200022B RID: 555
	[Serializable]
	public class GetPlayFabIDsFromGenericIDsResult : PlayFabResultCommon
	{
		// Token: 0x04000C63 RID: 3171
		public List<GenericPlayFabIdPair> Data;
	}
}
