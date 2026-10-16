using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000259 RID: 601
	[Serializable]
	public class LinkAndroidDeviceIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CC1 RID: 3265
		public string AndroidDevice;

		// Token: 0x04000CC2 RID: 3266
		public string AndroidDeviceId;

		// Token: 0x04000CC3 RID: 3267
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CC4 RID: 3268
		public bool? ForceLink;

		// Token: 0x04000CC5 RID: 3269
		public string OS;
	}
}
