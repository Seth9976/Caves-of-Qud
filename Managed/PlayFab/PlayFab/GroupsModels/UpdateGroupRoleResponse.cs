using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	public class UpdateGroupRoleResponse : PlayFabResultCommon
	{
		// Token: 0x04000689 RID: 1673
		public string OperationReason;

		// Token: 0x0400068A RID: 1674
		public int ProfileVersion;

		// Token: 0x0400068B RID: 1675
		public OperationTypes? SetResult;
	}
}
