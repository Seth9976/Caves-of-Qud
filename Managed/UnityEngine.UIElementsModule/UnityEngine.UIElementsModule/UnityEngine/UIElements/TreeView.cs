using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B7 RID: 439
	public class TreeView : BaseTreeView
	{
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0003D6DA File Offset: 0x0003B8DA
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x0003D6E4 File Offset: 0x0003B8E4
		[CreateProperty]
		public new Func<VisualElement> makeItem
		{
			get
			{
				return this.m_MakeItem;
			}
			set
			{
				bool flag = value != this.m_MakeItem;
				if (flag)
				{
					this.m_MakeItem = value;
					base.Rebuild();
					base.NotifyPropertyChanged(in TreeView.makeItemProperty);
				}
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0003D71E File Offset: 0x0003B91E
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x0003D728 File Offset: 0x0003B928
		[CreateProperty]
		public VisualTreeAsset itemTemplate
		{
			get
			{
				return this.m_ItemTemplate;
			}
			set
			{
				bool flag = this.m_ItemTemplate == value;
				if (!flag)
				{
					this.m_ItemTemplate = value;
					bool flag2 = this.makeItem != this.m_TemplateMakeItem;
					if (flag2)
					{
						this.makeItem = this.m_TemplateMakeItem;
					}
					else
					{
						base.Rebuild();
					}
					base.NotifyPropertyChanged(in TreeView.itemTemplateProperty);
				}
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003D788 File Offset: 0x0003B988
		private VisualElement TemplateMakeItem()
		{
			bool flag = this.m_ItemTemplate != null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.m_ItemTemplate.Instantiate();
			}
			else
			{
				visualElement = new Label(BaseVerticalCollectionView.k_InvalidTemplateError);
			}
			return visualElement;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0003D7C3 File Offset: 0x0003B9C3
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0003D7CC File Offset: 0x0003B9CC
		[CreateProperty]
		public new Action<VisualElement, int> bindItem
		{
			get
			{
				return this.m_BindItem;
			}
			set
			{
				bool flag = value != this.m_BindItem;
				if (flag)
				{
					this.m_BindItem = value;
					base.RefreshItems();
					base.NotifyPropertyChanged(in TreeView.bindItemProperty);
				}
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0003D806 File Offset: 0x0003BA06
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0003D810 File Offset: 0x0003BA10
		[CreateProperty]
		public new Action<VisualElement, int> unbindItem
		{
			get
			{
				return this.m_UnbindItem;
			}
			set
			{
				bool flag = value != this.m_UnbindItem;
				if (flag)
				{
					this.m_UnbindItem = value;
					base.NotifyPropertyChanged(in TreeView.unbindItemProperty);
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0003D843 File Offset: 0x0003BA43
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x0003D84C File Offset: 0x0003BA4C
		[CreateProperty]
		public new Action<VisualElement> destroyItem
		{
			get
			{
				return this.m_DestroyItem;
			}
			set
			{
				bool flag = value != this.m_DestroyItem;
				if (flag)
				{
					this.m_DestroyItem = value;
					base.NotifyPropertyChanged(in TreeView.destroyItemProperty);
				}
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003D87F File Offset: 0x0003BA7F
		internal override void SetRootItemsInternal<T>(IList<TreeViewItemData<T>> rootItems)
		{
			TreeViewHelpers<T, DefaultTreeViewController<T>>.SetRootItems(this, rootItems, () => new DefaultTreeViewController<T>());
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003D8AC File Offset: 0x0003BAAC
		internal override bool HasValidDataAndBindings()
		{
			return base.HasValidDataAndBindings() && this.makeItem != null == (this.bindItem != null);
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0003D8DD File Offset: 0x0003BADD
		public new TreeViewController viewController
		{
			get
			{
				return base.viewController as TreeViewController;
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003D8EA File Offset: 0x0003BAEA
		protected override CollectionViewController CreateViewController()
		{
			return new DefaultTreeViewController<object>();
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003D8F1 File Offset: 0x0003BAF1
		public TreeView()
			: this(null, null)
		{
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003D8FD File Offset: 0x0003BAFD
		public TreeView(Func<VisualElement> makeItem, Action<VisualElement, int> bindItem)
			: base(-1)
		{
			this.makeItem = makeItem;
			this.bindItem = bindItem;
			this.m_TemplateMakeItem = new Func<VisualElement>(this.TemplateMakeItem);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0003D92A File Offset: 0x0003BB2A
		public TreeView(int itemHeight, Func<VisualElement> makeItem, Action<VisualElement, int> bindItem)
			: this(makeItem, bindItem)
		{
			base.fixedItemHeight = (float)itemHeight;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0003D940 File Offset: 0x0003BB40
		private protected override IEnumerable<TreeViewItemData<T>> GetSelectedItemsInternal<T>()
		{
			return TreeViewHelpers<T, DefaultTreeViewController<T>>.GetSelectedItems(this);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003D958 File Offset: 0x0003BB58
		private protected override T GetItemDataForIndexInternal<T>(int index)
		{
			return TreeViewHelpers<T, DefaultTreeViewController<T>>.GetItemDataForIndex(this, index);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003D974 File Offset: 0x0003BB74
		private protected override T GetItemDataForIdInternal<T>(int id)
		{
			return TreeViewHelpers<T, DefaultTreeViewController<T>>.GetItemDataForId(this, id);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003D98D File Offset: 0x0003BB8D
		private protected override void AddItemInternal<T>(TreeViewItemData<T> item, int parentId, int childIndex, bool rebuildTree)
		{
			TreeViewHelpers<T, DefaultTreeViewController<T>>.AddItem(this, item, parentId, childIndex, rebuildTree);
		}

		// Token: 0x0400088F RID: 2191
		internal static readonly BindingId itemTemplateProperty = "itemTemplate";

		// Token: 0x04000890 RID: 2192
		internal static readonly BindingId makeItemProperty = "makeItem";

		// Token: 0x04000891 RID: 2193
		internal static readonly BindingId bindItemProperty = "bindItem";

		// Token: 0x04000892 RID: 2194
		internal static readonly BindingId unbindItemProperty = "unbindItem";

		// Token: 0x04000893 RID: 2195
		internal static readonly BindingId destroyItemProperty = "destroyItem";

		// Token: 0x04000894 RID: 2196
		private Func<VisualElement> m_MakeItem;

		// Token: 0x04000895 RID: 2197
		private Func<VisualElement> m_TemplateMakeItem;

		// Token: 0x04000896 RID: 2198
		private VisualTreeAsset m_ItemTemplate;

		// Token: 0x04000897 RID: 2199
		private Action<VisualElement, int> m_BindItem;

		// Token: 0x04000898 RID: 2200
		private Action<VisualElement, int> m_UnbindItem;

		// Token: 0x04000899 RID: 2201
		private Action<VisualElement> m_DestroyItem;

		// Token: 0x020001B8 RID: 440
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseTreeView.UxmlSerializedData
		{
			// Token: 0x06000DC1 RID: 3521 RVA: 0x0003D9F4 File Offset: 0x0003BBF4
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(TreeView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("itemTemplate", "item-template", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000DC2 RID: 3522 RVA: 0x0003DA29 File Offset: 0x0003BC29
			public override object CreateInstance()
			{
				return new TreeView();
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x0003DA30 File Offset: 0x0003BC30
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.itemTemplate_UxmlAttributeFlags);
				if (flag)
				{
					TreeView treeView = (TreeView)obj;
					treeView.itemTemplate = this.itemTemplate;
				}
			}

			// Token: 0x0400089A RID: 2202
			[SerializeField]
			private VisualTreeAsset itemTemplate;

			// Token: 0x0400089B RID: 2203
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags itemTemplate_UxmlAttributeFlags;
		}

		// Token: 0x020001B9 RID: 441
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TreeView, TreeView.UxmlTraits>
		{
		}

		// Token: 0x020001BA RID: 442
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseTreeView.UxmlTraits
		{
			// Token: 0x06000DC6 RID: 3526 RVA: 0x0003DA74 File Offset: 0x0003BC74
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TreeView treeView = ve as TreeView;
				VisualTreeAsset visualTreeAsset;
				bool flag = this.m_ItemTemplate.TryGetValueFromBag(bag, cc, out visualTreeAsset);
				if (flag)
				{
					treeView.itemTemplate = visualTreeAsset;
				}
			}

			// Token: 0x0400089C RID: 2204
			private UxmlAssetAttributeDescription<VisualTreeAsset> m_ItemTemplate = new UxmlAssetAttributeDescription<VisualTreeAsset>
			{
				name = "item-template"
			};
		}
	}
}
