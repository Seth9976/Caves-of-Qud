using System;
using System.Collections.Generic;
using System.Linq;
using Qud.UI;
using UnityEngine.Events;

namespace XRL.UI.Framework
{
	// Token: 0x020014F6 RID: 5366
	public class FilterBar : HorizontalScroller
	{
		// Token: 0x0600D1A1 RID: 53665 RVA: 0x004DE150 File Offset: 0x004DC350
		public void SetupContext(bool includeScrollContext = true)
		{
			this.rootContext.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.rootContext.contexts.Clear();
			this.rootContext.contexts.Add(this.searchInput.context);
			if (includeScrollContext)
			{
				this.rootContext.contexts.Add(this.scrollContext);
			}
			this.rootContext.Setup();
			this.searchInput.OnSearchTextChange.RemoveAllListeners();
			this.searchInput.OnSearchTextChange.AddListener(delegate(string s)
			{
				this.OnSearchTextChange.Invoke(s);
			});
			this.scrollContext.SetAxis(this.NavigationAxis, false);
			this.scrollContext.wraps = false;
			this.scrollContext.contexts.Add(this.rootContext);
			this.scrollContext.Setup();
		}

		// Token: 0x0600D1A2 RID: 53666 RVA: 0x004DE224 File Offset: 0x004DC424
		public void CategorySelected(string category)
		{
			if (this.SingleCategoryOnly)
			{
				this.enabledCategories.Clear();
				this.enabledCategories.Add(category);
			}
			else if (category == "*All")
			{
				if (!this.enabledCategories.Contains(category))
				{
					this.enabledCategories.Clear();
					this.enabledCategories.Add("*All");
				}
				else
				{
					this.enabledCategories.Remove("*All");
				}
			}
			else if (!this.enabledCategories.Contains(category))
			{
				this.enabledCategories.Add(category);
				this.enabledCategories.Remove("*All");
			}
			else
			{
				this.enabledCategories.Remove(category);
			}
			if (this.enabledCategories.Count == 0)
			{
				this.enabledCategories.Add("*All");
			}
			if (this.filtersUpdated != null)
			{
				this.filtersUpdated();
			}
		}

		// Token: 0x0600D1A3 RID: 53667 RVA: 0x004DE30C File Offset: 0x004DC50C
		public void ResetFilters()
		{
			FrameworkSearchInput frameworkSearchInput = this.searchInput;
			bool flag;
			if (frameworkSearchInput == null)
			{
				flag = null != null;
			}
			else
			{
				FrameworkSearchInput.SearchContext context = frameworkSearchInput.context;
				flag = ((context != null) ? context.inputText : null) != null;
			}
			if (flag)
			{
				this.searchInput.context.inputText = "";
			}
			this.enabledCategories.Clear();
			this.enabledCategories.Add("*All");
		}

		// Token: 0x0600D1A4 RID: 53668 RVA: 0x004DE36C File Offset: 0x004DC56C
		public void CategoryLeft()
		{
			if (this.choices.Count == 0)
			{
				return;
			}
			int num = this.choices.FindIndex((FrameworkDataElement i) => (i as FilterBarCategoryButtonData).category == this.enabledCategories.FirstOrDefault<string>());
			num--;
			if (num < 0)
			{
				num = Math.Max(0, this.choices.Count - 1);
			}
			this.enabledCategories.Clear();
			this.enabledCategories.Add((this.choices[num] as FilterBarCategoryButtonData).category);
			this.UpdateViewFromData();
			if (this.filtersUpdated != null)
			{
				this.filtersUpdated();
			}
		}

		// Token: 0x0600D1A5 RID: 53669 RVA: 0x004DE400 File Offset: 0x004DC600
		public void CategoryRight()
		{
			if (this.choices.Count == 0)
			{
				return;
			}
			int num = this.choices.FindIndex((FrameworkDataElement i) => (i as FilterBarCategoryButtonData).category == this.enabledCategories.FirstOrDefault<string>());
			num++;
			if (num >= this.choices.Count)
			{
				num = 0;
			}
			this.enabledCategories.Clear();
			this.enabledCategories.Add((this.choices[num] as FilterBarCategoryButtonData).category);
			this.UpdateViewFromData();
			if (this.filtersUpdated != null)
			{
				this.filtersUpdated();
			}
		}

		// Token: 0x0600D1A6 RID: 53670 RVA: 0x004DE48C File Offset: 0x004DC68C
		public void UpdateViewFromData()
		{
			foreach (FrameworkDataElement frameworkDataElement in this.choices)
			{
				FilterBarCategoryButtonData filterBarCategoryButtonData = frameworkDataElement as FilterBarCategoryButtonData;
				if (filterBarCategoryButtonData != null)
				{
					filterBarCategoryButtonData.button.FiltersUpdated(this);
				}
			}
		}

		// Token: 0x0600D1A7 RID: 53671 RVA: 0x004DE4EC File Offset: 0x004DC6EC
		public void SetCategoriesViaButtonBarData(IEnumerable<FilterBarCategoryButtonData> data)
		{
			this.choices.Clear();
			this.choices.AddRange(data);
			this.lastContexts.Clear();
			this.lastContexts.AddRange(this.scrollContext.contexts);
			this.scrollContext.contexts.Clear();
			this.LayoutChildren();
			this.scrollContext.Setup();
			this.UpdateViewFromData();
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600D1A8 RID: 53672 RVA: 0x004DE558 File Offset: 0x004DC758
		public override bool allowPrefabPoolingDuringLayoutChildren
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600D1A9 RID: 53673 RVA: 0x004DE55C File Offset: 0x004DC75C
		public void SetCategories(IEnumerable<string> categories)
		{
			this.choices.Clear();
			if (categories != null)
			{
				this.choices.AddRange(categories.Select<string, FilterBarCategoryButtonData>((string c) => new FilterBarCategoryButtonData(c, delegate(string c)
				{
					this.CategorySelected(c);
				})));
			}
			this.lastContexts.Clear();
			this.lastContexts.AddRange(this.scrollContext.contexts);
			this.scrollContext.contexts.Clear();
			this.LayoutChildren();
			this.scrollContext.Setup();
			this.UpdateViewFromData();
		}

		// Token: 0x040052D9 RID: 21209
		public ScrollContext<NavigationContext> rootContext = new ScrollContext<NavigationContext>();

		// Token: 0x040052DA RID: 21210
		public List<string> enabledCategories = new List<string>(24);

		// Token: 0x040052DB RID: 21211
		public FrameworkSearchInput searchInput;

		// Token: 0x040052DC RID: 21212
		public HashSet<string> visibleCategories = new HashSet<string>();

		// Token: 0x040052DD RID: 21213
		public List<FilterBarCategoryButton> categoryButtons;

		// Token: 0x040052DE RID: 21214
		public UnityEvent<string> OnSearchTextChange = new UnityEvent<string>();

		// Token: 0x040052DF RID: 21215
		public UnityEvent<string> OnCategorySelected = new UnityEvent<string>();

		// Token: 0x040052E0 RID: 21216
		public bool SingleCategoryOnly;

		// Token: 0x040052E1 RID: 21217
		public Action filtersUpdated;
	}
}
