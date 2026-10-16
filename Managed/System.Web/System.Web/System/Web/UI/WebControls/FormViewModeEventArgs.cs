using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event.</summary>
	// Token: 0x020002AF RID: 687
	public class FormViewModeEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewModeEventArgs" /> class.</summary>
		/// <param name="mode">One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</param>
		/// <param name="cancelingEdit">true to indicate the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, false.</param>
		// Token: 0x06001AB6 RID: 6838 RVA: 0x000434CA File Offset: 0x000416CA
		public FormViewModeEventArgs(FormViewMode mode, bool cancelingEdit)
			: base(false)
		{
			this._mode = mode;
			this._cancelingEdit = cancelingEdit;
		}

		/// <summary>Gets a value indicating whether the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event was raised as a result of the user canceling an edit operation.</summary>
		/// <returns>true to indicate the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, false.</returns>
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x000434E1 File Offset: 0x000416E1
		public bool CancelingEdit
		{
			get
			{
				return this._cancelingEdit;
			}
		}

		/// <summary>Gets or sets the mode to which the <see cref="T:System.Web.UI.WebControls.FormView" /> control is changing.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</returns>
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x000434E9 File Offset: 0x000416E9
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x000434F1 File Offset: 0x000416F1
		public FormViewMode NewMode
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

		// Token: 0x040016D1 RID: 5841
		private FormViewMode _mode;

		// Token: 0x040016D2 RID: 5842
		private bool _cancelingEdit;
	}
}
