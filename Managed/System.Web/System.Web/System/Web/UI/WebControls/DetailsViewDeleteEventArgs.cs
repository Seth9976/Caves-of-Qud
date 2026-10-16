using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemDeleting" /> event. </summary>
	// Token: 0x02000383 RID: 899
	public class DetailsViewDeleteEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewDeleteEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row being deleted.</param>
		// Token: 0x06002357 RID: 9047 RVA: 0x0005A2A5 File Offset: 0x000584A5
		public DetailsViewDeleteEventArgs(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0005A2B4 File Offset: 0x000584B4
		internal DetailsViewDeleteEventArgs(int index, IOrderedDictionary keys, IOrderedDictionary values)
		{
			this.rowIndex = index;
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the index of the row being deleted.</summary>
		/// <returns>The index of the row being deleted.</returns>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x0005A2D1 File Offset: 0x000584D1
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets an ordered dictionary of key field name/value pairs that contains the names and values of the key fields of the deleted items.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains an ordered dictionary of key field name/value pairs used to match the item to delete.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x0005A2D9 File Offset: 0x000584D9
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the item to delete.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the item to delete.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x0005A2E1 File Offset: 0x000584E1
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x0400196D RID: 6509
		private int rowIndex;

		// Token: 0x0400196E RID: 6510
		private IOrderedDictionary keys;

		// Token: 0x0400196F RID: 6511
		private IOrderedDictionary values;
	}
}
