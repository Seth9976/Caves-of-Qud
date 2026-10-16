using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	public class DeviceInfoRequest : PlayFabRequestCommon
	{
		// Token: 0x04000B84 RID: 2948
		public Dictionary<string, object> Info;
	}
}
