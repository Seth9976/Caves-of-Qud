using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200030C RID: 780
	[Serializable]
	public class UpdateUserDataRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F47 RID: 3911
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F48 RID: 3912
		public Dictionary<string, string> Data;

		// Token: 0x04000F49 RID: 3913
		public List<string> KeysToRemove;

		// Token: 0x04000F4A RID: 3914
		public UserDataPermission? Permission;
	}
}
