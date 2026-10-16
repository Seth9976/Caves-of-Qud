using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.DataGrid.ItemDataBound" /> events of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200028E RID: 654
	public class DataGridItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridItemEventArgs" /> class.</summary>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that represents an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" />. </param>
		// Token: 0x06001A4E RID: 6734 RVA: 0x000433A6 File Offset: 0x000415A6
		public DataGridItemEventArgs(DataGridItem item)
		{
			this.item = item;
		}

		/// <summary>Gets the referenced item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when the event is raised.</summary>
		/// <returns>The referenced item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when the event is raised.</returns>
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x000433B5 File Offset: 0x000415B5
		public DataGridItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x040016A1 RID: 5793
		private DataGridItem item;
	}
}
