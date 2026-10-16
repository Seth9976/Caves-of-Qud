using System;

namespace System.Net.Security
{
	// Token: 0x02000847 RID: 2119
	internal sealed class SafeFreeContextBuffer_SECURITY : SafeFreeContextBuffer
	{
		// Token: 0x0600437A RID: 17274 RVA: 0x000EB720 File Offset: 0x000E9920
		internal SafeFreeContextBuffer_SECURITY()
		{
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x000EB728 File Offset: 0x000E9928
		protected override bool ReleaseHandle()
		{
			return global::Interop.SspiCli.FreeContextBuffer(this.handle) == 0;
		}
	}
}
