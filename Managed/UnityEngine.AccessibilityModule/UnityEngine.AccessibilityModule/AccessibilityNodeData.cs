using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000009 RID: 9
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoAccessibilityNodeData")]
	[NativeHeader("Modules/Accessibility/Bindings/AccessibilityNodeData.bindings.h")]
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNodeData.h")]
	internal struct AccessibilityNodeData
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000285B File Offset: 0x00000A5B
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002863 File Offset: 0x00000A63
		public int id { readonly get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000286C File Offset: 0x00000A6C
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002874 File Offset: 0x00000A74
		public bool isActive { readonly get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000287D File Offset: 0x00000A7D
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002885 File Offset: 0x00000A85
		public string label { readonly get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000288E File Offset: 0x00000A8E
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002896 File Offset: 0x00000A96
		public string value { readonly get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000289F File Offset: 0x00000A9F
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000028A7 File Offset: 0x00000AA7
		public string hint { readonly get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000028B0 File Offset: 0x00000AB0
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000028B8 File Offset: 0x00000AB8
		public AccessibilityRole role { readonly get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000028C1 File Offset: 0x00000AC1
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000028C9 File Offset: 0x00000AC9
		public bool allowsDirectInteraction { readonly get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000028D2 File Offset: 0x00000AD2
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000028DA File Offset: 0x00000ADA
		public AccessibilityState state { readonly get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000028E3 File Offset: 0x00000AE3
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000028EB File Offset: 0x00000AEB
		public Rect frame { readonly get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000028F4 File Offset: 0x00000AF4
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000028FC File Offset: 0x00000AFC
		public int parentId { readonly get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002905 File Offset: 0x00000B05
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000290D File Offset: 0x00000B0D
		public int[] childIds { readonly get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002916 File Offset: 0x00000B16
		public readonly bool isFocused { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000291E File Offset: 0x00000B1E
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002926 File Offset: 0x00000B26
		internal SystemLanguage language { readonly get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000292F File Offset: 0x00000B2F
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002937 File Offset: 0x00000B37
		public bool implementsSelected { readonly get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002940 File Offset: 0x00000B40
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002948 File Offset: 0x00000B48
		public bool implementsDismissed { readonly get; set; }
	}
}
