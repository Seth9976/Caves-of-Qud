using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000017 RID: 23
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GoogleQueryPurchasesService : IGoogleQueryPurchasesService
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002E45 File Offset: 0x00001045
		internal GoogleQueryPurchasesService(IGoogleBillingClient billingClient, IGooglePurchaseBuilder purchaseBuilder)
		{
			this.m_BillingClient = billingClient;
			this.m_PurchaseBuilder = purchaseBuilder;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E5C File Offset: 0x0000105C
		public async Task<List<IGooglePurchase>> QueryPurchases()
		{
			return (await Task.WhenAll<IEnumerable<IGooglePurchase>>(new Task<IEnumerable<IGooglePurchase>>[]
			{
				this.QueryPurchasesWithSkuType(GoogleProductTypeEnum.Sub()),
				this.QueryPurchasesWithSkuType(GoogleProductTypeEnum.InApp())
			})).SelectMany<IEnumerable<IGooglePurchase>, IGooglePurchase>((IEnumerable<IGooglePurchase> result) => result).ToList<IGooglePurchase>();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002EA0 File Offset: 0x000010A0
		private Task<IEnumerable<IGooglePurchase>> QueryPurchasesWithSkuType(string skuType)
		{
			TaskCompletionSource<IEnumerable<IGooglePurchase>> taskCompletion = new TaskCompletionSource<IEnumerable<IGooglePurchase>>();
			this.m_BillingClient.QueryPurchasesAsync(skuType, delegate(IGoogleBillingResult billingResult, IEnumerable<AndroidJavaObject> purchases)
			{
				IEnumerable<IGooglePurchase> enumerable2;
				if (!GoogleQueryPurchasesService.IsResultOk(billingResult))
				{
					IEnumerable<IGooglePurchase> enumerable = Enumerable.Empty<IGooglePurchase>();
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = this.m_PurchaseBuilder.BuildPurchases(purchases);
				}
				IEnumerable<IGooglePurchase> enumerable3 = enumerable2;
				taskCompletion.TrySetResult(enumerable3);
			});
			return taskCompletion.Task;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002EE8 File Offset: 0x000010E8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public IGooglePurchase GetPurchaseByToken(string purchaseToken, string skuType)
		{
			TaskCompletionSource<IGooglePurchase> taskCompletion = new TaskCompletionSource<IGooglePurchase>();
			Func<AndroidJavaObject, bool> <>9__1;
			this.m_BillingClient.QueryPurchasesAsync(skuType, delegate(IGoogleBillingResult billingResult, IEnumerable<AndroidJavaObject> purchases)
			{
				Func<AndroidJavaObject, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (AndroidJavaObject purchase) => purchase != null && purchase.Call<string>("getPurchaseToken", Array.Empty<object>()) == purchaseToken);
				}
				AndroidJavaObject androidJavaObject = purchases.FirstOrDefault<AndroidJavaObject>(func);
				IGooglePurchase googlePurchase = ((androidJavaObject != null && GoogleQueryPurchasesService.IsResultOk(billingResult)) ? this.m_PurchaseBuilder.BuildPurchase(androidJavaObject) : null);
				taskCompletion.TrySetResult(googlePurchase);
			});
			return taskCompletion.Task.Result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F3C File Offset: 0x0000113C
		private static bool IsResultOk(IGoogleBillingResult result)
		{
			return result.responseCode == GoogleBillingResponseCode.Ok;
		}

		// Token: 0x0400002A RID: 42
		private readonly IGoogleBillingClient m_BillingClient;

		// Token: 0x0400002B RID: 43
		private readonly IGooglePurchaseBuilder m_PurchaseBuilder;
	}
}
