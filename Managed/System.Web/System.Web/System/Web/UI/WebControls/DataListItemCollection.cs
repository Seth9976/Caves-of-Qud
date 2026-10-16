using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the collection of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200037F RID: 895
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataListItemCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> class.</summary>
		/// <param name="items">A <see cref="T:System.Collections.ArrayList" /> object that contains the items with which to initialize the collection. </param>
		// Token: 0x06002281 RID: 8833 RVA: 0x00057161 File Offset: 0x00055361
		public DataListItemCollection(ArrayList items)
		{
			this.list = items;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the collection.</returns>
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x00057170 File Offset: 0x00055370
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> can be modified.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataListItem" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItem" /> object in the collection at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> in the collection to retrieve. </param>
		// Token: 0x17000AFE RID: 2814
		public DataListItem this[int index]
		{
			get
			{
				return (DataListItem)this.list[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> collection to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" /> object.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> collection. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> object to receive the copied contents. </param>
		// Token: 0x06002287 RID: 8839 RVA: 0x00057190 File Offset: 0x00055390
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> interface that contains all <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> interface that contains all <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.DataListItemCollection" />.</returns>
		// Token: 0x06002288 RID: 8840 RVA: 0x0005719F File Offset: 0x0005539F
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x0400192D RID: 6445
		private ArrayList list;
	}
}
