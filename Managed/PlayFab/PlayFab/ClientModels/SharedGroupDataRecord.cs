using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002C9 RID: 713
	[Serializable]
	public class SharedGroupDataRecord : PlayFabBaseModel
	{
		// Token: 0x04000E8C RID: 3724
		public DateTime LastUpdated;

		// Token: 0x04000E8D RID: 3725
		public string LastUpdatedBy;

		// Token: 0x04000E8E RID: 3726
		public UserDataPermission? Permission;

		// Token: 0x04000E8F RID: 3727
		public string Value;
	}
}
