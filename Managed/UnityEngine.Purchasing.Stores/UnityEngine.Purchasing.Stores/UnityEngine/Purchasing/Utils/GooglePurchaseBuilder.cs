using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing.Utils
{
	// Token: 0x02000097 RID: 151
	internal class GooglePurchaseBuilder : IGooglePurchaseBuilder
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000B4B5 File Offset: 0x000096B5
		public GooglePurchaseBuilder(IGoogleCachedQueryProductDetailsService cachedQueryProductDetailsService, ILogger logger)
		{
			this.m_CachedQueryProductDetailsService = cachedQueryProductDetailsService;
			this.m_Logger = logger;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000B4CB File Offset: 0x000096CB
		public IEnumerable<IGooglePurchase> BuildPurchases(IEnumerable<AndroidJavaObject> purchases)
		{
			return purchases.Select<AndroidJavaObject, IGooglePurchase>(new Func<AndroidJavaObject, IGooglePurchase>(this.BuildPurchase)).IgnoreExceptions<IGooglePurchase, ArgumentException>(new Action<ArgumentException>(this.LogWarningForException)).ToList<IGooglePurchase>();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000B4F6 File Offset: 0x000096F6
		private void LogWarningForException(Exception exception)
		{
			this.m_Logger.LogIAPWarning(exception.Message);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000B50C File Offset: 0x0000970C
		public IGooglePurchase BuildPurchase(AndroidJavaObject purchase)
		{
			IEnumerable<AndroidJavaObject> cachedQueriedProducts = this.m_CachedQueryProductDetailsService.GetCachedQueriedProducts();
			IGooglePurchase googlePurchase;
			using (AndroidJavaObject androidJavaObject = purchase.Call<AndroidJavaObject>("getProducts", Array.Empty<object>()))
			{
				IEnumerable<string> enumerable = androidJavaObject.Enumerate<string>();
				try
				{
					IEnumerable<AndroidJavaObject> enumerable2 = GooglePurchaseBuilder.TryFindAllProductDetails(enumerable, cachedQueriedProducts);
					googlePurchase = new GooglePurchase(purchase, enumerable2);
				}
				catch (InvalidOperationException)
				{
					string text = purchase.Call<string>("getOrderId", Array.Empty<object>());
					string text2 = purchase.Call<string>("getPurchaseToken", Array.Empty<object>());
					throw new ArgumentException(string.Concat(new string[] { "Unable to process purchase with order id: ", text, " and purchase token: ", text2, " because the product details associated with the purchased products were not found." }));
				}
			}
			return googlePurchase;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000B5D0 File Offset: 0x000097D0
		private static IEnumerable<AndroidJavaObject> TryFindAllProductDetails(IEnumerable<string> skus, IEnumerable<AndroidJavaObject> productDetails)
		{
			return skus.Select<string, AndroidJavaObject>((string sku) => productDetails.First<AndroidJavaObject>((AndroidJavaObject productDetail) => sku == productDetail.Call<string>("getProductId", Array.Empty<object>())));
		}

		// Token: 0x040001E4 RID: 484
		private readonly IGoogleCachedQueryProductDetailsService m_CachedQueryProductDetailsService;

		// Token: 0x040001E5 RID: 485
		private readonly ILogger m_Logger;
	}
}
