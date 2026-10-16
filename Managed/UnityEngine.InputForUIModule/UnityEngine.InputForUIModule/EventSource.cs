using System;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200000F RID: 15
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal enum EventSource
	{
		// Token: 0x04000048 RID: 72
		Unspecified,
		// Token: 0x04000049 RID: 73
		Keyboard,
		// Token: 0x0400004A RID: 74
		Gamepad,
		// Token: 0x0400004B RID: 75
		Mouse,
		// Token: 0x0400004C RID: 76
		Pen,
		// Token: 0x0400004D RID: 77
		Touch
	}
}
