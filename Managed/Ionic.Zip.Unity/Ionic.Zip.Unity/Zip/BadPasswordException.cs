using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000010 RID: 16
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000B")]
	public class BadPasswordException : ZipException
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000024BB File Offset: 0x000006BB
		public BadPasswordException()
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000024C3 File Offset: 0x000006C3
		public BadPasswordException(string message)
			: base(message)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000024CC File Offset: 0x000006CC
		public BadPasswordException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
