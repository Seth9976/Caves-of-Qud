using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	public class ListGroupBlocksResponse : PlayFabResultCommon
	{
		// Token: 0x0400065B RID: 1627
		public List<GroupBlock> BlockedEntities;
	}
}
