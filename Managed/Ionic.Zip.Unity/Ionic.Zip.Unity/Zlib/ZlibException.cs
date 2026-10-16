using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x02000064 RID: 100
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	public class ZlibException : Exception
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0001F909 File Offset: 0x0001DB09
		public ZlibException()
		{
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001F911 File Offset: 0x0001DB11
		public ZlibException(string s)
			: base(s)
		{
		}
	}
}
