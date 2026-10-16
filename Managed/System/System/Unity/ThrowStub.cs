using System;

namespace Unity
{
	// Token: 0x02000880 RID: 2176
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060044D1 RID: 17617 RVA: 0x00011F38 File Offset: 0x00010138
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
