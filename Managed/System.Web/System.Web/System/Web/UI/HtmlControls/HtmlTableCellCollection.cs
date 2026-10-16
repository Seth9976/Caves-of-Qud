using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.HtmlControls
{
	/// <summary>A collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects that represent the cells in a single row of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200026F RID: 623
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HtmlTableCellCollection : ICollection, IEnumerable
	{
		// Token: 0x060019A9 RID: 6569 RVA: 0x0004296C File Offset: 0x00040B6C
		internal HtmlTableCellCollection(HtmlTableRow tr)
		{
			this.cc = tr.Controls;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. The default value is 0.</returns>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x00042980 File Offset: 0x00040B80
		public int Count
		{
			get
			{
				return this.cc.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection is read-only.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection is synchronized (thread safe).</summary>
		/// <returns>false for all cases, which indicates that access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> is not synchronized (not thread safe).</returns>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> that represents a cell contained in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />.</returns>
		/// <param name="index">An ordinal index value that specifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to return. </param>
		// Token: 0x17000827 RID: 2087
		public HtmlTableCell this[int index]
		{
			get
			{
				return (HtmlTableCell)this.cc[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object to the end of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to add to the collection. </param>
		// Token: 0x060019AF RID: 6575 RVA: 0x000429A0 File Offset: 0x00040BA0
		public void Add(HtmlTableCell cell)
		{
			this.cc.Add(cell);
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
		// Token: 0x060019B0 RID: 6576 RVA: 0x000429AE File Offset: 0x00040BAE
		public void Clear()
		{
			this.cc.Clear();
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection to the specified <see cref="T:System.Array" />, beginning with the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. </param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the items. </param>
		// Token: 0x060019B1 RID: 6577 RVA: 0x000429BB File Offset: 0x00040BBB
		public void CopyTo(Array array, int index)
		{
			this.cc.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />.</returns>
		// Token: 0x060019B2 RID: 6578 RVA: 0x000429CA File Offset: 0x00040BCA
		public IEnumerator GetEnumerator()
		{
			return this.cc.GetEnumerator();
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object at the specified index location of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
		/// <param name="index">The location in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> at which to add the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" />. </param>
		/// <param name="cell">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to add to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. </param>
		// Token: 0x060019B3 RID: 6579 RVA: 0x000429D7 File Offset: 0x00040BD7
		public void Insert(int index, HtmlTableCell cell)
		{
			this.cc.AddAt(index, cell);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. </param>
		// Token: 0x060019B4 RID: 6580 RVA: 0x000429E6 File Offset: 0x00040BE6
		public void Remove(HtmlTableCell cell)
		{
			this.cc.Remove(cell);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" /> collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCell" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableCellCollection" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is outside the range of index values in the collection. </exception>
		// Token: 0x060019B5 RID: 6581 RVA: 0x000429F4 File Offset: 0x00040BF4
		public void RemoveAt(int index)
		{
			this.cc.RemoveAt(index);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal HtmlTableCellCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400163F RID: 5695
		private ControlCollection cc;
	}
}
