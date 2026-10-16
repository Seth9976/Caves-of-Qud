using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Uniject;
using UnityEngine.Purchasing.Models;
using UnityEngine.Purchasing.Telemetry;
using UnityEngine.Scripting;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001E RID: 30
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class ProductDetailsResponseListener : AndroidJavaProxy
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000035DC File Offset: 0x000017DC
		internal ProductDetailsResponseListener(Action<IGoogleBillingResult, List<AndroidJavaObject>> onProductDetailsResponseAction, IUtil util, ITelemetryDiagnostics telemetryDiagnostics)
			: base("com.android.billingclient.api.ProductDetailsResponseListener")
		{
			this.m_OnProductDetailsResponse = onProductDetailsResponseAction;
			this.m_Util = util;
			this.m_TelemetryDiagnostics = telemetryDiagnostics;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003600 File Offset: 0x00001800
		[Preserve]
		public void onProductDetailsResponse(AndroidJavaObject billingResult, AndroidJavaObject queryProductDetailsResult)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				List<AndroidJavaObject> list = null;
				try
				{
					using (AndroidJavaObject androidJavaObject = queryProductDetailsResult.Call<AndroidJavaObject>("getProductDetailsList", Array.Empty<object>()))
					{
						list = androidJavaObject.Enumerate<AndroidJavaObject>().ToList<AndroidJavaObject>();
						this.m_OnProductDetailsResponse(new GoogleBillingResult(billingResult), list);
					}
				}
				catch (Exception ex)
				{
					this.m_TelemetryDiagnostics.SendDiagnostic("sku_details_response_error", ex);
				}
				if (list != null)
				{
					foreach (AndroidJavaObject androidJavaObject2 in list)
					{
						if (androidJavaObject2 != null)
						{
							androidJavaObject2.Dispose();
						}
					}
				}
				billingResult.Dispose();
				queryProductDetailsResult.Dispose();
			});
		}

		// Token: 0x0400003C RID: 60
		private const string k_AndroidProductDetailsResponseListenerClassName = "com.android.billingclient.api.ProductDetailsResponseListener";

		// Token: 0x0400003D RID: 61
		private readonly Action<IGoogleBillingResult, List<AndroidJavaObject>> m_OnProductDetailsResponse;

		// Token: 0x0400003E RID: 62
		private readonly IUtil m_Util;

		// Token: 0x0400003F RID: 63
		private readonly ITelemetryDiagnostics m_TelemetryDiagnostics;
	}
}
