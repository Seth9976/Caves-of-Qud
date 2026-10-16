using System;
using UnityEngine.Bindings;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNodeData.h")]
	[Flags]
	public enum AccessibilityRole : ushort
	{
		// Token: 0x04000012 RID: 18
		None = 0,
		// Token: 0x04000013 RID: 19
		Button = 1,
		// Token: 0x04000014 RID: 20
		Image = 2,
		// Token: 0x04000015 RID: 21
		StaticText = 4,
		// Token: 0x04000016 RID: 22
		SearchField = 8,
		// Token: 0x04000017 RID: 23
		KeyboardKey = 16,
		// Token: 0x04000018 RID: 24
		Header = 32,
		// Token: 0x04000019 RID: 25
		TabBar = 64,
		// Token: 0x0400001A RID: 26
		Slider = 128,
		// Token: 0x0400001B RID: 27
		Toggle = 256
	}
}
