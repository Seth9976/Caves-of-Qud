using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001F1 RID: 497
	[Serializable]
	public class GenericPlayFabIdPair : PlayFabBaseModel
	{
		// Token: 0x04000BC6 RID: 3014
		public GenericServiceId GenericId;

		// Token: 0x04000BC7 RID: 3015
		public string PlayFabId;
	}
}
