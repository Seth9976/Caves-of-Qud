using System;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000B2 RID: 178
	internal static class GooglePurchaseStateEnum
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000C69E File Offset: 0x0000A89E
		private static AndroidJavaObject GetPurchaseStateJavaObject()
		{
			return new AndroidJavaClass("com.android.billingclient.api.Purchase$PurchaseState");
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		internal static int Purchased()
		{
			if (GooglePurchaseStateEnum.s_Purchased == null)
			{
				using (AndroidJavaObject purchaseStateJavaObject = GooglePurchaseStateEnum.GetPurchaseStateJavaObject())
				{
					GooglePurchaseStateEnum.s_Purchased = new int?(purchaseStateJavaObject.GetStatic<int>("PURCHASED"));
				}
			}
			return GooglePurchaseStateEnum.s_Purchased.Value;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000C708 File Offset: 0x0000A908
		internal static int Pending()
		{
			if (GooglePurchaseStateEnum.s_Pending == null)
			{
				using (AndroidJavaObject purchaseStateJavaObject = GooglePurchaseStateEnum.GetPurchaseStateJavaObject())
				{
					GooglePurchaseStateEnum.s_Pending = new int?(purchaseStateJavaObject.GetStatic<int>("PENDING"));
				}
			}
			return GooglePurchaseStateEnum.s_Pending.Value;
		}

		// Token: 0x0400020E RID: 526
		private const string k_AndroidPurchaseStateClassName = "com.android.billingclient.api.Purchase$PurchaseState";

		// Token: 0x0400020F RID: 527
		private static int? s_Purchased;

		// Token: 0x04000210 RID: 528
		private static int? s_Pending;
	}
}
