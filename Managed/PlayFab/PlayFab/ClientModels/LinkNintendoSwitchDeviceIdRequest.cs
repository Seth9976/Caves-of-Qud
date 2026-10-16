using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200026C RID: 620
	[Serializable]
	public class LinkNintendoSwitchDeviceIdRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CEC RID: 3308
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CED RID: 3309
		public bool? ForceLink;

		// Token: 0x04000CEE RID: 3310
		public string NintendoSwitchDeviceId;
	}
}
