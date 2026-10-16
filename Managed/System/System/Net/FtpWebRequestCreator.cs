using System;

namespace System.Net
{
	// Token: 0x02000595 RID: 1429
	internal class FtpWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x06002E62 RID: 11874 RVA: 0x0000219B File Offset: 0x0000039B
		internal FtpWebRequestCreator()
		{
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000A08FD File Offset: 0x0009EAFD
		public WebRequest Create(Uri uri)
		{
			return new FtpWebRequest(uri);
		}
	}
}
