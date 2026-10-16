using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000056 RID: 86
	internal class ClickDetector
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000D696 File Offset: 0x0000B896
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000D69D File Offset: 0x0000B89D
		internal static int s_DoubleClickTime { get; set; } = -1;

		// Token: 0x060002DA RID: 730 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		public ClickDetector()
		{
			this.m_ClickStatus = new List<ClickDetector.ButtonClickStatus>(PointerId.maxPointers);
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				this.m_ClickStatus.Add(new ClickDetector.ButtonClickStatus());
			}
			bool flag = ClickDetector.s_DoubleClickTime == -1;
			if (flag)
			{
				ClickDetector.s_DoubleClickTime = Event.GetDoubleClickTime();
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000D710 File Offset: 0x0000B910
		private void StartClickTracking(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				VisualElement elementTarget = evt.elementTarget;
				bool flag2 = elementTarget != buttonClickStatus.m_Target;
				if (flag2)
				{
					buttonClickStatus.Reset();
				}
				buttonClickStatus.m_Target = elementTarget;
				bool flag3 = evt.timestamp - buttonClickStatus.m_LastPointerDownTime > (long)ClickDetector.s_DoubleClickTime;
				if (flag3)
				{
					buttonClickStatus.m_ClickCount = 1;
				}
				else
				{
					buttonClickStatus.m_ClickCount++;
				}
				buttonClickStatus.m_LastPointerDownTime = evt.timestamp;
				buttonClickStatus.m_PointerDownPosition = pointerEvent.position;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		private void SendClickEvent(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				VisualElement elementTarget = evt.elementTarget;
				bool flag2 = elementTarget != null && ClickDetector.ContainsPointer(elementTarget, pointerEvent);
				if (flag2)
				{
					bool flag3 = buttonClickStatus.m_Target != null && buttonClickStatus.m_ClickCount > 0;
					if (flag3)
					{
						VisualElement visualElement = buttonClickStatus.m_Target.FindCommonAncestor(evt.elementTarget);
						bool flag4 = visualElement != null;
						if (flag4)
						{
							using (ClickEvent pooled = ClickEvent.GetPooled(pointerEvent, buttonClickStatus.m_ClickCount))
							{
								pooled.elementTarget = visualElement;
								visualElement.SendEvent(pooled, DispatchMode.Immediate);
							}
						}
					}
				}
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000D890 File Offset: 0x0000BA90
		private void CancelClickTracking(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				buttonClickStatus.Reset();
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000D8CC File Offset: 0x0000BACC
		public void ProcessEvent<TEvent>(PointerEventBase<TEvent> evt) where TEvent : PointerEventBase<TEvent>, new()
		{
			bool flag = evt.eventTypeId == EventBase<PointerDownEvent>.TypeId() && evt.button == 0;
			if (flag)
			{
				this.StartClickTracking(evt);
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<PointerMoveEvent>.TypeId();
				if (flag2)
				{
					bool flag3 = evt.button == 0 && (evt.pressedButtons & 1) == 1;
					if (flag3)
					{
						this.StartClickTracking(evt);
					}
					else
					{
						bool flag4 = evt.button == 0 && (evt.pressedButtons & 1) == 0;
						if (flag4)
						{
							this.SendClickEvent(evt);
						}
						else
						{
							ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[evt.pointerId];
							bool flag5 = buttonClickStatus.m_Target != null;
							if (flag5)
							{
								buttonClickStatus.m_LastPointerDownTime = 0L;
							}
						}
					}
				}
				else
				{
					bool flag6 = evt.eventTypeId == EventBase<PointerCancelEvent>.TypeId();
					if (flag6)
					{
						this.CancelClickTracking(evt);
					}
					else
					{
						bool flag7 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() && evt.button == 0;
						if (flag7)
						{
							this.SendClickEvent(evt);
						}
					}
				}
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
		private static bool ContainsPointer(VisualElement element, IPointerEvent pe)
		{
			bool flag = !element.worldBound.Contains(pe.position) || element.panel == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				VisualElement topElementUnderPointer = element.elementPanel.GetTopElementUnderPointer(pe.pointerId);
				flag2 = element == topElementUnderPointer || element.Contains(topElementUnderPointer);
			}
			return flag2;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000DA40 File Offset: 0x0000BC40
		internal void Cleanup(List<VisualElement> elements)
		{
			foreach (ClickDetector.ButtonClickStatus buttonClickStatus in this.m_ClickStatus)
			{
				bool flag = buttonClickStatus.m_Target == null;
				if (!flag)
				{
					bool flag2 = elements.Contains(buttonClickStatus.m_Target);
					if (flag2)
					{
						buttonClickStatus.Reset();
					}
				}
			}
		}

		// Token: 0x040001A9 RID: 425
		private List<ClickDetector.ButtonClickStatus> m_ClickStatus;

		// Token: 0x02000057 RID: 87
		private class ButtonClickStatus
		{
			// Token: 0x060002E2 RID: 738 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
			public void Reset()
			{
				this.m_Target = null;
				this.m_ClickCount = 0;
				this.m_LastPointerDownTime = 0L;
				this.m_PointerDownPosition = Vector3.zero;
			}

			// Token: 0x040001AB RID: 427
			public VisualElement m_Target;

			// Token: 0x040001AC RID: 428
			public Vector3 m_PointerDownPosition;

			// Token: 0x040001AD RID: 429
			public long m_LastPointerDownTime;

			// Token: 0x040001AE RID: 430
			public int m_ClickCount;
		}
	}
}
