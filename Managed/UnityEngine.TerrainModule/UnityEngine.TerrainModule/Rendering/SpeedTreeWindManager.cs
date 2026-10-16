using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000037 RID: 55
	[NativeHeader("Modules/Terrain/Public/SpeedTreeWindManager.h")]
	[StaticAccessor("GetSpeedTreeWindManager()", StaticAccessorType.Dot)]
	internal static class SpeedTreeWindManager
	{
		// Token: 0x06000284 RID: 644 RVA: 0x000072C8 File Offset: 0x000054C8
		public unsafe static void UpdateWindAndWriteBufferWindParams(ReadOnlySpan<int> renderersID, SpeedTreeWindParamsBufferIterator windParams, bool history)
		{
			ReadOnlySpan<int> readOnlySpan = renderersID;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				SpeedTreeWindManager.UpdateWindAndWriteBufferWindParams_Injected(ref managedSpanWrapper, ref windParams, history);
			}
		}

		// Token: 0x06000285 RID: 645
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateWindAndWriteBufferWindParams_Injected(ref ManagedSpanWrapper renderersID, [In] ref SpeedTreeWindParamsBufferIterator windParams, bool history);
	}
}
