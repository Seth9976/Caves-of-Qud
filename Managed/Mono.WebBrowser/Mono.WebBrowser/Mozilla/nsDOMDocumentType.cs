using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000A6 RID: 166
	internal class nsDOMDocumentType
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x000051DF File Offset: 0x000033DF
		public static nsIDOMDocumentType GetProxy(IWebBrowser control, nsIDOMDocumentType obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDocumentType).GUID, obj) as nsIDOMDocumentType;
		}
	}
}
