using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000CC RID: 204
	internal class nsDOMNodeList
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0000549E File Offset: 0x0000369E
		public static nsIDOMNodeList GetProxy(IWebBrowser control, nsIDOMNodeList obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMNodeList).GUID, obj) as nsIDOMNodeList;
		}
	}
}
