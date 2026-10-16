using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000256 RID: 598
	[Serializable]
	public class ItemInstance : PlayFabBaseModel
	{
		// Token: 0x04000CAC RID: 3244
		public string Annotation;

		// Token: 0x04000CAD RID: 3245
		public List<string> BundleContents;

		// Token: 0x04000CAE RID: 3246
		public string BundleParent;

		// Token: 0x04000CAF RID: 3247
		public string CatalogVersion;

		// Token: 0x04000CB0 RID: 3248
		public Dictionary<string, string> CustomData;

		// Token: 0x04000CB1 RID: 3249
		public string DisplayName;

		// Token: 0x04000CB2 RID: 3250
		public DateTime? Expiration;

		// Token: 0x04000CB3 RID: 3251
		public string ItemClass;

		// Token: 0x04000CB4 RID: 3252
		public string ItemId;

		// Token: 0x04000CB5 RID: 3253
		public string ItemInstanceId;

		// Token: 0x04000CB6 RID: 3254
		public DateTime? PurchaseDate;

		// Token: 0x04000CB7 RID: 3255
		public int? RemainingUses;

		// Token: 0x04000CB8 RID: 3256
		public string UnitCurrency;

		// Token: 0x04000CB9 RID: 3257
		public uint UnitPrice;

		// Token: 0x04000CBA RID: 3258
		public int? UsesIncrementedBy;
	}
}
