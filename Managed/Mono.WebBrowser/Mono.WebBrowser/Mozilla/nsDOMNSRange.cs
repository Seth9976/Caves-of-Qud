using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000C6 RID: 198
	internal class nsDOMNSRange
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x0000542F File Offset: 0x0000362F
		public static nsIDOMNSRange GetProxy(IWebBrowser control, nsIDOMNSRange obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMNSRange).GUID, obj) as nsIDOMNSRange;
		}
	}
}
