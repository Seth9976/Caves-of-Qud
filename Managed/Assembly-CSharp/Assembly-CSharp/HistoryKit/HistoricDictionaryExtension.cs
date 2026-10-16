using System;
using System.Collections.Generic;

namespace HistoryKit
{
	// Token: 0x02000369 RID: 873
	public static class HistoricDictionaryExtension
	{
		// Token: 0x06002066 RID: 8294 RVA: 0x0009F21B File Offset: 0x0009D41B
		public static void SetValue<K, V>(this Dictionary<K, V> list, K key, V value)
		{
			if (list.ContainsKey(key))
			{
				list[key] = value;
				return;
			}
			list.Add(key, value);
		}
	}
}
