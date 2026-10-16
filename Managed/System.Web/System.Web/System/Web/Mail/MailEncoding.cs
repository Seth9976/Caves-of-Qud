using System;

namespace System.Web.Mail
{
	/// <summary>Provides enumerated values for e-mail encoding. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	// Token: 0x020000F2 RID: 242
	[Obsolete("The recommended alternative is System.Net.Mime.TransferEncoding. http://go.microsoft.com/fwlink/?linkid=14202")]
	public enum MailEncoding
	{
		/// <summary>Specifies that the e-mail message uses UUEncode encoding.</summary>
		// Token: 0x04001129 RID: 4393
		UUEncode,
		/// <summary>Specifies that the e-mail message uses Base64 encoding.</summary>
		// Token: 0x0400112A RID: 4394
		Base64
	}
}
