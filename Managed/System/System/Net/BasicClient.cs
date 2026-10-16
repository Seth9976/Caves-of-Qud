using System;

namespace System.Net
{
	// Token: 0x0200066D RID: 1645
	internal class BasicClient : IAuthenticationModule
	{
		// Token: 0x060033DC RID: 13276 RVA: 0x000B4976 File Offset: 0x000B2B76
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			if (challenge.Trim().ToLower().IndexOf("basic", StringComparison.Ordinal) == -1)
			{
				return null;
			}
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000B49A4 File Offset: 0x000B2BA4
		private static byte[] GetBytes(string str)
		{
			int i = str.Length;
			byte[] array = new byte[i];
			for (i--; i >= 0; i--)
			{
				array[i] = (byte)str[i];
			}
			return array;
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000B49DC File Offset: 0x000B2BDC
		private static Authorization InternalAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null || credentials == null)
			{
				return null;
			}
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.AuthUri, "basic");
			if (credential == null)
			{
				return null;
			}
			string userName = credential.UserName;
			if (userName == null || userName == "")
			{
				return null;
			}
			string password = credential.Password;
			string domain = credential.Domain;
			byte[] array;
			if (domain == null || domain == "" || domain.Trim() == "")
			{
				array = BasicClient.GetBytes(userName + ":" + password);
			}
			else
			{
				array = BasicClient.GetBytes(string.Concat(new string[] { domain, "\\", userName, ":", password }));
			}
			return new Authorization("Basic " + Convert.ToBase64String(array));
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000B4AB6 File Offset: 0x000B2CB6
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000B4ABF File Offset: 0x000B2CBF
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}
	}
}
