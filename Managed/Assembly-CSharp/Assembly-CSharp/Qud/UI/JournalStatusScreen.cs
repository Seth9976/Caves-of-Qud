using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using Genkit;
using HistoryKit;
using Kobold;
using Qud.API;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Parts;

namespace Qud.UI
{
	// Token: 0x0200046D RID: 1133
	[HasGameBasedStaticCache]
	[UIView("JournalStatusScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menus", UICanvas = "StatusScreens", UICanvasHost = 1)]
	public class JournalStatusScreen : BaseStatusScreen<JournalStatusScreen>
	{
		// Token: 0x06002B13 RID: 11027 RVA: 0x000E4C63 File Offset: 0x000E2E63
		public override IRenderable GetTabIcon()
		{
			if (this.icon == null)
			{
				this.icon = new Renderable("Items/sw_book1.bmp", " ", "&w", null, 'W');
			}
			return this.icon;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000E4C90 File Offset: 0x000E2E90
		public override string GetTabString()
		{
			return "Journal";
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000E4C97 File Offset: 0x000E2E97
		public override bool WantsCategoryBar()
		{
			return true;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000E4C9A File Offset: 0x000E2E9A
		public void HandleNavigationXAxis()
		{
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000E4C9C File Offset: 0x000E2E9C
		public bool isCollapsed(string category, bool defaultValue)
		{
			bool flag;
			if (!this.categoryCollapsed.TryGetValue(category, out flag))
			{
				return defaultValue;
			}
			return flag;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000E4CBC File Offset: 0x000E2EBC
		public void UpdateData()
		{
			int currentCategory = this.CurrentCategory;
			int? lastCategory = JournalStatusScreen.LastCategory;
			if ((currentCategory == lastCategory.GetValueOrDefault()) & (lastCategory != null))
			{
				return;
			}
			foreach (JournalLineData journalLineData in this.rawLineData)
			{
				journalLineData.free();
			}
			this.rawLineData.Clear();
			this.rawLineData.AddRange(from n in JournalScreen.GetRawEntriesFor(this.categoryInfos[this.CurrentCategory].Name, null)
				select PooledFrameworkDataElement<JournalLineData>.next().set(n, this, false, false, null, null, 0, 0));
			if (this.filterText.IsNullOrEmpty())
			{
				this.searchResult = this.rawLineData;
			}
			else
			{
				this.searchResult = from m in Process.ExtractTop<JournalLineData>(this.searcher, this.rawLineData, (JournalLineData i) => i.searchText, null, this.rawLineData.Count<JournalLineData>(), 50)
					select m.Value;
			}
			if (this.currentInfo.UsesCategories)
			{
				foreach (KeyValuePair<string, List<JournalLineData>> keyValuePair in this.categories)
				{
					keyValuePair.Value.Clear();
				}
				foreach (JournalLineData journalLineData2 in this.searchResult)
				{
					string text = this.currentInfo.CategoryFor(journalLineData2);
					if (!this.categories.ContainsKey(text))
					{
						this.categories.Add(text, new List<JournalLineData>());
					}
					this.categories[text].Add(journalLineData2);
				}
			}
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000E4ED0 File Offset: 0x000E30D0
		public void highlightSultanPeriod(int Period)
		{
			for (int i = 0; i < this.sultanStatuePeriod.Count; i++)
			{
				this.sultanStatues[i].SetHighlight(this.sultanStatuePeriod[i] == Period);
			}
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000E4F14 File Offset: 0x000E3114
		public void UpdateStatues()
		{
			int num = 0;
			List<HistoricEntitySnapshot> list = (from s in HistoryAPI.GetKnownSultans()
				select s.GetCurrentSnapshot()).ToList<HistoricEntitySnapshot>();
			list.Sort((HistoricEntitySnapshot a, HistoricEntitySnapshot b) => a.entity.lastYear.CompareTo(b.entity.lastYear));
			this.sultanStatuePeriod.Clear();
			using (List<HistoricEntitySnapshot>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HistoricEntitySnapshot historicEntitySnapshot = enumerator.Current;
					int num2 = int.Parse(historicEntitySnapshot.GetProperty("period", "0"));
					string property = historicEntitySnapshot.GetProperty("name", "<unknown>");
					string text = historicEntitySnapshot.GetProperty("statueTile", null) ?? SultanShrine.GetStatueForSultan(property, num2);
					this.sultanStatuePeriod.Add(num2);
					if (this.sultanStatues.Count <= num)
					{
						global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate<global::UnityEngine.GameObject>(this.sultanShrineLinePrefab);
						this.sultanStatues.Add(gameObject.GetComponent<JournalSultanStatueLine>());
						gameObject.transform.SetParent(this.sultanShrineContentRoot.transform, false);
					}
					this.sultanStatues[num].gameObject.SetActive(true);
					this.sultanStatues[num].SetHighlight(false);
					this.sultanStatues[num].SetBase(historicEntitySnapshot.GetProperty("statueBase", "unknown") == "true");
					this.sultanStatues[num].text.SetText(property);
					this.sultanStatues[num].fadedImage.sprite = SpriteManager.GetUnitySprite(text);
					this.sultanStatues[num].fullImage.sprite = SpriteManager.GetUnitySprite(text);
					num++;
				}
				goto IL_01E8;
			}
			IL_01CD:
			this.sultanStatues[num].gameObject.SetActive(false);
			num++;
			IL_01E8:
			if (num >= this.sultanStatues.Count)
			{
				return;
			}
			goto IL_01CD;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000E5134 File Offset: 0x000E3334
		public override void UpdateViewFromData()
		{
			this.marginaliaScroller.SetActive(this.currentInfo.UsesMarginalia);
			this.mapScroller.SetActive(this.currentInfo.UsesMap);
			if (this.currentInfo.UsesSultans)
			{
				this.UpdateStatues();
			}
			this.categoryText.SetText(JournalScreen.GetTabDisplayName(this.currentInfo.Name));
			this.categoryText.style = ((Media.sizeClass < Media.SizeClass.Medium) ? UITextSkin.Size.normal : UITextSkin.Size.header);
			this.UpdateData();
			this.updateCategoryButtonBar();
			this.usedCategories.Clear();
			if (this.currentInfo.UsesCategories)
			{
				List<string> list = this.categories.Keys.ToList<string>();
				if (this.currentInfo.SortCategoriesAZ)
				{
					list.Sort();
				}
				this.lineData.Clear();
				int num = 0;
				foreach (string text in list)
				{
					List<JournalLineData> list2 = this.categories[text];
					if (list2.Count > 0)
					{
						string text2 = text;
						this.usedCategories.Add(text);
						int num2 = 0;
						if (num < this.sultanStatuePeriod.Count)
						{
							num2 = this.sultanStatuePeriod[num];
						}
						if (this.currentInfo.Name == JournalScreen.STR_SULTANS)
						{
							text2 = "{{W|HISTORY OF " + text2.ToUpper() + "}}";
						}
						JournalLineData journalLineData = PooledFrameworkDataElement<JournalLineData>.next().set(null, this, true, !this.isCollapsed(text2, this.currentInfo.UsesSultans), text2, null, num2, 0);
						this.lineData.Add(journalLineData);
						this.rawLineData.Add(journalLineData);
						JournalLineData journalLineData2 = list2.FirstOrDefault<JournalLineData>();
						if (journalLineData2 != null)
						{
							journalLineData.sultanPeriod = journalLineData2.sultanPeriod;
						}
						if (journalLineData2 != null)
						{
							journalLineData._mapTarget = journalLineData2.mapTarget;
						}
						if (!this.isCollapsed(text2, this.currentInfo.UsesSultans))
						{
							int num3 = 0;
							foreach (JournalLineData journalLineData3 in list2)
							{
								journalLineData3.categoryName = text;
								journalLineData3.categoryOffset = num3 + 1;
								this.lineData.Add(journalLineData3);
								num3++;
							}
						}
					}
					num++;
				}
				if (this.lineData.Count == 0)
				{
					this.lineData.Add(PooledFrameworkDataElement<JournalLineData>.next().set(null, this, true, false, JournalStatusScreen.NO_ENTRIES_TEXT, null, 0, 0));
					if (this.sultanScroller.activeSelf)
					{
						this.ResetLayoutFrame(3);
					}
					this.sultanScroller.SetActive(false);
				}
				else
				{
					if (this.sultanScroller.activeSelf != this.currentInfo.UsesSultans)
					{
						this.ResetLayoutFrame(3);
					}
					this.sultanScroller.SetActive(this.currentInfo.UsesSultans);
				}
				this.controller.BeforeShow(this.lineData);
				this.lineData.Clear();
				return;
			}
			if (this.searchResult.Count<JournalLineData>() == 0)
			{
				this.controller.BeforeShow(new List<FrameworkDataElement> { PooledFrameworkDataElement<JournalLineData>.next().set(null, this, true, false, JournalStatusScreen.NO_ENTRIES_TEXT, null, 0, 0) });
				if (this.sultanScroller.activeSelf)
				{
					this.ResetLayoutFrame(3);
				}
				this.sultanScroller.SetActive(false);
				return;
			}
			if (this.sultanScroller.activeSelf != this.currentInfo.UsesSultans)
			{
				this.ResetLayoutFrame(3);
			}
			this.sultanScroller.SetActive(this.currentInfo.UsesSultans);
			this.controller.BeforeShow(this.searchResult);
			this.HandleHighlightItem(this.searchResult.FirstOrDefault<JournalLineData>());
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000E551C File Offset: 0x000E371C
		public static string ObservationCategory(JournalLineData data)
		{
			string text;
			if (data == null)
			{
				text = null;
			}
			else
			{
				IBaseJournalEntry entry = data.entry;
				text = ((entry != null) ? entry.LearnedFrom : null);
			}
			return text ?? "Unknown";
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000E5540 File Offset: 0x000E3740
		public static string LocationCategory(JournalLineData data)
		{
			JournalMapNote journalMapNote = data.entry as JournalMapNote;
			if (journalMapNote != null)
			{
				return ((journalMapNote != null) ? journalMapNote.Category : null) ?? "Unknown";
			}
			return "Unknown";
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000E5578 File Offset: 0x000E3778
		public static string SultanCategory(JournalLineData data)
		{
			JournalSultanNote journalSultanNote = data.entry as JournalSultanNote;
			if (journalSultanNote != null)
			{
				return HistoryAPI.GetEntityName((journalSultanNote != null) ? journalSultanNote.SultanID : null) ?? "Unknown";
			}
			return "Unknown";
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000E55B4 File Offset: 0x000E37B4
		public static string VillageCategory(JournalLineData data)
		{
			JournalVillageNote journalVillageNote = data.entry as JournalVillageNote;
			if (journalVillageNote != null)
			{
				return HistoryAPI.GetEntityName((journalVillageNote != null) ? journalVillageNote.VillageID : null) ?? "Unknown";
			}
			return "Unknown";
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000E55F0 File Offset: 0x000E37F0
		public static string AccomplishmentCategory(JournalLineData data)
		{
			JournalAccomplishment journalAccomplishment = data.entry as JournalAccomplishment;
			if (journalAccomplishment == null)
			{
				return "Unknown";
			}
			if (journalAccomplishment == null)
			{
				return null;
			}
			return journalAccomplishment.Category;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x000E561D File Offset: 0x000E381D
		public JournalStatusScreen.CategoryInfo currentInfo
		{
			get
			{
				return this.categoryInfos[this.CurrentCategory];
			}
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000E5630 File Offset: 0x000E3830
		public override void FilterUpdated(string filterText)
		{
			int num = this.categoryBarButtons.IndexOf(this.categoryBarButtons.Where<FilterBarCategoryButtonData>((FilterBarCategoryButtonData b) => b.category == this.filterBar.enabledCategories.FirstOrDefault<string>()).FirstOrDefault<FilterBarCategoryButtonData>());
			if (num >= 0)
			{
				this.CurrentCategory = num;
			}
			this.OnSearchTextChange(filterText);
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000E5678 File Offset: 0x000E3878
		public void updateCategoryButtonBar()
		{
			if (this.categoryBarButtons == null)
			{
				this.categoryBarButtons = new List<FilterBarCategoryButtonData>(this.categoryInfos.Count);
				this.categoryBarButtons.AddRange(this.categoryInfos.Select<JournalStatusScreen.CategoryInfo, FilterBarCategoryButtonData>((JournalStatusScreen.CategoryInfo s) => new FilterBarCategoryButtonData
				{
					category = s.Name
				}));
				for (int i = 0; i < this.categoryBarButtons.Count; i++)
				{
					int n = i;
					this.categoryBarButtons[i].onSelect = delegate(string c)
					{
						this.filterBar.CategorySelected(this.categoryBarButtons[n].category);
					};
				}
			}
			int j = 0;
			int count = this.categoryBarButtons.Count;
			while (j < count)
			{
				FilterBarCategoryButtonData filterBarCategoryButtonData = this.categoryBarButtons[j];
				filterBarCategoryButtonData.tooltip = JournalScreen.GetTabDisplayName(filterBarCategoryButtonData.category);
				j++;
			}
			this.categoryBar.SetCategoriesViaButtonBarData(this.categoryBarButtons);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000E5768 File Offset: 0x000E3968
		public void HandleSelectItem(FrameworkDataElement element)
		{
			JournalLineData journalLineData = element as JournalLineData;
			if (journalLineData != null)
			{
				JournalMapNote journalMapNote = journalLineData.entry as JournalMapNote;
				if (journalMapNote != null)
				{
					journalMapNote.Tracked = !journalMapNote.Tracked;
					this.UpdateViewFromData();
					ZoneActivatedEvent.Send(The.ZoneManager.GetZone("JoppaWorld"));
					return;
				}
				if (journalLineData.category)
				{
					if (!this.categoryCollapsed.ContainsKey(journalLineData.categoryName))
					{
						this.categoryCollapsed.Add(journalLineData.categoryName, this.currentInfo.UsesSultans);
					}
					this.categoryCollapsed[journalLineData.categoryName] = !this.categoryCollapsed[journalLineData.categoryName];
					this.UpdateViewFromData();
				}
			}
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000E5820 File Offset: 0x000E3A20
		public void HandleHighlightItem(FrameworkDataElement element)
		{
			JournalLineData journalLineData = element as JournalLineData;
			if (journalLineData != null)
			{
				if (this.currentInfo.UsesSultans)
				{
					this.highlightSultanPeriod(journalLineData.sultanPeriod);
				}
				JournalAccomplishment journalAccomplishment = journalLineData.entry as JournalAccomplishment;
				if (journalAccomplishment != null)
				{
					this.marginaliaController.RenderAccomplishment(journalAccomplishment);
					return;
				}
				if (journalLineData.mapTarget == null)
				{
					this.mapController.SetHighlights(null);
				}
				else
				{
					this.mapController.SetHighlights(new List<Location2D> { journalLineData.mapTarget });
				}
				if (this.currentInfo.UsesMap)
				{
					if (this.currentMapHighlight == element)
					{
						return;
					}
					this.currentMapHighlight = element;
					this.mapController.RefreshMap();
				}
				else
				{
					this.mapController.SetHighlights(null);
					this.mapController.RefreshMap();
				}
				if (this.currentInfo.UsesMap && journalLineData.mapTarget != null)
				{
					this.mapController.SetTarget(journalLineData.mapTarget.X, journalLineData.mapTarget.Y);
				}
			}
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000E5925 File Offset: 0x000E3B25
		public void Update()
		{
			if (this.lastClass != Media.sizeClass)
			{
				this.lastClass = Media.sizeClass;
				this.statueLayoutGroup.spacing = (float)((this.lastClass < Media.SizeClass.Medium) ? (-32) : 16);
			}
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000E595C File Offset: 0x000E3B5C
		public async void HandleInsert()
		{
			Debug.Log("journal insert");
			if (this.currentInfo.CanDelete)
			{
				await APIDispatch.RunAndWaitAsync<bool>(() => JournalScreen.HandleInsert(this.currentInfo.Name, this.GO));
				this.UpdateViewFromData();
			}
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000E5994 File Offset: 0x000E3B94
		public async void HandleDelete(JournalLineData line)
		{
			Debug.Log("journal delete");
			if (this.currentInfo.CanDelete)
			{
				await APIDispatch.RunAndWaitAsync<bool>(() => JournalScreen.HandleDelete(this.currentInfo.Name, line.entry, this.GO));
				this.UpdateViewFromData();
			}
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000E59D3 File Offset: 0x000E3BD3
		public void OnSearchTextChange(string text)
		{
			this.filterText = text;
			if (string.IsNullOrWhiteSpace(text))
			{
				this.searcher.searchText = text;
			}
			else
			{
				this.searcher.searchText = text.ToLower();
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000E5A09 File Offset: 0x000E3C09
		public void HandleVPositive()
		{
			this.categoryCollapsed.Clear();
			this.UpdateViewFromData();
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000E5A1C File Offset: 0x000E3C1C
		public void HandleVNegative()
		{
			this.categoryCollapsed.Clear();
			foreach (string text in this.usedCategories)
			{
				this.categoryCollapsed[text] = true;
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000E5A88 File Offset: 0x000E3C88
		public override NavigationContext ShowScreen(global::XRL.World.GameObject GO, StatusScreensScreen parent)
		{
			this.currentMapHighlight = null;
			this.categoryBar = parent.filterBar;
			this.categoryBar.SingleCategoryOnly = true;
			FilterBar filterBar = this.categoryBar;
			if (filterBar.enabledCategories == null)
			{
				filterBar.enabledCategories = new List<string>();
			}
			this.categoryBar.enabledCategories.Clear();
			List<string> enabledCategories = this.categoryBar.enabledCategories;
			JournalStatusScreen.CategoryInfo currentInfo = this.currentInfo;
			enabledCategories.Add(((currentInfo != null) ? currentInfo.Name : null) ?? JournalScreen.STR_LOCATIONS);
			this.GO = GO;
			this.filterBar = parent.filterBar;
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Add(this.controller.scrollContext);
			this.vertNav.wraps = false;
			this.controller.onSelected.RemoveAllListeners();
			this.controller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectItem));
			this.controller.onHighlight.RemoveAllListeners();
			this.controller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightItem));
			this.controller.scrollContext.wraps = false;
			this.controller.selectedPosition = 0;
			this.controller.scrollContext.ActivateAndEnable();
			this.controller.scrollContext.SetAxis(InputAxisTypes.NavigationYAxis, false);
			ScrollContext<FrameworkDataElement, NavigationContext> scrollContext = this.controller.scrollContext;
			if (scrollContext.commandHandlers == null)
			{
				scrollContext.commandHandlers = new Dictionary<string, Action>();
			}
			this.controller.scrollContext.commandHandlers["V Positive"] = new Action(this.HandleVPositive);
			this.controller.scrollContext.commandHandlers["V Negative"] = new Action(this.HandleVNegative);
			ScrollContext<NavigationContext> scrollContext2 = this.vertNav;
			if (scrollContext2.axisHandlers == null)
			{
				scrollContext2.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			scrollContext2 = this.vertNav;
			if (scrollContext2.menuOptionDescriptions == null)
			{
				scrollContext2.menuOptionDescriptions = new List<MenuOption> { this.CMD_INSERT, this.CMD_DELETE };
			}
			scrollContext2 = this.vertNav;
			if (scrollContext2.commandHandlers == null)
			{
				scrollContext2.commandHandlers = new Dictionary<string, Action>();
			}
			this.vertNav.commandHandlers["CmdInsert"] = new Action(this.HandleInsert);
			this.vertNav.Setup();
			this.OnSearchTextChange(null);
			base.ShowScreen(GO, parent);
			return this.vertNav;
		}

		// Token: 0x04001732 RID: 5938
		public global::UnityEngine.GameObject sultanScroller;

		// Token: 0x04001733 RID: 5939
		public List<JournalSultanStatueLine> sultanStatues = new List<JournalSultanStatueLine>();

		// Token: 0x04001734 RID: 5940
		public List<int> sultanStatuePeriod = new List<int>();

		// Token: 0x04001735 RID: 5941
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001736 RID: 5942
		public FilterBar categoryBar;

		// Token: 0x04001737 RID: 5943
		public FrameworkScroller controller;

		// Token: 0x04001738 RID: 5944
		private Renderable icon;

		// Token: 0x04001739 RID: 5945
		public int CurrentCategory;

		// Token: 0x0400173A RID: 5946
		public int MaxCategory = 6;

		// Token: 0x0400173B RID: 5947
		private List<string> usedCategories = new List<string>(64);

		// Token: 0x0400173C RID: 5948
		private List<JournalLineData> lineData = new List<JournalLineData>();

		// Token: 0x0400173D RID: 5949
		private List<JournalLineData> rawLineData = new List<JournalLineData>();

		// Token: 0x0400173E RID: 5950
		private Dictionary<string, List<JournalLineData>> categories = new Dictionary<string, List<JournalLineData>>();

		// Token: 0x0400173F RID: 5951
		[GameBasedStaticCache(true, false)]
		public static int? LastCategory = null;

		// Token: 0x04001740 RID: 5952
		public Dictionary<string, bool> categoryCollapsed = new Dictionary<string, bool>();

		// Token: 0x04001741 RID: 5953
		private IEnumerable<JournalLineData> searchResult;

		// Token: 0x04001742 RID: 5954
		public global::UnityEngine.GameObject sultanShrineContentRoot;

		// Token: 0x04001743 RID: 5955
		public global::UnityEngine.GameObject sultanShrineLinePrefab;

		// Token: 0x04001744 RID: 5956
		public MapScrollerController marginaliaController;

		// Token: 0x04001745 RID: 5957
		public global::UnityEngine.GameObject marginaliaScroller;

		// Token: 0x04001746 RID: 5958
		public MapScrollerController mapController;

		// Token: 0x04001747 RID: 5959
		public global::UnityEngine.GameObject mapScroller;

		// Token: 0x04001748 RID: 5960
		public static string NO_ENTRIES_TEXT = " No entries found.";

		// Token: 0x04001749 RID: 5961
		public List<JournalStatusScreen.CategoryInfo> categoryInfos = new List<JournalStatusScreen.CategoryInfo>
		{
			new JournalStatusScreen.CategoryInfo
			{
				N = 0,
				Name = JournalScreen.STR_LOCATIONS,
				UsesCategories = true,
				UsesMap = true,
				CanAdd = true,
				CanDelete = true,
				CategoryFor = new Func<JournalLineData, string>(JournalStatusScreen.LocationCategory),
				SortCategoriesAZ = true
			},
			new JournalStatusScreen.CategoryInfo
			{
				N = 1,
				Name = JournalScreen.STR_OBSERVATIONS,
				UsesCategories = true,
				CategoryFor = new Func<JournalLineData, string>(JournalStatusScreen.ObservationCategory),
				SortCategoriesAZ = true
			},
			new JournalStatusScreen.CategoryInfo
			{
				N = 2,
				Name = JournalScreen.STR_SULTANS,
				UsesSultans = true,
				UsesCategories = true,
				CategoryFor = new Func<JournalLineData, string>(JournalStatusScreen.SultanCategory)
			},
			new JournalStatusScreen.CategoryInfo
			{
				N = 3,
				Name = JournalScreen.STR_VILLAGES,
				UsesCategories = true,
				UsesMap = true,
				CategoryFor = new Func<JournalLineData, string>(JournalStatusScreen.VillageCategory),
				SortCategoriesAZ = true
			},
			new JournalStatusScreen.CategoryInfo
			{
				N = 4,
				Name = JournalScreen.STR_CHRONOLOGY,
				UsesMarginalia = true,
				CanAdd = true,
				CanDelete = true,
				UsesCategories = false,
				CategoryFor = new Func<JournalLineData, string>(JournalStatusScreen.AccomplishmentCategory)
			},
			new JournalStatusScreen.CategoryInfo
			{
				N = 5,
				Name = JournalScreen.STR_GENERAL,
				CanAdd = true,
				CanDelete = true,
				SortCategoriesAZ = true
			},
			new JournalStatusScreen.CategoryInfo
			{
				N = 6,
				Name = JournalScreen.STR_RECIPES,
				CanDelete = true
			}
		};

		// Token: 0x0400174A RID: 5962
		public UITextSkin categoryText;

		// Token: 0x0400174B RID: 5963
		public global::UnityEngine.GameObject categoryContainer;

		// Token: 0x0400174C RID: 5964
		private List<FilterBarCategoryButtonData> categoryBarButtons;

		// Token: 0x0400174D RID: 5965
		private FrameworkDataElement currentMapHighlight;

		// Token: 0x0400174E RID: 5966
		public HorizontalLayoutGroup statueLayoutGroup;

		// Token: 0x0400174F RID: 5967
		public Media.SizeClass lastClass = Media.SizeClass.Unset;

		// Token: 0x04001750 RID: 5968
		public MenuOption CMD_INSERT = new MenuOption
		{
			Id = "CmdInsert",
			InputCommand = "CmdInsert",
			Description = "Add"
		};

		// Token: 0x04001751 RID: 5969
		public MenuOption CMD_DELETE = new MenuOption
		{
			Id = "CmdDelete",
			InputCommand = "CmdDelete",
			Description = "Delete"
		};

		// Token: 0x04001752 RID: 5970
		private JournalLineData searcher = new JournalLineData();

		// Token: 0x04001753 RID: 5971
		private FilterBar filterBar;

		// Token: 0x04001754 RID: 5972
		public global::XRL.World.GameObject GO;

		// Token: 0x020018A6 RID: 6310
		public class CategoryInfo
		{
			// Token: 0x04006317 RID: 25367
			public int N;

			// Token: 0x04006318 RID: 25368
			public bool Selected;

			// Token: 0x04006319 RID: 25369
			public string Name;

			// Token: 0x0400631A RID: 25370
			public bool UsesCategories;

			// Token: 0x0400631B RID: 25371
			public bool UsesMarginalia;

			// Token: 0x0400631C RID: 25372
			public bool UsesMap;

			// Token: 0x0400631D RID: 25373
			public bool SortCategoriesAZ;

			// Token: 0x0400631E RID: 25374
			public bool CanAdd;

			// Token: 0x0400631F RID: 25375
			public bool CanDelete;

			// Token: 0x04006320 RID: 25376
			public bool UsesSultans;

			// Token: 0x04006321 RID: 25377
			public Func<JournalLineData, string> CategoryFor;
		}
	}
}
