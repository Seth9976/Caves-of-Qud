using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.MiniJSON;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000049 RID: 73
	internal class UDPBindings : INativeUDPStore, INativeStore
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000058A8 File Offset: 0x00003AA8
		public UDPBindings()
		{
			Type classType = UdpIapBridgeInterface.GetClassType();
			if (classType != null)
			{
				this.m_Bridge = Activator.CreateInstance(classType);
				return;
			}
			Debug.LogError("Failed to access UDP. Please make sure your UDP package is installed and up-to-date");
			throw new NotImplementedException();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000058E6 File Offset: 0x00003AE6
		public void Initialize(Action<bool, string> callback)
		{
			if (this.m_Bridge != null)
			{
				UdpIapBridgeInterface.GetInitMethod().Invoke(this.m_Bridge, new object[] { callback });
				return;
			}
			Debug.LogError("Cannot Initialize UDP store module. Please make sure your UDP package is installed and up-to-date");
			throw new NotImplementedException();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000591B File Offset: 0x00003B1B
		public void Purchase(string productId, Action<bool, string> callback, string developerPayload = null)
		{
			if (this.m_Bridge != null)
			{
				UdpIapBridgeInterface.GetPurchaseMethod().Invoke(this.m_Bridge, new object[] { productId, callback, developerPayload });
				return;
			}
			Debug.LogError("Cannot Purchase via UDP. Please make sure your UDP package is installed and up-to-date");
			throw new NotImplementedException();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005958 File Offset: 0x00003B58
		public void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products, Action<bool, string> callback)
		{
			if (this.m_Bridge == null)
			{
				Debug.LogError("Cannot Retrieve Products from UDP. Please make sure your UDP package is installed and up-to-date");
				throw new NotImplementedException();
			}
			if (this.m_RetrieveProductsCallbackCache != null)
			{
				callback(false, "{ \"error\" : \"already retrieving products\" }");
				return;
			}
			this.m_RetrieveProductsCallbackCache = callback;
			Action<bool, object> action = new Action<bool, object>(this.OnInventoryQueried);
			MethodInfo retrieveProductsMethod = UdpIapBridgeInterface.GetRetrieveProductsMethod();
			List<string> list = new List<string>();
			foreach (ProductDefinition productDefinition in products)
			{
				list.Add(productDefinition.storeSpecificId);
			}
			retrieveProductsMethod.Invoke(this.m_Bridge, new object[]
			{
				new ReadOnlyCollection<string>(list),
				action
			});
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005A18 File Offset: 0x00003C18
		public void FinishTransaction(ProductDefinition productDefinition, string transactionID)
		{
			if (this.m_Bridge != null)
			{
				UdpIapBridgeInterface.GetFinishTransactionMethod().Invoke(this.m_Bridge, new object[] { transactionID });
				return;
			}
			Debug.LogError("Cannot Complete transaction for UDP. Please make sure your UDP package is installed and up-to-date");
			throw new NotImplementedException();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005A50 File Offset: 0x00003C50
		private void OnInventoryQueried(bool success, object payload)
		{
			bool flag = success;
			Type classType = InventoryInterface.GetClassType();
			string text4;
			if (success)
			{
				if (classType != null)
				{
					if (payload != null && payload.GetType() == classType)
					{
						HashSet<ProductDescription> hashSet = new HashSet<ProductDescription>();
						foreach (object obj in ((IEnumerable)InventoryInterface.GetProductListMethod().Invoke(payload, null)).Cast<object>().ToList<object>())
						{
							PropertyInfo priceProp = ProductInfoInterface.GetPriceProp();
							PropertyInfo titleProp = ProductInfoInterface.GetTitleProp();
							PropertyInfo descriptionProp = ProductInfoInterface.GetDescriptionProp();
							PropertyInfo currencyProp = ProductInfoInterface.GetCurrencyProp();
							PropertyInfo priceAmountMicrosProp = ProductInfoInterface.GetPriceAmountMicrosProp();
							ProductMetadata productMetadata = new ProductMetadata((string)priceProp.GetValue(obj, null), (string)titleProp.GetValue(obj, null), (string)descriptionProp.GetValue(obj, null), (string)currencyProp.GetValue(obj, null), Convert.ToDecimal((long)priceAmountMicrosProp.GetValue(obj, null)) / 1000000m);
							string text = (string)ProductInfoInterface.GetProductIdProp().GetValue(obj, null);
							ProductDescription productDescription = new ProductDescription(text, productMetadata);
							if ((bool)InventoryInterface.HasPurchaseMethod().Invoke(payload, new object[] { text }))
							{
								Dictionary<string, string> dictionary = UDPBindings.StringPropertyToDictionary(InventoryInterface.GetPurchaseInfoMethod().Invoke(payload, new object[] { text }));
								string text2 = dictionary["GameOrderId"];
								string text3 = dictionary["ProductId"];
								if (!string.IsNullOrEmpty(text2))
								{
									dictionary["transactionId"] = text2;
								}
								if (!string.IsNullOrEmpty(text3))
								{
									dictionary["storeSpecificId"] = text3;
								}
								productDescription = new ProductDescription(text, productMetadata, dictionary.toJson(), text2);
							}
							hashSet.Add(productDescription);
						}
						text4 = JSONSerializer.SerializeProductDescs(hashSet);
					}
					else
					{
						flag = false;
						text4 = "{ \"error\" : \"Cannot load inventory from UDP. Please make sure your UDP package is installed and up-to-date\" }";
					}
				}
				else
				{
					flag = false;
					text4 = "{ \"error\" : \"Cannot parse inventory type for UDP. Please make sure your UDP package is installed and up-to-date\" }";
				}
			}
			else
			{
				text4 = (string)payload;
			}
			this.m_RetrieveProductsCallbackCache(flag, text4);
			this.m_RetrieveProductsCallbackCache = null;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005C80 File Offset: 0x00003E80
		public void RetrieveProducts(string json)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005C87 File Offset: 0x00003E87
		public void Purchase(string productJSON, string developerPayload)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005C8E File Offset: 0x00003E8E
		public void FinishTransaction(string productJSON, string transactionID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005C98 File Offset: 0x00003E98
		internal static Dictionary<string, string> StringPropertyToDictionary(object info)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (info == null)
			{
				return dictionary;
			}
			foreach (PropertyInfo propertyInfo in info.GetType().GetProperties())
			{
				if (propertyInfo.PropertyType == typeof(string))
				{
					string text = (string)propertyInfo.GetValue(info, null);
					if (!string.IsNullOrEmpty(text))
					{
						dictionary[propertyInfo.Name] = text;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0400009B RID: 155
		private readonly object m_Bridge;

		// Token: 0x0400009C RID: 156
		private Action<bool, string> m_RetrieveProductsCallbackCache;
	}
}
