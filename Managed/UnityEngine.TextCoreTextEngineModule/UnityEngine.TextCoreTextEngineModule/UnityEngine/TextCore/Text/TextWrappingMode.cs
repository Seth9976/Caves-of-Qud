using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200004E RID: 78
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal enum TextWrappingMode
	{
		// Token: 0x040002E4 RID: 740
		NoWrap,
		// Token: 0x040002E5 RID: 741
		Normal,
		// Token: 0x040002E6 RID: 742
		PreserveWhitespace,
		// Token: 0x040002E7 RID: 743
		PreserveWhitespaceNoWrap
	}
}
