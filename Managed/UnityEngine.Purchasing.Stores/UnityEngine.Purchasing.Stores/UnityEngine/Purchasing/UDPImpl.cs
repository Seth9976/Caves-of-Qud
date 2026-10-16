using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.MiniJSON;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200004A RID: 74
	internal class UDPImpl : JSONStore, IUDPExtensions, IStoreExtension
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00005D0C File Offset: 0x00003F0C
		public void SetNativeStore(INativeUDPStore nativeUdpStore)
		{
			this.m_Bindings = nativeUdpStore;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005D15 File Offset: 0x00003F15
		public override void Initialize(IStoreCallback callback)
		{
			this.unity = callback;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005D20 File Offset: 0x00003F20
		public override void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products)
		{
			UDPImpl.<>c__DisplayClass10_0 CS$<>8__locals1 = new UDPImpl.<>c__DisplayClass10_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.products = products;
			if (!this.m_Initialized)
			{
				this.m_Bindings.Initialize(delegate(bool success, string message)
				{
					CS$<>8__locals1.<>4__this.m_LastInitError = "";
					CS$<>8__locals1.<>4__this.m_UserInfo = null;
					if (success)
					{
						if (!string.IsNullOrEmpty(message))
						{
							Dictionary<string, object> dictionary = message.HashtableFromJson();
							if (dictionary.ContainsKey("Channel"))
							{
								Type classType = UserInfoInterface.GetClassType();
								if (classType != null)
								{
									CS$<>8__locals1.<>4__this.m_UserInfo = Activator.CreateInstance(classType);
									UDPImpl.DictionaryToStringProperty(dictionary, CS$<>8__locals1.<>4__this.m_UserInfo);
								}
							}
						}
						CS$<>8__locals1.<>4__this.m_Initialized = true;
						CS$<>8__locals1.<>4__this.m_Bindings.RetrieveProducts(CS$<>8__locals1.products, new Action<bool, string>(base.<RetrieveProducts>g__retrieveCallback|0));
						return;
					}
					CS$<>8__locals1.<>4__this.m_LastInitError = message;
					CS$<>8__locals1.<>4__this.unity.OnSetupFailed(InitializationFailureReason.AppNotKnown, null);
				});
				return;
			}
			this.m_Bindings.RetrieveProducts(CS$<>8__locals1.products, new Action<bool, string>(CS$<>8__locals1.<RetrieveProducts>g__retrieveCallback|0));
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005D80 File Offset: 0x00003F80
		public override void Purchase(ProductDefinition product, string developerPayload)
		{
			this.m_Bindings.Purchase(product.storeSpecificId, delegate(bool success, string message)
			{
				Dictionary<string, object> dictionary = message.HashtableFromJson();
				if (success)
				{
					string @string = dictionary.GetString("GameOrderId", "");
					string string2 = dictionary.GetString("ProductId", "");
					if (!string.IsNullOrEmpty(@string))
					{
						dictionary["transactionId"] = @string;
					}
					if (!string.IsNullOrEmpty(string2))
					{
						dictionary["storeSpecificId"] = string2;
					}
					if (!product.storeSpecificId.Equals(string2))
					{
						this.m_Logger.LogFormat(LogType.Error, "UDPImpl received mismatching product Id for purchase. Expected {0}, received {1}", new object[] { product.storeSpecificId, string2 });
					}
					string text = dictionary.toJson();
					this.unity.OnPurchaseSucceeded(product.storeSpecificId, text, @string);
					return;
				}
				if (dictionary.ContainsKey("errorCode") && Convert.ToInt32(dictionary["errorCode"]) == -303)
				{
					if (this.m_DeferredCallback != null)
					{
						this.OnPurchaseDeferred(product.storeSpecificId);
					}
					return;
				}
				PurchaseFailureReason purchaseFailureReason = (PurchaseFailureReason)Enum.Parse(typeof(PurchaseFailureReason), "Unknown");
				string text2 = purchaseFailureReason.ToString();
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				dictionary2["error"] = text2;
				Dictionary<string, object> dictionary3 = dictionary2;
				if (dictionary.ContainsKey("purchaseInfo"))
				{
					dictionary3["purchaseInfo"] = dictionary["purchaseInfo"];
				}
				dictionary3.toJson();
				PurchaseFailureDescription purchaseFailureDescription = new PurchaseFailureDescription(product.storeSpecificId, purchaseFailureReason, message);
				this.unity.OnPurchaseFailed(purchaseFailureDescription);
			}, developerPayload);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005DC4 File Offset: 0x00003FC4
		private void OnPurchaseDeferred(string productId)
		{
			if (productId == null || productId == "")
			{
				return;
			}
			ProductCollection products = this.unity.products;
			Product product = ((products != null) ? products.WithStoreSpecificID(productId) : null);
			if (product != null)
			{
				this.m_DeferredCallback(product);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005E0A File Offset: 0x0000400A
		public override void FinishTransaction(ProductDefinition product, string transactionId)
		{
			if (product == null || transactionId == null)
			{
				return;
			}
			if (product.type == ProductType.Consumable)
			{
				this.m_Bindings.FinishTransaction(product, transactionId);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005E28 File Offset: 0x00004028
		public string GetLastInitializationError()
		{
			return this.m_LastInitError;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005E30 File Offset: 0x00004030
		public object GetUserInfo()
		{
			return this.m_UserInfo;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005E38 File Offset: 0x00004038
		public void RegisterPurchaseDeferredListener(Action<Product> callback)
		{
			this.m_DeferredCallback = callback;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005E41 File Offset: 0x00004041
		public void EnableDebugLog(bool enable)
		{
			if (StoreServiceInterface.GetClassType() != null)
			{
				StoreServiceInterface.GetEnableDebugLoggingMethod().Invoke(null, new object[] { enable });
				return;
			}
			Debug.LogError("Failed to access StoreService for UDP, cannot enable logging");
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005E78 File Offset: 0x00004078
		private static void DictionaryToStringProperty(Dictionary<string, object> dic, object info)
		{
			foreach (PropertyInfo propertyInfo in info.GetType().GetProperties())
			{
				if (propertyInfo.PropertyType == typeof(string))
				{
					propertyInfo.SetValue(info, dic.GetString(propertyInfo.Name, ""), null);
				}
			}
		}

		// Token: 0x0400009D RID: 157
		private INativeUDPStore m_Bindings;

		// Token: 0x0400009E RID: 158
		private object m_UserInfo;

		// Token: 0x0400009F RID: 159
		private string m_LastInitError;

		// Token: 0x040000A0 RID: 160
		private const string k_Unknown = "Unknown";

		// Token: 0x040000A1 RID: 161
		private bool m_Initialized;

		// Token: 0x040000A2 RID: 162
		private const int PURCHASE_PENDING_CODE = -303;

		// Token: 0x040000A3 RID: 163
		private Action<Product> m_DeferredCallback;

		// Token: 0x040000A4 RID: 164
		private const string k_Errorcode = "errorCode";
	}
}
