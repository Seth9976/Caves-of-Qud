using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000315 RID: 789
	public static class PointerCaptureHelper
	{
		// Token: 0x060016A8 RID: 5800 RVA: 0x00057B10 File Offset: 0x00055D10
		private static PointerDispatchState GetStateFor(IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			PointerDispatchState pointerDispatchState;
			if (visualElement == null)
			{
				pointerDispatchState = null;
			}
			else
			{
				IPanel panel = visualElement.panel;
				if (panel == null)
				{
					pointerDispatchState = null;
				}
				else
				{
					EventDispatcher dispatcher = panel.dispatcher;
					pointerDispatchState = ((dispatcher != null) ? dispatcher.pointerState : null);
				}
			}
			return pointerDispatchState;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00057B50 File Offset: 0x00055D50
		public static bool HasPointerCapture(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			return stateFor != null && stateFor.HasPointerCapture(handler, pointerId);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00057B76 File Offset: 0x00055D76
		public static void CapturePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			if (stateFor != null)
			{
				stateFor.CapturePointer(handler, pointerId);
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00057B8D File Offset: 0x00055D8D
		public static void ReleasePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			if (stateFor != null)
			{
				stateFor.ReleasePointer(handler, pointerId);
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00057BA4 File Offset: 0x00055DA4
		public static IEventHandler GetCapturingElement(this IPanel panel, int pointerId)
		{
			IEventHandler eventHandler;
			if (panel == null)
			{
				eventHandler = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				eventHandler = ((dispatcher != null) ? dispatcher.pointerState.GetCapturingElement(pointerId) : null);
			}
			return eventHandler;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00057BD4 File Offset: 0x00055DD4
		public static void ReleasePointer(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ReleasePointer(pointerId);
				}
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00057BF4 File Offset: 0x00055DF4
		internal static void ActivateCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ActivateCompatibilityMouseEvents(pointerId);
				}
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00057C14 File Offset: 0x00055E14
		internal static void PreventCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.PreventCompatibilityMouseEvents(pointerId);
				}
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00057C34 File Offset: 0x00055E34
		internal static bool ShouldSendCompatibilityMouseEvents(this IPanel panel, IPointerEvent evt)
		{
			bool? flag;
			if (panel == null)
			{
				flag = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				flag = ((dispatcher != null) ? new bool?(dispatcher.pointerState.ShouldSendCompatibilityMouseEvents(evt)) : null);
			}
			return flag ?? true;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00057C8D File Offset: 0x00055E8D
		internal static void ProcessPointerCapture(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ProcessPointerCapture(pointerId);
				}
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00057CAD File Offset: 0x00055EAD
		internal static void ResetPointerDispatchState(this IPanel panel)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.Reset();
				}
			}
		}
	}
}
