using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000230 RID: 560
	[Serializable]
	public class GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C68 RID: 3176
		public List<string> NintendoSwitchDeviceIds;
	}
}
