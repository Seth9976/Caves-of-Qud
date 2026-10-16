using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000080 RID: 128
	internal class nsDOMAttr
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x00004F20 File Offset: 0x00003120
		public static nsIDOMAttr GetProxy(IWebBrowser control, nsIDOMAttr obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMAttr).GUID, obj) as nsIDOMAttr;
		}
	}
}
