using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowCancelingEdit" /> event.</summary>
	// Token: 0x020002B6 RID: 694
	public class GridViewCancelEditEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewCancelEditEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row containing the Cancel button that raised the event. </param>
		// Token: 0x06001ACD RID: 6861 RVA: 0x0004351A File Offset: 0x0004171A
		public GridViewCancelEditEventArgs(int rowIndex)
		{
			this._rowIndex = rowIndex;
		}

		/// <summary>Gets the index of the row containing the Cancel button that raised the event.</summary>
		/// <returns>The zero-based index of the row containing the Cancel button that raised the event.</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00043529 File Offset: 0x00041729
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x040016D9 RID: 5849
		private int _rowIndex;
	}
}
