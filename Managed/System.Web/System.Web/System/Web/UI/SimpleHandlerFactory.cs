using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x0200021F RID: 543
	internal class SimpleHandlerFactory : IHttpHandlerFactory
	{
		// Token: 0x06001627 RID: 5671 RVA: 0x00039613 File Offset: 0x00037813
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
		{
			return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(IHttpHandler)) as IHttpHandler;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
