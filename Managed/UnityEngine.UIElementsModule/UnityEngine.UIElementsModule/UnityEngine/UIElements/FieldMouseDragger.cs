using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B6 RID: 694
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class FieldMouseDragger<T> : BaseFieldMouseDragger
	{
		// Token: 0x060013BB RID: 5051 RVA: 0x0004F2FD File Offset: 0x0004D4FD
		public FieldMouseDragger(IValueField<T> drivenField)
		{
			this.m_DrivenField = drivenField;
			this.m_DragElement = null;
			this.m_DragHotZone = new Rect(0f, 0f, -1f, -1f);
			this.dragging = false;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0004F33C File Offset: 0x0004D53C
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x0004F344 File Offset: 0x0004D544
		public bool dragging { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0004F34D File Offset: 0x0004D54D
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x0004F355 File Offset: 0x0004D555
		public T startValue { get; set; }

		// Token: 0x060013C0 RID: 5056 RVA: 0x0004F360 File Offset: 0x0004D560
		public sealed override void SetDragZone(VisualElement dragElement, Rect hotZone)
		{
			bool flag = this.m_DragElement != null;
			if (flag)
			{
				this.m_DragElement.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.UpdateValueOnPointerDown), TrickleDown.TrickleDown);
				this.m_DragElement.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.UpdateValueOnPointerUp), TrickleDown.NoTrickleDown);
				this.m_DragElement.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.UpdateValueOnKeyDown), TrickleDown.NoTrickleDown);
			}
			this.m_DragElement = dragElement;
			this.m_DragHotZone = hotZone;
			bool flag2 = this.m_DragElement != null;
			if (flag2)
			{
				this.dragging = false;
				this.m_DragElement.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.UpdateValueOnPointerDown), TrickleDown.TrickleDown);
				this.m_DragElement.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.UpdateValueOnPointerUp), TrickleDown.NoTrickleDown);
				this.m_DragElement.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.UpdateValueOnKeyDown), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004F438 File Offset: 0x0004D638
		private bool CanStartDrag(int button, Vector2 localPosition)
		{
			return button == 0 && (this.m_DragHotZone.width < 0f || this.m_DragHotZone.height < 0f || this.m_DragHotZone.Contains(this.m_DragElement.WorldToLocal(localPosition)));
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004F490 File Offset: 0x0004D690
		private void UpdateValueOnPointerDown(PointerDownEvent evt)
		{
			bool flag = this.CanStartDrag(evt.button, evt.localPosition);
			if (flag)
			{
				bool flag2 = evt.pointerType == PointerType.mouse;
				if (flag2)
				{
					this.m_DragElement.CaptureMouse();
					this.ProcessDownEvent(evt);
				}
				else
				{
					bool flag3 = this.m_DragElement.panel.contextType == ContextType.Editor;
					if (flag3)
					{
						this.m_DragElement.CapturePointer(evt.pointerId);
						this.ProcessDownEvent(evt);
					}
				}
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004F51C File Offset: 0x0004D71C
		private void ProcessDownEvent(EventBase evt)
		{
			evt.StopPropagation();
			this.dragging = true;
			this.m_DragElement.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.UpdateValueOnPointerMove), TrickleDown.NoTrickleDown);
			this.startValue = this.m_DrivenField.value;
			this.m_DrivenField.StartDragging();
			BaseVisualElementPanel baseVisualElementPanel = this.m_DragElement.panel as BaseVisualElementPanel;
			if (baseVisualElementPanel != null)
			{
				UIElementsBridge uiElementsBridge = baseVisualElementPanel.uiElementsBridge;
				if (uiElementsBridge != null)
				{
					uiElementsBridge.SetWantsMouseJumping(1);
				}
			}
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0004F598 File Offset: 0x0004D798
		private void UpdateValueOnPointerMove(PointerMoveEvent evt)
		{
			this.ProcessMoveEvent(evt.shiftKey, evt.altKey, evt.deltaPosition);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004F5BC File Offset: 0x0004D7BC
		private void ProcessMoveEvent(bool shiftKey, bool altKey, Vector2 deltaPosition)
		{
			bool dragging = this.dragging;
			if (dragging)
			{
				DeltaSpeed deltaSpeed = (shiftKey ? DeltaSpeed.Fast : (altKey ? DeltaSpeed.Slow : DeltaSpeed.Normal));
				this.m_DrivenField.ApplyInputDeviceDelta(deltaPosition, deltaSpeed, this.startValue);
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004F5FD File Offset: 0x0004D7FD
		private void UpdateValueOnPointerUp(PointerUpEvent evt)
		{
			this.ProcessUpEvent(evt, evt.pointerId);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0004F610 File Offset: 0x0004D810
		private void ProcessUpEvent(EventBase evt, int pointerId)
		{
			bool dragging = this.dragging;
			if (dragging)
			{
				this.dragging = false;
				this.m_DragElement.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.UpdateValueOnPointerMove), TrickleDown.NoTrickleDown);
				this.m_DragElement.ReleasePointer(pointerId);
				bool flag = evt is IMouseEvent;
				if (flag)
				{
					this.m_DragElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
				}
				BaseVisualElementPanel baseVisualElementPanel = this.m_DragElement.panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					UIElementsBridge uiElementsBridge = baseVisualElementPanel.uiElementsBridge;
					if (uiElementsBridge != null)
					{
						uiElementsBridge.SetWantsMouseJumping(0);
					}
				}
				this.m_DrivenField.StopDragging();
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0004F6B4 File Offset: 0x0004D8B4
		private void UpdateValueOnKeyDown(KeyDownEvent evt)
		{
			bool flag = this.dragging && evt.keyCode == KeyCode.Escape;
			if (flag)
			{
				this.dragging = false;
				this.m_DrivenField.value = this.startValue;
				this.m_DrivenField.StopDragging();
				VisualElement elementTarget = evt.elementTarget;
				IPanel panel = ((elementTarget != null) ? elementTarget.panel : null);
				panel.ReleasePointer(PointerId.mousePointerId);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					UIElementsBridge uiElementsBridge = baseVisualElementPanel.uiElementsBridge;
					if (uiElementsBridge != null)
					{
						uiElementsBridge.SetWantsMouseJumping(0);
					}
				}
			}
		}

		// Token: 0x04000ABA RID: 2746
		private readonly IValueField<T> m_DrivenField;

		// Token: 0x04000ABB RID: 2747
		private VisualElement m_DragElement;

		// Token: 0x04000ABC RID: 2748
		private Rect m_DragHotZone;
	}
}
