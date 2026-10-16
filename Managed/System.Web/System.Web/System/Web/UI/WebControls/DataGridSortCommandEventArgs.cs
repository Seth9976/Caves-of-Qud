using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.SortCommand" /> event of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000292 RID: 658
	public class DataGridSortCommandEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridSortCommandEventArgs" /> class.</summary>
		/// <param name="commandSource">The source of the command. </param>
		/// <param name="dce">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06001A5B RID: 6747 RVA: 0x000433E3 File Offset: 0x000415E3
		public DataGridSortCommandEventArgs(object commandSource, DataGridCommandEventArgs dce)
		{
			this.commandSource = commandSource;
			this.sortExpression = (string)dce.CommandArgument;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>The source of the command.</returns>
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x00043403 File Offset: 0x00041603
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		/// <summary>Gets the expression used to sort the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The expression used to sort the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0004340B File Offset: 0x0004160B
		public string SortExpression
		{
			get
			{
				return this.sortExpression;
			}
		}

		// Token: 0x040016A4 RID: 5796
		private string sortExpression;

		// Token: 0x040016A5 RID: 5797
		private object commandSource;
	}
}
