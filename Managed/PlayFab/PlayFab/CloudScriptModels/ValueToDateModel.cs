using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	public class ValueToDateModel : PlayFabBaseModel
	{
		// Token: 0x0400090C RID: 2316
		public string Currency;

		// Token: 0x0400090D RID: 2317
		public uint TotalValue;

		// Token: 0x0400090E RID: 2318
		public string TotalValueAsDecimal;
	}
}
