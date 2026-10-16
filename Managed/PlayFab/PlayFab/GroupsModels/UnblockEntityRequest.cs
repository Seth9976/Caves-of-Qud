using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	public class UnblockEntityRequest : PlayFabRequestCommon
	{
		// Token: 0x04000678 RID: 1656
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000679 RID: 1657
		public EntityKey Entity;

		// Token: 0x0400067A RID: 1658
		public EntityKey Group;
	}
}
