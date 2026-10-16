using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006C7 RID: 1735
	[NativeHeader("Modules/UIElements/Core/Layout/Native/LayoutNative.h")]
	internal static class LayoutNative
	{
		// Token: 0x0600321D RID: 12829
		[NativeMethod(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CalculateLayout(IntPtr node, float parentWidth, float parentHeight, int parentDirection, IntPtr state, IntPtr exceptionGCHandle);

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x0600321E RID: 12830 RVA: 0x000BD35C File Offset: 0x000BB55C
		// (remove) Token: 0x0600321F RID: 12831 RVA: 0x000BD390 File Offset: 0x000BB590
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal static event Action<LayoutNative.LayoutLogData> onLayoutLog;

		// Token: 0x06003220 RID: 12832 RVA: 0x000BD3C4 File Offset: 0x000BB5C4
		[RequiredByNativeCode]
		private unsafe static void LayoutLog_Internal(IntPtr nodePtr, LayoutNative.LayoutLogEventType type, string message)
		{
			LayoutNative.LayoutLogData layoutLogData = new LayoutNative.LayoutLogData();
			layoutLogData.node = *(LayoutNode*)(void*)nodePtr;
			layoutLogData.message = message;
			layoutLogData.eventType = type;
			LayoutNative.onLayoutLog(layoutLogData);
		}

		// Token: 0x020006C8 RID: 1736
		internal enum LayoutLogEventType
		{
			// Token: 0x0400190A RID: 6410
			None,
			// Token: 0x0400190B RID: 6411
			Error,
			// Token: 0x0400190C RID: 6412
			Measure,
			// Token: 0x0400190D RID: 6413
			Layout,
			// Token: 0x0400190E RID: 6414
			CacheUsage,
			// Token: 0x0400190F RID: 6415
			BeginLayout,
			// Token: 0x04001910 RID: 6416
			EndLayout
		}

		// Token: 0x020006C9 RID: 1737
		internal class LayoutLogData
		{
			// Token: 0x04001911 RID: 6417
			public LayoutNode node;

			// Token: 0x04001912 RID: 6418
			public LayoutNative.LayoutLogEventType eventType;

			// Token: 0x04001913 RID: 6419
			public string message;
		}
	}
}
