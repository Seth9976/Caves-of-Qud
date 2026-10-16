using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020006F7 RID: 1783
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum DataType
	{
		// Token: 0x04001A4D RID: 6733
		None,
		// Token: 0x04001A4E RID: 6734
		Number,
		// Token: 0x04001A4F RID: 6735
		Integer,
		// Token: 0x04001A50 RID: 6736
		Length,
		// Token: 0x04001A51 RID: 6737
		Percentage,
		// Token: 0x04001A52 RID: 6738
		Color,
		// Token: 0x04001A53 RID: 6739
		Resource,
		// Token: 0x04001A54 RID: 6740
		Url,
		// Token: 0x04001A55 RID: 6741
		Time,
		// Token: 0x04001A56 RID: 6742
		Angle,
		// Token: 0x04001A57 RID: 6743
		CustomIdent
	}
}
