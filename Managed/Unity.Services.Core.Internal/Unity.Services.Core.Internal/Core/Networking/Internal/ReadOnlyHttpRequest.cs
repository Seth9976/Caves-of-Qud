using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x0200001F RID: 31
	internal struct ReadOnlyHttpRequest
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002243 File Offset: 0x00000443
		public ReadOnlyHttpRequest(HttpRequest request)
		{
			this.m_Request = request;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000224C File Offset: 0x0000044C
		public string Method
		{
			get
			{
				return this.m_Request.Method;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002259 File Offset: 0x00000459
		public string Url
		{
			get
			{
				return this.m_Request.Url;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002266 File Offset: 0x00000466
		public IReadOnlyDictionary<string, string> Headers
		{
			get
			{
				return this.m_Request.Headers;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002273 File Offset: 0x00000473
		public byte[] Body
		{
			get
			{
				return this.m_Request.Body;
			}
		}

		// Token: 0x0400001A RID: 26
		private HttpRequest m_Request;
	}
}
