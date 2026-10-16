using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x02000028 RID: 40
	[Flags]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNodeFlags.h")]
	public enum HierarchyNodeFlags : uint
	{
		// Token: 0x04000065 RID: 101
		None = 0U,
		// Token: 0x04000066 RID: 102
		Expanded = 1U,
		// Token: 0x04000067 RID: 103
		Selected = 2U,
		// Token: 0x04000068 RID: 104
		Cut = 4U,
		// Token: 0x04000069 RID: 105
		Hidden = 8U
	}
}
