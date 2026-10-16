using System;

namespace System.Web.Mail
{
	/// <summary>Provides enumerated values for e-mail format. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	// Token: 0x020000F3 RID: 243
	[Obsolete("The recommended alternative is System.Net.Mail.MailMessage.IsBodyHtml. http://go.microsoft.com/fwlink/?linkid=14202")]
	public enum MailFormat
	{
		/// <summary>Specifies that the e-mail format is plain text.</summary>
		// Token: 0x0400112C RID: 4396
		Text,
		/// <summary>Specifies that the e-mail format is HTML.</summary>
		// Token: 0x0400112D RID: 4397
		Html
	}
}
