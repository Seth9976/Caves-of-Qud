using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020006F6 RID: 1782
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum ExpressionType
	{
		// Token: 0x04001A48 RID: 6728
		Unknown,
		// Token: 0x04001A49 RID: 6729
		Data,
		// Token: 0x04001A4A RID: 6730
		Keyword,
		// Token: 0x04001A4B RID: 6731
		Combinator
	}
}
