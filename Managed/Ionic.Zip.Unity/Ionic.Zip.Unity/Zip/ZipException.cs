using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x0200000F RID: 15
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00006")]
	public class ZipException : Exception
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000024A0 File Offset: 0x000006A0
		public ZipException()
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000024A8 File Offset: 0x000006A8
		public ZipException(string message)
			: base(message)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000024B1 File Offset: 0x000006B1
		public ZipException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
