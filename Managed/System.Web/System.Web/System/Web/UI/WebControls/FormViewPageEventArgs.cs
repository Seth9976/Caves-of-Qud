using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.PageIndexChanging" /> event.</summary>
	// Token: 0x020002B1 RID: 689
	public class FormViewPageEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewPageEventArgs" /> class.</summary>
		/// <param name="newPageIndex">The index of the new page to display.</param>
		// Token: 0x06001ABE RID: 6846 RVA: 0x000434FA File Offset: 0x000416FA
		public FormViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		/// <summary>Gets or sets the index of the new page to display in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The index of the new page to display in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00043509 File Offset: 0x00041709
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x00043511 File Offset: 0x00041711
		public int NewPageIndex
		{
			get
			{
				return this._newPageIndex;
			}
			set
			{
				this._newPageIndex = value;
			}
		}

		// Token: 0x040016D3 RID: 5843
		private int _newPageIndex;
	}
}
