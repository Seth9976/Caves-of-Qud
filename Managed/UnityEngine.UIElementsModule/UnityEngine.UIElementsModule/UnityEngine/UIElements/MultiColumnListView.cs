using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000154 RID: 340
	public class MultiColumnListView : BaseListView
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x000317C9 File Offset: 0x0002F9C9
		public new MultiColumnListViewController viewController
		{
			get
			{
				return base.viewController as MultiColumnListViewController;
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000B19 RID: 2841 RVA: 0x000317D8 File Offset: 0x0002F9D8
		// (remove) Token: 0x06000B1A RID: 2842 RVA: 0x00031810 File Offset: 0x0002FA10
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action columnSortingChanged;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000B1B RID: 2843 RVA: 0x00031848 File Offset: 0x0002FA48
		// (remove) Token: 0x06000B1C RID: 2844 RVA: 0x00031880 File Offset: 0x0002FA80
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ContextualMenuPopulateEvent, Column> headerContextMenuPopulateEvent;

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000318B5 File Offset: 0x0002FAB5
		public IEnumerable<SortColumnDescription> sortedColumns
		{
			get
			{
				return this.m_SortedColumns;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x000318BD File Offset: 0x0002FABD
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x000318C8 File Offset: 0x0002FAC8
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
					base.NotifyPropertyChanged(in MultiColumnListView.columnsProperty);
				}
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00031958 File Offset: 0x0002FB58
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x00031960 File Offset: 0x0002FB60
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
					base.NotifyPropertyChanged(in MultiColumnListView.sortColumnDescriptionsProperty);
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x000319C5 File Offset: 0x0002FBC5
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x000319D0 File Offset: 0x0002FBD0
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

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x000319E0 File Offset: 0x0002FBE0
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x000319E8 File Offset: 0x0002FBE8
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
					base.NotifyPropertyChanged(in MultiColumnListView.sortingModeProperty);
				}
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00031A39 File Offset: 0x0002FC39
		public MultiColumnListView()
			: this(new Columns())
		{
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00031A48 File Offset: 0x0002FC48
		public MultiColumnListView(Columns columns)
		{
			base.scrollView.viewDataKey = "unity-multi-column-scroll-view";
			this.columns = columns ?? new Columns();
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00031A95 File Offset: 0x0002FC95
		protected override CollectionViewController CreateViewController()
		{
			return new MultiColumnListViewController(this.columns, this.sortColumnDescriptions, this.m_SortedColumns);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00031AB0 File Offset: 0x0002FCB0
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

		// Token: 0x06000B2A RID: 2858 RVA: 0x00031B6F File Offset: 0x0002FD6F
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableMultiColumnListViewItem>();
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00031B79 File Offset: 0x0002FD79
		private void RaiseColumnSortingChanged()
		{
			Action action = this.columnSortingChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00031B90 File Offset: 0x0002FD90
		private void ColumnsChanged(object sender, BindablePropertyChangedEventArgs args)
		{
			BindingId propertyName = args.propertyName;
			base.NotifyPropertyChanged(in propertyName);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00031BAF File Offset: 0x0002FDAF
		private void RaiseHeaderContextMenuPopulate(ContextualMenuPopulateEvent evt, Column column)
		{
			Action<ContextualMenuPopulateEvent, Column> action = this.headerContextMenuPopulateEvent;
			if (action != null)
			{
				action(evt, column);
			}
		}

		// Token: 0x04000689 RID: 1673
		private static readonly BindingId columnsProperty = "columns";

		// Token: 0x0400068A RID: 1674
		private static readonly BindingId sortColumnDescriptionsProperty = "sortColumnDescriptions";

		// Token: 0x0400068B RID: 1675
		private static readonly BindingId sortingModeProperty = "sortingMode";

		// Token: 0x0400068C RID: 1676
		private Columns m_Columns;

		// Token: 0x0400068D RID: 1677
		private ColumnSortingMode m_SortingMode;

		// Token: 0x0400068E RID: 1678
		private SortColumnDescriptions m_SortColumnDescriptions = new SortColumnDescriptions();

		// Token: 0x0400068F RID: 1679
		private List<SortColumnDescription> m_SortedColumns = new List<SortColumnDescription>();

		// Token: 0x02000155 RID: 341
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseListView.UxmlSerializedData
		{
			// Token: 0x06000B2F RID: 2863 RVA: 0x00031BF8 File Offset: 0x0002FDF8
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(MultiColumnListView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("sortingEnabled", "sorting-enabled", null, Array.Empty<string>()),
					new UxmlAttributeNames("sortingMode", "sorting-mode", null, Array.Empty<string>()),
					new UxmlAttributeNames("columns", "columns", null, Array.Empty<string>()),
					new UxmlAttributeNames("sortColumnDescriptions", "sort-column-descriptions", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000B30 RID: 2864 RVA: 0x00031C8C File Offset: 0x0002FE8C
			public override object CreateInstance()
			{
				return new MultiColumnListView();
			}

			// Token: 0x06000B31 RID: 2865 RVA: 0x00031C94 File Offset: 0x0002FE94
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				MultiColumnListView multiColumnListView = (MultiColumnListView)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.sortingMode_UxmlAttributeFlags);
				if (flag)
				{
					multiColumnListView.sortingMode = this.sortingMode;
				}
				else
				{
					bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.sortingEnabled_UxmlAttributeFlags);
					if (flag2)
					{
						multiColumnListView.sortingEnabled = this.sortingEnabled;
					}
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.sortColumnDescriptions_UxmlAttributeFlags) && this.sortColumnDescriptions != null;
				if (flag3)
				{
					SortColumnDescriptions sortColumnDescriptions = new SortColumnDescriptions();
					this.sortColumnDescriptions.Deserialize(sortColumnDescriptions);
					multiColumnListView.sortColumnDescriptions = sortColumnDescriptions;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.columns_UxmlAttributeFlags) && this.columns != null;
				if (flag4)
				{
					Columns columns = new Columns();
					this.columns.Deserialize(columns);
					multiColumnListView.columns = columns;
				}
			}

			// Token: 0x04000692 RID: 1682
			[UxmlObjectReference]
			[SerializeReference]
			private Columns.UxmlSerializedData columns;

			// Token: 0x04000693 RID: 1683
			[UxmlObjectReference]
			[SerializeReference]
			private SortColumnDescriptions.UxmlSerializedData sortColumnDescriptions;

			// Token: 0x04000694 RID: 1684
			[SerializeField]
			private ColumnSortingMode sortingMode;

			// Token: 0x04000695 RID: 1685
			[HideInInspector]
			[SerializeField]
			private bool sortingEnabled;

			// Token: 0x04000696 RID: 1686
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags sortingEnabled_UxmlAttributeFlags;

			// Token: 0x04000697 RID: 1687
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags sortingMode_UxmlAttributeFlags;

			// Token: 0x04000698 RID: 1688
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags columns_UxmlAttributeFlags;

			// Token: 0x04000699 RID: 1689
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags sortColumnDescriptions_UxmlAttributeFlags;
		}

		// Token: 0x02000156 RID: 342
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<MultiColumnListView, MultiColumnListView.UxmlTraits>
		{
		}

		// Token: 0x02000157 RID: 343
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseListView.UxmlTraits
		{
			// Token: 0x06000B34 RID: 2868 RVA: 0x00031D74 File Offset: 0x0002FF74
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				MultiColumnListView multiColumnListView = (MultiColumnListView)ve;
				string text;
				bool flag = this.m_SortingMode.TryGetValueFromBagAsString(bag, cc, out text);
				if (flag)
				{
					bool flag3;
					bool flag2 = bool.TryParse(text, out flag3);
					if (flag2)
					{
						multiColumnListView.sortingMode = (flag3 ? ColumnSortingMode.Custom : ColumnSortingMode.None);
					}
					else
					{
						multiColumnListView.sortingMode = this.m_SortingMode.GetValueFromBag(bag, cc);
					}
				}
				multiColumnListView.sortColumnDescriptions = this.m_SortColumnDescriptions.GetValueFromBag(bag, cc);
				multiColumnListView.columns = this.m_Columns.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400069A RID: 1690
			private readonly UxmlEnumAttributeDescription<ColumnSortingMode> m_SortingMode = new UxmlEnumAttributeDescription<ColumnSortingMode>
			{
				name = "sorting-mode",
				obsoleteNames = new string[] { "sorting-enabled" }
			};

			// Token: 0x0400069B RID: 1691
			private readonly UxmlObjectAttributeDescription<Columns> m_Columns = new UxmlObjectAttributeDescription<Columns>();

			// Token: 0x0400069C RID: 1692
			private readonly UxmlObjectAttributeDescription<SortColumnDescriptions> m_SortColumnDescriptions = new UxmlObjectAttributeDescription<SortColumnDescriptions>();
		}
	}
}
