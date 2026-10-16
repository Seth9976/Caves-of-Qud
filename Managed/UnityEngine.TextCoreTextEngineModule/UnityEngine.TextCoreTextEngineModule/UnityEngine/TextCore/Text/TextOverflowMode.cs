using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200004D RID: 77
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal enum TextOverflowMode
	{
		// Token: 0x040002DD RID: 733
		Overflow,
		// Token: 0x040002DE RID: 734
		Ellipsis,
		// Token: 0x040002DF RID: 735
		Masking,
		// Token: 0x040002E0 RID: 736
		Truncate,
		// Token: 0x040002E1 RID: 737
		ScrollRect,
		// Token: 0x040002E2 RID: 738
		Linked = 6
	}
}
