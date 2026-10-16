using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	public class AssetSummary : PlayFabBaseModel
	{
		// Token: 0x0400033E RID: 830
		public string FileName;

		// Token: 0x0400033F RID: 831
		public Dictionary<string, string> Metadata;
	}
}
