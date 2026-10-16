using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000A2 RID: 162
	internal class nsDOMDocumentRange
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00005195 File Offset: 0x00003395
		public static nsIDOMDocumentRange GetProxy(IWebBrowser control, nsIDOMDocumentRange obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDocumentRange).GUID, obj) as nsIDOMDocumentRange;
		}
	}
}
