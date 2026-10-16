using System;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x0200001C RID: 28
	internal static class HttpRequestExtensions
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002161 File Offset: 0x00000361
		public static HttpRequest AsGet(this HttpRequest self)
		{
			return self.SetMethod("GET");
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000216E File Offset: 0x0000036E
		public static HttpRequest AsPost(this HttpRequest self)
		{
			return self.SetMethod("POST");
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000217B File Offset: 0x0000037B
		public static HttpRequest AsPut(this HttpRequest self)
		{
			return self.SetMethod("PUT");
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002188 File Offset: 0x00000388
		public static HttpRequest AsDelete(this HttpRequest self)
		{
			return self.SetMethod("DELETE");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002195 File Offset: 0x00000395
		public static HttpRequest AsPatch(this HttpRequest self)
		{
			return self.SetMethod("PATCH");
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000021A2 File Offset: 0x000003A2
		public static HttpRequest AsHead(this HttpRequest self)
		{
			return self.SetMethod("HEAD");
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000021AF File Offset: 0x000003AF
		public static HttpRequest AsConnect(this HttpRequest self)
		{
			return self.SetMethod("CONNECT");
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000021BC File Offset: 0x000003BC
		public static HttpRequest AsOptions(this HttpRequest self)
		{
			return self.SetMethod("OPTIONS");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000021C9 File Offset: 0x000003C9
		public static HttpRequest AsTrace(this HttpRequest self)
		{
			return self.SetMethod("TRACE");
		}
	}
}
