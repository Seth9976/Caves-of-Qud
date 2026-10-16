using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000DD RID: 221
	[Serializable]
	public class MultiplayerEmptyRequest : PlayFabRequestCommon
	{
		// Token: 0x0400054F RID: 1359
		public Dictionary<string, string> CustomTags;
	}
}
