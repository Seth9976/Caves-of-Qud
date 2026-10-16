using System;

namespace System.Web
{
	// Token: 0x0200009A RID: 154
	internal sealed class HttpNotFoundHandler : IHttpHandler
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x000112F0 File Offset: 0x0000F4F0
		public void ProcessRequest(HttpContext context)
		{
			string path = context.Request.Path;
			throw new HttpException(404, "Path '" + path + "' was not found.", path);
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
