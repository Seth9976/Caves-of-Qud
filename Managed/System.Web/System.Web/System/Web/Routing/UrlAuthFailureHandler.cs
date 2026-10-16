using System;

namespace System.Web.Routing
{
	// Token: 0x020004F6 RID: 1270
	internal sealed class UrlAuthFailureHandler : IHttpHandler
	{
		// Token: 0x060038C2 RID: 14530 RVA: 0x00003A2B File Offset: 0x00001C2B
		public void ProcessRequest(HttpContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x060038C3 RID: 14531 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
