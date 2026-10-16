using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001C7 RID: 455
	[Serializable]
	public class CatalogItem : PlayFabBaseModel
	{
		// Token: 0x04000990 RID: 2448
		public CatalogItemBundleInfo Bundle;

		// Token: 0x04000991 RID: 2449
		public bool CanBecomeCharacter;

		// Token: 0x04000992 RID: 2450
		public string CatalogVersion;

		// Token: 0x04000993 RID: 2451
		public CatalogItemConsumableInfo Consumable;

		// Token: 0x04000994 RID: 2452
		public CatalogItemContainerInfo Container;

		// Token: 0x04000995 RID: 2453
		public string CustomData;

		// Token: 0x04000996 RID: 2454
		public string Description;

		// Token: 0x04000997 RID: 2455
		public string DisplayName;

		// Token: 0x04000998 RID: 2456
		public int InitialLimitedEditionCount;

		// Token: 0x04000999 RID: 2457
		public bool IsLimitedEdition;

		// Token: 0x0400099A RID: 2458
		public bool IsStackable;

		// Token: 0x0400099B RID: 2459
		public bool IsTradable;

		// Token: 0x0400099C RID: 2460
		public string ItemClass;

		// Token: 0x0400099D RID: 2461
		public string ItemId;

		// Token: 0x0400099E RID: 2462
		public string ItemImageUrl;

		// Token: 0x0400099F RID: 2463
		public Dictionary<string, uint> RealCurrencyPrices;

		// Token: 0x040009A0 RID: 2464
		public List<string> Tags;

		// Token: 0x040009A1 RID: 2465
		public Dictionary<string, uint> VirtualCurrencyPrices;
	}
}
