using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000471 RID: 1137
	[UIView("MessageLogStatusScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menus", UICanvas = "StatusScreens", UICanvasHost = 1)]
	public class MessageLogStatusScreen : BaseStatusScreen<MessageLogStatusScreen>
	{
		// Token: 0x06002B4F RID: 11087 RVA: 0x000E676D File Offset: 0x000E496D
		public override IRenderable GetTabIcon()
		{
			if (this.icon == null)
			{
				this.icon = new Renderable("Items/sw_crayons.bmp", " ", "&w", null, 'R');
			}
			return this.icon;
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000E679A File Offset: 0x000E499A
		public override string GetTabString()
		{
			if (Media.sizeClass < Media.SizeClass.Medium)
			{
				return "Log";
			}
			return "Message Log";
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000E67B0 File Offset: 0x000E49B0
		public void HandleNavigationXAxis()
		{
			NavigationController.currentEvent.handled = true;
			MetricsManager.LogInfo(string.Format("NAVCATEGORY {0}", NavigationController.currentEvent.axisValue));
			int currentCategory = this.CurrentCategory;
			int? axisValue = NavigationController.currentEvent.axisValue;
			int num = 0;
			this.CurrentCategory = currentCategory + (((axisValue.GetValueOrDefault() > num) & (axisValue != null)) ? 1 : (-1));
			if (this.CurrentCategory < 0)
			{
				this.CurrentCategory = this.MaxCategory;
			}
			if (this.CurrentCategory > this.MaxCategory)
			{
				this.CurrentCategory = 0;
			}
			this.controller.BeforeShow(The.Game.Player.Messages.Messages.Select<string, MessageLogLineData>((string t) => new MessageLogLineData().set(t)));
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000E6885 File Offset: 0x000E4A85
		public void updateCategoryButtonBar()
		{
			this.categoryBar.SetButtons(this.categoryInfos.Select<MessageLogStatusScreen.CategoryInfo, ButtonBar.ButtonBarButtonData>((MessageLogStatusScreen.CategoryInfo s) => new ButtonBar.ButtonBarButtonData
			{
				label = s.Name,
				Highlighted = ((s.N == this.CurrentCategory) ? ButtonBar.ButtonBarButtonData.HighlightState.Highlighted : ButtonBar.ButtonBarButtonData.HighlightState.NotHighlighted)
			}));
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000E68A9 File Offset: 0x000E4AA9
		public override void FilterUpdated(string filterText)
		{
			this.OnSearchTextChange(filterText);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000E68B2 File Offset: 0x000E4AB2
		public void OnSearchTextChange(string text)
		{
			this.filterText = text;
			if (string.IsNullOrWhiteSpace(text))
			{
				this.searcher.sortText = text;
			}
			else
			{
				this.searcher.sortText = text.ToLower();
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000E68E8 File Offset: 0x000E4AE8
		public override void UpdateViewFromData()
		{
			global::XRL.World.Event.ResetToPin();
			this.logLines.ForEach(delegate(MessageLogLineData l)
			{
				l.free();
			});
			this.logLines.Clear();
			foreach (string text in The.Game.Player.Messages.Messages)
			{
				this.logLines.AddRange(from t in text.Split('\n', StringSplitOptions.None)
					select PooledFrameworkDataElement<MessageLogLineData>.next().set(t));
			}
			IEnumerable<MessageLogLineData> enumerable;
			if (string.IsNullOrEmpty(this.searcher.sortText))
			{
				enumerable = this.logLines;
			}
			else
			{
				enumerable = from m in Process.ExtractTop<MessageLogLineData>(this.searcher, this.logLines, (MessageLogLineData i) => i.sortText, null, this.logLines.Count<MessageLogLineData>(), 50)
					select m.Value;
			}
			this.controller.BeforeShow(enumerable);
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000E6A40 File Offset: 0x000E4C40
		public override NavigationContext ShowScreen(GameObject GO, StatusScreensScreen parent)
		{
			this.filterBar = parent.filterBar;
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Add(this.controller.scrollContext);
			this.vertNav.wraps = false;
			this.vertNav.Setup();
			this.controller.onSelected.RemoveAllListeners();
			this.controller.onHighlight.RemoveAllListeners();
			this.controller.scrollContext.wraps = false;
			base.ShowScreen(GO, parent);
			this.UpdateViewFromData();
			this.controller.scrollContext.selectedPosition = this.controller.choices.Count - 1;
			this.controller.scrollContext.ActivateAndEnable();
			return this.vertNav;
		}

		// Token: 0x04001773 RID: 6003
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001774 RID: 6004
		public ButtonBar categoryBar;

		// Token: 0x04001775 RID: 6005
		public FrameworkScroller controller;

		// Token: 0x04001776 RID: 6006
		private Renderable icon;

		// Token: 0x04001777 RID: 6007
		public int CurrentCategory;

		// Token: 0x04001778 RID: 6008
		public int MaxCategory = 6;

		// Token: 0x04001779 RID: 6009
		public List<MessageLogStatusScreen.CategoryInfo> categoryInfos = new List<MessageLogStatusScreen.CategoryInfo>
		{
			new MessageLogStatusScreen.CategoryInfo
			{
				N = 0,
				Name = JournalScreen.STR_OBSERVATIONS
			},
			new MessageLogStatusScreen.CategoryInfo
			{
				N = 1,
				Name = JournalScreen.STR_LOCATIONS
			},
			new MessageLogStatusScreen.CategoryInfo
			{
				N = 2,
				Name = JournalScreen.STR_SULTANS
			},
			new MessageLogStatusScreen.CategoryInfo
			{
				N = 3,
				Name = JournalScreen.STR_VILLAGES
			},
			new MessageLogStatusScreen.CategoryInfo
			{
				N = 4,
				Name = JournalScreen.STR_CHRONOLOGY
			},
			new MessageLogStatusScreen.CategoryInfo
			{
				N = 5,
				Name = JournalScreen.STR_GENERAL
			},
			new MessageLogStatusScreen.CategoryInfo
			{
				N = 6,
				Name = JournalScreen.STR_RECIPES
			}
		};

		// Token: 0x0400177A RID: 6010
		public string SelectedSubCategory;

		// Token: 0x0400177B RID: 6011
		private MessageLogLineData searcher = new MessageLogLineData();

		// Token: 0x0400177C RID: 6012
		private List<MessageLogLineData> logLines = new List<MessageLogLineData>();

		// Token: 0x0400177D RID: 6013
		private FilterBar filterBar;

		// Token: 0x020018AE RID: 6318
		public class CategoryInfo
		{
			// Token: 0x04006337 RID: 25399
			public int N;

			// Token: 0x04006338 RID: 25400
			public bool Selected;

			// Token: 0x04006339 RID: 25401
			public string Name;
		}
	}
}
