using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.Collections
{
	// Token: 0x02000721 RID: 1825
	internal static class DictionaryExtensions
	{
		// Token: 0x060034FF RID: 13567 RVA: 0x000CD548 File Offset: 0x000CB748
		public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue fallbackValue = default(TValue))
		{
			TValue tvalue;
			return dict.TryGetValue(key, out tvalue) ? tvalue : fallbackValue;
		}
	}
}
