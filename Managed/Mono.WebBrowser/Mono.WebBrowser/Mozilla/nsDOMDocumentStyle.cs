using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000A4 RID: 164
	internal class nsDOMDocumentStyle
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x000051BA File Offset: 0x000033BA
		public static nsIDOMDocumentStyle GetProxy(IWebBrowser control, nsIDOMDocumentStyle obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDocumentStyle).GUID, obj) as nsIDOMDocumentStyle;
		}
	}
}
