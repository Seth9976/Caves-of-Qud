using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200009C RID: 156
	internal class nsDOMDocument
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x00005126 File Offset: 0x00003326
		public static nsIDOMDocument GetProxy(IWebBrowser control, nsIDOMDocument obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMDocument).GUID, obj) as nsIDOMDocument;
		}
	}
}
