using System;

namespace System.Net
{
	// Token: 0x02000635 RID: 1589
	internal interface IAutoWebProxy : IWebProxy
	{
		// Token: 0x06003229 RID: 12841
		ProxyChain GetProxies(Uri destination);
	}
}
