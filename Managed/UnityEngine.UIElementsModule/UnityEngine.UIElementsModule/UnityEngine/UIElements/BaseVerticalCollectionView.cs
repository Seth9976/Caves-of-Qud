using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200009E RID: 158
	public abstract class BaseVerticalCollectionView : BindableElement, ISerializationCallbackReceiver
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000604 RID: 1540 RVA: 0x0001BFBF File Offset: 0x0001A1BF
		// (remove) Token: 0x06000605 RID: 1541 RVA: 0x0001BFC9 File Offset: 0x0001A1C9
		[Obsolete("onItemsChosen is deprecated, use itemsChosen instead", false)]
		public event Action<IEnumerable<object>> onItemsChosen
		{
			add
			{
				this.itemsChosen += value;
			}
			remove
			{
				this.itemsChosen -= value;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000606 RID: 1542 RVA: 0x0001BFD4 File Offset: 0x0001A1D4
		// (remove) Token: 0x06000607 RID: 1543 RVA: 0x0001C00C File Offset: 0x0001A20C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IEnumerable<object>> itemsChosen;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000608 RID: 1544 RVA: 0x0001C041 File Offset: 0x0001A241
		// (remove) Token: 0x06000609 RID: 1545 RVA: 0x0001C04B File Offset: 0x0001A24B
		[Obsolete("onSelectionChange is deprecated, use selectionChanged instead", false)]
		public event Action<IEnumerable<object>> onSelectionChange
		{
			add
			{
				this.selectionChanged += value;
			}
			remove
			{
				this.selectionChanged -= value;
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600060A RID: 1546 RVA: 0x0001C058 File Offset: 0x0001A258
		// (remove) Token: 0x0600060B RID: 1547 RVA: 0x0001C090 File Offset: 0x0001A290
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IEnumerable<object>> selectionChanged;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600060C RID: 1548 RVA: 0x0001C0C5 File Offset: 0x0001A2C5
		// (remove) Token: 0x0600060D RID: 1549 RVA: 0x0001C0CF File Offset: 0x0001A2CF
		[Obsolete("onSelectedIndicesChange is deprecated, use selectedIndicesChanged instead", false)]
		public event Action<IEnumerable<int>> onSelectedIndicesChange
		{
			add
			{
				this.selectedIndicesChanged += value;
			}
			remove
			{
				this.selectedIndicesChanged -= value;
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600060E RID: 1550 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		// (remove) Token: 0x0600060F RID: 1551 RVA: 0x0001C114 File Offset: 0x0001A314
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IEnumerable<int>> selectedIndicesChanged;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000610 RID: 1552 RVA: 0x0001C14C File Offset: 0x0001A34C
		// (remove) Token: 0x06000611 RID: 1553 RVA: 0x0001C184 File Offset: 0x0001A384
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, int> itemIndexChanged;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000612 RID: 1554 RVA: 0x0001C1BC File Offset: 0x0001A3BC
		// (remove) Token: 0x06000613 RID: 1555 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action itemsSourceChanged;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000614 RID: 1556 RVA: 0x0001C22C File Offset: 0x0001A42C
		// (remove) Token: 0x06000615 RID: 1557 RVA: 0x0001C264 File Offset: 0x0001A464
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event Action m_SelectionNotChanged = delegate
		{
		};

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000616 RID: 1558 RVA: 0x0001C299 File Offset: 0x0001A499
		// (remove) Token: 0x06000617 RID: 1559 RVA: 0x0001C2A3 File Offset: 0x0001A4A3
		internal event Action selectionNotChanged
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			add
			{
				this.m_SelectionNotChanged += value;
			}
			remove
			{
				this.m_SelectionNotChanged -= value;
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000618 RID: 1560 RVA: 0x0001C2B0 File Offset: 0x0001A4B0
		// (remove) Token: 0x06000619 RID: 1561 RVA: 0x0001C2E8 File Offset: 0x0001A4E8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Func<CanStartDragArgs, bool> canStartDrag;

		// Token: 0x0600061A RID: 1562 RVA: 0x0001C31D File Offset: 0x0001A51D
		internal bool HasCanStartDrag()
		{
			return this.canStartDrag != null;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001C328 File Offset: 0x0001A528
		internal bool RaiseCanStartDrag(ReusableCollectionItem item, IEnumerable<int> ids)
		{
			Func<CanStartDragArgs, bool> func = this.canStartDrag;
			return func == null || func(new CanStartDragArgs((item != null) ? item.rootElement : null, (item != null) ? item.id : BaseTreeView.invalidId, ids));
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600061C RID: 1564 RVA: 0x0001C370 File Offset: 0x0001A570
		// (remove) Token: 0x0600061D RID: 1565 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Func<SetupDragAndDropArgs, StartDragArgs> setupDragAndDrop;

		// Token: 0x0600061E RID: 1566 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
		internal StartDragArgs RaiseSetupDragAndDrop(ReusableCollectionItem item, IEnumerable<int> ids, StartDragArgs args)
		{
			Func<SetupDragAndDropArgs, StartDragArgs> func = this.setupDragAndDrop;
			return (func != null) ? func(new SetupDragAndDropArgs((item != null) ? item.rootElement : null, ids, args)) : args;
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600061F RID: 1567 RVA: 0x0001C418 File Offset: 0x0001A618
		// (remove) Token: 0x06000620 RID: 1568 RVA: 0x0001C450 File Offset: 0x0001A650
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Func<HandleDragAndDropArgs, DragVisualMode> dragAndDropUpdate;

		// Token: 0x06000621 RID: 1569 RVA: 0x0001C488 File Offset: 0x0001A688
		internal DragVisualMode RaiseHandleDragAndDrop(Vector2 pointerPosition, DragAndDropArgs dragAndDropArgs)
		{
			Func<HandleDragAndDropArgs, DragVisualMode> func = this.dragAndDropUpdate;
			return (func != null) ? func(new HandleDragAndDropArgs(pointerPosition, dragAndDropArgs)) : DragVisualMode.None;
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000622 RID: 1570 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		// (remove) Token: 0x06000623 RID: 1571 RVA: 0x0001C4EC File Offset: 0x0001A6EC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Func<HandleDragAndDropArgs, DragVisualMode> handleDrop;

		// Token: 0x06000624 RID: 1572 RVA: 0x0001C524 File Offset: 0x0001A724
		internal DragVisualMode RaiseDrop(Vector2 pointerPosition, DragAndDropArgs dragAndDropArgs)
		{
			Func<HandleDragAndDropArgs, DragVisualMode> func = this.handleDrop;
			return (func != null) ? func(new HandleDragAndDropArgs(pointerPosition, dragAndDropArgs)) : DragVisualMode.None;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001C54F File Offset: 0x0001A74F
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0001B4DC File Offset: 0x000196DC
		[CreateProperty]
		public IList itemsSource
		{
			get
			{
				CollectionViewController viewController = this.viewController;
				return (viewController != null) ? viewController.itemsSource : null;
			}
			set
			{
				this.GetOrCreateViewController().itemsSource = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001C563 File Offset: 0x0001A763
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0001C563 File Offset: 0x0001A763
		[Obsolete("makeItem has been moved to ListView and TreeView. Use these ones instead.")]
		public Func<VisualElement> makeItem
		{
			get
			{
				throw new UnityException("makeItem has been moved to ListView and TreeView. Use these ones instead.");
			}
			set
			{
				throw new UnityException("makeItem has been moved to ListView and TreeView. Use these ones instead.");
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001C56F File Offset: 0x0001A76F
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0001C56F File Offset: 0x0001A76F
		[Obsolete("bindItem has been moved to ListView and TreeView. Use these ones instead.")]
		public Action<VisualElement, int> bindItem
		{
			get
			{
				throw new UnityException("bindItem has been moved to ListView and TreeView. Use these ones instead.");
			}
			set
			{
				throw new UnityException("bindItem has been moved to ListView and TreeView. Use these ones instead.");
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001C57B File Offset: 0x0001A77B
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0001C57B File Offset: 0x0001A77B
		[Obsolete("unbindItem has been moved to ListView and TreeView. Use these ones instead.")]
		public Action<VisualElement, int> unbindItem
		{
			get
			{
				throw new UnityException("unbindItem has been moved to ListView and TreeView. Use these ones instead.");
			}
			set
			{
				throw new UnityException("unbindItem has been moved to ListView and TreeView. Use these ones instead.");
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001C587 File Offset: 0x0001A787
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0001C587 File Offset: 0x0001A787
		[Obsolete("destroyItem has been moved to ListView and TreeView. Use these ones instead.")]
		public Action<VisualElement> destroyItem
		{
			get
			{
				throw new UnityException("destroyItem has been moved to ListView and TreeView. Use these ones instead.");
			}
			set
			{
				throw new UnityException("destroyItem has been moved to ListView and TreeView. Use these ones instead.");
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001C593 File Offset: 0x0001A793
		public override VisualElement contentContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001C598 File Offset: 0x0001A798
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001C5B0 File Offset: 0x0001A7B0
		[CreateProperty]
		public SelectionType selectionType
		{
			get
			{
				return this.m_SelectionType;
			}
			set
			{
				SelectionType selectionType = this.m_SelectionType;
				this.m_SelectionType = value;
				bool flag = this.m_SelectionType == SelectionType.None;
				if (flag)
				{
					this.ClearSelection();
				}
				else
				{
					bool flag2 = this.m_SelectionType == SelectionType.Single;
					if (flag2)
					{
						bool flag3 = this.m_Selection.indexCount > 1;
						if (flag3)
						{
							this.SetSelection(this.m_Selection.FirstIndex());
						}
					}
				}
				bool flag4 = selectionType != this.m_SelectionType;
				if (flag4)
				{
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.selectionTypeProperty);
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001C637 File Offset: 0x0001A837
		[CreateProperty(ReadOnly = true)]
		public object selectedItem
		{
			get
			{
				return this.m_Selection.FirstObject();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001C644 File Offset: 0x0001A844
		[CreateProperty(ReadOnly = true)]
		public IEnumerable<object> selectedItems
		{
			get
			{
				foreach (int index in this.m_Selection.indices)
				{
					object item;
					bool flag = this.m_Selection.items.TryGetValue(index, out item);
					if (flag)
					{
						yield return item;
					}
					else
					{
						yield return null;
					}
					item = null;
				}
				List<int>.Enumerator enumerator = default(List<int>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001C664 File Offset: 0x0001A864
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001C691 File Offset: 0x0001A891
		[CreateProperty]
		public int selectedIndex
		{
			get
			{
				return (this.m_Selection.indexCount == 0) ? (-1) : this.m_Selection.FirstIndex();
			}
			set
			{
				this.SetSelection(value);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001C69B File Offset: 0x0001A89B
		[CreateProperty(ReadOnly = true)]
		public IEnumerable<int> selectedIndices
		{
			get
			{
				return this.m_Selection.indices;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001C6A8 File Offset: 0x0001A8A8
		public IEnumerable<int> selectedIds
		{
			get
			{
				return this.m_Selection.selectedIds;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001C6B5 File Offset: 0x0001A8B5
		internal IEnumerable<ReusableCollectionItem> activeItems
		{
			get
			{
				CollectionVirtualizationController virtualizationController = this.m_VirtualizationController;
				return ((virtualizationController != null) ? virtualizationController.activeItems : null) ?? BaseVerticalCollectionView.k_EmptyItems;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001C6D2 File Offset: 0x0001A8D2
		internal ScrollView scrollView
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_ScrollView;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001C6DA File Offset: 0x0001A8DA
		internal ListViewDragger dragger
		{
			get
			{
				return this.m_Dragger;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001C6E2 File Offset: 0x0001A8E2
		internal CollectionVirtualizationController virtualizationController
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.GetOrCreateVirtualizationController();
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001C6EA File Offset: 0x0001A8EA
		public CollectionViewController viewController
		{
			get
			{
				return this.m_ViewController;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001C6F2 File Offset: 0x0001A8F2
		[Obsolete("resolvedItemHeight is deprecated and will be removed from the API.", false)]
		public float resolvedItemHeight
		{
			get
			{
				return this.ResolveItemHeight(-1f);
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001C700 File Offset: 0x0001A900
		internal float ResolveItemHeight(float height = -1f)
		{
			return (height < 0f) ? this.fixedItemHeight : height;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001C723 File Offset: 0x0001A923
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0001C738 File Offset: 0x0001A938
		[CreateProperty]
		public bool showBorder
		{
			get
			{
				return this.m_ScrollView.ClassListContains(BaseVerticalCollectionView.borderUssClassName);
			}
			set
			{
				bool showBorder = this.showBorder;
				this.m_ScrollView.EnableInClassList(BaseVerticalCollectionView.borderUssClassName, value);
				bool flag = showBorder != this.showBorder;
				if (flag)
				{
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.showBorderProperty);
				}
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001C77C File Offset: 0x0001A97C
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0001C7C4 File Offset: 0x0001A9C4
		[CreateProperty]
		public bool reorderable
		{
			get
			{
				ListViewDragger dragger = this.m_Dragger;
				bool? flag;
				if (dragger == null)
				{
					flag = null;
				}
				else
				{
					ICollectionDragAndDropController dragAndDropController = dragger.dragAndDropController;
					flag = ((dragAndDropController != null) ? new bool?(dragAndDropController.enableReordering) : null);
				}
				bool? flag2 = flag;
				return flag2.GetValueOrDefault();
			}
			set
			{
				bool reorderable = this.reorderable;
				try
				{
					ICollectionDragAndDropController dragAndDropController = this.m_Dragger.dragAndDropController;
					bool flag = dragAndDropController != null && dragAndDropController.enableReordering != value;
					if (flag)
					{
						dragAndDropController.enableReordering = value;
						this.Rebuild();
					}
				}
				finally
				{
					bool flag2 = reorderable != this.reorderable;
					if (flag2)
					{
						base.NotifyPropertyChanged(in BaseVerticalCollectionView.reorderableProperty);
					}
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001C840 File Offset: 0x0001AA40
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0001C848 File Offset: 0x0001AA48
		[CreateProperty]
		public bool horizontalScrollingEnabled
		{
			get
			{
				return this.m_HorizontalScrollingEnabled;
			}
			set
			{
				bool flag = this.m_HorizontalScrollingEnabled == value;
				if (!flag)
				{
					this.m_HorizontalScrollingEnabled = value;
					this.m_ScrollView.horizontalScrollerVisibility = (value ? ScrollerVisibility.Auto : ScrollerVisibility.Hidden);
					this.m_ScrollView.mode = (value ? ScrollViewMode.VerticalAndHorizontal : ScrollViewMode.Vertical);
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.horizontalScrollingEnabledProperty);
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001C8A0 File Offset: 0x0001AAA0
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0001C8B8 File Offset: 0x0001AAB8
		[CreateProperty]
		public AlternatingRowBackground showAlternatingRowBackgrounds
		{
			get
			{
				return this.m_ShowAlternatingRowBackgrounds;
			}
			set
			{
				bool flag = this.m_ShowAlternatingRowBackgrounds == value;
				if (!flag)
				{
					this.m_ShowAlternatingRowBackgrounds = value;
					this.RefreshItems();
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.showAlternatingRowBackgroundsProperty);
				}
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001C8EF File Offset: 0x0001AAEF
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0001C8F8 File Offset: 0x0001AAF8
		[CreateProperty]
		public CollectionVirtualizationMethod virtualizationMethod
		{
			get
			{
				return this.m_VirtualizationMethod;
			}
			set
			{
				bool flag = this.m_VirtualizationMethod == value;
				if (!flag)
				{
					this.m_VirtualizationMethod = value;
					this.CreateVirtualizationController();
					this.Rebuild();
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.virtualizationMethodProperty);
				}
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001C936 File Offset: 0x0001AB36
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0001C93F File Offset: 0x0001AB3F
		[Obsolete("itemHeight is deprecated, use fixedItemHeight instead.", false)]
		public int itemHeight
		{
			get
			{
				return (int)this.fixedItemHeight;
			}
			set
			{
				this.fixedItemHeight = (float)value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001C94A File Offset: 0x0001AB4A
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0001C954 File Offset: 0x0001AB54
		[CreateProperty]
		public float fixedItemHeight
		{
			get
			{
				return this.m_FixedItemHeight;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("fixedItemHeight", "Value needs to be positive for virtualization.");
				}
				this.m_ItemHeightIsInline = true;
				bool flag2 = Math.Abs(this.m_FixedItemHeight - value) > float.Epsilon;
				if (flag2)
				{
					this.m_FixedItemHeight = value;
					this.RefreshItems();
					base.NotifyPropertyChanged(in BaseVerticalCollectionView.fixedItemHeightProperty);
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001C9B9 File Offset: 0x0001ABB9
		internal float lastHeight
		{
			get
			{
				return this.m_LastHeight;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001C9C1 File Offset: 0x0001ABC1
		internal bool isRebuildScheduled
		{
			get
			{
				IVisualElementScheduledItem rebuildScheduled = this.m_RebuildScheduled;
				return rebuildScheduled != null && rebuildScheduled.isActive;
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001C9D5 File Offset: 0x0001ABD5
		private protected virtual void CreateVirtualizationController()
		{
			this.CreateVirtualizationController<ReusableCollectionItem>();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001C9E0 File Offset: 0x0001ABE0
		internal CollectionVirtualizationController GetOrCreateVirtualizationController()
		{
			bool flag = this.m_VirtualizationController == null;
			if (flag)
			{
				this.CreateVirtualizationController();
			}
			return this.m_VirtualizationController;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		internal void CreateVirtualizationController<T>() where T : ReusableCollectionItem, new()
		{
			CollectionVirtualizationMethod virtualizationMethod = this.virtualizationMethod;
			CollectionVirtualizationMethod collectionVirtualizationMethod = virtualizationMethod;
			if (collectionVirtualizationMethod != CollectionVirtualizationMethod.FixedHeight)
			{
				if (collectionVirtualizationMethod != CollectionVirtualizationMethod.DynamicHeight)
				{
					throw new ArgumentOutOfRangeException("virtualizationMethod", this.virtualizationMethod, "Unsupported virtualizationMethod virtualization");
				}
				this.m_VirtualizationController = new DynamicHeightVirtualizationController<T>(this);
			}
			else
			{
				this.m_VirtualizationController = new FixedHeightVirtualizationController<T>(this);
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001CA68 File Offset: 0x0001AC68
		internal CollectionViewController GetOrCreateViewController()
		{
			bool flag = this.m_ViewController == null;
			if (flag)
			{
				this.SetViewController(this.CreateViewController());
			}
			return this.m_ViewController;
		}

		// Token: 0x06000653 RID: 1619
		protected abstract CollectionViewController CreateViewController();

		// Token: 0x06000654 RID: 1620 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		public virtual void SetViewController(CollectionViewController controller)
		{
			bool flag = this.m_ViewController != null;
			if (flag)
			{
				this.m_ViewController.itemIndexChanged -= this.m_ItemIndexChangedCallback;
				this.m_ViewController.itemsSourceChanged -= this.m_ItemsSourceChangedCallback;
				this.m_ViewController.Dispose();
				this.m_ViewController = null;
			}
			this.m_ViewController = controller;
			bool flag2 = this.m_ViewController != null;
			if (flag2)
			{
				this.m_ViewController.SetView(this);
				this.m_ViewController.itemIndexChanged += this.m_ItemIndexChangedCallback;
				this.m_ViewController.itemsSourceChanged += this.m_ItemsSourceChangedCallback;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001CB38 File Offset: 0x0001AD38
		internal virtual ListViewDragger CreateDragger()
		{
			return new ListViewDragger(this);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001CB50 File Offset: 0x0001AD50
		internal void InitializeDragAndDropController(bool enableReordering)
		{
			bool flag = this.m_Dragger != null;
			if (flag)
			{
				this.m_Dragger.UnregisterCallbacksFromTarget(true);
				this.m_Dragger.dragAndDropController = null;
				this.m_Dragger = null;
			}
			this.m_Dragger = this.CreateDragger();
			this.m_Dragger.dragAndDropController = this.CreateDragAndDropController();
			bool flag2 = this.m_Dragger.dragAndDropController == null;
			if (!flag2)
			{
				this.m_Dragger.dragAndDropController.enableReordering = enableReordering;
			}
		}

		// Token: 0x06000657 RID: 1623
		internal abstract ICollectionDragAndDropController CreateDragAndDropController();

		// Token: 0x06000658 RID: 1624 RVA: 0x0001CBD2 File Offset: 0x0001ADD2
		internal void SetDragAndDropController(ICollectionDragAndDropController dragAndDropController)
		{
			if (this.m_Dragger == null)
			{
				this.m_Dragger = this.CreateDragger();
			}
			this.m_Dragger.dragAndDropController = dragAndDropController;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001CBF8 File Offset: 0x0001ADF8
		public BaseVerticalCollectionView()
		{
			base.AddToClassList(BaseVerticalCollectionView.ussClassName);
			this.m_Selection = new BaseVerticalCollectionView.Selection
			{
				selectedIds = this.m_SelectedIds
			};
			this.selectionType = SelectionType.Single;
			this.m_ScrollView = new ScrollView();
			this.m_ScrollView.AddToClassList(BaseVerticalCollectionView.listScrollViewUssClassName);
			this.m_ScrollView.verticalScroller.valueChanged += delegate(float v)
			{
				this.OnScroll(new Vector2(0f, v));
			};
			this.m_ScrollView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChanged), TrickleDown.NoTrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			base.hierarchy.Add(this.m_ScrollView);
			this.m_ScrollView.contentContainer.focusable = true;
			this.m_ScrollView.contentContainer.usageHints &= ~UsageHints.GroupTransform;
			this.m_ScrollView.viewDataKey = "unity-vertical-collection-scroll-view";
			this.m_ScrollView.verticalScroller.viewDataKey = null;
			this.m_ScrollView.horizontalScroller.viewDataKey = null;
			this.focusable = true;
			base.isCompositeRoot = true;
			base.delegatesFocus = true;
			this.m_ItemIndexChangedCallback = new Action<int, int>(this.OnItemIndexChanged);
			this.m_ItemsSourceChangedCallback = new Action(this.OnItemsSourceChanged);
			this.InitializeDragAndDropController(false);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		public BaseVerticalCollectionView(IList itemsSource, float itemHeight = -1f)
			: this()
		{
			bool flag = Math.Abs(itemHeight - -1f) > float.Epsilon;
			if (flag)
			{
				this.m_FixedItemHeight = itemHeight;
				this.m_ItemHeightIsInline = true;
			}
			bool flag2 = itemsSource != null;
			if (flag2)
			{
				this.itemsSource = itemsSource;
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001CE3C File Offset: 0x0001B03C
		[Obsolete("makeItem and bindItem are now in ListView and TreeView directly, please use a constructor without these parameters.")]
		public BaseVerticalCollectionView(IList itemsSource, float itemHeight = -1f, Func<VisualElement> makeItem = null, Action<VisualElement, int> bindItem = null)
			: this()
		{
			bool flag = Math.Abs(itemHeight - -1f) > float.Epsilon;
			if (flag)
			{
				this.m_FixedItemHeight = itemHeight;
				this.m_ItemHeightIsInline = true;
			}
			this.itemsSource = itemsSource;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001CE80 File Offset: 0x0001B080
		public VisualElement GetRootElementForId(int id)
		{
			ReusableCollectionItem reusableCollectionItem = this.activeItems.FirstOrDefault<ReusableCollectionItem>((ReusableCollectionItem t) => t.id == id);
			return (reusableCollectionItem != null) ? reusableCollectionItem.rootElement : null;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001CEC4 File Offset: 0x0001B0C4
		public VisualElement GetRootElementForIndex(int index)
		{
			return this.GetRootElementForId(this.viewController.GetIdForIndex(index));
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		internal virtual bool HasValidDataAndBindings()
		{
			return this.m_ViewController != null && this.itemsSource != null;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001CF0E File Offset: 0x0001B10E
		private void OnItemIndexChanged(int srcIndex, int dstIndex)
		{
			Action<int, int> action = this.itemIndexChanged;
			if (action != null)
			{
				action(srcIndex, dstIndex);
			}
			this.RefreshItems();
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001CF2C File Offset: 0x0001B12C
		private void OnItemsSourceChanged()
		{
			Action action = this.itemsSourceChanged;
			if (action != null)
			{
				action();
			}
			base.NotifyPropertyChanged(in BaseVerticalCollectionView.itemsSourceProperty);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001CF50 File Offset: 0x0001B150
		public void RefreshItem(int index)
		{
			foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
			{
				int index2 = reusableCollectionItem.index;
				bool flag = index2 == index;
				if (flag)
				{
					this.viewController.InvokeUnbindItem(reusableCollectionItem, index2);
					this.viewController.InvokeBindItem(reusableCollectionItem, index2);
					break;
				}
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001CFCC File Offset: 0x0001B1CC
		public void RefreshItems()
		{
			using (BaseVerticalCollectionView.k_RefreshMarker.Auto())
			{
				bool flag = this.m_ViewController == null;
				if (!flag)
				{
					IVisualElementScheduledItem rebuildScheduled = this.m_RebuildScheduled;
					bool flag2 = rebuildScheduled != null && rebuildScheduled.isActive;
					if (flag2)
					{
						this.Rebuild();
					}
					else
					{
						this.m_ViewController.PreRefresh();
						this.RefreshSelection();
						this.virtualizationController.Refresh(false);
						this.PostRefresh();
					}
				}
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001D064 File Offset: 0x0001B264
		[Obsolete("Refresh() has been deprecated. Use Rebuild() instead. (UnityUpgradable) -> Rebuild()", false)]
		public void Refresh()
		{
			this.Rebuild();
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001D070 File Offset: 0x0001B270
		public void Rebuild()
		{
			using (BaseVerticalCollectionView.k_RebuildMarker.Auto())
			{
				bool flag = this.m_ViewController == null;
				if (!flag)
				{
					this.m_ViewController.PreRefresh();
					this.RefreshSelection();
					this.virtualizationController.Refresh(true);
					this.PostRefresh();
					IVisualElementScheduledItem rebuildScheduled = this.m_RebuildScheduled;
					if (rebuildScheduled != null)
					{
						rebuildScheduled.Pause();
					}
				}
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001D0F8 File Offset: 0x0001B2F8
		internal void ScheduleRebuild()
		{
			bool flag = this.m_RebuildScheduled == null;
			if (flag)
			{
				this.m_RebuildScheduled = base.schedule.Execute(new Action(this.Rebuild));
			}
			else
			{
				bool flag2 = !this.m_RebuildScheduled.isActive;
				if (flag2)
				{
					this.m_RebuildScheduled.Resume();
				}
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001D150 File Offset: 0x0001B350
		private void RefreshSelection()
		{
			BaseVerticalCollectionView.<>c__DisplayClass193_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.selectedIndicesChanged = false;
			CS$<>8__locals1.previousSelectionCount = this.m_Selection.indexCount;
			this.m_Selection.items.Clear();
			CollectionViewController viewController = this.viewController;
			bool flag = ((viewController != null) ? viewController.itemsSource : null) == null;
			if (flag)
			{
				this.m_Selection.ClearIndices();
				this.<RefreshSelection>g__NotifyIfChanged|193_0(ref CS$<>8__locals1);
			}
			else
			{
				bool flag2 = this.m_Selection.idCount > 0;
				if (flag2)
				{
					List<int> list;
					using (CollectionPool<List<int>, int>.Get(out list))
					{
						foreach (int num in this.m_Selection.selectedIds)
						{
							int indexForId = this.viewController.GetIndexForId(num);
							bool flag3 = indexForId < 0;
							if (flag3)
							{
								CS$<>8__locals1.selectedIndicesChanged = true;
							}
							else
							{
								bool flag4 = !this.m_Selection.ContainsIndex(indexForId);
								if (flag4)
								{
									CS$<>8__locals1.selectedIndicesChanged = true;
								}
								list.Add(indexForId);
							}
						}
						this.m_Selection.ClearIndices();
						foreach (int num2 in list)
						{
							this.m_Selection.AddIndex(num2, this.viewController.GetItemForIndex(num2));
						}
					}
				}
				else
				{
					bool flag5 = this.m_Selection.idCount == 0 && this.m_Selection.indexCount > 0;
					if (flag5)
					{
						this.m_Selection.ClearIndices();
						CS$<>8__locals1.selectedIndicesChanged = true;
					}
				}
				this.<RefreshSelection>g__NotifyIfChanged|193_0(ref CS$<>8__locals1);
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001D348 File Offset: 0x0001B548
		private protected virtual void PostRefresh()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.m_LastHeight = this.m_ScrollView.layout.height;
				bool flag2 = base.panel == null || float.IsNaN(this.m_ScrollView.layout.height);
				if (!flag2)
				{
					this.Resize(this.m_ScrollView.layout.size);
				}
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001D3C1 File Offset: 0x0001B5C1
		public void ScrollTo(VisualElement visualElement)
		{
			this.m_ScrollView.ScrollTo(visualElement);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001D3D4 File Offset: 0x0001B5D4
		public void ScrollToItem(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.ScrollToItem(index);
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001D3FE File Offset: 0x0001B5FE
		[Obsolete("ScrollToId() has been deprecated. Use ScrollToItemById() instead. (UnityUpgradable) -> ScrollToItemById(*)", false)]
		public void ScrollToId(int id)
		{
			this.ScrollToItemById(id);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001D40C File Offset: 0x0001B60C
		public void ScrollToItemById(int id)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				int indexForId = this.viewController.GetIndexForId(id);
				this.virtualizationController.ScrollToItem(indexForId);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001D444 File Offset: 0x0001B644
		private void OnScroll(Vector2 offset)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.OnScroll(offset);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001D46E File Offset: 0x0001B66E
		private void Resize(Vector2 size)
		{
			this.virtualizationController.Resize(size);
			this.m_LastHeight = size.y;
			this.virtualizationController.UpdateBackground();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001D498 File Offset: 0x0001B698
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
				this.m_ScrollView.contentContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001D55C File Offset: 0x0001B75C
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.RemoveManipulator(this.m_NavigationManipulator);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001D60B File Offset: 0x0001B80B
		[Obsolete("OnKeyDown is obsolete and will be removed from ListView. Use the event system instead, i.e. SendEvent(EventBase e).", true)]
		public void OnKeyDown(KeyDownEvent evt)
		{
			this.m_NavigationManipulator.OnKeyDown(evt);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001D61C File Offset: 0x0001B81C
		private bool Apply(KeyboardNavigationOperation op, bool shiftKey, bool altKey)
		{
			BaseVerticalCollectionView.<>c__DisplayClass204_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.shiftKey = shiftKey;
			bool flag = this.selectionType == SelectionType.None || !this.HasValidDataAndBindings();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				switch (op)
				{
				case KeyboardNavigationOperation.SelectAll:
					this.SelectAll();
					return true;
				case KeyboardNavigationOperation.Cancel:
					this.ClearSelection();
					return true;
				case KeyboardNavigationOperation.Submit:
				{
					Action<IEnumerable<object>> action = this.itemsChosen;
					if (action != null)
					{
						action(this.selectedItems);
					}
					this.ScrollToItem(this.selectedIndex);
					return true;
				}
				case KeyboardNavigationOperation.Previous:
				{
					bool flag3 = this.selectedIndex > 0;
					if (flag3)
					{
						this.<Apply>g__HandleSelectionAndScroll|204_0(this.selectedIndex - 1, ref CS$<>8__locals1);
						return true;
					}
					break;
				}
				case KeyboardNavigationOperation.Next:
				{
					bool flag4 = this.selectedIndex + 1 < this.m_ViewController.itemsSource.Count;
					if (flag4)
					{
						this.<Apply>g__HandleSelectionAndScroll|204_0(this.selectedIndex + 1, ref CS$<>8__locals1);
						return true;
					}
					break;
				}
				case KeyboardNavigationOperation.MoveRight:
				{
					bool flag5 = this.m_Selection.indexCount > 0;
					if (flag5)
					{
						return this.HandleItemNavigation(true, altKey);
					}
					break;
				}
				case KeyboardNavigationOperation.MoveLeft:
				{
					bool flag6 = this.m_Selection.indexCount > 0;
					if (flag6)
					{
						return this.HandleItemNavigation(false, altKey);
					}
					break;
				}
				case KeyboardNavigationOperation.PageUp:
				{
					bool flag7 = this.m_Selection.indexCount > 0;
					if (flag7)
					{
						int num = (this.m_IsRangeSelectionDirectionUp ? this.m_Selection.minIndex : this.m_Selection.maxIndex);
						this.<Apply>g__HandleSelectionAndScroll|204_0(Mathf.Max(0, num - (this.virtualizationController.visibleItemCount - 1)), ref CS$<>8__locals1);
					}
					return true;
				}
				case KeyboardNavigationOperation.PageDown:
				{
					bool flag8 = this.m_Selection.indexCount > 0;
					if (flag8)
					{
						int num2 = (this.m_IsRangeSelectionDirectionUp ? this.m_Selection.minIndex : this.m_Selection.maxIndex);
						this.<Apply>g__HandleSelectionAndScroll|204_0(Mathf.Min(this.viewController.itemsSource.Count - 1, num2 + (this.virtualizationController.visibleItemCount - 1)), ref CS$<>8__locals1);
					}
					return true;
				}
				case KeyboardNavigationOperation.Begin:
					this.<Apply>g__HandleSelectionAndScroll|204_0(0, ref CS$<>8__locals1);
					return true;
				case KeyboardNavigationOperation.End:
					this.<Apply>g__HandleSelectionAndScroll|204_0(this.m_ViewController.itemsSource.Count - 1, ref CS$<>8__locals1);
					return true;
				default:
					throw new ArgumentOutOfRangeException("op", op, null);
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001D8A8 File Offset: 0x0001BAA8
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			KeyDownEvent keyDownEvent = sourceEvent as KeyDownEvent;
			bool flag;
			if (keyDownEvent == null || !keyDownEvent.shiftKey)
			{
				INavigationEvent navigationEvent = sourceEvent as INavigationEvent;
				if (navigationEvent != null)
				{
					if (navigationEvent.shiftKey)
					{
						goto IL_0030;
					}
				}
				flag = false;
				goto IL_0038;
			}
			IL_0030:
			flag = true;
			IL_0038:
			bool flag2 = flag;
			keyDownEvent = sourceEvent as KeyDownEvent;
			bool flag3;
			if (keyDownEvent == null || !keyDownEvent.altKey)
			{
				INavigationEvent navigationEvent = sourceEvent as INavigationEvent;
				if (navigationEvent != null)
				{
					if (navigationEvent.altKey)
					{
						goto IL_006C;
					}
				}
				flag3 = false;
				goto IL_0072;
			}
			IL_006C:
			flag3 = true;
			IL_0072:
			bool flag4 = flag3;
			bool flag5 = this.Apply(op, flag2, flag4);
			if (flag5)
			{
				sourceEvent.StopPropagation();
			}
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.IgnoreEvent(sourceEvent);
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001D954 File Offset: 0x0001BB54
		private protected virtual bool HandleItemNavigation(bool moveIn, bool altKey)
		{
			return false;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001D968 File Offset: 0x0001BB68
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = (evt.pressedButtons & 1) == 0;
				if (flag2)
				{
					this.ProcessPointerUp(evt);
				}
				else
				{
					this.ProcessPointerDown(evt);
				}
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001D9AA File Offset: 0x0001BBAA
		private void OnPointerDown(PointerDownEvent evt)
		{
			this.ProcessPointerDown(evt);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					this.ClearSelection();
				}
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001D9EB File Offset: 0x0001BBEB
		private void OnPointerUp(PointerUpEvent evt)
		{
			this.ProcessPointerUp(evt);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001D9F8 File Offset: 0x0001BBF8
		private void ProcessPointerDown(IPointerEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					int button = evt.button;
					bool flag3 = button != 0 && button != 1;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							this.m_TouchDownPosition = evt.position;
							PointerDownEvent pointerDownEvent = evt as PointerDownEvent;
							long num = ((pointerDownEvent != null) ? pointerDownEvent.timestamp : 0L);
							this.m_PointerDownCount = ((num - this.m_LastPointerDownTimeStamp < (long)Event.GetDoubleClickTime()) ? (this.m_PointerDownCount + 1) : 1);
							this.m_LastPointerDownTimeStamp = num;
						}
						else
						{
							this.m_PointerDownCount = evt.clickCount;
							this.DoSelect(evt.localPosition, evt.button, this.m_PointerDownCount, evt.actionKey, evt.shiftKey);
						}
					}
				}
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		private void ProcessPointerUp(IPointerEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					int button = evt.button;
					bool flag3 = button != 0 && button != 1;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							bool flag5 = (evt.position - this.m_TouchDownPosition).sqrMagnitude <= 100f;
							if (flag5)
							{
								this.DoSelect(evt.localPosition, evt.button, this.m_PointerDownCount, evt.actionKey, evt.shiftKey);
							}
							PointerUpEvent pointerUpEvent = evt as PointerUpEvent;
							long num = ((pointerUpEvent != null) ? pointerUpEvent.timestamp : 0L);
							this.m_PointerDownCount = ((num - this.m_LastPointerDownTimeStamp < (long)Event.GetDoubleClickTime()) ? this.m_PointerDownCount : 0);
						}
						else
						{
							int indexFromPosition = this.virtualizationController.GetIndexFromPosition(evt.localPosition);
							bool flag6 = this.selectionType == SelectionType.Multiple && evt.button == 0 && !evt.shiftKey && !evt.actionKey && this.m_Selection.indexCount > 1 && this.m_Selection.ContainsIndex(indexFromPosition);
							if (flag6)
							{
								this.ProcessSingleClick(indexFromPosition);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001DC48 File Offset: 0x0001BE48
		private void DoSelect(Vector2 localPosition, int mouseButton, int clickCount, bool actionKey, bool shiftKey)
		{
			int indexFromPosition = this.virtualizationController.GetIndexFromPosition(localPosition);
			int num = ((this.m_Selection.indexCount > 0 && this.m_Selection.FirstIndex() != indexFromPosition) ? 1 : ((clickCount > 2) ? 2 : clickCount));
			bool flag = indexFromPosition > this.viewController.itemsSource.Count - 1;
			if (!flag)
			{
				bool flag2 = this.selectionType == SelectionType.None;
				if (!flag2)
				{
					int idForIndex = this.viewController.GetIdForIndex(indexFromPosition);
					int num2 = num;
					int num3 = num2;
					if (num3 != 1)
					{
						if (num3 == 2)
						{
							bool flag3 = this.itemsChosen == null;
							if (!flag3)
							{
								bool flag4 = false;
								foreach (int num4 in this.selectedIndices)
								{
									bool flag5 = indexFromPosition == num4;
									if (flag5)
									{
										flag4 = true;
										break;
									}
								}
								this.ProcessSingleClick(indexFromPosition);
								bool flag6 = !flag4;
								if (!flag6)
								{
									bool flag7 = !this.allowSingleClickChoice && mouseButton == 0;
									if (flag7)
									{
										Action<IEnumerable<object>> action = this.itemsChosen;
										if (action != null)
										{
											action(this.selectedItems);
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag8 = this.selectionType == SelectionType.Multiple && actionKey;
						if (flag8)
						{
							bool flag9 = this.m_Selection.ContainsId(idForIndex);
							if (flag9)
							{
								this.RemoveFromSelection(indexFromPosition);
							}
							else
							{
								this.AddToSelection(indexFromPosition);
							}
						}
						else
						{
							bool flag10 = this.selectionType == SelectionType.Multiple && shiftKey;
							if (flag10)
							{
								bool flag11 = this.m_Selection.indexCount == 0;
								if (flag11)
								{
									this.SetSelection(indexFromPosition);
								}
								else
								{
									this.DoRangeSelection(indexFromPosition);
								}
							}
							else
							{
								bool flag12 = this.selectionType == SelectionType.Multiple && this.m_Selection.ContainsIndex(indexFromPosition);
								if (flag12)
								{
									Action selectionNotChanged = this.m_SelectionNotChanged;
									if (selectionNotChanged != null)
									{
										selectionNotChanged();
									}
								}
								else
								{
									bool flag13 = this.selectionType == SelectionType.Single && this.m_Selection.ContainsIndex(indexFromPosition);
									if (flag13)
									{
										Action selectionNotChanged2 = this.m_SelectionNotChanged;
										if (selectionNotChanged2 != null)
										{
											selectionNotChanged2();
										}
									}
									else
									{
										this.SetSelection(indexFromPosition);
									}
									bool flag14 = this.allowSingleClickChoice && mouseButton == 0;
									if (flag14)
									{
										Action<IEnumerable<object>> action2 = this.itemsChosen;
										if (action2 != null)
										{
											action2(this.selectedItems);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001DEBC File Offset: 0x0001C0BC
		internal void DoRangeSelection(int rangeSelectionFinalIndex)
		{
			int num = (this.m_IsRangeSelectionDirectionUp ? this.m_Selection.maxIndex : this.m_Selection.minIndex);
			this.ClearSelectionWithoutValidation();
			List<int> list = new List<int>();
			this.m_IsRangeSelectionDirectionUp = rangeSelectionFinalIndex < num;
			bool isRangeSelectionDirectionUp = this.m_IsRangeSelectionDirectionUp;
			if (isRangeSelectionDirectionUp)
			{
				for (int i = rangeSelectionFinalIndex; i <= num; i++)
				{
					list.Add(i);
				}
			}
			else
			{
				for (int j = rangeSelectionFinalIndex; j >= num; j--)
				{
					list.Add(j);
				}
			}
			this.AddToSelection(list);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001DF59 File Offset: 0x0001C159
		private void ProcessSingleClick(int clickedIndex)
		{
			this.SetSelection(clickedIndex);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001DF64 File Offset: 0x0001C164
		internal void SelectAll()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = this.selectionType != SelectionType.Multiple;
				if (!flag2)
				{
					for (int i = 0; i < this.m_ViewController.itemsSource.Count; i++)
					{
						int idForIndex = this.viewController.GetIdForIndex(i);
						object itemForIndex = this.viewController.GetItemForIndex(i);
						foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
						{
							bool flag3 = reusableCollectionItem.id == idForIndex;
							if (flag3)
							{
								reusableCollectionItem.SetSelected(true);
							}
						}
						bool flag4 = !this.m_Selection.ContainsId(idForIndex);
						if (flag4)
						{
							this.m_Selection.AddId(idForIndex);
							this.m_Selection.AddIndex(i, itemForIndex);
						}
					}
					this.NotifyOfSelectionChange();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001E07C File Offset: 0x0001C27C
		public void AddToSelection(int index)
		{
			this.AddToSelection(new int[] { index });
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001E090 File Offset: 0x0001C290
		internal void AddToSelection(IList<int> indexes)
		{
			bool flag = !this.HasValidDataAndBindings() || indexes == null || indexes.Count == 0;
			if (!flag)
			{
				foreach (int num in indexes)
				{
					this.AddToSelectionWithoutValidation(num);
				}
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001E10C File Offset: 0x0001C30C
		private void AddToSelectionWithoutValidation(int index)
		{
			bool flag = this.m_Selection.ContainsIndex(index);
			if (!flag)
			{
				int idForIndex = this.viewController.GetIdForIndex(index);
				object itemForIndex = this.viewController.GetItemForIndex(index);
				foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
				{
					bool flag2 = reusableCollectionItem.id == idForIndex;
					if (flag2)
					{
						reusableCollectionItem.SetSelected(true);
					}
				}
				this.m_Selection.AddId(idForIndex);
				this.m_Selection.AddIndex(index, itemForIndex);
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001E1B8 File Offset: 0x0001C3B8
		public void RemoveFromSelection(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.RemoveFromSelectionWithoutValidation(index);
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001E1EC File Offset: 0x0001C3EC
		private void RemoveFromSelectionWithoutValidation(int index)
		{
			bool flag = !this.m_Selection.TryRemove(index);
			if (!flag)
			{
				int idForIndex = this.viewController.GetIdForIndex(index);
				foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
				{
					bool flag2 = reusableCollectionItem.id == idForIndex;
					if (flag2)
					{
						reusableCollectionItem.SetSelected(false);
					}
				}
				this.m_Selection.RemoveId(idForIndex);
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001E27C File Offset: 0x0001C47C
		public void SetSelection(int index)
		{
			bool flag = index < 0;
			if (flag)
			{
				this.ClearSelection();
			}
			else
			{
				this.SetSelection(new int[] { index });
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001E2AD File Offset: 0x0001C4AD
		public void SetSelection(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, true);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001E2B9 File Offset: 0x0001C4B9
		public void SetSelectionWithoutNotify(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, false);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		internal void SetSelectionInternal(IEnumerable<int> indices, bool sendNotification)
		{
			bool flag = !this.HasValidDataAndBindings() || indices == null;
			if (!flag)
			{
				bool flag2 = this.MatchesExistingSelection(indices);
				if (!flag2)
				{
					int selectedIndex = this.selectedIndex;
					this.ClearSelectionWithoutValidation();
					ICollection collection = indices as ICollection;
					bool flag3 = collection != null && this.m_Selection.capacity < collection.Count;
					if (flag3)
					{
						this.m_Selection.capacity = collection.Count;
					}
					foreach (int num in indices)
					{
						this.AddToSelectionWithoutValidation(num);
					}
					if (sendNotification)
					{
						bool flag4 = selectedIndex != this.selectedIndex;
						if (flag4)
						{
							base.NotifyPropertyChanged(in BaseVerticalCollectionView.selectedIndexProperty);
						}
						this.NotifyOfSelectionChange();
					}
					base.SaveViewData();
				}
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
		private bool MatchesExistingSelection(IEnumerable<int> indices)
		{
			IList<int> list = indices as IList<int>;
			List<int> list2 = null;
			bool flag3;
			try
			{
				bool flag = list == null;
				if (flag)
				{
					list2 = CollectionPool<List<int>, int>.Get();
					list2.AddRange(indices);
					list = list2;
				}
				bool flag2 = list.Count != this.m_Selection.indexCount;
				if (flag2)
				{
					flag3 = false;
				}
				else
				{
					for (int i = 0; i < list.Count; i++)
					{
						bool flag4 = list[i] != this.m_Selection.indices[i];
						if (flag4)
						{
							return false;
						}
					}
					flag3 = true;
				}
			}
			finally
			{
				bool flag5 = list2 != null;
				if (flag5)
				{
					CollectionPool<List<int>, int>.Release(list2);
				}
			}
			return flag3;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001E484 File Offset: 0x0001C684
		private void NotifyOfSelectionChange()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				Action<IEnumerable<object>> action = this.selectionChanged;
				if (action != null)
				{
					action(this.selectedItems);
				}
				Action<IEnumerable<int>> action2 = this.selectedIndicesChanged;
				if (action2 != null)
				{
					action2(this.m_Selection.indices);
				}
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001E4D8 File Offset: 0x0001C6D8
		public void ClearSelection()
		{
			bool flag = !this.HasValidDataAndBindings() || this.m_Selection.idCount == 0;
			if (!flag)
			{
				this.ClearSelectionWithoutValidation();
				this.NotifyOfSelectionChange();
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001E514 File Offset: 0x0001C714
		private void ClearSelectionWithoutValidation()
		{
			foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
			{
				reusableCollectionItem.SetSelected(false);
			}
			this.m_Selection.Clear();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001E574 File Offset: 0x0001C774
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.m_ScrollView.UpdateContentViewTransform();
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
		[EventInterest(new Type[]
		{
			typeof(PointerUpEvent),
			typeof(FocusInEvent),
			typeof(FocusOutEvent),
			typeof(NavigationSubmitEvent)
		})]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId();
			if (flag)
			{
				ListViewDragger dragger = this.m_Dragger;
				if (dragger != null)
				{
					dragger.OnPointerUpEvent((PointerUpEvent)evt);
				}
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
				if (flag2)
				{
					CollectionVirtualizationController virtualizationController = this.m_VirtualizationController;
					if (virtualizationController != null)
					{
						virtualizationController.OnFocusIn(evt.elementTarget);
					}
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<FocusOutEvent>.TypeId();
					if (flag3)
					{
						CollectionVirtualizationController virtualizationController2 = this.m_VirtualizationController;
						if (virtualizationController2 != null)
						{
							virtualizationController2.OnFocusOut(((FocusOutEvent)evt).relatedTarget as VisualElement);
						}
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId();
						if (flag4)
						{
							bool flag5 = evt.target == this;
							if (flag5)
							{
								this.m_ScrollView.contentContainer.Focus();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00002152 File Offset: 0x00000352
		[EventInterest(EventInterestOptions.Inherit)]
		[Obsolete("ExecuteDefaultAction override has been removed because default event handling was migrated to HandleEventBubbleUp. Please use HandleEventBubbleUp.", false)]
		protected override void ExecuteDefaultAction(EventBase evt)
		{
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001E688 File Offset: 0x0001C888
		private void OnSizeChanged(GeometryChangedEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = Mathf.Approximately(evt.newRect.width, evt.oldRect.width) && Mathf.Approximately(evt.newRect.height, evt.oldRect.height);
				if (!flag2)
				{
					this.Resize(evt.newRect.size);
				}
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001E708 File Offset: 0x0001C908
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			int num;
			bool flag = !this.m_ItemHeightIsInline && e.customStyle.TryGetValue(BaseVerticalCollectionView.s_ItemHeightProperty, out num);
			if (flag)
			{
				bool flag2 = Math.Abs(this.m_FixedItemHeight - (float)num) > float.Epsilon;
				if (flag2)
				{
					this.m_FixedItemHeight = (float)num;
					this.RefreshItems();
				}
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00002152 File Offset: 0x00000352
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001E763 File Offset: 0x0001C963
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_Selection.selectedIds = this.m_SelectedIds;
			this.RefreshItems();
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001E954 File Offset: 0x0001CB54
		[CompilerGenerated]
		private void <RefreshSelection>g__NotifyIfChanged|193_0(ref BaseVerticalCollectionView.<>c__DisplayClass193_0 A_1)
		{
			bool flag = A_1.selectedIndicesChanged || this.m_Selection.indexCount != A_1.previousSelectionCount;
			if (flag)
			{
				this.NotifyOfSelectionChange();
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001E990 File Offset: 0x0001CB90
		[CompilerGenerated]
		private void <Apply>g__HandleSelectionAndScroll|204_0(int index, ref BaseVerticalCollectionView.<>c__DisplayClass204_0 A_2)
		{
			bool flag = index < 0 || index >= this.m_ViewController.itemsSource.Count;
			if (!flag)
			{
				bool flag2 = ((this.selectionType == SelectionType.Multiple) & A_2.shiftKey) && this.m_Selection.indexCount != 0;
				if (flag2)
				{
					this.DoRangeSelection(index);
				}
				else
				{
					this.selectedIndex = index;
				}
				this.ScrollToItem(index);
			}
		}

		// Token: 0x0400034C RID: 844
		internal static readonly BindingId itemsSourceProperty = "itemsSource";

		// Token: 0x0400034D RID: 845
		internal static readonly BindingId selectionTypeProperty = "selectionType";

		// Token: 0x0400034E RID: 846
		internal static readonly BindingId selectedItemProperty = "selectedItem";

		// Token: 0x0400034F RID: 847
		internal static readonly BindingId selectedItemsProperty = "selectedItems";

		// Token: 0x04000350 RID: 848
		internal static readonly BindingId selectedIndexProperty = "selectedIndex";

		// Token: 0x04000351 RID: 849
		internal static readonly BindingId selectedIndicesProperty = "selectedIndices";

		// Token: 0x04000352 RID: 850
		internal static readonly BindingId showBorderProperty = "showBorder";

		// Token: 0x04000353 RID: 851
		internal static readonly BindingId reorderableProperty = "reorderable";

		// Token: 0x04000354 RID: 852
		internal static readonly BindingId horizontalScrollingEnabledProperty = "horizontalScrollingEnabled";

		// Token: 0x04000355 RID: 853
		internal static readonly BindingId showAlternatingRowBackgroundsProperty = "showAlternatingRowBackgrounds";

		// Token: 0x04000356 RID: 854
		internal static readonly BindingId virtualizationMethodProperty = "virtualizationMethod";

		// Token: 0x04000357 RID: 855
		internal static readonly BindingId fixedItemHeightProperty = "fixedItemHeight";

		// Token: 0x04000358 RID: 856
		internal const string internalBindingKey = "__unity-collection-view-internal-binding";

		// Token: 0x04000359 RID: 857
		private static readonly ProfilerMarker k_RefreshMarker = new ProfilerMarker("BaseVerticalCollectionView.RefreshItems");

		// Token: 0x0400035A RID: 858
		private static readonly ProfilerMarker k_RebuildMarker = new ProfilerMarker("BaseVerticalCollectionView.Rebuild");

		// Token: 0x04000365 RID: 869
		private SelectionType m_SelectionType;

		// Token: 0x04000366 RID: 870
		private static readonly List<ReusableCollectionItem> k_EmptyItems = new List<ReusableCollectionItem>();

		// Token: 0x04000367 RID: 871
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool allowSingleClickChoice = false;

		// Token: 0x04000368 RID: 872
		private bool m_HorizontalScrollingEnabled;

		// Token: 0x04000369 RID: 873
		[SerializeField]
		[DontCreateProperty]
		private AlternatingRowBackground m_ShowAlternatingRowBackgrounds = AlternatingRowBackground.None;

		// Token: 0x0400036A RID: 874
		internal static readonly string k_InvalidTemplateError = "Template Not Found";

		// Token: 0x0400036B RID: 875
		internal const int s_DefaultItemHeight = 22;

		// Token: 0x0400036C RID: 876
		internal float m_FixedItemHeight = 22f;

		// Token: 0x0400036D RID: 877
		internal bool m_ItemHeightIsInline;

		// Token: 0x0400036E RID: 878
		private CollectionVirtualizationMethod m_VirtualizationMethod;

		// Token: 0x0400036F RID: 879
		private readonly ScrollView m_ScrollView;

		// Token: 0x04000370 RID: 880
		private CollectionViewController m_ViewController;

		// Token: 0x04000371 RID: 881
		private CollectionVirtualizationController m_VirtualizationController;

		// Token: 0x04000372 RID: 882
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x04000373 RID: 883
		[DontCreateProperty]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[SerializeField]
		internal SerializedVirtualizationData serializedVirtualizationData = new SerializedVirtualizationData();

		// Token: 0x04000374 RID: 884
		[DontCreateProperty]
		[SerializeField]
		private List<int> m_SelectedIds = new List<int>();

		// Token: 0x04000375 RID: 885
		private readonly BaseVerticalCollectionView.Selection m_Selection;

		// Token: 0x04000376 RID: 886
		private float m_LastHeight;

		// Token: 0x04000377 RID: 887
		private bool m_IsRangeSelectionDirectionUp;

		// Token: 0x04000378 RID: 888
		private ListViewDragger m_Dragger;

		// Token: 0x04000379 RID: 889
		internal const float ItemHeightUnset = -1f;

		// Token: 0x0400037A RID: 890
		internal static CustomStyleProperty<int> s_ItemHeightProperty = new CustomStyleProperty<int>("--unity-item-height");

		// Token: 0x0400037B RID: 891
		private readonly Action<int, int> m_ItemIndexChangedCallback;

		// Token: 0x0400037C RID: 892
		private readonly Action m_ItemsSourceChangedCallback;

		// Token: 0x0400037D RID: 893
		private IVisualElementScheduledItem m_RebuildScheduled;

		// Token: 0x0400037E RID: 894
		public static readonly string ussClassName = "unity-collection-view";

		// Token: 0x0400037F RID: 895
		public static readonly string borderUssClassName = BaseVerticalCollectionView.ussClassName + "--with-border";

		// Token: 0x04000380 RID: 896
		public static readonly string itemUssClassName = BaseVerticalCollectionView.ussClassName + "__item";

		// Token: 0x04000381 RID: 897
		public static readonly string dragHoverBarUssClassName = BaseVerticalCollectionView.ussClassName + "__drag-hover-bar";

		// Token: 0x04000382 RID: 898
		public static readonly string dragHoverMarkerUssClassName = BaseVerticalCollectionView.ussClassName + "__drag-hover-marker";

		// Token: 0x04000383 RID: 899
		public static readonly string itemDragHoverUssClassName = BaseVerticalCollectionView.itemUssClassName + "--drag-hover";

		// Token: 0x04000384 RID: 900
		public static readonly string itemSelectedVariantUssClassName = BaseVerticalCollectionView.itemUssClassName + "--selected";

		// Token: 0x04000385 RID: 901
		public static readonly string itemAlternativeBackgroundUssClassName = BaseVerticalCollectionView.itemUssClassName + "--alternative-background";

		// Token: 0x04000386 RID: 902
		public static readonly string listScrollViewUssClassName = BaseVerticalCollectionView.ussClassName + "__scroll-view";

		// Token: 0x04000387 RID: 903
		internal static readonly string backgroundFillUssClassName = BaseVerticalCollectionView.ussClassName + "__background-fill";

		// Token: 0x04000388 RID: 904
		internal int m_PreviousRefreshedCount;

		// Token: 0x04000389 RID: 905
		private Vector3 m_TouchDownPosition;

		// Token: 0x0400038A RID: 906
		private long m_LastPointerDownTimeStamp;

		// Token: 0x0400038B RID: 907
		private int m_PointerDownCount;

		// Token: 0x0200009F RID: 159
		[ExcludeFromDocs]
		[Serializable]
		public new abstract class UxmlSerializedData : BindableElement.UxmlSerializedData
		{
			// Token: 0x06000696 RID: 1686 RVA: 0x0001EA08 File Offset: 0x0001CC08
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(BaseVerticalCollectionView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("fixedItemHeight", "fixed-item-height", null, new string[] { "itemHeight", "item-height" }),
					new UxmlAttributeNames("virtualizationMethod", "virtualization-method", null, Array.Empty<string>()),
					new UxmlAttributeNames("showBorder", "show-border", null, Array.Empty<string>()),
					new UxmlAttributeNames("selectionType", "selection-type", null, Array.Empty<string>()),
					new UxmlAttributeNames("showAlternatingRowBackgrounds", "show-alternating-row-backgrounds", null, Array.Empty<string>()),
					new UxmlAttributeNames("reorderable", "reorderable", null, Array.Empty<string>()),
					new UxmlAttributeNames("horizontalScrollingEnabled", "horizontal-scrolling", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x0001EB04 File Offset: 0x0001CD04
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				BaseVerticalCollectionView baseVerticalCollectionView = (BaseVerticalCollectionView)obj;
				baseVerticalCollectionView.SetViewController(null);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.fixedItemHeight_UxmlAttributeFlags);
				if (flag)
				{
					baseVerticalCollectionView.fixedItemHeight = this.fixedItemHeight;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.virtualizationMethod_UxmlAttributeFlags);
				if (flag2)
				{
					baseVerticalCollectionView.virtualizationMethod = this.virtualizationMethod;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.showBorder_UxmlAttributeFlags);
				if (flag3)
				{
					baseVerticalCollectionView.showBorder = this.showBorder;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.selectionType_UxmlAttributeFlags);
				if (flag4)
				{
					baseVerticalCollectionView.selectionType = this.selectionType;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.showAlternatingRowBackgrounds_UxmlAttributeFlags);
				if (flag5)
				{
					baseVerticalCollectionView.showAlternatingRowBackgrounds = this.showAlternatingRowBackgrounds;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.reorderable_UxmlAttributeFlags);
				if (flag6)
				{
					baseVerticalCollectionView.reorderable = this.reorderable;
				}
				bool flag7 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.horizontalScrollingEnabled_UxmlAttributeFlags);
				if (flag7)
				{
					baseVerticalCollectionView.horizontalScrollingEnabled = this.horizontalScrollingEnabled;
				}
			}

			// Token: 0x0400038C RID: 908
			[UxmlAttribute(obsoleteNames = new string[] { "itemHeight", "item-height" })]
			[SerializeField]
			[FixedItemHeightDecorator]
			private float fixedItemHeight;

			// Token: 0x0400038D RID: 909
			[SerializeField]
			private CollectionVirtualizationMethod virtualizationMethod;

			// Token: 0x0400038E RID: 910
			[SerializeField]
			private SelectionType selectionType;

			// Token: 0x0400038F RID: 911
			[SerializeField]
			private AlternatingRowBackground showAlternatingRowBackgrounds;

			// Token: 0x04000390 RID: 912
			[SerializeField]
			private bool showBorder;

			// Token: 0x04000391 RID: 913
			[SerializeField]
			private bool reorderable;

			// Token: 0x04000392 RID: 914
			[UxmlAttribute("horizontal-scrolling")]
			[SerializeField]
			private bool horizontalScrollingEnabled;

			// Token: 0x04000393 RID: 915
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags fixedItemHeight_UxmlAttributeFlags;

			// Token: 0x04000394 RID: 916
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags virtualizationMethod_UxmlAttributeFlags;

			// Token: 0x04000395 RID: 917
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags showBorder_UxmlAttributeFlags;

			// Token: 0x04000396 RID: 918
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags selectionType_UxmlAttributeFlags;

			// Token: 0x04000397 RID: 919
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags showAlternatingRowBackgrounds_UxmlAttributeFlags;

			// Token: 0x04000398 RID: 920
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags reorderable_UxmlAttributeFlags;

			// Token: 0x04000399 RID: 921
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags horizontalScrollingEnabled_UxmlAttributeFlags;
		}

		// Token: 0x020000A0 RID: 160
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001EC00 File Offset: 0x0001CE00
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x0001EC20 File Offset: 0x0001CE20
			public UxmlTraits()
			{
				base.focusable.defaultValue = true;
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x0001ED3C File Offset: 0x0001CF3C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int num = 0;
				BaseVerticalCollectionView baseVerticalCollectionView = (BaseVerticalCollectionView)ve;
				baseVerticalCollectionView.reorderable = this.m_Reorderable.GetValueFromBag(bag, cc);
				bool flag = this.m_FixedItemHeight.TryGetValueFromBag(bag, cc, ref num);
				if (flag)
				{
					baseVerticalCollectionView.fixedItemHeight = (float)num;
				}
				baseVerticalCollectionView.virtualizationMethod = this.m_VirtualizationMethod.GetValueFromBag(bag, cc);
				baseVerticalCollectionView.showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				baseVerticalCollectionView.selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				baseVerticalCollectionView.showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
				baseVerticalCollectionView.horizontalScrollingEnabled = this.m_HorizontalScrollingEnabled.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400039A RID: 922
			private readonly UxmlEnumAttributeDescription<CollectionVirtualizationMethod> m_VirtualizationMethod = new UxmlEnumAttributeDescription<CollectionVirtualizationMethod>
			{
				name = "virtualization-method",
				defaultValue = CollectionVirtualizationMethod.FixedHeight
			};

			// Token: 0x0400039B RID: 923
			private readonly UxmlIntAttributeDescription m_FixedItemHeight = new UxmlIntAttributeDescription
			{
				name = "fixed-item-height",
				obsoleteNames = new string[] { "itemHeight", "item-height" },
				defaultValue = 22
			};

			// Token: 0x0400039C RID: 924
			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			// Token: 0x0400039D RID: 925
			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			// Token: 0x0400039E RID: 926
			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};

			// Token: 0x0400039F RID: 927
			private readonly UxmlBoolAttributeDescription m_Reorderable = new UxmlBoolAttributeDescription
			{
				name = "reorderable",
				defaultValue = false
			};

			// Token: 0x040003A0 RID: 928
			private readonly UxmlBoolAttributeDescription m_HorizontalScrollingEnabled = new UxmlBoolAttributeDescription
			{
				name = "horizontal-scrolling",
				defaultValue = false
			};
		}

		// Token: 0x020000A2 RID: 162
		private class Selection
		{
			// Token: 0x17000108 RID: 264
			// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001EE8B File Offset: 0x0001D08B
			// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0001EE93 File Offset: 0x0001D093
			public List<int> selectedIds { get; set; }

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001EE9C File Offset: 0x0001D09C
			public int indexCount
			{
				get
				{
					return this.indices.Count;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001EEA9 File Offset: 0x0001D0A9
			public int idCount
			{
				get
				{
					return this.selectedIds.Count;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001EEB8 File Offset: 0x0001D0B8
			public int minIndex
			{
				get
				{
					bool flag = this.m_MinIndex == -1;
					if (flag)
					{
						this.m_MinIndex = this.indices.Min();
					}
					return this.m_MinIndex;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001EEF0 File Offset: 0x0001D0F0
			public int maxIndex
			{
				get
				{
					bool flag = this.m_MaxIndex == -1;
					if (flag)
					{
						this.m_MaxIndex = this.indices.Max();
					}
					return this.m_MaxIndex;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001EF26 File Offset: 0x0001D126
			// (set) Token: 0x060006AB RID: 1707 RVA: 0x0001EF34 File Offset: 0x0001D134
			public int capacity
			{
				get
				{
					return this.indices.Capacity;
				}
				set
				{
					this.indices.Capacity = value;
					bool flag = this.selectedIds.Capacity < value;
					if (flag)
					{
						this.selectedIds.Capacity = value;
					}
				}
			}

			// Token: 0x060006AC RID: 1708 RVA: 0x0001EF6E File Offset: 0x0001D16E
			public int FirstIndex()
			{
				return (this.indices.Count > 0) ? this.indices[0] : (-1);
			}

			// Token: 0x060006AD RID: 1709 RVA: 0x0001EF90 File Offset: 0x0001D190
			public object FirstObject()
			{
				object obj;
				return this.items.TryGetValue(this.FirstIndex(), out obj) ? obj : null;
			}

			// Token: 0x060006AE RID: 1710 RVA: 0x0001EFB6 File Offset: 0x0001D1B6
			public bool ContainsIndex(int index)
			{
				return this.m_IndexLookup.Contains(index);
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x0001EFC4 File Offset: 0x0001D1C4
			public bool ContainsId(int id)
			{
				return this.m_IdLookup.Contains(id);
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x0001EFD2 File Offset: 0x0001D1D2
			public void AddId(int id)
			{
				this.selectedIds.Add(id);
				this.m_IdLookup.Add(id);
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x0001EFF0 File Offset: 0x0001D1F0
			public void AddIndex(int index, object obj)
			{
				this.m_IndexLookup.Add(index);
				this.indices.Add(index);
				this.items[index] = obj;
				bool flag = index < this.m_MinIndex;
				if (flag)
				{
					this.m_MinIndex = index;
				}
				bool flag2 = index > this.m_MaxIndex;
				if (flag2)
				{
					this.m_MaxIndex = index;
				}
			}

			// Token: 0x060006B2 RID: 1714 RVA: 0x0001F050 File Offset: 0x0001D250
			public bool TryRemove(int index)
			{
				bool flag = !this.m_IndexLookup.Remove(index);
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					int num = this.indices.IndexOf(index);
					bool flag3 = num >= 0;
					if (flag3)
					{
						this.indices.RemoveAt(num);
						this.items.Remove(index);
						bool flag4 = index == this.m_MinIndex;
						if (flag4)
						{
							this.m_MinIndex = -1;
						}
						bool flag5 = index == this.m_MaxIndex;
						if (flag5)
						{
							this.m_MaxIndex = -1;
						}
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x0001F0DA File Offset: 0x0001D2DA
			public void RemoveId(int id)
			{
				this.selectedIds.Remove(id);
				this.m_IdLookup.Remove(id);
			}

			// Token: 0x060006B4 RID: 1716 RVA: 0x0001F0F7 File Offset: 0x0001D2F7
			public void ClearItems()
			{
				this.items.Clear();
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x0001F106 File Offset: 0x0001D306
			public void ClearIds()
			{
				this.m_IdLookup.Clear();
				this.selectedIds.Clear();
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x0001F121 File Offset: 0x0001D321
			public void ClearIndices()
			{
				this.m_IndexLookup.Clear();
				this.indices.Clear();
				this.m_MinIndex = -1;
				this.m_MaxIndex = -1;
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x0001F14A File Offset: 0x0001D34A
			public void Clear()
			{
				this.ClearItems();
				this.ClearIds();
				this.ClearIndices();
			}

			// Token: 0x040003A5 RID: 933
			private readonly HashSet<int> m_IndexLookup = new HashSet<int>();

			// Token: 0x040003A6 RID: 934
			private readonly HashSet<int> m_IdLookup = new HashSet<int>();

			// Token: 0x040003A7 RID: 935
			private int m_MinIndex = -1;

			// Token: 0x040003A8 RID: 936
			private int m_MaxIndex = -1;

			// Token: 0x040003AA RID: 938
			public readonly List<int> indices = new List<int>();

			// Token: 0x040003AB RID: 939
			public readonly Dictionary<int, object> items = new Dictionary<int, object>();
		}
	}
}
