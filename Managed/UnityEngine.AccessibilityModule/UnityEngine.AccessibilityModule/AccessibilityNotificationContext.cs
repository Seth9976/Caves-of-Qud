using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000C RID: 12
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoAccessibilityNotificationContext")]
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNotificationContext.h")]
	[NativeHeader("Modules/Accessibility/Bindings/AccessibilityNotificationContext.bindings.h")]
	internal struct AccessibilityNotificationContext
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002BFA File Offset: 0x00000DFA
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002C02 File Offset: 0x00000E02
		public AccessibilityNotification notification { readonly get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002C0B File Offset: 0x00000E0B
		public readonly bool isScreenReaderEnabled { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002C13 File Offset: 0x00000E13
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002C1B File Offset: 0x00000E1B
		public string announcement { readonly get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002C24 File Offset: 0x00000E24
		public readonly bool wasAnnouncementSuccessful { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002C2C File Offset: 0x00000E2C
		public readonly int currentNodeId { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002C34 File Offset: 0x00000E34
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002C3C File Offset: 0x00000E3C
		public int nextNodeId { readonly get; set; }
	}
}
