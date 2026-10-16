using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	public class CoreCapacity : PlayFabBaseModel
	{
		// Token: 0x040003B7 RID: 951
		public int Available;

		// Token: 0x040003B8 RID: 952
		public string Region;

		// Token: 0x040003B9 RID: 953
		public int Total;

		// Token: 0x040003BA RID: 954
		public AzureVmFamily? VmFamily;
	}
}
