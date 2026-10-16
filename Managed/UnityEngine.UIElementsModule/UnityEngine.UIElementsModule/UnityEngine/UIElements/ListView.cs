using System;
using System.Collections;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000131 RID: 305
	public class ListView : BaseListView
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0002AF80 File Offset: 0x00029180
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x0002AF88 File Offset: 0x00029188
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
					base.NotifyPropertyChanged(in ListView.makeItemProperty);
				}
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002AFC2 File Offset: 0x000291C2
		internal void SetMakeItemWithoutNotify(Func<VisualElement> func)
		{
			this.m_MakeItem = func;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002AFCC File Offset: 0x000291CC
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0002AFD4 File Offset: 0x000291D4
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
					bool flag2 = this.m_TemplateMakeItem != this.makeItem;
					if (flag2)
					{
						this.makeItem = this.m_TemplateMakeItem;
					}
					else
					{
						base.Rebuild();
					}
					base.NotifyPropertyChanged(in ListView.itemTemplateProperty);
				}
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002B034 File Offset: 0x00029234
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

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0002B06F File Offset: 0x0002926F
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0002B078 File Offset: 0x00029278
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
					base.NotifyPropertyChanged(in ListView.bindItemProperty);
				}
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002B0B2 File Offset: 0x000292B2
		internal void SetBindItemWithoutNotify(Action<VisualElement, int> callback)
		{
			this.m_BindItem = callback;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0002B0BC File Offset: 0x000292BC
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0002B0C4 File Offset: 0x000292C4
		[CreateProperty]
		public new Action<VisualElement, int> unbindItem
		{
			get
			{
				return this.m_UnbindItem;
			}
			set
			{
				bool flag = value == this.m_UnbindItem;
				if (!flag)
				{
					this.m_UnbindItem = value;
					base.NotifyPropertyChanged(in ListView.unbindItemProperty);
				}
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0002B0F7 File Offset: 0x000292F7
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0002B100 File Offset: 0x00029300
		[CreateProperty]
		public new Action<VisualElement> destroyItem
		{
			get
			{
				return this.m_DestroyItem;
			}
			set
			{
				bool flag = value == this.m_DestroyItem;
				if (!flag)
				{
					this.m_DestroyItem = value;
					base.NotifyPropertyChanged(in ListView.destroyItemProperty);
				}
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002B134 File Offset: 0x00029334
		internal override bool HasValidDataAndBindings()
		{
			return base.HasValidDataAndBindings() && ((base.autoAssignSource && this.makeItem != null) || this.makeItem != null == (this.bindItem != null));
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002B178 File Offset: 0x00029378
		protected override CollectionViewController CreateViewController()
		{
			return new ListViewController();
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002B17F File Offset: 0x0002937F
		public ListView()
		{
			base.AddToClassList(BaseListView.ussClassName);
			this.m_TemplateMakeItem = new Func<VisualElement>(this.TemplateMakeItem);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002B1A7 File Offset: 0x000293A7
		public ListView(IList itemsSource, float itemHeight = -1f, Func<VisualElement> makeItem = null, Action<VisualElement, int> bindItem = null)
			: base(itemsSource, itemHeight)
		{
			base.AddToClassList(BaseListView.ussClassName);
			this.m_TemplateMakeItem = new Func<VisualElement>(this.TemplateMakeItem);
			this.makeItem = makeItem;
			this.bindItem = bindItem;
		}

		// Token: 0x04000590 RID: 1424
		internal static readonly BindingId itemTemplateProperty = "itemTemplate";

		// Token: 0x04000591 RID: 1425
		internal static readonly BindingId makeItemProperty = "makeItem";

		// Token: 0x04000592 RID: 1426
		internal static readonly BindingId bindItemProperty = "bindItem";

		// Token: 0x04000593 RID: 1427
		internal static readonly BindingId unbindItemProperty = "unbindItem";

		// Token: 0x04000594 RID: 1428
		internal static readonly BindingId destroyItemProperty = "destroyItem";

		// Token: 0x04000595 RID: 1429
		private Func<VisualElement> m_MakeItem;

		// Token: 0x04000596 RID: 1430
		private Func<VisualElement> m_TemplateMakeItem;

		// Token: 0x04000597 RID: 1431
		private VisualTreeAsset m_ItemTemplate;

		// Token: 0x04000598 RID: 1432
		private Action<VisualElement, int> m_BindItem;

		// Token: 0x04000599 RID: 1433
		private Action<VisualElement, int> m_UnbindItem;

		// Token: 0x0400059A RID: 1434
		private Action<VisualElement> m_DestroyItem;

		// Token: 0x02000132 RID: 306
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseListView.UxmlSerializedData
		{
			// Token: 0x060009DF RID: 2527 RVA: 0x0002B23C File Offset: 0x0002943C
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(ListView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("itemTemplate", "item-template", null, Array.Empty<string>())
				});
			}

			// Token: 0x060009E0 RID: 2528 RVA: 0x0002B271 File Offset: 0x00029471
			public override object CreateInstance()
			{
				return new ListView();
			}

			// Token: 0x060009E1 RID: 2529 RVA: 0x0002B278 File Offset: 0x00029478
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.itemTemplate_UxmlAttributeFlags);
				if (flag)
				{
					ListView listView = (ListView)obj;
					listView.itemTemplate = this.itemTemplate;
				}
			}

			// Token: 0x0400059B RID: 1435
			[SerializeField]
			private VisualTreeAsset itemTemplate;

			// Token: 0x0400059C RID: 1436
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags itemTemplate_UxmlAttributeFlags;
		}

		// Token: 0x02000133 RID: 307
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ListView, ListView.UxmlTraits>
		{
		}

		// Token: 0x02000134 RID: 308
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseListView.UxmlTraits
		{
			// Token: 0x060009E4 RID: 2532 RVA: 0x0002B2C8 File Offset: 0x000294C8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ListView listView = ve as ListView;
				VisualTreeAsset visualTreeAsset;
				bool flag = this.m_ItemTemplate.TryGetValueFromBag(bag, cc, out visualTreeAsset);
				if (flag)
				{
					listView.itemTemplate = visualTreeAsset;
				}
			}

			// Token: 0x0400059D RID: 1437
			private UxmlAssetAttributeDescription<VisualTreeAsset> m_ItemTemplate = new UxmlAssetAttributeDescription<VisualTreeAsset>
			{
				name = "item-template"
			};
		}
	}
}
