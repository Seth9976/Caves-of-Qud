using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002D1 RID: 721
	[Serializable]
	public class StatisticUpdate : PlayFabBaseModel
	{
		// Token: 0x04000EB4 RID: 3764
		public string StatisticName;

		// Token: 0x04000EB5 RID: 3765
		public int Value;

		// Token: 0x04000EB6 RID: 3766
		public uint? Version;
	}
}
