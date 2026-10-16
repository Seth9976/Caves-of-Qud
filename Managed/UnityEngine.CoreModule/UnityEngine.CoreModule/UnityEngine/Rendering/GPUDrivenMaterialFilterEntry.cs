using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000523 RID: 1315
	[UsedByNativeCode]
	internal struct GPUDrivenMaterialFilterEntry
	{
		// Token: 0x04001065 RID: 4197
		public GPUDrivenBitOpType op;

		// Token: 0x04001066 RID: 4198
		public int minQueueValue;

		// Token: 0x04001067 RID: 4199
		public int maxQueueValue;

		// Token: 0x04001068 RID: 4200
		public ShaderTagId keyTag;

		// Token: 0x04001069 RID: 4201
		public ShaderTagId valueTag;

		// Token: 0x0400106A RID: 4202
		public int flags;

		// Token: 0x0400106B RID: 4203
		public string keyword;
	}
}
