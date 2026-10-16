using System;

namespace Unity
{
	// Token: 0x0200004D RID: 77
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00009DA5 File Offset: 0x00007FA5
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
