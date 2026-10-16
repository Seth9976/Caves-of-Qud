using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001F5 RID: 501
	[Serializable]
	public class GetAdPlacementsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000BCF RID: 3023
		public string AppId;

		// Token: 0x04000BD0 RID: 3024
		public NameIdentifier Identifier;
	}
}
