using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the pager row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	// Token: 0x0200039C RID: 924
	public class FormViewPagerRow : FormViewRow, INamingContainer, INonBindingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewPagerRow" /> class.</summary>
		/// <param name="rowIndex">The index of the row in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
		// Token: 0x060024E2 RID: 9442 RVA: 0x0005E097 File Offset: 0x0005C297
		public FormViewPagerRow(int rowIndex, DataControlRowType rowType, DataControlRowState rowState)
			: base(rowIndex, rowType, rowState)
		{
		}
	}
}
