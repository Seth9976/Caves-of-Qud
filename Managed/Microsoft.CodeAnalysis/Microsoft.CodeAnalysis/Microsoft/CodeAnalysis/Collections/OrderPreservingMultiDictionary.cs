using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x020004A0 RID: 1184
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrderPreservingMultiDictionary<K, V> : IEnumerable<KeyValuePair<K, OrderPreservingMultiDictionary<K, V>.ValueSet>>, IEnumerable
	{
		// Token: 0x06002EB9 RID: 11961 RVA: 0x00083AAB File Offset: 0x00081CAB
		private OrderPreservingMultiDictionary(ObjectPool<OrderPreservingMultiDictionary<K, V>> pool)
		{
			this._pool = pool;
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x00083ABC File Offset: 0x00081CBC
		public void Free()
		{
			if (this._dictionary != null)
			{
				foreach (KeyValuePair<K, OrderPreservingMultiDictionary<K, V>.ValueSet> keyValuePair in this._dictionary)
				{
					keyValuePair.Value.Free();
				}
				this._dictionary.Free();
				this._dictionary = null;
			}
			ObjectPool<OrderPreservingMultiDictionary<K, V>> pool = this._pool;
			if (pool == null)
			{
				return;
			}
			pool.Free(this);
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x00083B44 File Offset: 0x00081D44
		public static ObjectPool<OrderPreservingMultiDictionary<K, V>> CreatePool()
		{
			return new ObjectPool<OrderPreservingMultiDictionary<K, V>>((ObjectPool<OrderPreservingMultiDictionary<K, V>> pool) => new OrderPreservingMultiDictionary<K, V>(pool), 16);
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x00083B6C File Offset: 0x00081D6C
		public static OrderPreservingMultiDictionary<K, V> GetInstance()
		{
			return OrderPreservingMultiDictionary<K, V>.s_poolInstance.Allocate();
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x00083B78 File Offset: 0x00081D78
		public OrderPreservingMultiDictionary()
		{
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x00083B80 File Offset: 0x00081D80
		private void EnsureDictionary()
		{
			if (this._dictionary == null)
			{
				this._dictionary = PooledDictionary<K, OrderPreservingMultiDictionary<K, V>.ValueSet>.GetInstance();
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x00083B95 File Offset: 0x00081D95
		public bool IsEmpty
		{
			get
			{
				return this._dictionary == null;
			}
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x00083BA0 File Offset: 0x00081DA0
		public void Add(K k, V v)
		{
			OrderPreservingMultiDictionary<K, V>.ValueSet valueSet;
			if (this._dictionary != null && this._dictionary.TryGetValue(k, ref valueSet))
			{
				this._dictionary[k] = valueSet.WithAddedItem(v);
				return;
			}
			this.EnsureDictionary();
			this._dictionary[k] = new OrderPreservingMultiDictionary<K, V>.ValueSet(v);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x00083BF4 File Offset: 0x00081DF4
		public bool TryGetValue<[Nullable(2)] TArg>(K key, Func<V, TArg, bool> predicate, TArg arg, [MaybeNullWhen(false)] out V value)
		{
			OrderPreservingMultiDictionary<K, V>.ValueSet valueSet;
			if (this._dictionary != null && this._dictionary.TryGetValue(key, ref valueSet))
			{
				return valueSet.TryGetValue<TArg>(predicate, arg, out value);
			}
			value = default(V);
			return false;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00083C2E File Offset: 0x00081E2E
		[return: Nullable(new byte[] { 0, 1, 0, 0, 0 })]
		public Dictionary<K, OrderPreservingMultiDictionary<K, V>.ValueSet>.Enumerator GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return this._dictionary.GetEnumerator();
			}
			return OrderPreservingMultiDictionary<K, V>.s_emptyDictionary.GetEnumerator();
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x00083C4E File Offset: 0x00081E4E
		[return: Nullable(new byte[] { 1, 0, 1, 0, 0, 0 })]
		IEnumerator<KeyValuePair<K, OrderPreservingMultiDictionary<K, V>.ValueSet>> IEnumerable<KeyValuePair<K, OrderPreservingMultiDictionary<K, V>.ValueSet>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x00083C5B File Offset: 0x00081E5B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000D82 RID: 3458
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<V> this[K k]
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				OrderPreservingMultiDictionary<K, V>.ValueSet valueSet;
				if (this._dictionary != null && this._dictionary.TryGetValue(k, ref valueSet))
				{
					return valueSet.Items;
				}
				return ImmutableArray<V>.Empty;
			}
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x00083C9C File Offset: 0x00081E9C
		public bool Contains(K key, V value)
		{
			OrderPreservingMultiDictionary<K, V>.ValueSet valueSet;
			return this._dictionary != null && this._dictionary.TryGetValue(key, ref valueSet) && valueSet.Contains(value);
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x00083CCB File Offset: 0x00081ECB
		[Nullable(new byte[] { 1, 1, 0, 0, 0 })]
		public Dictionary<K, OrderPreservingMultiDictionary<K, V>.ValueSet>.KeyCollection Keys
		{
			[return: Nullable(new byte[] { 1, 1, 0, 0, 0 })]
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Keys;
				}
				return OrderPreservingMultiDictionary<K, V>.s_emptyDictionary.Keys;
			}
		}

		// Token: 0x0400127E RID: 4734
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		private readonly ObjectPool<OrderPreservingMultiDictionary<K, V>> _pool;

		// Token: 0x0400127F RID: 4735
		private static readonly ObjectPool<OrderPreservingMultiDictionary<K, V>> s_poolInstance = OrderPreservingMultiDictionary<K, V>.CreatePool();

		// Token: 0x04001280 RID: 4736
		[Nullable(new byte[] { 1, 1, 0, 0, 0 })]
		private static readonly Dictionary<K, OrderPreservingMultiDictionary<K, V>.ValueSet> s_emptyDictionary = new Dictionary<K, OrderPreservingMultiDictionary<K, V>.ValueSet>();

		// Token: 0x04001281 RID: 4737
		[Nullable(new byte[] { 2, 1, 0, 0, 0 })]
		private PooledDictionary<K, OrderPreservingMultiDictionary<K, V>.ValueSet> _dictionary;

		// Token: 0x02000749 RID: 1865
		[Nullable(0)]
		public struct ValueSet : IEnumerable<V>, IEnumerable
		{
			// Token: 0x06003D6B RID: 15723 RVA: 0x000ADCD8 File Offset: 0x000ABED8
			internal ValueSet(V value)
			{
				this._value = value;
			}

			// Token: 0x06003D6C RID: 15724 RVA: 0x000ADCE6 File Offset: 0x000ABEE6
			internal ValueSet(ArrayBuilder<V> values)
			{
				this._value = values;
			}

			// Token: 0x06003D6D RID: 15725 RVA: 0x000ADCEF File Offset: 0x000ABEEF
			internal void Free()
			{
				ArrayBuilder<V> arrayBuilder = this._value as ArrayBuilder<V>;
				if (arrayBuilder == null)
				{
					return;
				}
				arrayBuilder.Free();
			}

			// Token: 0x170010F7 RID: 4343
			internal V this[int index]
			{
				get
				{
					ArrayBuilder<V> arrayBuilder = this._value as ArrayBuilder<V>;
					if (arrayBuilder != null)
					{
						return arrayBuilder[index];
					}
					if (index == 0)
					{
						return (V)((object)this._value);
					}
					throw new IndexOutOfRangeException();
				}
			}

			// Token: 0x06003D6F RID: 15727 RVA: 0x000ADD40 File Offset: 0x000ABF40
			public bool TryGetValue<[Nullable(2)] TArg>(Func<V, TArg, bool> predicate, TArg arg, [MaybeNullWhen(false)] out V value)
			{
				ArrayBuilder<V> arrayBuilder = this._value as ArrayBuilder<V>;
				if (arrayBuilder != null)
				{
					foreach (V v in arrayBuilder)
					{
						if (predicate.Invoke(v, arg))
						{
							value = v;
							return true;
						}
					}
				}
				else
				{
					V v2 = (V)((object)this._value);
					if (predicate.Invoke(v2, arg))
					{
						value = v2;
						return true;
					}
				}
				value = default(V);
				return false;
			}

			// Token: 0x06003D70 RID: 15728 RVA: 0x000ADDB4 File Offset: 0x000ABFB4
			internal bool Contains(V item)
			{
				ArrayBuilder<V> arrayBuilder = this._value as ArrayBuilder<V>;
				if (arrayBuilder != null)
				{
					return arrayBuilder.Contains(item);
				}
				return EqualityComparer<V>.Default.Equals(item, (V)((object)this._value));
			}

			// Token: 0x170010F8 RID: 4344
			// (get) Token: 0x06003D71 RID: 15729 RVA: 0x000ADDF0 File Offset: 0x000ABFF0
			[Nullable(new byte[] { 0, 1 })]
			internal ImmutableArray<V> Items
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					ArrayBuilder<V> arrayBuilder = this._value as ArrayBuilder<V>;
					if (arrayBuilder == null)
					{
						return ImmutableArray.Create<V>((V)((object)this._value));
					}
					return arrayBuilder.ToImmutable();
				}
			}

			// Token: 0x170010F9 RID: 4345
			// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000ADE23 File Offset: 0x000AC023
			internal int Count
			{
				get
				{
					ArrayBuilder<V> arrayBuilder = this._value as ArrayBuilder<V>;
					if (arrayBuilder == null)
					{
						return 1;
					}
					return arrayBuilder.Count;
				}
			}

			// Token: 0x06003D73 RID: 15731 RVA: 0x000ADE3B File Offset: 0x000AC03B
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003D74 RID: 15732 RVA: 0x000ADE48 File Offset: 0x000AC048
			IEnumerator<V> IEnumerable<V>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003D75 RID: 15733 RVA: 0x000ADE55 File Offset: 0x000AC055
			[NullableContext(0)]
			public OrderPreservingMultiDictionary<K, V>.ValueSet.Enumerator GetEnumerator()
			{
				return new OrderPreservingMultiDictionary<K, V>.ValueSet.Enumerator(this);
			}

			// Token: 0x06003D76 RID: 15734 RVA: 0x000ADE64 File Offset: 0x000AC064
			[NullableContext(0)]
			internal OrderPreservingMultiDictionary<K, V>.ValueSet WithAddedItem([Nullable(1)] V item)
			{
				ArrayBuilder<V> arrayBuilder = this._value as ArrayBuilder<V>;
				if (arrayBuilder == null)
				{
					arrayBuilder = ArrayBuilder<V>.GetInstance(2);
					arrayBuilder.Add((V)((object)this._value));
					arrayBuilder.Add(item);
				}
				else
				{
					arrayBuilder.Add(item);
				}
				return new OrderPreservingMultiDictionary<K, V>.ValueSet(arrayBuilder);
			}

			// Token: 0x04001CE6 RID: 7398
			private readonly object _value;

			// Token: 0x020007F5 RID: 2037
			[NullableContext(0)]
			public struct Enumerator : IEnumerator<V>, IEnumerator, IDisposable
			{
				// Token: 0x0600409C RID: 16540 RVA: 0x000B4627 File Offset: 0x000B2827
				public Enumerator(OrderPreservingMultiDictionary<K, V>.ValueSet valueSet)
				{
					this._valueSet = valueSet;
					this._count = this._valueSet.Count;
					this._index = -1;
				}

				// Token: 0x170011B4 RID: 4532
				// (get) Token: 0x0600409D RID: 16541 RVA: 0x000B4648 File Offset: 0x000B2848
				[Nullable(1)]
				public V Current
				{
					[NullableContext(1)]
					get
					{
						return this._valueSet[this._index];
					}
				}

				// Token: 0x170011B5 RID: 4533
				// (get) Token: 0x0600409E RID: 16542 RVA: 0x000B4669 File Offset: 0x000B2869
				[Nullable(1)]
				object IEnumerator.Current
				{
					[NullableContext(1)]
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600409F RID: 16543 RVA: 0x000B4676 File Offset: 0x000B2876
				public bool MoveNext()
				{
					this._index++;
					return this._index < this._count;
				}

				// Token: 0x060040A0 RID: 16544 RVA: 0x000B4694 File Offset: 0x000B2894
				public void Reset()
				{
					this._index = -1;
				}

				// Token: 0x060040A1 RID: 16545 RVA: 0x000B469D File Offset: 0x000B289D
				public void Dispose()
				{
				}

				// Token: 0x04001E6A RID: 7786
				private readonly OrderPreservingMultiDictionary<K, V>.ValueSet _valueSet;

				// Token: 0x04001E6B RID: 7787
				private readonly int _count;

				// Token: 0x04001E6C RID: 7788
				private int _index;
			}
		}
	}
}
