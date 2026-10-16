using System;
using System.Diagnostics;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x02000729 RID: 1833
	internal class ColumnMover : PointerManipulator
	{
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000CE2C9 File Offset: 0x000CC4C9
		// (set) Token: 0x0600352B RID: 13611 RVA: 0x000CE2D1 File Offset: 0x000CC4D1
		public ColumnLayout columnLayout { get; set; }

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x000CE2DA File Offset: 0x000CC4DA
		// (set) Token: 0x0600352D RID: 13613 RVA: 0x000CE2E4 File Offset: 0x000CC4E4
		public bool active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				bool flag = this.m_Active == value;
				if (!flag)
				{
					this.m_Active = value;
					Action<ColumnMover> action = this.activeChanged;
					if (action != null)
					{
						action(this);
					}
				}
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x000CE31B File Offset: 0x000CC51B
		// (set) Token: 0x0600352F RID: 13615 RVA: 0x000CE324 File Offset: 0x000CC524
		public bool moving
		{
			get
			{
				return this.m_Moving;
			}
			set
			{
				bool flag = this.m_Moving == value;
				if (!flag)
				{
					this.m_Moving = value;
					Action<ColumnMover> action = this.movingChanged;
					if (action != null)
					{
						action(this);
					}
				}
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06003530 RID: 13616 RVA: 0x000CE35C File Offset: 0x000CC55C
		// (remove) Token: 0x06003531 RID: 13617 RVA: 0x000CE394 File Offset: 0x000CC594
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ColumnMover> activeChanged;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06003532 RID: 13618 RVA: 0x000CE3CC File Offset: 0x000CC5CC
		// (remove) Token: 0x06003533 RID: 13619 RVA: 0x000CE404 File Offset: 0x000CC604
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ColumnMover> movingChanged;

		// Token: 0x06003534 RID: 13620 RVA: 0x000CE43C File Offset: 0x000CC63C
		public ColumnMover()
		{
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000CE470 File Offset: 0x000CC670
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000CE514 File Offset: 0x000CC714
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000CE5B8 File Offset: 0x000CC7B8
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000CE5F0 File Offset: 0x000CC7F0
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessMoveEvent(evt, evt.localPosition);
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000CE620 File Offset: 0x000CC820
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000CE664 File Offset: 0x000CC864
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000CE69C File Offset: 0x000CC89C
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000CE6C8 File Offset: 0x000CC8C8
		protected void ProcessCancelEvent(EventBase evt, int pointerId)
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
			evt.StopPropagation();
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000CE728 File Offset: 0x000CC928
		private void OnKeyDown(KeyDownEvent e)
		{
			bool flag = e.keyCode == KeyCode.Escape && this.moving;
			if (flag)
			{
				this.EndDragMove(true);
			}
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000CE758 File Offset: 0x000CC958
		private void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			bool active = this.active;
			if (active)
			{
				evt.StopImmediatePropagation();
			}
			else
			{
				base.target.CapturePointer(pointerId);
				bool flag = !(evt is IPointerEvent);
				if (flag)
				{
					base.target.panel.ProcessPointerCapture(pointerId);
				}
				VisualElement visualElement = evt.currentTarget as VisualElement;
				MultiColumnCollectionHeader firstAncestorOfType = visualElement.GetFirstAncestorOfType<MultiColumnCollectionHeader>();
				bool flag2 = !firstAncestorOfType.columns.reorderable;
				if (!flag2)
				{
					this.m_Header = firstAncestorOfType;
					Vector2 vector = visualElement.ChangeCoordinatesTo(this.m_Header, localPosition);
					this.columnLayout = this.m_Header.columnLayout;
					this.m_Cancelled = false;
					this.m_StartPos = vector.x;
					this.active = true;
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000CE820 File Offset: 0x000CCA20
		private void ProcessMoveEvent(EventBase e, Vector2 localPosition)
		{
			bool cancelled = this.m_Cancelled;
			if (!cancelled)
			{
				VisualElement visualElement = e.currentTarget as VisualElement;
				Vector2 vector = visualElement.ChangeCoordinatesTo(this.m_Header, localPosition);
				bool flag = !this.moving && Mathf.Abs(this.m_StartPos - vector.x) > 5f;
				if (flag)
				{
					this.BeginDragMove(this.m_StartPos);
				}
				bool moving = this.moving;
				if (moving)
				{
					this.DragMove(vector.x);
				}
				e.StopPropagation();
			}
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000CE8B0 File Offset: 0x000CCAB0
		private void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			bool flag2 = this.moving || this.m_Cancelled;
			this.EndDragMove(false);
			bool flag3 = flag2;
			if (flag3)
			{
				evt.StopImmediatePropagation();
			}
			else
			{
				evt.StopPropagation();
			}
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000CE924 File Offset: 0x000CCB24
		private void BeginDragMove(float pos)
		{
			float num = 0f;
			Columns columns = this.columnLayout.columns;
			foreach (Column column in columns.visibleList)
			{
				num += this.columnLayout.GetDesiredWidth(column);
				bool flag = this.m_ColumnToMove == null;
				if (flag)
				{
					bool flag2 = num > pos;
					if (flag2)
					{
						this.m_ColumnToMove = column;
					}
				}
			}
			this.moving = true;
			this.m_LastPos = pos;
			this.m_PreviewElement = new MultiColumnHeaderColumnMovePreview();
			this.m_LocationPreviewElement = new MultiColumnHeaderColumnMoveLocationPreview();
			this.m_Header.hierarchy.Add(this.m_PreviewElement);
			ScrollView firstAncestorOfType = this.m_Header.GetFirstAncestorOfType<ScrollView>();
			VisualElement visualElement = ((firstAncestorOfType != null) ? firstAncestorOfType.parent : null) ?? this.m_Header;
			visualElement.hierarchy.Add(this.m_LocationPreviewElement);
			this.m_ColumnToMovePos = this.columnLayout.GetDesiredPosition(this.m_ColumnToMove);
			this.m_ColumnToMoveWidth = this.columnLayout.GetDesiredWidth(this.m_ColumnToMove);
			this.UpdateMoveLocation();
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000CEA64 File Offset: 0x000CCC64
		internal void DragMove(float pos)
		{
			this.m_LastPos = pos;
			this.UpdateMoveLocation();
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000CEA78 File Offset: 0x000CCC78
		private void UpdatePreviewPosition()
		{
			this.m_PreviewElement.style.left = this.m_ColumnToMovePos + this.m_LastPos - this.m_StartPos;
			this.m_PreviewElement.style.width = this.m_ColumnToMoveWidth;
			bool flag = this.m_DestinationColumn != null;
			if (flag)
			{
				this.m_LocationPreviewElement.style.left = this.columnLayout.GetDesiredPosition(this.m_DestinationColumn) + ((!this.m_MoveBeforeDestination) ? this.columnLayout.GetDesiredWidth(this.m_DestinationColumn) : 0f);
			}
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000CEB24 File Offset: 0x000CCD24
		private void UpdateMoveLocation()
		{
			float num = 0f;
			this.m_DestinationColumn = null;
			this.m_MoveBeforeDestination = false;
			foreach (Column column in this.columnLayout.columns.visibleList)
			{
				this.m_DestinationColumn = column;
				float desiredWidth = this.columnLayout.GetDesiredWidth(this.m_DestinationColumn);
				float num2 = num + desiredWidth / 2f;
				num += desiredWidth;
				bool flag = num > this.m_LastPos;
				if (flag)
				{
					this.m_MoveBeforeDestination = this.m_LastPos < num2;
					break;
				}
			}
			this.UpdatePreviewPosition();
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000CEBE0 File Offset: 0x000CCDE0
		private void EndDragMove(bool cancelled)
		{
			bool flag = !this.moving || this.m_Cancelled;
			if (!flag)
			{
				this.m_Cancelled = cancelled;
				bool flag2 = !cancelled;
				if (flag2)
				{
					int num = this.m_DestinationColumn.displayIndex;
					bool flag3 = !this.m_MoveBeforeDestination;
					if (flag3)
					{
						num++;
					}
					bool flag4 = this.m_ColumnToMove.displayIndex < num;
					if (flag4)
					{
						num--;
					}
					bool flag5 = this.m_ColumnToMove.displayIndex != num;
					if (flag5)
					{
						this.columnLayout.columns.ReorderDisplay(this.m_ColumnToMove.displayIndex, num);
					}
				}
				VisualElement previewElement = this.m_PreviewElement;
				if (previewElement != null)
				{
					previewElement.RemoveFromHierarchy();
				}
				this.m_PreviewElement = null;
				MultiColumnHeaderColumnMoveLocationPreview locationPreviewElement = this.m_LocationPreviewElement;
				if (locationPreviewElement != null)
				{
					locationPreviewElement.RemoveFromHierarchy();
				}
				this.m_LocationPreviewElement = null;
				this.m_ColumnToMove = null;
				this.moving = false;
			}
		}

		// Token: 0x04001B0C RID: 6924
		private const float k_StartDragDistance = 5f;

		// Token: 0x04001B0D RID: 6925
		private float m_StartPos;

		// Token: 0x04001B0E RID: 6926
		private float m_LastPos;

		// Token: 0x04001B0F RID: 6927
		private bool m_Active;

		// Token: 0x04001B10 RID: 6928
		private bool m_Moving;

		// Token: 0x04001B11 RID: 6929
		private bool m_Cancelled;

		// Token: 0x04001B12 RID: 6930
		private MultiColumnCollectionHeader m_Header;

		// Token: 0x04001B13 RID: 6931
		private VisualElement m_PreviewElement;

		// Token: 0x04001B14 RID: 6932
		private MultiColumnHeaderColumnMoveLocationPreview m_LocationPreviewElement;

		// Token: 0x04001B15 RID: 6933
		private Column m_ColumnToMove;

		// Token: 0x04001B16 RID: 6934
		private float m_ColumnToMovePos;

		// Token: 0x04001B17 RID: 6935
		private float m_ColumnToMoveWidth;

		// Token: 0x04001B18 RID: 6936
		private Column m_DestinationColumn;

		// Token: 0x04001B19 RID: 6937
		private bool m_MoveBeforeDestination;
	}
}
