using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000667 RID: 1639
	[Flags]
	internal enum RenderDataFlags
	{
		// Token: 0x04001744 RID: 5956
		IsInChain = 1,
		// Token: 0x04001745 RID: 5957
		IsGroupTransform = 2,
		// Token: 0x04001746 RID: 5958
		IsIgnoringDynamicColorHint = 4,
		// Token: 0x04001747 RID: 5959
		HasExtraData = 8,
		// Token: 0x04001748 RID: 5960
		HasExtraMeshes = 16
	}
}
