using System;
using UnityEngine.Bindings;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNodeData.h")]
	[Flags]
	public enum AccessibilityState : ushort
	{
		// Token: 0x0400001D RID: 29
		None = 0,
		// Token: 0x0400001E RID: 30
		Disabled = 1,
		// Token: 0x0400001F RID: 31
		Selected = 2
	}
}
