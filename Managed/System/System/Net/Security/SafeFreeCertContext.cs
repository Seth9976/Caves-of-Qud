using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000848 RID: 2120
	internal sealed class SafeFreeCertContext : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600437C RID: 17276 RVA: 0x00013B50 File Offset: 0x00011D50
		internal SafeFreeCertContext()
			: base(true)
		{
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x00013B79 File Offset: 0x00011D79
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x000EB738 File Offset: 0x000E9938
		protected override bool ReleaseHandle()
		{
			global::Interop.Crypt32.CertFreeCertificateContext(this.handle);
			return true;
		}

		// Token: 0x040028EE RID: 10478
		private const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;
	}
}
