using System;

namespace System.Net
{
	// Token: 0x02000695 RID: 1685
	internal class HttpRequestCreator : IWebRequestCreate
	{
		// Token: 0x0600356F RID: 13679 RVA: 0x0000219B File Offset: 0x0000039B
		internal HttpRequestCreator()
		{
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000BABEA File Offset: 0x000B8DEA
		public WebRequest Create(Uri uri)
		{
			return new HttpWebRequest(uri);
		}
	}
}
