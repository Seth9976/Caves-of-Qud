using System;

namespace Unity
{
	// Token: 0x020001D5 RID: 469
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000BF2 RID: 3058 RVA: 0x000168FB File Offset: 0x00014AFB
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
