using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection that contains the key field of each record in a data source. This class cannot be inherited.</summary>
	// Token: 0x0200037C RID: 892
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataKeyCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> class.</summary>
		/// <param name="keys">A <see cref="T:System.Collections.ArrayList" /> that contains key fields from the data source. </param>
		// Token: 0x06002212 RID: 8722 RVA: 0x00055C40 File Offset: 0x00053E40
		public DataKeyCollection(ArrayList keys)
		{
			this.list = keys;
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x00055C4F File Offset: 0x00053E4F
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether items in the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> can be modified.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the key field at the specified index in the collection.</summary>
		/// <returns>The key field at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the key field to retrieve from the collection. </param>
		// Token: 0x17000AD1 RID: 2769
		public object this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		/// <summary>Gets the object used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" /> object.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> object to receive the copied contents. </param>
		// Token: 0x06002218 RID: 8728 RVA: 0x00055C6A File Offset: 0x00053E6A
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Creates a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all key fields in the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all key fields in the <see cref="T:System.Web.UI.WebControls.DataKeyCollection" />.</returns>
		// Token: 0x06002219 RID: 8729 RVA: 0x00055C79 File Offset: 0x00053E79
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x0400190C RID: 6412
		private ArrayList list;
	}
}
