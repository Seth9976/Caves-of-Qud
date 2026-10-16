using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for a cancelable event.</summary>
	// Token: 0x020002DB RID: 731
	public class LoginCancelEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> class with the <see cref="P:System.Web.UI.WebControls.LoginCancelEventArgs.Cancel" /> property set to false.</summary>
		// Token: 0x06001B53 RID: 6995 RVA: 0x0004364E File Offset: 0x0004184E
		public LoginCancelEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> class with the <see cref="P:System.Web.UI.WebControls.LoginCancelEventArgs.Cancel" /> property set to the specified value.</summary>
		/// <param name="cancel">true to cancel the event; otherwise, false.</param>
		// Token: 0x06001B54 RID: 6996 RVA: 0x00043657 File Offset: 0x00041857
		public LoginCancelEventArgs(bool cancel)
		{
			this._cancel = cancel;
		}

		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>true if the event should be canceled; otherwise, false.</returns>
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x00043666 File Offset: 0x00041866
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x0004366E File Offset: 0x0004186E
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x0400170A RID: 5898
		private bool _cancel;
	}
}
