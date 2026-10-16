using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000322 RID: 802
	[Serializable]
	public class UserSettings : PlayFabBaseModel
	{
		// Token: 0x04000F9B RID: 3995
		public bool GatherDeviceInfo;

		// Token: 0x04000F9C RID: 3996
		public bool GatherFocusInfo;

		// Token: 0x04000F9D RID: 3997
		public bool NeedsAttribution;
	}
}
