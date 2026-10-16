using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000D8 RID: 216
	internal class nsDOMStyleSheetList
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x0000557C File Offset: 0x0000377C
		public static nsIDOMStyleSheetList GetProxy(IWebBrowser control, nsIDOMStyleSheetList obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMStyleSheetList).GUID, obj) as nsIDOMStyleSheetList;
		}
	}
}
