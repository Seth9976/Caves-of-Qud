using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Purchasing.Security;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200007E RID: 126
	public class SubscriptionManager
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x000099BC File Offset: 0x00007BBC
		public static void UpdateSubscription(Product newProduct, Product oldProduct, string developerPayload, Action<Product, string> appleStore, Action<string, string> googleStore)
		{
			if (oldProduct.receipt == null)
			{
				Debug.LogError("The product has not been purchased, a subscription can only be upgrade/downgrade when has already been purchased");
				return;
			}
			Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(oldProduct.receipt);
			if (dictionary == null || !dictionary.ContainsKey("Store") || !dictionary.ContainsKey("Payload"))
			{
				Debug.LogWarning("The product receipt does not contain enough information");
				return;
			}
			string text = (string)dictionary["Store"];
			if ((string)dictionary["Payload"] != null)
			{
				if (text == "GooglePlay")
				{
					SubscriptionManager subscriptionManager = new SubscriptionManager(oldProduct, null);
					SubscriptionInfo subscriptionInfo;
					try
					{
						subscriptionInfo = subscriptionManager.getSubscriptionInfo();
					}
					catch (Exception ex)
					{
						Debug.unityLogger.LogError("Error: the product that will be updated does not have a valid receipt", ex);
						return;
					}
					string storeSpecificId = newProduct.definition.storeSpecificId;
					googleStore(subscriptionInfo.getSubscriptionInfoJsonString(), storeSpecificId);
					return;
				}
				if (!(text == "AppleAppStore") && !(text == "MacAppStore"))
				{
					Debug.LogWarning("This store does not support update subscriptions");
					return;
				}
				appleStore(newProduct, developerPayload);
				return;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00009ACC File Offset: 0x00007CCC
		public static void UpdateSubscriptionInGooglePlayStore(Product oldProduct, Product newProduct, Action<string, string> googlePlayUpdateCallback)
		{
			SubscriptionManager subscriptionManager = new SubscriptionManager(oldProduct, null);
			SubscriptionInfo subscriptionInfo;
			try
			{
				subscriptionInfo = subscriptionManager.getSubscriptionInfo();
			}
			catch (Exception ex)
			{
				Debug.unityLogger.LogError("Error: the product that will be updated does not have a valid receipt", ex);
				return;
			}
			string storeSpecificId = newProduct.definition.storeSpecificId;
			googlePlayUpdateCallback(subscriptionInfo.getSubscriptionInfoJsonString(), storeSpecificId);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009B28 File Offset: 0x00007D28
		public static void UpdateSubscriptionInAppleStore(Product newProduct, string developerPayload, Action<Product, string> appleStoreUpdateCallback)
		{
			appleStoreUpdateCallback(newProduct, developerPayload);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00009B32 File Offset: 0x00007D32
		public SubscriptionManager(Product product, string intro_json)
		{
			this.receipt = product.receipt;
			this.productId = product.definition.storeSpecificId;
			this.intro_json = intro_json;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009B5E File Offset: 0x00007D5E
		public SubscriptionManager(string receipt, string id, string intro_json)
		{
			this.receipt = receipt;
			this.productId = id;
			this.intro_json = intro_json;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009B7C File Offset: 0x00007D7C
		public SubscriptionInfo getSubscriptionInfo()
		{
			if (this.receipt != null)
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(this.receipt);
				object obj;
				bool flag = dictionary.TryGetValue("Payload", out obj);
				object obj2;
				bool flag2 = dictionary.TryGetValue("Store", out obj2);
				if (flag && flag2)
				{
					string text = obj as string;
					string text2 = obj2 as string;
					if (text2 == "GooglePlay")
					{
						return this.getGooglePlayStoreSubInfo(text);
					}
					if (!(text2 == "AppleAppStore") && !(text2 == "MacAppStore"))
					{
						if (!(text2 == "AmazonApps"))
						{
							throw new StoreSubscriptionInfoNotSupportedException("Store not supported: " + text2);
						}
						return this.getAmazonAppStoreSubInfo(this.productId);
					}
					else
					{
						if (this.productId == null)
						{
							throw new NullProductIdException();
						}
						return this.getAppleAppStoreSubInfo(text, this.productId);
					}
				}
			}
			throw new NullReceiptException();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00009C5B File Offset: 0x00007E5B
		private SubscriptionInfo getAmazonAppStoreSubInfo(string productId)
		{
			return new SubscriptionInfo(productId);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009C64 File Offset: 0x00007E64
		private SubscriptionInfo getAppleAppStoreSubInfo(string payload, string productId)
		{
			AppleReceipt appleReceipt = null;
			ILogger unityLogger = Debug.unityLogger;
			try
			{
				appleReceipt = new AppleReceiptParser().Parse(Convert.FromBase64String(payload));
			}
			catch (ArgumentException ex)
			{
				unityLogger.Log("Unable to parse Apple receipt", ex);
			}
			catch (IAPSecurityException ex2)
			{
				unityLogger.Log("Unable to parse Apple receipt", ex2);
			}
			catch (NullReferenceException ex3)
			{
				unityLogger.Log("Unable to parse Apple receipt", ex3);
			}
			List<AppleInAppPurchaseReceipt> list = new List<AppleInAppPurchaseReceipt>();
			if (appleReceipt != null && appleReceipt.inAppPurchaseReceipts != null && appleReceipt.inAppPurchaseReceipts.Length != 0)
			{
				foreach (AppleInAppPurchaseReceipt appleInAppPurchaseReceipt in appleReceipt.inAppPurchaseReceipts)
				{
					if (appleInAppPurchaseReceipt.productID.Equals(productId))
					{
						list.Add(appleInAppPurchaseReceipt);
					}
				}
			}
			if (list.Count != 0)
			{
				return new SubscriptionInfo(this.findMostRecentReceipt(list), this.intro_json);
			}
			return null;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00009D54 File Offset: 0x00007F54
		private AppleInAppPurchaseReceipt findMostRecentReceipt(List<AppleInAppPurchaseReceipt> receipts)
		{
			receipts.Sort((AppleInAppPurchaseReceipt b, AppleInAppPurchaseReceipt a) => a.purchaseDate.CompareTo(b.purchaseDate));
			return receipts[0];
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00009D84 File Offset: 0x00007F84
		private SubscriptionInfo getGooglePlayStoreSubInfo(string payload)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
			object obj9;
			dictionary.TryGetValue("skuDetails", out obj9);
			List<object> list = obj9 as List<object>;
			IEnumerable<string> enumerable;
			if (list == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = list.Select<object, string>((object obj) => obj as string);
			}
			IEnumerable<string> enumerable2 = enumerable;
			bool flag = false;
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)MiniJson.JsonDecode((string)dictionary["json"]);
			object obj2;
			bool flag2 = dictionary2.TryGetValue("autoRenewing", out obj2);
			bool flag3 = false;
			if (flag2)
			{
				flag3 = (bool)obj2;
			}
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			object obj3;
			bool flag4 = dictionary2.TryGetValue("purchaseTime", out obj3);
			long num = 0L;
			if (flag4)
			{
				num = (long)obj3;
			}
			DateTime dateTime2 = dateTime.AddMilliseconds((double)num);
			object obj4;
			bool flag5 = dictionary2.TryGetValue("developerPayload", out obj4);
			bool flag6 = false;
			bool flag7 = false;
			string text = null;
			if (flag5)
			{
				Dictionary<string, object> dictionary3 = (Dictionary<string, object>)MiniJson.JsonDecode((string)obj4);
				object obj5;
				if (dictionary3.TryGetValue("is_free_trial", out obj5))
				{
					flag6 = (bool)obj5;
				}
				object obj6;
				if (dictionary3.TryGetValue("has_introductory_price_trial", out obj6))
				{
					flag7 = (bool)obj6;
				}
				object obj7;
				bool flag8 = dictionary3.TryGetValue("is_updated", out obj7);
				bool flag9 = false;
				if (flag8)
				{
					flag9 = (bool)obj7;
				}
				object obj8;
				if (flag9 && dictionary3.TryGetValue("update_subscription_metadata", out obj8))
				{
					text = (string)obj8;
				}
			}
			return new SubscriptionInfo(enumerable2.First<string>(), flag3, dateTime2, flag6, flag7, flag, text);
		}

		// Token: 0x040001AD RID: 429
		private readonly string receipt;

		// Token: 0x040001AE RID: 430
		private readonly string productId;

		// Token: 0x040001AF RID: 431
		private readonly string intro_json;
	}
}
