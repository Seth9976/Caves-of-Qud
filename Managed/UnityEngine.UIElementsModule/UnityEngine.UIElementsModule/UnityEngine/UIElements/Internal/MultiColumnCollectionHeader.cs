using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Pool;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x0200072C RID: 1836
	internal class MultiColumnCollectionHeader : VisualElement, IDisposable
	{
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x000CF226 File Offset: 0x000CD426
		internal bool isApplyingViewState
		{
			get
			{
				return this.m_ApplyingViewState;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x000CF22E File Offset: 0x000CD42E
		public Dictionary<Column, MultiColumnCollectionHeader.ColumnData> columnDataMap { get; } = new Dictionary<Column, MultiColumnCollectionHeader.ColumnData>();

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000CF236 File Offset: 0x000CD436
		public ColumnLayout columnLayout { get; }

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x000CF23E File Offset: 0x000CD43E
		public VisualElement columnContainer { get; }

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x000CF246 File Offset: 0x000CD446
		public VisualElement resizeHandleContainer { get; }

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x000CF24E File Offset: 0x000CD44E
		public IEnumerable<SortColumnDescription> sortedColumns
		{
			get
			{
				return this.m_SortedColumns;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x0600355D RID: 13661 RVA: 0x000CF24E File Offset: 0x000CD44E
		internal IReadOnlyList<SortColumnDescription> sortedColumnReadonly
		{
			get
			{
				return this.m_SortedColumns;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x000CF256 File Offset: 0x000CD456
		// (set) Token: 0x0600355F RID: 13663 RVA: 0x000CF25E File Offset: 0x000CD45E
		public SortColumnDescriptions sortDescriptions
		{
			get
			{
				return this.m_SortDescriptions;
			}
			protected internal set
			{
				this.m_SortDescriptions = value;
				this.m_SortDescriptions.changed += this.UpdateSortedColumns;
				this.UpdateSortedColumns();
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000CF287 File Offset: 0x000CD487
		public Columns columns { get; }

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000CF28F File Offset: 0x000CD48F
		// (set) Token: 0x06003562 RID: 13666 RVA: 0x000CF298 File Offset: 0x000CD498
		public bool sortingEnabled
		{
			get
			{
				return this.m_SortingEnabled;
			}
			set
			{
				bool flag = this.m_SortingEnabled == value;
				if (!flag)
				{
					this.m_SortingEnabled = value;
					this.UpdateSortingStatus();
					this.UpdateSortedColumns();
				}
			}
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06003563 RID: 13667 RVA: 0x000CF2CC File Offset: 0x000CD4CC
		// (remove) Token: 0x06003564 RID: 13668 RVA: 0x000CF304 File Offset: 0x000CD504
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, float> columnResized;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06003565 RID: 13669 RVA: 0x000CF33C File Offset: 0x000CD53C
		// (remove) Token: 0x06003566 RID: 13670 RVA: 0x000CF374 File Offset: 0x000CD574
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action columnSortingChanged;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06003567 RID: 13671 RVA: 0x000CF3AC File Offset: 0x000CD5AC
		// (remove) Token: 0x06003568 RID: 13672 RVA: 0x000CF3E4 File Offset: 0x000CD5E4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ContextualMenuPopulateEvent, Column> contextMenuPopulateEvent;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06003569 RID: 13673 RVA: 0x000CF41C File Offset: 0x000CD61C
		// (remove) Token: 0x0600356A RID: 13674 RVA: 0x000CF454 File Offset: 0x000CD654
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action viewDataRestored;

		// Token: 0x0600356B RID: 13675 RVA: 0x000CF489 File Offset: 0x000CD689
		public MultiColumnCollectionHeader()
			: this(new Columns(), new SortColumnDescriptions(), new List<SortColumnDescription>())
		{
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000CF4A4 File Offset: 0x000CD6A4
		public MultiColumnCollectionHeader(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
		{
			base.AddToClassList(MultiColumnCollectionHeader.ussClassName);
			this.columns = columns;
			this.m_SortedColumns = sortedColumns;
			this.sortDescriptions = sortDescriptions;
			this.columnContainer = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.columnContainer.AddToClassList(MultiColumnCollectionHeader.columnContainerUssClassName);
			base.Add(this.columnContainer);
			this.resizeHandleContainer = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.resizeHandleContainer.AddToClassList(MultiColumnCollectionHeader.handleContainerUssClassName);
			this.resizeHandleContainer.StretchToParentSize();
			base.Add(this.resizeHandleContainer);
			this.columnLayout = new ColumnLayout(columns);
			this.columnLayout.layoutRequested += this.ScheduleDoLayout;
			foreach (Column column in columns.visibleList)
			{
				this.OnColumnAdded(column);
			}
			this.columns.columnAdded += this.OnColumnAdded;
			this.columns.columnRemoved += this.OnColumnRemoved;
			this.columns.columnChanged += this.OnColumnChanged;
			this.columns.columnReordered += this.OnColumnReordered;
			this.columns.columnResized += this.OnColumnResized;
			this.AddManipulator(new ContextualMenuManipulator(new Action<ContextualMenuPopulateEvent>(this.OnContextualMenuManipulator)));
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000CF66C File Offset: 0x000CD86C
		private void ScheduleDoLayout()
		{
			bool doLayoutScheduled = this.m_DoLayoutScheduled;
			if (!doLayoutScheduled)
			{
				base.schedule.Execute(new Action(this.DoLayout));
				this.m_DoLayoutScheduled = true;
			}
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000CF6A8 File Offset: 0x000CD8A8
		private void ResizeToFit()
		{
			this.columnLayout.ResizeToFit(base.layout.width);
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000CF6D0 File Offset: 0x000CD8D0
		private void UpdateSortedColumns()
		{
			bool sortingUpdatesTemporarilyDisabled = this.m_SortingUpdatesTemporarilyDisabled;
			if (!sortingUpdatesTemporarilyDisabled)
			{
				List<MultiColumnCollectionHeader.SortedColumnState> list;
				using (CollectionPool<List<MultiColumnCollectionHeader.SortedColumnState>, MultiColumnCollectionHeader.SortedColumnState>.Get(out list))
				{
					bool sortingEnabled = this.sortingEnabled;
					if (sortingEnabled)
					{
						foreach (SortColumnDescription sortColumnDescription in this.sortDescriptions)
						{
							Column column = null;
							bool flag = sortColumnDescription.columnIndex != -1;
							if (flag)
							{
								column = this.columns[sortColumnDescription.columnIndex];
							}
							else
							{
								bool flag2 = !string.IsNullOrEmpty(sortColumnDescription.columnName);
								if (flag2)
								{
									column = this.columns[sortColumnDescription.columnName];
								}
							}
							bool flag3 = column != null && column.sortable;
							if (flag3)
							{
								sortColumnDescription.column = column;
								list.Add(new MultiColumnCollectionHeader.SortedColumnState(sortColumnDescription, sortColumnDescription.direction));
							}
							else
							{
								sortColumnDescription.column = null;
							}
						}
					}
					bool flag4 = this.m_OldSortedColumnStates.SequenceEqual<MultiColumnCollectionHeader.SortedColumnState>(list);
					if (flag4)
					{
						return;
					}
					this.m_SortedColumns.Clear();
					foreach (MultiColumnCollectionHeader.SortedColumnState sortedColumnState in list)
					{
						this.m_SortedColumns.Add(sortedColumnState.columnDesc);
					}
					this.m_OldSortedColumnStates.CopyFrom<MultiColumnCollectionHeader.SortedColumnState>(list);
				}
				this.SaveViewState();
				this.RaiseColumnSortingChanged();
			}
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000CF8B4 File Offset: 0x000CDAB4
		private void UpdateColumnControls()
		{
			bool flag = false;
			Column column = null;
			foreach (Column column2 in this.columns.visibleList)
			{
				flag |= column2.stretchable;
				MultiColumnCollectionHeader.ColumnData columnData = null;
				bool flag2 = this.columnDataMap.TryGetValue(column2, out columnData);
				if (flag2)
				{
					columnData.control.style.minWidth = column2.minWidth;
					columnData.control.style.maxWidth = column2.maxWidth;
					columnData.resizeHandle.style.display = ((this.columns.resizable && column2.resizable) ? DisplayStyle.Flex : DisplayStyle.None);
				}
				column = column2;
			}
			bool flag3 = flag;
			if (flag3)
			{
				this.columnContainer.style.flexGrow = 1f;
				MultiColumnCollectionHeader.ColumnData columnData2;
				bool flag4 = this.columns.stretchMode == Columns.StretchMode.GrowAndFill && this.columnDataMap.TryGetValue(column, out columnData2);
				if (flag4)
				{
					columnData2.resizeHandle.style.display = DisplayStyle.None;
				}
			}
			else
			{
				this.columnContainer.style.flexGrow = 0f;
			}
			this.UpdateSortingStatus();
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000CFA28 File Offset: 0x000CDC28
		private void OnColumnAdded(Column column, int index)
		{
			this.OnColumnAdded(column);
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000CFA34 File Offset: 0x000CDC34
		private void OnColumnAdded(Column column)
		{
			bool flag = this.columnDataMap.ContainsKey(column);
			if (!flag)
			{
				bool visible = column.visible;
				if (visible)
				{
					MultiColumnHeaderColumn multiColumnHeaderColumn = new MultiColumnHeaderColumn(column);
					MultiColumnHeaderColumnResizeHandle multiColumnHeaderColumnResizeHandle = new MultiColumnHeaderColumnResizeHandle();
					multiColumnHeaderColumn.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnColumnControlGeometryChanged), TrickleDown.NoTrickleDown);
					multiColumnHeaderColumn.clickable.clickedWithEventInfo += this.OnColumnClicked;
					multiColumnHeaderColumn.mover.activeChanged += this.OnMoveManipulatorActivated;
					multiColumnHeaderColumnResizeHandle.dragArea.AddManipulator(new ColumnResizer(column));
					this.columnDataMap[column] = new MultiColumnCollectionHeader.ColumnData
					{
						control = multiColumnHeaderColumn,
						resizeHandle = multiColumnHeaderColumnResizeHandle
					};
					this.columnContainer.Insert(column.visibleIndex, multiColumnHeaderColumn);
					this.resizeHandleContainer.Insert(column.visibleIndex, multiColumnHeaderColumnResizeHandle);
				}
				this.UpdateColumnControls();
				this.SaveViewState();
			}
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000CFB20 File Offset: 0x000CDD20
		private void OnColumnRemoved(Column column)
		{
			MultiColumnCollectionHeader.ColumnData columnData;
			bool flag = !this.columnDataMap.TryGetValue(column, out columnData);
			if (!flag)
			{
				this.CleanupColumnData(columnData);
				this.columnDataMap.Remove(column);
				this.UpdateColumnControls();
				this.SaveViewState();
			}
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000CFB68 File Offset: 0x000CDD68
		private void OnColumnChanged(Column column, ColumnDataType type)
		{
			bool flag = type == ColumnDataType.Visibility;
			if (flag)
			{
				bool visible = column.visible;
				if (visible)
				{
					this.OnColumnAdded(column);
				}
				else
				{
					this.OnColumnRemoved(column);
				}
				this.ApplyColumnSorting();
			}
			this.UpdateColumnControls();
			bool flag2 = type == ColumnDataType.Visibility;
			if (flag2)
			{
				this.SaveViewState();
			}
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000CFBBC File Offset: 0x000CDDBC
		private void OnColumnReordered(Column column, int from, int to)
		{
			bool flag = !column.visible || from == to;
			if (!flag)
			{
				MultiColumnCollectionHeader.ColumnData columnData;
				bool flag2 = this.columnDataMap.TryGetValue(column, out columnData);
				if (flag2)
				{
					int num = column.visibleIndex;
					bool flag3 = num == this.columns.visibleList.Count<Column>() - 1;
					if (flag3)
					{
						columnData.control.BringToFront();
					}
					else
					{
						bool flag4 = to > from;
						if (flag4)
						{
							num++;
						}
						columnData.control.PlaceBehind(this.columnContainer[num]);
						columnData.resizeHandle.PlaceBehind(this.resizeHandleContainer[num]);
					}
				}
				this.UpdateColumnControls();
				this.SaveViewState();
			}
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000CFC76 File Offset: 0x000CDE76
		private void OnColumnResized(Column column)
		{
			this.SaveViewState();
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000CFC80 File Offset: 0x000CDE80
		private void OnContextualMenuManipulator(ContextualMenuPopulateEvent evt)
		{
			Column column3 = null;
			bool flag = this.columns.visibleList.Count<Column>() > 0;
			foreach (Column column2 in this.columns.visibleList)
			{
				bool flag2 = this.columns.stretchMode == Columns.StretchMode.GrowAndFill && flag && column2.stretchable;
				if (flag2)
				{
					flag = false;
				}
				bool flag3 = column3 == null;
				if (flag3)
				{
					MultiColumnCollectionHeader.ColumnData columnData;
					bool flag4 = this.columnDataMap.TryGetValue(column2, out columnData);
					if (flag4)
					{
						bool flag5 = columnData.control.layout.Contains(evt.localMousePosition);
						if (flag5)
						{
							column3 = column2;
						}
					}
				}
			}
			evt.menu.AppendAction("Resize To Fit", delegate(DropdownMenuAction a)
			{
				this.ResizeToFit();
			}, flag ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
			evt.menu.AppendSeparator(null);
			using (IEnumerator<Column> enumerator2 = this.columns.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Column column = enumerator2.Current;
					string text = column.title;
					bool flag6 = string.IsNullOrEmpty(text);
					if (flag6)
					{
						text = column.name;
					}
					bool flag7 = string.IsNullOrEmpty(text);
					if (flag7)
					{
						text = "Unnamed Column_" + column.index.ToString();
					}
					evt.menu.AppendAction(text, delegate(DropdownMenuAction a)
					{
						column.visible = !column.visible;
					}, delegate(DropdownMenuAction a)
					{
						bool flag8 = !string.IsNullOrEmpty(column.name) && this.columns.primaryColumnName == column.name;
						DropdownMenuAction.Status status;
						if (flag8)
						{
							status = DropdownMenuAction.Status.Disabled;
						}
						else
						{
							bool flag9 = !column.optional;
							if (flag9)
							{
								status = DropdownMenuAction.Status.Disabled;
							}
							else
							{
								bool visible = column.visible;
								if (visible)
								{
									status = DropdownMenuAction.Status.Checked;
								}
								else
								{
									status = DropdownMenuAction.Status.Normal;
								}
							}
						}
						return status;
					}, null);
				}
			}
			Action<ContextualMenuPopulateEvent, Column> action = this.contextMenuPopulateEvent;
			if (action != null)
			{
				action(evt, column3);
			}
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000CFE70 File Offset: 0x000CE070
		private void OnMoveManipulatorActivated(ColumnMover mover)
		{
			this.resizeHandleContainer.style.display = (mover.active ? DisplayStyle.None : DisplayStyle.Flex);
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000CFE98 File Offset: 0x000CE098
		private void OnGeometryChanged(GeometryChangedEvent e)
		{
			bool flag = float.IsNaN(e.newRect.width) || float.IsNaN(e.newRect.height);
			if (!flag)
			{
				this.columnLayout.Dirty();
				bool flag2 = e.layoutPass > 2;
				if (flag2)
				{
					this.ScheduleDoLayout();
				}
				else
				{
					this.DoLayout();
				}
			}
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000CFF04 File Offset: 0x000CE104
		private void DoLayout()
		{
			this.columnLayout.DoLayout(base.layout.width);
			this.m_DoLayoutScheduled = false;
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000CFF34 File Offset: 0x000CE134
		private void OnColumnControlGeometryChanged(GeometryChangedEvent evt)
		{
			MultiColumnHeaderColumn multiColumnHeaderColumn = evt.target as MultiColumnHeaderColumn;
			bool flag = multiColumnHeaderColumn == null;
			if (!flag)
			{
				MultiColumnCollectionHeader.ColumnData columnData = this.columnDataMap[multiColumnHeaderColumn.column];
				columnData.resizeHandle.style.left = multiColumnHeaderColumn.layout.xMax;
				bool flag2 = Math.Abs(evt.newRect.width - evt.oldRect.width) < float.Epsilon;
				if (!flag2)
				{
					this.RaiseColumnResized(this.columnContainer.IndexOf(evt.elementTarget));
				}
			}
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000CFFDC File Offset: 0x000CE1DC
		private void OnColumnClicked(EventBase evt)
		{
			bool flag = !this.sortingEnabled;
			if (!flag)
			{
				MultiColumnHeaderColumn multiColumnHeaderColumn = evt.currentTarget as MultiColumnHeaderColumn;
				bool flag2 = multiColumnHeaderColumn == null || !multiColumnHeaderColumn.column.sortable;
				if (!flag2)
				{
					IPointerEvent pointerEvent = evt as IPointerEvent;
					bool flag3 = pointerEvent != null;
					EventModifiers eventModifiers;
					if (flag3)
					{
						eventModifiers = pointerEvent.modifiers;
					}
					else
					{
						IMouseEvent mouseEvent = evt as IMouseEvent;
						bool flag4 = mouseEvent != null;
						if (!flag4)
						{
							return;
						}
						eventModifiers = mouseEvent.modifiers;
					}
					this.m_SortingUpdatesTemporarilyDisabled = true;
					try
					{
						this.UpdateSortColumnDescriptionsOnClick(multiColumnHeaderColumn.column, eventModifiers);
					}
					finally
					{
						this.m_SortingUpdatesTemporarilyDisabled = false;
					}
					this.UpdateSortedColumns();
				}
			}
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000D0098 File Offset: 0x000CE298
		private void UpdateSortColumnDescriptionsOnClick(Column column, EventModifiers modifiers)
		{
			SortColumnDescription sortColumnDescription = this.sortDescriptions.FirstOrDefault<SortColumnDescription>((SortColumnDescription d) => d.column == column || (!string.IsNullOrEmpty(column.name) && d.columnName == column.name) || d.columnIndex == column.index);
			bool flag = sortColumnDescription != null;
			if (flag)
			{
				bool flag2 = modifiers == EventModifiers.Shift;
				if (flag2)
				{
					this.sortDescriptions.Remove(sortColumnDescription);
					return;
				}
				sortColumnDescription.direction = ((sortColumnDescription.direction == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending);
			}
			else
			{
				sortColumnDescription = (string.IsNullOrEmpty(column.name) ? new SortColumnDescription(column.index, SortDirection.Ascending) : new SortColumnDescription(column.name, SortDirection.Ascending));
			}
			EventModifiers eventModifiers = EventModifiers.Control;
			RuntimePlatform platform = Application.platform;
			bool flag3 = platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer;
			if (flag3)
			{
				eventModifiers = EventModifiers.Command;
			}
			bool flag4 = modifiers != eventModifiers;
			if (flag4)
			{
				this.sortDescriptions.Clear();
			}
			bool flag5 = !this.sortDescriptions.Contains(sortColumnDescription);
			if (flag5)
			{
				this.sortDescriptions.Add(sortColumnDescription);
			}
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000D01A4 File Offset: 0x000CE3A4
		public void ScrollHorizontally(float horizontalOffset)
		{
			base.transform.position = new Vector3(-horizontalOffset, base.transform.position.y, base.transform.position.z);
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000D01DA File Offset: 0x000CE3DA
		private void RaiseColumnResized(int columnIndex)
		{
			Action<int, float> action = this.columnResized;
			if (action != null)
			{
				action(columnIndex, this.columnContainer[columnIndex].resolvedStyle.width);
			}
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000D0208 File Offset: 0x000CE408
		private void RaiseColumnSortingChanged()
		{
			this.ApplyColumnSorting();
			bool flag = !this.m_ApplyingViewState;
			if (flag)
			{
				Action action = this.columnSortingChanged;
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000D023C File Offset: 0x000CE43C
		private void ApplyColumnSorting()
		{
			foreach (Column column in this.columns.visibleList)
			{
				MultiColumnCollectionHeader.ColumnData columnData;
				bool flag = !this.columnDataMap.TryGetValue(column, out columnData);
				if (!flag)
				{
					columnData.control.sortOrderLabel = "";
					columnData.control.RemoveFromClassList(MultiColumnHeaderColumn.sortedAscendingUssClassName);
					columnData.control.RemoveFromClassList(MultiColumnHeaderColumn.sortedDescendingUssClassName);
				}
			}
			List<MultiColumnCollectionHeader.ColumnData> list = new List<MultiColumnCollectionHeader.ColumnData>();
			foreach (SortColumnDescription sortColumnDescription in this.sortedColumns)
			{
				MultiColumnCollectionHeader.ColumnData columnData2;
				bool flag2 = this.columnDataMap.TryGetValue(sortColumnDescription.column, out columnData2);
				if (flag2)
				{
					list.Add(columnData2);
					bool flag3 = sortColumnDescription.direction == SortDirection.Ascending;
					if (flag3)
					{
						columnData2.control.AddToClassList(MultiColumnHeaderColumn.sortedAscendingUssClassName);
					}
					else
					{
						columnData2.control.AddToClassList(MultiColumnHeaderColumn.sortedDescendingUssClassName);
					}
				}
			}
			bool flag4 = list.Count > 1;
			if (flag4)
			{
				for (int i = 0; i < list.Count; i++)
				{
					list[i].control.sortOrderLabel = (i + 1).ToString();
				}
			}
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000D03C8 File Offset: 0x000CE5C8
		private void UpdateSortingStatus()
		{
			bool flag = false;
			foreach (Column column in this.columns.visibleList)
			{
				MultiColumnCollectionHeader.ColumnData columnData;
				bool flag2 = !this.columnDataMap.TryGetValue(column, out columnData);
				if (!flag2)
				{
					bool flag3 = this.sortingEnabled && column.sortable;
					if (flag3)
					{
						flag = true;
					}
				}
			}
			foreach (Column column2 in this.columns.visibleList)
			{
				MultiColumnCollectionHeader.ColumnData columnData2;
				bool flag4 = !this.columnDataMap.TryGetValue(column2, out columnData2);
				if (!flag4)
				{
					bool flag5 = flag;
					if (flag5)
					{
						columnData2.control.AddToClassList(MultiColumnHeaderColumn.sortableUssClassName);
					}
					else
					{
						columnData2.control.RemoveFromClassList(MultiColumnHeaderColumn.sortableUssClassName);
					}
				}
			}
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000D04E0 File Offset: 0x000CE6E0
		internal override void OnViewDataReady()
		{
			try
			{
				this.m_ApplyingViewState = true;
				base.OnViewDataReady();
				string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
				this.m_ViewState = base.GetOrCreateViewData<MultiColumnCollectionHeader.ViewState>(this.m_ViewState, fullHierarchicalViewDataKey);
				this.m_ViewState.Apply(this);
				Action action = this.viewDataRestored;
				if (action != null)
				{
					action();
				}
			}
			finally
			{
				this.m_ApplyingViewState = false;
			}
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000D0554 File Offset: 0x000CE754
		private void SaveViewState()
		{
			bool applyingViewState = this.m_ApplyingViewState;
			if (!applyingViewState)
			{
				MultiColumnCollectionHeader.ViewState viewState = this.m_ViewState;
				if (viewState != null)
				{
					viewState.Save(this);
				}
				base.SaveViewData();
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000D0588 File Offset: 0x000CE788
		private void CleanupColumnData(MultiColumnCollectionHeader.ColumnData data)
		{
			data.control.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnColumnControlGeometryChanged), TrickleDown.NoTrickleDown);
			data.control.clickable.clickedWithEventInfo -= this.OnColumnClicked;
			data.control.mover.activeChanged -= this.OnMoveManipulatorActivated;
			data.control.RemoveFromHierarchy();
			data.control.Dispose();
			data.resizeHandle.RemoveFromHierarchy();
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000D0610 File Offset: 0x000CE810
		public void Dispose()
		{
			this.sortDescriptions.changed -= this.UpdateSortedColumns;
			this.columnLayout.layoutRequested -= this.ScheduleDoLayout;
			this.columns.columnAdded -= this.OnColumnAdded;
			this.columns.columnRemoved -= this.OnColumnRemoved;
			this.columns.columnChanged -= this.OnColumnChanged;
			this.columns.columnReordered -= this.OnColumnReordered;
			this.columns.columnResized -= this.OnColumnResized;
			foreach (MultiColumnCollectionHeader.ColumnData columnData in this.columnDataMap.Values)
			{
				this.CleanupColumnData(columnData);
			}
			this.columnDataMap.Clear();
		}

		// Token: 0x04001B27 RID: 6951
		private const int kMaxStableLayoutPassCount = 2;

		// Token: 0x04001B28 RID: 6952
		public static readonly string ussClassName = "unity-multi-column-header";

		// Token: 0x04001B29 RID: 6953
		public static readonly string columnContainerUssClassName = MultiColumnCollectionHeader.ussClassName + "__column-container";

		// Token: 0x04001B2A RID: 6954
		public static readonly string handleContainerUssClassName = MultiColumnCollectionHeader.ussClassName + "__resize-handle-container";

		// Token: 0x04001B2B RID: 6955
		public static readonly string reorderableUssClassName = MultiColumnCollectionHeader.ussClassName + "__header";

		// Token: 0x04001B2C RID: 6956
		private bool m_SortingEnabled;

		// Token: 0x04001B2D RID: 6957
		private List<SortColumnDescription> m_SortedColumns;

		// Token: 0x04001B2E RID: 6958
		private SortColumnDescriptions m_SortDescriptions;

		// Token: 0x04001B2F RID: 6959
		private List<MultiColumnCollectionHeader.SortedColumnState> m_OldSortedColumnStates = new List<MultiColumnCollectionHeader.SortedColumnState>();

		// Token: 0x04001B30 RID: 6960
		private bool m_SortingUpdatesTemporarilyDisabled;

		// Token: 0x04001B31 RID: 6961
		private MultiColumnCollectionHeader.ViewState m_ViewState;

		// Token: 0x04001B32 RID: 6962
		private bool m_ApplyingViewState;

		// Token: 0x04001B33 RID: 6963
		private bool m_DoLayoutScheduled;

		// Token: 0x0200072D RID: 1837
		[Serializable]
		private class ViewState
		{
			// Token: 0x06003589 RID: 13705 RVA: 0x000D0780 File Offset: 0x000CE980
			internal void Save(MultiColumnCollectionHeader header)
			{
				this.m_SortDescriptions.Clear();
				this.m_OrderedColumnStates.Clear();
				foreach (SortColumnDescription sortColumnDescription in header.sortDescriptions)
				{
					this.m_SortDescriptions.Add(sortColumnDescription);
				}
				foreach (Column column in header.columns.displayList)
				{
					MultiColumnCollectionHeader.ViewState.ColumnState columnState = new MultiColumnCollectionHeader.ViewState.ColumnState
					{
						index = column.index,
						name = column.name,
						actualWidth = column.desiredWidth,
						width = column.width,
						visible = column.visible
					};
					this.m_OrderedColumnStates.Add(columnState);
				}
				this.m_HasPersistedData = true;
			}

			// Token: 0x0600358A RID: 13706 RVA: 0x000D0890 File Offset: 0x000CEA90
			internal void Apply(MultiColumnCollectionHeader header)
			{
				bool flag = !this.m_HasPersistedData;
				if (!flag)
				{
					int num = Math.Min(this.m_OrderedColumnStates.Count, header.columns.Count);
					int num2 = 0;
					int num3 = 0;
					while (num3 < this.m_OrderedColumnStates.Count && num2 < num)
					{
						MultiColumnCollectionHeader.ViewState.ColumnState columnState = this.m_OrderedColumnStates[num3];
						Column column = null;
						bool flag2 = !string.IsNullOrEmpty(columnState.name);
						if (flag2)
						{
							bool flag3 = header.columns.Contains(columnState.name);
							if (flag3)
							{
								column = header.columns[columnState.name];
							}
							goto IL_00E2;
						}
						bool flag4 = columnState.index > header.columns.Count - 1;
						if (!flag4)
						{
							column = header.columns[columnState.index];
							bool flag5 = !string.IsNullOrEmpty(column.name);
							if (flag5)
							{
								column = null;
							}
							goto IL_00E2;
						}
						IL_0135:
						num3++;
						continue;
						IL_00E2:
						bool flag6 = column == null;
						if (flag6)
						{
							goto IL_0135;
						}
						header.columns.ReorderDisplay(column.displayIndex, num2++);
						column.visible = columnState.visible;
						column.width = columnState.width;
						column.desiredWidth = columnState.actualWidth;
						goto IL_0135;
					}
					header.sortDescriptions.Clear();
					foreach (SortColumnDescription sortColumnDescription in this.m_SortDescriptions)
					{
						header.sortDescriptions.Add(sortColumnDescription);
					}
				}
			}

			// Token: 0x04001B3D RID: 6973
			[SerializeField]
			private bool m_HasPersistedData;

			// Token: 0x04001B3E RID: 6974
			[SerializeField]
			private List<SortColumnDescription> m_SortDescriptions = new List<SortColumnDescription>();

			// Token: 0x04001B3F RID: 6975
			[SerializeField]
			private List<MultiColumnCollectionHeader.ViewState.ColumnState> m_OrderedColumnStates = new List<MultiColumnCollectionHeader.ViewState.ColumnState>();

			// Token: 0x0200072E RID: 1838
			[Serializable]
			private struct ColumnState
			{
				// Token: 0x04001B40 RID: 6976
				public int index;

				// Token: 0x04001B41 RID: 6977
				public string name;

				// Token: 0x04001B42 RID: 6978
				public float actualWidth;

				// Token: 0x04001B43 RID: 6979
				public Length width;

				// Token: 0x04001B44 RID: 6980
				public bool visible;
			}
		}

		// Token: 0x0200072F RID: 1839
		internal class ColumnData
		{
			// Token: 0x17000D08 RID: 3336
			// (get) Token: 0x0600358C RID: 13708 RVA: 0x000D0A73 File Offset: 0x000CEC73
			// (set) Token: 0x0600358D RID: 13709 RVA: 0x000D0A7B File Offset: 0x000CEC7B
			public MultiColumnHeaderColumn control { get; set; }

			// Token: 0x17000D09 RID: 3337
			// (get) Token: 0x0600358E RID: 13710 RVA: 0x000D0A84 File Offset: 0x000CEC84
			// (set) Token: 0x0600358F RID: 13711 RVA: 0x000D0A8C File Offset: 0x000CEC8C
			public MultiColumnHeaderColumnResizeHandle resizeHandle { get; set; }
		}

		// Token: 0x02000730 RID: 1840
		private struct SortedColumnState
		{
			// Token: 0x06003591 RID: 13713 RVA: 0x000D0A95 File Offset: 0x000CEC95
			public SortedColumnState(SortColumnDescription desc, SortDirection dir)
			{
				this.columnDesc = desc;
				this.direction = dir;
			}

			// Token: 0x04001B47 RID: 6983
			public SortColumnDescription columnDesc;

			// Token: 0x04001B48 RID: 6984
			public SortDirection direction;
		}
	}
}
