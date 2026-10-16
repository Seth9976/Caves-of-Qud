using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000014 RID: 20
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00007")]
	public class BadStateException : ZipException
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002513 File Offset: 0x00000713
		public BadStateException()
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000251B File Offset: 0x0000071B
		public BadStateException(string message)
			: base(message)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002524 File Offset: 0x00000724
		public BadStateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
