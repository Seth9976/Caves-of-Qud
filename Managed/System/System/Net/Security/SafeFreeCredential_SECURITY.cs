using System;

namespace System.Net.Security
{
	// Token: 0x0200084B RID: 2123
	internal sealed class SafeFreeCredential_SECURITY : SafeFreeCredentials
	{
		// Token: 0x06004389 RID: 17289 RVA: 0x000EB97D File Offset: 0x000E9B7D
		protected override bool ReleaseHandle()
		{
			return global::Interop.SspiCli.FreeCredentialsHandle(ref this._handle) == 0;
		}
	}
}
