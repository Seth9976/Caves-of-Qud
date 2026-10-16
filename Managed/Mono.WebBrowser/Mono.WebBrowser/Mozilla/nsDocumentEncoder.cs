using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000E6 RID: 230
	internal class nsDocumentEncoder
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x0000567F File Offset: 0x0000387F
		public static nsIDocumentEncoder GetProxy(IWebBrowser control, nsIDocumentEncoder obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDocumentEncoder).GUID, obj) as nsIDocumentEncoder;
		}
	}
}
