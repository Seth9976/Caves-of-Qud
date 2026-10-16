using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200016F RID: 367
	[NativeHeader("Runtime/GfxDevice/FrameTiming.h")]
	public struct FrameTiming
	{
		// Token: 0x040004B2 RID: 1202
		[NativeName("totalFrameTime")]
		public double cpuFrameTime;

		// Token: 0x040004B3 RID: 1203
		[NativeName("mainThreadActiveTime")]
		public double cpuMainThreadFrameTime;

		// Token: 0x040004B4 RID: 1204
		[NativeName("mainThreadPresentWaitTime")]
		public double cpuMainThreadPresentWaitTime;

		// Token: 0x040004B5 RID: 1205
		[NativeName("renderThreadActiveTime")]
		public double cpuRenderThreadFrameTime;

		// Token: 0x040004B6 RID: 1206
		[NativeName("gpuFrameTime")]
		public double gpuFrameTime;

		// Token: 0x040004B7 RID: 1207
		[NativeName("frameStartTimestamp")]
		public ulong frameStartTimestamp;

		// Token: 0x040004B8 RID: 1208
		[NativeName("firstSubmitTimestamp")]
		public ulong firstSubmitTimestamp;

		// Token: 0x040004B9 RID: 1209
		[NativeName("presentFrameTimestamp")]
		public ulong cpuTimePresentCalled;

		// Token: 0x040004BA RID: 1210
		[NativeName("frameCompleteTimestamp")]
		public ulong cpuTimeFrameComplete;

		// Token: 0x040004BB RID: 1211
		[NativeName("heightScale")]
		public float heightScale;

		// Token: 0x040004BC RID: 1212
		[NativeName("widthScale")]
		public float widthScale;

		// Token: 0x040004BD RID: 1213
		[NativeName("syncInterval")]
		public uint syncInterval;
	}
}
