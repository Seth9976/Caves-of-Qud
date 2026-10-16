using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of designers.</summary>
	// Token: 0x02000453 RID: 1107
	public class DesignerCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified designers.</summary>
		/// <param name="designers">An array of <see cref="T:System.ComponentModel.Design.IDesignerHost" /> objects to store. </param>
		// Token: 0x060023EC RID: 9196 RVA: 0x00081774 File Offset: 0x0007F974
		public DesignerCollection(IDesignerHost[] designers)
		{
			if (designers != null)
			{
				this._designers = new ArrayList(designers);
				return;
			}
			this._designers = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified set of designers.</summary>
		/// <param name="designers">A list that contains the collection of designers to add. </param>
		// Token: 0x060023ED RID: 9197 RVA: 0x00081797 File Offset: 0x0007F997
		public DesignerCollection(IList designers)
		{
			this._designers = designers;
		}

		/// <summary>Gets the number of designers in the collection.</summary>
		/// <returns>The number of designers in the collection.</returns>
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x000817A6 File Offset: 0x0007F9A6
		public int Count
		{
			get
			{
				return this._designers.Count;
			}
		}

		/// <summary>Gets the designer at the specified index.</summary>
		/// <returns>The designer at the specified index.</returns>
		/// <param name="index">The index of the designer to return. </param>
		// Token: 0x17000744 RID: 1860
		public virtual IDesignerHost this[int index]
		{
			get
			{
				return (IDesignerHost)this._designers[index];
			}
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x060023F0 RID: 9200 RVA: 0x000817C6 File Offset: 0x0007F9C6
		public IEnumerator GetEnumerator()
		{
			return this._designers.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000817D3 File Offset: 0x0007F9D3
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x00002F6A File Offset: 0x0000116A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x060023F4 RID: 9204 RVA: 0x000817DB File Offset: 0x0007F9DB
		void ICollection.CopyTo(Array array, int index)
		{
			this._designers.CopyTo(array, index);
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x060023F5 RID: 9205 RVA: 0x000817EA File Offset: 0x0007F9EA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040010E6 RID: 4326
		private IList _designers;
	}
}
