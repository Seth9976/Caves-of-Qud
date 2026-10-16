using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace Stores.Util
{
	// Token: 0x02000007 RID: 7
	internal class JsonProductDescriptionsDeserializer
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002220 File Offset: 0x00000420
		public List<ProductDescription> DeserializeProductDescriptions(string json)
		{
			List<object> list = (List<object>)MiniJson.JsonDecode(json);
			List<ProductDescription> list2 = new List<ProductDescription>();
			foreach (object obj in list)
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
				ProductMetadata productMetadata = this.DeserializeMetadata((Dictionary<string, object>)dictionary["metadata"]);
				ProductDescription productDescription = new ProductDescription((string)dictionary["storeSpecificId"], productMetadata, dictionary.TryGetString("receipt"), dictionary.TryGetString("transactionId"), ProductType.NonConsumable);
				list2.Add(productDescription);
			}
			return list2;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022CC File Offset: 0x000004CC
		internal virtual ProductMetadata DeserializeMetadata(Dictionary<string, object> data)
		{
			decimal num;
			try
			{
				num = Convert.ToDecimal(data["localizedPrice"]);
			}
			catch
			{
				num = 0.0m;
			}
			return new ProductMetadata(data.TryGetString("localizedPriceString"), data.TryGetString("localizedTitle"), data.TryGetString("localizedDescription"), data.TryGetString("isoCurrencyCode"), num);
		}
	}
}
