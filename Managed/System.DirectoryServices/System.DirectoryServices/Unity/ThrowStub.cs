using System;

namespace Unity
{
	// Token: 0x02000099 RID: 153
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x00004D97 File Offset: 0x00002F97
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
