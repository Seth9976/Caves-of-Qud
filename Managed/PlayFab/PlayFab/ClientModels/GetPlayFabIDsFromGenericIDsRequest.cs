using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200022A RID: 554
	[Serializable]
	public class GetPlayFabIDsFromGenericIDsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C62 RID: 3170
		public List<GenericServiceId> GenericIDs;
	}
}
