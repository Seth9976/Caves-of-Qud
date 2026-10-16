using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000077 RID: 119
	internal class ReusableListViewItem : ReusableCollectionItem
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00014A95 File Offset: 0x00012C95
		public override VisualElement rootElement
		{
			get
			{
				return this.m_Container ?? base.bindableElement;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public void Init(VisualElement item, bool usesAnimatedDragger)
		{
			base.Init(item);
			VisualElement visualElement = new VisualElement
			{
				name = BaseListView.reorderableItemUssClassName
			};
			this.UpdateHierarchy(visualElement, base.bindableElement, usesAnimatedDragger);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00014AE0 File Offset: 0x00012CE0
		protected void UpdateHierarchy(VisualElement root, VisualElement item, bool usesAnimatedDragger)
		{
			if (usesAnimatedDragger)
			{
				bool flag = this.m_Container != null;
				if (!flag)
				{
					this.m_Container = root;
					this.m_Container.AddToClassList(BaseListView.reorderableItemUssClassName);
					this.m_DragHandle = new VisualElement
					{
						name = BaseListView.reorderableItemHandleUssClassName
					};
					this.m_DragHandle.AddToClassList(BaseListView.reorderableItemHandleUssClassName);
					VisualElement visualElement = new VisualElement
					{
						name = BaseListView.reorderableItemHandleBarUssClassName
					};
					visualElement.AddToClassList(BaseListView.reorderableItemHandleBarUssClassName);
					this.m_DragHandle.Add(visualElement);
					VisualElement visualElement2 = new VisualElement
					{
						name = BaseListView.reorderableItemHandleBarUssClassName
					};
					visualElement2.AddToClassList(BaseListView.reorderableItemHandleBarUssClassName);
					this.m_DragHandle.Add(visualElement2);
					this.m_ItemContainer = new VisualElement
					{
						name = BaseListView.reorderableItemContainerUssClassName
					};
					this.m_ItemContainer.AddToClassList(BaseListView.reorderableItemContainerUssClassName);
					this.m_ItemContainer.Add(item);
					this.m_Container.Add(this.m_DragHandle);
					this.m_Container.Add(this.m_ItemContainer);
				}
			}
			else
			{
				bool flag2 = this.m_Container == null;
				if (!flag2)
				{
					this.m_Container.RemoveFromHierarchy();
					this.m_Container = null;
				}
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00014C24 File Offset: 0x00012E24
		public void UpdateDragHandle(bool needsDragHandle)
		{
			if (needsDragHandle)
			{
				bool flag = this.m_DragHandle.parent == null;
				if (flag)
				{
					this.rootElement.Insert(0, this.m_DragHandle);
					this.rootElement.AddToClassList(BaseListView.reorderableItemUssClassName);
				}
			}
			else
			{
				VisualElement dragHandle = this.m_DragHandle;
				bool flag2 = ((dragHandle != null) ? dragHandle.parent : null) != null;
				if (flag2)
				{
					this.m_DragHandle.RemoveFromHierarchy();
					this.rootElement.RemoveFromClassList(BaseListView.reorderableItemUssClassName);
				}
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00014CB0 File Offset: 0x00012EB0
		public void SetDragHandleEnabled(bool enabled)
		{
			bool flag = this.m_DragHandle != null;
			if (flag)
			{
				this.m_DragHandle.SetEnabled(enabled);
				this.m_DragHandle.tooltip = (enabled ? null : ReusableListViewItem.k_SortingDisablesReorderingTooltip);
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00014CF1 File Offset: 0x00012EF1
		public override void PreAttachElement()
		{
			base.PreAttachElement();
			this.rootElement.AddToClassList(BaseListView.itemUssClassName);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00014D0C File Offset: 0x00012F0C
		public override void DetachElement()
		{
			base.DetachElement();
			this.rootElement.RemoveFromClassList(BaseListView.itemUssClassName);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00014D28 File Offset: 0x00012F28
		public override void SetDragGhost(bool dragGhost)
		{
			base.SetDragGhost(dragGhost);
			bool flag = this.m_DragHandle != null;
			if (flag)
			{
				this.m_DragHandle.EnableInClassList("unity-hidden", base.isDragGhost);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00014D64 File Offset: 0x00012F64
		protected override void OnGeometryChanged(GeometryChangedEvent evt)
		{
			base.OnGeometryChanged(evt);
			VisualElement itemContainer = this.m_ItemContainer;
			if (itemContainer != null)
			{
				itemContainer.UpdateWorldTransform();
			}
		}

		// Token: 0x0400022E RID: 558
		private static readonly string k_SortingDisablesReorderingTooltip = "Reordering is disabled when the collection is being sorted.";

		// Token: 0x0400022F RID: 559
		private VisualElement m_Container;

		// Token: 0x04000230 RID: 560
		private VisualElement m_DragHandle;

		// Token: 0x04000231 RID: 561
		private VisualElement m_ItemContainer;
	}
}
