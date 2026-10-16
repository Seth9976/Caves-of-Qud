using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002B6 RID: 694
	[Serializable]
	public class RemoveGenericIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E68 RID: 3688
		public GenericServiceId GenericId;
	}
}
