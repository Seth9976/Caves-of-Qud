using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates a collection of <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> and <see cref="T:System.Web.UI.WebControls.TableCell" /> objects that make up a row in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000416 RID: 1046
	[Editor("System.Web.UI.Design.WebControls.TableCellsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class TableCellCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06002F38 RID: 12088 RVA: 0x0007AB45 File Offset: 0x00078D45
		internal TableCellCollection(TableRow tr)
		{
			this.cc = tr.Controls;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. The default is 0.</returns>
		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x0007AB59 File Offset: 0x00078D59
		public int Count
		{
			get
			{
				return this.cc.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> is read-only.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents an element in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</returns>
		/// <param name="index">An ordinal index value that specifies the <see cref="T:System.Web.UI.WebControls.TableCell" /> to return. </param>
		// Token: 0x17000F12 RID: 3858
		public TableCell this[int index]
		{
			get
			{
				return (TableCell)this.cc[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> to the end of the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>The index number of the <see cref="T:System.Web.UI.WebControls.TableCell" />.</returns>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to add to the collection. </param>
		// Token: 0x06002F3E RID: 12094 RVA: 0x0007AB7C File Offset: 0x00078D7C
		public int Add(TableCell cell)
		{
			int num = this.cc.IndexOf(cell);
			if (num < 0)
			{
				this.cc.Add(cell);
				num = this.cc.Count;
			}
			return num;
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at the specified index location.</summary>
		/// <param name="index">The location in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at which to add the <see cref="T:System.Web.UI.WebControls.TableCell" />. </param>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to add to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		// Token: 0x06002F3F RID: 12095 RVA: 0x0007ABB3 File Offset: 0x00078DB3
		public void AddAt(int index, TableCell cell)
		{
			if (this.cc.IndexOf(cell) < 0)
			{
				this.cc.AddAt(index, cell);
			}
		}

		/// <summary>Appends the <see cref="T:System.Web.UI.WebControls.TableCell" /> objects from the specified array to the end of the collection.</summary>
		/// <param name="cells">The array containing the <see cref="T:System.Web.UI.WebControls.TableCell" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="cells" /> parameter is null. </exception>
		// Token: 0x06002F40 RID: 12096 RVA: 0x0007ABD4 File Offset: 0x00078DD4
		public void AddRange(TableCell[] cells)
		{
			foreach (TableCell tableCell in cells)
			{
				if (this.cc.IndexOf(tableCell) < 0)
				{
					this.cc.Add(tableCell);
				}
			}
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.TableCell" /> objects from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		// Token: 0x06002F41 RID: 12097 RVA: 0x0007AC10 File Offset: 0x00078E10
		public void Clear()
		{
			this.cc.Clear();
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> to the specified <see cref="T:System.Array" />, starting with the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the items. </param>
		// Token: 0x06002F42 RID: 12098 RVA: 0x0007AC1D File Offset: 0x00078E1D
		public void CopyTo(Array array, int index)
		{
			this.cc.CopyTo(array, index);
		}

		/// <summary>Returns a value that represents the index of the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> within the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. The default is -1, which indicates that a match has not been found.</returns>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to get the index of in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		// Token: 0x06002F43 RID: 12099 RVA: 0x0007AC2C File Offset: 0x00078E2C
		public int GetCellIndex(TableCell cell)
		{
			return this.cc.IndexOf(cell);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableCell" /> objects in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableCell" /> objects within the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</returns>
		// Token: 0x06002F44 RID: 12100 RVA: 0x0007AC3A File Offset: 0x00078E3A
		public IEnumerator GetEnumerator()
		{
			return this.cc.GetEnumerator();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to remove from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		// Token: 0x06002F45 RID: 12101 RVA: 0x0007AC47 File Offset: 0x00078E47
		public void Remove(TableCell cell)
		{
			this.cc.Remove(cell);
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.TableCell" /> to remove from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		// Token: 0x06002F46 RID: 12102 RVA: 0x0007AC55 File Offset: 0x00078E55
		public void RemoveAt(int index)
		{
			this.cc.RemoveAt(index);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the list has a fixed size; otherwise, false</returns>
		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x00008A4D File Offset: 0x00006C4D
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
		// Token: 0x17000F15 RID: 3861
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
		// Token: 0x06002F4A RID: 12106 RVA: 0x0007AC93 File Offset: 0x00078E93
		int IList.Add(object value)
		{
			this.cc.Add((TableRow)value);
			return this.cc.IndexOf((TableRow)value);
		}

		/// <summary>Determines whether the specified object is contained within the collection.</summary>
		/// <returns>true if the object is in the collection; otherwise, false.</returns>
		/// <param name="o">The object to locate within the collection.</param>
		// Token: 0x06002F4B RID: 12107 RVA: 0x0007ACB7 File Offset: 0x00078EB7
		bool IList.Contains(object value)
		{
			return this.cc.Contains((TableRow)value);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of the object within the collection; otherwise, -1 if the object is not in the collection.</returns>
		/// <param name="o">The object to locate within the collection.</param>
		// Token: 0x06002F4C RID: 12108 RVA: 0x0007ACCA File Offset: 0x00078ECA
		int IList.IndexOf(object value)
		{
			return this.cc.IndexOf((TableRow)value);
		}

		/// <summary>Inserts an object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index within the collection at which to insert the object.</param>
		/// <param name="o">The object to insert into the collection.</param>
		// Token: 0x06002F4D RID: 12109 RVA: 0x0007ACDD File Offset: 0x00078EDD
		void IList.Insert(int index, object value)
		{
			this.cc.AddAt(index, (TableRow)value);
		}

		/// <summary>Removes an object from the collection.</summary>
		/// <param name="o">The object to remove from the collection.</param>
		// Token: 0x06002F4E RID: 12110 RVA: 0x0007ACF1 File Offset: 0x00078EF1
		void IList.Remove(object value)
		{
			this.cc.Remove((TableRow)value);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal TableCellCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001BE1 RID: 7137
		private ControlCollection cc;
	}
}
