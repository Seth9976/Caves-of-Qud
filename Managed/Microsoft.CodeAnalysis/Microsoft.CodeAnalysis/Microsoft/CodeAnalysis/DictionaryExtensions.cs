using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200011A RID: 282
	internal static class DictionaryExtensions
	{
		// Token: 0x060009CA RID: 2506 RVA: 0x0001C000 File Offset: 0x0001A200
		[NullableContext(1)]
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			TValue tvalue;
			if (dictionary.TryGetValue(key, ref tvalue))
			{
				return tvalue;
			}
			dictionary.Add(key, value);
			return value;
		}
	}
}
