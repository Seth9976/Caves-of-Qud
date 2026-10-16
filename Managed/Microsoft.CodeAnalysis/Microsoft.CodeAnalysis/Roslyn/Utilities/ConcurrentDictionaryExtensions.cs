using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x02000034 RID: 52
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ConcurrentDictionaryExtensions
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000492A File Offset: 0x00002B2A
		public static void Add<K, [Nullable(2)] V>(this ConcurrentDictionary<K, V> dict, K key, V value)
		{
			if (!dict.TryAdd(key, value))
			{
				throw new ArgumentException("adding a duplicate", "key");
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004948 File Offset: 0x00002B48
		public static TValue GetOrAdd<TKey, [Nullable(2)] TArg, [Nullable(2)] TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			TValue tvalue;
			if (dictionary.TryGetValue(key, ref tvalue))
			{
				return tvalue;
			}
			Func<TKey, TValue> func;
			TValue orAdd;
			using (PooledDelegates.GetPooledFunction<TKey, TArg, TValue>(valueFactory, factoryArgument, out func))
			{
				orAdd = dictionary.GetOrAdd(key, func);
			}
			return orAdd;
		}
	}
}
