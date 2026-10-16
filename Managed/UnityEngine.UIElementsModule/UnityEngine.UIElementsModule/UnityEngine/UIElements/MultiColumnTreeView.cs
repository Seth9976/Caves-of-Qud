using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000158 RID: 344
	public class MultiColumnTreeView : BaseTreeView
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00031E60 File Offset: 0x00030060
		public new MultiColumnTreeViewController viewController
		{
			get
			{
				return base.viewController as MultiColumnTreeViewController;
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000B37 RID: 2871 RVA: 0x00031E70 File Offset: 0x00030070
		// (remove) Token: 0x06000B38 RID: 2872 RVA: 0x00031EA8 File Offset: 0x000300A8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action columnSortingChanged;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000B39 RID: 2873 RVA: 0x00031EE0 File Offset: 0x000300E0
		// (remove) Token: 0x06000B3A RID: 2874 RVA: 0x00031F18 File Offset: 0x00030118
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ContextualMenuPopulateEvent, Column> headerContextMenuPopulateEvent;

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00031F4D File Offset: 0x0003014D
		public IEnumerable<SortColumnDescription> sortedColumns
		{
			get
			{
				return this.m_SortedColumns;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00031F55 File Offset: 0x00030155
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x00031F60 File Offset: 0x00030160
		[CreateProperty]
		public Columns columns
		{
			get
			{
				return this.m_Columns;
			}
			private set
			{
				bool flag = this.m_Columns != null;
				if (flag)
				{
					this.m_Columns.propertyChanged -= this.ColumnsChanged;
				}
				bool flag2 = value == null;
				if (flag2)
				{
					this.m_Columns.Clear();
				}
				else
				{
					this.m_Columns = value;
					this.m_Columns.propertyChanged += this.ColumnsChanged;
					bool flag3 = this.m_Columns.Count > 0;
					if (flag3)
					{
						base.GetOrCreateViewController();
					}
					base.NotifyPropertyChanged(in MultiColumnTreeView.columnsProperty);
				}
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00031FF0 File Offset: 0x000301F0
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00031FF8 File Offset: 0x000301F8
		[CreateProperty]
		public SortColumnDescriptions sortColumnDescriptions
		{
			get
			{
				return this.m_SortColumnDescriptions;
			}
			private set
			{
				bool flag = value == null;
				if (flag)
				{
					this.m_SortColumnDescriptions.Clear();
				}
				else
				{
					this.m_SortColumnDescriptions = value;
					bool flag2 = this.viewController != null;
					if (flag2)
					{
						this.viewController.columnController.header.sortDescriptions = value;
						this.RaiseColumnSortingChanged();
					}
					base.NotifyPropertyChanged(in MultiColumnTreeView.sortColumnDescriptionsProperty);
				}
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0003205D File Offset: 0x0003025D
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00032068 File Offset: 0x00030268
		[Obsolete("sortingEnabled has been deprecated. Use sortingMode instead.", false)]
		public bool sortingEnabled
		{
			get
			{
				return this.sortingMode == ColumnSortingMode.Custom;
			}
			set
			{
				this.sortingMode = (value ? ColumnSortingMode.Custom : ColumnSortingMode.None);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00032078 File Offset: 0x00030278
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x00032080 File Offset: 0x00030280
		[CreateProperty]
		public ColumnSortingMode sortingMode
		{
			get
			{
				return this.m_SortingMode;
			}
			set
			{
				bool flag = this.sortingMode == value;
				if (!flag)
				{
					this.m_SortingMode = value;
					bool flag2 = this.viewController != null;
					if (flag2)
					{
						this.viewController.columnController.sortingMode = value;
					}
					base.NotifyPropertyChanged(in MultiColumnTreeView.sortingModeProperty);
				}
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000320D1 File Offset: 0x000302D1
		public MultiColumnTreeView()
			: this(new Columns())
		{
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000320E0 File Offset: 0x000302E0
		public MultiColumnTreeView(Columns columns)
		{
			base.scrollView.viewDataKey = "unity-multi-column-scroll-view";
			this.columns = columns ?? new Columns();
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0003212D File Offset: 0x0003032D
		internal override void SetRootItemsInternal<T>(IList<TreeViewItemData<T>> rootItems)
		{
			TreeViewHelpers<T, DefaultMultiColumnTreeViewController<T>>.SetRootItems(this, rootItems, () => new DefaultMultiColumnTreeViewController<T>(this.columns, this.m_SortColumnDescriptions, this.m_SortedColumns));
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00032144 File Offset: 0x00030344
		private protected override IEnumerable<TreeViewItemData<T>> GetSelectedItemsInternal<T>()
		{
			return TreeViewHelpers<T, DefaultMultiColumnTreeViewController<T>>.GetSelectedItems(this);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0003215C File Offset: 0x0003035C
		private protected override T GetItemDataForIndexInternal<T>(int index)
		{
			return TreeViewHelpers<T, DefaultMultiColumnTreeViewController<T>>.GetItemDataForIndex(this, index);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00032178 File Offset: 0x00030378
		private protected override T GetItemDataForIdInternal<T>(int id)
		{
			return TreeViewHelpers<T, DefaultMultiColumnTreeViewController<T>>.GetItemDataForId(this, id);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00032191 File Offset: 0x00030391
		private protected override void AddItemInternal<T>(TreeViewItemData<T> item, int parentId, int childIndex, bool rebuildTree)
		{
			TreeViewHelpers<T, DefaultMultiColumnTreeViewController<T>>.AddItem(this, item, parentId, childIndex, rebuildTree);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000321A0 File Offset: 0x000303A0
		protected override CollectionViewController CreateViewController()
		{
			return new DefaultMultiColumnTreeViewController<object>(this.columns, this.sortColumnDescriptions, this.m_SortedColumns);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000321BC File Offset: 0x000303BC
		public override void SetViewController(CollectionViewController controller)
		{
			bool flag = this.viewController != null;
			if (flag)
			{
				this.viewController.columnController.columnSortingChanged -= this.RaiseColumnSortingChanged;
				this.viewController.columnController.headerContextMenuPopulateEvent -= this.RaiseHeaderContextMenuPopulate;
			}
			base.SetViewController(controller);
			bool flag2 = this.viewController != null;
			if (flag2)
			{
				this.viewController.columnController.sortingMode = this.m_SortingMode;
				this.viewController.columnController.columnSortingChanged += this.RaiseColumnSortingChanged;
				this.viewController.columnController.headerContextMenuPopulateEvent += this.RaiseHeaderContextMenuPopulate;
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0003227B File Offset: 0x0003047B
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableMultiColumnTreeViewItem>();
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00032285 File Offset: 0x00030485
		private void RaiseColumnSortingChanged()
		{
			Action action = this.columnSortingChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0003229C File Offset: 0x0003049C
		private void ColumnsChanged(object sender, BindablePropertyChangedEventArgs args)
		{
			BindingId propertyName = args.propertyName;
			base.NotifyPropertyChanged(in propertyName);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000322BB File Offset: 0x000304BB
		private void RaiseHeaderContextMenuPopulate(ContextualMenuPopulateEvent evt, Column column)
		{
			Action<ContextualMenuPopulateEvent, Column> action = this.headerContextMenuPopulateEvent;
			if (action != null)
			{
				action(evt, column);
			}
		}

		// Token: 0x0400069D RID: 1693
		private static readonly BindingId columnsProperty = "columns";

		// Token: 0x0400069E RID: 1694
		private static readonly BindingId sortColumnDescriptionsProperty = "sortColumnDescriptions";

		// Token: 0x0400069F RID: 1695
		private static readonly BindingId sortingModeProperty = "sortingMode";

		// Token: 0x040006A0 RID: 1696
		private Columns m_Columns;

		// Token: 0x040006A1 RID: 1697
		private ColumnSortingMode m_SortingMode;

		// Token: 0x040006A2 RID: 1698
		private SortColumnDescriptions m_SortColumnDescriptions = new SortColumnDescriptions();

		// Token: 0x040006A3 RID: 1699
		private List<SortColumnDescription> m_SortedColumns = new List<SortColumnDescription>();

		// Token: 0x02000159 RID: 345
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseTreeView.UxmlSerializedData
		{
			// Token: 0x06000B53 RID: 2899 RVA: 0x0003231C File Offset: 0x0003051C
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(MultiColumnTreeView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("sortingEnabled", "sorting-enabled", null, Array.Empty<string>()),
					new UxmlAttributeNames("sortingMode", "sorting-mode", null, Array.Empty<string>()),
					new UxmlAttributeNames("columns", "columns", null, Array.Empty<string>()),
					new UxmlAttributeNames("sortColumnDescriptions", "sort-column-descriptions", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000B54 RID: 2900 RVA: 0x000323B0 File Offset: 0x000305B0
			public override object CreateInstance()
			{
				return new MultiColumnTreeView();
			}

			// Token: 0x06000B55 RID: 2901 RVA: 0x000323B8 File Offset: 0x000305B8
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				MultiColumnTreeView multiColumnTreeView = (MultiColumnTreeView)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.sortingMode_UxmlAttributeFlags);
				if (flag)
				{
					multiColumnTreeView.sortingMode = this.sortingMode;
				}
				else
				{
					bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.sortingEnabled_UxmlAttributeFlags);
					if (flag2)
					{
						multiColumnTreeView.sortingEnabled = this.sortingEnabled;
					}
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.sortColumnDescriptions_UxmlAttributeFlags) && this.sortColumnDescriptions != null;
				if (flag3)
				{
					SortColumnDescriptions sortColumnDescriptions = new SortColumnDescriptions();
					this.sortColumnDescriptions.Deserialize(sortColumnDescriptions);
					multiColumnTreeView.sortColumnDescriptions = sortColumnDescriptions;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.columns_UxmlAttributeFlags) && this.columns != null;
				if (flag4)
				{
					Columns columns = new Columns();
					this.columns.Deserialize(columns);
					multiColumnTreeView.columns = columns;
				}
			}

			// Token: 0x040006A6 RID: 1702
			[SerializeField]
			[HideInInspector]
			private bool sortingEnabled;

			// Token: 0x040006A7 RID: 1703
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags sortingEnabled_UxmlAttributeFlags;

			// Token: 0x040006A8 RID: 1704
			[SerializeField]
			private ColumnSortingMode sortingMode;

			// Token: 0x040006A9 RID: 1705
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags sortingMode_UxmlAttributeFlags;

			// Token: 0x040006AA RID: 1706
			[SerializeReference]
			[UxmlObjectReference]
			private Columns.UxmlSerializedData columns;

			// Token: 0x040006AB RID: 1707
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags columns_UxmlAttributeFlags;

			// Token: 0x040006AC RID: 1708
			[SerializeReference]
			[UxmlObjectReference]
			private SortColumnDescriptions.UxmlSerializedData sortColumnDescriptions;

			// Token: 0x040006AD RID: 1709
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags sortColumnDescriptions_UxmlAttributeFlags;
		}

		// Token: 0x0200015A RID: 346
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<MultiColumnTreeView, MultiColumnTreeView.UxmlTraits>
		{
		}

		// Token: 0x0200015B RID: 347
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseTreeView.UxmlTraits
		{
			// Token: 0x06000B58 RID: 2904 RVA: 0x000324A0 File Offset: 0x000306A0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				MultiColumnTreeView multiColumnTreeView = (MultiColumnTreeView)ve;
				string text;
				bool flag = this.m_SortingMode.TryGetValueFromBagAsString(bag, cc, out text);
				if (flag)
				{
					bool flag3;
					bool flag2 = bool.TryParse(text, out flag3);
					if (flag2)
					{
						multiColumnTreeView.sortingMode = (flag3 ? ColumnSortingMode.Custom : ColumnSortingMode.None);
					}
					else
					{
						multiColumnTreeView.sortingMode = this.m_SortingMode.GetValueFromBag(bag, cc);
					}
				}
				multiColumnTreeView.sortColumnDescriptions = this.m_SortColumnDescriptions.GetValueFromBag(bag, cc);
				multiColumnTreeView.columns = this.m_Columns.GetValueFromBag(bag, cc);
			}

			// Token: 0x040006AE RID: 1710
			private readonly UxmlEnumAttributeDescription<ColumnSortingMode> m_SortingMode = new UxmlEnumAttributeDescription<ColumnSortingMode>
			{
				name = "sorting-mode",
				obsoleteNames = new string[] { "sorting-enabled" }
			};

			// Token: 0x040006AF RID: 1711
			private readonly UxmlObjectAttributeDescription<Columns> m_Columns = new UxmlObjectAttributeDescription<Columns>();

			// Token: 0x040006B0 RID: 1712
			private readonly UxmlObjectAttributeDescription<SortColumnDescriptions> m_SortColumnDescriptions = new UxmlObjectAttributeDescription<SortColumnDescriptions>();
		}
	}
}
