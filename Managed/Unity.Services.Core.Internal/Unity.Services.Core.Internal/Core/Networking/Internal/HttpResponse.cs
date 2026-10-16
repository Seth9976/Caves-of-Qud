using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x0200001D RID: 29
	internal class HttpResponse
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000021D6 File Offset: 0x000003D6
		public HttpResponse SetRequest(HttpRequest request)
		{
			this.Request = new ReadOnlyHttpRequest(request);
			return this;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000021E5 File Offset: 0x000003E5
		public HttpResponse SetRequest(ReadOnlyHttpRequest request)
		{
			this.Request = request;
			return this;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000021EF File Offset: 0x000003EF
		public HttpResponse SetHeader(string key, string value)
		{
			this.Headers[key] = value;
			return this;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000021FF File Offset: 0x000003FF
		public HttpResponse SetHeaders(Dictionary<string, string> headers)
		{
			this.Headers = headers;
			return this;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002209 File Offset: 0x00000409
		public HttpResponse SetData(byte[] data)
		{
			this.Data = data;
			return this;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002213 File Offset: 0x00000413
		public HttpResponse SetStatusCode(long statusCode)
		{
			this.StatusCode = statusCode;
			return this;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000221D File Offset: 0x0000041D
		public HttpResponse SetErrorMessage(string errorMessage)
		{
			this.ErrorMessage = errorMessage;
			return this;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002227 File Offset: 0x00000427
		public HttpResponse SetIsHttpError(bool isHttpError)
		{
			this.IsHttpError = isHttpError;
			return this;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002231 File Offset: 0x00000431
		public HttpResponse SetIsNetworkError(bool isNetworkError)
		{
			this.IsNetworkError = isNetworkError;
			return this;
		}

		// Token: 0x04000013 RID: 19
		public ReadOnlyHttpRequest Request;

		// Token: 0x04000014 RID: 20
		public Dictionary<string, string> Headers;

		// Token: 0x04000015 RID: 21
		public byte[] Data;

		// Token: 0x04000016 RID: 22
		public long StatusCode;

		// Token: 0x04000017 RID: 23
		public string ErrorMessage;

		// Token: 0x04000018 RID: 24
		public bool IsHttpError;

		// Token: 0x04000019 RID: 25
		public bool IsNetworkError;
	}
}
