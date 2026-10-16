using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000304 RID: 772
	[Serializable]
	public class UpdateCharacterDataRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F37 RID: 3895
		public string CharacterId;

		// Token: 0x04000F38 RID: 3896
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F39 RID: 3897
		public Dictionary<string, string> Data;

		// Token: 0x04000F3A RID: 3898
		public List<string> KeysToRemove;

		// Token: 0x04000F3B RID: 3899
		public UserDataPermission? Permission;
	}
}
