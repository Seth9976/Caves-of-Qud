using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004C4 RID: 1220
	public enum SynchronisationStageFlags
	{
		// Token: 0x04000E97 RID: 3735
		VertexProcessing = 1,
		// Token: 0x04000E98 RID: 3736
		PixelProcessing,
		// Token: 0x04000E99 RID: 3737
		ComputeProcessing = 4,
		// Token: 0x04000E9A RID: 3738
		AllGPUOperations = 7
	}
}
