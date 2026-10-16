using System;

namespace System.Net.Security
{
	// Token: 0x0200084E RID: 2126
	internal sealed class SafeFreeContextBufferChannelBinding_SECURITY : SafeFreeContextBufferChannelBinding
	{
		// Token: 0x06004393 RID: 17299 RVA: 0x000EB728 File Offset: 0x000E9928
		protected override bool ReleaseHandle()
		{
			return global::Interop.SspiCli.FreeContextBuffer(this.handle) == 0;
		}
	}
}
