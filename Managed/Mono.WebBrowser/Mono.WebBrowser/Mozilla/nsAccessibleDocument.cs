using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000070 RID: 112
	internal class nsAccessibleDocument
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public static nsIAccessibleDocument GetProxy(IWebBrowser control, nsIAccessibleDocument obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIAccessibleDocument).GUID, obj) as nsIAccessibleDocument;
		}
	}
}
