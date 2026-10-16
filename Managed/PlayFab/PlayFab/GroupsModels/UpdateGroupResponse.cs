using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	public class UpdateGroupResponse : PlayFabResultCommon
	{
		// Token: 0x04000681 RID: 1665
		public string OperationReason;

		// Token: 0x04000682 RID: 1666
		public int ProfileVersion;

		// Token: 0x04000683 RID: 1667
		public OperationTypes? SetResult;
	}
}
