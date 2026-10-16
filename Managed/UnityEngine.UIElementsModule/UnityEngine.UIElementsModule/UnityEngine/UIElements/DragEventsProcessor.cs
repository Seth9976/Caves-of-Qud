using System;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x020001ED RID: 493
	internal abstract class DragEventsProcessor
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x000435F8 File Offset: 0x000417F8
		internal bool isRegistered
		{
			get
			{
				return this.m_IsRegistered;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00043600 File Offset: 0x00041800
		internal DragEventsProcessor.DragState dragState
		{
			get
			{
				return this.m_DragState;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0000E724 File Offset: 0x0000C924
		protected virtual bool supportsDragEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00043608 File Offset: 0x00041808
		private bool useDragEvents
		{
			get
			{
				return this.isEditorContext && this.supportsDragEvents;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0004361B File Offset: 0x0004181B
		protected IDragAndDrop dragAndDrop
		{
			get
			{
				return DragAndDropUtility.GetDragAndDrop(this.m_Target.panel);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00043630 File Offset: 0x00041830
		internal virtual bool isEditorContext
		{
			get
			{
				Assert.IsNotNull<VisualElement>(this.m_Target);
				Assert.IsNotNull<VisualElement>(this.m_Target.parent);
				return this.m_Target.panel.contextType == ContextType.Editor;
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00043674 File Offset: 0x00041874
		internal DragEventsProcessor(VisualElement target)
		{
			this.m_Target = target;
			this.m_Target.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.RegisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
			this.RegisterCallbacksFromTarget();
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000436C9 File Offset: 0x000418C9
		private void RegisterCallbacksFromTarget(AttachToPanelEvent evt)
		{
			this.RegisterCallbacksFromTarget();
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000436D4 File Offset: 0x000418D4
		private void RegisterCallbacksFromTarget()
		{
			bool isRegistered = this.m_IsRegistered;
			if (!isRegistered)
			{
				this.m_IsRegistered = true;
				this.m_Target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.TrickleDown);
				this.m_Target.RegisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancelEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCapturedOut), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerOutEvent>(new EventCallback<PointerOutEvent>(this.OnPointerOutEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000437C0 File Offset: 0x000419C0
		private void UnregisterCallbacksFromTarget(DetachFromPanelEvent evt)
		{
			this.UnregisterCallbacksFromTarget(false);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000437CC File Offset: 0x000419CC
		internal void UnregisterCallbacksFromTarget(bool unregisterPanelEvents = false)
		{
			this.m_IsRegistered = false;
			this.m_Target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.TrickleDown);
			this.m_Target.UnregisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancelEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCapturedOut), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerOutEvent>(new EventCallback<PointerOutEvent>(this.OnPointerOutEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			if (unregisterPanelEvents)
			{
				this.m_Target.UnregisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.RegisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
				this.m_Target.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000F44 RID: 3908
		protected abstract bool CanStartDrag(Vector3 pointerPosition);

		// Token: 0x06000F45 RID: 3909
		protected internal abstract StartDragArgs StartDrag(Vector3 pointerPosition);

		// Token: 0x06000F46 RID: 3910
		protected internal abstract void UpdateDrag(Vector3 pointerPosition);

		// Token: 0x06000F47 RID: 3911
		protected internal abstract void OnDrop(Vector3 pointerPosition);

		// Token: 0x06000F48 RID: 3912
		protected abstract void ClearDragAndDropUI(bool dragCancelled);

		// Token: 0x06000F49 RID: 3913 RVA: 0x000438E4 File Offset: 0x00041AE4
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			bool flag = evt.button != 0;
			if (flag)
			{
				this.m_DragState = DragEventsProcessor.DragState.None;
			}
			else
			{
				bool flag2 = this.CanStartDrag(evt.position);
				if (flag2)
				{
					this.m_DragState = DragEventsProcessor.DragState.CanStartDrag;
					this.m_Start = evt.position;
				}
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00043930 File Offset: 0x00041B30
		private void OnPointerOutEvent(PointerOutEvent evt)
		{
			bool flag = this.m_DragState == DragEventsProcessor.DragState.CanStartDrag;
			if (flag)
			{
				bool flag2 = (this.m_Start - evt.position).sqrMagnitude <= 0f;
				if (!flag2)
				{
					this.m_PendingPerformDrag = true;
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00043984 File Offset: 0x00041B84
		internal void OnPointerUpEvent(PointerUpEvent evt)
		{
			bool flag = !this.useDragEvents && this.m_DragState == DragEventsProcessor.DragState.Dragging;
			if (flag)
			{
				DragEventsProcessor dragEventsProcessor = this.GetDropTarget(evt.position) ?? this;
				dragEventsProcessor.UpdateDrag(evt.position);
				dragEventsProcessor.OnDrop(evt.position);
				dragEventsProcessor.ClearDragAndDropUI(false);
				evt.StopPropagation();
			}
			this.m_Target.ReleasePointer(evt.pointerId);
			this.ClearDragAndDropUI(this.m_DragState == DragEventsProcessor.DragState.Dragging);
			this.dragAndDrop.DragCleanup();
			this.m_DragState = DragEventsProcessor.DragState.None;
			this.m_PendingPerformDrag = false;
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00043A28 File Offset: 0x00041C28
		private void OnPointerLeaveEvent(PointerLeaveEvent evt)
		{
			this.ClearDragAndDropUI(false);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00043A33 File Offset: 0x00041C33
		private void OnPointerCancelEvent(PointerCancelEvent evt)
		{
			this.CancelDragAndDrop(evt.pointerId);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00043A43 File Offset: 0x00041C43
		private void OnPointerCapturedOut(PointerCaptureOutEvent evt)
		{
			this.CancelDragAndDrop(-1);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00043A50 File Offset: 0x00041C50
		private void OnGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = this.m_Target.resolvedStyle.display == DisplayStyle.None;
			if (flag)
			{
				this.CancelDragAndDrop(-1);
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00043A80 File Offset: 0x00041C80
		private void CancelDragAndDrop(int releaseCapturePointerId = -1)
		{
			bool flag = this.m_DragState == DragEventsProcessor.DragState.None && !this.m_PendingPerformDrag;
			if (!flag)
			{
				bool flag2 = !this.useDragEvents;
				if (flag2)
				{
					this.ClearDragAndDropUI(true);
				}
				bool flag3 = releaseCapturePointerId != -1;
				if (flag3)
				{
					this.m_Target.ReleasePointer(releaseCapturePointerId);
				}
				this.ClearDragAndDropUI(this.m_DragState == DragEventsProcessor.DragState.Dragging);
				this.dragAndDrop.DragCleanup();
				this.m_DragState = DragEventsProcessor.DragState.None;
				this.m_PendingPerformDrag = false;
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00043B00 File Offset: 0x00041D00
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool isHandledByDraggable = evt.isHandledByDraggable;
			if (!isHandledByDraggable)
			{
				bool flag = !this.useDragEvents && this.m_DragState == DragEventsProcessor.DragState.Dragging;
				if (flag)
				{
					DragEventsProcessor dragEventsProcessor = this.GetDropTarget(evt.position) ?? this;
					dragEventsProcessor.UpdateDrag(evt.position);
					this.m_PendingPerformDrag = false;
				}
				else
				{
					bool flag2 = this.m_DragState != DragEventsProcessor.DragState.CanStartDrag;
					if (!flag2)
					{
						bool flag3 = (this.m_Start - evt.position).sqrMagnitude >= 100f || this.m_PendingPerformDrag;
						if (flag3)
						{
							StartDragArgs startDragArgs = this.StartDrag(this.m_Start);
							bool flag4 = startDragArgs.visualMode == DragVisualMode.Rejected;
							if (flag4)
							{
								this.m_DragState = DragEventsProcessor.DragState.None;
							}
							else
							{
								bool flag5 = !this.useDragEvents;
								if (flag5)
								{
									bool supportsDragEvents = this.supportsDragEvents;
									if (supportsDragEvents)
									{
										this.dragAndDrop.StartDrag(startDragArgs, evt.position);
									}
								}
								else
								{
									bool flag6 = Event.current != null && Event.current.type != EventType.MouseDown && Event.current.type != EventType.MouseDrag;
									if (flag6)
									{
										return;
									}
									this.dragAndDrop.StartDrag(startDragArgs, evt.position);
								}
								this.m_DragState = DragEventsProcessor.DragState.Dragging;
								this.m_Target.CapturePointer(evt.pointerId);
								evt.isHandledByDraggable = true;
								this.m_PendingPerformDrag = false;
								evt.StopPropagation();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00043C88 File Offset: 0x00041E88
		private DragEventsProcessor GetDropTarget(Vector2 position)
		{
			DragEventsProcessor dragEventsProcessor = null;
			bool flag = this.m_Target.worldBound.Contains(position);
			if (flag)
			{
				dragEventsProcessor = this;
			}
			else
			{
				bool supportsDragEvents = this.supportsDragEvents;
				if (supportsDragEvents)
				{
					VisualElement visualElement = this.m_Target.elementPanel.Pick(position);
					BaseVerticalCollectionView baseVerticalCollectionView = ((visualElement != null) ? visualElement.GetFirstOfType<BaseVerticalCollectionView>() : null);
					dragEventsProcessor = ((baseVerticalCollectionView != null) ? baseVerticalCollectionView.dragger : null);
				}
			}
			return dragEventsProcessor;
		}

		// Token: 0x0400094C RID: 2380
		private bool m_IsRegistered;

		// Token: 0x0400094D RID: 2381
		private DragEventsProcessor.DragState m_DragState;

		// Token: 0x0400094E RID: 2382
		private Vector3 m_Start;

		// Token: 0x0400094F RID: 2383
		private bool m_PendingPerformDrag;

		// Token: 0x04000950 RID: 2384
		protected readonly VisualElement m_Target;

		// Token: 0x020001EE RID: 494
		internal enum DragState
		{
			// Token: 0x04000952 RID: 2386
			None,
			// Token: 0x04000953 RID: 2387
			CanStartDrag,
			// Token: 0x04000954 RID: 2388
			Dragging
		}
	}
}
