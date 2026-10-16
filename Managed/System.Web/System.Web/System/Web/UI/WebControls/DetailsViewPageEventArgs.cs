using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.PageIndexChanging" /> event.</summary>
	// Token: 0x020002A3 RID: 675
	public class DetailsViewPageEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewPageEventArgs" /> class.</summary>
		/// <param name="newPageIndex">The index of the new page to display.</param>
		// Token: 0x06001A93 RID: 6803 RVA: 0x000434AA File Offset: 0x000416AA
		public DetailsViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		/// <summary>Gets or sets the index of the new page to display in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The index of the new page to display in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x000434B9 File Offset: 0x000416B9
		// (set) Token: 0x06001A95 RID: 6805 RVA: 0x000434C1 File Offset: 0x000416C1
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

		// Token: 0x040016B7 RID: 5815
		private int _newPageIndex;
	}
}
