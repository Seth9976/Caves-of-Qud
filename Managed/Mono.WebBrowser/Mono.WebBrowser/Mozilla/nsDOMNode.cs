using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000CA RID: 202
	internal class nsDOMNode
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x00005479 File Offset: 0x00003679
		public static nsIDOMNode GetProxy(IWebBrowser control, nsIDOMNode obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMNode).GUID, obj) as nsIDOMNode;
		}
	}
}
