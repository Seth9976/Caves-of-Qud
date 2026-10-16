using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000012 RID: 18
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00009")]
	public class BadCrcException : ZipException
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000024F1 File Offset: 0x000006F1
		public BadCrcException()
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000024F9 File Offset: 0x000006F9
		public BadCrcException(string message)
			: base(message)
		{
		}
	}
}
