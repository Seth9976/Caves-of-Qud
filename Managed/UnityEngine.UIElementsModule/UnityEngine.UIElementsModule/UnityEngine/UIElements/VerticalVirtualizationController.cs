using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007B RID: 123
	internal abstract class VerticalVirtualizationController<T> : CollectionVirtualizationController where T : ReusableCollectionItem, new()
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00015428 File Offset: 0x00013628
		public override IEnumerable<ReusableCollectionItem> activeItems
		{
			get
			{
				return this.m_ActiveItems;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00015430 File Offset: 0x00013630
		internal int itemsCount
		{
			get
			{
				CollectionViewController viewController = this.m_CollectionView.viewController;
				return (viewController != null) ? viewController.GetItemsCount() : this.m_CollectionView.itemsSource.Count;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00015458 File Offset: 0x00013658
		protected virtual bool VisibleItemPredicate(T i)
		{
			return i.rootElement.style.display == DisplayStyle.Flex;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001547C File Offset: 0x0001367C
		internal T firstVisibleItem
		{
			get
			{
				foreach (T t in this.m_ActiveItems)
				{
					bool flag = this.m_VisibleItemPredicateDelegate(t);
					if (flag)
					{
						return t;
					}
				}
				return default(T);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000154F0 File Offset: 0x000136F0
		internal T lastVisibleItem
		{
			get
			{
				int i = this.m_ActiveItems.Count;
				while (i > 0)
				{
					T t = this.m_ActiveItems[--i];
					bool flag = this.m_VisibleItemPredicateDelegate(t);
					if (flag)
					{
						return t;
					}
				}
				return default(T);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001554C File Offset: 0x0001374C
		public override int visibleItemCount
		{
			get
			{
				int num = 0;
				foreach (T t in this.m_ActiveItems)
				{
					bool flag = this.m_VisibleItemPredicateDelegate(t);
					if (flag)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000155B8 File Offset: 0x000137B8
		protected SerializedVirtualizationData serializedData
		{
			get
			{
				return this.m_CollectionView.serializedVirtualizationData;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000155C5 File Offset: 0x000137C5
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00015603 File Offset: 0x00013803
		public override int firstVisibleIndex
		{
			get
			{
				return Mathf.Min(this.serializedData.firstVisibleIndex, (this.m_CollectionView.viewController != null) ? (this.m_CollectionView.viewController.GetItemsCount() - 1) : this.serializedData.firstVisibleIndex);
			}
			protected set
			{
				this.serializedData.firstVisibleIndex = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00015611 File Offset: 0x00013811
		protected float lastHeight
		{
			get
			{
				return this.m_CollectionView.lastHeight;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000E724 File Offset: 0x0000C924
		protected virtual bool alwaysRebindOnRefresh
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015620 File Offset: 0x00013820
		protected VerticalVirtualizationController(BaseVerticalCollectionView collectionView)
			: base(collectionView.scrollView)
		{
			this.m_CollectionView = collectionView;
			this.m_ActiveItems = new List<T>();
			this.m_VisibleItemPredicateDelegate = new Func<T, bool>(this.VisibleItemPredicate);
			this.m_PerformDeferredScrollToItem = new Action(this.PerformDeferredScrollToItem);
			this.m_ScrollCallback = new Action(this.OnScrollUpdate);
			this.m_ScrollView.contentContainer.disableClipping = false;
			collectionView.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanelEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001573C File Offset: 0x0001393C
		public override void Refresh(bool rebuild)
		{
			bool flag = this.m_CollectionView.HasValidDataAndBindings();
			BaseVerticalCollectionView collectionView = this.m_CollectionView;
			IList itemsSource = this.m_CollectionView.itemsSource;
			collectionView.m_PreviousRefreshedCount = ((itemsSource != null) ? itemsSource.Count : 0);
			for (int i = 0; i < this.m_ActiveItems.Count; i++)
			{
				int num = this.firstVisibleIndex + i;
				T t = this.m_ActiveItems[i];
				bool flag2 = t.rootElement.style.display == DisplayStyle.Flex;
				if (rebuild)
				{
					bool flag3 = flag && t.index != -1;
					if (flag3)
					{
						this.m_CollectionView.viewController.InvokeUnbindItem(t, t.index);
					}
					this.m_Pool.Release(t);
				}
				else
				{
					bool flag4 = this.m_CollectionView.itemsSource != null && num >= 0 && num < this.itemsCount;
					if (flag4)
					{
						bool flag5 = !flag;
						if (!flag5)
						{
							bool flag6 = t.index != -1;
							if (flag6)
							{
								this.m_CollectionView.viewController.InvokeUnbindItem(t, t.index);
							}
							bool flag7 = flag2 || this.alwaysRebindOnRefresh;
							if (flag7)
							{
								this.Setup(t, num);
							}
						}
					}
					else
					{
						this.ReleaseItem(i--);
					}
				}
			}
			if (rebuild)
			{
				this.m_Pool.Clear();
				this.m_ActiveItems.Clear();
				this.m_ScrollView.Clear();
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000158F8 File Offset: 0x00013AF8
		public override void UnbindAll()
		{
			bool flag = this.m_CollectionView.HasValidDataAndBindings();
			bool flag2 = !flag;
			if (!flag2)
			{
				foreach (T t in this.m_ActiveItems)
				{
					this.m_CollectionView.viewController.InvokeUnbindItem(t, t.index);
				}
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00015984 File Offset: 0x00013B84
		protected void Setup(T recycledItem, int newIndex)
		{
			bool isDragGhost = recycledItem.isDragGhost;
			bool flag = this.GetDraggedIndex() == newIndex;
			if (flag)
			{
				bool flag2 = recycledItem.index != -1;
				if (flag2)
				{
					this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
				}
				recycledItem.SetDragGhost(true);
				recycledItem.index = this.m_DraggedItem.index;
				recycledItem.rootElement.style.display = DisplayStyle.Flex;
				this.m_CollectionView.viewController.SetBindingContext(recycledItem, recycledItem.index);
			}
			else
			{
				bool flag3 = isDragGhost;
				if (flag3)
				{
					recycledItem.SetDragGhost(false);
				}
				bool flag4 = newIndex >= this.itemsCount;
				if (flag4)
				{
					recycledItem.rootElement.style.display = DisplayStyle.None;
					bool flag5 = recycledItem.index >= 0 && recycledItem.index < this.itemsCount;
					if (flag5)
					{
						this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
						recycledItem.index = -1;
					}
				}
				else
				{
					recycledItem.rootElement.style.display = DisplayStyle.Flex;
					int idForIndex = this.m_CollectionView.viewController.GetIdForIndex(newIndex);
					bool flag6 = recycledItem.index == newIndex && recycledItem.id == idForIndex;
					if (!flag6)
					{
						bool flag7 = this.m_CollectionView.showAlternatingRowBackgrounds != AlternatingRowBackground.None && newIndex % 2 == 1;
						recycledItem.rootElement.EnableInClassList(BaseVerticalCollectionView.itemAlternativeBackgroundUssClassName, flag7);
						int index = recycledItem.index;
						bool flag8 = recycledItem.index != -1;
						if (flag8)
						{
							this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
						}
						recycledItem.index = newIndex;
						recycledItem.id = idForIndex;
						int num = newIndex - this.firstVisibleIndex;
						bool flag9 = num >= this.m_ScrollView.contentContainer.childCount;
						if (flag9)
						{
							recycledItem.rootElement.BringToFront();
						}
						else
						{
							bool flag10 = num >= 0;
							if (flag10)
							{
								recycledItem.rootElement.PlaceBehind(this.m_ScrollView.contentContainer[num]);
							}
							else
							{
								recycledItem.rootElement.SendToBack();
							}
						}
						this.m_CollectionView.viewController.InvokeBindItem(recycledItem, newIndex);
						this.HandleFocus(recycledItem, index);
					}
				}
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00015C8C File Offset: 0x00013E8C
		protected bool ShouldDeferScrollToItem(int index)
		{
			bool isDirty = this.m_ScrollView.contentContainer.layoutNode.IsDirty;
			bool flag;
			if (isDirty)
			{
				this.m_DeferredScrollToItemIndex = new int?(index);
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015CCC File Offset: 0x00013ECC
		protected void ScheduleDeferredScrollToItem()
		{
			bool flag = this.m_DeferredScrollToItemIndex == null;
			if (!flag)
			{
				bool flag2 = this.m_ScheduleDeferredScrollToItem == null;
				if (flag2)
				{
					this.m_ScheduleDeferredScrollToItem = this.m_CollectionView.schedule.Execute(this.m_PerformDeferredScrollToItem);
				}
				else
				{
					this.m_ScheduleDeferredScrollToItem.Pause();
					this.m_ScheduleDeferredScrollToItem.Resume();
				}
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00015D34 File Offset: 0x00013F34
		private void PerformDeferredScrollToItem()
		{
			bool flag = this.m_DeferredScrollToItemIndex != null;
			if (flag)
			{
				int value = this.m_DeferredScrollToItemIndex.Value;
				this.m_DeferredScrollToItemIndex = null;
				this.ScrollToItem(value);
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00015D74 File Offset: 0x00013F74
		private void OnDetachFromPanelEvent(DetachFromPanelEvent evt)
		{
			IVisualElementScheduledItem scrollScheduledItem = this.m_ScrollScheduledItem;
			bool flag = scrollScheduledItem != null && scrollScheduledItem.isActive;
			if (flag)
			{
				this.m_ScrollScheduledItem.Pause();
				this.m_ScrollScheduledItem = null;
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00015DB0 File Offset: 0x00013FB0
		public override void OnFocusIn(VisualElement leafTarget)
		{
			bool flag = leafTarget == this.m_ScrollView.contentContainer;
			if (!flag)
			{
				this.m_LastFocusedElementTreeChildIndexes.Clear();
				bool flag2 = this.m_ScrollView.contentContainer.FindElementInTree(leafTarget, this.m_LastFocusedElementTreeChildIndexes);
				if (flag2)
				{
					VisualElement visualElement = this.m_ScrollView.contentContainer[this.m_LastFocusedElementTreeChildIndexes[0]];
					foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
					{
						bool flag3 = reusableCollectionItem.rootElement == visualElement;
						if (flag3)
						{
							this.m_LastFocusedElementIndex = reusableCollectionItem.index;
							break;
						}
					}
					this.m_LastFocusedElementTreeChildIndexes.RemoveAt(0);
				}
				else
				{
					this.m_LastFocusedElementIndex = -1;
				}
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00015E94 File Offset: 0x00014094
		public override void OnFocusOut(VisualElement willFocus)
		{
			bool flag = willFocus == null || willFocus != this.m_ScrollView.contentContainer;
			if (flag)
			{
				this.m_LastFocusedElementTreeChildIndexes.Clear();
				this.m_LastFocusedElementIndex = -1;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00015ED4 File Offset: 0x000140D4
		private void HandleFocus(ReusableCollectionItem recycledItem, int previousIndex)
		{
			bool flag = this.m_LastFocusedElementIndex == -1;
			if (!flag)
			{
				bool flag2 = this.m_LastFocusedElementIndex == recycledItem.index;
				if (flag2)
				{
					VisualElement visualElement = recycledItem.rootElement.ElementAtTreePath(this.m_LastFocusedElementTreeChildIndexes);
					if (visualElement != null)
					{
						visualElement.Focus();
					}
				}
				else
				{
					bool flag3 = this.m_LastFocusedElementIndex != previousIndex;
					if (flag3)
					{
						VisualElement visualElement2 = recycledItem.rootElement.ElementAtTreePath(this.m_LastFocusedElementTreeChildIndexes);
						if (visualElement2 != null)
						{
							visualElement2.Blur();
						}
					}
					else
					{
						this.m_ScrollView.contentContainer.Focus();
					}
				}
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00015F64 File Offset: 0x00014164
		public override void UpdateBackground()
		{
			float num;
			bool flag = this.m_CollectionView.showAlternatingRowBackgrounds != AlternatingRowBackground.All || (num = this.m_ScrollView.contentViewport.resolvedStyle.height - this.GetExpectedContentHeight()) <= 0f;
			if (flag)
			{
				VisualElement emptyRows = this.m_EmptyRows;
				if (emptyRows != null)
				{
					emptyRows.RemoveFromHierarchy();
				}
			}
			else
			{
				bool flag2 = this.lastVisibleItem == null;
				if (!flag2)
				{
					bool flag3 = this.m_EmptyRows == null;
					if (flag3)
					{
						this.m_EmptyRows = new VisualElement
						{
							classList = { BaseVerticalCollectionView.backgroundFillUssClassName }
						};
					}
					bool flag4 = this.m_EmptyRows.parent == null;
					if (flag4)
					{
						this.m_ScrollView.contentViewport.Add(this.m_EmptyRows);
					}
					float expectedItemHeight = this.GetExpectedItemHeight(-1);
					int num2 = Mathf.FloorToInt(num / expectedItemHeight) + 1;
					bool flag5 = num2 > this.m_EmptyRows.childCount;
					if (flag5)
					{
						int num3 = num2 - this.m_EmptyRows.childCount;
						for (int i = 0; i < num3; i++)
						{
							VisualElement visualElement = new VisualElement();
							visualElement.style.flexShrink = 0f;
							this.m_EmptyRows.Add(visualElement);
						}
					}
					T t = this.lastVisibleItem;
					int num4 = ((t != null) ? t.index : (-1));
					int childCount = this.m_EmptyRows.hierarchy.childCount;
					for (int j = 0; j < childCount; j++)
					{
						VisualElement visualElement2 = this.m_EmptyRows.hierarchy[j];
						num4++;
						visualElement2.style.height = expectedItemHeight;
						visualElement2.EnableInClassList(BaseVerticalCollectionView.itemAlternativeBackgroundUssClassName, num4 % 2 == 1);
					}
				}
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00016140 File Offset: 0x00014340
		internal override void StartDragItem(ReusableCollectionItem item)
		{
			this.m_DraggedItem = item as T;
			int num = this.m_ActiveItems.IndexOf(this.m_DraggedItem);
			this.m_ActiveItems.RemoveAt(num);
			T orMakeItemAtIndex = this.GetOrMakeItemAtIndex(num, num);
			this.Setup(orMakeItemAtIndex, this.m_DraggedItem.index);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000161A0 File Offset: 0x000143A0
		internal override void EndDrag(int dropIndex)
		{
			ReusableCollectionItem recycledItemFromIndex = this.m_CollectionView.GetRecycledItemFromIndex(dropIndex);
			int num = ((recycledItemFromIndex != null) ? this.m_ScrollView.IndexOf(recycledItemFromIndex.rootElement) : this.m_ActiveItems.Count);
			this.m_ScrollView.Insert(num, this.m_DraggedItem.rootElement);
			this.m_ActiveItems.Insert(num, this.m_DraggedItem);
			for (int i = 0; i < this.m_ActiveItems.Count; i++)
			{
				T t = this.m_ActiveItems[i];
				bool isDragGhost = t.isDragGhost;
				if (isDragGhost)
				{
					t.index = -1;
					this.ReleaseItem(i);
					i--;
				}
			}
			bool flag = Math.Min(dropIndex, this.itemsCount - 1) != this.m_DraggedItem.index;
			if (flag)
			{
				bool flag2 = this.lastVisibleItem != null;
				if (flag2)
				{
					this.lastVisibleItem.rootElement.style.display = DisplayStyle.None;
				}
				bool flag3 = this.m_DraggedItem.index < dropIndex;
				if (flag3)
				{
					this.m_CollectionView.viewController.InvokeUnbindItem(this.m_DraggedItem, this.m_DraggedItem.index);
					this.m_DraggedItem.index = -1;
				}
				else
				{
					bool flag4 = recycledItemFromIndex != null;
					if (flag4)
					{
						this.m_CollectionView.viewController.InvokeUnbindItem(recycledItemFromIndex, recycledItemFromIndex.index);
						recycledItemFromIndex.index = -1;
					}
				}
			}
			this.m_DraggedItem = default(T);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001635C File Offset: 0x0001455C
		internal virtual T GetOrMakeItemAtIndex(int activeItemIndex = -1, int scrollViewIndex = -1)
		{
			T t = this.m_Pool.Get();
			bool flag = t.rootElement == null;
			if (flag)
			{
				this.m_CollectionView.viewController.InvokeMakeItem(t);
				t.onDestroy += this.OnDestroyItem;
			}
			t.PreAttachElement();
			bool flag2 = activeItemIndex == -1;
			if (flag2)
			{
				this.m_ActiveItems.Add(t);
			}
			else
			{
				this.m_ActiveItems.Insert(activeItemIndex, t);
			}
			bool flag3 = scrollViewIndex == -1;
			if (flag3)
			{
				this.m_ScrollView.Add(t.rootElement);
			}
			else
			{
				this.m_ScrollView.Insert(scrollViewIndex, t.rootElement);
			}
			return t;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00016434 File Offset: 0x00014634
		internal virtual void ReleaseItem(int activeItemsIndex)
		{
			T t = this.m_ActiveItems[activeItemsIndex];
			bool flag = t.index != -1;
			if (flag)
			{
				this.m_CollectionView.viewController.InvokeUnbindItem(t, t.index);
			}
			this.m_Pool.Release(t);
			this.m_ActiveItems.Remove(t);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000164A2 File Offset: 0x000146A2
		private void OnDestroyItem(ReusableCollectionItem item)
		{
			this.m_CollectionView.viewController.InvokeDestroyItem(item);
			item.onDestroy -= this.OnDestroyItem;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00002152 File Offset: 0x00000352
		protected virtual void OnScrollUpdate()
		{
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000164CC File Offset: 0x000146CC
		protected int GetDraggedIndex()
		{
			ListViewDraggerAnimated listViewDraggerAnimated = this.m_CollectionView.dragger as ListViewDraggerAnimated;
			bool flag = listViewDraggerAnimated != null && listViewDraggerAnimated.isDragging;
			int num;
			if (flag)
			{
				num = listViewDraggerAnimated.draggedItem.index;
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00016510 File Offset: 0x00014710
		protected void ScheduleScroll()
		{
			bool flag = this.m_ScrollScheduledItem == null;
			if (flag)
			{
				this.m_ScrollScheduledItem = this.m_CollectionView.schedule.Execute(this.m_ScrollCallback);
			}
			else
			{
				this.m_ScrollScheduledItem.Pause();
				this.m_ScrollScheduledItem.Resume();
			}
		}

		// Token: 0x0400023F RID: 575
		private readonly ObjectPool<T> m_Pool = new ObjectPool<T>(() => new T(), null, delegate(T i)
		{
			i.DetachElement();
		}, delegate(T i)
		{
			i.DestroyElement();
		}, true, 10, 10000);

		// Token: 0x04000240 RID: 576
		protected BaseVerticalCollectionView m_CollectionView;

		// Token: 0x04000241 RID: 577
		protected const int k_ExtraVisibleItems = 2;

		// Token: 0x04000242 RID: 578
		protected List<T> m_ActiveItems;

		// Token: 0x04000243 RID: 579
		protected T m_DraggedItem;

		// Token: 0x04000244 RID: 580
		private int? m_DeferredScrollToItemIndex;

		// Token: 0x04000245 RID: 581
		private readonly Action m_PerformDeferredScrollToItem;

		// Token: 0x04000246 RID: 582
		private IVisualElementScheduledItem m_ScheduleDeferredScrollToItem;

		// Token: 0x04000247 RID: 583
		private IVisualElementScheduledItem m_ScrollScheduledItem;

		// Token: 0x04000248 RID: 584
		private Action m_ScrollCallback;

		// Token: 0x04000249 RID: 585
		private int m_LastFocusedElementIndex = -1;

		// Token: 0x0400024A RID: 586
		private List<int> m_LastFocusedElementTreeChildIndexes = new List<int>();

		// Token: 0x0400024B RID: 587
		protected readonly Func<T, bool> m_VisibleItemPredicateDelegate;

		// Token: 0x0400024C RID: 588
		protected List<T> m_ScrollInsertionList = new List<T>();

		// Token: 0x0400024D RID: 589
		private VisualElement m_EmptyRows;
	}
}
