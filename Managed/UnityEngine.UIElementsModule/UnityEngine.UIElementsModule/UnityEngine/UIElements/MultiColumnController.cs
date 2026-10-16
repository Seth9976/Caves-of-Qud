using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Pool;
using UnityEngine.UIElements.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000153 RID: 339
	public class MultiColumnController : IDisposable
	{
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000AF1 RID: 2801 RVA: 0x00030708 File Offset: 0x0002E908
		// (remove) Token: 0x06000AF2 RID: 2802 RVA: 0x00030740 File Offset: 0x0002E940
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action columnSortingChanged;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000AF3 RID: 2803 RVA: 0x00030778 File Offset: 0x0002E978
		// (remove) Token: 0x06000AF4 RID: 2804 RVA: 0x000307B0 File Offset: 0x0002E9B0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ContextualMenuPopulateEvent, Column> headerContextMenuPopulateEvent;

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x000307E5 File Offset: 0x0002E9E5
		internal MultiColumnCollectionHeader header
		{
			get
			{
				return this.m_MultiColumnHeader;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000307ED File Offset: 0x0002E9ED
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x000307F5 File Offset: 0x0002E9F5
		internal ColumnSortingMode sortingMode
		{
			get
			{
				return this.m_SortingMode;
			}
			set
			{
				this.m_SortingMode = value;
				this.header.sortingEnabled = this.m_SortingMode > ColumnSortingMode.None;
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00030814 File Offset: 0x0002EA14
		public MultiColumnController(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
		{
			this.m_MultiColumnHeader = new MultiColumnCollectionHeader(columns, sortDescriptions, sortedColumns)
			{
				viewDataKey = MultiColumnController.k_HeaderViewDataKey
			};
			this.m_MultiColumnHeader.columnSortingChanged += this.OnColumnSortingChanged;
			this.m_MultiColumnHeader.contextMenuPopulateEvent += this.OnContextMenuPopulateEvent;
			this.m_MultiColumnHeader.columnResized += this.OnColumnResized;
			this.m_MultiColumnHeader.viewDataRestored += this.OnViewDataRestored;
			this.m_MultiColumnHeader.columns.columnAdded += this.OnColumnAdded;
			this.m_MultiColumnHeader.columns.columnRemoved += this.OnColumnRemoved;
			this.m_MultiColumnHeader.columns.columnReordered += this.OnColumnReordered;
			this.m_MultiColumnHeader.columns.columnChanged += this.OnColumnsChanged;
			this.m_MultiColumnHeader.columns.changed += this.OnColumnChanged;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00030934 File Offset: 0x0002EB34
		private static void BindCellItem<T>(VisualElement ve, int rowIndex, Column column, T item)
		{
			bool flag = column.bindCell != null;
			if (flag)
			{
				column.bindCell(ve, rowIndex);
			}
			else
			{
				MultiColumnController.DefaultBindCellItem<T>(ve, item);
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0003096B File Offset: 0x0002EB6B
		private static void UnbindCellItem(VisualElement ve, int rowIndex, Column column)
		{
			Action<VisualElement, int> unbindCell = column.unbindCell;
			if (unbindCell != null)
			{
				unbindCell(ve, rowIndex);
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00030984 File Offset: 0x0002EB84
		private static VisualElement DefaultMakeCellItem()
		{
			Label label = new Label();
			label.AddToClassList(MultiColumnController.cellLabelUssClassName);
			return label;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000309AC File Offset: 0x0002EBAC
		private static void DefaultBindCellItem<T>(VisualElement ve, T item)
		{
			Label label = ve as Label;
			bool flag = label != null;
			if (flag)
			{
				label.text = item.ToString();
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000309E0 File Offset: 0x0002EBE0
		public VisualElement MakeItem()
		{
			VisualElement visualElement = new VisualElement
			{
				name = MultiColumnController.rowContainerUssClassName
			};
			visualElement.AddToClassList(MultiColumnController.rowContainerUssClassName);
			foreach (Column column in this.m_MultiColumnHeader.columns.visibleList)
			{
				VisualElement visualElement2 = new VisualElement();
				visualElement2.AddToClassList(MultiColumnController.cellUssClassName);
				Func<VisualElement> makeCell = column.makeCell;
				VisualElement visualElement3 = ((makeCell != null) ? makeCell() : null) ?? MultiColumnController.DefaultMakeCellItem();
				visualElement2.SetProperty(MultiColumnController.bindableElementPropertyName, visualElement3);
				visualElement2.Add(visualElement3);
				visualElement.Add(visualElement2);
			}
			return visualElement;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00030AA8 File Offset: 0x0002ECA8
		public void BindItem<T>(VisualElement element, int index, T item)
		{
			int num = 0;
			foreach (Column column in this.m_MultiColumnHeader.columns.visibleList)
			{
				MultiColumnCollectionHeader.ColumnData columnData;
				bool flag = !this.m_MultiColumnHeader.columnDataMap.TryGetValue(column, out columnData);
				if (!flag)
				{
					VisualElement visualElement = element[num++];
					VisualElement visualElement2 = visualElement.GetProperty(MultiColumnController.bindableElementPropertyName) as VisualElement;
					MultiColumnController.BindCellItem<T>(visualElement2, index, column, item);
					visualElement.style.width = columnData.control.resolvedStyle.width;
					visualElement.SetProperty(MultiColumnController.k_BoundColumnVePropertyName, column);
				}
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00030B80 File Offset: 0x0002ED80
		public void UnbindItem(VisualElement element, int index)
		{
			index = this.GetSourceIndex(index);
			foreach (VisualElement visualElement in element.Children())
			{
				Column column = visualElement.GetProperty(MultiColumnController.k_BoundColumnVePropertyName) as Column;
				bool flag = column == null;
				if (!flag)
				{
					VisualElement visualElement2 = visualElement.GetProperty(MultiColumnController.bindableElementPropertyName) as VisualElement;
					MultiColumnController.UnbindCellItem(visualElement2, index, column);
				}
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00030C0C File Offset: 0x0002EE0C
		public void DestroyItem(VisualElement element)
		{
			foreach (VisualElement visualElement in element.Children())
			{
				Column column = visualElement.GetProperty(MultiColumnController.k_BoundColumnVePropertyName) as Column;
				bool flag = column == null;
				if (!flag)
				{
					VisualElement visualElement2 = visualElement.GetProperty(MultiColumnController.bindableElementPropertyName) as VisualElement;
					Action<VisualElement> destroyCell = column.destroyCell;
					if (destroyCell != null)
					{
						destroyCell(visualElement2);
					}
					visualElement.ClearProperty(MultiColumnController.k_BoundColumnVePropertyName);
				}
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00030CA4 File Offset: 0x0002EEA4
		public void PrepareView(BaseVerticalCollectionView collectionView)
		{
			bool flag = this.m_View != null;
			if (flag)
			{
				Debug.LogWarning("Trying to initialize multi column view more than once. This shouldn't happen.");
			}
			else
			{
				this.m_View = collectionView;
				this.m_HeaderContainer = new VisualElement
				{
					name = MultiColumnController.headerContainerUssClassName
				};
				this.m_HeaderContainer.AddToClassList(MultiColumnController.headerContainerUssClassName);
				this.m_HeaderContainer.viewDataKey = MultiColumnController.k_HeaderContainerViewDataKey;
				collectionView.scrollView.hierarchy.Insert(0, this.m_HeaderContainer);
				this.m_HeaderContainer.Add(this.m_MultiColumnHeader);
				this.m_View.scrollView.horizontalScroller.valueChanged += this.OnHorizontalScrollerValueChanged;
				this.m_View.scrollView.contentViewport.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnViewportGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_MultiColumnHeader.columnContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnColumnContainerGeometryChanged), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00030DA0 File Offset: 0x0002EFA0
		public void Dispose()
		{
			bool flag = this.m_View != null;
			if (flag)
			{
				this.m_View.scrollView.horizontalScroller.valueChanged -= this.OnHorizontalScrollerValueChanged;
				this.m_View.scrollView.contentViewport.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnViewportGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_View = null;
			}
			this.m_MultiColumnHeader.columnContainer.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnColumnContainerGeometryChanged), TrickleDown.NoTrickleDown);
			this.m_MultiColumnHeader.columnSortingChanged -= this.OnColumnSortingChanged;
			this.m_MultiColumnHeader.contextMenuPopulateEvent -= this.OnContextMenuPopulateEvent;
			this.m_MultiColumnHeader.columnResized -= this.OnColumnResized;
			this.m_MultiColumnHeader.viewDataRestored -= this.OnViewDataRestored;
			this.m_MultiColumnHeader.columns.columnAdded -= this.OnColumnAdded;
			this.m_MultiColumnHeader.columns.columnRemoved -= this.OnColumnRemoved;
			this.m_MultiColumnHeader.columns.columnReordered -= this.OnColumnReordered;
			this.m_MultiColumnHeader.columns.columnChanged -= this.OnColumnsChanged;
			this.m_MultiColumnHeader.columns.changed -= this.OnColumnChanged;
			this.m_MultiColumnHeader.RemoveFromHierarchy();
			this.m_MultiColumnHeader.Dispose();
			this.m_MultiColumnHeader = null;
			this.m_HeaderContainer.RemoveFromHierarchy();
			this.m_HeaderContainer = null;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00030F4A File Offset: 0x0002F14A
		private void OnHorizontalScrollerValueChanged(float v)
		{
			this.m_MultiColumnHeader.ScrollHorizontally(v);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00030F5C File Offset: 0x0002F15C
		private void OnViewportGeometryChanged(GeometryChangedEvent evt)
		{
			float num = this.m_MultiColumnHeader.resolvedStyle.paddingLeft + this.m_MultiColumnHeader.resolvedStyle.paddingRight;
			this.m_MultiColumnHeader.style.maxWidth = evt.newRect.width - num;
			this.m_MultiColumnHeader.style.maxWidth = evt.newRect.width - num;
			this.UpdateContentContainer(this.m_View);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00030FE5 File Offset: 0x0002F1E5
		private void OnColumnContainerGeometryChanged(GeometryChangedEvent evt)
		{
			this.UpdateContentContainer(this.m_View);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00030FF8 File Offset: 0x0002F1F8
		private void UpdateContentContainer(BaseVerticalCollectionView collectionView)
		{
			float width = this.m_MultiColumnHeader.columnContainer.layout.width;
			float num = Mathf.Max(width, collectionView.scrollView.contentViewport.resolvedStyle.width);
			collectionView.scrollView.contentContainer.style.width = num;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00031058 File Offset: 0x0002F258
		private void OnColumnSortingChanged()
		{
			this.UpdateDragger();
			bool flag = this.sortingMode == ColumnSortingMode.Default;
			if (flag)
			{
				this.m_View.RefreshItems();
			}
			Action action = this.columnSortingChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0003109C File Offset: 0x0002F29C
		internal void UpdateDragger()
		{
			bool flag = this.sortingMode == ColumnSortingMode.None;
			if (flag)
			{
				this.m_View.dragger.enabled = true;
			}
			else
			{
				this.m_View.dragger.enabled = this.header.sortedColumnReadonly.Count == 0;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000310F0 File Offset: 0x0002F2F0
		internal void SortIfNeeded()
		{
			this.UpdateDragger();
			bool flag = this.sortingMode == ColumnSortingMode.None || this.sortingMode != ColumnSortingMode.Default || this.m_View.itemsSource == null;
			if (!flag)
			{
				this.m_View.virtualizationController.UnbindAll();
				List<int> sortedToSourceIndex = this.m_SortedToSourceIndex;
				if (sortedToSourceIndex != null)
				{
					sortedToSourceIndex.Clear();
				}
				List<int> sourceToSortedIndex = this.m_SourceToSortedIndex;
				if (sourceToSortedIndex != null)
				{
					sourceToSortedIndex.Clear();
				}
				bool flag2 = this.header.sortedColumnReadonly.Count == 0;
				if (!flag2)
				{
					int count = this.m_View.itemsSource.Count;
					if (this.m_SortedToSourceIndex == null)
					{
						this.m_SortedToSourceIndex = new List<int>(count);
					}
					if (this.m_SourceToSortedIndex == null)
					{
						this.m_SourceToSortedIndex = new List<int>(count);
					}
					BaseTreeViewController baseTreeViewController = this.m_View.viewController as BaseTreeViewController;
					bool flag3 = baseTreeViewController != null;
					if (flag3)
					{
						List<int> list;
						using (CollectionPool<List<int>, int>.Get(out list))
						{
							Dictionary<int, int> dictionary;
							using (CollectionPool<Dictionary<int, int>, KeyValuePair<int, int>>.Get(out dictionary))
							{
								for (int i = 0; i < count; i++)
								{
									int idForIndex = baseTreeViewController.GetIdForIndex(i);
									dictionary[idForIndex] = i;
									list.Add(idForIndex);
								}
								list.Sort(new Comparison<int>(this.CombinedComparisonById));
								for (int j = 0; j < list.Count; j++)
								{
									this.m_SortedToSourceIndex.Add(dictionary[list[j]]);
								}
							}
						}
					}
					else
					{
						for (int k = 0; k < count; k++)
						{
							this.m_SortedToSourceIndex.Add(k);
						}
						this.m_SortedToSourceIndex.Sort(new Comparison<int>(this.CombinedComparisonByIndex));
					}
					for (int l = 0; l < count; l++)
					{
						this.m_SourceToSortedIndex.Add(-1);
					}
					for (int m = 0; m < this.m_SortedToSourceIndex.Count; m++)
					{
						this.m_SourceToSortedIndex[this.m_SortedToSourceIndex[m]] = m;
					}
				}
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00031358 File Offset: 0x0002F558
		private int CombinedComparisonById(int idA, int idB)
		{
			BaseTreeViewController baseTreeViewController = this.m_View.viewController as BaseTreeViewController;
			int num = baseTreeViewController.GetParentId(idA);
			int num2 = baseTreeViewController.GetParentId(idB);
			bool flag = num != num2;
			if (flag)
			{
				int i = baseTreeViewController.GetIndentationDepth(idA);
				int j = baseTreeViewController.GetIndentationDepth(idB);
				int num3 = i;
				int num4 = j;
				while (i > j)
				{
					i--;
					idA = num;
					num = baseTreeViewController.GetParentId(num);
				}
				while (j > i)
				{
					j--;
					idB = num2;
					num2 = baseTreeViewController.GetParentId(num2);
				}
				while (num != num2)
				{
					idA = num;
					idB = num2;
					num = baseTreeViewController.GetParentId(num);
					num2 = baseTreeViewController.GetParentId(num2);
				}
				bool flag2 = idA == idB;
				if (flag2)
				{
					return num3.CompareTo(num4);
				}
			}
			int indexForId = baseTreeViewController.GetIndexForId(idA);
			int indexForId2 = baseTreeViewController.GetIndexForId(idB);
			int num5 = 0;
			foreach (SortColumnDescription sortColumnDescription in this.header.sortedColumns)
			{
				Comparison<int> comparison = sortColumnDescription.column.comparison;
				num5 = ((comparison != null) ? comparison(indexForId, indexForId2) : 0);
				bool flag3 = num5 != 0;
				if (flag3)
				{
					bool flag4 = sortColumnDescription.direction == SortDirection.Descending;
					if (flag4)
					{
						num5 = -num5;
					}
					break;
				}
			}
			return (num5 == 0) ? idA.CompareTo(idB) : num5;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000314EC File Offset: 0x0002F6EC
		private int CombinedComparisonByIndex(int a, int b)
		{
			int num = 0;
			foreach (SortColumnDescription sortColumnDescription in this.header.sortedColumns)
			{
				Comparison<int> comparison = sortColumnDescription.column.comparison;
				num = ((comparison != null) ? comparison(a, b) : 0);
				bool flag = num != 0;
				if (flag)
				{
					bool flag2 = sortColumnDescription.direction == SortDirection.Descending;
					if (flag2)
					{
						num = -num;
					}
					break;
				}
			}
			return (num == 0) ? a.CompareTo(b) : num;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0003158C File Offset: 0x0002F78C
		internal int GetSourceIndex(int sortedIndex)
		{
			return MultiColumnController.GetIndexFromList(sortedIndex, this.m_SortedToSourceIndex);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0003159A File Offset: 0x0002F79A
		internal int GetSortedIndex(int sourceIndex)
		{
			return MultiColumnController.GetIndexFromList(sourceIndex, this.m_SourceToSortedIndex);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x000315A8 File Offset: 0x0002F7A8
		private static int GetIndexFromList(int index, List<int> indices)
		{
			bool flag = indices == null;
			int num;
			if (flag)
			{
				num = index;
			}
			else
			{
				bool flag2 = index < 0 || index >= indices.Count;
				if (flag2)
				{
					num = index;
				}
				else
				{
					num = ((indices.Count > 0) ? indices[index] : index);
				}
			}
			return num;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x000315F4 File Offset: 0x0002F7F4
		private void OnContextMenuPopulateEvent(ContextualMenuPopulateEvent evt, Column column)
		{
			Action<ContextualMenuPopulateEvent, Column> action = this.headerContextMenuPopulateEvent;
			if (action != null)
			{
				action(evt, column);
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0003160C File Offset: 0x0002F80C
		private void OnColumnResized(int index, float width)
		{
			bool isRebuildScheduled = this.m_View.isRebuildScheduled;
			if (!isRebuildScheduled)
			{
				foreach (ReusableCollectionItem reusableCollectionItem in this.m_View.activeItems)
				{
					reusableCollectionItem.bindableElement.ElementAt(index).style.width = width;
				}
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0003168C File Offset: 0x0002F88C
		private void OnColumnAdded(Column column, int index)
		{
			this.m_View.Rebuild();
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0003168C File Offset: 0x0002F88C
		private void OnColumnRemoved(Column column)
		{
			this.m_View.Rebuild();
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0003169C File Offset: 0x0002F89C
		private void OnColumnReordered(Column column, int from, int to)
		{
			bool isApplyingViewState = this.m_MultiColumnHeader.isApplyingViewState;
			if (!isApplyingViewState)
			{
				this.m_View.Rebuild();
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000316C8 File Offset: 0x0002F8C8
		private void OnColumnsChanged(Column column, ColumnDataType type)
		{
			bool isApplyingViewState = this.m_MultiColumnHeader.isApplyingViewState;
			if (!isApplyingViewState)
			{
				bool flag = type == ColumnDataType.Visibility;
				if (flag)
				{
					this.m_View.ScheduleRebuild();
				}
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000316FC File Offset: 0x0002F8FC
		private void OnColumnChanged(ColumnsDataType type)
		{
			bool isApplyingViewState = this.m_MultiColumnHeader.isApplyingViewState;
			if (!isApplyingViewState)
			{
				bool flag = type == ColumnsDataType.PrimaryColumn;
				if (flag)
				{
					this.m_View.ScheduleRebuild();
				}
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0003168C File Offset: 0x0002F88C
		private void OnViewDataRestored()
		{
			this.m_View.Rebuild();
		}

		// Token: 0x04000678 RID: 1656
		private static readonly PropertyName k_BoundColumnVePropertyName = "__unity-multi-column-bound-column";

		// Token: 0x04000679 RID: 1657
		internal static readonly PropertyName bindableElementPropertyName = "__unity-multi-column-bindable-element";

		// Token: 0x0400067A RID: 1658
		internal static readonly string baseUssClassName = "unity-multi-column-view";

		// Token: 0x0400067B RID: 1659
		private static readonly string k_HeaderContainerViewDataKey = "unity-multi-column-header-container";

		// Token: 0x0400067C RID: 1660
		public static readonly string headerContainerUssClassName = MultiColumnController.baseUssClassName + "__header-container";

		// Token: 0x0400067D RID: 1661
		public static readonly string rowContainerUssClassName = MultiColumnController.baseUssClassName + "__row-container";

		// Token: 0x0400067E RID: 1662
		public static readonly string cellUssClassName = MultiColumnController.baseUssClassName + "__cell";

		// Token: 0x0400067F RID: 1663
		public static readonly string cellLabelUssClassName = MultiColumnController.cellUssClassName + "__label";

		// Token: 0x04000680 RID: 1664
		private static readonly string k_HeaderViewDataKey = "Header";

		// Token: 0x04000683 RID: 1667
		private List<int> m_SortedToSourceIndex;

		// Token: 0x04000684 RID: 1668
		private List<int> m_SourceToSortedIndex;

		// Token: 0x04000685 RID: 1669
		private ColumnSortingMode m_SortingMode;

		// Token: 0x04000686 RID: 1670
		private BaseVerticalCollectionView m_View;

		// Token: 0x04000687 RID: 1671
		private VisualElement m_HeaderContainer;

		// Token: 0x04000688 RID: 1672
		private MultiColumnCollectionHeader m_MultiColumnHeader;
	}
}
