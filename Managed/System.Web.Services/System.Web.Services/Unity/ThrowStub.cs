using System;

namespace Unity
{
	// Token: 0x02000156 RID: 342
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x000457BC File Offset: 0x000439BC
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
