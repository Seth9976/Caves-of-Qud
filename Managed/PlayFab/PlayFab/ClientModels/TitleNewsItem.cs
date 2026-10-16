using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002DB RID: 731
	[Serializable]
	public class TitleNewsItem : PlayFabBaseModel
	{
		// Token: 0x04000EDE RID: 3806
		public string Body;

		// Token: 0x04000EDF RID: 3807
		public string NewsId;

		// Token: 0x04000EE0 RID: 3808
		public DateTime Timestamp;

		// Token: 0x04000EE1 RID: 3809
		public string Title;
	}
}
