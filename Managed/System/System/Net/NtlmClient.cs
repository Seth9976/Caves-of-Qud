using System;
using Mono.Http;

namespace System.Net
{
	// Token: 0x020006AD RID: 1709
	internal class NtlmClient : IAuthenticationModule
	{
		// Token: 0x0600368C RID: 13964 RVA: 0x000BF80E File Offset: 0x000BDA0E
		public NtlmClient()
		{
			this.authObject = new NtlmClient();
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000BF821 File Offset: 0x000BDA21
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (this.authObject == null)
			{
				return null;
			}
			return this.authObject.Authenticate(challenge, webRequest, credentials);
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x00002F6A File Offset: 0x0000116A
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return null;
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600368F RID: 13967 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003690 RID: 13968 RVA: 0x00003062 File Offset: 0x00001262
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001FF8 RID: 8184
		private IAuthenticationModule authObject;
	}
}
