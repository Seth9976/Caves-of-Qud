using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyPropertyStorageType.h")]
	public enum HierarchyPropertyStorageType
	{
		// Token: 0x04000073 RID: 115
		Sparse,
		// Token: 0x04000074 RID: 116
		Dense,
		// Token: 0x04000075 RID: 117
		Blob,
		// Token: 0x04000076 RID: 118
		Default = 1
	}
}
