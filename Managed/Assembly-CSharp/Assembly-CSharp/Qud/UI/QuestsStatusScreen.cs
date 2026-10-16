using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using Genkit;
using Qud.API;
using UnityEngine;
using UnityEngine.Events;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000474 RID: 1140
	[UIView("QuestsStatusScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menus", UICanvas = "StatusScreens", UICanvasHost = 1)]
	public class QuestsStatusScreen : BaseStatusScreen<QuestsStatusScreen>
	{
		// Token: 0x06002B68 RID: 11112 RVA: 0x000E739E File Offset: 0x000E559E
		public override IRenderable GetTabIcon()
		{
			if (this.icon == null)
			{
				this.icon = new Renderable("Items/sw_scroll1.bmp", " ", "&y", null, 'Y');
			}
			return this.icon;
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000E73CB File Offset: 0x000E55CB
		public override string GetTabString()
		{
			return "Quests";
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000E73D2 File Offset: 0x000E55D2
		public void HandleVPositive()
		{
			this.CollapsedEntries.Clear();
			this.UpdateViewFromData();
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000E73E8 File Offset: 0x000E55E8
		public void HandleVNegative()
		{
			this.CollapsedEntries.Clear();
			foreach (Quest quest in from q in QuestsAPI.allQuests()
				where !q.Finished
				select q)
			{
				this.CollapsedEntries.Add(quest.ID);
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000E7474 File Offset: 0x000E5674
		public void HandleSelectItem(FrameworkDataElement element)
		{
			QuestsLineData questsLineData = element as QuestsLineData;
			if (questsLineData != null)
			{
				questsLineData.expanded = !questsLineData.expanded;
				if (!questsLineData.expanded)
				{
					this.CollapsedEntries.Add(questsLineData.quest.ID);
				}
				if (questsLineData.expanded)
				{
					this.CollapsedEntries.Remove(questsLineData.quest.ID);
				}
				this.UpdateViewFromData();
			}
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000E74E0 File Offset: 0x000E56E0
		public void HandleHighlightItem(FrameworkDataElement element)
		{
			QuestsLineData questsLineData = element as QuestsLineData;
			if (questsLineData != null && questsLineData != null && questsLineData.quest != null && questsLineData.quest.QuestGiverLocationZoneID != null)
			{
				Location2D location2D = ZoneManager.GetWorldMapLocationForZoneID(questsLineData.quest.QuestGiverLocationZoneID);
				if (location2D == null)
				{
					location2D = Location2D.Get(0, 0);
				}
				this.mapController.SetTarget(location2D.X, location2D.Y);
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000E7548 File Offset: 0x000E5748
		public void OnSearchTextChange(string text)
		{
			this.searcher.searchText = ((text != null) ? text.ToLower() : null);
			this.UpdateViewFromData();
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000E7568 File Offset: 0x000E5768
		public override void UpdateViewFromData()
		{
			foreach (QuestsLineData questsLineData in this.searchResult)
			{
				questsLineData.free();
			}
			this.searchResult.Clear();
			IEnumerable<QuestsLineData> enumerable = from q in QuestsAPI.allQuests()
				where !q2.Finished
				select PooledFrameworkDataElement<QuestsLineData>.next().set(q2, !this.CollapsedEntries.Contains(q2.ID));
			if (string.IsNullOrEmpty(this.searcher.searchText))
			{
				this.searchResult.AddRange(enumerable);
			}
			else
			{
				this.searchResult.AddRange(from m in Process.ExtractTop<QuestsLineData>(this.searcher, enumerable, (QuestsLineData i) => i.searchText, null, enumerable.Count<QuestsLineData>(), 50)
					select m.Value);
			}
			IEnumerable<Quest> enumerable2 = this.searchResult.Select<QuestsLineData, Quest>((QuestsLineData d) => d.quest);
			HashSet<string> hashSet = new HashSet<string>();
			List<MapScrollerController.MapPinData> list = new List<MapScrollerController.MapPinData>();
			using (IEnumerator<Quest> enumerator2 = enumerable2.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Quest q2 = enumerator2.Current;
					if (!hashSet.Contains(q2.QuestGiverLocationZoneID))
					{
						hashSet.Add(q2.QuestGiverLocationZoneID);
						MapScrollerController.MapPinData mapPinData = new MapScrollerController.MapPinData();
						Location2D location2D = ZoneManager.GetWorldMapLocationForZoneID(q2.QuestGiverLocationZoneID);
						if (location2D == null)
						{
							location2D = Location2D.Get(0, 0);
						}
						if (location2D != null)
						{
							mapPinData.x = location2D.X;
							mapPinData.y = location2D.Y;
							mapPinData.title = "{{W|" + q2.QuestGiverLocationName + "}}";
							StringBuilder stringBuilder = new StringBuilder();
							IEnumerable<Quest> enumerable3 = enumerable2;
							Func<Quest, bool> func;
							Func<Quest, bool> <>9__8;
							if ((func = <>9__8) == null)
							{
								func = (<>9__8 = (Quest l) => l.QuestGiverLocationName == q2.QuestGiverLocationName);
							}
							foreach (Quest quest in enumerable3.Where<Quest>(func))
							{
								if (stringBuilder.Length != 0)
								{
									stringBuilder.Append("\n");
								}
								stringBuilder.Append("{{B|quest:}} " + ConsoleLib.Console.ColorUtility.StripFormatting(quest.DisplayName));
							}
							mapPinData.details = stringBuilder.ToString();
							list.Add(mapPinData);
						}
					}
				}
			}
			if (this.searchResult.Count == 0)
			{
				this.searchResult.Add(new QuestsLineData().set(null, false));
			}
			this.mapController.SetHighlights(from q in this.searchResult.Select<QuestsLineData, Quest>(delegate(QuestsLineData d)
				{
					if (d == null)
					{
						return null;
					}
					return d.quest;
				})
				where q != null && !q.Finished && q.QuestGiverLocationZoneID != null
				select ZoneManager.GetWorldMapLocationForZoneID((q != null) ? q.QuestGiverLocationZoneID : null) ?? Location2D.Get(0, 0));
			this.controller.BeforeShow(this.searchResult);
			this.mapController.RefreshMap();
			this.mapController.SetPins(list);
			this.mapController.mapTarget.gameObject.SetActive(false);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000E7958 File Offset: 0x000E5B58
		public override void FilterUpdated(string filterText)
		{
			this.OnSearchTextChange(filterText);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000E7964 File Offset: 0x000E5B64
		public override NavigationContext ShowScreen(XRL.World.GameObject GO, StatusScreensScreen parent)
		{
			SingletonWindowBase<QuestsStatusScreen>.instance = this;
			this.searcher.searchText = "";
			this.filterBar = parent.filterBar;
			this.controller.onSelected.RemoveAllListeners();
			this.controller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectItem));
			this.controller.onHighlight.RemoveAllListeners();
			this.controller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightItem));
			this.controller.scrollContext.wraps = false;
			this.UpdateViewFromData();
			this.controller.selectedPosition = 0;
			this.controller.scrollContext.ActivateAndEnable();
			ScrollContext<FrameworkDataElement, NavigationContext> scrollContext = this.controller.scrollContext;
			if (scrollContext.commandHandlers == null)
			{
				scrollContext.commandHandlers = new Dictionary<string, Action>();
			}
			this.controller.scrollContext.commandHandlers["V Positive"] = new Action(this.HandleVPositive);
			this.controller.scrollContext.commandHandlers["V Negative"] = new Action(this.HandleVNegative);
			base.ShowScreen(GO, parent);
			return this.controller.scrollContext;
		}

		// Token: 0x0400178F RID: 6031
		public FrameworkScroller controller;

		// Token: 0x04001790 RID: 6032
		private Renderable icon;

		// Token: 0x04001791 RID: 6033
		public HashSet<string> CollapsedEntries = new HashSet<string>();

		// Token: 0x04001792 RID: 6034
		public QuestsLineData searcher = new QuestsLineData();

		// Token: 0x04001793 RID: 6035
		private List<QuestsLineData> searchResult = new List<QuestsLineData>();

		// Token: 0x04001794 RID: 6036
		public MapScrollerController mapController;

		// Token: 0x04001795 RID: 6037
		public global::UnityEngine.GameObject mapScroller;

		// Token: 0x04001796 RID: 6038
		private FilterBar filterBar;
	}
}
