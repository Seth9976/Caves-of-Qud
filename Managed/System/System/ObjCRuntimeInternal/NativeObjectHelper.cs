using System;

namespace ObjCRuntimeInternal
{
	// Token: 0x02000119 RID: 281
	internal static class NativeObjectHelper
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x00012582 File Offset: 0x00010782
		public static IntPtr GetHandle(this INativeObject self)
		{
			if (self != null)
			{
				return self.Handle;
			}
			return IntPtr.Zero;
		}
	}
}
