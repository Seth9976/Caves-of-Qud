using System;

namespace Unity
{
	// Token: 0x020007BE RID: 1982
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06004FCA RID: 20426 RVA: 0x000180BD File Offset: 0x000162BD
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
