using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.MiniJSON;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing.Utils
{
	// Token: 0x02000099 RID: 153
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class ProductDetailsConverter : IProductDetailsConverter
	{
		// Token: 0x06000379 RID: 889 RVA: 0x0000B62C File Offset: 0x0000982C
		public List<ProductDescription> ConvertOnQueryProductDetailsResponse(IEnumerable<AndroidJavaObject> productDetails)
		{
			return productDetails.Select<AndroidJavaObject, ProductDescription>(new Func<AndroidJavaObject, ProductDescription>(ProductDetailsConverter.ToProductDescription)).ToList<ProductDescription>();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000B648 File Offset: 0x00009848
		private static ProductDescription ToProductDescription(AndroidJavaObject productDetails)
		{
			ProductDescription productDescription;
			try
			{
				productDescription = ProductDetailsConverter.BuildProductDescription(productDetails);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
			return productDescription;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000B678 File Offset: 0x00009878
		internal static ProductDescription BuildProductDescription(AndroidJavaObject productDetails)
		{
			string text = productDetails.Call<string>("getProductId", Array.Empty<object>());
			ProductDescription productDescription;
			using (AndroidJavaObject androidJavaObject = productDetails.Call<AndroidJavaObject>("getOneTimePurchaseOfferDetails", Array.Empty<object>()))
			{
				using (AndroidJavaObject androidJavaObject2 = productDetails.Call<AndroidJavaObject>("getSubscriptionOfferDetails", Array.Empty<object>()).Enumerate().FirstOrDefault<AndroidJavaObject>())
				{
					using (AndroidJavaObject androidJavaObject3 = ((androidJavaObject2 != null) ? androidJavaObject2.Call<AndroidJavaObject>("getPricingPhases", Array.Empty<object>()) : null))
					{
						using (AndroidJavaObject androidJavaObject4 = ((androidJavaObject3 != null) ? androidJavaObject3.Call<AndroidJavaObject>("getPricingPhaseList", Array.Empty<object>()) : null))
						{
							List<AndroidJavaObject> list = ((androidJavaObject4 != null) ? androidJavaObject4.Enumerate().ToList<AndroidJavaObject>() : null);
							AndroidJavaObject androidJavaObject5 = ((list != null) ? list.LastOrDefault<AndroidJavaObject>() : null);
							AndroidJavaObject androidJavaObject6 = null;
							AndroidJavaObject androidJavaObject7 = null;
							if (list != null)
							{
								for (int i = list.Count - 2; i >= 0; i--)
								{
									AndroidJavaObject androidJavaObject8 = list[i];
									if (androidJavaObject8.Call<long>("getPriceAmountMicros", Array.Empty<object>()) == 0L)
									{
										androidJavaObject6 = androidJavaObject8;
									}
									else
									{
										androidJavaObject7 = androidJavaObject8;
									}
								}
							}
							string text2 = ((androidJavaObject != null) ? androidJavaObject.Call<string>("getFormattedPrice", Array.Empty<object>()) : null) ?? ((androidJavaObject5 != null) ? androidJavaObject5.Call<string>("getFormattedPrice", Array.Empty<object>()) : null);
							decimal num = ((androidJavaObject != null) ? Convert.ToDecimal((androidJavaObject.Call<long>("getPriceAmountMicros", Array.Empty<object>()) > 0L) ? androidJavaObject.Call<long>("getPriceAmountMicros", Array.Empty<object>()) : 0L) : Convert.ToDecimal((androidJavaObject5 != null && androidJavaObject5.Call<long>("getPriceAmountMicros", Array.Empty<object>()) > 0L) ? androidJavaObject5.Call<long>("getPriceAmountMicros", Array.Empty<object>()) : 0L));
							decimal num2 = num / 1000000m;
							string text3 = productDetails.Call<string>("getTitle", Array.Empty<object>());
							string text4 = productDetails.Call<string>("getDescription", Array.Empty<object>());
							string text5 = ((androidJavaObject != null) ? androidJavaObject.Call<string>("getPriceCurrencyCode", Array.Empty<object>()) : null) ?? ((androidJavaObject5 != null) ? androidJavaObject5.Call<string>("getPriceCurrencyCode", Array.Empty<object>()) : null);
							string text6 = ((androidJavaObject5 != null) ? androidJavaObject5.Call<string>("getBillingPeriod", Array.Empty<object>()) : null);
							string text7 = ((androidJavaObject6 != null) ? androidJavaObject6.Call<string>("getBillingPeriod", Array.Empty<object>()) : null);
							string text8 = ((androidJavaObject7 != null) ? androidJavaObject7.Call<string>("getFormattedPrice", Array.Empty<object>()) : null);
							decimal num3 = ((androidJavaObject7 == null) ? 0m : Convert.ToDecimal(androidJavaObject7.Call<long>("getPriceAmountMicros", Array.Empty<object>())));
							string text9 = ((androidJavaObject7 != null) ? androidJavaObject7.Call<string>("getBillingPeriod", Array.Empty<object>()) : null);
							int num4 = ((androidJavaObject7 != null) ? androidJavaObject7.Call<int>("getBillingCycleCount", Array.Empty<object>()) : 0);
							Dictionary<string, object> dictionary = new Dictionary<string, object>();
							dictionary["productId"] = text;
							dictionary["type"] = productDetails.Call<string>("getProductType", Array.Empty<object>());
							dictionary["title"] = text3;
							dictionary["name"] = productDetails.Call<string>("getName", Array.Empty<object>());
							dictionary["description"] = text4;
							dictionary["price"] = text2 ?? "";
							dictionary["price_amount_micros"] = num;
							dictionary["price_currency_code"] = text5 ?? "";
							if (androidJavaObject5 != null)
							{
								dictionary["subscriptionPeriod"] = text6 ?? "";
							}
							if (androidJavaObject6 != null)
							{
								dictionary["freeTrialPeriod"] = text7 ?? "";
							}
							if (androidJavaObject7 != null)
							{
								dictionary["introductoryPrice"] = text8 ?? "";
								dictionary["introductoryPricePeriod"] = text9 ?? "";
								dictionary["introductoryPriceCycles"] = num4;
								dictionary["introductoryPriceAmountMicros"] = num3;
							}
							string text10 = dictionary.toJson();
							GoogleProductMetadata googleProductMetadata = new GoogleProductMetadata(text2, text3, text4, text5, num2)
							{
								originalJson = text10,
								subscriptionPeriod = text6,
								introductoryPrice = text8,
								introductoryPriceCycles = num4,
								introductoryPricePeriod = text9,
								freeTrialPeriod = text7
							};
							productDescription = new ProductDescription(text, googleProductMetadata, "", "");
						}
					}
				}
			}
			return productDescription;
		}
	}
}
