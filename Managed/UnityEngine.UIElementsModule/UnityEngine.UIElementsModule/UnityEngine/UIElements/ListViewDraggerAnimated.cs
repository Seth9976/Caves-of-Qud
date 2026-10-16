using System;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000200 RID: 512
	internal class ListViewDraggerAnimated : ListViewDragger
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x000454BC File Offset: 0x000436BC
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x000454C4 File Offset: 0x000436C4
		public bool isDragging { get; private set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x000454CD File Offset: 0x000436CD
		public ReusableCollectionItem draggedItem
		{
			get
			{
				return this.m_Item;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00011DCA File Offset: 0x0000FFCA
		protected override bool supportsDragEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000454D5 File Offset: 0x000436D5
		public ListViewDraggerAnimated(BaseVerticalCollectionView listView)
			: base(listView)
		{
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000454E0 File Offset: 0x000436E0
		protected internal override StartDragArgs StartDrag(Vector3 pointerPosition)
		{
			bool flag = !base.enabled;
			StartDragArgs startDragArgs;
			if (flag)
			{
				startDragArgs = base.StartDrag(pointerPosition);
			}
			else
			{
				base.targetView.ClearSelection();
				ReusableCollectionItem recycledItem = base.GetRecycledItem(pointerPosition);
				bool flag2 = recycledItem == null;
				if (flag2)
				{
					startDragArgs = new StartDragArgs(string.Empty, DragVisualMode.Rejected);
				}
				else
				{
					bool flag3 = base.targetView.selectionType > SelectionType.None;
					if (flag3)
					{
						base.targetView.SetSelection(recycledItem.index);
					}
					this.isDragging = true;
					this.m_Item = recycledItem;
					base.targetView.virtualizationController.StartDragItem(this.m_Item);
					float y = this.m_Item.rootElement.layout.y;
					this.m_SelectionHeight = this.m_Item.rootElement.layout.height;
					this.m_Item.rootElement.style.position = Position.Absolute;
					this.m_Item.rootElement.style.height = this.m_Item.rootElement.layout.height;
					this.m_Item.rootElement.style.width = this.m_Item.rootElement.layout.width;
					this.m_Item.rootElement.style.top = y;
					this.m_DragStartIndex = this.m_Item.index;
					this.m_CurrentIndex = this.m_DragStartIndex;
					this.m_CurrentPointerPosition = pointerPosition;
					this.m_LocalOffsetOnStart = base.targetScrollView.contentContainer.WorldToLocal(pointerPosition).y - y;
					ReusableCollectionItem recycledItemFromIndex = base.targetView.GetRecycledItemFromIndex(this.m_CurrentIndex + 1);
					bool flag4 = recycledItemFromIndex != null;
					if (flag4)
					{
						this.m_OffsetItem = recycledItemFromIndex;
						this.Animate(this.m_OffsetItem, this.m_SelectionHeight);
						this.m_OffsetItem.rootElement.style.paddingTop = this.m_SelectionHeight;
						bool flag5 = base.targetView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight;
						if (flag5)
						{
							this.m_OffsetItem.rootElement.style.height = base.targetView.fixedItemHeight + this.m_SelectionHeight;
						}
					}
					startDragArgs = base.dragAndDropController.SetupDragAndDrop(new int[] { this.m_Item.index }, true);
				}
			}
			return startDragArgs;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0004576C File Offset: 0x0004396C
		protected internal override void UpdateDrag(Vector3 pointerPosition)
		{
			bool flag = !base.enabled;
			if (flag)
			{
				base.UpdateDrag(pointerPosition);
			}
			else
			{
				bool flag2 = this.m_Item == null;
				if (!flag2)
				{
					base.HandleDragAndScroll(pointerPosition);
					this.m_CurrentPointerPosition = pointerPosition;
					Vector2 vector = base.targetScrollView.contentContainer.WorldToLocal(this.m_CurrentPointerPosition);
					Rect layout = this.m_Item.rootElement.layout;
					float height = base.targetScrollView.contentContainer.layout.height;
					layout.y = Mathf.Clamp(vector.y - this.m_LocalOffsetOnStart, 0f, height - this.m_SelectionHeight);
					float num = base.targetScrollView.contentContainer.resolvedStyle.paddingTop;
					this.m_CurrentIndex = -1;
					foreach (ReusableCollectionItem reusableCollectionItem in base.targetView.activeItems)
					{
						bool flag3 = reusableCollectionItem.index < 0 || (reusableCollectionItem.rootElement.style.display == DisplayStyle.None && !reusableCollectionItem.isDragGhost);
						if (!flag3)
						{
							bool flag4 = reusableCollectionItem.index == this.m_Item.index && reusableCollectionItem.index < base.targetView.itemsSource.Count - 1;
							if (flag4)
							{
								float expectedItemHeight = base.targetView.virtualizationController.GetExpectedItemHeight(reusableCollectionItem.index + 1);
								bool flag5 = Mathf.Approximately(layout.y + expectedItemHeight, height);
								if (!flag5)
								{
									bool flag6 = layout.y <= num + expectedItemHeight * 0.5f;
									if (flag6)
									{
										this.m_CurrentIndex = reusableCollectionItem.index;
									}
								}
							}
							else
							{
								float expectedItemHeight2 = base.targetView.virtualizationController.GetExpectedItemHeight(reusableCollectionItem.index);
								bool flag7 = layout.y <= num + expectedItemHeight2 * 0.5f;
								if (flag7)
								{
									bool flag8 = this.m_CurrentIndex == -1;
									if (flag8)
									{
										this.m_CurrentIndex = reusableCollectionItem.index;
									}
									bool flag9 = this.m_OffsetItem == reusableCollectionItem;
									if (flag9)
									{
										break;
									}
									this.Animate(this.m_OffsetItem, 0f);
									this.Animate(reusableCollectionItem, this.m_SelectionHeight);
									this.m_OffsetItem = reusableCollectionItem;
									break;
								}
								else
								{
									num += expectedItemHeight2;
								}
							}
						}
					}
					bool flag10 = this.m_CurrentIndex == -1;
					if (flag10)
					{
						this.m_CurrentIndex = base.targetView.itemsSource.Count;
						this.Animate(this.m_OffsetItem, 0f);
						this.m_OffsetItem = null;
					}
					this.m_Item.rootElement.layout = layout;
					this.m_Item.rootElement.BringToFront();
				}
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00045A80 File Offset: 0x00043C80
		private void Animate(ReusableCollectionItem element, float paddingTop)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = element.animator != null;
				if (flag2)
				{
					bool flag3 = (element.animator.isRunning && element.animator.to.paddingTop == paddingTop) || (!element.animator.isRunning && element.rootElement.style.paddingTop == paddingTop);
					if (flag3)
					{
						return;
					}
				}
				ValueAnimation<StyleValues> animator = element.animator;
				if (animator != null)
				{
					animator.Stop();
				}
				ValueAnimation<StyleValues> animator2 = element.animator;
				if (animator2 != null)
				{
					animator2.Recycle();
				}
				StyleValues styleValues = ((base.targetView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight) ? new StyleValues
				{
					paddingTop = paddingTop,
					height = base.targetView.ResolveItemHeight(-1f) + paddingTop
				} : new StyleValues
				{
					paddingTop = paddingTop
				});
				element.animator = element.rootElement.experimental.animation.Start(styleValues, 500);
				element.animator.KeepAlive();
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00045BA8 File Offset: 0x00043DA8
		protected internal override void OnDrop(Vector3 pointerPosition)
		{
			bool flag = !base.enabled;
			if (flag)
			{
				base.OnDrop(pointerPosition);
			}
			else
			{
				bool flag2 = this.m_Item == null;
				if (!flag2)
				{
					this.isDragging = false;
					this.m_Item.rootElement.ClearManualLayout();
					base.targetView.virtualizationController.EndDrag(this.m_CurrentIndex);
					bool flag3 = this.m_OffsetItem != null;
					if (flag3)
					{
						ValueAnimation<StyleValues> animator = this.m_OffsetItem.animator;
						if (animator != null)
						{
							animator.Stop();
						}
						ValueAnimation<StyleValues> animator2 = this.m_OffsetItem.animator;
						if (animator2 != null)
						{
							animator2.Recycle();
						}
						this.m_OffsetItem.animator = null;
						this.m_OffsetItem.rootElement.style.paddingTop = 0f;
						bool flag4 = base.targetView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight;
						if (flag4)
						{
							this.m_OffsetItem.rootElement.style.height = base.targetView.ResolveItemHeight(-1f);
						}
					}
					ListViewDragger.DragPosition dragPosition = new ListViewDragger.DragPosition
					{
						recycledItem = this.m_Item,
						insertAtIndex = this.m_CurrentIndex,
						dropPosition = DragAndDropPosition.BetweenItems
					};
					DragAndDropArgs dragAndDropArgs = base.MakeDragAndDropArgs(dragPosition);
					base.dragAndDropController.OnDrop(dragAndDropArgs);
					base.dragAndDrop.AcceptDrag();
					this.m_Item = null;
					this.m_OffsetItem = null;
				}
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00002152 File Offset: 0x00000352
		protected override void ClearDragAndDropUI(bool dragCancelled)
		{
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00045D24 File Offset: 0x00043F24
		protected override bool TryGetDragPosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			dragPosition.recycledItem = this.m_Item;
			dragPosition.insertAtIndex = this.m_CurrentIndex;
			dragPosition.dropPosition = DragAndDropPosition.BetweenItems;
			return true;
		}

		// Token: 0x04000984 RID: 2436
		private int m_DragStartIndex;

		// Token: 0x04000985 RID: 2437
		private int m_CurrentIndex;

		// Token: 0x04000986 RID: 2438
		private float m_SelectionHeight;

		// Token: 0x04000987 RID: 2439
		private float m_LocalOffsetOnStart;

		// Token: 0x04000988 RID: 2440
		private Vector3 m_CurrentPointerPosition;

		// Token: 0x04000989 RID: 2441
		private ReusableCollectionItem m_Item;

		// Token: 0x0400098A RID: 2442
		private ReusableCollectionItem m_OffsetItem;
	}
}
