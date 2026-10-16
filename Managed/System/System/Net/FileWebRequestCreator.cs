using System;

namespace System.Net
{
	// Token: 0x0200065B RID: 1627
	internal class FileWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x0600334B RID: 13131 RVA: 0x0000219B File Offset: 0x0000039B
		internal FileWebRequestCreator()
		{
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000B2EE4 File Offset: 0x000B10E4
		public WebRequest Create(Uri uri)
		{
			return new FileWebRequest(uri);
		}
	}
}
