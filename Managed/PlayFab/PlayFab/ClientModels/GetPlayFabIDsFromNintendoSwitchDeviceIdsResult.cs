using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000231 RID: 561
	[Serializable]
	public class GetPlayFabIDsFromNintendoSwitchDeviceIdsResult : PlayFabResultCommon
	{
		// Token: 0x04000C69 RID: 3177
		public List<NintendoSwitchPlayFabIdPair> Data;
	}
}
