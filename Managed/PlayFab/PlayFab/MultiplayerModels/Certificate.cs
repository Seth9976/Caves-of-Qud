using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	public class Certificate : PlayFabBaseModel
	{
		// Token: 0x040003AA RID: 938
		public string Base64EncodedValue;

		// Token: 0x040003AB RID: 939
		public string Name;

		// Token: 0x040003AC RID: 940
		public string Password;
	}
}
