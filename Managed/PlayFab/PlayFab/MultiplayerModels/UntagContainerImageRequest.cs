using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	public class UntagContainerImageRequest : PlayFabRequestCommon
	{
		// Token: 0x040005BB RID: 1467
		public Dictionary<string, string> CustomTags;

		// Token: 0x040005BC RID: 1468
		public string ImageName;

		// Token: 0x040005BD RID: 1469
		public string Tag;
	}
}
