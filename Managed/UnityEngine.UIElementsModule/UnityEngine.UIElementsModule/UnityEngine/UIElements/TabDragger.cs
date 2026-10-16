using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A1 RID: 417
	internal class TabDragger : PointerManipulator
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0003A71A File Offset: 0x0003891A
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x0003A722 File Offset: 0x00038922
		private TabLayout tabLayout { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0003A72B File Offset: 0x0003892B
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x0003A733 File Offset: 0x00038933
		internal bool active { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0003A73C File Offset: 0x0003893C
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x0003A744 File Offset: 0x00038944
		internal bool isVertical { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0003A74D File Offset: 0x0003894D
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x0003A758 File Offset: 0x00038958
		internal bool moving
		{
			get
			{
				return this.m_Moving;
			}
			private set
			{
				bool flag = this.m_Moving == value;
				if (!flag)
				{
					this.m_Moving = value;
					this.m_TabToMove.EnableInClassList(Tab.draggingUssClassName, this.moving);
				}
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003A794 File Offset: 0x00038994
		public TabDragger()
		{
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0003A7D4 File Offset: 0x000389D4
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
			base.target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0003A878 File Offset: 0x00038A78
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0003A91C File Offset: 0x00038B1C
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				bool active = this.active;
				if (active)
				{
					evt.StopImmediatePropagation();
				}
				else
				{
					this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
				}
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0003A968 File Offset: 0x00038B68
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessMoveEvent(evt, evt.localPosition);
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0003A998 File Offset: 0x00038B98
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0003A9DC File Offset: 0x00038BDC
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0003AA14 File Offset: 0x00038C14
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003AA40 File Offset: 0x00038C40
		private void ProcessCancelEvent(EventBase evt, int pointerId)
		{
			this.active = false;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			bool moving = this.moving;
			if (moving)
			{
				this.EndDragMove(true);
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003AA98 File Offset: 0x00038C98
		private void OnKeyDown(KeyDownEvent e)
		{
			bool flag = e.keyCode == KeyCode.Escape && this.moving;
			if (flag)
			{
				this.active = false;
				bool flag2 = this.m_DraggingPointerId != PointerId.invalidPointerId;
				if (flag2)
				{
					base.target.ReleasePointer(this.m_DraggingPointerId);
				}
				this.EndDragMove(true);
				e.StopPropagation();
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003AB00 File Offset: 0x00038D00
		private void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			VisualElement visualElement = evt.currentTarget as VisualElement;
			TabView tabView = ((visualElement != null) ? visualElement.GetFirstAncestorOfType<TabView>() : null);
			bool flag = tabView == null || !tabView.reorderable;
			if (!flag)
			{
				base.target.CapturePointer(pointerId);
				this.m_DraggingPointerId = pointerId;
				bool flag2 = !(evt is IPointerEvent);
				if (flag2)
				{
					base.target.panel.ProcessPointerCapture(pointerId);
				}
				this.m_TabView = tabView;
				this.m_Header = tabView.header;
				this.isVertical = this.m_Header.resolvedStyle.flexDirection == FlexDirection.Column;
				this.tabLayout = new TabLayout(this.m_TabView, this.isVertical);
				Vector2 vector = visualElement.ChangeCoordinatesTo(this.m_Header, localPosition);
				this.m_Cancelled = false;
				this.m_StartPos = (this.isVertical ? vector.y : vector.x);
				this.active = true;
				evt.StopPropagation();
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003ABFC File Offset: 0x00038DFC
		private void ProcessMoveEvent(EventBase e, Vector2 localPosition)
		{
			bool cancelled = this.m_Cancelled;
			if (!cancelled)
			{
				VisualElement visualElement = e.currentTarget as VisualElement;
				Vector2 vector = visualElement.ChangeCoordinatesTo(this.m_Header, localPosition);
				float num = (this.isVertical ? vector.y : vector.x);
				bool flag = !this.moving && Mathf.Abs(this.m_StartPos - num) > 5f;
				if (flag)
				{
					this.BeginDragMove(this.m_StartPos);
				}
				bool moving = this.moving;
				if (moving)
				{
					this.DragMove(num);
				}
				e.StopPropagation();
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0003AC9C File Offset: 0x00038E9C
		private void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			this.EndDragMove(false);
			evt.StopPropagation();
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003ACF0 File Offset: 0x00038EF0
		private void BeginDragMove(float pos)
		{
			float num = 0f;
			List<VisualElement> tabHeaders = this.m_TabView.tabHeaders;
			this.m_TabToMove = this.m_TabView.tabHeaders[0];
			foreach (VisualElement visualElement in tabHeaders)
			{
				num += (this.isVertical ? TabLayout.GetHeight(visualElement) : TabLayout.GetWidth(visualElement));
				bool flag = num > pos;
				if (flag)
				{
					this.m_TabToMove = visualElement;
					break;
				}
			}
			this.moving = true;
			this.m_LastPos = pos;
			this.m_PreviewElement = new TabDragPreview();
			this.m_LocationPreviewElement = new TabDragLocationPreview
			{
				classList = { this.isVertical ? TabDragLocationPreview.verticalUssClassName : TabDragLocationPreview.horizontalUssClassName }
			};
			this.m_Header.hierarchy.Add(this.m_PreviewElement);
			this.m_Header.Add(this.m_LocationPreviewElement);
			int num2 = this.m_TabView.tabHeaders.IndexOf(this.m_TabToMove);
			Tab tab = this.m_TabView.tabs[num2];
			this.m_TabView.activeTab = tab;
			this.m_TabToMovePos = this.tabLayout.GetTabOffset(this.m_TabToMove);
			this.UpdateMoveLocation();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003AE60 File Offset: 0x00039060
		private void DragMove(float pos)
		{
			this.m_LastPos = pos;
			this.UpdateMoveLocation();
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003AE74 File Offset: 0x00039074
		private void UpdatePreviewPosition()
		{
			float num = this.m_TabToMovePos + this.m_LastPos - this.m_StartPos;
			float width = TabLayout.GetWidth(this.m_TabToMove);
			float tabOffset = this.tabLayout.GetTabOffset(this.m_DestinationTab);
			float num2 = (this.isVertical ? TabLayout.GetHeight(this.m_DestinationTab) : TabLayout.GetWidth(this.m_DestinationTab));
			float num3 = ((!this.m_MoveBeforeDestination) ? num2 : 0f);
			bool isVertical = this.isVertical;
			if (isVertical)
			{
				this.m_PreviewElement.style.top = num;
				this.m_PreviewElement.style.height = TabLayout.GetHeight(this.m_TabToMove);
				this.m_PreviewElement.style.width = width;
				bool flag = this.m_DestinationTab != null;
				if (flag)
				{
					this.m_LocationPreviewElement.preview.style.width = width;
					this.m_LocationPreviewElement.style.top = tabOffset + num3;
				}
			}
			else
			{
				this.m_PreviewElement.style.left = num;
				this.m_PreviewElement.style.width = width;
				bool flag2 = this.m_DestinationTab != null;
				if (flag2)
				{
					this.m_LocationPreviewElement.style.left = tabOffset + num3;
				}
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003AFEC File Offset: 0x000391EC
		private void UpdateMoveLocation()
		{
			float num = 0f;
			this.m_DestinationTab = null;
			this.m_MoveBeforeDestination = false;
			foreach (VisualElement visualElement in this.m_TabView.tabHeaders)
			{
				this.m_DestinationTab = visualElement;
				float num2 = (this.isVertical ? TabLayout.GetHeight(this.m_DestinationTab) : TabLayout.GetWidth(this.m_DestinationTab));
				float num3 = num + num2 / 2f;
				num += num2;
				bool flag = num > this.m_LastPos;
				if (flag)
				{
					this.m_MoveBeforeDestination = this.m_LastPos < num3;
					break;
				}
			}
			this.UpdatePreviewPosition();
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003B0B8 File Offset: 0x000392B8
		private void EndDragMove(bool cancelled)
		{
			bool flag = !this.moving || this.m_Cancelled;
			if (!flag)
			{
				this.m_Cancelled = cancelled;
				bool flag2 = !cancelled;
				if (flag2)
				{
					int num = this.m_TabView.tabHeaders.IndexOf(this.m_TabToMove);
					int num2 = this.m_TabView.tabHeaders.IndexOf(this.m_DestinationTab);
					bool flag3 = !this.m_MoveBeforeDestination;
					if (flag3)
					{
						num2++;
					}
					bool flag4 = num < num2;
					if (flag4)
					{
						num2--;
					}
					bool flag5 = num != num2;
					if (flag5)
					{
						this.tabLayout.ReorderDisplay(num, num2);
					}
				}
				VisualElement previewElement = this.m_PreviewElement;
				if (previewElement != null)
				{
					previewElement.RemoveFromHierarchy();
				}
				this.m_PreviewElement = null;
				TabDragLocationPreview locationPreviewElement = this.m_LocationPreviewElement;
				if (locationPreviewElement != null)
				{
					locationPreviewElement.RemoveFromHierarchy();
				}
				this.m_LocationPreviewElement = null;
				this.moving = false;
				this.m_TabToMove = null;
			}
		}

		// Token: 0x04000830 RID: 2096
		private const float k_StartDragDistance = 5f;

		// Token: 0x04000831 RID: 2097
		private float m_StartPos;

		// Token: 0x04000832 RID: 2098
		private float m_LastPos;

		// Token: 0x04000833 RID: 2099
		private bool m_Moving;

		// Token: 0x04000834 RID: 2100
		private bool m_Cancelled;

		// Token: 0x04000835 RID: 2101
		private VisualElement m_Header;

		// Token: 0x04000836 RID: 2102
		private TabView m_TabView;

		// Token: 0x04000837 RID: 2103
		private VisualElement m_PreviewElement;

		// Token: 0x04000838 RID: 2104
		private TabDragLocationPreview m_LocationPreviewElement;

		// Token: 0x04000839 RID: 2105
		private VisualElement m_TabToMove;

		// Token: 0x0400083A RID: 2106
		private float m_TabToMovePos;

		// Token: 0x0400083B RID: 2107
		private VisualElement m_DestinationTab;

		// Token: 0x0400083C RID: 2108
		private bool m_MoveBeforeDestination;

		// Token: 0x0400083D RID: 2109
		private int m_DraggingPointerId = PointerId.invalidPointerId;
	}
}
