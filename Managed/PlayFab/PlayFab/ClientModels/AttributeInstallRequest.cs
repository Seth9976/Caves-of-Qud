using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public class AttributeInstallRequest : PlayFabRequestCommon
	{
		// Token: 0x04000984 RID: 2436
		public string Adid;

		// Token: 0x04000985 RID: 2437
		public string Idfa;
	}
}
