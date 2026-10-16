using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x0200050D RID: 1293
	public struct BatchCullingOutput
	{
		// Token: 0x0400100A RID: 4106
		public NativeArray<BatchCullingOutputDrawCommands> drawCommands;

		// Token: 0x0400100B RID: 4107
		public NativeArray<IntPtr> customCullingResult;
	}
}
