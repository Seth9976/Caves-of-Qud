using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002F3 RID: 755
	[Serializable]
	public class UnlinkNintendoSwitchDeviceIdRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F1E RID: 3870
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F1F RID: 3871
		public string NintendoSwitchDeviceId;
	}
}
