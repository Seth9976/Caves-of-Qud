using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the function of a row in a data control, such as a <see cref="T:System.Web.UI.WebControls.DetailsView" /> or <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	// Token: 0x0200028B RID: 651
	public enum DataControlRowType
	{
		/// <summary>A header row of a data control. Header rows cannot be data-bound.</summary>
		// Token: 0x04001699 RID: 5785
		Header,
		/// <summary>A footer row of a data control. Footer rows cannot be data-bound.</summary>
		// Token: 0x0400169A RID: 5786
		Footer,
		/// <summary>A data row of a data control. Only <see cref="F:System.Web.UI.WebControls.DataControlRowType.DataRow" /> rows can be data-bound.</summary>
		// Token: 0x0400169B RID: 5787
		DataRow,
		/// <summary>A row separator. Row separators cannot be data-bound.</summary>
		// Token: 0x0400169C RID: 5788
		Separator,
		/// <summary>A row that displays pager buttons or a pager control. Pager rows cannot be data-bound.</summary>
		// Token: 0x0400169D RID: 5789
		Pager,
		/// <summary>The empty row of a data-bound control. The empty row is displayed when the data-bound control has no records to display and the EmptyDataTemplate template is not null.</summary>
		// Token: 0x0400169E RID: 5790
		EmptyDataRow
	}
}
