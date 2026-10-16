using System;
using System.Collections;
using System.Collections.Generic;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeNamespaceImport" /> objects.</summary>
	// Token: 0x02000322 RID: 802
	[Serializable]
	public class CodeNamespaceImportCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000517 RID: 1303
		public CodeNamespaceImport this[int index]
		{
			get
			{
				return (CodeNamespaceImport)this._data[index];
			}
			set
			{
				this._data[index] = value;
				this.SyncKeys();
			}
		}

		/// <summary>Gets the number of namespaces in the collection.</summary>
		/// <returns>The number of namespaces in the collection.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00060742 File Offset: 0x0005E942
		public int Count
		{
			get
			{
				return this._data.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.  This property always returns false.</returns>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x00003062 File Offset: 0x00001262
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.  This property always returns false.</returns>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00003062 File Offset: 0x00001262
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to add to the collection. </param>
		// Token: 0x06001977 RID: 6519 RVA: 0x0006074F File Offset: 0x0005E94F
		public void Add(CodeNamespaceImport value)
		{
			if (!this._keys.ContainsKey(value.Namespace))
			{
				this._keys[value.Namespace] = value;
				this._data.Add(value);
			}
		}

		/// <summary>Adds a set of <see cref="T:System.CodeDom.CodeNamespaceImport" /> objects to the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeNamespaceImport" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001978 RID: 6520 RVA: 0x00060784 File Offset: 0x0005E984
		public void AddRange(CodeNamespaceImport[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (CodeNamespaceImport codeNamespaceImport in value)
			{
				this.Add(codeNamespaceImport);
			}
		}

		/// <summary>Clears the collection of members.</summary>
		// Token: 0x06001979 RID: 6521 RVA: 0x000607BA File Offset: 0x0005E9BA
		public void Clear()
		{
			this._data.Clear();
			this._keys.Clear();
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000607D4 File Offset: 0x0005E9D4
		private void SyncKeys()
		{
			this._keys.Clear();
			foreach (object obj in this._data)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				this._keys[codeNamespaceImport.Namespace] = codeNamespaceImport;
			}
		}

		/// <summary>Gets an enumerator that enumerates the collection members.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that indicates the collection members.</returns>
		// Token: 0x0600197B RID: 6523 RVA: 0x00060844 File Offset: 0x0005EA44
		public IEnumerator GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x1700051B RID: 1307
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (CodeNamespaceImport)value;
				this.SyncKeys();
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0006086F File Offset: 0x0005EA6F
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false. This property always returns false. </returns>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  This property always returns null.</returns>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x00002F6A File Offset: 0x0000116A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06001981 RID: 6529 RVA: 0x00060877 File Offset: 0x0005EA77
		void ICollection.CopyTo(Array array, int index)
		{
			this._data.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001982 RID: 6530 RVA: 0x00060886 File Offset: 0x0005EA86
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001983 RID: 6531 RVA: 0x0006088E File Offset: 0x0005EA8E
		int IList.Add(object value)
		{
			return this._data.Add((CodeNamespaceImport)value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Collections.IList" />.</summary>
		// Token: 0x06001984 RID: 6532 RVA: 0x000608A1 File Offset: 0x0005EAA1
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the value is in the list; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001985 RID: 6533 RVA: 0x000608A9 File Offset: 0x0005EAA9
		bool IList.Contains(object value)
		{
			return this._data.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />. </summary>
		/// <returns>The index of <paramref name="value" /> if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001986 RID: 6534 RVA: 0x000608B7 File Offset: 0x0005EAB7
		int IList.IndexOf(object value)
		{
			return this._data.IndexOf((CodeNamespaceImport)value);
		}

		/// <summary>Inserts an item in the <see cref="T:System.Collections.IList" /> at the specified position. </summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001987 RID: 6535 RVA: 0x000608CA File Offset: 0x0005EACA
		void IList.Insert(int index, object value)
		{
			this._data.Insert(index, (CodeNamespaceImport)value);
			this.SyncKeys();
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />. </summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001988 RID: 6536 RVA: 0x000608E4 File Offset: 0x0005EAE4
		void IList.Remove(object value)
		{
			this._data.Remove((CodeNamespaceImport)value);
			this.SyncKeys();
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.IList" />. </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06001989 RID: 6537 RVA: 0x000608FD File Offset: 0x0005EAFD
		void IList.RemoveAt(int index)
		{
			this._data.RemoveAt(index);
			this.SyncKeys();
		}

		// Token: 0x04000DE5 RID: 3557
		private readonly ArrayList _data = new ArrayList();

		// Token: 0x04000DE6 RID: 3558
		private readonly Dictionary<string, CodeNamespaceImport> _keys = new Dictionary<string, CodeNamespaceImport>(StringComparer.OrdinalIgnoreCase);
	}
}
