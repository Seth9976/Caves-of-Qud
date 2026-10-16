using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowUpdating" /> event.</summary>
	// Token: 0x020003A5 RID: 933
	public class GridViewUpdateEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewUpdateEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row being updated.</param>
		// Token: 0x06002605 RID: 9733 RVA: 0x00061D6C File Offset: 0x0005FF6C
		public GridViewUpdateEventArgs(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00061D7B File Offset: 0x0005FF7B
		internal GridViewUpdateEventArgs(int rowIndex, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
		{
			this.rowIndex = rowIndex;
			this.keys = keys;
			this.newValues = newValues;
			this.oldValues = oldValues;
		}

		/// <summary>Gets the index of the row being updated.</summary>
		/// <returns>The index of the row being updated.</returns>
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x00061DA0 File Offset: 0x0005FFA0
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets a dictionary of field name/value pairs that represent the primary key of the row to update.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object containing field name/value pairs that represent the primary key of the row to update.</returns>
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x00061DA8 File Offset: 0x0005FFA8
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary containing the revised values of the non-key field name/value pairs in the row to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object containing the revised values of the non-key field name/value pairs in the row to update.</returns>
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x00061DB0 File Offset: 0x0005FFB0
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary containing the original field name/value pairs in the row to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains the original values of the field name/value pairs in the row to update.</returns>
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x00061DB8 File Offset: 0x0005FFB8
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x04001A3B RID: 6715
		private int rowIndex;

		// Token: 0x04001A3C RID: 6716
		private IOrderedDictionary keys;

		// Token: 0x04001A3D RID: 6717
		private IOrderedDictionary newValues;

		// Token: 0x04001A3E RID: 6718
		private IOrderedDictionary oldValues;
	}
}
