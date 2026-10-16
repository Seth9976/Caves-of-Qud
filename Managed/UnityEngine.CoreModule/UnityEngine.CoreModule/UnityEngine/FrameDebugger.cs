using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020002F2 RID: 754
	[NativeHeader("Runtime/Profiler/PerformanceTools/FrameDebugger.h")]
	[StaticAccessor("FrameDebugger", StaticAccessorType.DoubleColon)]
	public static class FrameDebugger
	{
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000476D0 File Offset: 0x000458D0
		public static bool enabled
		{
			get
			{
				return FrameDebugger.IsLocalEnabled() || FrameDebugger.IsRemoteEnabled();
			}
		}

		// Token: 0x060024F5 RID: 9461
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsLocalEnabled();

		// Token: 0x060024F6 RID: 9462
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsRemoteEnabled();
	}
}
