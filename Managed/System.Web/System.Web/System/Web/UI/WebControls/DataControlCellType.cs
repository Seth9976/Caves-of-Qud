using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Describes the function of a <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object. </summary>
	// Token: 0x02000289 RID: 649
	public enum DataControlCellType
	{
		/// <summary>A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that is the header cell of a table. Items in the cell are not bound to data.</summary>
		// Token: 0x0400168F RID: 5775
		Header,
		/// <summary>A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that is the footer cell of a table. Items in the cell are not bound to data.</summary>
		// Token: 0x04001690 RID: 5776
		Footer,
		/// <summary>A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains data. </summary>
		// Token: 0x04001691 RID: 5777
		DataCell
	}
}
