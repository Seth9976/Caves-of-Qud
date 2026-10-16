using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public class StringEqualityRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x040005A6 RID: 1446
		public List<bool> EnabledOverrides;

		// Token: 0x040005A7 RID: 1447
		public uint SecondsBetweenExpansions;
	}
}
