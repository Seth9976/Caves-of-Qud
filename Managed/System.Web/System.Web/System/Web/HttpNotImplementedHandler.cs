using System;

namespace System.Web
{
	// Token: 0x0200009B RID: 155
	internal sealed class HttpNotImplementedHandler : IHttpHandler
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x00011324 File Offset: 0x0000F524
		public void ProcessRequest(HttpContext context)
		{
			HttpRequest request = context.Request;
			throw new HttpException(501, request.HttpMethod + " " + request.Path + " is not implemented.");
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
