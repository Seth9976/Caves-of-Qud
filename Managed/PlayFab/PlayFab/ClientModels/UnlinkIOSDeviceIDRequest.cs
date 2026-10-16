using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002EE RID: 750
	[Serializable]
	public class UnlinkIOSDeviceIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F1A RID: 3866
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F1B RID: 3867
		public string DeviceId;
	}
}
