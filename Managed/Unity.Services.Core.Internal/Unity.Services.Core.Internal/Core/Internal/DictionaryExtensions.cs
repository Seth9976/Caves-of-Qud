using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000057 RID: 87
	internal static class DictionaryExtensions
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00004028 File Offset: 0x00002228
		public static TDictionary MergeNoOverride<TDictionary, TKey, TValue>(this TDictionary self, [NotNull] IDictionary<TKey, TValue> dictionary) where TDictionary : IDictionary<TKey, TValue>
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				if (!self.ContainsKey(keyValuePair.Key))
				{
					self[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return self;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000409C File Offset: 0x0000229C
		public static TDictionary MergeAllowOverride<TDictionary, TKey, TValue>(this TDictionary self, [NotNull] IDictionary<TKey, TValue> dictionary) where TDictionary : IDictionary<TKey, TValue>
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				self[keyValuePair.Key] = keyValuePair.Value;
			}
			return self;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000040FC File Offset: 0x000022FC
		public static bool ValueEquals<TKey, TValue>(this IDictionary<TKey, TValue> x, IDictionary<TKey, TValue> y)
		{
			return x.ValueEquals(y, EqualityComparer<TValue>.Default);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000410C File Offset: 0x0000230C
		public static bool ValueEquals<TKey, TValue, TComparer>(this IDictionary<TKey, TValue> x, IDictionary<TKey, TValue> y, TComparer valueComparer) where TComparer : IEqualityComparer<TValue>
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null || x.Count != y.Count)
			{
				return false;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in x)
			{
				TValue tvalue;
				if (!y.TryGetValue(keyValuePair.Key, out tvalue) || !valueComparer.Equals(keyValuePair.Value, tvalue))
				{
					return false;
				}
			}
			return true;
		}
	}
}
