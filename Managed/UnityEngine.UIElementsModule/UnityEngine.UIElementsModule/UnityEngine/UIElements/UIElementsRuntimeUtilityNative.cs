using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements
{
	// Token: 0x020002FC RID: 764
	[VisibleToOtherModules(new string[] { "Unity.UIElements" })]
	[NativeHeader("Modules/UIElements/Core/Native/UIElementsRuntimeUtilityNative.h")]
	internal static class UIElementsRuntimeUtilityNative
	{
		// Token: 0x060015A2 RID: 5538 RVA: 0x00055DB1 File Offset: 0x00053FB1
		[RequiredByNativeCode]
		public static void UpdatePanels()
		{
			Action updatePanelsCallback = UIElementsRuntimeUtilityNative.UpdatePanelsCallback;
			if (updatePanelsCallback != null)
			{
				updatePanelsCallback();
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00055DC5 File Offset: 0x00053FC5
		[RequiredByNativeCode]
		public static void RepaintPanels(bool onlyOffscreen)
		{
			Action<bool> repaintPanelsCallback = UIElementsRuntimeUtilityNative.RepaintPanelsCallback;
			if (repaintPanelsCallback != null)
			{
				repaintPanelsCallback(onlyOffscreen);
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00055DDA File Offset: 0x00053FDA
		[RequiredByNativeCode]
		public static void RenderOffscreenPanels()
		{
			Action renderOffscreenPanelsCallback = UIElementsRuntimeUtilityNative.RenderOffscreenPanelsCallback;
			if (renderOffscreenPanelsCallback != null)
			{
				renderOffscreenPanelsCallback();
			}
		}

		// Token: 0x060015A5 RID: 5541
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterPlayerloopCallback();

		// Token: 0x060015A6 RID: 5542
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterPlayerloopCallback();

		// Token: 0x060015A7 RID: 5543
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VisualElementCreation();

		// Token: 0x04000BE0 RID: 3040
		internal static Action UpdatePanelsCallback;

		// Token: 0x04000BE1 RID: 3041
		internal static Action<bool> RepaintPanelsCallback;

		// Token: 0x04000BE2 RID: 3042
		internal static Action RenderOffscreenPanelsCallback;
	}
}
