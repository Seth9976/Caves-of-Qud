using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.HtmlControls
{
	/// <summary>A collection of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects that represent the rows of an <see cref="T:System.Web.UI.HtmlControls.HtmlTable" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000272 RID: 626
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HtmlTableRowCollection : ICollection, IEnumerable
	{
		// Token: 0x060019CE RID: 6606 RVA: 0x00042BE9 File Offset: 0x00040DE9
		internal HtmlTableRowCollection(HtmlTable table)
		{
			this.cc = table.Controls;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. The default value is 0.</returns>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00042BFD File Offset: 0x00040DFD
		public int Count
		{
			get
			{
				return this.cc.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases, which indicates that access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> is not synchronized (not thread safe).</returns>
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> that represents a row contained in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />.</returns>
		/// <param name="index">An ordinal index value that specifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to return. </param>
		// Token: 0x17000835 RID: 2101
		public HtmlTableRow this[int index]
		{
			get
			{
				return (HtmlTableRow)this.cc[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object to the end of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to add to the collection. </param>
		// Token: 0x060019D4 RID: 6612 RVA: 0x00042C1D File Offset: 0x00040E1D
		public void Add(HtmlTableRow row)
		{
			this.cc.Add(row);
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		// Token: 0x060019D5 RID: 6613 RVA: 0x00042C2B File Offset: 0x00040E2B
		public void Clear()
		{
			this.cc.Clear();
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection to the specified <see cref="T:System.Array" /> object, starting at the specified index in the array.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
		/// <param name="index">The first index in the specified array to receive the items. </param>
		// Token: 0x060019D6 RID: 6614 RVA: 0x00042C38 File Offset: 0x00040E38
		public void CopyTo(Array array, int index)
		{
			this.cc.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> objects in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />.</returns>
		// Token: 0x060019D7 RID: 6615 RVA: 0x00042C47 File Offset: 0x00040E47
		public IEnumerator GetEnumerator()
		{
			return this.cc.GetEnumerator();
		}

		/// <summary>Adds an <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object to the specified location in the collection.</summary>
		/// <param name="index">The location in the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> at which to add the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" />. </param>
		/// <param name="row">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to add to the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
		// Token: 0x060019D8 RID: 6616 RVA: 0x00042C54 File Offset: 0x00040E54
		public void Insert(int index, HtmlTableRow row)
		{
			this.cc.AddAt(index, row);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
		// Token: 0x060019D9 RID: 6617 RVA: 0x00042C63 File Offset: 0x00040E63
		public void Remove(HtmlTableRow row)
		{
			this.cc.Remove(row);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> object at the specified index from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" /> collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRow" /> to remove from the <see cref="T:System.Web.UI.HtmlControls.HtmlTableRowCollection" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is outside the range of index values in the collection. </exception>
		// Token: 0x060019DA RID: 6618 RVA: 0x00042C71 File Offset: 0x00040E71
		public void RemoveAt(int index)
		{
			this.cc.RemoveAt(index);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal HtmlTableRowCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001641 RID: 5697
		private ControlCollection cc;
	}
}
