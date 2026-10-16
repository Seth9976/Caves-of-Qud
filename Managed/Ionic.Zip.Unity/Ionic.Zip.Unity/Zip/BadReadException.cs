using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000011 RID: 17
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000A")]
	public class BadReadException : ZipException
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000024D6 File Offset: 0x000006D6
		public BadReadException()
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000024DE File Offset: 0x000006DE
		public BadReadException(string message)
			: base(message)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000024E7 File Offset: 0x000006E7
		public BadReadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
