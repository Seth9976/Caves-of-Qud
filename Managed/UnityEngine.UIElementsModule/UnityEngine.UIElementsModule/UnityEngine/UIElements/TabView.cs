using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A2 RID: 418
	[UxmlElement(null, new Type[] { typeof(Tab) })]
	public class TabView : VisualElement
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0003B19E File Offset: 0x0003939E
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0003B1A6 File Offset: 0x000393A6
		internal VisualElement header
		{
			get
			{
				return this.m_HeaderContainer;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0003B1AE File Offset: 0x000393AE
		internal List<Tab> tabs
		{
			get
			{
				return this.m_Tabs;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0003B1B6 File Offset: 0x000393B6
		internal List<VisualElement> tabHeaders
		{
			get
			{
				return this.m_TabHeaders;
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000D1B RID: 3355 RVA: 0x0003B1C0 File Offset: 0x000393C0
		// (remove) Token: 0x06000D1C RID: 3356 RVA: 0x0003B1F8 File Offset: 0x000393F8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Tab, Tab> activeTabChanged;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000D1D RID: 3357 RVA: 0x0003B230 File Offset: 0x00039430
		// (remove) Token: 0x06000D1E RID: 3358 RVA: 0x0003B268 File Offset: 0x00039468
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, int> tabReordered;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000D1F RID: 3359 RVA: 0x0003B2A0 File Offset: 0x000394A0
		// (remove) Token: 0x06000D20 RID: 3360 RVA: 0x0003B2D8 File Offset: 0x000394D8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Tab, int> tabClosed;

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0003B30D File Offset: 0x0003950D
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0003B318 File Offset: 0x00039518
		public Tab activeTab
		{
			get
			{
				return this.m_ActiveTab;
			}
			set
			{
				bool flag = value == null && this.m_Tabs.Count > 0;
				if (flag)
				{
					throw new NullReferenceException("Active tab cannot be null when there are available tabs.");
				}
				bool flag2 = this.m_Tabs.IndexOf(value) == -1;
				if (flag2)
				{
					throw new Exception("The tab to be set as active does not exist in this TabView.");
				}
				bool flag3 = value == this.m_ActiveTab;
				if (!flag3)
				{
					Tab activeTab = this.m_ActiveTab;
					Tab activeTab2 = this.m_ActiveTab;
					if (activeTab2 != null)
					{
						activeTab2.SetInactive();
					}
					this.m_ActiveTab = value;
					Tab activeTab3 = this.m_ActiveTab;
					if (activeTab3 != null)
					{
						activeTab3.SetActive();
					}
					bool flag4 = !this.m_ApplyingViewState;
					if (flag4)
					{
						this.SaveViewState();
					}
					Action<Tab, Tab> action = this.activeTabChanged;
					if (action != null)
					{
						action(activeTab, value);
					}
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0003B3D4 File Offset: 0x000395D4
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x0003B418 File Offset: 0x00039618
		public int selectedTabIndex
		{
			get
			{
				bool flag = this.activeTab == null || this.m_Tabs.Count == 0;
				int num;
				if (flag)
				{
					num = -1;
				}
				else
				{
					num = this.m_Tabs.IndexOf(this.activeTab);
				}
				return num;
			}
			set
			{
				bool flag = value >= 0 && this.m_Tabs.Count > value;
				if (flag)
				{
					this.activeTab = this.m_Tabs[value];
				}
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0003B452 File Offset: 0x00039652
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x0003B45C File Offset: 0x0003965C
		[CreateProperty]
		public bool reorderable
		{
			get
			{
				return this.m_Reorderable;
			}
			set
			{
				bool flag = this.m_Reorderable == value;
				if (!flag)
				{
					this.m_Reorderable = value;
					base.EnableInClassList(TabView.reorderableUssClassName, value);
					foreach (Tab tab in this.m_Tabs)
					{
						tab.EnableTabDragHandles(value);
					}
					base.NotifyPropertyChanged(in TabView.reorderableProperty);
				}
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0003B4E4 File Offset: 0x000396E4
		public TabView()
		{
			base.AddToClassList(TabView.ussClassName);
			this.m_HeaderContainer = new VisualElement
			{
				name = TabView.headerContainerClassName,
				classList = { TabView.headerContainerClassName }
			};
			base.hierarchy.Add(this.m_HeaderContainer);
			this.m_ContentContainer = new VisualElement
			{
				name = TabView.contentContainerUssClassName,
				classList = { TabView.contentContainerUssClassName }
			};
			base.hierarchy.Add(this.m_ContentContainer);
			this.m_ContentContainer.elementAdded += this.OnElementAdded;
			this.m_ContentContainer.elementRemoved += this.OnElementRemoved;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003B5C8 File Offset: 0x000397C8
		internal override void OnViewDataReady()
		{
			try
			{
				this.m_ApplyingViewState = true;
				base.OnViewDataReady();
				string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
				this.m_ViewState = base.GetOrCreateViewData<TabView.ViewState>(this.m_ViewState, fullHierarchicalViewDataKey);
				this.m_ViewState.Apply(this);
			}
			finally
			{
				this.m_ApplyingViewState = false;
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0003B62C File Offset: 0x0003982C
		private void SaveViewState()
		{
			bool applyingViewState = this.m_ApplyingViewState;
			if (!applyingViewState)
			{
				TabView.ViewState viewState = this.m_ViewState;
				if (viewState != null)
				{
					viewState.Save(this);
				}
				base.SaveViewData();
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0003B660 File Offset: 0x00039860
		private void UpdateIndexes()
		{
			for (int i = 0; i < this.m_Tabs.Count; i++)
			{
				this.m_Tabs[i].index = i;
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003B6A0 File Offset: 0x000398A0
		private void OnElementAdded(VisualElement ve, int index)
		{
			Tab tab = ve as Tab;
			bool flag = tab == null || this.m_Reordering;
			if (!flag)
			{
				VisualElement tabHeader = tab.tabHeader;
				bool flag2 = tabHeader != null;
				if (flag2)
				{
					this.m_HeaderContainer.Insert(index, tabHeader);
					this.m_TabHeaders.Insert(index, tabHeader);
					this.m_Tabs.Insert(index, tab);
					tab.EnableTabDragHandles(this.m_Reorderable);
					tab.closed += this.OnTabClosed;
				}
				tab.selected += this.OnTabSelected;
				this.UpdateIndexes();
				bool flag3 = this.activeTab == null;
				if (flag3)
				{
					this.activeTab = tab;
				}
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003B758 File Offset: 0x00039958
		private void OnElementRemoved(VisualElement ve)
		{
			Tab tab = ve as Tab;
			bool flag = tab == null || this.m_Reordering;
			if (!flag)
			{
				VisualElement tabHeader = tab.tabHeader;
				this.m_HeaderContainer.Remove(tabHeader);
				this.m_TabHeaders.Remove(tabHeader);
				this.m_Tabs.Remove(tab);
				tab.EnableTabDragHandles(false);
				tab.hierarchy.Insert(0, tabHeader);
				tab.SetInactive();
				this.UpdateIndexes();
				bool flag2 = this.activeTab == tab && this.m_Tabs.Count > 0;
				if (flag2)
				{
					this.activeTab = this.m_Tabs[0];
				}
				else
				{
					bool flag3 = this.m_Tabs.Count == 0;
					if (flag3)
					{
						this.m_ActiveTab = null;
					}
				}
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003B827 File Offset: 0x00039A27
		private void OnTabSelected(Tab tab)
		{
			this.activeTab = tab;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003B832 File Offset: 0x00039A32
		private void OnTabClosed(Tab tab)
		{
			Action<Tab, int> action = this.tabClosed;
			if (action != null)
			{
				action(tab, tab.index);
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0003B850 File Offset: 0x00039A50
		public void ReorderTab(int from, int to)
		{
			VisualElement visualElement = this.m_TabHeaders[from];
			Tab tab = this.m_Tabs[from];
			bool flag = !visualElement.visible || !this.reorderable || from == to;
			if (!flag)
			{
				this.m_Reordering = true;
				this.m_TabHeaders.RemoveAt(from);
				this.m_TabHeaders.Insert(to, visualElement);
				this.m_Tabs.RemoveAt(from);
				this.m_Tabs.Insert(to, tab);
				this.m_HeaderContainer.Insert(to, visualElement);
				base.Insert(to, tab);
				this.m_Reordering = false;
				this.UpdateIndexes();
				Action<int, int> action = this.tabReordered;
				if (action != null)
				{
					action(from, to);
				}
				bool flag2 = !this.m_ApplyingViewState;
				if (flag2)
				{
					this.SaveViewState();
				}
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003B924 File Offset: 0x00039B24
		public Tab GetTab(int index)
		{
			bool flag = index < 0 || index >= this.m_Tabs.Count;
			Tab tab;
			if (flag)
			{
				tab = null;
			}
			else
			{
				tab = this.m_Tabs[index];
			}
			return tab;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003B964 File Offset: 0x00039B64
		public VisualElement GetTabHeader(int index)
		{
			bool flag = index < 0 || index >= this.m_Tabs.Count;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = null;
			}
			else
			{
				visualElement = this.m_TabHeaders[index];
			}
			return visualElement;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0003B9A4 File Offset: 0x00039BA4
		internal Tab FindTabByKey(string key)
		{
			return this.m_Tabs.Find((Tab tab) => tab.viewDataKey == key);
		}

		// Token: 0x04000841 RID: 2113
		internal static readonly BindingId reorderableProperty = "reorderable";

		// Token: 0x04000842 RID: 2114
		public static readonly string ussClassName = "unity-tab-view";

		// Token: 0x04000843 RID: 2115
		public static readonly string headerContainerClassName = TabView.ussClassName + "__header-container";

		// Token: 0x04000844 RID: 2116
		public static readonly string contentContainerUssClassName = TabView.ussClassName + "__content-container";

		// Token: 0x04000845 RID: 2117
		public static readonly string reorderableUssClassName = TabView.ussClassName + "__reorderable";

		// Token: 0x04000846 RID: 2118
		public static readonly string verticalUssClassName = TabView.ussClassName + "__vertical";

		// Token: 0x04000847 RID: 2119
		private VisualElement m_HeaderContainer;

		// Token: 0x04000848 RID: 2120
		private VisualElement m_ContentContainer;

		// Token: 0x04000849 RID: 2121
		private List<Tab> m_Tabs = new List<Tab>();

		// Token: 0x0400084A RID: 2122
		private List<VisualElement> m_TabHeaders = new List<VisualElement>();

		// Token: 0x0400084B RID: 2123
		private Tab m_ActiveTab;

		// Token: 0x0400084C RID: 2124
		private TabView.ViewState m_ViewState;

		// Token: 0x0400084D RID: 2125
		private bool m_ApplyingViewState;

		// Token: 0x0400084E RID: 2126
		private bool m_Reordering;

		// Token: 0x04000852 RID: 2130
		private bool m_Reorderable;

		// Token: 0x020001A3 RID: 419
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06000D34 RID: 3380 RVA: 0x0003BA52 File Offset: 0x00039C52
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(TabView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("reorderable", "reorderable", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000D35 RID: 3381 RVA: 0x0003BA87 File Offset: 0x00039C87
			public override object CreateInstance()
			{
				return new TabView();
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x0003BA90 File Offset: 0x00039C90
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.reorderable_UxmlAttributeFlags);
				if (flag)
				{
					TabView tabView = (TabView)obj;
					tabView.reorderable = this.reorderable;
				}
			}

			// Token: 0x04000853 RID: 2131
			[SerializeField]
			private bool reorderable;

			// Token: 0x04000854 RID: 2132
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags reorderable_UxmlAttributeFlags;
		}

		// Token: 0x020001A4 RID: 420
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TabView, TabView.UxmlTraits>
		{
		}

		// Token: 0x020001A5 RID: 421
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000D39 RID: 3385 RVA: 0x0003BAD4 File Offset: 0x00039CD4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TabView tabView = (TabView)ve;
				tabView.reorderable = this.m_Reorderable.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000855 RID: 2133
			private readonly UxmlBoolAttributeDescription m_Reorderable = new UxmlBoolAttributeDescription
			{
				name = "reorderable",
				defaultValue = false
			};
		}

		// Token: 0x020001A6 RID: 422
		[Serializable]
		private class ViewState : ISerializationCallbackReceiver
		{
			// Token: 0x06000D3B RID: 3387 RVA: 0x0003BB30 File Offset: 0x00039D30
			internal void Save(TabView tabView)
			{
				this.m_HasPersistedData = true;
				bool flag = tabView.m_ActiveTab != null;
				if (flag)
				{
					this.m_ActiveTabKey = tabView.m_ActiveTab.viewDataKey;
				}
				this.m_TabOrder.Clear();
				bool flag2 = !tabView.reorderable;
				if (!flag2)
				{
					foreach (Tab tab in tabView.tabs)
					{
						this.m_TabOrder.Add(tab.viewDataKey);
					}
				}
			}

			// Token: 0x06000D3C RID: 3388 RVA: 0x0003BBD4 File Offset: 0x00039DD4
			internal void Apply(TabView tabView)
			{
				bool flag = !this.m_HasPersistedData;
				if (!flag)
				{
					int num = Math.Min(this.m_TabOrder.Count, tabView.tabs.Count);
					int num2 = 0;
					Tab tab = tabView.FindTabByKey(this.m_ActiveTabKey);
					bool flag2 = tab != null;
					if (flag2)
					{
						tabView.activeTab = tab;
					}
					bool flag3 = !tabView.reorderable;
					if (!flag3)
					{
						int num3 = 0;
						while (num3 < this.m_TabOrder.Count && num2 < num)
						{
							string text = this.m_TabOrder[num3];
							Tab tab2 = tabView.FindTabByKey(text);
							bool flag4 = tab2 != null;
							if (flag4)
							{
								int num4 = tabView.tabs.IndexOf(tab2);
								tabView.ReorderTab(num4, num2++);
							}
							num3++;
						}
					}
				}
			}

			// Token: 0x06000D3D RID: 3389 RVA: 0x0003BCAE File Offset: 0x00039EAE
			public void OnBeforeSerialize()
			{
				this.m_HasPersistedData = true;
			}

			// Token: 0x06000D3E RID: 3390 RVA: 0x0003BCAE File Offset: 0x00039EAE
			public void OnAfterDeserialize()
			{
				this.m_HasPersistedData = true;
			}

			// Token: 0x04000856 RID: 2134
			private bool m_HasPersistedData;

			// Token: 0x04000857 RID: 2135
			[SerializeField]
			private List<string> m_TabOrder = new List<string>();

			// Token: 0x04000858 RID: 2136
			[SerializeField]
			private string m_ActiveTabKey;
		}
	}
}
