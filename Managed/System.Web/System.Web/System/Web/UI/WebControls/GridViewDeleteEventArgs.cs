using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowDeleting" /> event.</summary>
	// Token: 0x020003A1 RID: 929
	public class GridViewDeleteEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewDeleteEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row that contains the Delete button that raised the event. </param>
		// Token: 0x060025E2 RID: 9698 RVA: 0x00061BAF File Offset: 0x0005FDAF
		public GridViewDeleteEventArgs(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x00061BBE File Offset: 0x0005FDBE
		internal GridViewDeleteEventArgs(int index, IOrderedDictionary keys, IOrderedDictionary values)
		{
			this.rowIndex = index;
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the index of the row being deleted.</summary>
		/// <returns>The zero-based index of the row being deleted.</returns>
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x00061BDB File Offset: 0x0005FDDB
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets a dictionary of field name/value pairs that represent the primary key of the row to delete.</summary>
		/// <returns>A dictionary that contains field name/value pairs that represent the primary key of the row to delete.</returns>
		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x00061BE3 File Offset: 0x0005FDE3
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the row to delete.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains the non-key field name/value pairs of the row to delete.</returns>
		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x00061BEB File Offset: 0x0005FDEB
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001A2D RID: 6701
		private int rowIndex;

		// Token: 0x04001A2E RID: 6702
		private IOrderedDictionary keys;

		// Token: 0x04001A2F RID: 6703
		private IOrderedDictionary values;
	}
}
