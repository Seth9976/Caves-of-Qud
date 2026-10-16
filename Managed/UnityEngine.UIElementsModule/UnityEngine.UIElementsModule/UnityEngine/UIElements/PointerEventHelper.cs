using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028B RID: 651
	internal static class PointerEventHelper
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x0004CC98 File Offset: 0x0004AE98
		public static EventBase GetPooled(EventType eventType, Vector3 mousePosition, Vector2 delta, int button, int clickCount, EventModifiers modifiers, int displayIndex)
		{
			bool flag = eventType == EventType.MouseDown && !PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, button);
			EventBase eventBase;
			if (flag)
			{
				eventBase = PointerEventBase<PointerDownEvent>.GetPooled(eventType, mousePosition, delta, button, clickCount, modifiers, displayIndex);
			}
			else
			{
				bool flag2 = eventType == EventType.MouseUp && !PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, button);
				if (flag2)
				{
					eventBase = PointerEventBase<PointerUpEvent>.GetPooled(eventType, mousePosition, delta, button, clickCount, modifiers, displayIndex);
				}
				else
				{
					eventBase = PointerEventBase<PointerMoveEvent>.GetPooled(eventType, mousePosition, delta, button, clickCount, modifiers, displayIndex);
				}
			}
			return eventBase;
		}
	}
}
