using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	public class SetObjectsResponse : PlayFabResultCommon
	{
		// Token: 0x04000752 RID: 1874
		public int ProfileVersion;

		// Token: 0x04000753 RID: 1875
		public List<SetObjectInfo> SetResults;
	}
}
