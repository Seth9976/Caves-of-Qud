using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	// Token: 0x02000389 RID: 905
	public class DetailsViewRowCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> class using the specified <see cref="T:System.Collections.ArrayList" /> object.</summary>
		/// <param name="rows">An <see cref="T:System.Collections.ArrayList" /> that contains the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects with which to initialize the collection.</param>
		// Token: 0x06002379 RID: 9081 RVA: 0x0005A481 File Offset: 0x00058681
		public DetailsViewRowCollection(ArrayList rows)
		{
			this.rows = rows;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> at the specified index in the collection.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> to retrieve from the collection.</param>
		// Token: 0x17000B53 RID: 2899
		public DetailsViewRow this[int index]
		{
			get
			{
				return (DetailsViewRow)this.rows[index];
			}
		}

		/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the copied contents.</param>
		// Token: 0x0600237B RID: 9083 RVA: 0x0005A4AE File Offset: 0x000586AE
		public void CopyTo(DetailsViewRow[] array, int index)
		{
			this.rows.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that contains all <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</returns>
		// Token: 0x0600237C RID: 9084 RVA: 0x0005A4BD File Offset: 0x000586BD
		public IEnumerator GetEnumerator()
		{
			return this.rows.GetEnumerator();
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object.</summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</returns>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x0005A4CA File Offset: 0x000586CA
		public int Count
		{
			get
			{
				return this.rows.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object is synchronized (thread safe).</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object used to synchronize access to the collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the rows in the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object can be modified.</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Copies all the items from this <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> object to the specified <see cref="T:System.Array" /> object, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" />.</param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the copied contents.</param>
		// Token: 0x06002381 RID: 9089 RVA: 0x0005A4AE File Offset: 0x000586AE
		void ICollection.CopyTo(Array array, int index)
		{
			this.rows.CopyTo(array, index);
		}

		// Token: 0x04001980 RID: 6528
		private ArrayList rows = new ArrayList();
	}
}
