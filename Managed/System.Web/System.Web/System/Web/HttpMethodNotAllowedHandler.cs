using System;

namespace System.Web
{
	// Token: 0x02000098 RID: 152
	internal class HttpMethodNotAllowedHandler : IHttpHandler
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public void ProcessRequest(HttpContext context)
		{
			throw new HttpException(405, "Method not allowed");
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
