using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates a collection of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects that represent a single row in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200041D RID: 1053
	[Editor("System.Web.UI.Design.WebControls.TableRowsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class TableRowCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06002F7A RID: 12154 RVA: 0x0007B3B6 File Offset: 0x000795B6
		internal TableRowCollection(Table table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			this.cc = table.Controls;
			this.owner = table;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. The default is 0.</returns>
		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x0007B3DF File Offset: 0x000795DF
		public int Count
		{
			get
			{
				return this.cc.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> is read-only.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableRow" /> that represents an element in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</returns>
		/// <param name="index">An ordinal index value that specifies which <see cref="T:System.Web.UI.WebControls.TableRow" /> object to return. This index is zero-based.</param>
		// Token: 0x17000F27 RID: 3879
		public TableRow this[int index]
		{
			get
			{
				return (TableRow)this.cc[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.TableRow" />.</returns>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to add to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002F80 RID: 12160 RVA: 0x0007B400 File Offset: 0x00079600
		public int Add(TableRow row)
		{
			if (row == null)
			{
				throw new NullReferenceException();
			}
			if (row.TableRowSectionSet)
			{
				this.owner.GenerateTableSections = true;
			}
			row.Container = this;
			int num = this.cc.IndexOf(row);
			if (num < 0)
			{
				this.cc.Add(row);
				num = this.cc.Count;
			}
			return num;
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> object to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at the specified index location.</summary>
		/// <param name="index">The location in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at which to add the <see cref="T:System.Web.UI.WebControls.TableRow" />. </param>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to add to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002F81 RID: 12161 RVA: 0x0007B45C File Offset: 0x0007965C
		public void AddAt(int index, TableRow row)
		{
			if (row == null)
			{
				throw new NullReferenceException();
			}
			if (this.cc.IndexOf(row) < 0)
			{
				if (row.TableRowSectionSet)
				{
					this.owner.GenerateTableSections = true;
				}
				row.Container = this;
				this.cc.AddAt(index, row);
			}
		}

		/// <summary>Appends the <see cref="T:System.Web.UI.WebControls.TableRow" /> objects from the specified array to the end of the collection.</summary>
		/// <param name="rows">The array containing the <see cref="T:System.Web.UI.WebControls.TableRow" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="rows" /> parameter is null. </exception>
		// Token: 0x06002F82 RID: 12162 RVA: 0x0007B4AC File Offset: 0x000796AC
		public void AddRange(TableRow[] rows)
		{
			foreach (TableRow tableRow in rows)
			{
				if (tableRow == null)
				{
					throw new NullReferenceException();
				}
				if (this.cc.IndexOf(tableRow) < 0)
				{
					if (tableRow.TableRowSectionSet)
					{
						this.owner.GenerateTableSections = true;
					}
					tableRow.Container = this;
					this.cc.Add(tableRow);
				}
			}
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.TableRow" /> controls from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		// Token: 0x06002F83 RID: 12163 RVA: 0x0007B50C File Offset: 0x0007970C
		public void Clear()
		{
			this.owner.GenerateTableSections = false;
			this.cc.Clear();
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> to the specified <see cref="T:System.Array" />, starting with the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive copied contents. </param>
		// Token: 0x06002F84 RID: 12164 RVA: 0x0007B525 File Offset: 0x00079725
		public void CopyTo(Array array, int index)
		{
			this.cc.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableRow" /> objects within the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableRow" /> objects within the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</returns>
		// Token: 0x06002F85 RID: 12165 RVA: 0x0007B534 File Offset: 0x00079734
		public IEnumerator GetEnumerator()
		{
			return this.cc.GetEnumerator();
		}

		/// <summary>Returns a value that represents the index of the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>The ordinal index position of the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> within the collection. The default is -1, which indicates that the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> has not been found.</returns>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to search for in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002F86 RID: 12166 RVA: 0x0007B541 File Offset: 0x00079741
		public int GetRowIndex(TableRow row)
		{
			return this.cc.IndexOf(row);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x0007B54F File Offset: 0x0007974F
		internal void RowTableSectionSet()
		{
			this.owner.GenerateTableSections = true;
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to remove from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002F88 RID: 12168 RVA: 0x0007B55D File Offset: 0x0007975D
		public void Remove(TableRow row)
		{
			if (row != null)
			{
				row.Container = null;
			}
			this.cc.Remove(row);
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.TableRow" /> object to remove from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002F89 RID: 12169 RVA: 0x0007B578 File Offset: 0x00079778
		public void RemoveAt(int index)
		{
			TableRow tableRow = this[index];
			if (tableRow != null)
			{
				tableRow.Container = null;
			}
			this.cc.RemoveAt(index);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the list has a fixed size; otherwise, false</returns>
		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x00008A4D File Offset: 0x00006C4D
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		// Token: 0x17000F2A RID: 3882
		object IList.this[int index]
		{
			get
			{
				return this.cc[index];
			}
			set
			{
				this.cc.AddAt(index, (TableRow)value);
				this.cc.RemoveAt(index + 1);
			}
		}

		/// <summary>Adds an object to the collection.</summary>
		/// <returns>The index at which the object was added to the collection.</returns>
		/// <param name="o">The object to add to the collection.</param>
		// Token: 0x06002F8D RID: 12173 RVA: 0x0007B5D3 File Offset: 0x000797D3
		int IList.Add(object value)
		{
			return this.Add(value as TableRow);
		}

		/// <summary>Determines whether the specified object is contained within the collection.</summary>
		/// <returns>true if the object is in the collection; otherwise, false.</returns>
		/// <param name="o">The object to locate within the collection.</param>
		// Token: 0x06002F8E RID: 12174 RVA: 0x0007B5E1 File Offset: 0x000797E1
		bool IList.Contains(object value)
		{
			return this.cc.Contains(value as TableRow);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of the object within the collection; otherwise, -1 if the object is not in the collection.</returns>
		/// <param name="o">The object to locate within the collection.</param>
		// Token: 0x06002F8F RID: 12175 RVA: 0x0007B5F4 File Offset: 0x000797F4
		int IList.IndexOf(object value)
		{
			return this.cc.IndexOf(value as TableRow);
		}

		/// <summary>Inserts an object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index within the collection at which to insert the object.</param>
		/// <param name="o">The object to insert into the collection.</param>
		// Token: 0x06002F90 RID: 12176 RVA: 0x0007B607 File Offset: 0x00079807
		void IList.Insert(int index, object value)
		{
			this.AddAt(index, value as TableRow);
		}

		/// <summary>Removes an object from the collection.</summary>
		/// <param name="o">The object to remove from the collection.</param>
		// Token: 0x06002F91 RID: 12177 RVA: 0x0007B616 File Offset: 0x00079816
		void IList.Remove(object value)
		{
			this.Remove(value as TableRow);
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal TableRowCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001BE9 RID: 7145
		private ControlCollection cc;

		// Token: 0x04001BEA RID: 7146
		private Table owner;
	}
}
