using System;
using System.IO;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000045 RID: 69
	internal abstract class MimeReturnWriter : MimeFormatter
	{
		// Token: 0x0600017D RID: 381
		internal abstract void Write(HttpResponse response, Stream outputStream, object returnValue);
	}
}
