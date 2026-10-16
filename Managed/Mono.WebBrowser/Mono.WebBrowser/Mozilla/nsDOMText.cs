using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000DA RID: 218
	internal class nsDOMText
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x000055A1 File Offset: 0x000037A1
		public static nsIDOMText GetProxy(IWebBrowser control, nsIDOMText obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMText).GUID, obj) as nsIDOMText;
		}
	}
}
