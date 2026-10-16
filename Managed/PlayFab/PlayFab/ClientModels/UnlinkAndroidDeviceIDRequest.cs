using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002E1 RID: 737
	[Serializable]
	public class UnlinkAndroidDeviceIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F10 RID: 3856
		public string AndroidDeviceId;

		// Token: 0x04000F11 RID: 3857
		public Dictionary<string, string> CustomTags;
	}
}
