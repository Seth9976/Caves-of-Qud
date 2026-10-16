using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000330 RID: 816
	[Serializable]
	public class ValueToDateModel : PlayFabBaseModel
	{
		// Token: 0x04000FCA RID: 4042
		public string Currency;

		// Token: 0x04000FCB RID: 4043
		public uint TotalValue;

		// Token: 0x04000FCC RID: 4044
		public string TotalValueAsDecimal;
	}
}
