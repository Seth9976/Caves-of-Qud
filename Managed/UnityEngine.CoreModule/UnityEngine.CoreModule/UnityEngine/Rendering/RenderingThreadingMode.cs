using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004C0 RID: 1216
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public enum RenderingThreadingMode
	{
		// Token: 0x04000E80 RID: 3712
		Direct,
		// Token: 0x04000E81 RID: 3713
		SingleThreaded,
		// Token: 0x04000E82 RID: 3714
		MultiThreaded,
		// Token: 0x04000E83 RID: 3715
		LegacyJobified,
		// Token: 0x04000E84 RID: 3716
		NativeGraphicsJobs,
		// Token: 0x04000E85 RID: 3717
		NativeGraphicsJobsWithoutRenderThread,
		// Token: 0x04000E86 RID: 3718
		NativeGraphicsJobsSplitThreading
	}
}
