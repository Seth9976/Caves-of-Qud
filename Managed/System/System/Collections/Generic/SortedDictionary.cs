using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted on the key. </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020004D0 RID: 1232
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[Serializable]
	public class SortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that is empty and uses the default <see cref="T:System.Collections.Generic.IComparer`1" /> implementation for the key type.</summary>
		// Token: 0x060027DE RID: 10206 RVA: 0x00089CA5 File Offset: 0x00087EA5
		public SortedDictionary()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the default <see cref="T:System.Collections.Generic.IComparer`1" /> implementation for the key type.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060027DF RID: 10207 RVA: 0x00089CAE File Offset: 0x00087EAE
		public SortedDictionary(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to compare keys.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060027E0 RID: 10208 RVA: 0x00089CB8 File Offset: 0x00087EB8
		public SortedDictionary(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._set = new TreeSet<KeyValuePair<TKey, TValue>>(new SortedDictionary<TKey, TValue>.KeyValuePairComparer(comparer));
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this._set.Add(keyValuePair);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that is empty and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to compare keys.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x060027E1 RID: 10209 RVA: 0x00089D2C File Offset: 0x00087F2C
		public SortedDictionary(IComparer<TKey> comparer)
		{
			this._set = new TreeSet<KeyValuePair<TKey, TValue>>(new SortedDictionary<TKey, TValue>.KeyValuePairComparer(comparer));
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x00089D45 File Offset: 0x00087F45
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this._set.Add(keyValuePair);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x00089D54 File Offset: 0x00087F54
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(keyValuePair);
			if (node == null)
			{
				return false;
			}
			if (keyValuePair.Value == null)
			{
				return node.Item.Value == null;
			}
			return EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x00089DB8 File Offset: 0x00087FB8
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(keyValuePair);
			if (node == null)
			{
				return false;
			}
			if (EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value))
			{
				this._set.Remove(keyValuePair);
				return true;
			}
			return false;
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000824 RID: 2084
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
				if (node == null)
				{
					throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
				}
				return node.Item.Value;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
				if (node == null)
				{
					this._set.Add(new KeyValuePair<TKey, TValue>(key, value));
					return;
				}
				node.Item = new KeyValuePair<TKey, TValue>(node.Item.Key, value);
				this._set.UpdateVersion();
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x00089EE7 File Offset: 0x000880E7
		public int Count
		{
			get
			{
				return this._set.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> used to order the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IComparer`1" /> used to order the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /></returns>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x00089EF4 File Offset: 0x000880F4
		public IComparer<TKey> Comparer
		{
			get
			{
				return ((SortedDictionary<TKey, TValue>.KeyValuePairComparer)this._set.Comparer).keyComparer;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x00089F0B File Offset: 0x0008810B
		public SortedDictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new SortedDictionary<TKey, TValue>.KeyCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x00089F27 File Offset: 0x00088127
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x00089F27 File Offset: 0x00088127
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> containing the values in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060027ED RID: 10221 RVA: 0x00089F2F File Offset: 0x0008812F
		public SortedDictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new SortedDictionary<TKey, TValue>.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x00089F4B File Offset: 0x0008814B
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x00089F4B File Offset: 0x0008814B
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</exception>
		// Token: 0x060027F0 RID: 10224 RVA: 0x00089F53 File Offset: 0x00088153
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._set.Add(new KeyValuePair<TKey, TValue>(key, value));
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		// Token: 0x060027F1 RID: 10225 RVA: 0x00089F7B File Offset: 0x0008817B
		public void Clear()
		{
			this._set.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060027F2 RID: 10226 RVA: 0x00089F88 File Offset: 0x00088188
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this._set.Contains(new KeyValuePair<TKey, TValue>(key, default(TValue)));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />. The value can be null for reference types.</param>
		// Token: 0x060027F3 RID: 10227 RVA: 0x00089FC4 File Offset: 0x000881C4
		public bool ContainsValue(TValue value)
		{
			bool found = false;
			if (value == null)
			{
				this._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					if (node.Item.Value == null)
					{
						found = true;
						return false;
					}
					return true;
				});
			}
			else
			{
				EqualityComparer<TValue> valueComparer = EqualityComparer<TValue>.Default;
				this._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					if (valueComparer.Equals(node.Item.Value, value))
					{
						found = true;
						return false;
					}
					return true;
				});
			}
			return found;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> to the specified array of <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structures, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structures that is the destination of the elements copied from the current <see cref="T:System.Collections.Generic.SortedDictionary`2" /> The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x060027F4 RID: 10228 RVA: 0x0008A042 File Offset: 0x00088242
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this._set.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.Enumerator" /> for the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x060027F5 RID: 10229 RVA: 0x0008A051 File Offset: 0x00088251
		public SortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0008A05A File Offset: 0x0008825A
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> is not found in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060027F7 RID: 10231 RVA: 0x0008A068 File Offset: 0x00088268
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this._set.Remove(new KeyValuePair<TKey, TValue>(key, default(TValue)));
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060027F8 RID: 10232 RVA: 0x0008A0A4 File Offset: 0x000882A4
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
			if (node == null)
			{
				value = default(TValue);
				return false;
			}
			value = node.Item.Value;
			return true;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.Generic.ICollection`1" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060027F9 RID: 10233 RVA: 0x0008A100 File Offset: 0x00088300
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._set).CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns false.</returns>
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x00003062 File Offset: 0x00001262
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns false.</returns>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x00003062 File Offset: 0x00001262
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x00089F27 File Offset: 0x00088127
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x00089F4B File Offset: 0x0008814B
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		/// <param name="key">The key of the element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</exception>
		// Token: 0x17000831 RID: 2097
		object IDictionary.this[object key]
		{
			get
			{
				TValue tvalue;
				if (SortedDictionary<TKey, TValue>.IsCompatibleKey(key) && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null && default(TValue) != null)
				{
					throw new ArgumentNullException("value");
				}
				try
				{
					TKey tkey = (TKey)((object)key);
					try
					{
						this[tkey] = (TValue)((object)value);
					}
					catch (InvalidCastException)
					{
						throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
					}
				}
				catch (InvalidCastException)
				{
					throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
				}
			}
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.-or-<paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.-or-An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x06002800 RID: 10240 RVA: 0x0008A1F0 File Offset: 0x000883F0
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null && default(TValue) != null)
			{
				throw new ArgumentNullException("value");
			}
			try
			{
				TKey tkey = (TKey)((object)key);
				try
				{
					this.Add(tkey, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06002801 RID: 10241 RVA: 0x0008A2A0 File Offset: 0x000884A0
		bool IDictionary.Contains(object key)
		{
			return SortedDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0008A2B8 File Offset: 0x000884B8
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06002803 RID: 10243 RVA: 0x0008A2D1 File Offset: 0x000884D1
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06002804 RID: 10244 RVA: 0x0008A2DF File Offset: 0x000884DF
		void IDictionary.Remove(object key)
		{
			if (SortedDictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002805 RID: 10245 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. </returns>
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x0008A2F6 File Offset: 0x000884F6
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._set).SyncRoot;
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06002807 RID: 10247 RVA: 0x0008A05A File Offset: 0x0008825A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x04001593 RID: 5523
		[NonSerialized]
		private SortedDictionary<TKey, TValue>.KeyCollection _keys;

		// Token: 0x04001594 RID: 5524
		[NonSerialized]
		private SortedDictionary<TKey, TValue>.ValueCollection _values;

		// Token: 0x04001595 RID: 5525
		private TreeSet<KeyValuePair<TKey, TValue>> _set;

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		// Token: 0x020004D1 RID: 1233
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06002808 RID: 10248 RVA: 0x0008A303 File Offset: 0x00088503
			internal Enumerator(SortedDictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this._treeEnum = dictionary._set.GetEnumerator();
				this._getEnumeratorRetType = getEnumeratorRetType;
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06002809 RID: 10249 RVA: 0x0008A31D File Offset: 0x0008851D
			public bool MoveNext()
			{
				return this._treeEnum.MoveNext();
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.Enumerator" />.</summary>
			// Token: 0x0600280A RID: 10250 RVA: 0x0008A32A File Offset: 0x0008852A
			public void Dispose()
			{
				this._treeEnum.Dispose();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> at the current position of the enumerator.</returns>
			// Token: 0x17000834 RID: 2100
			// (get) Token: 0x0600280B RID: 10251 RVA: 0x0008A337 File Offset: 0x00088537
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this._treeEnum.Current;
				}
			}

			// Token: 0x17000835 RID: 2101
			// (get) Token: 0x0600280C RID: 10252 RVA: 0x0008A344 File Offset: 0x00088544
			internal bool NotStartedOrEnded
			{
				get
				{
					return this._treeEnum.NotStartedOrEnded;
				}
			}

			// Token: 0x0600280D RID: 10253 RVA: 0x0008A351 File Offset: 0x00088551
			internal void Reset()
			{
				this._treeEnum.Reset();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600280E RID: 10254 RVA: 0x0008A351 File Offset: 0x00088551
			void IEnumerator.Reset()
			{
				this._treeEnum.Reset();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000836 RID: 2102
			// (get) Token: 0x0600280F RID: 10255 RVA: 0x0008A360 File Offset: 0x00088560
			object IEnumerator.Current
			{
				get
				{
					if (this.NotStartedOrEnded)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					KeyValuePair<TKey, TValue> keyValuePair;
					if (this._getEnumeratorRetType == 2)
					{
						keyValuePair = this.Current;
						object obj = keyValuePair.Key;
						keyValuePair = this.Current;
						return new DictionaryEntry(obj, keyValuePair.Value);
					}
					keyValuePair = this.Current;
					TKey key = keyValuePair.Key;
					keyValuePair = this.Current;
					return new KeyValuePair<TKey, TValue>(key, keyValuePair.Value);
				}
			}

			/// <summary>Gets the key of the element at the current position of the enumerator.</summary>
			/// <returns>The key of the element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000837 RID: 2103
			// (get) Token: 0x06002810 RID: 10256 RVA: 0x0008A3E0 File Offset: 0x000885E0
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this.NotStartedOrEnded)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Key;
				}
			}

			/// <summary>Gets the value of the element at the current position of the enumerator.</summary>
			/// <returns>The value of the element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000838 RID: 2104
			// (get) Token: 0x06002811 RID: 10257 RVA: 0x0008A414 File Offset: 0x00088614
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this.NotStartedOrEnded)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Value;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator as a <see cref="T:System.Collections.DictionaryEntry" /> structure.</summary>
			/// <returns>The element in the collection at the current position of the dictionary, as a <see cref="T:System.Collections.DictionaryEntry" /> structure.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x06002812 RID: 10258 RVA: 0x0008A448 File Offset: 0x00088648
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this.NotStartedOrEnded)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x04001596 RID: 5526
			private SortedSet<KeyValuePair<TKey, TValue>>.Enumerator _treeEnum;

			// Token: 0x04001597 RID: 5527
			private int _getEnumeratorRetType;

			// Token: 0x04001598 RID: 5528
			internal const int KeyValuePair = 1;

			// Token: 0x04001599 RID: 5529
			internal const int DictEntry = 2;
		}

		/// <summary>Represents the collection of keys in a <see cref="T:System.Collections.Generic.SortedDictionary`2" />. This class cannot be inherited. </summary>
		// Token: 0x020004D2 RID: 1234
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.SortedDictionary`2" /> whose keys are reflected in the new <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x06002813 RID: 10259 RVA: 0x0008A493 File Offset: 0x00088693
			public KeyCollection(SortedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this._dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</returns>
			// Token: 0x06002814 RID: 10260 RVA: 0x0008A4B0 File Offset: 0x000886B0
			public SortedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06002815 RID: 10261 RVA: 0x0008A4BD File Offset: 0x000886BD
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06002816 RID: 10262 RVA: 0x0008A4BD File Offset: 0x000886BD
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> elements to an existing one-dimensional array, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06002817 RID: 10263 RVA: 0x0008A4D0 File Offset: 0x000886D0
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				if (array.Length - index < this.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					TKey[] array2 = array;
					int index2 = index;
					index = index2 + 1;
					array2[index2] = node.Item.Key;
					return true;
				});
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06002818 RID: 10264 RVA: 0x0008A568 File Offset: 0x00088768
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (array.GetLowerBound(0) != 0)
				{
					throw new ArgumentException("The lower bound of target array must be zero.", "array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				if (array.Length - index < this._dictionary.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				try
				{
					object[] objects = (object[])array;
					this._dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
					{
						object[] objects2 = objects;
						int index2 = index;
						index = index2 + 1;
						objects2[index2] = node.Item.Key;
						return true;
					});
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</returns>
			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x06002819 RID: 10265 RVA: 0x0008A670 File Offset: 0x00088870
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x1700083B RID: 2107
			// (get) Token: 0x0600281A RID: 10266 RVA: 0x0000390E File Offset: 0x00001B0E
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600281B RID: 10267 RVA: 0x0008A67D File Offset: 0x0008887D
			void ICollection<TKey>.Add(TKey item)
			{
				throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
			}

			// Token: 0x0600281C RID: 10268 RVA: 0x0008A67D File Offset: 0x0008887D
			void ICollection<TKey>.Clear()
			{
				throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
			}

			// Token: 0x0600281D RID: 10269 RVA: 0x0008A689 File Offset: 0x00088889
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this._dictionary.ContainsKey(item);
			}

			// Token: 0x0600281E RID: 10270 RVA: 0x0008A67D File Offset: 0x0008887D
			bool ICollection<TKey>.Remove(TKey item)
			{
				throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />, this property always returns false.</returns>
			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x0600281F RID: 10271 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
			// Token: 0x1700083D RID: 2109
			// (get) Token: 0x06002820 RID: 10272 RVA: 0x0008A697 File Offset: 0x00088897
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x0400159A RID: 5530
			private SortedDictionary<TKey, TValue> _dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			// Token: 0x020004D3 RID: 1235
			public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
			{
				// Token: 0x06002821 RID: 10273 RVA: 0x0008A6A4 File Offset: 0x000888A4
				internal Enumerator(SortedDictionary<TKey, TValue> dictionary)
				{
					this._dictEnum = dictionary.GetEnumerator();
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection.Enumerator" />.</summary>
				// Token: 0x06002822 RID: 10274 RVA: 0x0008A6B2 File Offset: 0x000888B2
				public void Dispose()
				{
					this._dictEnum.Dispose();
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06002823 RID: 10275 RVA: 0x0008A6BF File Offset: 0x000888BF
				public bool MoveNext()
				{
					return this._dictEnum.MoveNext();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x1700083E RID: 2110
				// (get) Token: 0x06002824 RID: 10276 RVA: 0x0008A6CC File Offset: 0x000888CC
				public TKey Current
				{
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this._dictEnum.Current;
						return keyValuePair.Key;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x1700083F RID: 2111
				// (get) Token: 0x06002825 RID: 10277 RVA: 0x0008A6EC File Offset: 0x000888EC
				object IEnumerator.Current
				{
					get
					{
						if (this._dictEnum.NotStartedOrEnded)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						return this.Current;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06002826 RID: 10278 RVA: 0x0008A711 File Offset: 0x00088911
				void IEnumerator.Reset()
				{
					this._dictEnum.Reset();
				}

				// Token: 0x0400159B RID: 5531
				private SortedDictionary<TKey, TValue>.Enumerator _dictEnum;
			}
		}

		/// <summary>Represents the collection of values in a <see cref="T:System.Collections.Generic.SortedDictionary`2" />. This class cannot be inherited</summary>
		// Token: 0x020004D6 RID: 1238
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> class that reflects the values in the specified <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.SortedDictionary`2" /> whose values are reflected in the new <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x0600282B RID: 10283 RVA: 0x0008A791 File Offset: 0x00088991
			public ValueCollection(SortedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this._dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</returns>
			// Token: 0x0600282C RID: 10284 RVA: 0x0008A7AE File Offset: 0x000889AE
			public SortedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x0600282D RID: 10285 RVA: 0x0008A7BB File Offset: 0x000889BB
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x0600282E RID: 10286 RVA: 0x0008A7BB File Offset: 0x000889BB
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> elements to an existing one-dimensional array, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x0600282F RID: 10287 RVA: 0x0008A7D0 File Offset: 0x000889D0
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				if (array.Length - index < this.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					TValue[] array2 = array;
					int index2 = index;
					index = index2 + 1;
					array2[index2] = node.Item.Value;
					return true;
				});
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06002830 RID: 10288 RVA: 0x0008A868 File Offset: 0x00088A68
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (array.GetLowerBound(0) != 0)
				{
					throw new ArgumentException("The lower bound of target array must be zero.", "array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				if (array.Length - index < this._dictionary.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				try
				{
					object[] objects = (object[])array;
					this._dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
					{
						object[] objects2 = objects;
						int index2 = index;
						index = index2 + 1;
						objects2[index2] = node.Item.Value;
						return true;
					});
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</returns>
			// Token: 0x17000840 RID: 2112
			// (get) Token: 0x06002831 RID: 10289 RVA: 0x0008A970 File Offset: 0x00088B70
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x06002832 RID: 10290 RVA: 0x0000390E File Offset: 0x00001B0E
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002833 RID: 10291 RVA: 0x0008A97D File Offset: 0x00088B7D
			void ICollection<TValue>.Add(TValue item)
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x06002834 RID: 10292 RVA: 0x0008A97D File Offset: 0x00088B7D
			void ICollection<TValue>.Clear()
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x06002835 RID: 10293 RVA: 0x0008A989 File Offset: 0x00088B89
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this._dictionary.ContainsValue(item);
			}

			// Token: 0x06002836 RID: 10294 RVA: 0x0008A97D File Offset: 0x00088B7D
			bool ICollection<TValue>.Remove(TValue item)
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />, this property always returns false.</returns>
			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06002837 RID: 10295 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06002838 RID: 10296 RVA: 0x0008A997 File Offset: 0x00088B97
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x040015A0 RID: 5536
			private SortedDictionary<TKey, TValue> _dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			// Token: 0x020004D7 RID: 1239
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x06002839 RID: 10297 RVA: 0x0008A9A4 File Offset: 0x00088BA4
				internal Enumerator(SortedDictionary<TKey, TValue> dictionary)
				{
					this._dictEnum = dictionary.GetEnumerator();
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection.Enumerator" />.</summary>
				// Token: 0x0600283A RID: 10298 RVA: 0x0008A9B2 File Offset: 0x00088BB2
				public void Dispose()
				{
					this._dictEnum.Dispose();
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x0600283B RID: 10299 RVA: 0x0008A9BF File Offset: 0x00088BBF
				public bool MoveNext()
				{
					return this._dictEnum.MoveNext();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17000844 RID: 2116
				// (get) Token: 0x0600283C RID: 10300 RVA: 0x0008A9CC File Offset: 0x00088BCC
				public TValue Current
				{
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this._dictEnum.Current;
						return keyValuePair.Value;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x17000845 RID: 2117
				// (get) Token: 0x0600283D RID: 10301 RVA: 0x0008A9EC File Offset: 0x00088BEC
				object IEnumerator.Current
				{
					get
					{
						if (this._dictEnum.NotStartedOrEnded)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						return this.Current;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x0600283E RID: 10302 RVA: 0x0008AA11 File Offset: 0x00088C11
				void IEnumerator.Reset()
				{
					this._dictEnum.Reset();
				}

				// Token: 0x040015A1 RID: 5537
				private SortedDictionary<TKey, TValue>.Enumerator _dictEnum;
			}
		}

		// Token: 0x020004DA RID: 1242
		[Serializable]
		internal sealed class KeyValuePairComparer : Comparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x06002843 RID: 10307 RVA: 0x0008AA91 File Offset: 0x00088C91
			public KeyValuePairComparer(IComparer<TKey> keyComparer)
			{
				if (keyComparer == null)
				{
					this.keyComparer = Comparer<TKey>.Default;
					return;
				}
				this.keyComparer = keyComparer;
			}

			// Token: 0x06002844 RID: 10308 RVA: 0x0008AAAF File Offset: 0x00088CAF
			public override int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this.keyComparer.Compare(x.Key, y.Key);
			}

			// Token: 0x040015A6 RID: 5542
			internal IComparer<TKey> keyComparer;
		}
	}
}
