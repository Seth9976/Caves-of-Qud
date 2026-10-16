using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000170 RID: 368
	[StaticAccessor("GetUncheckedRealGfxDevice().GetFrameTimingManager()", StaticAccessorType.Dot)]
	public static class FrameTimingManager
	{
		// Token: 0x06000D31 RID: 3377
		[StaticAccessor("FrameTimingManager", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsFeatureEnabled();

		// Token: 0x06000D32 RID: 3378
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CaptureFrameTimings();

		// Token: 0x06000D33 RID: 3379 RVA: 0x00018C90 File Offset: 0x00016E90
		public unsafe static uint GetLatestTimings(uint numFrames, FrameTiming[] timings)
		{
			Span<FrameTiming> span = new Span<FrameTiming>(timings);
			uint latestTimings_Injected;
			fixed (FrameTiming* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				latestTimings_Injected = FrameTimingManager.GetLatestTimings_Injected(numFrames, ref managedSpanWrapper);
			}
			return latestTimings_Injected;
		}

		// Token: 0x06000D34 RID: 3380
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetVSyncsPerSecond();

		// Token: 0x06000D35 RID: 3381
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong GetGpuTimerFrequency();

		// Token: 0x06000D36 RID: 3382
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong GetCpuTimerFrequency();

		// Token: 0x06000D37 RID: 3383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetLatestTimings_Injected(uint numFrames, ref ManagedSpanWrapper timings);
	}
}
