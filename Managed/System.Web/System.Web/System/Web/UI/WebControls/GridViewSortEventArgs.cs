using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.Sorting" /> event.</summary>
	// Token: 0x020002C4 RID: 708
	public class GridViewSortEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewSortEventArgs" /> class.</summary>
		/// <param name="sortExpression">The sort expression used to sort the items in the control.</param>
		/// <param name="sortDirection">A <see cref="T:System.Web.UI.WebControls.SortDirection" /> that indicates the direction in which to sort the items in the control.</param>
		// Token: 0x06001B00 RID: 6912 RVA: 0x000435FF File Offset: 0x000417FF
		public GridViewSortEventArgs(string sortExpression, SortDirection sortDirection)
		{
			this._sortExpression = sortExpression;
			this._sortDirection = sortDirection;
		}

		/// <summary>Gets or sets the direction in which to sort the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SortDirection" /> values.</returns>
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x00043615 File Offset: 0x00041815
		// (set) Token: 0x06001B02 RID: 6914 RVA: 0x0004361D File Offset: 0x0004181D
		public SortDirection SortDirection
		{
			get
			{
				return this._sortDirection;
			}
			set
			{
				this._sortDirection = value;
			}
		}

		/// <summary>Gets or sets the expression used to sort the items in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The expression used to sort the items in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x00043626 File Offset: 0x00041826
		// (set) Token: 0x06001B04 RID: 6916 RVA: 0x0004362E File Offset: 0x0004182E
		public string SortExpression
		{
			get
			{
				return this._sortExpression;
			}
			set
			{
				this._sortExpression = value;
			}
		}

		// Token: 0x040016E1 RID: 5857
		private string _sortExpression;

		// Token: 0x040016E2 RID: 5858
		private SortDirection _sortDirection;
	}
}
