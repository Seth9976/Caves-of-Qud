using System;

namespace System.Net.Mail
{
	// Token: 0x0200080E RID: 2062
	internal class CCredentialsByHost : ICredentialsByHost
	{
		// Token: 0x06004207 RID: 16903 RVA: 0x000E4701 File Offset: 0x000E2901
		public CCredentialsByHost(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x000E4717 File Offset: 0x000E2917
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return new NetworkCredential(this.userName, this.password);
		}

		// Token: 0x04002797 RID: 10135
		private string userName;

		// Token: 0x04002798 RID: 10136
		private string password;
	}
}
