using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002D4 RID: 724
	[Serializable]
	public class StoreItem : PlayFabBaseModel
	{
		// Token: 0x04000EBC RID: 3772
		public object CustomData;

		// Token: 0x04000EBD RID: 3773
		public uint? DisplayPosition;

		// Token: 0x04000EBE RID: 3774
		public string ItemId;

		// Token: 0x04000EBF RID: 3775
		public Dictionary<string, uint> RealCurrencyPrices;

		// Token: 0x04000EC0 RID: 3776
		public Dictionary<string, uint> VirtualCurrencyPrices;
	}
}
