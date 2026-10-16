using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowCreated" /> and <see cref="E:System.Web.UI.WebControls.GridView.RowDataBound" /> events.</summary>
	// Token: 0x020002C0 RID: 704
	public class GridViewRowEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewRowEventArgs" /> class.</summary>
		/// <param name="row">A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the row being created or data-bound. </param>
		// Token: 0x06001AF3 RID: 6899 RVA: 0x000435C8 File Offset: 0x000417C8
		public GridViewRowEventArgs(GridViewRow row)
		{
			this._row = row;
		}

		/// <summary>Gets the row being created or data-bound.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the row being created or data-bound.</returns>
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x000435D7 File Offset: 0x000417D7
		public GridViewRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x040016DF RID: 5855
		private GridViewRow _row;
	}
}
