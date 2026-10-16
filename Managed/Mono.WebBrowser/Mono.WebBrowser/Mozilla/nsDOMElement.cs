using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000AA RID: 170
	internal class nsDOMElement
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x00005229 File Offset: 0x00003429
		public static nsIDOMElement GetProxy(IWebBrowser control, nsIDOMElement obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMElement).GUID, obj) as nsIDOMElement;
		}
	}
}
