using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.PageIndexChanged" /> event of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000290 RID: 656
	public class DataGridPageChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridPageChangedEventArgs" /> class.</summary>
		/// <param name="commandSource">The source of the command. </param>
		/// <param name="newPageIndex">The index of the page selected by the user from the page selection element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. </param>
		// Token: 0x06001A54 RID: 6740 RVA: 0x000433BD File Offset: 0x000415BD
		public DataGridPageChangedEventArgs(object commandSource, int newPageIndex)
		{
			this.commandSource = commandSource;
			this.newPageIndex = newPageIndex;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>The source of the command.</returns>
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x000433D3 File Offset: 0x000415D3
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		/// <summary>Gets the index of the page selected by the user in the page selection element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The index of the page selected by the user in the page selection element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x000433DB File Offset: 0x000415DB
		public int NewPageIndex
		{
			get
			{
				return this.newPageIndex;
			}
		}

		// Token: 0x040016A2 RID: 5794
		private object commandSource;

		// Token: 0x040016A3 RID: 5795
		private int newPageIndex;
	}
}
