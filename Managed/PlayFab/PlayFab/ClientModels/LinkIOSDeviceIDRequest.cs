using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000267 RID: 615
	[Serializable]
	public class LinkIOSDeviceIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CE0 RID: 3296
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CE1 RID: 3297
		public string DeviceId;

		// Token: 0x04000CE2 RID: 3298
		public string DeviceModel;

		// Token: 0x04000CE3 RID: 3299
		public bool? ForceLink;

		// Token: 0x04000CE4 RID: 3300
		public string OS;
	}
}
