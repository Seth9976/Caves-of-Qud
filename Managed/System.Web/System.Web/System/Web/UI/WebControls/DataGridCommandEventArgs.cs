using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.CancelCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.DeleteCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.EditCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCommand" />, and <see cref="E:System.Web.UI.WebControls.DataGrid.UpdateCommand" /> events of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200028C RID: 652
	public class DataGridCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> class.</summary>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" />. </param>
		/// <param name="commandSource">The source of the command. </param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06001A47 RID: 6727 RVA: 0x0004337F File Offset: 0x0004157F
		public DataGridCommandEventArgs(DataGridItem item, object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>The source of the command.</returns>
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00043396 File Offset: 0x00041596
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		/// <summary>Gets the item containing the command source in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x0004339E File Offset: 0x0004159E
		public DataGridItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x0400169F RID: 5791
		private DataGridItem item;

		// Token: 0x040016A0 RID: 5792
		private object commandSource;
	}
}
