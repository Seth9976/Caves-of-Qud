using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event.</summary>
	// Token: 0x020002A1 RID: 673
	public class DetailsViewModeEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewModeEventArgs" /> class.</summary>
		/// <param name="mode">One of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> enumeration values.</param>
		/// <param name="cancelingEdit">true to indicate the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, false.</param>
		// Token: 0x06001A8B RID: 6795 RVA: 0x0004347A File Offset: 0x0004167A
		public DetailsViewModeEventArgs(DetailsViewMode mode, bool cancelingEdit)
			: base(false)
		{
			this._mode = mode;
			this._cancelingEdit = cancelingEdit;
		}

		/// <summary>Gets a value indicating whether the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event was raised as a result of the user canceling an edit operation.</summary>
		/// <returns>true to indicate the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, false.</returns>
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00043491 File Offset: 0x00041691
		public bool CancelingEdit
		{
			get
			{
				return this._cancelingEdit;
			}
		}

		/// <summary>Gets or sets the mode to which the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is changing.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> enumeration values.</returns>
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x00043499 File Offset: 0x00041699
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x000434A1 File Offset: 0x000416A1
		public DetailsViewMode NewMode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		// Token: 0x040016B5 RID: 5813
		private DetailsViewMode _mode;

		// Token: 0x040016B6 RID: 5814
		private bool _cancelingEdit;
	}
}
