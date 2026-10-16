using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000050 RID: 80
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MultiDictionary<K, [Nullable(2)] V> : IEnumerable<KeyValuePair<K, MultiDictionary<K, V>.ValueSet>>, IEnumerable
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000672F File Offset: 0x0000492F
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000673C File Offset: 0x0000493C
		public bool IsEmpty
		{
			get
			{
				return this._dictionary.Count == 0;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000674C File Offset: 0x0000494C
		[Nullable(new byte[] { 1, 1, 0, 0, 0 })]
		public Dictionary<K, MultiDictionary<K, V>.ValueSet>.KeyCollection Keys
		{
			[return: Nullable(new byte[] { 1, 1, 0, 0, 0 })]
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006759 File Offset: 0x00004959
		[Nullable(new byte[] { 1, 1, 0, 0, 0 })]
		public Dictionary<K, MultiDictionary<K, V>.ValueSet>.ValueCollection Values
		{
			[return: Nullable(new byte[] { 1, 1, 0, 0, 0 })]
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x17000035 RID: 53
		[Nullable(0)]
		public MultiDictionary<K, V>.ValueSet this[[Nullable(1)] K k]
		{
			[NullableContext(0)]
			get
			{
				MultiDictionary<K, V>.ValueSet valueSet;
				if (!this._dictionary.TryGetValue(k, ref valueSet))
				{
					return this._emptySet;
				}
				return valueSet;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000678D File Offset: 0x0000498D
		public MultiDictionary()
		{
			this._dictionary = new Dictionary<K, MultiDictionary<K, V>.ValueSet>();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000067AD File Offset: 0x000049AD
		public MultiDictionary(IEqualityComparer<K> comparer)
		{
			this._dictionary = new Dictionary<K, MultiDictionary<K, V>.ValueSet>(comparer);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000067CE File Offset: 0x000049CE
		public MultiDictionary(int capacity, IEqualityComparer<K> comparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<V> valueComparer = null)
		{
			this._dictionary = new Dictionary<K, MultiDictionary<K, V>.ValueSet>(capacity, comparer);
			this._valueComparer = valueComparer;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000067F8 File Offset: 0x000049F8
		public bool Add(K k, V v)
		{
			MultiDictionary<K, V>.ValueSet valueSet;
			MultiDictionary<K, V>.ValueSet valueSet2;
			if (this._dictionary.TryGetValue(k, ref valueSet))
			{
				valueSet2 = valueSet.Add(v);
				if (valueSet2.Equals(valueSet))
				{
					return false;
				}
			}
			else
			{
				valueSet2 = new MultiDictionary<K, V>.ValueSet(v, this._valueComparer);
			}
			this._dictionary[k] = valueSet2;
			return true;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000684B File Offset: 0x00004A4B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006858 File Offset: 0x00004A58
		[return: Nullable(new byte[] { 0, 1, 0, 0, 0 })]
		public Dictionary<K, MultiDictionary<K, V>.ValueSet>.Enumerator GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006865 File Offset: 0x00004A65
		[return: Nullable(new byte[] { 1, 0, 1, 0, 0, 0 })]
		IEnumerator<KeyValuePair<K, MultiDictionary<K, V>.ValueSet>> IEnumerable<KeyValuePair<K, MultiDictionary<K, V>.ValueSet>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006872 File Offset: 0x00004A72
		public bool ContainsKey(K k)
		{
			return this._dictionary.ContainsKey(k);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006880 File Offset: 0x00004A80
		internal void Clear()
		{
			this._dictionary.Clear();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000688D File Offset: 0x00004A8D
		public void Remove(K key)
		{
			this._dictionary.Remove(key);
		}

		// Token: 0x04000078 RID: 120
		[Nullable(new byte[] { 1, 1, 0, 0, 0 })]
		private readonly Dictionary<K, MultiDictionary<K, V>.ValueSet> _dictionary;

		// Token: 0x04000079 RID: 121
		[Nullable(new byte[] { 2, 1 })]
		private readonly IEqualityComparer<V> _valueComparer;

		// Token: 0x0400007A RID: 122
		[Nullable(0)]
		private readonly MultiDictionary<K, V>.ValueSet _emptySet = new MultiDictionary<K, V>.ValueSet(null, null);

		// Token: 0x02000501 RID: 1281
		[NullableContext(0)]
		public struct ValueSet : IEnumerable<V>, IEnumerable
		{
			// Token: 0x17000EAE RID: 3758
			// (get) Token: 0x060032EE RID: 13038 RVA: 0x000907C0 File Offset: 0x0008E9C0
			public int Count
			{
				get
				{
					if (this._value == null)
					{
						return 0;
					}
					ImmutableHashSet<V> immutableHashSet = this._value as ImmutableHashSet<V>;
					if (immutableHashSet == null)
					{
						return 1;
					}
					return immutableHashSet.Count;
				}
			}

			// Token: 0x060032EF RID: 13039 RVA: 0x000907EE File Offset: 0x0008E9EE
			[NullableContext(2)]
			public ValueSet(object value, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<V> equalityComparer = null)
			{
				this._value = value;
				this._equalityComparer = equalityComparer ?? ImmutableHashSet<V>.Empty.KeyComparer;
			}

			// Token: 0x060032F0 RID: 13040 RVA: 0x0009080C File Offset: 0x0008EA0C
			[NullableContext(1)]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060032F1 RID: 13041 RVA: 0x00090819 File Offset: 0x0008EA19
			[NullableContext(1)]
			IEnumerator<V> IEnumerable<V>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060032F2 RID: 13042 RVA: 0x00090826 File Offset: 0x0008EA26
			public MultiDictionary<K, V>.ValueSet.Enumerator GetEnumerator()
			{
				return new MultiDictionary<K, V>.ValueSet.Enumerator(this);
			}

			// Token: 0x060032F3 RID: 13043 RVA: 0x00090834 File Offset: 0x0008EA34
			public MultiDictionary<K, V>.ValueSet Add([Nullable(1)] V v)
			{
				ImmutableHashSet<V> immutableHashSet = this._value as ImmutableHashSet<V>;
				if (immutableHashSet == null)
				{
					if (this._equalityComparer.Equals((V)((object)this._value), v))
					{
						return this;
					}
					immutableHashSet = ImmutableHashSet.Create<V>(this._equalityComparer, (V)((object)this._value));
				}
				return new MultiDictionary<K, V>.ValueSet(immutableHashSet.Add(v), this._equalityComparer);
			}

			// Token: 0x060032F4 RID: 13044 RVA: 0x0009089C File Offset: 0x0008EA9C
			[NullableContext(1)]
			public bool Contains(V v)
			{
				ImmutableHashSet<V> immutableHashSet = this._value as ImmutableHashSet<V>;
				if (immutableHashSet == null)
				{
					return this._equalityComparer.Equals((V)((object)this._value), v);
				}
				return immutableHashSet.Contains(v);
			}

			// Token: 0x060032F5 RID: 13045 RVA: 0x000908D8 File Offset: 0x0008EAD8
			[NullableContext(1)]
			public bool Contains(V v, IEqualityComparer<V> comparer)
			{
				foreach (V v2 in this)
				{
					if (comparer.Equals(v2, v))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060032F6 RID: 13046 RVA: 0x00090930 File Offset: 0x0008EB30
			[NullableContext(1)]
			public V Single()
			{
				return (V)((object)this._value);
			}

			// Token: 0x060032F7 RID: 13047 RVA: 0x0009093D File Offset: 0x0008EB3D
			public bool Equals(MultiDictionary<K, V>.ValueSet other)
			{
				return this._value == other._value;
			}

			// Token: 0x04001408 RID: 5128
			[Nullable(2)]
			private readonly object _value;

			// Token: 0x04001409 RID: 5129
			[Nullable(1)]
			private readonly IEqualityComparer<V> _equalityComparer;

			// Token: 0x02000798 RID: 1944
			public struct Enumerator : IEnumerator<V>, IEnumerator, IDisposable
			{
				// Token: 0x06003F03 RID: 16131 RVA: 0x000B1178 File Offset: 0x000AF378
				public Enumerator(MultiDictionary<K, V>.ValueSet v)
				{
					if (v._value == null)
					{
						this._value = default(V);
						this._values = default(ImmutableHashSet<V>.Enumerator);
						this._count = 0;
						return;
					}
					ImmutableHashSet<V> immutableHashSet = v._value as ImmutableHashSet<V>;
					if (immutableHashSet == null)
					{
						this._value = (V)((object)v._value);
						this._values = default(ImmutableHashSet<V>.Enumerator);
						this._count = 1;
						return;
					}
					this._value = default(V);
					this._values = immutableHashSet.GetEnumerator();
					this._count = immutableHashSet.Count;
				}

				// Token: 0x06003F04 RID: 16132 RVA: 0x000B1205 File Offset: 0x000AF405
				public void Dispose()
				{
				}

				// Token: 0x06003F05 RID: 16133 RVA: 0x000B1207 File Offset: 0x000AF407
				public void Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x17001166 RID: 4454
				// (get) Token: 0x06003F06 RID: 16134 RVA: 0x000B120E File Offset: 0x000AF40E
				[Nullable(2)]
				object IEnumerator.Current
				{
					[NullableContext(2)]
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17001167 RID: 4455
				// (get) Token: 0x06003F07 RID: 16135 RVA: 0x000B121B File Offset: 0x000AF41B
				[Nullable(1)]
				public V Current
				{
					[NullableContext(1)]
					get
					{
						if (this._count <= 1)
						{
							return this._value;
						}
						return this._values.Current;
					}
				}

				// Token: 0x06003F08 RID: 16136 RVA: 0x000B1238 File Offset: 0x000AF438
				public bool MoveNext()
				{
					int count = this._count;
					if (count == 0)
					{
						return false;
					}
					if (count == 1)
					{
						this._count = 0;
						return true;
					}
					if (this._values.MoveNext())
					{
						return true;
					}
					this._count = 0;
					return false;
				}

				// Token: 0x04001D89 RID: 7561
				[Nullable(1)]
				[AllowNull]
				private readonly V _value;

				// Token: 0x04001D8A RID: 7562
				[Nullable(new byte[] { 0, 1 })]
				private ImmutableHashSet<V>.Enumerator _values;

				// Token: 0x04001D8B RID: 7563
				private int _count;
			}
		}
	}
}
