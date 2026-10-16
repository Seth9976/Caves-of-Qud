using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000A2 RID: 162
	public static class NativeLeakDetection
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00006644 File Offset: 0x00004844
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000665C File Offset: 0x0000485C
		public static NativeLeakDetectionMode Mode
		{
			get
			{
				return UnsafeUtility.GetLeakDetectionMode();
			}
			set
			{
				bool flag = value < NativeLeakDetectionMode.Disabled || value > NativeLeakDetectionMode.EnabledWithStackTrace;
				if (flag)
				{
					throw new ArgumentException("NativeLeakDetectionMode out of range");
				}
				UnsafeUtility.SetLeakDetectionMode(value);
			}
		}
	}
}
