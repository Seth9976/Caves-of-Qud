using System;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000AF RID: 175
	internal static class GoogleBillingStrings
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000C497 File Offset: 0x0000A697
		internal static string getWarningMessageMoreThanOneSkuFound(string sku)
		{
			return "More than one SKU found when purchasing SKU " + sku + ". Please verify your Google Play Store in-app product settings.";
		}

		// Token: 0x04000201 RID: 513
		internal const string errorPurchaseStateUnspecified = "PurchaseState is UNSPECIFIED_STATE, no more details exists";

		// Token: 0x04000202 RID: 514
		internal const string errorUserCancelled = "User cancelled purchase";

		// Token: 0x04000203 RID: 515
		internal const string errorItemAlreadyOwned = "Item already owned";
	}
}
