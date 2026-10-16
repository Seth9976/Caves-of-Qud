using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/Accessibility/Native/AccessibilitySettings.h")]
	public static class AccessibilitySettings
	{
		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFontScale();

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsBoldTextEnabled();

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsClosedCaptioningEnabled();

		// Token: 0x06000088 RID: 136 RVA: 0x00002C48 File Offset: 0x00000E48
		[RequiredByNativeCode]
		private static void Internal_OnFontScaleChanged(float newFontScale)
		{
			AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext
			{
				notification = AccessibilityNotification.FontScaleChanged,
				fontScale = newFontScale
			});
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002C78 File Offset: 0x00000E78
		[RequiredByNativeCode]
		private static void Internal_OnBoldTextStatusChanged(bool enabled)
		{
			AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext
			{
				notification = AccessibilityNotification.BoldTextStatusChanged,
				isBoldTextEnabled = enabled
			});
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002CA8 File Offset: 0x00000EA8
		[RequiredByNativeCode]
		private static void Internal_OnClosedCaptioningStatusChanged(bool enabled)
		{
			AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext
			{
				notification = AccessibilityNotification.ClosedCaptioningStatusChanged,
				isClosedCaptioningEnabled = enabled
			});
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public static float fontScale
		{
			get
			{
				return AccessibilitySettings.GetFontScale();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002CDF File Offset: 0x00000EDF
		public static bool isBoldTextEnabled
		{
			get
			{
				return AccessibilitySettings.IsBoldTextEnabled();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public static bool isClosedCaptioningEnabled
		{
			get
			{
				return AccessibilitySettings.IsClosedCaptioningEnabled();
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600008E RID: 142 RVA: 0x00002CF0 File Offset: 0x00000EF0
		// (remove) Token: 0x0600008F RID: 143 RVA: 0x00002D24 File Offset: 0x00000F24
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<float> fontScaleChanged;

		// Token: 0x06000090 RID: 144 RVA: 0x00002D57 File Offset: 0x00000F57
		internal static void InvokeFontScaleChanged(float newFontScale)
		{
			Action<float> action = AccessibilitySettings.fontScaleChanged;
			if (action != null)
			{
				action(newFontScale);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000091 RID: 145 RVA: 0x00002D6C File Offset: 0x00000F6C
		// (remove) Token: 0x06000092 RID: 146 RVA: 0x00002DA0 File Offset: 0x00000FA0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<bool> boldTextStatusChanged;

		// Token: 0x06000093 RID: 147 RVA: 0x00002DD3 File Offset: 0x00000FD3
		internal static void InvokeBoldTextStatusChanged(bool enabled)
		{
			Action<bool> action = AccessibilitySettings.boldTextStatusChanged;
			if (action != null)
			{
				action(enabled);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000094 RID: 148 RVA: 0x00002DE8 File Offset: 0x00000FE8
		// (remove) Token: 0x06000095 RID: 149 RVA: 0x00002E1C File Offset: 0x0000101C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<bool> closedCaptioningStatusChanged;

		// Token: 0x06000096 RID: 150 RVA: 0x00002E4F File Offset: 0x0000104F
		internal static void InvokeClosedCaptionStatusChanged(bool enabled)
		{
			Action<bool> action = AccessibilitySettings.closedCaptioningStatusChanged;
			if (action != null)
			{
				action(enabled);
			}
		}
	}
}
