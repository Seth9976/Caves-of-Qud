using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using XRL.CharacterBuilds;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000463 RID: 1123
	[UIView("FactionsStatusScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menus", UICanvas = "StatusScreens", UICanvasHost = 1)]
	public class FactionsStatusScreen : BaseStatusScreen<FactionsStatusScreen>
	{
		// Token: 0x06002A88 RID: 10888 RVA: 0x000DFF6E File Offset: 0x000DE16E
		public override IRenderable GetTabIcon()
		{
			if (this.icon == null)
			{
				this.icon = new Renderable("Items/sw_unfurled_scroll1.bmp", " ", "&Y", null, 'w');
			}
			return this.icon;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000DFF9B File Offset: 0x000DE19B
		public override string GetTabString()
		{
			Media.SizeClass sizeClass = Media.sizeClass;
			return "Reputation";
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000DFFAC File Offset: 0x000DE1AC
		public void ToggleExpanded(FactionsLineData data)
		{
			string id = data.id;
			if (this.CollapsedFactions.Contains(id))
			{
				this.CollapsedFactions.Remove(id);
			}
			else
			{
				this.CollapsedFactions.Add(id);
			}
			data.expanded = !this.CollapsedFactions.Contains(id);
			this.UpdateViewFromData();
			this.parent.updateMenuBar = true;
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000E0014 File Offset: 0x000DE214
		public void ExpandAll()
		{
			foreach (FactionsLineData factionsLineData in this.dataList)
			{
				this.CollapsedFactions.Remove(factionsLineData.id);
				factionsLineData.expanded = true;
			}
			FrameworkScroller frameworkScroller = this.controller;
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = null;
			IEnumerable<FactionsLineData> enumerable = this.sortedData;
			frameworkScroller.BeforeShow(embarkBuilderModuleWindowDescriptor, enumerable ?? this.dataList);
			this.parent.updateMenuBar = true;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000E00A0 File Offset: 0x000DE2A0
		public void CollapseAll()
		{
			foreach (FactionsLineData factionsLineData in this.dataList)
			{
				this.CollapsedFactions.Add(factionsLineData.id);
				factionsLineData.expanded = false;
			}
			FrameworkScroller frameworkScroller = this.controller;
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = null;
			IEnumerable<FactionsLineData> enumerable = this.sortedData;
			frameworkScroller.BeforeShow(embarkBuilderModuleWindowDescriptor, enumerable ?? this.dataList);
			this.parent.updateMenuBar = true;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000E012C File Offset: 0x000DE32C
		public override void HandleMenuOption(FrameworkDataElement data)
		{
			if (data == this.EXPAND_ALL)
			{
				this.ExpandAll();
				return;
			}
			if (data == this.COLLAPSE_ALL)
			{
				this.CollapseAll();
				return;
			}
			base.HandleMenuOption(data);
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000E0155 File Offset: 0x000DE355
		public void OnSearchTextChange(string text)
		{
			this.filterText = text;
			if (text.IsNullOrEmpty())
			{
				this.searcher.searchText = text;
			}
			else
			{
				this.searcher.searchText = text.ToLower();
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000E018B File Offset: 0x000DE38B
		public void ExpansionUpdated()
		{
			this.controller.BeforeShow(null, this.dataList);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000E01A0 File Offset: 0x000DE3A0
		public override void UpdateViewFromData()
		{
			this.rawData.ForEach(delegate(FactionsLineData e)
			{
				e.free();
			});
			this.rawData.Clear();
			this.rawData.AddRange(from f in FactionsScreen.getFactionsByName().Select<string, Faction>(new Func<string, Faction>(Factions.Get))
				where f.Visible
				select f into faction
				select PooledFrameworkDataElement<FactionsLineData>.next().set(faction.Name, ColorUtility.CapitalizeExceptFormatting(faction.GetFormattedName()), faction.Emblem, !this.CollapsedFactions.Contains(faction.Name)));
			if (this.filterText.IsNullOrEmpty())
			{
				this.dataList = this.rawData;
			}
			else
			{
				this.dataList = from i in Process.ExtractTop<FactionsLineData>(this.searcher, this.rawData, (FactionsLineData i) => i.searchText, null, this.rawData.Count<FactionsLineData>(), 50)
					select i.Value;
			}
			this.sortedData.Clear();
			this.sortedData.AddRange(this.dataList);
			if (this.SortMode == 0)
			{
				this.sortedData.Sort((FactionsLineData a, FactionsLineData b) => b.rep.CompareTo(a.rep));
			}
			else if (this.SortMode == 1)
			{
				this.sortedData.Sort((FactionsLineData a, FactionsLineData b) => a.rep.CompareTo(b.rep));
			}
			else
			{
				this.sortedData.Sort((FactionsLineData a, FactionsLineData b) => a.name.CompareTo(b.name));
			}
			this.controller.BeforeShow(this.sortedData);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000E037C File Offset: 0x000DE57C
		public async void HandleCmdOptions()
		{
			int num = await Popup.PickOptionAsync("Sort By", null, "", new string[]
			{
				(this.SortMode == 0) ? "{{W|Highest reputation}}" : "{{y|Highest reputation}}",
				(this.SortMode == 1) ? "{{W|Lowest reputation}}" : "{{y|Lowest reputation}}",
				(this.SortMode == 2) ? "{{W|Alphabetical}}" : "{{y|Alphabetical}}"
			}, null, null, null, null, 0, 60, this.SortMode, -1, false, true, false, true);
			if (num >= 0)
			{
				this.SortMode = num;
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000E03B3 File Offset: 0x000DE5B3
		public override void FilterUpdated(string filterText)
		{
			this.OnSearchTextChange(filterText);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000E03BC File Offset: 0x000DE5BC
		public override NavigationContext ShowScreen(GameObject GO, StatusScreensScreen parent)
		{
			this.statusScreensScreen = parent;
			this.filterBar = parent.filterBar;
			this.parent = parent;
			this.context.screen = this;
			this.controller.onSelected.RemoveAllListeners();
			this.controller.onHighlight.RemoveAllListeners();
			this.controller.scrollContext.wraps = false;
			this.context.proxyTo = this.controller.scrollContext;
			this.controller.scrollContext.parentContext = this.context;
			FactionsStatusScreen.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			this.context.axisHandlers[InputAxisTypes.NavigationVAxis] = XRL.UI.Framework.Event.Helpers.Handle(XRL.UI.Framework.Event.Helpers.Axis(new Action(this.ExpandAll), new Action(this.CollapseAll), null));
			context = this.context;
			if (context.menuOptionDescriptions == null)
			{
				context.menuOptionDescriptions = new List<MenuOption>
				{
					this.EXPAND_ALL,
					this.COLLAPSE_ALL,
					new MenuOption
					{
						Id = "CmdOptions",
						InputCommand = "CmdOptions",
						Description = "Sort Options"
					},
					new MenuOption
					{
						Id = "CmdFilter",
						InputCommand = "CmdFilter",
						Description = "Filter"
					}
				};
			}
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>();
			}
			this.context.commandHandlers["CmdOptions"] = delegate
			{
				this.HandleCmdOptions();
			};
			this.FilterUpdated(null);
			this.controller.selectedPosition = 0;
			this.controller.scrollContext.ActivateAndEnable();
			base.ShowScreen(GO, parent);
			return this.context;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000E0594 File Offset: 0x000DE794
		public override void Prewarm()
		{
			List<FactionsLineData> list = new List<FactionsLineData>();
			for (int i = 0; i < 40; i++)
			{
				list.Add(new FactionsLineData
				{
					id = "Bears",
					label = "label",
					expanded = true,
					name = "Bears",
					rep = 600
				});
			}
			this.controller.BeforeShow(list);
		}

		// Token: 0x04001683 RID: 5763
		public FrameworkScroller controller;

		// Token: 0x04001684 RID: 5764
		private Renderable icon;

		// Token: 0x04001685 RID: 5765
		public HashSet<string> CollapsedFactions = new HashSet<string>();

		// Token: 0x04001686 RID: 5766
		public FactionsStatusScreen.Context context = new FactionsStatusScreen.Context();

		// Token: 0x04001687 RID: 5767
		public IEnumerable<FactionsLineData> dataList;

		// Token: 0x04001688 RID: 5768
		public MenuOption EXPAND_ALL = new MenuOption
		{
			Id = "V Positive",
			InputCommand = "V Positive",
			Description = "Expand All"
		};

		// Token: 0x04001689 RID: 5769
		public MenuOption COLLAPSE_ALL = new MenuOption
		{
			Id = "V Negative",
			InputCommand = "V Negative",
			Description = "Collapse All"
		};

		// Token: 0x0400168A RID: 5770
		protected StatusScreensScreen parent;

		// Token: 0x0400168B RID: 5771
		private FactionsLineData searcher = new FactionsLineData();

		// Token: 0x0400168C RID: 5772
		private List<FactionsLineData> sortedData = new List<FactionsLineData>();

		// Token: 0x0400168D RID: 5773
		private List<FactionsLineData> rawData = new List<FactionsLineData>();

		// Token: 0x0400168E RID: 5774
		private StatusScreensScreen statusScreensScreen;

		// Token: 0x0400168F RID: 5775
		private FilterBar filterBar;

		// Token: 0x04001690 RID: 5776
		public int SortMode = 2;

		// Token: 0x02001882 RID: 6274
		public class Context : ProxyNavigationContext
		{
			// Token: 0x04006299 RID: 25241
			public FactionsStatusScreen screen;
		}
	}
}
