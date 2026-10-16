using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EC RID: 748
	public static class MouseCaptureController
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x0005559C File Offset: 0x0005379C
		public static bool IsMouseCaptured()
		{
			bool flag = !MouseCaptureController.m_IsMouseCapturedWarningEmitted;
			if (flag)
			{
				Debug.LogError("MouseCaptureController.IsMouseCaptured() can not be used in playmode. Please use PointerCaptureHelper.GetCapturingElement() instead.");
				MouseCaptureController.m_IsMouseCapturedWarningEmitted = true;
			}
			return false;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000555D0 File Offset: 0x000537D0
		public static bool HasMouseCapture(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			return visualElement.HasPointerCapture(PointerId.mousePointerId);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x000555F4 File Offset: 0x000537F4
		public static void CaptureMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.CapturePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00055630 File Offset: 0x00053830
		public static void ReleaseMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.ReleasePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0005566C File Offset: 0x0005386C
		public static void ReleaseMouse()
		{
			bool flag = !MouseCaptureController.m_ReleaseMouseWarningEmitted;
			if (flag)
			{
				Debug.LogError("MouseCaptureController.ReleaseMouse() can not be used in playmode. Please use PointerCaptureHelper.GetCapturingElement() instead.");
				MouseCaptureController.m_ReleaseMouseWarningEmitted = true;
			}
		}

		// Token: 0x04000B88 RID: 2952
		private static bool m_IsMouseCapturedWarningEmitted;

		// Token: 0x04000B89 RID: 2953
		private static bool m_ReleaseMouseWarningEmitted;
	}
}
