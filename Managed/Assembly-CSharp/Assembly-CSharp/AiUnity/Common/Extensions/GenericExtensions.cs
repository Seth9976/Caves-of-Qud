using System;
using System.Collections.Generic;

namespace AiUnity.Common.Extensions
{
	// Token: 0x020002DD RID: 733
	public static class GenericExtensions
	{
		// Token: 0x06001942 RID: 6466 RVA: 0x00080D44 File Offset: 0x0007EF44
		public static bool AddUnique<T>(this List<T> list, T item)
		{
			if (!list.Exists((T p) => p.Equals(item)))
			{
				list.Add(item);
				return true;
			}
			return false;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00080D84 File Offset: 0x0007EF84
		public static V GetOrAdd<K, V>(this Dictionary<K, V> dictionary, K key, V defaultValue = default(V))
		{
			V v;
			if (dictionary.TryGetValue(key, out v))
			{
				return v;
			}
			dictionary.Add(key, defaultValue);
			return defaultValue;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00080DA7 File Offset: 0x0007EFA7
		public static bool IsNullOrEmpty(this Array array)
		{
			return array == null || array.Length == 0;
		}
	}
}
