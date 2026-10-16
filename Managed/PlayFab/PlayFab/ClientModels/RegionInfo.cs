using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002AC RID: 684
	[Serializable]
	public class RegionInfo : PlayFabBaseModel
	{
		// Token: 0x04000E48 RID: 3656
		public bool Available;

		// Token: 0x04000E49 RID: 3657
		public string Name;

		// Token: 0x04000E4A RID: 3658
		public string PingUrl;

		// Token: 0x04000E4B RID: 3659
		public Region? Region;
	}
}
