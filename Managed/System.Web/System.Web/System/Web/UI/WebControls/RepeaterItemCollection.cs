using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003FB RID: 1019
	public sealed class RepeaterItemCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> class.</summary>
		/// <param name="items">A <see cref="T:System.Collections.ArrayList" /> that contains the items with which to initialize the collection.</param>
		// Token: 0x06002D3C RID: 11580 RVA: 0x000760D2 File Offset: 0x000742D2
		public RepeaterItemCollection(ArrayList items)
		{
			this.l = items;
		}

		/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" /> object.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" />. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
		// Token: 0x06002D3D RID: 11581 RVA: 0x000760E1 File Offset: 0x000742E1
		public void CopyTo(Array array, int index)
		{
			this.l.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> interface that can iterate through all the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> interface that contains all <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" />.</returns>
		// Token: 0x06002D3E RID: 11582 RVA: 0x000760F0 File Offset: 0x000742F0
		public IEnumerator GetEnumerator()
		{
			return this.l.GetEnumerator();
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the collection.</returns>
		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000760FD File Offset: 0x000742FD
		public int Count
		{
			get
			{
				return this.l.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> can be modified.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> to retrieve in the collection.</param>
		// Token: 0x17000E82 RID: 3714
		public RepeaterItem this[int index]
		{
			get
			{
				return (RepeaterItem)this.l[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x0007611D File Offset: 0x0007431D
		public object SyncRoot
		{
			get
			{
				return this.l.SyncRoot;
			}
		}

		// Token: 0x04001B75 RID: 7029
		private ArrayList l;
	}
}
