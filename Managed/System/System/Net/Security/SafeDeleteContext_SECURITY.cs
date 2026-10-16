using System;

namespace System.Net.Security
{
	// Token: 0x0200084C RID: 2124
	internal sealed class SafeDeleteContext_SECURITY : SafeDeleteContext
	{
		// Token: 0x0600438A RID: 17290 RVA: 0x000EB98D File Offset: 0x000E9B8D
		internal SafeDeleteContext_SECURITY()
		{
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x000EB995 File Offset: 0x000E9B95
		protected override bool ReleaseHandle()
		{
			if (this._EffectiveCredential != null)
			{
				this._EffectiveCredential.DangerousRelease();
			}
			return global::Interop.SspiCli.DeleteSecurityContext(ref this._handle) == 0;
		}
	}
}
