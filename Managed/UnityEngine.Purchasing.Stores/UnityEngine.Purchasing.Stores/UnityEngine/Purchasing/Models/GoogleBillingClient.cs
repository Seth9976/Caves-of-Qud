using System;
using System.Collections.Generic;
using System.Linq;
using Uniject;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000AD RID: 173
	internal class GoogleBillingClient : IGoogleBillingClient
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000BB9C File Offset: 0x00009D9C
		private static AndroidJavaClass GetProductParamsClass()
		{
			if (GoogleBillingClient.s_AndroidProductClassName == null)
			{
				GoogleBillingClient.s_AndroidProductClassName = new AndroidJavaClass("com.android.billingclient.api.QueryProductDetailsParams$Product");
			}
			return GoogleBillingClient.s_AndroidProductClassName;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000BBB9 File Offset: 0x00009DB9
		private static AndroidJavaClass GetQueryProductDetailsParamsParamsClass()
		{
			if (GoogleBillingClient.s_AndroidQueryProductDetailsParamsClassName == null)
			{
				GoogleBillingClient.s_AndroidQueryProductDetailsParamsClassName = new AndroidJavaClass("com.android.billingclient.api.QueryProductDetailsParams");
			}
			return GoogleBillingClient.s_AndroidQueryProductDetailsParamsClassName;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000BBD6 File Offset: 0x00009DD6
		private static AndroidJavaClass GetBillingFlowParamClass()
		{
			if (GoogleBillingClient.s_BillingFlowParamsClass == null)
			{
				GoogleBillingClient.s_BillingFlowParamsClass = new AndroidJavaClass("com.android.billingclient.api.BillingFlowParams");
			}
			return GoogleBillingClient.s_BillingFlowParamsClass;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000BBF3 File Offset: 0x00009DF3
		private static AndroidJavaClass GetProductDetailsParamsClass()
		{
			if (GoogleBillingClient.s_ProductDetailsParamsClass == null)
			{
				GoogleBillingClient.s_ProductDetailsParamsClass = new AndroidJavaClass("com.android.billingclient.api.BillingFlowParams$ProductDetailsParams");
			}
			return GoogleBillingClient.s_ProductDetailsParamsClass;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000BC10 File Offset: 0x00009E10
		private static AndroidJavaClass GetSubscriptionUpdateParamClass()
		{
			if (GoogleBillingClient.s_SubscriptionUpdateParamsClass == null)
			{
				GoogleBillingClient.s_SubscriptionUpdateParamsClass = new AndroidJavaClass("com.android.billingclient.api.BillingFlowParams$SubscriptionUpdateParams");
			}
			return GoogleBillingClient.s_SubscriptionUpdateParamsClass;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000BC2D File Offset: 0x00009E2D
		private static AndroidJavaClass GetConsumeParamsClass()
		{
			if (GoogleBillingClient.s_ConsumeParamsClass == null)
			{
				GoogleBillingClient.s_ConsumeParamsClass = new AndroidJavaClass("com.android.billingclient.api.ConsumeParams");
			}
			return GoogleBillingClient.s_ConsumeParamsClass;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000BC4A File Offset: 0x00009E4A
		private static AndroidJavaClass GetAcknowledgePurchaseParamsClass()
		{
			if (GoogleBillingClient.s_AcknowledgePurchaseParamsClass == null)
			{
				GoogleBillingClient.s_AcknowledgePurchaseParamsClass = new AndroidJavaClass("com.android.billingclient.api.AcknowledgePurchaseParams");
			}
			return GoogleBillingClient.s_AcknowledgePurchaseParamsClass;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000BC67 File Offset: 0x00009E67
		private static AndroidJavaClass GetBillingClientClass()
		{
			if (GoogleBillingClient.s_BillingClientClass == null)
			{
				GoogleBillingClient.s_BillingClientClass = new AndroidJavaClass("com.android.billingclient.api.BillingClient");
			}
			return GoogleBillingClient.s_BillingClientClass;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000BC84 File Offset: 0x00009E84
		private static AndroidJavaClass GetPendingPurchasesParamsClass()
		{
			if (GoogleBillingClient.s_PendingPurchasesParamsClass == null)
			{
				GoogleBillingClient.s_PendingPurchasesParamsClass = new AndroidJavaClass("com.android.billingclient.api.PendingPurchasesParams");
			}
			return GoogleBillingClient.s_PendingPurchasesParamsClass;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000BCA4 File Offset: 0x00009EA4
		private static AndroidJavaObject PendingPurchasesParams()
		{
			AndroidJavaObject androidJavaObject2;
			using (AndroidJavaObject androidJavaObject = GoogleBillingClient.GetPendingPurchasesParamsClass().CallStatic<AndroidJavaObject>("newBuilder", Array.Empty<object>()))
			{
				androidJavaObject.Call<AndroidJavaObject>("enableOneTimeProducts", Array.Empty<object>()).Dispose();
				androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>());
			}
			return androidJavaObject2;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000BD0C File Offset: 0x00009F0C
		private static AndroidJavaClass GetQueryPurchasesParamsClass()
		{
			if (GoogleBillingClient.s_QueryPurchasesParamsClass == null)
			{
				GoogleBillingClient.s_QueryPurchasesParamsClass = new AndroidJavaClass("com.android.billingclient.api.QueryPurchasesParams");
			}
			return GoogleBillingClient.s_QueryPurchasesParamsClass;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000BD2C File Offset: 0x00009F2C
		private static AndroidJavaObject QueryPurchasesParams(string productType)
		{
			AndroidJavaObject androidJavaObject2;
			using (AndroidJavaObject androidJavaObject = GoogleBillingClient.GetQueryPurchasesParamsClass().CallStatic<AndroidJavaObject>("newBuilder", Array.Empty<object>()))
			{
				androidJavaObject.Call<AndroidJavaObject>("setProductType", new object[] { productType }).Dispose();
				androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>());
			}
			return androidJavaObject2;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000BD98 File Offset: 0x00009F98
		internal GoogleBillingClient(IGooglePurchaseUpdatedListener googlePurchaseUpdatedListener, IUtil util, ITelemetryDiagnostics telemetryDiagnostics)
		{
			this.m_Util = util;
			this.m_TelemetryDiagnostics = telemetryDiagnostics;
			using (AndroidJavaObject androidJavaObject = GoogleBillingClient.GetBillingClientClass().CallStatic<AndroidJavaObject>("newBuilder", new object[] { UnityActivity.GetCurrentActivity() }))
			{
				androidJavaObject.Call<AndroidJavaObject>("setListener", new object[] { googlePurchaseUpdatedListener }).Dispose();
				androidJavaObject.Call<AndroidJavaObject>("enablePendingPurchases", new object[] { GoogleBillingClient.PendingPurchasesParams() }).Dispose();
				this.m_BillingClient = androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>());
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000BE44 File Offset: 0x0000A044
		public void SetObfuscationAccountId(string obfuscationAccountId)
		{
			this.m_ObfuscatedAccountId = obfuscationAccountId;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000BE4D File Offset: 0x0000A04D
		public void SetObfuscationProfileId(string obfuscationProfileId)
		{
			this.m_ObfuscatedProfileId = obfuscationProfileId;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000BE56 File Offset: 0x0000A056
		public void StartConnection(IBillingClientStateListener billingClientStateListener)
		{
			this.m_BillingClient.Call("startConnection", new object[] { billingClientStateListener });
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000BE72 File Offset: 0x0000A072
		public void EndConnection()
		{
			this.m_BillingClient.Call("endConnection", Array.Empty<object>());
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000BE89 File Offset: 0x0000A089
		public bool IsReady()
		{
			return this.m_BillingClient.Call<bool>("isReady", Array.Empty<object>());
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public GoogleBillingConnectionState GetConnectionState()
		{
			return (GoogleBillingConnectionState)this.m_BillingClient.Call<int>("getConnectionState", Array.Empty<object>());
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public void QueryPurchasesAsync(string productType, Action<IGoogleBillingResult, IEnumerable<AndroidJavaObject>> onQueryPurchasesResponse)
		{
			AndroidJavaObject androidJavaObject = GoogleBillingClient.QueryPurchasesParams(productType);
			GooglePurchasesResponseListener googlePurchasesResponseListener = new GooglePurchasesResponseListener(onQueryPurchasesResponse);
			this.m_BillingClient.Call("queryPurchasesAsync", new object[] { androidJavaObject, googlePurchasesResponseListener });
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		public void QueryProductDetailsAsync(List<string> products, string type, Action<IGoogleBillingResult, List<AndroidJavaObject>> onProductDetailsResponseAction)
		{
			using (AndroidJavaObject androidJavaObject = GoogleBillingClient.QueryProductDetailsParams(products, type))
			{
				ProductDetailsResponseListener productDetailsResponseListener = new ProductDetailsResponseListener(onProductDetailsResponseAction, this.m_Util, this.m_TelemetryDiagnostics);
				this.m_BillingClient.Call("queryProductDetailsAsync", new object[] { androidJavaObject, productDetailsResponseListener });
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000BF58 File Offset: 0x0000A158
		private static AndroidJavaObject QueryProductDetailsParams(List<string> products, string type)
		{
			AndroidJavaObject androidJavaObject3;
			using (AndroidJavaObject androidJavaObject = GoogleBillingClient.GetQueryProductDetailsParamsParamsClass().CallStatic<AndroidJavaObject>("newBuilder", Array.Empty<object>()))
			{
				using (AndroidJavaObject androidJavaObject2 = GoogleBillingClient.QueryProductDetailsParamsProductList(products, type))
				{
					androidJavaObject.Call<AndroidJavaObject>("setProductList", new object[] { androidJavaObject2 }).Dispose();
					androidJavaObject3 = androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>());
				}
			}
			return androidJavaObject3;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		private static AndroidJavaObject QueryProductDetailsParamsProductList(List<string> products, string type)
		{
			return products.Select<string, AndroidJavaObject>((string product) => GoogleBillingClient.QueryProductDetailsParamsProduct(type, product)).ToList<AndroidJavaObject>().ToJava<AndroidJavaObject>();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000C018 File Offset: 0x0000A218
		private static AndroidJavaObject QueryProductDetailsParamsProduct(string type, string product)
		{
			AndroidJavaObject androidJavaObject2;
			using (AndroidJavaObject androidJavaObject = GoogleBillingClient.GetProductParamsClass().CallStatic<AndroidJavaObject>("newBuilder", Array.Empty<object>()))
			{
				androidJavaObject.Call<AndroidJavaObject>("setProductId", new object[] { product }).Dispose();
				androidJavaObject.Call<AndroidJavaObject>("setProductType", new object[] { type }).Dispose();
				androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>());
			}
			return androidJavaObject2;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000C09C File Offset: 0x0000A29C
		public AndroidJavaObject LaunchBillingFlow(AndroidJavaObject productDetails, string oldPurchaseToken, GooglePlayReplacementMode? replacementMode)
		{
			AndroidJavaObject androidJavaObject6;
			using (AndroidJavaObject androidJavaObject = productDetails.Call<AndroidJavaObject>("getSubscriptionOfferDetails", Array.Empty<object>()))
			{
				AndroidJavaObject androidJavaObject2 = ((androidJavaObject != null) ? androidJavaObject.Enumerate().ToList<AndroidJavaObject>().FirstOrDefault<AndroidJavaObject>() : null);
				string text = ((androidJavaObject2 != null) ? androidJavaObject2.Call<string>("getOfferToken", Array.Empty<object>()) : null);
				using (AndroidJavaObject androidJavaObject3 = GoogleBillingClient.GetProductDetailsParamsClass().CallStatic<AndroidJavaObject>("newBuilder", Array.Empty<object>()))
				{
					androidJavaObject3.Call<AndroidJavaObject>("setProductDetails", new object[] { productDetails }).Dispose();
					if (text != null)
					{
						androidJavaObject3.Call<AndroidJavaObject>("setOfferToken", new object[] { text }).Dispose();
					}
					using (AndroidJavaObject androidJavaObject4 = androidJavaObject3.Call<AndroidJavaObject>("build", Array.Empty<object>()))
					{
						AndroidJavaObject androidJavaObject5 = new List<AndroidJavaObject> { androidJavaObject4 }.ToJava<AndroidJavaObject>();
						androidJavaObject6 = this.m_BillingClient.Call<AndroidJavaObject>("launchBillingFlow", new object[]
						{
							UnityActivity.GetCurrentActivity(),
							this.MakeBillingFlowParams(androidJavaObject5, oldPurchaseToken, replacementMode)
						});
					}
				}
			}
			return androidJavaObject6;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		private AndroidJavaObject MakeBillingFlowParams(AndroidJavaObject productDetailsParamsList, string oldPurchaseToken, GooglePlayReplacementMode? replacementMode)
		{
			AndroidJavaObject androidJavaObject = GoogleBillingClient.GetBillingFlowParamClass().CallStatic<AndroidJavaObject>("newBuilder", Array.Empty<object>());
			androidJavaObject = this.SetObfuscatedAccountIdIfNeeded(androidJavaObject);
			androidJavaObject = this.SetObfuscatedProfileIdIfNeeded(androidJavaObject);
			androidJavaObject = androidJavaObject.Call<AndroidJavaObject>("setProductDetailsParamsList", new object[] { productDetailsParamsList });
			if (oldPurchaseToken != null && replacementMode != null)
			{
				AndroidJavaObject androidJavaObject2 = GoogleBillingClient.BuildSubscriptionUpdateParams(oldPurchaseToken, replacementMode.Value);
				androidJavaObject = androidJavaObject.Call<AndroidJavaObject>("setSubscriptionUpdateParams", new object[] { androidJavaObject2 });
			}
			return androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>());
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000C25C File Offset: 0x0000A45C
		private static AndroidJavaObject BuildSubscriptionUpdateParams(string oldPurchaseToken, GooglePlayReplacementMode replacementMode)
		{
			return GoogleBillingClient.GetSubscriptionUpdateParamClass().CallStatic<AndroidJavaObject>("newBuilder", Array.Empty<object>()).Call<AndroidJavaObject>("setSubscriptionReplacementMode", new object[] { (int)replacementMode })
				.Call<AndroidJavaObject>("setOldPurchaseToken", new object[] { oldPurchaseToken })
				.Call<AndroidJavaObject>("build", Array.Empty<object>());
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000C2B9 File Offset: 0x0000A4B9
		private AndroidJavaObject SetObfuscatedProfileIdIfNeeded(AndroidJavaObject billingFlowParams)
		{
			if (this.m_ObfuscatedProfileId != null)
			{
				billingFlowParams = billingFlowParams.Call<AndroidJavaObject>("setObfuscatedProfileId", new object[] { this.m_ObfuscatedProfileId });
			}
			return billingFlowParams;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		private AndroidJavaObject SetObfuscatedAccountIdIfNeeded(AndroidJavaObject billingFlowParams)
		{
			if (this.m_ObfuscatedAccountId != null)
			{
				billingFlowParams = billingFlowParams.Call<AndroidJavaObject>("setObfuscatedAccountId", new object[] { this.m_ObfuscatedAccountId });
			}
			return billingFlowParams;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000C308 File Offset: 0x0000A508
		public void ConsumeAsync(string purchaseToken, Action<IGoogleBillingResult> onConsume)
		{
			using (AndroidJavaObject androidJavaObject = GoogleBillingClient.GetConsumeParamsClass().CallStatic<AndroidJavaObject>("newBuilder", Array.Empty<object>()))
			{
				androidJavaObject.Call<AndroidJavaObject>("setPurchaseToken", new object[] { purchaseToken }).Dispose();
				using (AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>()))
				{
					this.m_BillingClient.Call("consumeAsync", new object[]
					{
						androidJavaObject2,
						new GoogleConsumeResponseListener(onConsume)
					});
				}
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		public void AcknowledgePurchase(string purchaseToken, Action<IGoogleBillingResult> onAcknowledge)
		{
			using (AndroidJavaObject androidJavaObject = GoogleBillingClient.GetAcknowledgePurchaseParamsClass().CallStatic<AndroidJavaObject>("newBuilder", Array.Empty<object>()))
			{
				androidJavaObject.Call<AndroidJavaObject>("setPurchaseToken", new object[] { purchaseToken }).Dispose();
				using (AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>()))
				{
					this.m_BillingClient.Call("acknowledgePurchase", new object[]
					{
						androidJavaObject2,
						new GoogleAcknowledgePurchaseListener(onAcknowledge)
					});
				}
			}
		}

		// Token: 0x040001E6 RID: 486
		private const string k_AndroidProductClassName = "com.android.billingclient.api.QueryProductDetailsParams$Product";

		// Token: 0x040001E7 RID: 487
		private static AndroidJavaClass s_AndroidProductClassName;

		// Token: 0x040001E8 RID: 488
		private const string k_AndroidQueryProductDetailsParamsClassName = "com.android.billingclient.api.QueryProductDetailsParams";

		// Token: 0x040001E9 RID: 489
		private static AndroidJavaClass s_AndroidQueryProductDetailsParamsClassName;

		// Token: 0x040001EA RID: 490
		private const string k_AndroidBillingFlowParamClassName = "com.android.billingclient.api.BillingFlowParams";

		// Token: 0x040001EB RID: 491
		private static AndroidJavaClass s_BillingFlowParamsClass;

		// Token: 0x040001EC RID: 492
		private const string k_AndroidProductDetailsParamsClassName = "com.android.billingclient.api.BillingFlowParams$ProductDetailsParams";

		// Token: 0x040001ED RID: 493
		private static AndroidJavaClass s_ProductDetailsParamsClass;

		// Token: 0x040001EE RID: 494
		private const string k_AndroidSubscriptionUpdateParamClassName = "com.android.billingclient.api.BillingFlowParams$SubscriptionUpdateParams";

		// Token: 0x040001EF RID: 495
		private static AndroidJavaClass s_SubscriptionUpdateParamsClass;

		// Token: 0x040001F0 RID: 496
		private const string k_AndroidConsumeParamsClassName = "com.android.billingclient.api.ConsumeParams";

		// Token: 0x040001F1 RID: 497
		private static AndroidJavaClass s_ConsumeParamsClass;

		// Token: 0x040001F2 RID: 498
		private const string k_AndroidAcknowledgePurchaseParamsClassName = "com.android.billingclient.api.AcknowledgePurchaseParams";

		// Token: 0x040001F3 RID: 499
		private static AndroidJavaClass s_AcknowledgePurchaseParamsClass;

		// Token: 0x040001F4 RID: 500
		private const string k_AndroidBillingClientClassName = "com.android.billingclient.api.BillingClient";

		// Token: 0x040001F5 RID: 501
		private static AndroidJavaClass s_BillingClientClass;

		// Token: 0x040001F6 RID: 502
		private const string k_AndroidPendingPurchasesParamsClassName = "com.android.billingclient.api.PendingPurchasesParams";

		// Token: 0x040001F7 RID: 503
		private static AndroidJavaClass s_PendingPurchasesParamsClass;

		// Token: 0x040001F8 RID: 504
		private const string k_AndroidQueryPurchasesParamsClassName = "com.android.billingclient.api.QueryPurchasesParams";

		// Token: 0x040001F9 RID: 505
		private static AndroidJavaClass s_QueryPurchasesParamsClass;

		// Token: 0x040001FA RID: 506
		private readonly AndroidJavaObject m_BillingClient;

		// Token: 0x040001FB RID: 507
		private string m_ObfuscatedAccountId;

		// Token: 0x040001FC RID: 508
		private string m_ObfuscatedProfileId;

		// Token: 0x040001FD RID: 509
		private readonly IUtil m_Util;

		// Token: 0x040001FE RID: 510
		private readonly ITelemetryDiagnostics m_TelemetryDiagnostics;
	}
}
