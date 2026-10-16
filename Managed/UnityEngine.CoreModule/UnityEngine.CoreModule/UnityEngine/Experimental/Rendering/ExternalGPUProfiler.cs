using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000592 RID: 1426
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public static class ExternalGPUProfiler
	{
		// Token: 0x06003707 RID: 14087
		[FreeFunction("ExternalGPUProfilerBindings::BeginGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginGPUCapture();

		// Token: 0x06003708 RID: 14088
		[FreeFunction("ExternalGPUProfilerBindings::EndGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndGPUCapture();

		// Token: 0x06003709 RID: 14089
		[FreeFunction("ExternalGPUProfilerBindings::IsAttached")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAttached();
	}
}
