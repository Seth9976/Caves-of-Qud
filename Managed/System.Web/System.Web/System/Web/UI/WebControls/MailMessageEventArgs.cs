using System;
using System.Net.Mail;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for e-mail–related events.</summary>
	// Token: 0x020002E0 RID: 736
	public class MailMessageEventArgs : LoginCancelEventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> class.</summary>
		/// <param name="message">The <see cref="T:System.Net.Mail.MailMessage" /> containing the message.</param>
		// Token: 0x06001B5B RID: 7003 RVA: 0x00043677 File Offset: 0x00041877
		public MailMessageEventArgs(MailMessage message)
		{
			this._message = message;
		}

		/// <summary>Gets the e-mail message contents.</summary>
		/// <returns>A <see cref="T:System.Web.Mail.MailMessage" /> containing the message contents.</returns>
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x00043686 File Offset: 0x00041886
		public MailMessage Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x04001715 RID: 5909
		private MailMessage _message;
	}
}
