using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x02000020 RID: 32
	internal struct ReadOnlyHttpResponse
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002280 File Offset: 0x00000480
		public ReadOnlyHttpResponse(HttpResponse response)
		{
			this.m_Response = response;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002289 File Offset: 0x00000489
		public ReadOnlyHttpRequest Request
		{
			get
			{
				return this.m_Response.Request;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002296 File Offset: 0x00000496
		public IReadOnlyDictionary<string, string> Headers
		{
			get
			{
				return this.m_Response.Headers;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000022A3 File Offset: 0x000004A3
		public byte[] Data
		{
			get
			{
				return this.m_Response.Data;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000022B0 File Offset: 0x000004B0
		public long StatusCode
		{
			get
			{
				return this.m_Response.StatusCode;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000022BD File Offset: 0x000004BD
		public string ErrorMessage
		{
			get
			{
				return this.m_Response.ErrorMessage;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000022CA File Offset: 0x000004CA
		public bool IsHttpError
		{
			get
			{
				return this.m_Response.IsHttpError;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000022D7 File Offset: 0x000004D7
		public bool IsNetworkError
		{
			get
			{
				return this.m_Response.IsNetworkError;
			}
		}

		// Token: 0x0400001B RID: 27
		private HttpResponse m_Response;
	}
}
