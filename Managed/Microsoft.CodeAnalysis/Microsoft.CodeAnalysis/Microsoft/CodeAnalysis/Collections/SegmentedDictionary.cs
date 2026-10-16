using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Collections.Internal;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x020004A7 RID: 1191
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class SegmentedDictionary<TKey, [Nullable(2)] TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06002F75 RID: 12149 RVA: 0x000857DB File Offset: 0x000839DB
		public SegmentedDictionary()
			: this(0, null)
		{
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000857E5 File Offset: 0x000839E5
		public SegmentedDictionary(int capacity)
			: this(capacity, null)
		{
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000857EF File Offset: 0x000839EF
		public SegmentedDictionary([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> comparer)
			: this(0, comparer)
		{
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000857FC File Offset: 0x000839FC
		public SegmentedDictionary(int capacity, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> comparer)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
			if (comparer != null && comparer != EqualityComparer<TKey>.Default)
			{
				this._comparer = comparer;
			}
			if (this._comparer == null)
			{
				this._comparer = EqualityComparer<TKey>.Default;
			}
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x0008584A File Offset: 0x00083A4A
		public SegmentedDictionary(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x00085854 File Offset: 0x00083A54
		public SegmentedDictionary(IDictionary<TKey, TValue> dictionary, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> comparer)
			: this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			if (dictionary.GetType() == typeof(SegmentedDictionary<TKey, TValue>))
			{
				SegmentedDictionary<TKey, TValue> segmentedDictionary = (SegmentedDictionary<TKey, TValue>)dictionary;
				int count = segmentedDictionary._count;
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = segmentedDictionary._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i]._next >= -1)
					{
						this.Add(entries[i]._key, entries[i]._value);
					}
				}
				return;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x00085930 File Offset: 0x00083B30
		public SegmentedDictionary([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> collection)
			: this(collection, null)
		{
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x0008593C File Offset: 0x00083B3C
		public SegmentedDictionary([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> collection, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> comparer)
		{
			ICollection<KeyValuePair<TKey, TValue>> collection2 = collection as ICollection<KeyValuePair<TKey, TValue>>;
			this..ctor((collection2 != null) ? collection2.Count : 0, comparer);
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x000859B4 File Offset: 0x00083BB4
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				return this._comparer ?? EqualityComparer<TKey>.Default;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06002F7E RID: 12158 RVA: 0x000859C5 File Offset: 0x00083BC5
		public int Count
		{
			get
			{
				return this._count - this._freeCount;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000859D4 File Offset: 0x00083BD4
		[Nullable(new byte[] { 1, 0, 0 })]
		public SegmentedDictionary<TKey, TValue>.KeyCollection Keys
		{
			[return: Nullable(new byte[] { 1, 0, 0 })]
			get
			{
				SegmentedDictionary<TKey, TValue>.KeyCollection keyCollection;
				if ((keyCollection = this._keys) == null)
				{
					keyCollection = (this._keys = new SegmentedDictionary<TKey, TValue>.KeyCollection(this));
				}
				return keyCollection;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x000859FA File Offset: 0x00083BFA
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x00085A02 File Offset: 0x00083C02
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x00085A0C File Offset: 0x00083C0C
		[Nullable(new byte[] { 1, 0, 0 })]
		public SegmentedDictionary<TKey, TValue>.ValueCollection Values
		{
			[return: Nullable(new byte[] { 1, 0, 0 })]
			get
			{
				SegmentedDictionary<TKey, TValue>.ValueCollection valueCollection;
				if ((valueCollection = this._values) == null)
				{
					valueCollection = (this._values = new SegmentedDictionary<TKey, TValue>.ValueCollection(this));
				}
				return valueCollection;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x00085A32 File Offset: 0x00083C32
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06002F84 RID: 12164 RVA: 0x00085A3A File Offset: 0x00083C3A
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		public TValue this[TKey key]
		{
			get
			{
				ref TValue ptr = ref this.FindValue(key);
				if (!RoslynUnsafe.IsNullRef<TValue>(ref ptr))
				{
					return ptr;
				}
				ThrowHelper.ThrowKeyNotFoundException<TKey>(key);
				return default(TValue);
			}
			set
			{
				this.TryInsert(key, value, InsertionBehavior.OverwriteExisting);
			}
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x00085A83 File Offset: 0x00083C83
		public void Add(TKey key, TValue value)
		{
			this.TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x00085A8F File Offset: 0x00083C8F
		void ICollection<KeyValuePair<TKey, TValue>>.Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x00085AA8 File Offset: 0x00083CA8
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> keyValuePair)
		{
			ref TValue ptr = ref this.FindValue(keyValuePair.Key);
			return !RoslynUnsafe.IsNullRef<TValue>(ref ptr) && EqualityComparer<TValue>.Default.Equals(ptr, keyValuePair.Value);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x00085AE8 File Offset: 0x00083CE8
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> keyValuePair)
		{
			ref TValue ptr = ref this.FindValue(keyValuePair.Key);
			if (!RoslynUnsafe.IsNullRef<TValue>(ref ptr) && EqualityComparer<TValue>.Default.Equals(ptr, keyValuePair.Value))
			{
				this.Remove(keyValuePair.Key);
				return true;
			}
			return false;
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x00085B38 File Offset: 0x00083D38
		public void Clear()
		{
			int count = this._count;
			if (count > 0)
			{
				SegmentedArray.Clear<int>(this._buckets, 0, this._buckets.Length);
				this._count = 0;
				this._freeList = -1;
				this._freeCount = 0;
				SegmentedArray.Clear<SegmentedDictionary<TKey, TValue>.Entry>(this._entries, 0, count);
			}
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x00085B89 File Offset: 0x00083D89
		public bool ContainsKey(TKey key)
		{
			return !RoslynUnsafe.IsNullRef<TValue>(this.FindValue(key));
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x00085B9C File Offset: 0x00083D9C
		public bool ContainsValue(TValue value)
		{
			SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._entries;
			if (value == null)
			{
				for (int i = 0; i < this._count; i++)
				{
					if (entries[i]._next >= -1 && entries[i]._value == null)
					{
						return true;
					}
				}
			}
			else
			{
				EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
				for (int j = 0; j < this._count; j++)
				{
					if (entries[j]._next >= -1 && @default.Equals(entries[j]._value, value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x00085C38 File Offset: 0x00083E38
		private void CopyTo([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index > array.Length)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			int count = this._count;
			SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._entries;
			for (int i = 0; i < count; i++)
			{
				if (entries[i]._next >= -1)
				{
					array[index++] = new KeyValuePair<TKey, TValue>(entries[i]._key, entries[i]._value);
				}
			}
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x00085CC1 File Offset: 0x00083EC1
		[NullableContext(0)]
		public SegmentedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new SegmentedDictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x00085CCA File Offset: 0x00083ECA
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new SegmentedDictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x00085CD8 File Offset: 0x00083ED8
		private unsafe ref TValue FindValue(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			ref SegmentedDictionary<TKey, TValue>.Entry ptr = ref RoslynUnsafe.NullRef<SegmentedDictionary<TKey, TValue>.Entry>();
			if (this._buckets.Length > 0)
			{
				IEqualityComparer<TKey> comparer = this._comparer;
				uint hashCode = (uint)comparer.GetHashCode(key);
				int i = *this.GetBucket(hashCode);
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._entries;
				uint num = 0U;
				i--;
				while (i < entries.Length)
				{
					ptr = entries[i];
					if (ptr._hashCode != hashCode || !comparer.Equals(ptr._key, key))
					{
						i = ptr._next;
						num += 1U;
						if (num <= (uint)entries.Length)
						{
							continue;
						}
						ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
					return ref ptr._value;
				}
			}
			IL_00A6:
			return RoslynUnsafe.NullRef<TValue>();
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x00085D94 File Offset: 0x00083F94
		private int Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			SegmentedArray<int> segmentedArray = new SegmentedArray<int>(prime);
			SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> segmentedArray2 = new SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry>(prime);
			this._freeList = -1;
			this._fastModMultiplier = HashHelpers.GetFastModMultiplier((uint)prime);
			this._buckets = segmentedArray;
			this._entries = segmentedArray2;
			return prime;
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x00085DDC File Offset: 0x00083FDC
		private bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets.Length == 0)
			{
				this.Initialize(0);
			}
			SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> segmentedArray = this._entries;
			IEqualityComparer<TKey> comparer = this._comparer;
			uint hashCode = (uint)comparer.GetHashCode(key);
			uint num = 0U;
			ref int ptr = ref this.GetBucket(hashCode);
			int i = ptr - 1;
			while (i < segmentedArray.Length)
			{
				if (segmentedArray[i]._hashCode == hashCode && comparer.Equals(segmentedArray[i]._key, key))
				{
					if (behavior == InsertionBehavior.OverwriteExisting)
					{
						segmentedArray[i]._value = value;
						return true;
					}
					if (behavior == InsertionBehavior.ThrowOnExisting)
					{
						ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException<TKey>(key);
					}
					return false;
				}
				else
				{
					i = segmentedArray[i]._next;
					num += 1U;
					if (num > (uint)segmentedArray.Length)
					{
						ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
				}
			}
			int num2;
			if (this._freeCount > 0)
			{
				num2 = this._freeList;
				this._freeList = -3 - segmentedArray[this._freeList]._next;
				this._freeCount--;
			}
			else
			{
				int count = this._count;
				if (count == segmentedArray.Length)
				{
					this.Resize();
					ptr = this.GetBucket(hashCode);
				}
				num2 = count;
				this._count = count + 1;
				segmentedArray = this._entries;
			}
			ref SegmentedDictionary<TKey, TValue>.Entry ptr2 = ref segmentedArray[num2];
			ptr2._hashCode = hashCode;
			ptr2._next = ptr - 1;
			ptr2._key = key;
			ptr2._value = value;
			ptr = num2 + 1;
			this._version++;
			return true;
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x00085F5E File Offset: 0x0008415E
		private void Resize()
		{
			this.Resize(HashHelpers.ExpandPrime(this._count));
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x00085F74 File Offset: 0x00084174
		private void Resize(int newSize)
		{
			SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> segmentedArray = new SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry>(newSize);
			int count = this._count;
			SegmentedArray.Copy<SegmentedDictionary<TKey, TValue>.Entry>(this._entries, segmentedArray, count);
			this._buckets = new SegmentedArray<int>(newSize);
			this._fastModMultiplier = HashHelpers.GetFastModMultiplier((uint)newSize);
			for (int i = 0; i < count; i++)
			{
				if (segmentedArray[i]._next >= -1)
				{
					ref int bucket = ref this.GetBucket(segmentedArray[i]._hashCode);
					segmentedArray[i]._next = bucket - 1;
					bucket = i + 1;
				}
			}
			this._entries = segmentedArray;
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x00086004 File Offset: 0x00084204
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets.Length > 0)
			{
				uint num = 0U;
				IEqualityComparer<TKey> comparer = this._comparer;
				uint num2 = (uint)((comparer != null) ? comparer.GetHashCode(key) : key.GetHashCode());
				ref int bucket = ref this.GetBucket(num2);
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._entries;
				int num3 = -1;
				int i = bucket - 1;
				while (i >= 0)
				{
					ref SegmentedDictionary<TKey, TValue>.Entry ptr = ref entries[i];
					if (ptr._hashCode == num2)
					{
						IEqualityComparer<TKey> comparer2 = this._comparer;
						if ((comparer2 != null) ? comparer2.Equals(ptr._key, key) : EqualityComparer<TKey>.Default.Equals(ptr._key, key))
						{
							if (num3 < 0)
							{
								bucket = ptr._next + 1;
							}
							else
							{
								entries[num3]._next = ptr._next;
							}
							ptr._next = -3 - this._freeList;
							ptr._key = default(TKey);
							ptr._value = default(TValue);
							this._freeList = i;
							this._freeCount++;
							return true;
						}
					}
					num3 = i;
					i = ptr._next;
					num += 1U;
					if (num > (uint)entries.Length)
					{
						ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
				}
			}
			return false;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x00086148 File Offset: 0x00084348
		public bool Remove(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets.Length > 0)
			{
				uint num = 0U;
				IEqualityComparer<TKey> comparer = this._comparer;
				uint num2 = (uint)((comparer != null) ? comparer.GetHashCode(key) : key.GetHashCode());
				ref int bucket = ref this.GetBucket(num2);
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._entries;
				int num3 = -1;
				int i = bucket - 1;
				while (i >= 0)
				{
					ref SegmentedDictionary<TKey, TValue>.Entry ptr = ref entries[i];
					if (ptr._hashCode == num2)
					{
						IEqualityComparer<TKey> comparer2 = this._comparer;
						if ((comparer2 != null) ? comparer2.Equals(ptr._key, key) : EqualityComparer<TKey>.Default.Equals(ptr._key, key))
						{
							if (num3 < 0)
							{
								bucket = ptr._next + 1;
							}
							else
							{
								entries[num3]._next = ptr._next;
							}
							value = ptr._value;
							ptr._next = -3 - this._freeList;
							ptr._key = default(TKey);
							ptr._value = default(TValue);
							this._freeList = i;
							this._freeCount++;
							return true;
						}
					}
					num3 = i;
					i = ptr._next;
					num += 1U;
					if (num > (uint)entries.Length)
					{
						ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000862A0 File Offset: 0x000844A0
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			ref TValue ptr = ref this.FindValue(key);
			if (!RoslynUnsafe.IsNullRef<TValue>(ref ptr))
			{
				value = ptr;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000862D3 File Offset: 0x000844D3
		public bool TryAdd(TKey key, TValue value)
		{
			return this.TryInsert(key, value, InsertionBehavior.None);
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x000862DE File Offset: 0x000844DE
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000862E1 File Offset: 0x000844E1
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000862EC File Offset: 0x000844EC
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index > array.Length)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._entries;
				for (int i = 0; i < this._count; i++)
				{
					if (entries[i]._next >= -1)
					{
						array3[index++] = new DictionaryEntry(entries[i]._key, entries[i]._value);
					}
				}
				return;
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			try
			{
				int count = this._count;
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries2 = this._entries;
				for (int j = 0; j < count; j++)
				{
					if (entries2[j]._next >= -1)
					{
						array4[index++] = new KeyValuePair<TKey, TValue>(entries2[j]._key, entries2[j]._value);
					}
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x0008644C File Offset: 0x0008464C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SegmentedDictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x0008645C File Offset: 0x0008465C
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			int length = this._entries.Length;
			if (length >= capacity)
			{
				return length;
			}
			this._version++;
			if (this._buckets.Length == 0)
			{
				return this.Initialize(capacity);
			}
			int prime = HashHelpers.GetPrime(capacity);
			this.Resize(prime);
			return prime;
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000864B7 File Offset: 0x000846B7
		public void TrimExcess()
		{
			this.TrimExcess(this.Count);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000864C8 File Offset: 0x000846C8
		public unsafe void TrimExcess(int capacity)
		{
			if (capacity < this.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			int prime = HashHelpers.GetPrime(capacity);
			SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._entries;
			int length = entries.Length;
			if (prime >= length)
			{
				return;
			}
			int count = this._count;
			this._version++;
			this.Initialize(prime);
			SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries2 = this._entries;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				uint hashCode = entries[i]._hashCode;
				if (entries[i]._next >= -1)
				{
					ref SegmentedDictionary<TKey, TValue>.Entry ptr = ref entries2[num];
					ptr = *entries[i];
					ref int bucket = ref this.GetBucket(hashCode);
					ptr._next = bucket - 1;
					bucket = num + 1;
					num++;
				}
			}
			this._count = num;
			this._freeCount = 0;
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000865A5 File Offset: 0x000847A5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000865A8 File Offset: 0x000847A8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000865AB File Offset: 0x000847AB
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x000865AE File Offset: 0x000847AE
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x000865B1 File Offset: 0x000847B1
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000865B9 File Offset: 0x000847B9
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000865C4 File Offset: 0x000847C4
		// (set) Token: 0x06002FA8 RID: 12200 RVA: 0x000865FC File Offset: 0x000847FC
		[Nullable(2)]
		object IDictionary.Item
		{
			[return: Nullable(2)]
			get
			{
				if (SegmentedDictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					ref TValue ptr = ref this.FindValue((TKey)((object)key));
					if (!RoslynUnsafe.IsNullRef<TValue>(ref ptr))
					{
						return ptr;
					}
				}
				return null;
			}
			[param: Nullable(2)]
			set
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
				try
				{
					TKey tkey = (TKey)((object)key);
					try
					{
						this[tkey] = (TValue)((object)value);
					}
					catch (InvalidCastException)
					{
						ThrowHelper.ThrowWrongValueTypeArgumentException<object>(value, typeof(TValue));
					}
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongKeyTypeArgumentException<object>(key, typeof(TKey));
				}
			}
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x00086674 File Offset: 0x00084874
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x00086688 File Offset: 0x00084888
		void IDictionary.Add(object key, [Nullable(2)] object value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
			try
			{
				TKey tkey = (TKey)((object)key);
				try
				{
					this.Add(tkey, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException<object>(value, typeof(TValue));
				}
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongKeyTypeArgumentException<object>(key, typeof(TKey));
			}
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x00086700 File Offset: 0x00084900
		bool IDictionary.Contains(object key)
		{
			return SegmentedDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x00086718 File Offset: 0x00084918
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SegmentedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x00086726 File Offset: 0x00084926
		void IDictionary.Remove(object key)
		{
			if (SegmentedDictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x00086740 File Offset: 0x00084940
		[MethodImpl(256)]
		private ref int GetBucket(uint hashCode)
		{
			SegmentedArray<int> buckets = this._buckets;
			return buckets[(int)HashHelpers.FastMod(hashCode, (uint)buckets.Length, this._fastModMultiplier)];
		}

		// Token: 0x0400128C RID: 4748
		private const bool SupportsComparerDevirtualization = false;

		// Token: 0x0400128D RID: 4749
		[Nullable(0)]
		private SegmentedArray<int> _buckets;

		// Token: 0x0400128E RID: 4750
		[Nullable(0)]
		private SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> _entries;

		// Token: 0x0400128F RID: 4751
		private ulong _fastModMultiplier;

		// Token: 0x04001290 RID: 4752
		private int _count;

		// Token: 0x04001291 RID: 4753
		private int _freeList;

		// Token: 0x04001292 RID: 4754
		private int _freeCount;

		// Token: 0x04001293 RID: 4755
		private int _version;

		// Token: 0x04001294 RID: 4756
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x04001295 RID: 4757
		[Nullable(new byte[] { 2, 0, 0 })]
		private SegmentedDictionary<TKey, TValue>.KeyCollection _keys;

		// Token: 0x04001296 RID: 4758
		[Nullable(new byte[] { 2, 0, 0 })]
		private SegmentedDictionary<TKey, TValue>.ValueCollection _values;

		// Token: 0x04001297 RID: 4759
		private const int StartOfFreeList = -3;

		// Token: 0x0200075B RID: 1883
		[Nullable(0)]
		private struct Entry
		{
			// Token: 0x04001D1B RID: 7451
			public uint _hashCode;

			// Token: 0x04001D1C RID: 7452
			public int _next;

			// Token: 0x04001D1D RID: 7453
			public TKey _key;

			// Token: 0x04001D1E RID: 7454
			public TValue _value;
		}

		// Token: 0x0200075C RID: 1884
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable, IDictionaryEnumerator
		{
			// Token: 0x06003DF6 RID: 15862 RVA: 0x000AED56 File Offset: 0x000ACF56
			[NullableContext(1)]
			internal Enumerator(SegmentedDictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this._dictionary = dictionary;
				this._version = dictionary._version;
				this._index = 0;
				this._getEnumeratorRetType = getEnumeratorRetType;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x06003DF7 RID: 15863 RVA: 0x000AED88 File Offset: 0x000ACF88
			public bool MoveNext()
			{
				if (this._version != this._dictionary._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				while (this._index < this._dictionary._count)
				{
					SegmentedDictionary<TKey, TValue> dictionary = this._dictionary;
					int index = this._index;
					this._index = index + 1;
					ref SegmentedDictionary<TKey, TValue>.Entry ptr = ref dictionary._entries[index];
					if (ptr._next >= -1)
					{
						this._current = new KeyValuePair<TKey, TValue>(ptr._key, ptr._value);
						return true;
					}
				}
				this._index = this._dictionary._count + 1;
				this._current = default(KeyValuePair<TKey, TValue>);
				return false;
			}

			// Token: 0x17001120 RID: 4384
			// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x000AEE26 File Offset: 0x000AD026
			[Nullable(new byte[] { 0, 1, 1 })]
			public KeyValuePair<TKey, TValue> Current
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					return this._current;
				}
			}

			// Token: 0x06003DF9 RID: 15865 RVA: 0x000AEE2E File Offset: 0x000AD02E
			public void Dispose()
			{
			}

			// Token: 0x17001121 RID: 4385
			// (get) Token: 0x06003DFA RID: 15866 RVA: 0x000AEE30 File Offset: 0x000AD030
			[Nullable(2)]
			object IEnumerator.Current
			{
				[NullableContext(2)]
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					if (this._getEnumeratorRetType == 1)
					{
						return new DictionaryEntry(this._current.Key, this._current.Value);
					}
					return new KeyValuePair<TKey, TValue>(this._current.Key, this._current.Value);
				}
			}

			// Token: 0x06003DFB RID: 15867 RVA: 0x000AEEB3 File Offset: 0x000AD0B3
			void IEnumerator.Reset()
			{
				if (this._version != this._dictionary._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = 0;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x17001122 RID: 4386
			// (get) Token: 0x06003DFC RID: 15868 RVA: 0x000AEEE0 File Offset: 0x000AD0E0
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return new DictionaryEntry(this._current.Key, this._current.Value);
				}
			}

			// Token: 0x17001123 RID: 4387
			// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000AEF34 File Offset: 0x000AD134
			[Nullable(1)]
			object IDictionaryEnumerator.Key
			{
				[NullableContext(1)]
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current.Key;
				}
			}

			// Token: 0x17001124 RID: 4388
			// (get) Token: 0x06003DFE RID: 15870 RVA: 0x000AEF68 File Offset: 0x000AD168
			[Nullable(2)]
			object IDictionaryEnumerator.Value
			{
				[NullableContext(2)]
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current.Value;
				}
			}

			// Token: 0x04001D1F RID: 7455
			[Nullable(1)]
			private readonly SegmentedDictionary<TKey, TValue> _dictionary;

			// Token: 0x04001D20 RID: 7456
			private readonly int _version;

			// Token: 0x04001D21 RID: 7457
			private int _index;

			// Token: 0x04001D22 RID: 7458
			[Nullable(new byte[] { 0, 1, 1 })]
			private KeyValuePair<TKey, TValue> _current;

			// Token: 0x04001D23 RID: 7459
			private readonly int _getEnumeratorRetType;

			// Token: 0x04001D24 RID: 7460
			internal const int DictEntry = 1;

			// Token: 0x04001D25 RID: 7461
			internal const int KeyValuePair = 2;
		}

		// Token: 0x0200075D RID: 1885
		[Nullable(0)]
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x06003DFF RID: 15871 RVA: 0x000AEF9C File Offset: 0x000AD19C
			public KeyCollection(SegmentedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this._dictionary = dictionary;
			}

			// Token: 0x06003E00 RID: 15872 RVA: 0x000AEFB4 File Offset: 0x000AD1B4
			[NullableContext(0)]
			public SegmentedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new SegmentedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06003E01 RID: 15873 RVA: 0x000AEFC4 File Offset: 0x000AD1C4
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this._dictionary._count;
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._dictionary._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i]._next >= -1)
					{
						array[index++] = entries[i]._key;
					}
				}
			}

			// Token: 0x17001125 RID: 4389
			// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000AF04E File Offset: 0x000AD24E
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x17001126 RID: 4390
			// (get) Token: 0x06003E03 RID: 15875 RVA: 0x000AF05B File Offset: 0x000AD25B
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003E04 RID: 15876 RVA: 0x000AF05E File Offset: 0x000AD25E
			void ICollection<TKey>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06003E05 RID: 15877 RVA: 0x000AF067 File Offset: 0x000AD267
			void ICollection<TKey>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06003E06 RID: 15878 RVA: 0x000AF070 File Offset: 0x000AD270
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this._dictionary.ContainsKey(item);
			}

			// Token: 0x06003E07 RID: 15879 RVA: 0x000AF07E File Offset: 0x000AD27E
			bool ICollection<TKey>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
				return false;
			}

			// Token: 0x06003E08 RID: 15880 RVA: 0x000AF088 File Offset: 0x000AD288
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return new SegmentedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06003E09 RID: 15881 RVA: 0x000AF09A File Offset: 0x000AD29A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SegmentedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06003E0A RID: 15882 RVA: 0x000AF0AC File Offset: 0x000AD2AC
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
				int count = this._dictionary._count;
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._dictionary._entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i]._next >= -1)
						{
							array3[index++] = entries[i]._key;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
			}

			// Token: 0x17001127 RID: 4391
			// (get) Token: 0x06003E0B RID: 15883 RVA: 0x000AF19C File Offset: 0x000AD39C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001128 RID: 4392
			// (get) Token: 0x06003E0C RID: 15884 RVA: 0x000AF19F File Offset: 0x000AD39F
			object ICollection.SyncRoot
			{
				get
				{
					return this._dictionary.SyncRoot;
				}
			}

			// Token: 0x04001D26 RID: 7462
			private readonly SegmentedDictionary<TKey, TValue> _dictionary;

			// Token: 0x020007FF RID: 2047
			[Nullable(0)]
			public struct Enumerator : IEnumerator<TKey>, IEnumerator, IDisposable
			{
				// Token: 0x060040D6 RID: 16598 RVA: 0x000B4C2B File Offset: 0x000B2E2B
				internal Enumerator(SegmentedDictionary<TKey, TValue> dictionary)
				{
					this._dictionary = dictionary;
					this._version = dictionary._version;
					this._index = 0;
					this._currentKey = default(TKey);
				}

				// Token: 0x060040D7 RID: 16599 RVA: 0x000B4C53 File Offset: 0x000B2E53
				public void Dispose()
				{
				}

				// Token: 0x060040D8 RID: 16600 RVA: 0x000B4C58 File Offset: 0x000B2E58
				public bool MoveNext()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					while (this._index < this._dictionary._count)
					{
						SegmentedDictionary<TKey, TValue> dictionary = this._dictionary;
						int index = this._index;
						this._index = index + 1;
						ref SegmentedDictionary<TKey, TValue>.Entry ptr = ref dictionary._entries[index];
						if (ptr._next >= -1)
						{
							this._currentKey = ptr._key;
							return true;
						}
					}
					this._index = this._dictionary._count + 1;
					this._currentKey = default(TKey);
					return false;
				}

				// Token: 0x170011C4 RID: 4548
				// (get) Token: 0x060040D9 RID: 16601 RVA: 0x000B4CEB File Offset: 0x000B2EEB
				public TKey Current
				{
					get
					{
						return this._currentKey;
					}
				}

				// Token: 0x170011C5 RID: 4549
				// (get) Token: 0x060040DA RID: 16602 RVA: 0x000B4CF3 File Offset: 0x000B2EF3
				[Nullable(2)]
				object IEnumerator.Current
				{
					[NullableContext(2)]
					get
					{
						if (this._index == 0 || this._index == this._dictionary._count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
						}
						return this._currentKey;
					}
				}

				// Token: 0x060040DB RID: 16603 RVA: 0x000B4D22 File Offset: 0x000B2F22
				void IEnumerator.Reset()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					this._index = 0;
					this._currentKey = default(TKey);
				}

				// Token: 0x04001E82 RID: 7810
				private readonly SegmentedDictionary<TKey, TValue> _dictionary;

				// Token: 0x04001E83 RID: 7811
				private int _index;

				// Token: 0x04001E84 RID: 7812
				private readonly int _version;

				// Token: 0x04001E85 RID: 7813
				[Nullable(2)]
				private TKey _currentKey;
			}
		}

		// Token: 0x0200075E RID: 1886
		[Nullable(0)]
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06003E0D RID: 15885 RVA: 0x000AF1AC File Offset: 0x000AD3AC
			public ValueCollection(SegmentedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this._dictionary = dictionary;
			}

			// Token: 0x06003E0E RID: 15886 RVA: 0x000AF1C4 File Offset: 0x000AD3C4
			[NullableContext(0)]
			public SegmentedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new SegmentedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06003E0F RID: 15887 RVA: 0x000AF1D4 File Offset: 0x000AD3D4
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if ((ulong)index > (ulong)((long)array.Length))
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this._dictionary._count;
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._dictionary._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i]._next >= -1)
					{
						array[index++] = entries[i]._value;
					}
				}
			}

			// Token: 0x17001129 RID: 4393
			// (get) Token: 0x06003E10 RID: 15888 RVA: 0x000AF25C File Offset: 0x000AD45C
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x1700112A RID: 4394
			// (get) Token: 0x06003E11 RID: 15889 RVA: 0x000AF269 File Offset: 0x000AD469
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003E12 RID: 15890 RVA: 0x000AF26C File Offset: 0x000AD46C
			void ICollection<TValue>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06003E13 RID: 15891 RVA: 0x000AF275 File Offset: 0x000AD475
			bool ICollection<TValue>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
				return false;
			}

			// Token: 0x06003E14 RID: 15892 RVA: 0x000AF27F File Offset: 0x000AD47F
			void ICollection<TValue>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06003E15 RID: 15893 RVA: 0x000AF288 File Offset: 0x000AD488
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this._dictionary.ContainsValue(item);
			}

			// Token: 0x06003E16 RID: 15894 RVA: 0x000AF296 File Offset: 0x000AD496
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return new SegmentedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06003E17 RID: 15895 RVA: 0x000AF2A8 File Offset: 0x000AD4A8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SegmentedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06003E18 RID: 15896 RVA: 0x000AF2BC File Offset: 0x000AD4BC
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
				int count = this._dictionary._count;
				SegmentedArray<SegmentedDictionary<TKey, TValue>.Entry> entries = this._dictionary._entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i]._next >= -1)
						{
							array3[index++] = entries[i]._value;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
			}

			// Token: 0x1700112B RID: 4395
			// (get) Token: 0x06003E19 RID: 15897 RVA: 0x000AF3AC File Offset: 0x000AD5AC
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700112C RID: 4396
			// (get) Token: 0x06003E1A RID: 15898 RVA: 0x000AF3AF File Offset: 0x000AD5AF
			object ICollection.SyncRoot
			{
				get
				{
					return this._dictionary.SyncRoot;
				}
			}

			// Token: 0x04001D27 RID: 7463
			private readonly SegmentedDictionary<TKey, TValue> _dictionary;

			// Token: 0x02000800 RID: 2048
			[Nullable(0)]
			public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
			{
				// Token: 0x060040DC RID: 16604 RVA: 0x000B4D4F File Offset: 0x000B2F4F
				internal Enumerator(SegmentedDictionary<TKey, TValue> dictionary)
				{
					this._dictionary = dictionary;
					this._version = dictionary._version;
					this._index = 0;
					this._currentValue = default(TValue);
				}

				// Token: 0x060040DD RID: 16605 RVA: 0x000B4D77 File Offset: 0x000B2F77
				public void Dispose()
				{
				}

				// Token: 0x060040DE RID: 16606 RVA: 0x000B4D7C File Offset: 0x000B2F7C
				public bool MoveNext()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					while (this._index < this._dictionary._count)
					{
						SegmentedDictionary<TKey, TValue> dictionary = this._dictionary;
						int index = this._index;
						this._index = index + 1;
						ref SegmentedDictionary<TKey, TValue>.Entry ptr = ref dictionary._entries[index];
						if (ptr._next >= -1)
						{
							this._currentValue = ptr._value;
							return true;
						}
					}
					this._index = this._dictionary._count + 1;
					this._currentValue = default(TValue);
					return false;
				}

				// Token: 0x170011C6 RID: 4550
				// (get) Token: 0x060040DF RID: 16607 RVA: 0x000B4E0F File Offset: 0x000B300F
				public TValue Current
				{
					get
					{
						return this._currentValue;
					}
				}

				// Token: 0x170011C7 RID: 4551
				// (get) Token: 0x060040E0 RID: 16608 RVA: 0x000B4E17 File Offset: 0x000B3017
				[Nullable(2)]
				object IEnumerator.Current
				{
					[NullableContext(2)]
					get
					{
						if (this._index == 0 || this._index == this._dictionary._count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
						}
						return this._currentValue;
					}
				}

				// Token: 0x060040E1 RID: 16609 RVA: 0x000B4E46 File Offset: 0x000B3046
				void IEnumerator.Reset()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					this._index = 0;
					this._currentValue = default(TValue);
				}

				// Token: 0x04001E86 RID: 7814
				private readonly SegmentedDictionary<TKey, TValue> _dictionary;

				// Token: 0x04001E87 RID: 7815
				private int _index;

				// Token: 0x04001E88 RID: 7816
				private readonly int _version;

				// Token: 0x04001E89 RID: 7817
				[Nullable(2)]
				private TValue _currentValue;
			}
		}
	}
}
