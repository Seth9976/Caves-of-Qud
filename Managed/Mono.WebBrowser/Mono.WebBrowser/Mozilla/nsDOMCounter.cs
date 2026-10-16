using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000096 RID: 150
	internal class nsDOMCounter
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x000050B7 File Offset: 0x000032B7
		public static nsIDOMCounter GetProxy(IWebBrowser control, nsIDOMCounter obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCounter).GUID, obj) as nsIDOMCounter;
		}
	}
}
