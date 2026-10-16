using System;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000091 RID: 145
	internal class NopHandler : IHttpHandler
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00002B4F File Offset: 0x00000D4F
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00002B13 File Offset: 0x00000D13
		public void ProcessRequest(HttpContext context)
		{
		}
	}
}
