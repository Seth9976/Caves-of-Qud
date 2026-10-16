using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000845 RID: 2117
	internal sealed class SafeSspiAuthDataHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06004372 RID: 17266 RVA: 0x00013B50 File Offset: 0x00011D50
		public SafeSspiAuthDataHandle()
			: base(true)
		{
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x000EB61F File Offset: 0x000E981F
		protected override bool ReleaseHandle()
		{
			return global::Interop.SspiCli.SspiFreeAuthIdentity(this.handle) == global::Interop.SECURITY_STATUS.OK;
		}
	}
}
