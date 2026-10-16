using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000041 RID: 65
	internal class JSONSerializer
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00004C9B File Offset: 0x00002E9B
		public static string SerializeProductDef(ProductDefinition product)
		{
			return MiniJson.JsonEncode(JSONSerializer.EncodeProductDef(product));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public static string SerializeProductDefs(IEnumerable<ProductDefinition> products)
		{
			List<object> list = new List<object>();
			foreach (ProductDefinition productDefinition in products)
			{
				list.Add(JSONSerializer.EncodeProductDef(productDefinition));
			}
			return MiniJson.JsonEncode(list);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004D04 File Offset: 0x00002F04
		public static string SerializeProductDescs(ProductDescription product)
		{
			return MiniJson.JsonEncode(JSONSerializer.EncodeProductDesc(product));
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004D14 File Offset: 0x00002F14
		public static string SerializeProductDescs(IEnumerable<ProductDescription> products)
		{
			List<object> list = new List<object>();
			foreach (ProductDescription productDescription in products)
			{
				list.Add(JSONSerializer.EncodeProductDesc(productDescription));
			}
			return MiniJson.JsonEncode(list);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004D70 File Offset: 0x00002F70
		public static Dictionary<string, string> DeserializeSubscriptionDescriptions(string json)
		{
			List<object> list = (List<object>)MiniJson.JsonDecode(json);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in list)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj;
				Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
				object obj2;
				if (dictionary2.TryGetValue("metadata", out obj2))
				{
					Dictionary<string, object> dictionary4 = (Dictionary<string, object>)obj2;
					dictionary3["introductoryPrice"] = dictionary4.TryGetString("introductoryPrice");
					dictionary3["introductoryPriceLocale"] = dictionary4.TryGetString("introductoryPriceLocale");
					dictionary3["introductoryPriceNumberOfPeriods"] = dictionary4.TryGetString("introductoryPriceNumberOfPeriods");
					dictionary3["numberOfUnits"] = dictionary4.TryGetString("numberOfUnits");
					dictionary3["unit"] = dictionary4.TryGetString("unit");
					if (!string.IsNullOrEmpty(dictionary3["numberOfUnits"]) && string.IsNullOrEmpty(dictionary3["unit"]))
					{
						dictionary3["unit"] = "0";
					}
				}
				else
				{
					Debug.LogWarning("metadata key not found in subscription description json");
				}
				object obj3;
				if (dictionary2.TryGetValue("storeSpecificId", out obj3))
				{
					string text = (string)obj3;
					dictionary.Add(text, MiniJson.JsonEncode(dictionary3));
				}
				else
				{
					Debug.LogWarning("storeSpecificId key not found in subscription description json");
				}
			}
			return dictionary;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004EE4 File Offset: 0x000030E4
		public static Dictionary<string, string> DeserializeProductDetails(string json)
		{
			List<object> list = (List<object>)MiniJson.JsonDecode(json);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in list)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj;
				Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
				object obj2;
				if (dictionary2.TryGetValue("metadata", out obj2))
				{
					Dictionary<string, object> dictionary4 = (Dictionary<string, object>)obj2;
					dictionary3["subscriptionNumberOfUnits"] = dictionary4.TryGetString("subscriptionNumberOfUnits");
					dictionary3["subscriptionPeriodUnit"] = dictionary4.TryGetString("subscriptionPeriodUnit");
					dictionary3["localizedPrice"] = dictionary4.TryGetString("localizedPrice");
					dictionary3["isoCurrencyCode"] = dictionary4.TryGetString("isoCurrencyCode");
					dictionary3["localizedPriceString"] = dictionary4.TryGetString("localizedPriceString");
					dictionary3["localizedTitle"] = dictionary4.TryGetString("localizedTitle");
					dictionary3["localizedDescription"] = dictionary4.TryGetString("localizedDescription");
					dictionary3["introductoryPrice"] = dictionary4.TryGetString("introductoryPrice");
					dictionary3["introductoryPriceLocale"] = dictionary4.TryGetString("introductoryPriceLocale");
					dictionary3["introductoryPriceNumberOfPeriods"] = dictionary4.TryGetString("introductoryPriceNumberOfPeriods");
					dictionary3["numberOfUnits"] = dictionary4.TryGetString("numberOfUnits");
					dictionary3["unit"] = dictionary4.TryGetString("unit");
					if (!string.IsNullOrEmpty(dictionary3["subscriptionNumberOfUnits"]) && string.IsNullOrEmpty(dictionary3["subscriptionPeriodUnit"]))
					{
						dictionary3["subscriptionPeriodUnit"] = "0";
					}
					if (!string.IsNullOrEmpty(dictionary3["numberOfUnits"]) && string.IsNullOrEmpty(dictionary3["unit"]))
					{
						dictionary3["unit"] = "0";
					}
				}
				else
				{
					Debug.LogWarning("metadata key not found in product details json");
				}
				object obj3;
				if (dictionary2.TryGetValue("storeSpecificId", out obj3))
				{
					string text = (string)obj3;
					dictionary.Add(text, MiniJson.JsonEncode(dictionary3));
				}
				else
				{
					Debug.LogWarning("storeSpecificId key not found in product details json");
				}
			}
			return dictionary;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000512C File Offset: 0x0000332C
		public static PurchaseFailureDescription DeserializeFailureReason(string json)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(json);
			PurchaseFailureReason purchaseFailureReason = PurchaseFailureReason.Unknown;
			object obj;
			if (dictionary.TryGetValue("reason", out obj))
			{
				if (Enum.IsDefined(typeof(PurchaseFailureReason), (string)obj))
				{
					purchaseFailureReason = (PurchaseFailureReason)Enum.Parse(typeof(PurchaseFailureReason), (string)obj);
				}
				object obj2;
				if (dictionary.TryGetValue("productId", out obj2))
				{
					return new PurchaseFailureDescription((string)obj2, purchaseFailureReason, JSONSerializer.BuildPurchaseFailureDescriptionMessage(dictionary));
				}
			}
			else
			{
				Debug.LogWarning("Reason key not found in purchase failure json: " + json);
			}
			return new PurchaseFailureDescription("Unknown ProductID", purchaseFailureReason, JSONSerializer.BuildPurchaseFailureDescriptionMessage(dictionary));
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000051CC File Offset: 0x000033CC
		private static string BuildPurchaseFailureDescriptionMessage(Dictionary<string, object> dic)
		{
			string text = dic.TryGetString("message");
			string text2 = dic.TryGetString("storeSpecificErrorCode");
			if (text == null && text2 == null)
			{
				return null;
			}
			if (text2 != null)
			{
				text2 = " storeSpecificErrorCode: " + text2;
			}
			return text + text2;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005210 File Offset: 0x00003410
		private static Dictionary<string, object> EncodeProductDef(ProductDefinition product)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{ "id", product.id },
				{ "storeSpecificId", product.storeSpecificId },
				{
					"type",
					product.type.ToString()
				}
			};
			bool flag = true;
			PropertyInfo property = typeof(ProductDefinition).GetProperty("enabled");
			if (property != null)
			{
				try
				{
					flag = Convert.ToBoolean(property.GetValue(product, null));
				}
				catch
				{
					flag = true;
				}
			}
			dictionary.Add("enabled", flag);
			List<object> list = new List<object>();
			PropertyInfo property2 = typeof(ProductDefinition).GetProperty("payouts");
			if (property2 != null)
			{
				Array array = property2.GetValue(product, null) as Array;
				if (array != null)
				{
					foreach (object obj in array)
					{
						Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
						Type type = obj.GetType();
						dictionary2["t"] = type.GetField("typeString").GetValue(obj);
						dictionary2["st"] = type.GetField("subtype").GetValue(obj);
						dictionary2["q"] = type.GetField("quantity").GetValue(obj);
						dictionary2["d"] = type.GetField("data").GetValue(obj);
						list.Add(dictionary2);
					}
				}
			}
			dictionary.Add("payouts", list);
			return dictionary;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000053E4 File Offset: 0x000035E4
		private static Dictionary<string, object> EncodeProductDesc(ProductDescription product)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "storeSpecificId", product.storeSpecificId } };
			FieldInfo field = typeof(ProductDescription).GetField("type");
			if (field != null)
			{
				object value = field.GetValue(product);
				dictionary.Add("type", value.ToString());
			}
			dictionary.Add("metadata", JSONSerializer.EncodeProductMeta(product.metadata));
			dictionary.Add("receipt", product.receipt);
			dictionary.Add("transactionId", product.transactionId);
			return dictionary;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005478 File Offset: 0x00003678
		private static Dictionary<string, object> EncodeProductMeta(ProductMetadata product)
		{
			return new Dictionary<string, object>
			{
				{ "localizedPriceString", product.localizedPriceString },
				{ "localizedTitle", product.localizedTitle },
				{ "localizedDescription", product.localizedDescription },
				{ "isoCurrencyCode", product.isoCurrencyCode },
				{
					"localizedPrice",
					Convert.ToDouble(product.localizedPrice)
				}
			};
		}
	}
}
