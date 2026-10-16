using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	// Token: 0x020005A3 RID: 1443
	[RequiredByNativeCode]
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal class SpriteRendererGroup
	{
		// Token: 0x060037C5 RID: 14277 RVA: 0x0006D73C File Offset: 0x0006B93C
		public static void AddRenderers(NativeArray<SpriteIntermediateRendererInfo> renderers)
		{
			SpriteRendererGroup.AddRenderers(renderers.GetUnsafeReadOnlyPtr<SpriteIntermediateRendererInfo>(), renderers.Length);
		}

		// Token: 0x060037C6 RID: 14278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void AddRenderers(void* renderers, int count);

		// Token: 0x060037C7 RID: 14279
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Clear();
	}
}
