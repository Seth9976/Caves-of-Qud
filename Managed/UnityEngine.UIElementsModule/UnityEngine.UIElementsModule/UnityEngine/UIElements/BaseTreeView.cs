using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000096 RID: 150
	public abstract class BaseTreeView : BaseVerticalCollectionView
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001B4C8 File Offset: 0x000196C8
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0001B4DC File Offset: 0x000196DC
		[CreateProperty(ReadOnly = true)]
		public new IList itemsSource
		{
			get
			{
				BaseTreeViewController viewController = this.viewController;
				return (viewController != null) ? viewController.itemsSource : null;
			}
			internal set
			{
				base.GetOrCreateViewController().itemsSource = value;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060005BF RID: 1471 RVA: 0x0001B4EC File Offset: 0x000196EC
		// (remove) Token: 0x060005C0 RID: 1472 RVA: 0x0001B524 File Offset: 0x00019724
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<TreeViewExpansionChangedArgs> itemExpandedChanged;

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001B559 File Offset: 0x00019759
		public void SetRootItems<T>(IList<TreeViewItemData<T>> rootItems)
		{
			this.SetRootItemsInternal<T>(rootItems);
		}

		// Token: 0x060005C2 RID: 1474
		internal abstract void SetRootItemsInternal<T>(IList<TreeViewItemData<T>> rootItems);

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001B564 File Offset: 0x00019764
		public IEnumerable<int> GetRootIds()
		{
			return this.viewController.GetRootItemIds();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001B584 File Offset: 0x00019784
		public int GetTreeCount()
		{
			return this.viewController.GetTreeItemsCount();
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001B5A1 File Offset: 0x000197A1
		public new BaseTreeViewController viewController
		{
			get
			{
				return base.viewController as BaseTreeViewController;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001B5AE File Offset: 0x000197AE
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableTreeViewItem>();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001B5B8 File Offset: 0x000197B8
		public override void SetViewController(CollectionViewController controller)
		{
			bool flag = this.viewController != null;
			if (flag)
			{
				this.viewController.itemIndexChanged -= this.OnItemIndexChanged;
				this.viewController.itemExpandedChanged -= this.OnItemExpandedChanged;
			}
			base.SetViewController(controller);
			bool flag2 = this.viewController != null;
			if (flag2)
			{
				this.viewController.itemIndexChanged += this.OnItemIndexChanged;
				this.viewController.itemExpandedChanged += this.OnItemExpandedChanged;
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001B64C File Offset: 0x0001984C
		private void OnItemIndexChanged(int srcIndex, int dstIndex)
		{
			base.RefreshItems();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001B656 File Offset: 0x00019856
		private void OnItemExpandedChanged(TreeViewExpansionChangedArgs arg)
		{
			Action<TreeViewExpansionChangedArgs> action = this.itemExpandedChanged;
			if (action != null)
			{
				action(arg);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001B66C File Offset: 0x0001986C
		internal override ICollectionDragAndDropController CreateDragAndDropController()
		{
			return new TreeViewReorderableDragAndDropController(this);
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001B674 File Offset: 0x00019874
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0001B67C File Offset: 0x0001987C
		[CreateProperty]
		public bool autoExpand
		{
			get
			{
				return this.m_AutoExpand;
			}
			set
			{
				bool flag = this.m_AutoExpand == value;
				if (!flag)
				{
					this.m_AutoExpand = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in BaseTreeView.autoExpandProperty);
				}
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001B6B3 File Offset: 0x000198B3
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0001B6BB File Offset: 0x000198BB
		internal List<int> expandedItemIds
		{
			get
			{
				return this.m_ExpandedItemIds;
			}
			set
			{
				this.m_ExpandedItemIds = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001B6C4 File Offset: 0x000198C4
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0001B6CC File Offset: 0x000198CC
		internal float? customIdent { get; private set; }

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001B6D5 File Offset: 0x000198D5
		public BaseTreeView()
			: this(-1)
		{
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001B6E0 File Offset: 0x000198E0
		public BaseTreeView(int itemHeight)
			: base(null, (float)itemHeight)
		{
			this.m_ExpandedItemIds = new List<int>();
			base.AddToClassList(BaseTreeView.ussClassName);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001B718 File Offset: 0x00019918
		public int GetIdForIndex(int index)
		{
			return this.viewController.GetIdForIndex(index);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001B738 File Offset: 0x00019938
		public int GetParentIdForIndex(int index)
		{
			return this.viewController.GetParentId(this.GetIdForIndex(index));
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001B75C File Offset: 0x0001995C
		public IEnumerable<int> GetChildrenIdsForIndex(int index)
		{
			return this.viewController.GetChildrenIdsByIndex(index);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001B77C File Offset: 0x0001997C
		public IEnumerable<TreeViewItemData<T>> GetSelectedItems<T>()
		{
			return this.GetSelectedItemsInternal<T>();
		}

		// Token: 0x060005D7 RID: 1495
		private protected abstract IEnumerable<TreeViewItemData<T>> GetSelectedItemsInternal<T>();

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001B794 File Offset: 0x00019994
		public T GetItemDataForIndex<T>(int index)
		{
			return this.GetItemDataForIndexInternal<T>(index);
		}

		// Token: 0x060005D9 RID: 1497
		private protected abstract T GetItemDataForIndexInternal<T>(int index);

		// Token: 0x060005DA RID: 1498 RVA: 0x0001B7B0 File Offset: 0x000199B0
		public T GetItemDataForId<T>(int id)
		{
			return this.GetItemDataForIdInternal<T>(id);
		}

		// Token: 0x060005DB RID: 1499
		private protected abstract T GetItemDataForIdInternal<T>(int id);

		// Token: 0x060005DC RID: 1500 RVA: 0x0001B7C9 File Offset: 0x000199C9
		public void AddItem<T>(TreeViewItemData<T> item, int parentId = -1, int childIndex = -1, bool rebuildTree = true)
		{
			this.AddItemInternal<T>(item, parentId, childIndex, rebuildTree);
		}

		// Token: 0x060005DD RID: 1501
		private protected abstract void AddItemInternal<T>(TreeViewItemData<T> item, int parentId, int childIndex, bool rebuildTree);

		// Token: 0x060005DE RID: 1502 RVA: 0x0001B7D8 File Offset: 0x000199D8
		public bool TryRemoveItem(int id, bool rebuildTree = true)
		{
			return this.viewController.TryRemoveItem(id, rebuildTree);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001B7F8 File Offset: 0x000199F8
		private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
		{
			float num;
			bool flag = evt.customStyle.TryGetValue(BaseTreeView.s_TreeViewIndentProperty, out num);
			if (flag)
			{
				this.customIdent = new float?(num);
				CollectionVirtualizationController virtualizationController = base.virtualizationController;
				if (virtualizationController != null)
				{
					virtualizationController.Refresh(false);
				}
			}
			else
			{
				this.customIdent = null;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001B854 File Offset: 0x00019A54
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			bool flag = this.viewController != null;
			if (flag)
			{
				this.viewController.OnViewDataReadyUpdateNodes();
				base.RefreshItems();
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001B88C File Offset: 0x00019A8C
		private protected override bool HandleItemNavigation(bool moveIn, bool altPressed)
		{
			int num = 1;
			bool flag = false;
			foreach (int num2 in base.selectedIds)
			{
				int indexForId = this.viewController.GetIndexForId(num2);
				bool flag2 = !this.viewController.HasChildrenByIndex(indexForId);
				if (flag2)
				{
					break;
				}
				bool flag3 = moveIn && !this.IsExpandedByIndex(indexForId);
				if (flag3)
				{
					this.ExpandItemByIndex(indexForId, altPressed);
					flag = true;
				}
				else
				{
					bool flag4 = !moveIn && this.IsExpandedByIndex(indexForId);
					if (flag4)
					{
						this.CollapseItemByIndex(indexForId, altPressed);
						flag = true;
					}
				}
			}
			bool flag5 = flag;
			bool flag6;
			if (flag5)
			{
				flag6 = true;
			}
			else
			{
				bool flag7 = !moveIn;
				if (flag7)
				{
					int idForIndex = this.viewController.GetIdForIndex(base.selectedIndex);
					int parentId = this.viewController.GetParentId(idForIndex);
					bool flag8 = parentId != -1;
					if (flag8)
					{
						this.SetSelectionById(parentId);
						base.ScrollToItemById(parentId);
						return true;
					}
					num = -1;
				}
				int num3 = base.selectedIndex;
				bool flag9;
				do
				{
					num3 += num;
					flag9 = this.viewController.HasChildrenByIndex(num3);
				}
				while (!flag9 && num3 >= 0 && num3 < this.itemsSource.Count);
				bool flag10 = flag9;
				if (flag10)
				{
					base.SetSelection(num3);
					base.ScrollToItem(num3);
					flag6 = true;
				}
				else
				{
					flag6 = false;
				}
			}
			return flag6;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001BA18 File Offset: 0x00019C18
		public void SetSelectionById(int id)
		{
			this.SetSelectionById(new int[] { id });
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001BA2C File Offset: 0x00019C2C
		public void SetSelectionById(IEnumerable<int> ids)
		{
			this.SetSelectionInternalById(ids, true);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001BA38 File Offset: 0x00019C38
		public void SetSelectionByIdWithoutNotify(IEnumerable<int> ids)
		{
			this.SetSelectionInternalById(ids, false);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001BA44 File Offset: 0x00019C44
		internal void SetSelectionInternalById(IEnumerable<int> ids, bool sendNotification)
		{
			bool flag = ids == null;
			if (!flag)
			{
				List<int> list = ids.Select<int, int>((int id) => this.GetItemIndex(id, true)).ToList<int>();
				base.SetSelectionInternal(list, sendNotification);
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001BA80 File Offset: 0x00019C80
		public void AddToSelectionById(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			base.AddToSelection(itemIndex);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001BAA0 File Offset: 0x00019CA0
		public void RemoveFromSelectionById(int id)
		{
			int itemIndex = this.GetItemIndex(id, false);
			base.RemoveFromSelection(itemIndex);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001BAC0 File Offset: 0x00019CC0
		private int GetItemIndex(int id, bool expand = false)
		{
			if (expand)
			{
				int num = this.viewController.GetParentId(id);
				List<int> list = CollectionPool<List<int>, int>.Get();
				this.viewController.GetExpandedItemIds(list);
				while (num != -1)
				{
					bool flag = !list.Contains(num);
					if (flag)
					{
						this.viewController.ExpandItem(num, false, true);
					}
					num = this.viewController.GetParentId(num);
				}
				CollectionPool<List<int>, int>.Release(list);
			}
			return this.viewController.GetIndexForId(id);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001BB4C File Offset: 0x00019D4C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void CopyExpandedStates(int sourceId, int targetId)
		{
			bool flag = this.IsExpanded(sourceId);
			if (flag)
			{
				this.ExpandItem(targetId, false, true);
				bool flag2 = this.viewController.HasChildren(sourceId);
				if (flag2)
				{
					bool flag3 = this.viewController.GetChildrenIds(sourceId).Count<int>() != this.viewController.GetChildrenIds(targetId).Count<int>();
					if (flag3)
					{
						Debug.LogWarning("Source and target hierarchies are not the same");
					}
					else
					{
						for (int i = 0; i < this.viewController.GetChildrenIds(sourceId).Count<int>(); i++)
						{
							int num = this.viewController.GetChildrenIds(sourceId).ElementAt<int>(i);
							int num2 = this.viewController.GetChildrenIds(targetId).ElementAt<int>(i);
							this.CopyExpandedStates(num, num2);
						}
					}
				}
			}
			else
			{
				this.CollapseItem(targetId, false, true);
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001BC28 File Offset: 0x00019E28
		public bool IsExpanded(int id)
		{
			return this.viewController.IsExpanded(id);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001BC46 File Offset: 0x00019E46
		public void CollapseItem(int id, bool collapseAllChildren = false, bool refresh = true)
		{
			this.viewController.CollapseItem(id, collapseAllChildren, refresh);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001BC58 File Offset: 0x00019E58
		public void ExpandItem(int id, bool expandAllChildren = false, bool refresh = true)
		{
			this.viewController.ExpandItem(id, expandAllChildren, refresh);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001BC6C File Offset: 0x00019E6C
		public void ExpandRootItems()
		{
			foreach (int num in this.viewController.GetRootItemIds())
			{
				this.viewController.ExpandItem(num, false, false);
			}
			base.RefreshItems();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		public void ExpandAll()
		{
			this.viewController.ExpandAll();
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001BCDF File Offset: 0x00019EDF
		public void CollapseAll()
		{
			this.viewController.CollapseAll();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001BCF0 File Offset: 0x00019EF0
		private bool IsExpandedByIndex(int index)
		{
			return this.viewController.IsExpandedByIndex(index);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001BD10 File Offset: 0x00019F10
		private void CollapseItemByIndex(int index, bool collapseAll)
		{
			bool flag = !this.viewController.HasChildrenByIndex(index);
			if (!flag)
			{
				this.viewController.CollapseItemByIndex(index, collapseAll, true);
				base.RefreshItems();
				base.SaveViewData();
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001BD50 File Offset: 0x00019F50
		private void ExpandItemByIndex(int index, bool expandAll)
		{
			bool flag = !this.viewController.HasChildrenByIndex(index);
			if (!flag)
			{
				this.viewController.ExpandItemByIndex(index, expandAll, true);
				base.RefreshItems();
				base.SaveViewData();
			}
		}

		// Token: 0x04000328 RID: 808
		internal static readonly BindingId autoExpandProperty = "autoExpand";

		// Token: 0x04000329 RID: 809
		internal static CustomStyleProperty<float> s_TreeViewIndentProperty = new CustomStyleProperty<float>("--unity-tree-view-indent");

		// Token: 0x0400032A RID: 810
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static readonly int invalidId = -1;

		// Token: 0x0400032B RID: 811
		public new static readonly string ussClassName = "unity-tree-view";

		// Token: 0x0400032C RID: 812
		public new static readonly string itemUssClassName = BaseTreeView.ussClassName + "__item";

		// Token: 0x0400032D RID: 813
		public static readonly string itemToggleUssClassName = BaseTreeView.ussClassName + "__item-toggle";

		// Token: 0x0400032E RID: 814
		[Obsolete("Individual item indents are no longer used, see itemIndentUssClassName instead", false)]
		public static readonly string itemIndentsContainerUssClassName = BaseTreeView.ussClassName + "__item-indents";

		// Token: 0x0400032F RID: 815
		public static readonly string itemIndentUssClassName = BaseTreeView.ussClassName + "__item-indent";

		// Token: 0x04000330 RID: 816
		public static readonly string itemContentContainerUssClassName = BaseTreeView.ussClassName + "__item-content";

		// Token: 0x04000332 RID: 818
		private bool m_AutoExpand;

		// Token: 0x04000333 RID: 819
		[SerializeField]
		[DontCreateProperty]
		private List<int> m_ExpandedItemIds;

		// Token: 0x02000097 RID: 151
		[ExcludeFromDocs]
		[Serializable]
		public new abstract class UxmlSerializedData : BaseVerticalCollectionView.UxmlSerializedData
		{
			// Token: 0x060005F5 RID: 1525 RVA: 0x0001BE39 File Offset: 0x0001A039
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(BaseTreeView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("autoExpand", "auto-expand", null, Array.Empty<string>())
				});
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x0001BE70 File Offset: 0x0001A070
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.autoExpand_UxmlAttributeFlags);
				if (flag)
				{
					BaseTreeView baseTreeView = (BaseTreeView)obj;
					baseTreeView.autoExpand = this.autoExpand;
				}
			}

			// Token: 0x04000335 RID: 821
			[SerializeField]
			private bool autoExpand;

			// Token: 0x04000336 RID: 822
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags autoExpand_UxmlAttributeFlags;
		}

		// Token: 0x02000098 RID: 152
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseVerticalCollectionView.UxmlTraits
		{
			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001BEAC File Offset: 0x0001A0AC
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x060005F9 RID: 1529 RVA: 0x0001BECC File Offset: 0x0001A0CC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				BaseTreeView baseTreeView = (BaseTreeView)ve;
				baseTreeView.autoExpand = this.m_AutoExpand.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000337 RID: 823
			private readonly UxmlBoolAttributeDescription m_AutoExpand = new UxmlBoolAttributeDescription
			{
				name = "auto-expand",
				defaultValue = false
			};
		}
	}
}
