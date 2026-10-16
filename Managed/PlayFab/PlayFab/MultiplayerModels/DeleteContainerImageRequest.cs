using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	public class DeleteContainerImageRequest : PlayFabRequestCommon
	{
		// Token: 0x0400044F RID: 1103
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000450 RID: 1104
		public string ImageName;
	}
}
