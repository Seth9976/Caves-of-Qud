using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000175 RID: 373
	internal static class IOSelector
	{
		// Token: 0x060009E0 RID: 2528
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Add(IntPtr handle, IOSelectorJob job);

		// Token: 0x060009E1 RID: 2529
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Remove(IntPtr handle);
	}
}
