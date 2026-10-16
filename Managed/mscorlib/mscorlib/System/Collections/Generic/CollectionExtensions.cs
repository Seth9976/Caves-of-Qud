using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AAF RID: 2735
	public static class CollectionExtensions
	{
		// Token: 0x060061D8 RID: 25048 RVA: 0x001470CC File Offset: 0x001452CC
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		// Token: 0x060061D9 RID: 25049 RVA: 0x001470EC File Offset: 0x001452EC
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				return defaultValue;
			}
			return tvalue;
		}

		// Token: 0x060061DA RID: 25050 RVA: 0x00147115 File Offset: 0x00145315
		public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, value);
				return true;
			}
			return false;
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x00147139 File Offset: 0x00145339
		public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (dictionary.TryGetValue(key, out value))
			{
				dictionary.Remove(key);
				return true;
			}
			value = default(TValue);
			return false;
		}
	}
}
