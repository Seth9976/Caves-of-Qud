using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("Modules/HierarchyCore/Public/HierarchySearch.h")]
	public enum HierarchySearchFilterOperator
	{
		// Token: 0x04000078 RID: 120
		Equal,
		// Token: 0x04000079 RID: 121
		Contains,
		// Token: 0x0400007A RID: 122
		Greater,
		// Token: 0x0400007B RID: 123
		GreaterOrEqual,
		// Token: 0x0400007C RID: 124
		Lesser,
		// Token: 0x0400007D RID: 125
		LesserOrEqual,
		// Token: 0x0400007E RID: 126
		NotEqual,
		// Token: 0x0400007F RID: 127
		Not
	}
}
