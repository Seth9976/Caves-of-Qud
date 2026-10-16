using System;

namespace System.Web
{
	// Token: 0x02000096 RID: 150
	internal class HttpForbiddenHandler : IHttpHandler
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x000111B0 File Offset: 0x0000F3B0
		public void ProcessRequest(HttpContext context)
		{
			HttpRequest httpRequest = ((context != null) ? context.Request : null);
			string text = ((httpRequest != null) ? httpRequest.Path : null);
			string text2 = "The type of page you have requested is not served because it has been explicitly forbidden. The extension '" + ((text == null) ? string.Empty : VirtualPathUtility.GetExtension(text)) + "' may be incorrect. Please review the URL below and make sure that it is spelled correctly.";
			throw new HttpException(403, "This type of page is not served.", (httpRequest != null) ? HttpUtility.HtmlEncode(httpRequest.Path) : null, text2);
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
