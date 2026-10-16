using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace Mono.Http
{
	// Token: 0x020000CE RID: 206
	internal class NtlmClient : IAuthenticationModule
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x0000C99C File Offset: 0x0000AB9C
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			string text = challenge.Trim();
			int num = text.ToLower().IndexOf("ntlm");
			if (num == -1)
			{
				return null;
			}
			num = text.IndexOfAny(new char[] { ' ', '\t' });
			if (num != -1)
			{
				text = text.Substring(num).Trim();
			}
			else
			{
				text = null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			ConditionalWeakTable<HttpWebRequest, NtlmSession> conditionalWeakTable = NtlmClient.cache;
			Authorization authorization;
			lock (conditionalWeakTable)
			{
				authorization = NtlmClient.cache.GetValue(httpWebRequest, (HttpWebRequest x) => new NtlmSession()).Authenticate(text, webRequest, credentials);
			}
			return authorization;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00002F6A File Offset: 0x0000116A
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return null;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00003062 File Offset: 0x00001262
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040003B3 RID: 947
		private static readonly ConditionalWeakTable<HttpWebRequest, NtlmSession> cache = new ConditionalWeakTable<HttpWebRequest, NtlmSession>();
	}
}
