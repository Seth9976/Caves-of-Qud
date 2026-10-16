using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002D2 RID: 722
	[Serializable]
	public class StatisticValue : PlayFabBaseModel
	{
		// Token: 0x04000EB7 RID: 3767
		public string StatisticName;

		// Token: 0x04000EB8 RID: 3768
		public int Value;

		// Token: 0x04000EB9 RID: 3769
		public uint Version;
	}
}
