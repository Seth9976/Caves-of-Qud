using System;
using System.IO;

namespace System.Web.Mail
{
	// Token: 0x020000FB RID: 251
	internal class SmtpException : IOException
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x00023B70 File Offset: 0x00021D70
		public SmtpException(string message)
			: base(message)
		{
		}
	}
}
