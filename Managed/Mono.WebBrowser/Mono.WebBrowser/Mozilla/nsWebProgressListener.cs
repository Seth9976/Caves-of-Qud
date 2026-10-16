using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000130 RID: 304
	internal class nsWebProgressListener
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00005BD8 File Offset: 0x00003DD8
		public static nsIWebProgressListener GetProxy(IWebBrowser control, nsIWebProgressListener obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIWebProgressListener).GUID, obj) as nsIWebProgressListener;
		}
	}
}
