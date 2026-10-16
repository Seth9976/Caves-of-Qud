using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200007E RID: 126
	internal class nsDOMAbstractView
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x00004EFB File Offset: 0x000030FB
		public static nsIDOMAbstractView GetProxy(IWebBrowser control, nsIDOMAbstractView obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMAbstractView).GUID, obj) as nsIDOMAbstractView;
		}
	}
}
