using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001A2 RID: 418
	[NativeHeader("Runtime/Export/Graphics/GraphicsBuffer.bindings.h")]
	[Flags]
	public enum ComputeBufferType
	{
		// Token: 0x04000540 RID: 1344
		Default = 0,
		// Token: 0x04000541 RID: 1345
		Raw = 1,
		// Token: 0x04000542 RID: 1346
		Append = 2,
		// Token: 0x04000543 RID: 1347
		Counter = 4,
		// Token: 0x04000544 RID: 1348
		Constant = 8,
		// Token: 0x04000545 RID: 1349
		Structured = 16,
		// Token: 0x04000546 RID: 1350
		[Obsolete("Enum member DrawIndirect has been deprecated. Use IndirectArguments instead (UnityUpgradable) -> IndirectArguments", false)]
		DrawIndirect = 256,
		// Token: 0x04000547 RID: 1351
		IndirectArguments = 256,
		// Token: 0x04000548 RID: 1352
		[Obsolete("Enum member GPUMemory has been deprecated. All compute buffers now follow the behavior previously defined by this member.", false)]
		GPUMemory = 512
	}
}
