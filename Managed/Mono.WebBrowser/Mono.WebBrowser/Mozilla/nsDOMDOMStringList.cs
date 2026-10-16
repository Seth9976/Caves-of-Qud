using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200009A RID: 154
	internal class nsDOMDOMStringList
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x00005101 File Offset: 0x00003301
		public static nsIDOMDOMStringList GetProxy(IWebBrowser control, nsIDOMDOMStringList obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDOMStringList).GUID, obj) as nsIDOMDOMStringList;
		}
	}
}
