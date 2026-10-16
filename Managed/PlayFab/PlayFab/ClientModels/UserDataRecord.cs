using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000315 RID: 789
	[Serializable]
	public class UserDataRecord : PlayFabBaseModel
	{
		// Token: 0x04000F6B RID: 3947
		public DateTime LastUpdated;

		// Token: 0x04000F6C RID: 3948
		public UserDataPermission? Permission;

		// Token: 0x04000F6D RID: 3949
		public string Value;
	}
}
