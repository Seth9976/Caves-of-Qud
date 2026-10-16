using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020005D7 RID: 1495
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum VisualTreeUpdatePhase
	{
		// Token: 0x04001430 RID: 5168
		Bindings,
		// Token: 0x04001431 RID: 5169
		DataBinding,
		// Token: 0x04001432 RID: 5170
		Animation,
		// Token: 0x04001433 RID: 5171
		Styles,
		// Token: 0x04001434 RID: 5172
		Layout,
		// Token: 0x04001435 RID: 5173
		TransformClip,
		// Token: 0x04001436 RID: 5174
		Repaint,
		// Token: 0x04001437 RID: 5175
		Count
	}
}
