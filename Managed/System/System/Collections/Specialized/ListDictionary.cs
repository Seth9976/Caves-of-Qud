using System;
using System.Threading;

namespace System.Collections.Specialized
{
	/// <summary>Implements IDictionary using a singly linked list. Recommended for collections that typically include fewer than 10 items.</summary>
	// Token: 0x020004AA RID: 1194
	[Serializable]
	public class ListDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the default comparer.</summary>
		// Token: 0x06002650 RID: 9808 RVA: 0x0000219B File Offset: 0x0000039B
		public ListDictionary()
		{
		}

		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		// Token: 0x06002651 RID: 9809 RVA: 0x00085F59 File Offset: 0x00084159
		public ListDictionary(IComparer comparer)
		{
			this.comparer = comparer;
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new entry using the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x170007B6 RID: 1974
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				ListDictionary.DictionaryNode dictionaryNode = this.head;
				if (this.comparer == null)
				{
					while (dictionaryNode != null)
					{
						if (dictionaryNode.key.Equals(key))
						{
							return dictionaryNode.value;
						}
						dictionaryNode = dictionaryNode.next;
					}
				}
				else
				{
					while (dictionaryNode != null)
					{
						object key2 = dictionaryNode.key;
						if (this.comparer.Compare(key2, key) == 0)
						{
							return dictionaryNode.value;
						}
						dictionaryNode = dictionaryNode.next;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.version++;
				ListDictionary.DictionaryNode dictionaryNode = null;
				ListDictionary.DictionaryNode next;
				for (next = this.head; next != null; next = next.next)
				{
					object key2 = next.key;
					if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
					{
						break;
					}
					dictionaryNode = next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x0008608A File Offset: 0x0008428A
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x00086092 File Offset: 0x00084292
		public ICollection Keys
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, true);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is read-only.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> has a fixed size.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x0008609B File Offset: 0x0008429B
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x000860BD File Offset: 0x000842BD
		public ICollection Values
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.ListDictionary" />. </exception>
		// Token: 0x0600265B RID: 9819 RVA: 0x000860C8 File Offset: 0x000842C8
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key));
				}
				dictionaryNode = next;
			}
			ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		// Token: 0x0600265C RID: 9820 RVA: 0x00086178 File Offset: 0x00084378
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.ListDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x0600265D RID: 9821 RVA: 0x00086198 File Offset: 0x00084398
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.ListDictionary" /> entries to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.ListDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.ListDictionary" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.ListDictionary" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x0600265E RID: 9822 RVA: 0x000861F4 File Offset: 0x000843F4
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Length - index < this.count)
			{
				throw new ArgumentException("Insufficient space in the target location to copy the information.");
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x0600265F RID: 9823 RVA: 0x00086279 File Offset: 0x00084479
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x06002660 RID: 9824 RVA: 0x00086279 File Offset: 0x00084479
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06002661 RID: 9825 RVA: 0x00086284 File Offset: 0x00084484
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			ListDictionary.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					break;
				}
				dictionaryNode = next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x0400150C RID: 5388
		private ListDictionary.DictionaryNode head;

		// Token: 0x0400150D RID: 5389
		private int version;

		// Token: 0x0400150E RID: 5390
		private int count;

		// Token: 0x0400150F RID: 5391
		private readonly IComparer comparer;

		// Token: 0x04001510 RID: 5392
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x020004AB RID: 1195
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06002662 RID: 9826 RVA: 0x00086323 File Offset: 0x00084523
			public NodeEnumerator(ListDictionary list)
			{
				this._list = list;
				this._version = list.version;
				this._start = true;
				this._current = null;
			}

			// Token: 0x170007BE RID: 1982
			// (get) Token: 0x06002663 RID: 9827 RVA: 0x0008634C File Offset: 0x0008454C
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x170007BF RID: 1983
			// (get) Token: 0x06002664 RID: 9828 RVA: 0x00086359 File Offset: 0x00084559
			public DictionaryEntry Entry
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._current.key, this._current.value);
				}
			}

			// Token: 0x170007C0 RID: 1984
			// (get) Token: 0x06002665 RID: 9829 RVA: 0x00086389 File Offset: 0x00084589
			public object Key
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current.key;
				}
			}

			// Token: 0x170007C1 RID: 1985
			// (get) Token: 0x06002666 RID: 9830 RVA: 0x000863A9 File Offset: 0x000845A9
			public object Value
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current.value;
				}
			}

			// Token: 0x06002667 RID: 9831 RVA: 0x000863CC File Offset: 0x000845CC
			public bool MoveNext()
			{
				if (this._version != this._list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._start)
				{
					this._current = this._list.head;
					this._start = false;
				}
				else if (this._current != null)
				{
					this._current = this._current.next;
				}
				return this._current != null;
			}

			// Token: 0x06002668 RID: 9832 RVA: 0x0008643B File Offset: 0x0008463B
			public void Reset()
			{
				if (this._version != this._list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._start = true;
				this._current = null;
			}

			// Token: 0x04001511 RID: 5393
			private ListDictionary _list;

			// Token: 0x04001512 RID: 5394
			private ListDictionary.DictionaryNode _current;

			// Token: 0x04001513 RID: 5395
			private int _version;

			// Token: 0x04001514 RID: 5396
			private bool _start;
		}

		// Token: 0x020004AC RID: 1196
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06002669 RID: 9833 RVA: 0x00086469 File Offset: 0x00084669
			public NodeKeyValueCollection(ListDictionary list, bool isKeys)
			{
				this._list = list;
				this._isKeys = isKeys;
			}

			// Token: 0x0600266A RID: 9834 RVA: 0x00086480 File Offset: 0x00084680
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				for (ListDictionary.DictionaryNode dictionaryNode = this._list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this._isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x170007C2 RID: 1986
			// (get) Token: 0x0600266B RID: 9835 RVA: 0x000864F0 File Offset: 0x000846F0
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionary.DictionaryNode dictionaryNode = this._list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170007C3 RID: 1987
			// (get) Token: 0x0600266C RID: 9836 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170007C4 RID: 1988
			// (get) Token: 0x0600266D RID: 9837 RVA: 0x0008651C File Offset: 0x0008471C
			object ICollection.SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x0600266E RID: 9838 RVA: 0x00086529 File Offset: 0x00084729
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionary.NodeKeyValueCollection.NodeKeyValueEnumerator(this._list, this._isKeys);
			}

			// Token: 0x04001515 RID: 5397
			private ListDictionary _list;

			// Token: 0x04001516 RID: 5398
			private bool _isKeys;

			// Token: 0x020004AD RID: 1197
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x0600266F RID: 9839 RVA: 0x0008653C File Offset: 0x0008473C
				public NodeKeyValueEnumerator(ListDictionary list, bool isKeys)
				{
					this._list = list;
					this._isKeys = isKeys;
					this._version = list.version;
					this._start = true;
					this._current = null;
				}

				// Token: 0x170007C5 RID: 1989
				// (get) Token: 0x06002670 RID: 9840 RVA: 0x0008656C File Offset: 0x0008476C
				public object Current
				{
					get
					{
						if (this._current == null)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						if (!this._isKeys)
						{
							return this._current.value;
						}
						return this._current.key;
					}
				}

				// Token: 0x06002671 RID: 9841 RVA: 0x000865A0 File Offset: 0x000847A0
				public bool MoveNext()
				{
					if (this._version != this._list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (this._start)
					{
						this._current = this._list.head;
						this._start = false;
					}
					else if (this._current != null)
					{
						this._current = this._current.next;
					}
					return this._current != null;
				}

				// Token: 0x06002672 RID: 9842 RVA: 0x0008660F File Offset: 0x0008480F
				public void Reset()
				{
					if (this._version != this._list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					this._start = true;
					this._current = null;
				}

				// Token: 0x04001517 RID: 5399
				private ListDictionary _list;

				// Token: 0x04001518 RID: 5400
				private ListDictionary.DictionaryNode _current;

				// Token: 0x04001519 RID: 5401
				private int _version;

				// Token: 0x0400151A RID: 5402
				private bool _isKeys;

				// Token: 0x0400151B RID: 5403
				private bool _start;
			}
		}

		// Token: 0x020004AE RID: 1198
		[Serializable]
		public class DictionaryNode
		{
			// Token: 0x0400151C RID: 5404
			public object key;

			// Token: 0x0400151D RID: 5405
			public object value;

			// Token: 0x0400151E RID: 5406
			public ListDictionary.DictionaryNode next;
		}
	}
}
