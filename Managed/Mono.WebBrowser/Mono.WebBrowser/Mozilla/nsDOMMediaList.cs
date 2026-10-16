using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000C0 RID: 192
	internal class nsDOMMediaList
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x000053C0 File Offset: 0x000035C0
		public static nsIDOMMediaList GetProxy(IWebBrowser control, nsIDOMMediaList obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMMediaList).GUID, obj) as nsIDOMMediaList;
		}
	}
}
