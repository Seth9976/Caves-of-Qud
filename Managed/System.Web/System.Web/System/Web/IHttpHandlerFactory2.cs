using System;

namespace System.Web
{
	// Token: 0x02000049 RID: 73
	internal interface IHttpHandlerFactory2 : IHttpHandlerFactory
	{
		// Token: 0x060003CB RID: 971
		IHttpHandler GetHandler(HttpContext context, string requestType, VirtualPath virtualPath, string physicalPath);
	}
}
