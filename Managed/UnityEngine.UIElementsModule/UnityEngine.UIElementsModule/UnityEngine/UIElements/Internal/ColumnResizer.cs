using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x0200072B RID: 1835
	internal class ColumnResizer : PointerManipulator
	{
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000CED3D File Offset: 0x000CCF3D
		// (set) Token: 0x06003549 RID: 13641 RVA: 0x000CED45 File Offset: 0x000CCF45
		public ColumnLayout columnLayout { get; set; }

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x000CED4E File Offset: 0x000CCF4E
		// (set) Token: 0x0600354B RID: 13643 RVA: 0x000CED56 File Offset: 0x000CCF56
		public bool preview { get; set; }

		// Token: 0x0600354C RID: 13644 RVA: 0x000CED60 File Offset: 0x000CCF60
		public ColumnResizer(Column column)
		{
			this.m_Column = column;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.m_Active = false;
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000CEDA4 File Offset: 0x000CCFA4
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000CEE18 File Offset: 0x000CD018
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000CEE8C File Offset: 0x000CD08C
		private void OnKeyDown(KeyDownEvent e)
		{
			bool flag = e.keyCode == KeyCode.Escape && this.m_Resizing && this.preview;
			if (flag)
			{
				this.EndDragResize(0f, true);
			}
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000CEEC8 File Offset: 0x000CD0C8
		private void OnPointerDown(PointerDownEvent e)
		{
			bool active = this.m_Active;
			if (active)
			{
				e.StopImmediatePropagation();
			}
			else
			{
				bool flag = base.CanStartManipulation(e);
				if (flag)
				{
					VisualElement visualElement = e.currentTarget as VisualElement;
					this.m_Header = visualElement.GetFirstAncestorOfType<MultiColumnCollectionHeader>();
					this.preview = this.m_Column.collection.resizePreview;
					bool preview = this.preview;
					if (preview)
					{
						bool flag2 = this.m_PreviewElement == null;
						if (flag2)
						{
							this.m_PreviewElement = new MultiColumnHeaderColumnResizePreview();
						}
						ScrollView firstAncestorOfType = this.m_Header.GetFirstAncestorOfType<ScrollView>();
						VisualElement visualElement2 = ((firstAncestorOfType != null) ? firstAncestorOfType.parent : null) ?? this.m_Header.parent;
						visualElement2.hierarchy.Add(this.m_PreviewElement);
					}
					this.columnLayout = this.m_Header.columnLayout;
					this.m_Start = visualElement.ChangeCoordinatesTo(this.m_Header, e.localPosition);
					this.BeginDragResize(this.m_Start.x);
					this.m_Active = true;
					base.target.CaptureMouse();
					e.StopPropagation();
				}
			}
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000CEFEC File Offset: 0x000CD1EC
		private void OnPointerMove(PointerMoveEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId);
			if (!flag)
			{
				VisualElement visualElement = e.currentTarget as VisualElement;
				Vector2 vector = visualElement.ChangeCoordinatesTo(this.m_Header, e.localPosition);
				this.DragResize(vector.x);
				e.StopPropagation();
			}
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000CF058 File Offset: 0x000CD258
		private void OnPointerUp(PointerUpEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId) || !base.CanStopManipulation(e);
			if (!flag)
			{
				VisualElement visualElement = e.currentTarget as VisualElement;
				Vector2 vector = visualElement.ChangeCoordinatesTo(this.m_Header, e.localPosition);
				this.EndDragResize(vector.x, false);
				this.m_Active = false;
				base.target.ReleasePointer(e.pointerId);
				e.StopPropagation();
			}
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000CF0E8 File Offset: 0x000CD2E8
		private void BeginDragResize(float pos)
		{
			this.m_Resizing = true;
			ColumnLayout columnLayout = this.columnLayout;
			if (columnLayout != null)
			{
				columnLayout.BeginDragResize(this.m_Column, this.m_Start.x, this.preview);
			}
			bool preview = this.preview;
			if (preview)
			{
				this.UpdatePreviewPosition();
			}
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000CF13C File Offset: 0x000CD33C
		private void DragResize(float pos)
		{
			bool flag = !this.m_Resizing;
			if (!flag)
			{
				ColumnLayout columnLayout = this.columnLayout;
				if (columnLayout != null)
				{
					columnLayout.DragResize(this.m_Column, pos);
				}
				bool preview = this.preview;
				if (preview)
				{
					this.UpdatePreviewPosition();
				}
			}
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000CF185 File Offset: 0x000CD385
		private void UpdatePreviewPosition()
		{
			this.m_PreviewElement.style.left = this.columnLayout.GetDesiredPosition(this.m_Column) + this.columnLayout.GetDesiredWidth(this.m_Column);
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000CF1C4 File Offset: 0x000CD3C4
		private void EndDragResize(float pos, bool cancelled)
		{
			bool flag = !this.m_Resizing;
			if (!flag)
			{
				bool preview = this.preview;
				if (preview)
				{
					VisualElement previewElement = this.m_PreviewElement;
					if (previewElement != null)
					{
						previewElement.RemoveFromHierarchy();
					}
					this.m_PreviewElement = null;
				}
				ColumnLayout columnLayout = this.columnLayout;
				if (columnLayout != null)
				{
					columnLayout.EndDragResize(this.m_Column, cancelled);
				}
				this.m_Resizing = false;
			}
		}

		// Token: 0x04001B1F RID: 6943
		private Vector2 m_Start;

		// Token: 0x04001B20 RID: 6944
		protected bool m_Active;

		// Token: 0x04001B21 RID: 6945
		private bool m_Resizing;

		// Token: 0x04001B22 RID: 6946
		private MultiColumnCollectionHeader m_Header;

		// Token: 0x04001B23 RID: 6947
		private Column m_Column;

		// Token: 0x04001B24 RID: 6948
		private VisualElement m_PreviewElement;
	}
}
