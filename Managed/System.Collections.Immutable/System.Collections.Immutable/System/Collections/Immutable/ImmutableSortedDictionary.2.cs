using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	/// <summary>Represents an immutable sorted dictionary.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	/// <typeparam name="TKey">The type of the key contained in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the value contained in the dictionary.</typeparam>
	// Token: 0x02000039 RID: 57
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	public sealed class ImmutableSortedDictionary<TKey, [Nullable(2)] TValue> : IImmutableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISortKeyCollection<TKey>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000079F1 File Offset: 0x00005BF1
		internal ImmutableSortedDictionary([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer = null, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer = null)
		{
			this._keyComparer = keyComparer ?? Comparer<TKey>.Default;
			this._valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
			this._root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007A24 File Offset: 0x00005C24
		private ImmutableSortedDictionary(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
			Requires.Range(count >= 0, "count", null);
			Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			root.Freeze();
			this._root = root;
			this._count = count;
			this._keyComparer = keyComparer;
			this._valueComparer = valueComparer;
		}

		/// <summary>Retrieves an empty immutable sorted dictionary that has the same ordering and key/value comparison rules as this dictionary instance.</summary>
		/// <returns>An empty dictionary with equivalent ordering and key/value comparison rules.</returns>
		// Token: 0x060002AB RID: 683 RVA: 0x00007A8E File Offset: 0x00005C8E
		public ImmutableSortedDictionary<TKey, TValue> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(this._keyComparer, this._valueComparer);
			}
			return this;
		}

		/// <summary>Gets the value comparer used to determine whether values are equal.</summary>
		/// <returns>The value comparer used to determine whether values are equal.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00007AB5 File Offset: 0x00005CB5
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._valueComparer;
			}
		}

		/// <summary>Gets a value that indicates whether this instance of the immutable sorted dictionary is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00007ABD File Offset: 0x00005CBD
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		/// <summary>Gets the number of key/value pairs in the immutable sorted dictionary.</summary>
		/// <returns>The number of key/value pairs in the dictionary.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00007ACA File Offset: 0x00005CCA
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Gets the keys in the immutable sorted dictionary.</summary>
		/// <returns>The keys in the immutable dictionary.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00007AD2 File Offset: 0x00005CD2
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this._root.Keys;
			}
		}

		/// <summary>Gets the values in the immutable sorted dictionary.</summary>
		/// <returns>The values in the dictionary.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00007ADF File Offset: 0x00005CDF
		public IEnumerable<TValue> Values
		{
			get
			{
				return this._root.Values;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007AEC File Offset: 0x00005CEC
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00007AF4 File Offset: 0x00005CF4
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00007AFC File Offset: 0x00005CFC
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00007B04 File Offset: 0x00005D04
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the key comparer for the immutable sorted dictionary.</summary>
		/// <returns>The key comparer for the dictionary.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00007B07 File Offset: 0x00005D07
		public IComparer<TKey> KeyComparer
		{
			get
			{
				return this._keyComparer;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00007B0F File Offset: 0x00005D0F
		[Nullable(new byte[] { 1, 0, 0 })]
		internal ImmutableSortedDictionary<TKey, TValue>.Node Root
		{
			[return: Nullable(new byte[] { 1, 0, 0 })]
			get
			{
				return this._root;
			}
		}

		/// <summary>Gets the <paramref name="TValue" /> associated with the specified key.</summary>
		/// <param name="key">The key to retrieve the value for.</param>
		/// <returns>The value associated with the specified key. If no results are found, the operation throws an exception.</returns>
		// Token: 0x17000072 RID: 114
		public TValue this[TKey key]
		{
			get
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				throw new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key.ToString()));
			}
		}

		/// <summary>Returns a read-only reference to the value associated with the provided <paramref name="key" />.</summary>
		/// <param name="key">Key of the entry to be looked up.</param>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The <paramref name="key" /> is not present.</exception>
		/// <returns>A read-only reference to the value associated with the provided <paramref name="key" />.</returns>
		// Token: 0x060002B8 RID: 696 RVA: 0x00007B59 File Offset: 0x00005D59
		public readonly ref TValue ValueRef(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ValueRef(key, this._keyComparer);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00007B78 File Offset: 0x00005D78
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00007B81 File Offset: 0x00005D81
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

		/// <summary>Creates an immutable sorted dictionary with the same contents as this dictionary that can be efficiently mutated across multiple operations by using standard mutable interfaces.</summary>
		/// <returns>A collection with the same contents as this dictionary.</returns>
		// Token: 0x060002BB RID: 699 RVA: 0x00007B88 File Offset: 0x00005D88
		[return: Nullable(new byte[] { 1, 0, 0 })]
		public ImmutableSortedDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableSortedDictionary<TKey, TValue>.Builder(this);
		}

		/// <summary>Adds an element with the specified key and value to the immutable sorted dictionary.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of entry to add.</param>
		/// <exception cref="T:System.ArgumentException">The given key already exists in the dictionary but has a different value.</exception>
		/// <returns>A new immutable sorted dictionary that contains the additional key/value pair.</returns>
		// Token: 0x060002BC RID: 700 RVA: 0x00007B90 File Offset: 0x00005D90
		public ImmutableSortedDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
			return this.Wrap(node, this._count + 1);
		}

		/// <summary>Sets the specified key and value in the immutable sorted dictionary, possibly overwriting an existing value for the given key.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The key value to set.</param>
		/// <returns>A new immutable sorted dictionary that contains the specified key/value pair.</returns>
		// Token: 0x060002BD RID: 701 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public ImmutableSortedDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			bool flag2;
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
			return this.Wrap(node, flag ? this._count : (this._count + 1));
		}

		/// <summary>Sets the specified key/value pairs in the immutable sorted dictionary, possibly overwriting existing values for the keys.</summary>
		/// <param name="items">The key/value pairs to set in the dictionary. If any of the keys already exist in the dictionary, this method will overwrite their previous values.</param>
		/// <returns>An immutable dictionary that contains the specified key/value pairs.</returns>
		// Token: 0x060002BE RID: 702 RVA: 0x00007C24 File Offset: 0x00005E24
		public ImmutableSortedDictionary<TKey, TValue> SetItems([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, true, false);
		}

		/// <summary>Adds the specific key/value pairs to the immutable sorted dictionary.</summary>
		/// <param name="items">The key/value pairs to add.</param>
		/// <exception cref="T:System.ArgumentException">One of the given keys already exists in the dictionary but has a different value.</exception>
		/// <returns>A new immutable dictionary that contains the additional key/value pairs.</returns>
		// Token: 0x060002BF RID: 703 RVA: 0x00007C3A File Offset: 0x00005E3A
		public ImmutableSortedDictionary<TKey, TValue> AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, false, false);
		}

		/// <summary>Removes the element with the specified value from the immutable sorted dictionary.</summary>
		/// <param name="value">The value of the element to remove.</param>
		/// <returns>A new immutable dictionary with the specified element removed; or this instance if the specified value cannot be found in the dictionary.</returns>
		// Token: 0x060002C0 RID: 704 RVA: 0x00007C50 File Offset: 0x00005E50
		public ImmutableSortedDictionary<TKey, TValue> Remove(TKey value)
		{
			Requires.NotNullAllowStructs<TKey>(value, "value");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root.Remove(value, this._keyComparer, out flag);
			return this.Wrap(node, this._count - 1);
		}

		/// <summary>Removes the elements with the specified keys from the immutable sorted dictionary.</summary>
		/// <param name="keys">The keys of the elements to remove.</param>
		/// <returns>A new immutable dictionary with the specified keys removed; or this instance if the specified keys cannot be found in the dictionary.</returns>
		// Token: 0x060002C1 RID: 705 RVA: 0x00007C8C File Offset: 0x00005E8C
		public ImmutableSortedDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (TKey tkey in keys)
			{
				bool flag;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = node.Remove(tkey, this._keyComparer, out flag);
				if (flag)
				{
					node = node2;
					num--;
				}
			}
			return this.Wrap(node, num);
		}

		/// <summary>Gets an instance of the immutable sorted dictionary that uses the specified key and value comparers.</summary>
		/// <param name="keyComparer">The key comparer to use.</param>
		/// <param name="valueComparer">The value comparer to use.</param>
		/// <returns>An instance of the immutable dictionary that uses the given comparers.</returns>
		// Token: 0x060002C2 RID: 706 RVA: 0x00007D0C File Offset: 0x00005F0C
		public ImmutableSortedDictionary<TKey, TValue> WithComparers([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = Comparer<TKey>.Default;
			}
			if (valueComparer == null)
			{
				valueComparer = EqualityComparer<TValue>.Default;
			}
			if (keyComparer != this._keyComparer)
			{
				ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = new ImmutableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode, 0, keyComparer, valueComparer);
				return immutableSortedDictionary.AddRange(this, false, true);
			}
			if (valueComparer == this._valueComparer)
			{
				return this;
			}
			return new ImmutableSortedDictionary<TKey, TValue>(this._root, this._count, this._keyComparer, valueComparer);
		}

		/// <summary>Gets an instance of the immutable sorted dictionary that uses the specified key comparer.</summary>
		/// <param name="keyComparer">The key comparer to use.</param>
		/// <returns>An instance of the immutable dictionary that uses the given comparer.</returns>
		// Token: 0x060002C3 RID: 707 RVA: 0x00007D73 File Offset: 0x00005F73
		public ImmutableSortedDictionary<TKey, TValue> WithComparers([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._valueComparer);
		}

		/// <summary>Determines whether the immutable sorted dictionary contains an element with the specified value.</summary>
		/// <param name="value">The value to locate. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if the dictionary contains an element with the specified value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002C4 RID: 708 RVA: 0x00007D82 File Offset: 0x00005F82
		public bool ContainsValue(TValue value)
		{
			return this._root.ContainsValue(value, this._valueComparer);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00007D96 File Offset: 0x00005F96
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00007DA0 File Offset: 0x00005FA0
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00007DAA File Offset: 0x00005FAA
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00007DB3 File Offset: 0x00005FB3
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00007DBC File Offset: 0x00005FBC
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00007DC5 File Offset: 0x00005FC5
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		/// <summary>Determines whether this immutable sorted map contains the specified key.</summary>
		/// <param name="key">The key to locate.</param>
		/// <returns>
		///   <see langword="true" /> if the immutable dictionary contains the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002CB RID: 715 RVA: 0x00007DCE File Offset: 0x00005FCE
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ContainsKey(key, this._keyComparer);
		}

		/// <summary>Determines whether this immutable sorted dictionary contains the specified key/value pair.</summary>
		/// <param name="pair">The key/value pair to locate.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key/value pair is found in the dictionary; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002CC RID: 716 RVA: 0x00007DED File Offset: 0x00005FED
		public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair)
		{
			return this._root.Contains(pair, this._keyComparer, this._valueComparer);
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value will be retrieved.</param>
		/// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, contains the default value for the type of the <paramref name="value" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the dictionary contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002CD RID: 717 RVA: 0x00007E07 File Offset: 0x00006007
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.TryGetValue(key, this._keyComparer, out value);
		}

		/// <summary>Determines whether this dictionary contains a specified key.</summary>
		/// <param name="equalKey">The key to search for.</param>
		/// <param name="actualKey">The matching key located in the dictionary if found, or <c>equalkey</c> if no match is found.</param>
		/// <returns>
		///   <see langword="true" /> if a match for <paramref name="equalKey" /> is found; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002CE RID: 718 RVA: 0x00007E27 File Offset: 0x00006027
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return this._root.TryGetKey(equalKey, this._keyComparer, out actualKey);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00007E47 File Offset: 0x00006047
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00007E4E File Offset: 0x0000604E
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00007E55 File Offset: 0x00006055
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00007E5C File Offset: 0x0000605C
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00007E63 File Offset: 0x00006063
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00007E6C File Offset: 0x0000606C
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00007EF8 File Offset: 0x000060F8
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00007EFB File Offset: 0x000060FB
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00007EFE File Offset: 0x000060FE
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00007F06 File Offset: 0x00006106
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		/// <summary>Adds an element with the provided key and value to the dictionary object.</summary>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.</param>
		// Token: 0x060002D9 RID: 729 RVA: 0x00007F0E File Offset: 0x0000610E
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines whether the immutable dictionary object contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the dictionary object.</param>
		/// <returns>
		///   <see langword="true" /> if the dictionary contains an element with the key; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002DA RID: 730 RVA: 0x00007F15 File Offset: 0x00006115
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the immutable dictionary object.</summary>
		/// <returns>An enumerator object for the dictionary object.</returns>
		// Token: 0x060002DB RID: 731 RVA: 0x00007F23 File Offset: 0x00006123
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		/// <summary>Removes the element with the specified key from the immutable dictionary object.</summary>
		/// <param name="key">The key of the element to remove.</param>
		// Token: 0x060002DC RID: 732 RVA: 0x00007F35 File Offset: 0x00006135
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="key">The key of the element to be accessed.</param>
		/// <returns>Value stored under the specified key.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00007F3C File Offset: 0x0000613C
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00007F4F File Offset: 0x0000614F
		[Nullable(2)]
		object IDictionary.Item
		{
			get
			{
				return this[(TKey)((object)key)];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Clears this instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The dictionary object is read-only.</exception>
		// Token: 0x060002DF RID: 735 RVA: 0x00007F56 File Offset: 0x00006156
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Copies the elements of the dictionary to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060002E0 RID: 736 RVA: 0x00007F5D File Offset: 0x0000615D
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index, this.Count);
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00007F72 File Offset: 0x00006172
		[DebuggerBrowsable(0)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread-safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00007F75 File Offset: 0x00006175
		[DebuggerBrowsable(0)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007F78 File Offset: 0x00006178
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator object that can be used to iterate through the collection.</returns>
		// Token: 0x060002E4 RID: 740 RVA: 0x00007FA5 File Offset: 0x000061A5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through the immutable sorted dictionary.</summary>
		/// <returns>An enumerator that can be used to iterate through the dictionary.</returns>
		// Token: 0x060002E5 RID: 741 RVA: 0x00007FB2 File Offset: 0x000061B2
		[NullableContext(0)]
		public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007FBF File Offset: 0x000061BF
		private static ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, count, keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00007FE0 File Offset: 0x000061E0
		private static bool TryCastToImmutableMap(IEnumerable<KeyValuePair<TKey, TValue>> sequence, [NotNullWhen(true)] out ImmutableSortedDictionary<TKey, TValue> other)
		{
			other = sequence as ImmutableSortedDictionary<TKey, TValue>;
			if (other != null)
			{
				return true;
			}
			ImmutableSortedDictionary<TKey, TValue>.Builder builder = sequence as ImmutableSortedDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008010 File Offset: 0x00006210
		private ImmutableSortedDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision, bool avoidToSortedMap)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			if (this.IsEmpty && !avoidToSortedMap)
			{
				return this.FillFromEmpty(items, overwriteOnCollision);
			}
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				bool flag = false;
				bool flag2;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = (overwriteOnCollision ? node.SetItem(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag, out flag2) : node.Add(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag2));
				if (flag2)
				{
					node = node2;
					if (!flag)
					{
						num++;
					}
				}
			}
			return this.Wrap(node, num);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000080E8 File Offset: 0x000062E8
		private ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int adjustedCountIfDifferentRoot)
		{
			if (this._root == root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, adjustedCountIfDifferentRoot, this._keyComparer, this._valueComparer);
			}
			return this.Clear();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008118 File Offset: 0x00006318
		private ImmutableSortedDictionary<TKey, TValue> FillFromEmpty(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary;
			if (ImmutableSortedDictionary<TKey, TValue>.TryCastToImmutableMap(items, out immutableSortedDictionary))
			{
				return immutableSortedDictionary.WithComparers(this.KeyComparer, this.ValueComparer);
			}
			IDictionary<TKey, TValue> dictionary = items as IDictionary<TKey, TValue>;
			SortedDictionary<TKey, TValue> sortedDictionary;
			if (dictionary != null)
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(dictionary, this.KeyComparer);
			}
			else
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(this.KeyComparer);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					TValue tvalue;
					if (overwriteOnCollision)
					{
						sortedDictionary[keyValuePair.Key] = keyValuePair.Value;
					}
					else if (sortedDictionary.TryGetValue(keyValuePair.Key, ref tvalue))
					{
						if (!this._valueComparer.Equals(tvalue, keyValuePair.Value))
						{
							throw new ArgumentException(SR.Format(SR.DuplicateKey, keyValuePair.Key));
						}
					}
					else
					{
						sortedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			if (sortedDictionary.Count == 0)
			{
				return this;
			}
			ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromSortedDictionary(sortedDictionary);
			return new ImmutableSortedDictionary<TKey, TValue>(node, sortedDictionary.Count, this.KeyComparer, this.ValueComparer);
		}

		/// <summary>Gets an empty immutable sorted dictionary.</summary>
		// Token: 0x0400002E RID: 46
		public static readonly ImmutableSortedDictionary<TKey, TValue> Empty = new ImmutableSortedDictionary<TKey, TValue>(null, null);

		// Token: 0x0400002F RID: 47
		private readonly ImmutableSortedDictionary<TKey, TValue>.Node _root;

		// Token: 0x04000030 RID: 48
		private readonly int _count;

		// Token: 0x04000031 RID: 49
		private readonly IComparer<TKey> _keyComparer;

		// Token: 0x04000032 RID: 50
		private readonly IEqualityComparer<TValue> _valueComparer;

		/// <summary>Represents a sorted dictionary that mutates with little or no memory allocations and that can produce or build on immutable sorted dictionary instances very efficiently.  
		///
		///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x02000072 RID: 114
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedDictionaryBuilderDebuggerProxy<, >))]
		public sealed class Builder : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x0600056E RID: 1390 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
			internal Builder(ImmutableSortedDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._keyComparer = map.KeyComparer;
				this._valueComparer = map.ValueComparer;
				this._count = map.Count;
				this._immutable = map;
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000E73A File Offset: 0x0000C93A
			ICollection<TKey> IDictionary<TKey, TValue>.Keys
			{
				get
				{
					return this.Root.Keys.ToArray<TKey>(this.Count);
				}
			}

			/// <summary>Gets a strongly typed, read-only collection of elements.</summary>
			/// <returns>A strongly typed, read-only collection of elements.</returns>
			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000E752 File Offset: 0x0000C952
			public IEnumerable<TKey> Keys
			{
				get
				{
					return this.Root.Keys;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000E75F File Offset: 0x0000C95F
			ICollection<TValue> IDictionary<TKey, TValue>.Values
			{
				get
				{
					return this.Root.Values.ToArray<TValue>(this.Count);
				}
			}

			/// <summary>Gets a collection that contains the values of the immutable sorted dictionary.</summary>
			/// <returns>A collection that contains the values of the object that implements the dictionary.</returns>
			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000E777 File Offset: 0x0000C977
			public IEnumerable<TValue> Values
			{
				get
				{
					return this.Root.Values;
				}
			}

			/// <summary>Gets the number of elements in this immutable sorted dictionary.</summary>
			/// <returns>The number of elements in this dictionary.</returns>
			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000E784 File Offset: 0x0000C984
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000E78C File Offset: 0x0000C98C
			bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000E78F File Offset: 0x0000C98F
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000E797 File Offset: 0x0000C997
			// (set) Token: 0x06000577 RID: 1399 RVA: 0x0000E79F File Offset: 0x0000C99F
			[Nullable(new byte[] { 1, 0, 0 })]
			private ImmutableSortedDictionary<TKey, TValue>.Node Root
			{
				get
				{
					return this._root;
				}
				set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			/// <summary>Gets or sets the value for a specified key in the immutable sorted dictionary.</summary>
			/// <param name="key">The key to retrieve the value for.</param>
			/// <returns>The value associated with the given key.</returns>
			// Token: 0x1700010F RID: 271
			public TValue this[TKey key]
			{
				get
				{
					TValue tvalue;
					if (this.TryGetValue(key, out tvalue))
					{
						return tvalue;
					}
					throw new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key.ToString()));
				}
				set
				{
					bool flag;
					bool flag2;
					this.Root = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
					if (flag2 && !flag)
					{
						this._count++;
					}
				}
			}

			/// <summary>Returns a read-only reference to the value associated with the provided <paramref name="key" />.</summary>
			/// <param name="key">Key of the entry to be looked up.</param>
			/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The <paramref name="key" /> is not present.</exception>
			/// <returns>A read-only reference to the value associated with the provided <paramref name="key" />.</returns>
			// Token: 0x0600057A RID: 1402 RVA: 0x0000E844 File Offset: 0x0000CA44
			public readonly ref TValue ValueRef(TKey key)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				return this._root.ValueRef(key, this._keyComparer);
			}

			/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.</summary>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, <see langword="false" />.</returns>
			// Token: 0x17000110 RID: 272
			// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000E863 File Offset: 0x0000CA63
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000E866 File Offset: 0x0000CA66
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
			/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
			// Token: 0x17000112 RID: 274
			// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000E869 File Offset: 0x0000CA69
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray<TKey>(this.Count);
				}
			}

			/// <summary>Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
			/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
			// Token: 0x17000113 RID: 275
			// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000E87C File Offset: 0x0000CA7C
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray<TValue>(this.Count);
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
			// Token: 0x17000114 RID: 276
			// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000E88F File Offset: 0x0000CA8F
			[DebuggerBrowsable(0)]
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
					return this._syncRoot;
				}
			}

			/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000E8B1 File Offset: 0x0000CAB1
			[DebuggerBrowsable(0)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets the key comparer.</summary>
			/// <returns>The key comparer.</returns>
			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000581 RID: 1409 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
			// (set) Token: 0x06000582 RID: 1410 RVA: 0x0000E8BC File Offset: 0x0000CABC
			public IComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
				set
				{
					Requires.NotNull<IComparer<TKey>>(value, "value");
					if (value != this._keyComparer)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
						int num = 0;
						foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
						{
							bool flag;
							node = node.Add(keyValuePair.Key, keyValuePair.Value, value, this._valueComparer, out flag);
							if (flag)
							{
								num++;
							}
						}
						this._keyComparer = value;
						this.Root = node;
						this._count = num;
					}
				}
			}

			/// <summary>Gets or sets the value comparer.</summary>
			/// <returns>The value comparer.</returns>
			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000E95C File Offset: 0x0000CB5C
			// (set) Token: 0x06000584 RID: 1412 RVA: 0x0000E964 File Offset: 0x0000CB64
			public IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TValue>>(value, "value");
					if (value != this._valueComparer)
					{
						this._valueComparer = value;
						this._immutable = null;
					}
				}
			}

			/// <summary>Adds an element with the provided key and value to the dictionary object.</summary>
			/// <param name="key">The key of the element to add.</param>
			/// <param name="value">The value of the element to add.</param>
			// Token: 0x06000585 RID: 1413 RVA: 0x0000E988 File Offset: 0x0000CB88
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			/// <summary>Determines whether the dictionary object contains an element with the specified key.</summary>
			/// <param name="key">The key to locate.</param>
			/// <returns>
			///   <see langword="true" /> if the dictionary contains an element with the key; otherwise, <see langword="false" />.</returns>
			// Token: 0x06000586 RID: 1414 RVA: 0x0000E99C File Offset: 0x0000CB9C
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the dictionary.</summary>
			/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the dictionary.</returns>
			// Token: 0x06000587 RID: 1415 RVA: 0x0000E9AA File Offset: 0x0000CBAA
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			/// <summary>Removes the element with the specified key from the dictionary.</summary>
			/// <param name="key">The key of the element to remove.</param>
			// Token: 0x06000588 RID: 1416 RVA: 0x0000E9BC File Offset: 0x0000CBBC
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			/// <summary>Gets or sets the element with the specified key.</summary>
			/// <param name="key">The key.</param>
			/// <returns>The value associated with the specified key.</returns>
			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000E9CB File Offset: 0x0000CBCB
			// (set) Token: 0x0600058A RID: 1418 RVA: 0x0000E9DE File Offset: 0x0000CBDE
			[Nullable(2)]
			object IDictionary.Item
			{
				get
				{
					return this[(TKey)((object)key)];
				}
				set
				{
					this[(TKey)((object)key)] = (TValue)((object)value);
				}
			}

			/// <summary>Copies the elements of the dictionary to an array, starting at a particular array index.  
			///
			///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x0600058B RID: 1419 RVA: 0x0000E9F2 File Offset: 0x0000CBF2
			void ICollection.CopyTo(Array array, int index)
			{
				this.Root.CopyTo(array, index, this.Count);
			}

			/// <summary>Adds an element that has the specified key and value to the immutable sorted dictionary.</summary>
			/// <param name="key">The key of the element to add.</param>
			/// <param name="value">The value of the element to add.</param>
			// Token: 0x0600058C RID: 1420 RVA: 0x0000EA08 File Offset: 0x0000CC08
			public void Add(TKey key, TValue value)
			{
				bool flag;
				this.Root = this.Root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
				if (flag)
				{
					this._count++;
				}
			}

			/// <summary>Determines whether the immutable sorted dictionary contains an element with the specified key.</summary>
			/// <param name="key">The key to locate in the dictionary.</param>
			/// <returns>
			///   <see langword="true" /> if the dictionary contains an element with the key; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600058D RID: 1421 RVA: 0x0000EA47 File Offset: 0x0000CC47
			public bool ContainsKey(TKey key)
			{
				return this.Root.ContainsKey(key, this._keyComparer);
			}

			/// <summary>Removes the element with the specified key from the immutable sorted dictionary.</summary>
			/// <param name="key">The key of the element to remove.</param>
			/// <returns>
			///   <see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="key" /> was not found in the original dictionary.</returns>
			// Token: 0x0600058E RID: 1422 RVA: 0x0000EA5C File Offset: 0x0000CC5C
			public bool Remove(TKey key)
			{
				bool flag;
				this.Root = this.Root.Remove(key, this._keyComparer, out flag);
				if (flag)
				{
					this._count--;
				}
				return flag;
			}

			/// <summary>Gets the value associated with the specified key.</summary>
			/// <param name="key">The key whose value will be retrieved.</param>
			/// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, contains the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
			/// <returns>
			///   <see langword="true" /> if the object that implements the dictionary contains an element with the specified key; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600058F RID: 1423 RVA: 0x0000EA95 File Offset: 0x0000CC95
			public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				return this.Root.TryGetValue(key, this._keyComparer, out value);
			}

			/// <summary>Determines whether this dictionary contains a specified key.</summary>
			/// <param name="equalKey">The key to search for.</param>
			/// <param name="actualKey">The matching key located in the dictionary if found, or <c>equalkey</c> if no match is found.</param>
			/// <returns>
			///   <see langword="true" /> if a match for <paramref name="equalKey" /> is found; otherwise, <see langword="false" />.</returns>
			// Token: 0x06000590 RID: 1424 RVA: 0x0000EAAA File Offset: 0x0000CCAA
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				return this.Root.TryGetKey(equalKey, this._keyComparer, out actualKey);
			}

			/// <summary>Adds the specified item to the immutable sorted dictionary.</summary>
			/// <param name="item">The object to add to the dictionary.</param>
			// Token: 0x06000591 RID: 1425 RVA: 0x0000EACA File Offset: 0x0000CCCA
			public void Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			/// <summary>Removes all items from the immutable sorted dictionary.</summary>
			// Token: 0x06000592 RID: 1426 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
			public void Clear()
			{
				this.Root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				this._count = 0;
			}

			/// <summary>Determines whether the immutable sorted dictionary contains a specific value.</summary>
			/// <param name="item">The object to locate in the dictionary.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="item" /> is found in the dictionary; otherwise, <see langword="false" />.</returns>
			// Token: 0x06000593 RID: 1427 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
			public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return this.Root.Contains(item, this._keyComparer, this._valueComparer);
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x0000EB0E File Offset: 0x0000CD0E
			void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex, this.Count);
			}

			/// <summary>Removes the first occurrence of a specific object from the immutable sorted dictionary.</summary>
			/// <param name="item">The object to remove from the dictionary.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="item" /> was successfully removed from the dictionary; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="item" /> is not found in the dictionary.</returns>
			// Token: 0x06000595 RID: 1429 RVA: 0x0000EB23 File Offset: 0x0000CD23
			public bool Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			/// <summary>Returns an enumerator that iterates through the immutable sorted dictionary.</summary>
			/// <returns>An enumerator that can be used to iterate through the dictionary.</returns>
			// Token: 0x06000596 RID: 1430 RVA: 0x0000EB3D File Offset: 0x0000CD3D
			[return: Nullable(new byte[] { 0, 1, 1 })]
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x0000EB4B File Offset: 0x0000CD4B
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An enumerator object that can be used to iterate through the collection.</returns>
			// Token: 0x06000598 RID: 1432 RVA: 0x0000EB58 File Offset: 0x0000CD58
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Determines whether the immutable sorted dictionary contains an element with the specified value.</summary>
			/// <param name="value">The value to locate in the dictionary. The value can be <see langword="null" /> for reference types.</param>
			/// <returns>
			///   <see langword="true" /> if the immutable sorted dictionary contains an element with the specified value; otherwise, <see langword="false" />.</returns>
			// Token: 0x06000599 RID: 1433 RVA: 0x0000EB65 File Offset: 0x0000CD65
			public bool ContainsValue(TValue value)
			{
				return this._root.ContainsValue(value, this._valueComparer);
			}

			/// <summary>Adds a sequence of values to the immutable sorted dictionary.</summary>
			/// <param name="items">The items to add to the dictionary.</param>
			// Token: 0x0600059A RID: 1434 RVA: 0x0000EB7C File Offset: 0x0000CD7C
			public void AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					this.Add(keyValuePair);
				}
			}

			/// <summary>Removes any entries with keys that match those found in the specified sequence from the immutable sorted dictionary.</summary>
			/// <param name="keys">The keys for entries to remove from the dictionary.</param>
			// Token: 0x0600059B RID: 1435 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey tkey in keys)
				{
					this.Remove(tkey);
				}
			}

			/// <summary>Gets the value for a given key if a matching key exists in the dictionary; otherwise the default value.</summary>
			/// <param name="key">The key to search for.</param>
			/// <returns>The value for the key, or <c>default(TValue)</c> if no matching key was found.</returns>
			// Token: 0x0600059C RID: 1436 RVA: 0x0000EC24 File Offset: 0x0000CE24
			[return: Nullable(2)]
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			/// <summary>Gets the value for a given key if a matching key exists in the dictionary; otherwise the default value.</summary>
			/// <param name="key">The key to search for.</param>
			/// <param name="defaultValue">The default value to return if no matching key is found in the dictionary.</param>
			/// <returns>The value for the key, or <paramref name="defaultValue" /> if no matching key was found.</returns>
			// Token: 0x0600059D RID: 1437 RVA: 0x0000EC44 File Offset: 0x0000CE44
			public TValue GetValueOrDefault(TKey key, TValue defaultValue)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				return defaultValue;
			}

			/// <summary>Creates an immutable sorted dictionary based on the contents of this instance.</summary>
			/// <returns>An immutable sorted dictionary.</returns>
			// Token: 0x0600059E RID: 1438 RVA: 0x0000EC6A File Offset: 0x0000CE6A
			public ImmutableSortedDictionary<TKey, TValue> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedDictionary<TKey, TValue>.Wrap(this.Root, this._count, this._keyComparer, this._valueComparer);
				}
				return this._immutable;
			}

			// Token: 0x040000CE RID: 206
			private ImmutableSortedDictionary<TKey, TValue>.Node _root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;

			// Token: 0x040000CF RID: 207
			private IComparer<TKey> _keyComparer = Comparer<TKey>.Default;

			// Token: 0x040000D0 RID: 208
			private IEqualityComparer<TValue> _valueComparer = EqualityComparer<TValue>.Default;

			// Token: 0x040000D1 RID: 209
			private int _count;

			// Token: 0x040000D2 RID: 210
			private ImmutableSortedDictionary<TKey, TValue> _immutable;

			// Token: 0x040000D3 RID: 211
			private int _version;

			// Token: 0x040000D4 RID: 212
			private object _syncRoot;
		}

		/// <summary>Enumerates the contents of a binary tree.  
		///
		///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x02000073 RID: 115
		[NullableContext(0)]
		[EditorBrowsable(2)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable, ISecurePooledObjectUser
		{
			// Token: 0x0600059F RID: 1439 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
			internal Enumerator([Nullable(new byte[] { 1, 0, 0 })] ImmutableSortedDictionary<TKey, TValue>.Node root, [Nullable(new byte[] { 2, 0, 0 })] ImmutableSortedDictionary<TKey, TValue>.Builder builder = null)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : (-1));
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element at the current position of the enumerator.</returns>
			// Token: 0x17000119 RID: 281
			// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000ED43 File Offset: 0x0000CF43
			[Nullable(new byte[] { 0, 1, 1 })]
			public KeyValuePair<TKey, TValue> Current
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000ED64 File Offset: 0x0000CF64
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			/// <summary>The current element.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000ED6C File Offset: 0x0000CF6C
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Releases the resources used by the current instance of the <see cref="T:System.Collections.Immutable.ImmutableSortedDictionary`2.Enumerator" /> class.</summary>
			// Token: 0x060005A3 RID: 1443 RVA: 0x0000ED7C File Offset: 0x0000CF7C
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			/// <summary>Advances the enumerator to the next element of the immutable sorted dictionary.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the sorted dictionary.</returns>
			// Token: 0x060005A4 RID: 1444 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the immutable sorted dictionary.</summary>
			// Token: 0x060005A5 RID: 1445 RVA: 0x0000EE34 File Offset: 0x0000D034
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._current = null;
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x0000EE91 File Offset: 0x0000D091
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this);
				}
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x0000EEBC File Offset: 0x0000D0BC
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
			private void PushLeft(ImmutableSortedDictionary<TKey, TValue>.Node node)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>(node));
					node = node.Left;
				}
			}

			// Token: 0x040000D5 RID: 213
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator>();

			// Token: 0x040000D6 RID: 214
			private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x040000D7 RID: 215
			private readonly int _poolUserId;

			// Token: 0x040000D8 RID: 216
			private ImmutableSortedDictionary<TKey, TValue>.Node _root;

			// Token: 0x040000D9 RID: 217
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>> _stack;

			// Token: 0x040000DA RID: 218
			private ImmutableSortedDictionary<TKey, TValue>.Node _current;

			// Token: 0x040000DB RID: 219
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000074 RID: 116
		[Nullable(0)]
		[DebuggerDisplay("{_key} = {_value}")]
		internal sealed class Node : IBinaryTree<KeyValuePair<TKey, TValue>>, IBinaryTree, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x060005AA RID: 1450 RVA: 0x0000EF33 File Offset: 0x0000D133
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x0000EF44 File Offset: 0x0000D144
			private Node(TKey key, TValue value, ImmutableSortedDictionary<TKey, TValue>.Node left, ImmutableSortedDictionary<TKey, TValue>.Node right, bool frozen = false)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(right, "right");
				this._key = key;
				this._value = value;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._frozen = frozen;
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000EFB9 File Offset: 0x0000D1B9
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
			[Nullable(new byte[] { 2, 0, 1, 1 })]
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<TKey, TValue>>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000EFCC File Offset: 0x0000D1CC
			[Nullable(new byte[] { 2, 0, 1, 1 })]
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<TKey, TValue>>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000EFDC File Offset: 0x0000D1DC
			[Nullable(new byte[] { 2, 0, 0 })]
			public ImmutableSortedDictionary<TKey, TValue>.Node Left
			{
				[return: Nullable(new byte[] { 2, 0, 0 })]
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000EFEC File Offset: 0x0000D1EC
			[Nullable(new byte[] { 2, 0, 0 })]
			public ImmutableSortedDictionary<TKey, TValue>.Node Right
			{
				[return: Nullable(new byte[] { 2, 0, 0 })]
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000EFFC File Offset: 0x0000D1FC
			[Nullable(new byte[] { 0, 1, 1 })]
			public KeyValuePair<TKey, TValue> Value
			{
				[return: Nullable(new byte[] { 0, 1, 1 })]
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000F00F File Offset: 0x0000D20F
			int IBinaryTree.Count
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000F016 File Offset: 0x0000D216
			internal IEnumerable<TKey> Keys
			{
				get
				{
					return Enumerable.Select<KeyValuePair<TKey, TValue>, TKey>(this, (KeyValuePair<TKey, TValue> p) => p.Key);
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000F03D File Offset: 0x0000D23D
			internal IEnumerable<TValue> Values
			{
				get
				{
					return Enumerable.Select<KeyValuePair<TKey, TValue>, TValue>(this, (KeyValuePair<TKey, TValue> p) => p.Value);
				}
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0000F064 File Offset: 0x0000D264
			[NullableContext(0)]
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, null);
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0000F06D File Offset: 0x0000D26D
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0000F07A File Offset: 0x0000D27A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0000F087 File Offset: 0x0000D287
			[NullableContext(0)]
			internal ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator([Nullable(new byte[] { 1, 0, 0 })] ImmutableSortedDictionary<TKey, TValue>.Builder builder)
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, builder);
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0000F090 File Offset: 0x0000D290
			internal void CopyTo([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<TKey, TValue>[] array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0000F118 File Offset: 0x0000D318
			internal void CopyTo(Array array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
				}
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromSortedDictionary(SortedDictionary<TKey, TValue> dictionary)
			{
				Requires.NotNull<SortedDictionary<TKey, TValue>>(dictionary, "dictionary");
				IOrderedCollection<KeyValuePair<TKey, TValue>> orderedCollection = dictionary.AsOrderedCollection<KeyValuePair<TKey, TValue>>();
				return ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableSortedDictionary<TKey, TValue>.Node Add(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				bool flag;
				return this.SetOrAdd(key, value, keyComparer, valueComparer, false, out flag, out mutated);
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x0000F22F File Offset: 0x0000D42F
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableSortedDictionary<TKey, TValue>.Node SetItem(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				return this.SetOrAdd(key, value, keyComparer, valueComparer, true, out replacedExistingValue, out mutated);
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0000F263 File Offset: 0x0000D463
			[return: Nullable(new byte[] { 1, 0, 0 })]
			internal ImmutableSortedDictionary<TKey, TValue>.Node Remove(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return this.RemoveRecursive(key, keyComparer, out mutated);
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0000F284 File Offset: 0x0000D484
			internal readonly ref TValue ValueRef(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (node.IsEmpty)
				{
					throw new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key.ToString()));
				}
				return ref node._value;
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x0000F2DC File Offset: 0x0000D4DC
			internal bool TryGetValue(TKey key, IComparer<TKey> keyComparer, [MaybeNullWhen(false)] out TValue value)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (node.IsEmpty)
				{
					value = default(TValue);
					return false;
				}
				value = node._value;
				return true;
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x0000F328 File Offset: 0x0000D528
			internal bool TryGetKey(TKey equalKey, IComparer<TKey> keyComparer, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(equalKey, keyComparer);
				if (node.IsEmpty)
				{
					actualKey = equalKey;
					return false;
				}
				actualKey = node._key;
				return true;
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x0000F372 File Offset: 0x0000D572
			internal bool ContainsKey(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return !this.Search(key, keyComparer).IsEmpty;
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x0000F39C File Offset: 0x0000D59C
			internal bool ContainsValue(TValue value, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					if (valueComparer.Equals(value, keyValuePair.Value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x0000F408 File Offset: 0x0000D608
			internal bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNullAllowStructs<TKey>(pair.Key, "Key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(pair.Key, keyComparer);
				return !node.IsEmpty && valueComparer.Equals(node._value, pair.Value);
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x0000F468 File Offset: 0x0000D668
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x0000F490 File Offset: 0x0000D690
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x0000F518 File Offset: 0x0000D718
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = tree.Mutate(null, ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree._right));
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(node);
			}

			// Token: 0x060005CC RID: 1484 RVA: 0x0000F558 File Offset: 0x0000D758
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = tree.Mutate(ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree._left), null);
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(node);
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x0000F598 File Offset: 0x0000D798
			private static int Balance(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x0000F5BC File Offset: 0x0000D7BC
			private static bool IsRightHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) >= 2;
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x0000F5D5 File Offset: 0x0000D7D5
			private static bool IsLeftHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) <= -2;
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
			private static ImmutableSortedDictionary<TKey, TValue>.Node MakeBalanced(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (ImmutableSortedDictionary<TKey, TValue>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedDictionary<TKey, TValue>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x060005D1 RID: 1489 RVA: 0x0000F654 File Offset: 0x0000D854
			private static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromList(IOrderedCollection<KeyValuePair<TKey, TValue>> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<KeyValuePair<TKey, TValue>>>(items, "items");
				Requires.Range(start >= 0, "start", null);
				Requires.Range(length >= 0, "length", null);
				if (length == 0)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				KeyValuePair<TKey, TValue> keyValuePair = items[start + num2];
				return new ImmutableSortedDictionary<TKey, TValue>.Node(keyValuePair.Key, keyValuePair.Value, node, node2, true);
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x0000F6DC File Offset: 0x0000D8DC
			private ImmutableSortedDictionary<TKey, TValue>.Node SetOrAdd(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
			{
				replacedExistingValue = false;
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this, this, false);
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node node2 = this._right.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, node2);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node node3 = this._left.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(node3, null);
					}
				}
				else
				{
					if (valueComparer.Equals(this._value, value))
					{
						mutated = false;
						return this;
					}
					if (!overwriteExistingValue)
					{
						throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
					}
					mutated = true;
					replacedExistingValue = true;
					node = new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this._left, this._right, false);
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x0000F7CC File Offset: 0x0000D9CC
			private ImmutableSortedDictionary<TKey, TValue>.Node RemoveRecursive(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
					}
					else if (this._right.IsEmpty && !this._left.IsEmpty)
					{
						node = this._left;
					}
					else if (!this._right.IsEmpty && this._left.IsEmpty)
					{
						node = this._right;
					}
					else
					{
						ImmutableSortedDictionary<TKey, TValue>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedDictionary<TKey, TValue>.Node node3 = this._right.Remove(node2._key, keyComparer, out flag);
						node = node2.Mutate(this._left, node3);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node node4 = this._left.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(node4, null);
					}
				}
				else
				{
					ImmutableSortedDictionary<TKey, TValue>.Node node5 = this._right.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, node5);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x0000F908 File Offset: 0x0000DB08
			private ImmutableSortedDictionary<TKey, TValue>.Node Mutate(ImmutableSortedDictionary<TKey, TValue>.Node left = null, ImmutableSortedDictionary<TKey, TValue>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedDictionary<TKey, TValue>.Node(this._key, this._value, left ?? this._left, right ?? this._right, false);
				}
				if (left != null)
				{
					this._left = left;
				}
				if (right != null)
				{
					this._right = right;
				}
				this._height = checked(1 + Math.Max(this._left._height, this._right._height));
				return this;
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x0000F980 File Offset: 0x0000DB80
			private ImmutableSortedDictionary<TKey, TValue>.Node Search(TKey key, IComparer<TKey> keyComparer)
			{
				if (this.IsEmpty)
				{
					return this;
				}
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, keyComparer);
				}
				return this._left.Search(key, keyComparer);
			}

			// Token: 0x040000DC RID: 220
			[Nullable(new byte[] { 1, 0, 0 })]
			internal static readonly ImmutableSortedDictionary<TKey, TValue>.Node EmptyNode = new ImmutableSortedDictionary<TKey, TValue>.Node();

			// Token: 0x040000DD RID: 221
			private readonly TKey _key;

			// Token: 0x040000DE RID: 222
			private readonly TValue _value;

			// Token: 0x040000DF RID: 223
			private bool _frozen;

			// Token: 0x040000E0 RID: 224
			private byte _height;

			// Token: 0x040000E1 RID: 225
			private ImmutableSortedDictionary<TKey, TValue>.Node _left;

			// Token: 0x040000E2 RID: 226
			private ImmutableSortedDictionary<TKey, TValue>.Node _right;
		}
	}
}
