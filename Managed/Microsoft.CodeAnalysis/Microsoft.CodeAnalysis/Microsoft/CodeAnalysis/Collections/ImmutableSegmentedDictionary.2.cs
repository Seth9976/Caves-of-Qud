using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x020004A3 RID: 1187
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct ImmutableSegmentedDictionary<TKey, [Nullable(2)] TValue> : IImmutableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEquatable<ImmutableSegmentedDictionary<TKey, TValue>>
	{
		// Token: 0x06002EDD RID: 11997 RVA: 0x00084038 File Offset: 0x00082238
		private ImmutableSegmentedDictionary(SegmentedDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dictionary = dictionary;
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x00084050 File Offset: 0x00082250
		public IEqualityComparer<TKey> KeyComparer
		{
			get
			{
				return this._dictionary.Comparer;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x0008405D File Offset: 0x0008225D
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x0008406A File Offset: 0x0008226A
		public bool IsEmpty
		{
			get
			{
				return this._dictionary.Count == 0;
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x0008407A File Offset: 0x0008227A
		public bool IsDefault
		{
			get
			{
				return this._dictionary == null;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x00084088 File Offset: 0x00082288
		public bool IsDefaultOrEmpty
		{
			get
			{
				SegmentedDictionary<TKey, TValue> dictionary = this._dictionary;
				return !(((dictionary != null) ? new int?(dictionary.Count) : default(int?)) != 0);
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x000840CE File Offset: 0x000822CE
		[Nullable(0)]
		public ImmutableSegmentedDictionary<TKey, TValue>.KeyCollection Keys
		{
			[NullableContext(0)]
			get
			{
				return new ImmutableSegmentedDictionary<TKey, TValue>.KeyCollection(this);
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x000840DB File Offset: 0x000822DB
		[Nullable(0)]
		public ImmutableSegmentedDictionary<TKey, TValue>.ValueCollection Values
		{
			[NullableContext(0)]
			get
			{
				return new ImmutableSegmentedDictionary<TKey, TValue>.ValueCollection(this);
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06002EE5 RID: 12005 RVA: 0x000840E8 File Offset: 0x000822E8
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x000840F5 File Offset: 0x000822F5
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x00084102 File Offset: 0x00082302
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x0008410F File Offset: 0x0008230F
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x0008411C File Offset: 0x0008231C
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x0008411F File Offset: 0x0008231F
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x0008412C File Offset: 0x0008232C
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x00084139 File Offset: 0x00082339
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x0008413C File Offset: 0x0008233C
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x0008413F File Offset: 0x0008233F
		object ICollection.SyncRoot
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x00084147 File Offset: 0x00082347
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D96 RID: 3478
		public TValue this[TKey key]
		{
			get
			{
				return this._dictionary[key];
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x00084158 File Offset: 0x00082358
		// (set) Token: 0x06002EF2 RID: 12018 RVA: 0x00084161 File Offset: 0x00082361
		TValue IDictionary<TKey, TValue>.Item
		{
			get
			{
				return this[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x00084168 File Offset: 0x00082368
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x00084176 File Offset: 0x00082376
		[Nullable(2)]
		object IDictionary.Item
		{
			[return: Nullable(2)]
			get
			{
				return this._dictionary[key];
			}
			[param: Nullable(2)]
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0008417D File Offset: 0x0008237D
		public static bool operator ==([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> left, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x00084187 File Offset: 0x00082387
		public static bool operator !=([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> left, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x00084194 File Offset: 0x00082394
		public static bool operator ==([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue>? left, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue>? right)
		{
			return left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000841B8 File Offset: 0x000823B8
		public static bool operator !=([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue>? left, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue>? right)
		{
			return !left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000841E0 File Offset: 0x000823E0
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public ImmutableSegmentedDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = this;
			if (immutableSegmentedDictionary.Contains(new KeyValuePair<TKey, TValue>(key, value)))
			{
				return immutableSegmentedDictionary;
			}
			return new ImmutableSegmentedDictionary<TKey, TValue>(new SegmentedDictionary<TKey, TValue>(immutableSegmentedDictionary._dictionary, immutableSegmentedDictionary._dictionary.Comparer) { { key, value } });
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0008422C File Offset: 0x0008242C
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public ImmutableSegmentedDictionary<TKey, TValue> AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = this;
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary2;
			if (immutableSegmentedDictionary.IsEmpty && ImmutableSegmentedDictionary<TKey, TValue>.TryCastToImmutableSegmentedDictionary(pairs, out immutableSegmentedDictionary2) && immutableSegmentedDictionary.KeyComparer == immutableSegmentedDictionary2.KeyComparer)
			{
				return immutableSegmentedDictionary2;
			}
			SegmentedDictionary<TKey, TValue> segmentedDictionary = null;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in pairs)
			{
				if (!(segmentedDictionary ?? immutableSegmentedDictionary._dictionary).Contains(keyValuePair))
				{
					if (segmentedDictionary == null)
					{
						segmentedDictionary = new SegmentedDictionary<TKey, TValue>(immutableSegmentedDictionary._dictionary, immutableSegmentedDictionary._dictionary.Comparer);
					}
					segmentedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (segmentedDictionary == null)
			{
				return immutableSegmentedDictionary;
			}
			return new ImmutableSegmentedDictionary<TKey, TValue>(segmentedDictionary);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000842EC File Offset: 0x000824EC
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public ImmutableSegmentedDictionary<TKey, TValue> Clear()
		{
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = this;
			if (immutableSegmentedDictionary.IsEmpty)
			{
				return immutableSegmentedDictionary;
			}
			return ImmutableSegmentedDictionary<TKey, TValue>.Empty.WithComparer(immutableSegmentedDictionary.KeyComparer);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x0008431C File Offset: 0x0008251C
		public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair)
		{
			TValue tvalue;
			return this.TryGetValue(pair.Key, out tvalue) && EqualityComparer<TValue>.Default.Equals(tvalue, pair.Value);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x0008434E File Offset: 0x0008254E
		public bool ContainsKey(TKey key)
		{
			return this._dictionary.ContainsKey(key);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0008435C File Offset: 0x0008255C
		public bool ContainsValue(TValue value)
		{
			return this._dictionary.ContainsValue(value);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x0008436A File Offset: 0x0008256A
		[NullableContext(0)]
		public ImmutableSegmentedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new ImmutableSegmentedDictionary<TKey, TValue>.Enumerator(this._dictionary, ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType.KeyValuePair);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x00084378 File Offset: 0x00082578
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public ImmutableSegmentedDictionary<TKey, TValue> Remove(TKey key)
		{
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = this;
			if (!immutableSegmentedDictionary._dictionary.ContainsKey(key))
			{
				return immutableSegmentedDictionary;
			}
			SegmentedDictionary<TKey, TValue> segmentedDictionary = new SegmentedDictionary<TKey, TValue>(immutableSegmentedDictionary._dictionary, immutableSegmentedDictionary._dictionary.Comparer);
			segmentedDictionary.Remove(key);
			return new ImmutableSegmentedDictionary<TKey, TValue>(segmentedDictionary);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000843BF File Offset: 0x000825BF
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public ImmutableSegmentedDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			ImmutableSegmentedDictionary<TKey, TValue>.Builder builder = this.ToBuilder();
			builder.RemoveRange(keys);
			return builder.ToImmutable();
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000843E4 File Offset: 0x000825E4
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public ImmutableSegmentedDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = this;
			if (immutableSegmentedDictionary.Contains(new KeyValuePair<TKey, TValue>(key, value)))
			{
				return immutableSegmentedDictionary;
			}
			SegmentedDictionary<TKey, TValue> segmentedDictionary = new SegmentedDictionary<TKey, TValue>(immutableSegmentedDictionary._dictionary, immutableSegmentedDictionary._dictionary.Comparer);
			segmentedDictionary[key] = value;
			return new ImmutableSegmentedDictionary<TKey, TValue>(segmentedDictionary);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00084430 File Offset: 0x00082630
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public ImmutableSegmentedDictionary<TKey, TValue> SetItems([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			ImmutableSegmentedDictionary<TKey, TValue>.Builder builder = this.ToBuilder();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				builder[keyValuePair.Key] = keyValuePair.Value;
			}
			return builder.ToImmutable();
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000844A0 File Offset: 0x000826A0
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = this;
			foreach (TKey tkey in immutableSegmentedDictionary.Keys)
			{
				if (immutableSegmentedDictionary.KeyComparer.Equals(tkey, equalKey))
				{
					actualKey = tkey;
					return true;
				}
			}
			actualKey = equalKey;
			return false;
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x00084520 File Offset: 0x00082720
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			return this._dictionary.TryGetValue(key, out value);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x00084530 File Offset: 0x00082730
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public ImmutableSegmentedDictionary<TKey, TValue> WithComparer([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = EqualityComparer<TKey>.Default;
			}
			ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = this;
			if (immutableSegmentedDictionary.KeyComparer == keyComparer)
			{
				return immutableSegmentedDictionary;
			}
			if (!immutableSegmentedDictionary.IsEmpty)
			{
				return ImmutableSegmentedDictionary.CreateRange<TKey, TValue>(keyComparer, immutableSegmentedDictionary);
			}
			if (keyComparer == ImmutableSegmentedDictionary<TKey, TValue>.Empty.KeyComparer)
			{
				return ImmutableSegmentedDictionary<TKey, TValue>.Empty;
			}
			return new ImmutableSegmentedDictionary<TKey, TValue>(new SegmentedDictionary<TKey, TValue>(keyComparer));
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0008458E File Offset: 0x0008278E
		[return: Nullable(new byte[] { 1, 0, 0 })]
		public ImmutableSegmentedDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableSegmentedDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0008459B File Offset: 0x0008279B
		public override int GetHashCode()
		{
			SegmentedDictionary<TKey, TValue> dictionary = this._dictionary;
			if (dictionary == null)
			{
				return 0;
			}
			return dictionary.GetHashCode();
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000845B0 File Offset: 0x000827B0
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is ImmutableSegmentedDictionary<TKey, TValue>)
			{
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = (ImmutableSegmentedDictionary<TKey, TValue>)obj;
				return this.Equals(immutableSegmentedDictionary);
			}
			return false;
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000845D5 File Offset: 0x000827D5
		public bool Equals([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> other)
		{
			return this._dictionary == other._dictionary;
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000845E5 File Offset: 0x000827E5
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000845F2 File Offset: 0x000827F2
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x00084601 File Offset: 0x00082801
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0008460F File Offset: 0x0008280F
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0008461E File Offset: 0x0008281E
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0008462C File Offset: 0x0008282C
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x0008463A File Offset: 0x0008283A
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x00084648 File Offset: 0x00082848
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new ImmutableSegmentedDictionary<TKey, TValue>.Enumerator(this._dictionary, ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType.KeyValuePair);
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x0008465B File Offset: 0x0008285B
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ImmutableSegmentedDictionary<TKey, TValue>.Enumerator(this._dictionary, ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType.DictionaryEntry);
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x0008466E File Offset: 0x0008286E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableSegmentedDictionary<TKey, TValue>.Enumerator(this._dictionary, ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType.KeyValuePair);
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x00084681 File Offset: 0x00082881
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this._dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x00084690 File Offset: 0x00082890
		bool IDictionary.Contains(object key)
		{
			return this._dictionary.Contains(key);
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x0008469E File Offset: 0x0008289E
		void ICollection.CopyTo(Array array, int index)
		{
			this._dictionary.CopyTo(array, index);
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000846AD File Offset: 0x000828AD
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000846B4 File Offset: 0x000828B4
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000846BB File Offset: 0x000828BB
		void ICollection<KeyValuePair<TKey, TValue>>.Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000846C2 File Offset: 0x000828C2
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000846C9 File Offset: 0x000828C9
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000846D0 File Offset: 0x000828D0
		void IDictionary.Add(object key, [Nullable(2)] object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000846D7 File Offset: 0x000828D7
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000846DE File Offset: 0x000828DE
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000846E8 File Offset: 0x000828E8
		private static bool TryCastToImmutableSegmentedDictionary([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> pairs, [Nullable(new byte[] { 0, 1, 1 })] out ImmutableSegmentedDictionary<TKey, TValue> other)
		{
			if (pairs is ImmutableSegmentedDictionary<TKey, TValue>)
			{
				ImmutableSegmentedDictionary<TKey, TValue> immutableSegmentedDictionary = (ImmutableSegmentedDictionary<TKey, TValue>)pairs;
				other = immutableSegmentedDictionary;
				return true;
			}
			ImmutableSegmentedDictionary<TKey, TValue>.Builder builder = pairs as ImmutableSegmentedDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			other = default(ImmutableSegmentedDictionary<TKey, TValue>);
			return false;
		}

		// Token: 0x04001288 RID: 4744
		[Nullable(new byte[] { 0, 1, 1 })]
		public static readonly ImmutableSegmentedDictionary<TKey, TValue> Empty = new ImmutableSegmentedDictionary<TKey, TValue>(new SegmentedDictionary<TKey, TValue>());

		// Token: 0x04001289 RID: 4745
		private readonly SegmentedDictionary<TKey, TValue> _dictionary;

		// Token: 0x0200074E RID: 1870
		[Nullable(0)]
		public sealed class Builder : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x06003D82 RID: 15746 RVA: 0x000ADF1F File Offset: 0x000AC11F
			internal Builder([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> dictionary)
			{
				this._dictionary = dictionary;
			}

			// Token: 0x170010FA RID: 4346
			// (get) Token: 0x06003D83 RID: 15747 RVA: 0x000ADF2E File Offset: 0x000AC12E
			// (set) Token: 0x06003D84 RID: 15748 RVA: 0x000ADF3C File Offset: 0x000AC13C
			public IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this.ReadOnlyDictionary.Comparer;
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (value != this.KeyComparer)
					{
						SegmentedDictionary<TKey, TValue> readOnlyDictionary = this.ReadOnlyDictionary;
						this._mutableDictionary = new SegmentedDictionary<TKey, TValue>(value);
						this.AddRange(readOnlyDictionary);
					}
				}
			}

			// Token: 0x170010FB RID: 4347
			// (get) Token: 0x06003D85 RID: 15749 RVA: 0x000ADF7A File Offset: 0x000AC17A
			public int Count
			{
				get
				{
					return this.ReadOnlyDictionary.Count;
				}
			}

			// Token: 0x170010FC RID: 4348
			// (get) Token: 0x06003D86 RID: 15750 RVA: 0x000ADF87 File Offset: 0x000AC187
			[Nullable(0)]
			public ImmutableSegmentedDictionary<TKey, TValue>.Builder.KeyCollection Keys
			{
				[NullableContext(0)]
				get
				{
					return new ImmutableSegmentedDictionary<TKey, TValue>.Builder.KeyCollection(this);
				}
			}

			// Token: 0x170010FD RID: 4349
			// (get) Token: 0x06003D87 RID: 15751 RVA: 0x000ADF8F File Offset: 0x000AC18F
			[Nullable(0)]
			public ImmutableSegmentedDictionary<TKey, TValue>.Builder.ValueCollection Values
			{
				[NullableContext(0)]
				get
				{
					return new ImmutableSegmentedDictionary<TKey, TValue>.Builder.ValueCollection(this);
				}
			}

			// Token: 0x170010FE RID: 4350
			// (get) Token: 0x06003D88 RID: 15752 RVA: 0x000ADF97 File Offset: 0x000AC197
			private SegmentedDictionary<TKey, TValue> ReadOnlyDictionary
			{
				get
				{
					return this._mutableDictionary ?? this._dictionary._dictionary;
				}
			}

			// Token: 0x170010FF RID: 4351
			// (get) Token: 0x06003D89 RID: 15753 RVA: 0x000ADFAE File Offset: 0x000AC1AE
			IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
			{
				get
				{
					return this.Keys;
				}
			}

			// Token: 0x17001100 RID: 4352
			// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000ADFBB File Offset: 0x000AC1BB
			IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
			{
				get
				{
					return this.Values;
				}
			}

			// Token: 0x17001101 RID: 4353
			// (get) Token: 0x06003D8B RID: 15755 RVA: 0x000ADFC8 File Offset: 0x000AC1C8
			ICollection<TKey> IDictionary<TKey, TValue>.Keys
			{
				get
				{
					return this.Keys;
				}
			}

			// Token: 0x17001102 RID: 4354
			// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000ADFD5 File Offset: 0x000AC1D5
			ICollection<TValue> IDictionary<TKey, TValue>.Values
			{
				get
				{
					return this.Values;
				}
			}

			// Token: 0x17001103 RID: 4355
			// (get) Token: 0x06003D8D RID: 15757 RVA: 0x000ADFE2 File Offset: 0x000AC1E2
			bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001104 RID: 4356
			// (get) Token: 0x06003D8E RID: 15758 RVA: 0x000ADFE5 File Offset: 0x000AC1E5
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys;
				}
			}

			// Token: 0x17001105 RID: 4357
			// (get) Token: 0x06003D8F RID: 15759 RVA: 0x000ADFF2 File Offset: 0x000AC1F2
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values;
				}
			}

			// Token: 0x17001106 RID: 4358
			// (get) Token: 0x06003D90 RID: 15760 RVA: 0x000ADFFF File Offset: 0x000AC1FF
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001107 RID: 4359
			// (get) Token: 0x06003D91 RID: 15761 RVA: 0x000AE002 File Offset: 0x000AC202
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001108 RID: 4360
			// (get) Token: 0x06003D92 RID: 15762 RVA: 0x000AE005 File Offset: 0x000AC205
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001109 RID: 4361
			// (get) Token: 0x06003D93 RID: 15763 RVA: 0x000AE008 File Offset: 0x000AC208
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700110A RID: 4362
			public TValue this[TKey key]
			{
				get
				{
					return this.ReadOnlyDictionary[key];
				}
				set
				{
					this.GetOrCreateMutableDictionary()[key] = value;
				}
			}

			// Token: 0x1700110B RID: 4363
			// (get) Token: 0x06003D96 RID: 15766 RVA: 0x000AE028 File Offset: 0x000AC228
			// (set) Token: 0x06003D97 RID: 15767 RVA: 0x000AE036 File Offset: 0x000AC236
			[Nullable(2)]
			object IDictionary.Item
			{
				[return: Nullable(2)]
				get
				{
					return this.ReadOnlyDictionary[key];
				}
				[param: Nullable(2)]
				set
				{
					this.GetOrCreateMutableDictionary()[key] = value;
				}
			}

			// Token: 0x06003D98 RID: 15768 RVA: 0x000AE048 File Offset: 0x000AC248
			private SegmentedDictionary<TKey, TValue> GetOrCreateMutableDictionary()
			{
				SegmentedDictionary<TKey, TValue> segmentedDictionary;
				if ((segmentedDictionary = this._mutableDictionary) == null)
				{
					segmentedDictionary = (this._mutableDictionary = new SegmentedDictionary<TKey, TValue>(this._dictionary._dictionary, this._dictionary.KeyComparer));
				}
				return segmentedDictionary;
			}

			// Token: 0x06003D99 RID: 15769 RVA: 0x000AE083 File Offset: 0x000AC283
			public void Add(TKey key, TValue value)
			{
				if (this.Contains(new KeyValuePair<TKey, TValue>(key, value)))
				{
					return;
				}
				this.GetOrCreateMutableDictionary().Add(key, value);
			}

			// Token: 0x06003D9A RID: 15770 RVA: 0x000AE0A2 File Offset: 0x000AC2A2
			public void Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x06003D9B RID: 15771 RVA: 0x000AE0B8 File Offset: 0x000AC2B8
			public void AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					this.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}

			// Token: 0x06003D9C RID: 15772 RVA: 0x000AE11C File Offset: 0x000AC31C
			public void Clear()
			{
				if (this.ReadOnlyDictionary.Count != 0)
				{
					if (this._mutableDictionary == null)
					{
						this._mutableDictionary = new SegmentedDictionary<TKey, TValue>(this.KeyComparer);
						return;
					}
					this._mutableDictionary.Clear();
				}
			}

			// Token: 0x06003D9D RID: 15773 RVA: 0x000AE150 File Offset: 0x000AC350
			public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				TValue tvalue;
				return this.TryGetValue(item.Key, out tvalue) && EqualityComparer<TValue>.Default.Equals(tvalue, item.Value);
			}

			// Token: 0x06003D9E RID: 15774 RVA: 0x000AE182 File Offset: 0x000AC382
			public bool ContainsKey(TKey key)
			{
				return this.ReadOnlyDictionary.ContainsKey(key);
			}

			// Token: 0x06003D9F RID: 15775 RVA: 0x000AE190 File Offset: 0x000AC390
			public bool ContainsValue(TValue value)
			{
				return this._dictionary.ContainsValue(value);
			}

			// Token: 0x06003DA0 RID: 15776 RVA: 0x000AE19E File Offset: 0x000AC39E
			[NullableContext(0)]
			public ImmutableSegmentedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableSegmentedDictionary<TKey, TValue>.Enumerator(this.GetOrCreateMutableDictionary(), ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType.KeyValuePair);
			}

			// Token: 0x06003DA1 RID: 15777 RVA: 0x000AE1AC File Offset: 0x000AC3AC
			[return: Nullable(2)]
			public TValue GetValueOrDefault(TKey key)
			{
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				return default(TValue);
			}

			// Token: 0x06003DA2 RID: 15778 RVA: 0x000AE1D0 File Offset: 0x000AC3D0
			public TValue GetValueOrDefault(TKey key, TValue defaultValue)
			{
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				return defaultValue;
			}

			// Token: 0x06003DA3 RID: 15779 RVA: 0x000AE1EB File Offset: 0x000AC3EB
			public bool Remove(TKey key)
			{
				return (this._mutableDictionary != null || this.ContainsKey(key)) && this.GetOrCreateMutableDictionary().Remove(key);
			}

			// Token: 0x06003DA4 RID: 15780 RVA: 0x000AE20C File Offset: 0x000AC40C
			public bool Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				if (!this.Contains(item))
				{
					return false;
				}
				this.GetOrCreateMutableDictionary().Remove(item.Key);
				return true;
			}

			// Token: 0x06003DA5 RID: 15781 RVA: 0x000AE230 File Offset: 0x000AC430
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				if (keys == null)
				{
					throw new ArgumentNullException("keys");
				}
				foreach (TKey tkey in keys)
				{
					this.Remove(tkey);
				}
			}

			// Token: 0x06003DA6 RID: 15782 RVA: 0x000AE288 File Offset: 0x000AC488
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				foreach (TKey tkey in this.Keys)
				{
					if (this.KeyComparer.Equals(tkey, equalKey))
					{
						actualKey = tkey;
						return true;
					}
				}
				actualKey = equalKey;
				return false;
			}

			// Token: 0x06003DA7 RID: 15783 RVA: 0x000AE2FC File Offset: 0x000AC4FC
			public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				return this.ReadOnlyDictionary.TryGetValue(key, out value);
			}

			// Token: 0x06003DA8 RID: 15784 RVA: 0x000AE30B File Offset: 0x000AC50B
			[return: Nullable(new byte[] { 0, 1, 1 })]
			public ImmutableSegmentedDictionary<TKey, TValue> ToImmutable()
			{
				this._dictionary = new ImmutableSegmentedDictionary<TKey, TValue>(this.ReadOnlyDictionary);
				this._mutableDictionary = null;
				return this._dictionary;
			}

			// Token: 0x06003DA9 RID: 15785 RVA: 0x000AE32B File Offset: 0x000AC52B
			void ICollection<KeyValuePair<TKey, TValue>>.CopyTo([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				this.ReadOnlyDictionary.CopyTo(array, arrayIndex);
			}

			// Token: 0x06003DAA RID: 15786 RVA: 0x000AE33A File Offset: 0x000AC53A
			[return: Nullable(new byte[] { 1, 0, 1, 1 })]
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return new ImmutableSegmentedDictionary<TKey, TValue>.Enumerator(this.GetOrCreateMutableDictionary(), ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType.KeyValuePair);
			}

			// Token: 0x06003DAB RID: 15787 RVA: 0x000AE34D File Offset: 0x000AC54D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ImmutableSegmentedDictionary<TKey, TValue>.Enumerator(this.GetOrCreateMutableDictionary(), ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType.KeyValuePair);
			}

			// Token: 0x06003DAC RID: 15788 RVA: 0x000AE360 File Offset: 0x000AC560
			bool IDictionary.Contains(object key)
			{
				return this.ReadOnlyDictionary.Contains(key);
			}

			// Token: 0x06003DAD RID: 15789 RVA: 0x000AE36E File Offset: 0x000AC56E
			void IDictionary.Add(object key, [Nullable(2)] object value)
			{
				this.GetOrCreateMutableDictionary().Add(key, value);
			}

			// Token: 0x06003DAE RID: 15790 RVA: 0x000AE37D File Offset: 0x000AC57D
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new ImmutableSegmentedDictionary<TKey, TValue>.Enumerator(this.GetOrCreateMutableDictionary(), ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType.DictionaryEntry);
			}

			// Token: 0x06003DAF RID: 15791 RVA: 0x000AE390 File Offset: 0x000AC590
			void IDictionary.Remove(object key)
			{
				this.GetOrCreateMutableDictionary().Remove(key);
			}

			// Token: 0x06003DB0 RID: 15792 RVA: 0x000AE39E File Offset: 0x000AC59E
			void ICollection.CopyTo(Array array, int index)
			{
				this.ReadOnlyDictionary.CopyTo(array, index);
			}

			// Token: 0x04001CEF RID: 7407
			[Nullable(new byte[] { 0, 1, 1 })]
			private ImmutableSegmentedDictionary<TKey, TValue> _dictionary;

			// Token: 0x04001CF0 RID: 7408
			[Nullable(new byte[] { 2, 1, 1 })]
			private SegmentedDictionary<TKey, TValue> _mutableDictionary;

			// Token: 0x020007F6 RID: 2038
			[Nullable(0)]
			public readonly struct KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, IReadOnlyCollection<TKey>, ICollection
			{
				// Token: 0x060040A2 RID: 16546 RVA: 0x000B469F File Offset: 0x000B289F
				internal KeyCollection(ImmutableSegmentedDictionary<TKey, TValue>.Builder dictionary)
				{
					this._dictionary = dictionary;
				}

				// Token: 0x170011B6 RID: 4534
				// (get) Token: 0x060040A3 RID: 16547 RVA: 0x000B46A8 File Offset: 0x000B28A8
				public int Count
				{
					get
					{
						return this._dictionary.Count;
					}
				}

				// Token: 0x170011B7 RID: 4535
				// (get) Token: 0x060040A4 RID: 16548 RVA: 0x000B46B5 File Offset: 0x000B28B5
				bool ICollection<TKey>.IsReadOnly
				{
					get
					{
						return false;
					}
				}

				// Token: 0x170011B8 RID: 4536
				// (get) Token: 0x060040A5 RID: 16549 RVA: 0x000B46B8 File Offset: 0x000B28B8
				bool ICollection.IsSynchronized
				{
					get
					{
						return false;
					}
				}

				// Token: 0x170011B9 RID: 4537
				// (get) Token: 0x060040A6 RID: 16550 RVA: 0x000B46BB File Offset: 0x000B28BB
				object ICollection.SyncRoot
				{
					get
					{
						return this._dictionary.SyncRoot;
					}
				}

				// Token: 0x060040A7 RID: 16551 RVA: 0x000B46C8 File Offset: 0x000B28C8
				void ICollection<TKey>.Add(TKey item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x060040A8 RID: 16552 RVA: 0x000B46CF File Offset: 0x000B28CF
				public void Clear()
				{
					this._dictionary.Clear();
				}

				// Token: 0x060040A9 RID: 16553 RVA: 0x000B46DC File Offset: 0x000B28DC
				public bool Contains(TKey item)
				{
					return this._dictionary.ContainsKey(item);
				}

				// Token: 0x060040AA RID: 16554 RVA: 0x000B46EA File Offset: 0x000B28EA
				public void CopyTo(TKey[] array, int arrayIndex)
				{
					this._dictionary.ReadOnlyDictionary.Keys.CopyTo(array, arrayIndex);
				}

				// Token: 0x060040AB RID: 16555 RVA: 0x000B4703 File Offset: 0x000B2903
				[return: Nullable(new byte[] { 0, 1, 1 })]
				public ImmutableSegmentedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
				{
					return new ImmutableSegmentedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary.GetEnumerator());
				}

				// Token: 0x060040AC RID: 16556 RVA: 0x000B4715 File Offset: 0x000B2915
				public bool Remove(TKey item)
				{
					return this._dictionary.Remove(item);
				}

				// Token: 0x060040AD RID: 16557 RVA: 0x000B4723 File Offset: 0x000B2923
				void ICollection.CopyTo(Array array, int index)
				{
					this._dictionary.ReadOnlyDictionary.Keys.CopyTo(array, index);
				}

				// Token: 0x060040AE RID: 16558 RVA: 0x000B473C File Offset: 0x000B293C
				IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060040AF RID: 16559 RVA: 0x000B4749 File Offset: 0x000B2949
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04001E6D RID: 7789
				private readonly ImmutableSegmentedDictionary<TKey, TValue>.Builder _dictionary;
			}

			// Token: 0x020007F7 RID: 2039
			[Nullable(0)]
			public readonly struct ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue>, ICollection
			{
				// Token: 0x060040B0 RID: 16560 RVA: 0x000B4756 File Offset: 0x000B2956
				internal ValueCollection(ImmutableSegmentedDictionary<TKey, TValue>.Builder dictionary)
				{
					this._dictionary = dictionary;
				}

				// Token: 0x170011BA RID: 4538
				// (get) Token: 0x060040B1 RID: 16561 RVA: 0x000B475F File Offset: 0x000B295F
				public int Count
				{
					get
					{
						return this._dictionary.Count;
					}
				}

				// Token: 0x170011BB RID: 4539
				// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000B476C File Offset: 0x000B296C
				bool ICollection<TValue>.IsReadOnly
				{
					get
					{
						return false;
					}
				}

				// Token: 0x170011BC RID: 4540
				// (get) Token: 0x060040B3 RID: 16563 RVA: 0x000B476F File Offset: 0x000B296F
				bool ICollection.IsSynchronized
				{
					get
					{
						return false;
					}
				}

				// Token: 0x170011BD RID: 4541
				// (get) Token: 0x060040B4 RID: 16564 RVA: 0x000B4772 File Offset: 0x000B2972
				object ICollection.SyncRoot
				{
					get
					{
						return this._dictionary.SyncRoot;
					}
				}

				// Token: 0x060040B5 RID: 16565 RVA: 0x000B477F File Offset: 0x000B297F
				void ICollection<TValue>.Add(TValue item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x060040B6 RID: 16566 RVA: 0x000B4786 File Offset: 0x000B2986
				public void Clear()
				{
					this._dictionary.Clear();
				}

				// Token: 0x060040B7 RID: 16567 RVA: 0x000B4793 File Offset: 0x000B2993
				public bool Contains(TValue item)
				{
					return this._dictionary.ContainsValue(item);
				}

				// Token: 0x060040B8 RID: 16568 RVA: 0x000B47A1 File Offset: 0x000B29A1
				public void CopyTo(TValue[] array, int arrayIndex)
				{
					this._dictionary.ReadOnlyDictionary.Values.CopyTo(array, arrayIndex);
				}

				// Token: 0x060040B9 RID: 16569 RVA: 0x000B47BA File Offset: 0x000B29BA
				[return: Nullable(new byte[] { 0, 1, 1 })]
				public ImmutableSegmentedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
				{
					return new ImmutableSegmentedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary.GetEnumerator());
				}

				// Token: 0x060040BA RID: 16570 RVA: 0x000B47CC File Offset: 0x000B29CC
				bool ICollection<TValue>.Remove(TValue item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x060040BB RID: 16571 RVA: 0x000B47D3 File Offset: 0x000B29D3
				void ICollection.CopyTo(Array array, int index)
				{
					this._dictionary.ReadOnlyDictionary.Values.CopyTo(array, index);
				}

				// Token: 0x060040BC RID: 16572 RVA: 0x000B47EC File Offset: 0x000B29EC
				IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060040BD RID: 16573 RVA: 0x000B47F9 File Offset: 0x000B29F9
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04001E6E RID: 7790
				private readonly ImmutableSegmentedDictionary<TKey, TValue>.Builder _dictionary;
			}
		}

		// Token: 0x0200074F RID: 1871
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable, IDictionaryEnumerator
		{
			// Token: 0x06003DB1 RID: 15793 RVA: 0x000AE3AD File Offset: 0x000AC5AD
			internal Enumerator([Nullable(1)] SegmentedDictionary<TKey, TValue> dictionary, ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType returnType)
			{
				this._dictionary = dictionary;
				this._returnType = returnType;
				this._enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x1700110C RID: 4364
			// (get) Token: 0x06003DB2 RID: 15794 RVA: 0x000AE3C9 File Offset: 0x000AC5C9
			[Nullable(new byte[] { 0, 1, 1 })]
			public KeyValuePair<TKey, TValue> Current
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x1700110D RID: 4365
			// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x000AE3D6 File Offset: 0x000AC5D6
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					if (this._returnType != ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType.DictionaryEntry)
					{
						return this.Current;
					}
					return this.Entry;
				}
			}

			// Token: 0x1700110E RID: 4366
			// (get) Token: 0x06003DB4 RID: 15796 RVA: 0x000AE404 File Offset: 0x000AC604
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x1700110F RID: 4367
			// (get) Token: 0x06003DB5 RID: 15797 RVA: 0x000AE43C File Offset: 0x000AC63C
			[Nullable(1)]
			object IDictionaryEnumerator.Key
			{
				[NullableContext(1)]
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x17001110 RID: 4368
			// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x000AE45C File Offset: 0x000AC65C
			[Nullable(2)]
			object IDictionaryEnumerator.Value
			{
				[NullableContext(2)]
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x06003DB7 RID: 15799 RVA: 0x000AE47C File Offset: 0x000AC67C
			public void Dispose()
			{
				this._enumerator.Dispose();
			}

			// Token: 0x06003DB8 RID: 15800 RVA: 0x000AE489 File Offset: 0x000AC689
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06003DB9 RID: 15801 RVA: 0x000AE496 File Offset: 0x000AC696
			public void Reset()
			{
				this._enumerator = this._dictionary.GetEnumerator();
			}

			// Token: 0x04001CF1 RID: 7409
			[Nullable(1)]
			private readonly SegmentedDictionary<TKey, TValue> _dictionary;

			// Token: 0x04001CF2 RID: 7410
			private readonly ImmutableSegmentedDictionary<TKey, TValue>.Enumerator.ReturnType _returnType;

			// Token: 0x04001CF3 RID: 7411
			[Nullable(new byte[] { 0, 1, 1 })]
			private SegmentedDictionary<TKey, TValue>.Enumerator _enumerator;

			// Token: 0x020007F8 RID: 2040
			internal enum ReturnType
			{
				// Token: 0x04001E70 RID: 7792
				KeyValuePair,
				// Token: 0x04001E71 RID: 7793
				DictionaryEntry
			}
		}

		// Token: 0x02000750 RID: 1872
		[Nullable(0)]
		public readonly struct KeyCollection : IReadOnlyCollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection<TKey>, ICollection
		{
			// Token: 0x06003DBA RID: 15802 RVA: 0x000AE4A9 File Offset: 0x000AC6A9
			internal KeyCollection([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> dictionary)
			{
				this._dictionary = dictionary;
			}

			// Token: 0x17001111 RID: 4369
			// (get) Token: 0x06003DBB RID: 15803 RVA: 0x000AE4B2 File Offset: 0x000AC6B2
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x17001112 RID: 4370
			// (get) Token: 0x06003DBC RID: 15804 RVA: 0x000AE4BF File Offset: 0x000AC6BF
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001113 RID: 4371
			// (get) Token: 0x06003DBD RID: 15805 RVA: 0x000AE4C2 File Offset: 0x000AC6C2
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001114 RID: 4372
			// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000AE4C5 File Offset: 0x000AC6C5
			object ICollection.SyncRoot
			{
				get
				{
					return this._dictionary.SyncRoot;
				}
			}

			// Token: 0x06003DBF RID: 15807 RVA: 0x000AE4D7 File Offset: 0x000AC6D7
			[NullableContext(0)]
			public ImmutableSegmentedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new ImmutableSegmentedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary.GetEnumerator());
			}

			// Token: 0x06003DC0 RID: 15808 RVA: 0x000AE4E9 File Offset: 0x000AC6E9
			public bool Contains(TKey item)
			{
				return this._dictionary.ContainsKey(item);
			}

			// Token: 0x06003DC1 RID: 15809 RVA: 0x000AE4F7 File Offset: 0x000AC6F7
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003DC2 RID: 15810 RVA: 0x000AE504 File Offset: 0x000AC704
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003DC3 RID: 15811 RVA: 0x000AE511 File Offset: 0x000AC711
			void ICollection<TKey>.CopyTo(TKey[] array, int arrayIndex)
			{
				this._dictionary._dictionary.Keys.CopyTo(array, arrayIndex);
			}

			// Token: 0x06003DC4 RID: 15812 RVA: 0x000AE52A File Offset: 0x000AC72A
			void ICollection.CopyTo(Array array, int index)
			{
				this._dictionary._dictionary.Keys.CopyTo(array, index);
			}

			// Token: 0x06003DC5 RID: 15813 RVA: 0x000AE543 File Offset: 0x000AC743
			void ICollection<TKey>.Add(TKey item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003DC6 RID: 15814 RVA: 0x000AE54A File Offset: 0x000AC74A
			void ICollection<TKey>.Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003DC7 RID: 15815 RVA: 0x000AE551 File Offset: 0x000AC751
			bool ICollection<TKey>.Remove(TKey item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04001CF4 RID: 7412
			[Nullable(new byte[] { 0, 1, 1 })]
			private readonly ImmutableSegmentedDictionary<TKey, TValue> _dictionary;

			// Token: 0x020007F9 RID: 2041
			[Nullable(0)]
			public struct Enumerator : IEnumerator<TKey>, IEnumerator, IDisposable
			{
				// Token: 0x060040BE RID: 16574 RVA: 0x000B4806 File Offset: 0x000B2A06
				internal Enumerator([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue>.Enumerator enumerator)
				{
					this._enumerator = enumerator;
				}

				// Token: 0x170011BE RID: 4542
				// (get) Token: 0x060040BF RID: 16575 RVA: 0x000B4810 File Offset: 0x000B2A10
				public TKey Current
				{
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
						return keyValuePair.Key;
					}
				}

				// Token: 0x170011BF RID: 4543
				// (get) Token: 0x060040C0 RID: 16576 RVA: 0x000B4830 File Offset: 0x000B2A30
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060040C1 RID: 16577 RVA: 0x000B483D File Offset: 0x000B2A3D
				public void Dispose()
				{
					this._enumerator.Dispose();
				}

				// Token: 0x060040C2 RID: 16578 RVA: 0x000B484A File Offset: 0x000B2A4A
				public bool MoveNext()
				{
					return this._enumerator.MoveNext();
				}

				// Token: 0x060040C3 RID: 16579 RVA: 0x000B4857 File Offset: 0x000B2A57
				public void Reset()
				{
					this._enumerator.Reset();
				}

				// Token: 0x04001E72 RID: 7794
				[Nullable(new byte[] { 0, 1, 1 })]
				private ImmutableSegmentedDictionary<TKey, TValue>.Enumerator _enumerator;
			}
		}

		// Token: 0x02000751 RID: 1873
		[NullableContext(0)]
		internal static class PrivateInterlocked
		{
			// Token: 0x06003DC8 RID: 15816 RVA: 0x000AE558 File Offset: 0x000AC758
			[return: Nullable(new byte[] { 0, 1, 1 })]
			internal static ImmutableSegmentedDictionary<TKey, TValue> VolatileRead([Nullable(new byte[] { 0, 1, 1 })] in ImmutableSegmentedDictionary<TKey, TValue> location)
			{
				SegmentedDictionary<TKey, TValue> segmentedDictionary = Volatile.Read<SegmentedDictionary<TKey, TValue>>(Unsafe.AsRef<SegmentedDictionary<TKey, TValue>>(in location._dictionary));
				if (segmentedDictionary == null)
				{
					return default(ImmutableSegmentedDictionary<TKey, TValue>);
				}
				return new ImmutableSegmentedDictionary<TKey, TValue>(segmentedDictionary);
			}

			// Token: 0x06003DC9 RID: 15817 RVA: 0x000AE58C File Offset: 0x000AC78C
			[return: Nullable(new byte[] { 0, 1, 1 })]
			internal static ImmutableSegmentedDictionary<TKey, TValue> InterlockedExchange([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> value)
			{
				SegmentedDictionary<TKey, TValue> segmentedDictionary = Interlocked.Exchange<SegmentedDictionary<TKey, TValue>>(Unsafe.AsRef<SegmentedDictionary<TKey, TValue>>(in location._dictionary), value._dictionary);
				if (segmentedDictionary == null)
				{
					return default(ImmutableSegmentedDictionary<TKey, TValue>);
				}
				return new ImmutableSegmentedDictionary<TKey, TValue>(segmentedDictionary);
			}

			// Token: 0x06003DCA RID: 15818 RVA: 0x000AE5C4 File Offset: 0x000AC7C4
			[return: Nullable(new byte[] { 0, 1, 1 })]
			internal static ImmutableSegmentedDictionary<TKey, TValue> InterlockedCompareExchange([Nullable(new byte[] { 0, 1, 1 })] ref ImmutableSegmentedDictionary<TKey, TValue> location, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> value, [Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> comparand)
			{
				SegmentedDictionary<TKey, TValue> segmentedDictionary = Interlocked.CompareExchange<SegmentedDictionary<TKey, TValue>>(Unsafe.AsRef<SegmentedDictionary<TKey, TValue>>(in location._dictionary), value._dictionary, comparand._dictionary);
				if (segmentedDictionary == null)
				{
					return default(ImmutableSegmentedDictionary<TKey, TValue>);
				}
				return new ImmutableSegmentedDictionary<TKey, TValue>(segmentedDictionary);
			}
		}

		// Token: 0x02000752 RID: 1874
		[Nullable(0)]
		public readonly struct ValueCollection : IReadOnlyCollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection<TValue>, ICollection
		{
			// Token: 0x06003DCB RID: 15819 RVA: 0x000AE601 File Offset: 0x000AC801
			internal ValueCollection([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue> dictionary)
			{
				this._dictionary = dictionary;
			}

			// Token: 0x17001115 RID: 4373
			// (get) Token: 0x06003DCC RID: 15820 RVA: 0x000AE60A File Offset: 0x000AC80A
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x17001116 RID: 4374
			// (get) Token: 0x06003DCD RID: 15821 RVA: 0x000AE617 File Offset: 0x000AC817
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001117 RID: 4375
			// (get) Token: 0x06003DCE RID: 15822 RVA: 0x000AE61A File Offset: 0x000AC81A
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001118 RID: 4376
			// (get) Token: 0x06003DCF RID: 15823 RVA: 0x000AE61D File Offset: 0x000AC81D
			object ICollection.SyncRoot
			{
				get
				{
					return this._dictionary.SyncRoot;
				}
			}

			// Token: 0x06003DD0 RID: 15824 RVA: 0x000AE62F File Offset: 0x000AC82F
			[NullableContext(0)]
			public ImmutableSegmentedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new ImmutableSegmentedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary.GetEnumerator());
			}

			// Token: 0x06003DD1 RID: 15825 RVA: 0x000AE641 File Offset: 0x000AC841
			public bool Contains(TValue item)
			{
				return this._dictionary.ContainsValue(item);
			}

			// Token: 0x06003DD2 RID: 15826 RVA: 0x000AE64F File Offset: 0x000AC84F
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003DD3 RID: 15827 RVA: 0x000AE65C File Offset: 0x000AC85C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003DD4 RID: 15828 RVA: 0x000AE669 File Offset: 0x000AC869
			void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex)
			{
				this._dictionary._dictionary.Values.CopyTo(array, arrayIndex);
			}

			// Token: 0x06003DD5 RID: 15829 RVA: 0x000AE682 File Offset: 0x000AC882
			void ICollection.CopyTo(Array array, int index)
			{
				this._dictionary._dictionary.Values.CopyTo(array, index);
			}

			// Token: 0x06003DD6 RID: 15830 RVA: 0x000AE69B File Offset: 0x000AC89B
			void ICollection<TValue>.Add(TValue item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003DD7 RID: 15831 RVA: 0x000AE6A2 File Offset: 0x000AC8A2
			void ICollection<TValue>.Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003DD8 RID: 15832 RVA: 0x000AE6A9 File Offset: 0x000AC8A9
			bool ICollection<TValue>.Remove(TValue item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04001CF5 RID: 7413
			[Nullable(new byte[] { 0, 1, 1 })]
			private readonly ImmutableSegmentedDictionary<TKey, TValue> _dictionary;

			// Token: 0x020007FA RID: 2042
			[NullableContext(0)]
			public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
			{
				// Token: 0x060040C4 RID: 16580 RVA: 0x000B4864 File Offset: 0x000B2A64
				internal Enumerator([Nullable(new byte[] { 0, 1, 1 })] ImmutableSegmentedDictionary<TKey, TValue>.Enumerator enumerator)
				{
					this._enumerator = enumerator;
				}

				// Token: 0x170011C0 RID: 4544
				// (get) Token: 0x060040C5 RID: 16581 RVA: 0x000B4870 File Offset: 0x000B2A70
				[Nullable(1)]
				public TValue Current
				{
					[NullableContext(1)]
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
						return keyValuePair.Value;
					}
				}

				// Token: 0x170011C1 RID: 4545
				// (get) Token: 0x060040C6 RID: 16582 RVA: 0x000B4890 File Offset: 0x000B2A90
				[Nullable(2)]
				object IEnumerator.Current
				{
					[NullableContext(2)]
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060040C7 RID: 16583 RVA: 0x000B489D File Offset: 0x000B2A9D
				public void Dispose()
				{
					this._enumerator.Dispose();
				}

				// Token: 0x060040C8 RID: 16584 RVA: 0x000B48AA File Offset: 0x000B2AAA
				public bool MoveNext()
				{
					return this._enumerator.MoveNext();
				}

				// Token: 0x060040C9 RID: 16585 RVA: 0x000B48B7 File Offset: 0x000B2AB7
				public void Reset()
				{
					this._enumerator.Reset();
				}

				// Token: 0x04001E73 RID: 7795
				[Nullable(new byte[] { 0, 1, 1 })]
				private ImmutableSegmentedDictionary<TKey, TValue>.Enumerator _enumerator;
			}
		}
	}
}
