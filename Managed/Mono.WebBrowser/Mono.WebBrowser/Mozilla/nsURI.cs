using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200011A RID: 282
	internal class nsURI
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x00005A41 File Offset: 0x00003C41
		public static nsIURI GetProxy(IWebBrowser control, nsIURI obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIURI).GUID, obj) as nsIURI;
		}
	}
}
