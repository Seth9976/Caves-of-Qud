using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x020000FC RID: 252
	internal class nsPersistentProperties
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x00005816 File Offset: 0x00003A16
		public static nsIPersistentProperties GetProxy(IWebBrowser control, nsIPersistentProperties obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIPersistentProperties).GUID, obj) as nsIPersistentProperties;
		}
	}
}
