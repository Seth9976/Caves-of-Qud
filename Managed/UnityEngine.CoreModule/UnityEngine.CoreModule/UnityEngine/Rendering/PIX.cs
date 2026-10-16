using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200047D RID: 1149
	[NativeConditional("PLATFORM_WIN && ENABLE_PROFILER")]
	[NativeHeader("PlatformDependent/Win/Profiler/PixBindings.h")]
	public class PIX
	{
		// Token: 0x06002D7C RID: 11644
		[FreeFunction("PIX::BeginGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginGPUCapture();

		// Token: 0x06002D7D RID: 11645
		[FreeFunction("PIX::EndGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndGPUCapture();

		// Token: 0x06002D7E RID: 11646
		[FreeFunction("PIX::IsAttached")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAttached();
	}
}
