using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001F9 RID: 505
	[Serializable]
	public class GetCharacterDataRequest : PlayFabRequestCommon
	{
		// Token: 0x04000BD4 RID: 3028
		public string CharacterId;

		// Token: 0x04000BD5 RID: 3029
		public uint? IfChangedFromDataVersion;

		// Token: 0x04000BD6 RID: 3030
		public List<string> Keys;

		// Token: 0x04000BD7 RID: 3031
		public string PlayFabId;
	}
}
