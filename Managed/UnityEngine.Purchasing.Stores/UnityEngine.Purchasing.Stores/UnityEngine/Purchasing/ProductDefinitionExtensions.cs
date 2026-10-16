using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200008E RID: 142
	internal static class ProductDefinitionExtensions
	{
		// Token: 0x06000334 RID: 820 RVA: 0x0000AC14 File Offset: 0x00008E14
		internal static List<ProductDefinition> DecodeJSON(this List<object> productsList, string storeName)
		{
			List<ProductDefinition> list = new List<ProductDefinition>();
			List<ProductDefinition> list2;
			try
			{
				foreach (object obj in productsList)
				{
					Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
					object obj2;
					dictionary.TryGetValue("id", out obj2);
					object obj3;
					dictionary.TryGetValue("store_ids", out obj3);
					object obj4;
					dictionary.TryGetValue("type", out obj4);
					Dictionary<string, object> dictionary2 = obj3 as Dictionary<string, object>;
					string text = (string)obj2;
					if (dictionary2 != null)
					{
						using (Dictionary<string, object>.Enumerator enumerator2 = dictionary2.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<string, object> keyValuePair = enumerator2.Current;
								string text2 = keyValuePair.Key.ToLower();
								string text3 = (string)keyValuePair.Value;
								if (!string.IsNullOrEmpty(text3) && storeName.ToLower() == text2)
								{
									text = text3;
								}
							}
							goto IL_00E2;
						}
						goto IL_00C3;
					}
					goto IL_00C3;
					IL_00E2:
					ProductType productType = (ProductType)Enum.Parse(typeof(ProductType), (string)obj4);
					ProductDefinition productDefinition = new ProductDefinition((string)obj2, text, productType);
					list.Add(productDefinition);
					continue;
					IL_00C3:
					object obj5;
					dictionary.TryGetValue("storeSpecificId", out obj5);
					string text4 = (string)obj5;
					if (text4 != null)
					{
						text = text4;
						goto IL_00E2;
					}
					goto IL_00E2;
				}
				list2 = list;
			}
			catch
			{
				list2 = null;
			}
			return list2;
		}
	}
}
