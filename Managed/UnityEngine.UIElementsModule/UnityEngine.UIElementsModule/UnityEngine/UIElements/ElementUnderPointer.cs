using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022A RID: 554
	internal class ElementUnderPointer
	{
		// Token: 0x0600105B RID: 4187 RVA: 0x000478B4 File Offset: 0x00045AB4
		internal VisualElement GetTopElementUnderPointer(int pointerId, out Vector2 pickPosition, out bool isTemporary)
		{
			pickPosition = this.m_PickingPointerPositions[pointerId];
			isTemporary = this.m_IsPickingPointerTemporaries[pointerId];
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000478EC File Offset: 0x00045AEC
		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00047908 File Offset: 0x00045B08
		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, Vector2 pointerPos)
		{
			Debug.Assert(pointerId >= 0, "SetElementUnderPointer expects pointerId >= 0");
			VisualElement visualElement = this.m_TopElementUnderPointer[pointerId];
			this.m_IsPickingPointerTemporaries[pointerId] = false;
			this.m_PickingPointerPositions[pointerId] = pointerPos;
			bool flag = newElementUnderPointer == visualElement;
			if (!flag)
			{
				this.m_PendingTopElementUnderPointer[pointerId] = newElementUnderPointer;
				this.m_TriggerEvent[pointerId] = null;
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00047964 File Offset: 0x00045B64
		private Vector2 GetEventPointerPosition(EventBase triggerEvent)
		{
			IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
			bool flag = pointerEvent != null;
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(pointerEvent.position.x, pointerEvent.position.y);
			}
			else
			{
				IMouseEvent mouseEvent = triggerEvent as IMouseEvent;
				bool flag2 = mouseEvent != null;
				if (flag2)
				{
					vector = mouseEvent.mousePosition;
				}
				else
				{
					vector = new Vector2(float.MinValue, float.MinValue);
				}
			}
			return vector;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000479CE File Offset: 0x00045BCE
		internal void SetTemporaryElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, EventBase triggerEvent)
		{
			this.SetElementUnderPointer(newElementUnderPointer, pointerId, triggerEvent, true);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000479DC File Offset: 0x00045BDC
		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, EventBase triggerEvent)
		{
			this.SetElementUnderPointer(newElementUnderPointer, pointerId, triggerEvent, false);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000479EC File Offset: 0x00045BEC
		private void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, EventBase triggerEvent, bool temporary)
		{
			Debug.Assert(pointerId >= 0, "SetElementUnderPointer expects pointerId >= 0");
			this.m_IsPickingPointerTemporaries[pointerId] = temporary;
			this.m_PickingPointerPositions[pointerId] = this.GetEventPointerPosition(triggerEvent);
			this.m_PendingTopElementUnderPointer[pointerId] = newElementUnderPointer;
			VisualElement visualElement = this.m_TopElementUnderPointer[pointerId];
			bool flag = newElementUnderPointer == visualElement;
			if (!flag)
			{
				IPointerOrMouseEvent pointerOrMouseEvent;
				bool flag2;
				if (this.m_TriggerEvent[pointerId] == null)
				{
					pointerOrMouseEvent = triggerEvent as IPointerOrMouseEvent;
					flag2 = pointerOrMouseEvent != null;
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					this.m_TriggerEvent[pointerId] = pointerOrMouseEvent;
				}
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00047A70 File Offset: 0x00045C70
		internal void CommitElementUnderPointers(EventDispatcher dispatcher, ContextType contextType)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				IPointerOrMouseEvent pointerOrMouseEvent = this.m_TriggerEvent[i];
				VisualElement visualElement = this.m_TopElementUnderPointer[i];
				VisualElement visualElement2 = this.m_PendingTopElementUnderPointer[i];
				bool flag = visualElement2 == visualElement;
				if (flag)
				{
					bool flag2 = pointerOrMouseEvent != null;
					if (flag2)
					{
						this.m_PickingPointerPositions[i] = pointerOrMouseEvent.position;
					}
				}
				else
				{
					this.m_TopElementUnderPointer[i] = visualElement2;
					bool flag3 = pointerOrMouseEvent == null;
					if (flag3)
					{
						using (new EventDispatcherGate(dispatcher))
						{
							Vector2 pointerPosition = PointerDeviceState.GetPointerPosition(i, contextType);
							PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, pointerPosition, i);
							PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, pointerPosition, i);
							this.m_PickingPointerPositions[i] = pointerPosition;
							bool flag4 = i == PointerId.mousePointerId;
							if (flag4)
							{
								MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, null, pointerPosition);
								MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, null, pointerPosition);
							}
						}
					}
					bool flag5 = pointerOrMouseEvent != null;
					if (flag5)
					{
						Vector3 position = pointerOrMouseEvent.position;
						this.m_PickingPointerPositions[i] = position;
						EventBase eventBase = pointerOrMouseEvent as EventBase;
						bool flag6 = eventBase != null;
						if (flag6)
						{
							bool flag7 = eventBase.eventTypeId == EventBase<PointerMoveEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerCancelEvent>.TypeId();
							if (flag7)
							{
								using (new EventDispatcherGate(dispatcher))
								{
									PointerEventsHelper.SendOverOut(visualElement, visualElement2, (IPointerEvent)pointerOrMouseEvent, position, i);
									PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, (IPointerEvent)pointerOrMouseEvent, position, i);
									IMouseEvent compatibilityMouseEvent;
									bool flag8;
									if (pointerOrMouseEvent.pointerId == PointerId.mousePointerId)
									{
										compatibilityMouseEvent = ((IPointerEventInternal)pointerOrMouseEvent).compatibilityMouseEvent;
										flag8 = compatibilityMouseEvent != null;
									}
									else
									{
										flag8 = false;
									}
									bool flag9 = flag8;
									if (flag9)
									{
										MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, compatibilityMouseEvent, position);
										MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, compatibilityMouseEvent, position);
									}
								}
							}
							else
							{
								bool flag10 = eventBase.eventTypeId == EventBase<WheelEvent>.TypeId();
								if (flag10)
								{
									using (new EventDispatcherGate(dispatcher))
									{
										MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, (IMouseEvent)pointerOrMouseEvent, position);
										MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, (IMouseEvent)pointerOrMouseEvent, position);
									}
								}
								else
								{
									bool flag11 = eventBase.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || eventBase.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
									if (flag11)
									{
										using (new EventDispatcherGate(dispatcher))
										{
											PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, position, i);
											PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, position, i);
											bool flag12 = i == PointerId.mousePointerId;
											if (flag12)
											{
												MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, (IMouseEvent)pointerOrMouseEvent, position);
												MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, (IMouseEvent)pointerOrMouseEvent, position);
											}
										}
									}
								}
							}
						}
					}
					this.m_TriggerEvent[i] = null;
				}
			}
		}

		// Token: 0x040009D8 RID: 2520
		private VisualElement[] m_PendingTopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		// Token: 0x040009D9 RID: 2521
		private VisualElement[] m_TopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		// Token: 0x040009DA RID: 2522
		private IPointerOrMouseEvent[] m_TriggerEvent = new IPointerOrMouseEvent[PointerId.maxPointers];

		// Token: 0x040009DB RID: 2523
		private Vector2[] m_PickingPointerPositions = new Vector2[PointerId.maxPointers];

		// Token: 0x040009DC RID: 2524
		private bool[] m_IsPickingPointerTemporaries = new bool[PointerId.maxPointers];
	}
}
