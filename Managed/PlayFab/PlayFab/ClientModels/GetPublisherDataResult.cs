using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200023B RID: 571
	[Serializable]
	public class GetPublisherDataResult : PlayFabResultCommon
	{
		// Token: 0x04000C75 RID: 3189
		public Dictionary<string, string> Data;
	}
}
