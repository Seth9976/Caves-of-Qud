using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200008E RID: 142
	internal class nsDOMCSSStyleSheet
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00005023 File Offset: 0x00003223
		public static nsIDOMCSSStyleSheet GetProxy(IWebBrowser control, nsIDOMCSSStyleSheet obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCSSStyleSheet).GUID, obj) as nsIDOMCSSStyleSheet;
		}
	}
}
