using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x0200001B RID: 27
	internal class HttpRequest
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000020C0 File Offset: 0x000002C0
		public HttpRequest()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000020C8 File Offset: 0x000002C8
		public HttpRequest(string method, string url, Dictionary<string, string> headers, byte[] body)
		{
			this.Method = method;
			this.Url = url;
			this.Headers = headers;
			this.Body = body;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000020ED File Offset: 0x000002ED
		public HttpRequest SetMethod(string method)
		{
			this.Method = method;
			return this;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000020F7 File Offset: 0x000002F7
		public HttpRequest SetUrl(string url)
		{
			this.Url = url;
			return this;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002101 File Offset: 0x00000301
		public HttpRequest SetHeader(string key, string value)
		{
			if (this.Headers == null)
			{
				this.Headers = new Dictionary<string, string>(1);
			}
			this.Headers[key] = value;
			return this;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002125 File Offset: 0x00000325
		public HttpRequest SetHeaders(Dictionary<string, string> headers)
		{
			this.Headers = headers;
			return this;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000212F File Offset: 0x0000032F
		public HttpRequest SetBody(byte[] body)
		{
			this.Body = body;
			return this;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002139 File Offset: 0x00000339
		public HttpRequest SetOptions(HttpOptions options)
		{
			this.Options = options;
			return this;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002143 File Offset: 0x00000343
		public HttpRequest SetRedirectLimit(int redirectLimit)
		{
			this.Options.RedirectLimit = redirectLimit;
			return this;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002152 File Offset: 0x00000352
		public HttpRequest SetTimeOutInSeconds(int timeout)
		{
			this.Options.RequestTimeoutInSeconds = timeout;
			return this;
		}

		// Token: 0x0400000E RID: 14
		public string Method;

		// Token: 0x0400000F RID: 15
		public string Url;

		// Token: 0x04000010 RID: 16
		public Dictionary<string, string> Headers;

		// Token: 0x04000011 RID: 17
		public byte[] Body;

		// Token: 0x04000012 RID: 18
		public HttpOptions Options;
	}
}
