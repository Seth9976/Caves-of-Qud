using System;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000016 RID: 22
	[RequiredByNativeCode]
	public struct VFXBatchedEffectInfo
	{
		// Token: 0x04000113 RID: 275
		public VisualEffectAsset vfxAsset;

		// Token: 0x04000114 RID: 276
		public uint activeBatchCount;

		// Token: 0x04000115 RID: 277
		public uint inactiveBatchCount;

		// Token: 0x04000116 RID: 278
		public uint activeInstanceCount;

		// Token: 0x04000117 RID: 279
		public uint unbatchedInstanceCount;

		// Token: 0x04000118 RID: 280
		public uint totalInstanceCapacity;

		// Token: 0x04000119 RID: 281
		public uint maxInstancePerBatchCapacity;

		// Token: 0x0400011A RID: 282
		public ulong totalGPUSizeInBytes;

		// Token: 0x0400011B RID: 283
		public ulong totalCPUSizeInBytes;
	}
}
