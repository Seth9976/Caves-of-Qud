using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the SendMailError event of controls such as the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control, the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control, and the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. </summary>
	// Token: 0x02000401 RID: 1025
	public class SendMailErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> class. </summary>
		/// <param name="e">An <see cref="T:System.Exception" /> object containing the exception.</param>
		// Token: 0x06002D70 RID: 11632 RVA: 0x000765C9 File Offset: 0x000747C9
		public SendMailErrorEventArgs(Exception e)
		{
			this.exception = e;
			this.exceptionHandled = true;
		}

		/// <summary>Returns the exception thrown by an SMTP mail service when an e-mail message cannot be sent.</summary>
		/// <returns>An <see cref="T:System.Exception" /> object that contains the exception.</returns>
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000765DF File Offset: 0x000747DF
		// (set) Token: 0x06002D72 RID: 11634 RVA: 0x000765E7 File Offset: 0x000747E7
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		/// <summary>Indicates if the SMTP exception that is contained in the <see cref="P:System.Web.UI.WebControls.SendMailErrorEventArgs.Exception" /> property has been handled.</summary>
		/// <returns>If true, the exception is consumed and handled by the <see cref="T:System.Web.UI.WebControls.SendMailErrorEventHandler" /> delegate. If false, the exception is rethrown, including the original call stack and error message.The default is false.</returns>
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x000765F0 File Offset: 0x000747F0
		// (set) Token: 0x06002D74 RID: 11636 RVA: 0x000765F8 File Offset: 0x000747F8
		public bool Handled
		{
			get
			{
				return this.exceptionHandled;
			}
			set
			{
				this.exceptionHandled = value;
			}
		}

		// Token: 0x04001B7A RID: 7034
		private Exception exception;

		// Token: 0x04001B7B RID: 7035
		private bool exceptionHandled;
	}
}
