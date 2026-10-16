using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200012A RID: 298
	internal class nsWebBrowserStream
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x00005B69 File Offset: 0x00003D69
		public static nsIWebBrowserStream GetProxy(IWebBrowser control, nsIWebBrowserStream obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWebBrowserStream).GUID, obj) as nsIWebBrowserStream;
		}
	}
}
