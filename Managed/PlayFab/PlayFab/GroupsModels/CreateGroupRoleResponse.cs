using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public class CreateGroupRoleResponse : PlayFabResultCommon
	{
		// Token: 0x0400061E RID: 1566
		public int ProfileVersion;

		// Token: 0x0400061F RID: 1567
		public string RoleId;

		// Token: 0x04000620 RID: 1568
		public string RoleName;
	}
}
