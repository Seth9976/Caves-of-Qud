using System;
using UnityEngine.Bindings;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNotificationContext.h")]
	internal enum AccessibilityNotification
	{
		// Token: 0x04000031 RID: 49
		None,
		// Token: 0x04000032 RID: 50
		Announcement,
		// Token: 0x04000033 RID: 51
		AnnouncementFinished,
		// Token: 0x04000034 RID: 52
		ScreenReaderStatusChanged,
		// Token: 0x04000035 RID: 53
		ScreenChanged,
		// Token: 0x04000036 RID: 54
		LayoutChanged,
		// Token: 0x04000037 RID: 55
		PageScrolled,
		// Token: 0x04000038 RID: 56
		ElementFocused,
		// Token: 0x04000039 RID: 57
		ElementUnfocused,
		// Token: 0x0400003A RID: 58
		FontScaleChanged,
		// Token: 0x0400003B RID: 59
		BoldTextStatusChanged,
		// Token: 0x0400003C RID: 60
		ClosedCaptioningStatusChanged
	}
}
