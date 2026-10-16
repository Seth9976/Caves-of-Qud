using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000112 RID: 274
	internal class nsSimpleEnumerator
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x000059AD File Offset: 0x00003BAD
		public static nsISimpleEnumerator GetProxy(IWebBrowser control, nsISimpleEnumerator obj)
		{
			return Base.GetProxyForObject(control, typeof(nsISimpleEnumerator).GUID, obj) as nsISimpleEnumerator;
		}
	}
}
