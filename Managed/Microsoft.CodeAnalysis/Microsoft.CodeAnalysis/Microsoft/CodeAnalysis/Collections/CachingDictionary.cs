using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x0200049B RID: 1179
	[NullableContext(1)]
	[Nullable(0)]
	internal class CachingDictionary<TKey, [Nullable(2)] TElement>
	{
		// Token: 0x06002E90 RID: 11920 RVA: 0x000834FE File Offset: 0x000816FE
		public CachingDictionary([Nullable(new byte[] { 1, 1, 0, 1 })] Func<TKey, ImmutableArray<TElement>> getElementsOfKey, Func<IEqualityComparer<TKey>, HashSet<TKey>> getKeys, IEqualityComparer<TKey> comparer)
		{
			this._getElementsOfKey = getElementsOfKey;
			this._getKeys = getKeys;
			this._comparer = comparer;
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x0008351C File Offset: 0x0008171C
		public bool Contains(TKey key)
		{
			return this[key].Length != 0;
		}

		// Token: 0x17000D77 RID: 3447
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TElement> this[TKey key]
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.GetOrCreateValue(key);
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x00083544 File Offset: 0x00081744
		public int Count
		{
			get
			{
				return this.EnsureFullyPopulated().Count;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x00083551 File Offset: 0x00081751
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this.EnsureFullyPopulated().Keys;
			}
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x00083560 File Offset: 0x00081760
		public void AddValues(ArrayBuilder<TElement> array)
		{
			foreach (KeyValuePair<TKey, ImmutableArray<TElement>> keyValuePair in this.EnsureFullyPopulated())
			{
				array.AddRange(keyValuePair.Value);
			}
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000835B4 File Offset: 0x000817B4
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		private ConcurrentDictionary<TKey, ImmutableArray<TElement>> CreateConcurrentDictionary()
		{
			return new ConcurrentDictionary<TKey, ImmutableArray<TElement>>(2, 0, this._comparer);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000835C3 File Offset: 0x000817C3
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		private IDictionary<TKey, ImmutableArray<TElement>> CreateDictionaryForFullyPopulatedMap(int capacity)
		{
			return new Dictionary<TKey, ImmutableArray<TElement>>(capacity, this._comparer);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000835D4 File Offset: 0x000817D4
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<TElement> GetOrCreateValue(TKey key)
		{
			IDictionary<TKey, ImmutableArray<TElement>> dictionary = this._map;
			ConcurrentDictionary<TKey, ImmutableArray<TElement>> concurrentDictionary;
			if (dictionary == null)
			{
				concurrentDictionary = this.CreateConcurrentDictionary();
				dictionary = Interlocked.CompareExchange<IDictionary<TKey, ImmutableArray<TElement>>>(ref this._map, concurrentDictionary, null);
				if (dictionary == null)
				{
					return this.AddToConcurrentMap(concurrentDictionary, key);
				}
			}
			ImmutableArray<TElement> immutableArray;
			if (dictionary.TryGetValue(key, ref immutableArray))
			{
				return immutableArray;
			}
			concurrentDictionary = dictionary as ConcurrentDictionary<TKey, ImmutableArray<TElement>>;
			if (concurrentDictionary != null)
			{
				return this.AddToConcurrentMap(concurrentDictionary, key);
			}
			return CachingDictionary<TKey, TElement>.s_emptySentinel;
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x00083634 File Offset: 0x00081834
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<TElement> AddToConcurrentMap([Nullable(new byte[] { 1, 1, 0, 1 })] ConcurrentDictionary<TKey, ImmutableArray<TElement>> map, TKey key)
		{
			ImmutableArray<TElement> immutableArray = this._getElementsOfKey.Invoke(key);
			if (immutableArray.IsDefaultOrEmpty)
			{
				immutableArray = CachingDictionary<TKey, TElement>.s_emptySentinel;
			}
			return map.GetOrAdd(key, immutableArray);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x00083665 File Offset: 0x00081865
		private static bool IsNotFullyPopulatedMap([Nullable(new byte[] { 2, 1, 0, 1 })] [NotNullWhen(false)] IDictionary<TKey, ImmutableArray<TElement>> existingMap)
		{
			return existingMap == null || existingMap is ConcurrentDictionary<TKey, ImmutableArray<TElement>>;
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x00083678 File Offset: 0x00081878
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		private IDictionary<TKey, ImmutableArray<TElement>> CreateFullyPopulatedMap([Nullable(new byte[] { 2, 1, 0, 1 })] IDictionary<TKey, ImmutableArray<TElement>> existingMap)
		{
			HashSet<TKey> hashSet = this._getKeys.Invoke(this._comparer);
			IDictionary<TKey, ImmutableArray<TElement>> dictionary = this.CreateDictionaryForFullyPopulatedMap(hashSet.Count);
			if (existingMap == null)
			{
				using (HashSet<TKey>.Enumerator enumerator = hashSet.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TKey tkey = enumerator.Current;
						dictionary.Add(tkey, this._getElementsOfKey.Invoke(tkey));
					}
					return dictionary;
				}
			}
			foreach (TKey tkey2 in hashSet)
			{
				ImmutableArray<TElement> immutableArray;
				if (!existingMap.TryGetValue(tkey2, ref immutableArray))
				{
					immutableArray = this._getElementsOfKey.Invoke(tkey2);
				}
				dictionary.Add(tkey2, immutableArray);
			}
			return dictionary;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x00083754 File Offset: 0x00081954
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		private IDictionary<TKey, ImmutableArray<TElement>> EnsureFullyPopulated()
		{
			IDictionary<TKey, ImmutableArray<TElement>> dictionary = null;
			IDictionary<TKey, ImmutableArray<TElement>> dictionary2 = this._map;
			while (CachingDictionary<TKey, TElement>.IsNotFullyPopulatedMap(dictionary2))
			{
				if (dictionary == null)
				{
					dictionary = this.CreateFullyPopulatedMap(dictionary2);
				}
				IDictionary<TKey, ImmutableArray<TElement>> dictionary3 = Interlocked.CompareExchange<IDictionary<TKey, ImmutableArray<TElement>>>(ref this._map, dictionary, dictionary2);
				if (dictionary3 == dictionary2)
				{
					return dictionary;
				}
				dictionary2 = dictionary3;
			}
			return dictionary2;
		}

		// Token: 0x04001274 RID: 4724
		[Nullable(new byte[] { 1, 1, 0, 1 })]
		private readonly Func<TKey, ImmutableArray<TElement>> _getElementsOfKey;

		// Token: 0x04001275 RID: 4725
		private readonly Func<IEqualityComparer<TKey>, HashSet<TKey>> _getKeys;

		// Token: 0x04001276 RID: 4726
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x04001277 RID: 4727
		[Nullable(new byte[] { 2, 1, 0, 1 })]
		private IDictionary<TKey, ImmutableArray<TElement>> _map;

		// Token: 0x04001278 RID: 4728
		[Nullable(new byte[] { 0, 1 })]
		private static readonly ImmutableArray<TElement> s_emptySentinel = ImmutableArray<TElement>.Empty;
	}
}
