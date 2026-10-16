using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000255 RID: 597
	[Serializable]
	public class GrantCharacterToUserResult : PlayFabResultCommon
	{
		// Token: 0x04000CA9 RID: 3241
		public string CharacterId;

		// Token: 0x04000CAA RID: 3242
		public string CharacterType;

		// Token: 0x04000CAB RID: 3243
		public bool Result;
	}
}
