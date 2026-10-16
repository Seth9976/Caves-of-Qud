using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	public class ListGroupApplicationsResponse : PlayFabResultCommon
	{
		// Token: 0x04000658 RID: 1624
		public List<GroupApplication> Applications;
	}
}
