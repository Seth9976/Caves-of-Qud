using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x0200007A RID: 122
	internal class nsChannel
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00004EB1 File Offset: 0x000030B1
		public static nsIChannel GetProxy(IWebBrowser control, nsIChannel obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIChannel).GUID, obj) as nsIChannel;
		}
	}
}
