using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000F6 RID: 246
	internal class nsLoadGroup
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x000057A7 File Offset: 0x000039A7
		public static nsILoadGroup GetProxy(IWebBrowser control, nsILoadGroup obj)
		{
			return Base.GetProxyForObject(control, typeof(nsILoadGroup).GUID, obj) as nsILoadGroup;
		}
	}
}
