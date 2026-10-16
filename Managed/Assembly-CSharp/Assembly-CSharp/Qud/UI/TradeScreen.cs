using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using UnityEngine;
using UnityEngine.Events;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Parts;

namespace Qud.UI
{
	// Token: 0x02000484 RID: 1156
	[UIView("ModernTrade", false, false, false, null, null, false, 0, false, NavCategory = "Trade", UICanvas = "Trade", UICanvasHost = 1)]
	public class TradeScreen : SingletonWindowBase<TradeScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x000ED800 File Offset: 0x000EBA00
		public static string TradeScreenVerb
		{
			get
			{
				if (TradeUI.ScreenMode != TradeUI.TradeScreenMode.Trade || TradeUI.costMultiple <= 0f)
				{
					return "transfer";
				}
				return "trade";
			}
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000ED820 File Offset: 0x000EBA20
		public async void HandleTradeSome(TradeLine line)
		{
			int? num = await Popup.AskNumberAsync("Add how many " + line.context.data.go.DisplayName + " to trade.", this.howManySelected(line.context.data.go), 0, line.context.data.go.Count, "", false);
			if (num != null)
			{
				TradeLineData tradeLineData = line.context.data;
				tradeLineData.numberSelected = await this.setHowManySelected(line.context.data.go, num.Value);
				tradeLineData = null;
				line.setData(line.context.data);
				this.UpdateTotals();
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000ED85F File Offset: 0x000EBA5F
		public int howManySelected(global::XRL.World.GameObject go)
		{
			return TradeScreen.NumberSelected[this.ObjectSide[go]][this.ObjectIndex[go]];
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000ED880 File Offset: 0x000EBA80
		public bool IsGoingToWarn(global::XRL.World.GameObject go)
		{
			return !this.importantWarnedObjects.Contains(go) && go.ShouldConfirmUseImportant(The.Player);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000ED8A4 File Offset: 0x000EBAA4
		public async Task<int> setHowManySelected(global::XRL.World.GameObject go, int total)
		{
			if (TradeUI.ScreenMode == TradeUI.TradeScreenMode.Trade && !this.importantWarnedObjects.Contains(go) && go.ShouldConfirmUseImportant(The.Player) && this.ObjectSide[go] == 1)
			{
				TaskAwaiter<bool> taskAwaiter = go.ConfirmUseImportantAsync(null, TradeScreen.TradeScreenVerb, null, -1).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (!taskAwaiter.GetResult())
				{
					return this.howManySelected(go);
				}
				this.importantWarnedObjects.Add(go);
			}
			TradeScreen.NumberSelected[this.ObjectSide[go]][this.ObjectIndex[go]] = total;
			if (TradeScreen.NumberSelected[this.ObjectSide[go]][this.ObjectIndex[go]] > go.Count)
			{
				TradeScreen.NumberSelected[this.ObjectSide[go]][this.ObjectIndex[go]] = go.Count;
			}
			if (TradeScreen.NumberSelected[this.ObjectSide[go]][this.ObjectIndex[go]] <= 0)
			{
				TradeScreen.NumberSelected[this.ObjectSide[go]][this.ObjectIndex[go]] = 0;
			}
			return this.howManySelected(go);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000ED8F8 File Offset: 0x000EBAF8
		public async Task<int> incrementHowManySelected(global::XRL.World.GameObject go, int delta)
		{
			await this.setHowManySelected(go, this.howManySelected(go) + delta);
			return this.howManySelected(go);
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000ED94C File Offset: 0x000EBB4C
		public bool isCollapsed(int side, string category)
		{
			bool flag;
			return this.categoryCollapsed[side].TryGetValue(category, out flag) && flag;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000ED96E File Offset: 0x000EBB6E
		public void OnSearchTextChange(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				this.searchText = "";
			}
			else
			{
				this.searchText = text;
			}
			this.BeforeShow();
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000ED994 File Offset: 0x000EBB94
		public void SetupContext()
		{
			this.filterBar.SetupContext(false);
			this.filterBar.OnSearchTextChange.RemoveAllListeners();
			this.filterBar.OnSearchTextChange.AddListener(new UnityAction<string>(this.OnSearchTextChange));
			this.filterBar.filtersUpdated = new Action(this.BeforeShow);
			this.filterBar.selectedPosition = 0;
			this.vertNav.parentContext = this.navigationContext;
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Clear();
			this.vertNav.contexts.Add(this.filterBar.scrollContext);
			this.vertNav.contexts.Add(this.horizNav);
			this.vertNav.contexts.Add(this.filterBar.rootContext);
			this.horizNav.parentContext = this.vertNav;
			this.horizNav.contexts.Clear();
			this.horizNav.contexts.Add(this.scrollerControllers[0].scrollContext);
			this.horizNav.contexts.Add(this.scrollerControllers[1].scrollContext);
			this.horizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			TradeScreen.Context context = this.navigationContext;
			if (context.buttonHandlers == null)
			{
				context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.CancelButton,
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Cancel))
				} };
			}
			context = this.navigationContext;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>
				{
					{
						TradeScreen.SET_FILTER.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(TradeScreen.SET_FILTER);
						})
					},
					{
						TradeScreen.TOGGLE_SORT.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(TradeScreen.TOGGLE_SORT);
						})
					},
					{
						TradeScreen.OFFER_TRADE.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(TradeScreen.OFFER_TRADE);
						})
					},
					{
						TradeScreen.ADD_ONE.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(TradeScreen.ADD_ONE);
						})
					},
					{
						TradeScreen.REMOVE_ONE.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(TradeScreen.REMOVE_ONE);
						})
					},
					{
						TradeScreen.TOGGLE_ALL.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(TradeScreen.TOGGLE_ALL);
						})
					},
					{
						"Category Left",
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.filterBar.CategoryLeft();
						})
					},
					{
						"Category Right",
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.filterBar.CategoryRight();
						})
					}
				};
			}
			TradeScreen.TOGGLE_SORT.Description = TradeScreen.sortModeDescription;
			this.vertNav.Setup();
			this.horizNav.wraps = true;
			this.horizNav.Setup();
			this.vertNav.wraps = true;
			this.vertNav.Setup();
			this.scrollerControllers[0].scrollContext.parentContext = this.horizNav;
			this.scrollerControllers[1].scrollContext.parentContext = this.horizNav;
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x000EDCA4 File Offset: 0x000EBEA4
		public static string sortModeDescription
		{
			get
			{
				return "sort: " + Markup.Color((SingletonWindowBase<TradeScreen>.instance.sortMode == TradeScreen.SortMode.AZ) ? "w" : "y", "a-z") + "/" + Markup.Color((SingletonWindowBase<TradeScreen>.instance.sortMode == TradeScreen.SortMode.Category) ? "w" : "y", "by class");
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000EDD05 File Offset: 0x000EBF05
		public void BackgroundClicked()
		{
			if (this.navigationContext.IsActive(true))
			{
				this.Cancel();
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000EDD1C File Offset: 0x000EBF1C
		protected async Task<TradeUI.OfferStatus> showScreen(global::XRL.World.GameObject trader, float costMultiple = 1f, TradeUI.TradeScreenMode screenMode = TradeUI.TradeScreenMode.Trade)
		{
			The.Player.IsRealityDistortionUsable(null, null);
			this.mode = screenMode;
			TradeScreen.Trader = trader;
			TradeScreen.CostMultiple = costMultiple;
			this.searchText = "";
			this.menucomplete.TrySetCanceled();
			this.menucomplete = new TaskCompletionSource<TradeUI.OfferStatus>();
			if (this.tradeEntries == null)
			{
				this.tradeEntries = new List<TradeEntry>[2];
				this.tradeEntries[0] = new List<TradeEntry>();
				this.tradeEntries[1] = new List<TradeEntry>();
				this.listItems = new List<TradeLineData>[2];
				this.listItems[0] = new List<TradeLineData>();
				this.listItems[1] = new List<TradeLineData>();
				this.usedCategories = new List<string>[2];
				this.usedCategories[0] = new List<string>();
				this.usedCategories[1] = new List<string>();
				this.objectCategories = new Dictionary<string, List<TradeEntry>>[2];
				this.objectCategories[0] = new Dictionary<string, List<TradeEntry>>();
				this.objectCategories[1] = new Dictionary<string, List<TradeEntry>>();
				this.categoryCollapsed = new Dictionary<string, bool>[2];
				this.categoryCollapsed[0] = new Dictionary<string, bool>();
				this.categoryCollapsed[1] = new Dictionary<string, bool>();
			}
			this.ClearAndSetupTradeUI();
			MinEvent.UIHold = true;
			GameManager.Instance.PushGameView("ModernTrade", true);
			TradeUI.OfferStatus offerStatus2;
			try
			{
				await The.UiContext;
				this.importantWarnedObjects.Clear();
				this.filterBar.ResetFilters();
				this.BeforeShow();
				this.Show();
				Canvas.ForceUpdateCanvases();
				if (this.tradeEntries[0].Count > 0)
				{
					this.scrollerControllers[0].scrollContext.ActivateAndEnable();
				}
				else if (this.tradeEntries[1].Count > 0)
				{
					this.scrollerControllers[1].scrollContext.ActivateAndEnable();
				}
				else
				{
					this.filterBar.scrollContext.ActivateAndEnable();
				}
				TradeUI.OfferStatus offerStatus = await this.menucomplete.Task;
				this.Cleanup();
				offerStatus2 = offerStatus;
			}
			finally
			{
				GameManager.Instance.PopGameView(true);
				MinEvent.UIHold = false;
			}
			return offerStatus2;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000EDD78 File Offset: 0x000EBF78
		public void ClearAndSetupTradeUI()
		{
			this.tradeEntries[0].Clear();
			this.tradeEntries[1].Clear();
			this.listItems[0].Clear();
			this.listItems[1].Clear();
			TradeUI.GetObjects(TradeScreen.Trader, this.tradeEntries[0], The.Player, TradeUI.costMultiple);
			TradeUI.GetObjects(The.Player, this.tradeEntries[1], TradeScreen.Trader, TradeUI.costMultiple);
			TradeScreen.NumberSelected[0] = new int[this.tradeEntries[0].Count];
			TradeScreen.NumberSelected[1] = new int[this.tradeEntries[1].Count];
			this.ObjectSide.Clear();
			this.ObjectIndex.Clear();
			for (int i = 0; i <= 1; i++)
			{
				for (int j = 0; j < this.tradeEntries[i].Count; j++)
				{
					if (this.tradeEntries[i][j].GO != null)
					{
						if (this.ObjectSide.ContainsKey(this.tradeEntries[i][j].GO) || this.ObjectIndex.ContainsKey(this.tradeEntries[i][j].GO))
						{
							MetricsManager.LogError("Trade object dupe " + this.tradeEntries[i][j].GO.Blueprint);
							this.tradeEntries[i].RemoveAt(j);
							j--;
						}
						else
						{
							this.ObjectSide.Add(this.tradeEntries[i][j].GO, i);
							this.ObjectIndex.Add(this.tradeEntries[i][j].GO, j);
						}
					}
				}
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000EDF3C File Offset: 0x000EC13C
		public static Task<TradeUI.OfferStatus> show(global::XRL.World.GameObject trader, float costMultiple, TradeUI.TradeScreenMode screenMode)
		{
			return NavigationController.instance.SuspendContextWhile<TradeUI.OfferStatus>(() => SingletonWindowBase<TradeScreen>.instance.showScreen(trader, costMultiple, screenMode));
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000EDF7C File Offset: 0x000EC17C
		public virtual void ResetLayoutFrame()
		{
			if (this.componentsToEnableOnResize != null)
			{
				MonoBehaviour[] array = this.componentsToEnableOnResize;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].enabled = true;
				}
			}
			this.lastStageScale = Options.StageScale;
			this.lastWidth = Screen.width;
			this.layoutGroupFrame = 2;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000EDFCC File Offset: 0x000EC1CC
		public void UpdateViewFromData()
		{
			global::XRL.World.Event.PinCurrentPool();
			ControlManager.GetHotkeySpread(new List<string> { "Menus", "UINav", "Trade", "UI" });
			foreach (List<TradeLineData> list in this.listItems)
			{
				list.ForEach(delegate(TradeLineData i)
				{
					i.free();
				});
				list.Clear();
			}
			List<string> list2 = new List<string>();
			list2.Add("*All");
			List<TradeEntry>[] array2 = this.tradeEntries;
			for (int k = 0; k < array2.Length; k++)
			{
				foreach (TradeEntry tradeEntry in array2[k])
				{
					if (tradeEntry.GO != null)
					{
						string inventoryCategory = tradeEntry.GO.GetInventoryCategory(false);
						if (!list2.Contains(inventoryCategory))
						{
							list2.Add(inventoryCategory);
						}
					}
				}
			}
			int j = 0;
			while (j < 2)
			{
				FrameworkScroller frameworkScroller = this.scrollerControllers[j];
				List<TradeEntry> list3 = this.tradeEntries[j];
				List<string> list4 = this.usedCategories[j];
				Dictionary<string, List<TradeEntry>> dictionary = this.objectCategories[j];
				List<TradeLineData> list5 = this.listItems[j];
				this.filteredEntries.Clear();
				if (this.searchText.IsNullOrEmpty() && this.filterBar.enabledCategories.Count == 1 && this.filterBar.enabledCategories[0] == "*All")
				{
					this.filteredEntries.AddRange(list3);
				}
				else if (this.searchText.IsNullOrEmpty())
				{
					this.filteredEntries.AddRange(list3.Where<TradeEntry>((TradeEntry i) => this.filterBar.enabledCategories.Contains("*All") || this.filterBar.enabledCategories.Contains(i.CategoryName) || (i.GO != null && this.filterBar.enabledCategories.Contains(i.GO.GetInventoryCategory(false)))));
				}
				else
				{
					this.searcher.CategoryName = this.searchText;
					this.filteredEntries.AddRange(from m in Process.ExtractTop<TradeEntry>(this.searcher, list3, (TradeEntry i) => i.NameForSort.ToLower(), null, list3.Count, 50)
						select m.Value into i
						where this.filterBar.enabledCategories.Contains("*All") || this.filterBar.enabledCategories.Contains(i.CategoryName) || (i.GO != null && this.filterBar.enabledCategories.Contains(i.GO.GetInventoryCategory(false)))
						select i);
				}
				if (this.sortMode == TradeScreen.SortMode.Category)
				{
					list4.Clear();
					foreach (KeyValuePair<string, List<TradeEntry>> keyValuePair in dictionary)
					{
						keyValuePair.Value.Clear();
					}
					foreach (TradeEntry tradeEntry2 in this.filteredEntries)
					{
						if (tradeEntry2.GO != null)
						{
							string inventoryCategory2 = tradeEntry2.GO.GetInventoryCategory(false);
							if (!dictionary.ContainsKey(inventoryCategory2))
							{
								dictionary.Add(inventoryCategory2, new List<TradeEntry>());
							}
							dictionary[inventoryCategory2].Add(tradeEntry2);
							if (!list4.Contains(inventoryCategory2))
							{
								list4.Add(inventoryCategory2);
							}
						}
					}
					list4.Sort();
				}
				if (this.sortMode == TradeScreen.SortMode.Category)
				{
					using (List<string>.Enumerator enumerator3 = list4.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							string text = enumerator3.Current;
							list5.Add(PooledFrameworkDataElement<TradeLineData>.next().set(j, TradeLineDataType.Category, TradeLineDataStyle.Interact, null, null, false, 0, text, this.isCollapsed(j, text), false, '\0', null));
							if (!this.isCollapsed(j, text))
							{
								dictionary[text].Sort((TradeEntry a, TradeEntry b) => a.CategoryName.CompareTo(b.CategoryName));
								foreach (TradeEntry tradeEntry3 in dictionary[text])
								{
									List<TradeLineData> list6 = list5;
									TradeLineData tradeLineData = PooledFrameworkDataElement<TradeLineData>.next();
									int num = j;
									TradeLineDataType tradeLineDataType = TradeLineDataType.Item;
									TradeLineDataStyle tradeLineDataStyle = TradeLineDataStyle.Interact;
									global::XRL.World.GameObject go = tradeEntry3.GO;
									TradeEntry tradeEntry4 = tradeEntry3;
									int k = this.howManySelected(tradeEntry3.GO);
									list6.Add(tradeLineData.set(num, tradeLineDataType, tradeLineDataStyle, go, tradeEntry4, j == 0, k, text, false, true, '\0', null));
								}
							}
						}
						goto IL_04F5;
					}
					goto IL_0437;
				}
				goto IL_0437;
				IL_04F5:
				frameworkScroller.BeforeShow(list5);
				frameworkScroller.onSelected.RemoveAllListeners();
				frameworkScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectItem));
				frameworkScroller.onHighlight.RemoveAllListeners();
				frameworkScroller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightObject));
				frameworkScroller.scrollContext.wraps = false;
				foreach (FrameworkUnityScrollChild frameworkUnityScrollChild in frameworkScroller.selectionClones)
				{
					frameworkUnityScrollChild.GetComponent<TradeLine>().screen = this;
				}
				j++;
				continue;
				IL_0437:
				if (this.sortMode == TradeScreen.SortMode.AZ)
				{
					this.filteredEntries.Sort((TradeEntry a, TradeEntry b) => a.NameForSort.CompareTo(b.NameForSort));
					foreach (TradeEntry tradeEntry5 in this.filteredEntries.Where<TradeEntry>((TradeEntry i) => i.GO != null))
					{
						List<TradeLineData> list7 = list5;
						TradeLineData tradeLineData2 = PooledFrameworkDataElement<TradeLineData>.next();
						int num2 = 0;
						TradeLineDataType tradeLineDataType2 = TradeLineDataType.Item;
						TradeLineDataStyle tradeLineDataStyle2 = TradeLineDataStyle.Interact;
						global::XRL.World.GameObject go2 = tradeEntry5.GO;
						TradeEntry tradeEntry6 = tradeEntry5;
						int k = this.howManySelected(tradeEntry5.GO);
						list7.Add(tradeLineData2.set(num2, tradeLineDataType2, tradeLineDataStyle2, go2, tradeEntry6, j == 0, k, null, false, true, '\0', null));
					}
					goto IL_04F5;
				}
				goto IL_04F5;
			}
			this.filterBar.SetCategories(list2);
			this.UpdateTotals();
			this.UpdateMenuBars();
			this.UpdateTitleBars();
			global::XRL.World.Event.ResetToPin();
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000EE5F0 File Offset: 0x000EC7F0
		public void BeforeShow()
		{
			this.ResetLayoutFrame();
			this.SetupContext();
			Rect rect = this.flowCheck.rect;
			this.UpdateViewFromData();
			if (rect.width != this.flowCheck.rect.width || rect.height != this.flowCheck.rect.height)
			{
				Canvas.ForceUpdateCanvases();
				this.UpdateViewFromData();
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000EE660 File Offset: 0x000EC860
		public void UpdateTitleBars()
		{
			if (TradeScreen.Trader != null)
			{
				this.traderIcons[0].FromRenderable(TradeScreen.Trader.RenderForUI("Trade,Title", false), true);
			}
			this.traderIcons[1].FromRenderable(The.Player.RenderForUI("Trade,Title", false), true);
			if (The.Player.Render.getHFlip())
			{
				this.traderIcons[1].transform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else
			{
				this.traderIcons[1].transform.localScale = new Vector3(1f, 1f, 1f);
			}
			if (TradeScreen.Trader != null)
			{
				this.traderNames[0].SetText(TradeScreen.Trader.BaseDisplayName);
			}
			this.traderNames[1].SetText(The.Player.DisplayName);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000EE746 File Offset: 0x000EC946
		public IEnumerable<FrameworkDataElement> yieldMenuOptions()
		{
			foreach (MenuOption menuOption in TradeScreen.getItemMenuOptions)
			{
				yield return menuOption;
			}
			List<MenuOption>.Enumerator enumerator = default(List<MenuOption>.Enumerator);
			if (NavigationController.instance.activeContext is TradeLine.Context)
			{
				foreach (MenuOption menuOption2 in NavigationController.instance.activeContext.menuOptionDescriptions)
				{
					yield return menuOption2;
				}
				enumerator = default(List<MenuOption>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000EE750 File Offset: 0x000EC950
		public void UpdateTotals()
		{
			TradeUI.UpdateTotals(this.Totals, this.Weight, this.tradeEntries, TradeScreen.NumberSelected);
			int carriedWeight = The.Player.GetCarriedWeight();
			int maxCarriedWeight = The.Player.GetMaxCarriedWeight();
			int num = (int)(LiquidVolume.GetLiquid("water").Weight * (double)TradeUI.CalculateTrade(this.Totals[0], this.Totals[1]));
			int num2 = Math.Max(0, carriedWeight + this.Weight[0] - this.Weight[1] - num);
			string text = "K";
			if (num2 > maxCarriedWeight)
			{
				text = "R";
			}
			this.totalLabels[0].SetText("{{B|" + TradeUI.FormatPrice(this.Totals[0], TradeScreen.CostMultiple) + " drams →}}");
			this.totalLabels[1].SetText("{{B|← " + TradeUI.FormatPrice(this.Totals[1], TradeScreen.CostMultiple) + " drams}}");
			UITextSkin uitextSkin = this.freeDramsLabels[0];
			string text2 = "{{{{W|${0}}}}}";
			global::XRL.World.GameObject trader = TradeScreen.Trader;
			uitextSkin.SetText(string.Format(text2, (trader != null) ? trader.GetFreeDrams("water", null, null, null, false) : 0));
			UITextSkin uitextSkin2 = this.freeDramsLabels[1];
			string text3 = "{{{{W|${0}}}}} | {{{{{1}|{2}/{3} lbs.}}}}";
			object[] array = new object[4];
			int num3 = 0;
			global::XRL.World.GameObject player = The.Player;
			array[num3] = ((player != null) ? player.GetFreeDrams("water", null, null, null, false) : 0);
			array[1] = text;
			array[2] = num2;
			array[3] = maxCarriedWeight;
			uitextSkin2.SetText(string.Format(text3, array));
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000EE8D8 File Offset: 0x000ECAD8
		public void UpdateMenuBars()
		{
			this.hotkeyText.SetText("{{W|[" + ControlManager.getCommandInputFormatted("CmdTradeOffer", true, false) + "]}}");
			this.hotkeyBar.BeforeShow(null, this.yieldMenuOptions());
			this.hotkeyBar.GetNavigationContext().disabled = true;
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000EE95B File Offset: 0x000ECB5B
		public int selectedSide
		{
			get
			{
				if (this.scrollerControllers[0].scrollContext.IsActive(true))
				{
					return 0;
				}
				if (this.scrollerControllers[1].scrollContext.IsActive(true))
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000EE98C File Offset: 0x000ECB8C
		public void HandleOfferTrade()
		{
			this.HandleMenuOption(TradeScreen.OFFER_TRADE);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000EE999 File Offset: 0x000ECB99
		public void HandleVPositive()
		{
			this.categoryCollapsed[0].Clear();
			this.categoryCollapsed[1].Clear();
			this.UpdateViewFromData();
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000EE9BC File Offset: 0x000ECBBC
		public void HandleVNegative()
		{
			this.categoryCollapsed[0].Clear();
			this.categoryCollapsed[1].Clear();
			foreach (string text in this.usedCategories[0])
			{
				this.categoryCollapsed[0][text] = true;
			}
			foreach (string text2 in this.usedCategories[1])
			{
				this.categoryCollapsed[1][text2] = true;
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000EEA88 File Offset: 0x000ECC88
		public async void HandleMenuOption(FrameworkDataElement element)
		{
			if (element.Id == "Cancel")
			{
				this.Cancel();
			}
			if (element == TradeScreen.SET_FILTER)
			{
				this.searchInput.EnterAndOpen();
			}
			if (element == TradeScreen.TOGGLE_SORT)
			{
				if (this.sortMode == TradeScreen.SortMode.AZ)
				{
					this.sortMode = TradeScreen.SortMode.Category;
				}
				else if (this.sortMode == TradeScreen.SortMode.Category)
				{
					this.sortMode = TradeScreen.SortMode.AZ;
				}
				this.BeforeShow();
			}
			if (element == TradeScreen.TOGGLE_ALL)
			{
				int side = this.selectedSide;
				if (side > -1)
				{
					bool flag = false;
					for (int i = 0; i < this.listItems[side].Count; i++)
					{
						if (this.listItems[side][i].go != null && this.howManySelected(this.listItems[side][i].go) < this.listItems[side][i].go.Count && !this.listItems[side][i].go.IsImportant())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						foreach (TradeLineData tradeLineData in this.listItems[side])
						{
							if (tradeLineData.go != null && (side != 1 || tradeLineData.go == null || !tradeLineData.go.IsImportant()))
							{
								await this.setHowManySelected(tradeLineData.go, tradeLineData.go.Count);
							}
						}
						List<TradeLineData>.Enumerator enumerator = default(List<TradeLineData>.Enumerator);
					}
					else
					{
						foreach (TradeLineData tradeLineData2 in this.listItems[side])
						{
							if (tradeLineData2.go != null)
							{
								await this.setHowManySelected(tradeLineData2.go, 0);
							}
						}
						List<TradeLineData>.Enumerator enumerator = default(List<TradeLineData>.Enumerator);
					}
					this.UpdateViewFromData();
				}
			}
			if (element == TradeScreen.OFFER_TRADE)
			{
				this.UpdateTotals();
				int Difference = TradeUI.CalculateTrade(this.Totals[0], this.Totals[1]);
				TutorialManager.OnTradeOffer();
				TradeUI.OfferStatus offerStatus = await APIDispatch.RunAndWaitAsync<TradeUI.OfferStatus>(() => TradeUI.PerformOffer(Difference, false, TradeScreen.Trader, this.mode, this.tradeEntries, TradeScreen.NumberSelected));
				TutorialManager.OnTradeComplete();
				if (offerStatus != TradeUI.OfferStatus.NEXT)
				{
					if (offerStatus == TradeUI.OfferStatus.TOP || offerStatus == TradeUI.OfferStatus.REFRESH)
					{
						this.ClearAndSetupTradeUI();
						this.UpdateViewFromData();
					}
					else
					{
						if (offerStatus == TradeUI.OfferStatus.CLOSE)
						{
							this.Hide();
						}
						this.Cleanup();
						this.menucomplete.TrySetResult(offerStatus);
					}
				}
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000EEAC7 File Offset: 0x000ECCC7
		public void Cleanup()
		{
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000EEAC9 File Offset: 0x000ECCC9
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000EEAD1 File Offset: 0x000ECCD1
		public void Cancel()
		{
			ControlManager.ResetInput(false, true);
			this.Hide();
			this.Cleanup();
			this.menucomplete.TrySetResult(TradeUI.OfferStatus.CLOSE);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000EEAF3 File Offset: 0x000ECCF3
		void ControlManager.IControllerChangedEvent.ControllerChanged()
		{
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000EEAF5 File Offset: 0x000ECCF5
		public void HandleVAxis(int? val)
		{
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000EEAF7 File Offset: 0x000ECCF7
		public void HandleXAxis(int? val)
		{
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000EEAFC File Offset: 0x000ECCFC
		public void ReselectAfterCategoryStateChange(TradeLineData item)
		{
			Predicate<TradeLineData> <>9__0;
			Predicate<TradeLineData> <>9__1;
			for (int j = 0; j < 2; j++)
			{
				int num = -1;
				if (item.go != null)
				{
					List<TradeLineData> list = this.listItems[j];
					Predicate<TradeLineData> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = (TradeLineData i) => i.go == item.go);
					}
					num = list.FindIndex(predicate);
				}
				if (num == -1)
				{
					List<TradeLineData> list2 = this.listItems[j];
					Predicate<TradeLineData> predicate2;
					if ((predicate2 = <>9__1) == null)
					{
						predicate2 = (<>9__1 = (TradeLineData i) => i.category == item.category);
					}
					num = list2.FindIndex(predicate2);
				}
				num = Math.Max(0, Math.Min(num, this.listItems[j].Count - 1));
				this.scrollerControllers[j].selectedPosition = num;
				this.scrollerControllers[j].ScrollSelectedIntoView();
				this.scrollerControllers[j].scrollContext.GetContextAt(num).Activate(false);
			}
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000EEBE8 File Offset: 0x000ECDE8
		public async void HandleItemQuickeyDown(FrameworkDataElement element)
		{
			TradeLineData tradeLineData = element as TradeLineData;
			if (tradeLineData != null)
			{
				if (tradeLineData.type == TradeLineDataType.Category)
				{
					this.categoryCollapsed[tradeLineData.side][tradeLineData.category] = !this.isCollapsed(tradeLineData.side, tradeLineData.category);
					this.BeforeShow();
				}
				else
				{
					if (this.howManySelected(tradeLineData.go) < tradeLineData.go.Count)
					{
						await this.setHowManySelected(tradeLineData.go, tradeLineData.go.Count);
					}
					else
					{
						await this.setHowManySelected(tradeLineData.go, 0);
					}
					this.BeforeShow();
				}
			}
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000EEC28 File Offset: 0x000ECE28
		public void HandleSelectItem(FrameworkDataElement element)
		{
			TradeLineData tradeLineData = element as TradeLineData;
			if (tradeLineData != null && tradeLineData.type == TradeLineDataType.Category)
			{
				this.categoryCollapsed[tradeLineData.side][tradeLineData.category] = !this.isCollapsed(tradeLineData.side, tradeLineData.category);
				this.BeforeShow();
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000EEC7C File Offset: 0x000ECE7C
		public void HandleHighlightObject(FrameworkDataElement element)
		{
			TradeLineData tradeLineData = element as TradeLineData;
			if (tradeLineData != null && tradeLineData.go != null)
			{
				global::XRL.World.GameObject go = tradeLineData.go;
				string text = " {{K|" + go.WeightEach.ToString() + "#}}";
				if (this.mode == TradeUI.TradeScreenMode.Trade)
				{
					string text2 = (go.IsCurrency ? "Y" : "B");
					string text3 = string.Concat(new string[]
					{
						"{{",
						text2,
						"|$}}{{C|",
						TradeUI.FormatPrice(TradeUI.GetValue(go, new bool?(tradeLineData.traderInventory)), TradeScreen.CostMultiple),
						"}}"
					});
					text = text + " " + text3;
				}
				this.detailsRightText.SetText(text);
				this.detailsLeftText.SetText(go.DisplayNameSingle);
				return;
			}
			this.detailsLeftText.SetText("");
			this.detailsRightText.SetText("");
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000EED7C File Offset: 0x000ECF7C
		public virtual void CheckLayoutFrame()
		{
			if (!base.canvasGroup.enabled)
			{
				return;
			}
			if (this.lastWidth != Screen.width || this.lastStageScale != Options.StageScale)
			{
				this.ResetLayoutFrame();
			}
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000EEDAC File Offset: 0x000ECFAC
		public void Update()
		{
			if (this.layoutGroupFrame > 0)
			{
				this.layoutGroupFrame--;
				if (this.layoutGroupFrame <= 0 && this.componentsToEnableOnResize != null)
				{
					MonoBehaviour[] array = this.componentsToEnableOnResize;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].enabled = false;
					}
				}
			}
			this.CheckLayoutFrame();
			if (base.isCurrentWindow() && this.navigationContext.IsActive(true) && this.lastContext != NavigationController.instance.activeContext)
			{
				this.lastContext = NavigationController.instance.activeContext;
				this.UpdateMenuBars();
			}
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000EEE43 File Offset: 0x000ED043
		public void MoveItem(TradeLineData data, int direction)
		{
		}

		// Token: 0x04001868 RID: 6248
		protected TaskCompletionSource<TradeUI.OfferStatus> menucomplete = new TaskCompletionSource<TradeUI.OfferStatus>();

		// Token: 0x04001869 RID: 6249
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x0400186A RID: 6250
		public ScrollContext<NavigationContext> horizNav = new ScrollContext<NavigationContext>();

		// Token: 0x0400186B RID: 6251
		public TradeScreen.Context navigationContext = new TradeScreen.Context
		{
			buttonHandlers = null
		};

		// Token: 0x0400186C RID: 6252
		public TradeUI.TradeScreenMode mode;

		// Token: 0x0400186D RID: 6253
		public FilterBar filterBar;

		// Token: 0x0400186E RID: 6254
		public FrameworkScroller hotkeyBar;

		// Token: 0x0400186F RID: 6255
		public UITextSkin hotkeyText;

		// Token: 0x04001870 RID: 6256
		public int[] Weight = new int[2];

		// Token: 0x04001871 RID: 6257
		public double[] Totals = new double[2];

		// Token: 0x04001872 RID: 6258
		public List<TradeEntry>[] tradeEntries;

		// Token: 0x04001873 RID: 6259
		public List<TradeLineData>[] listItems;

		// Token: 0x04001874 RID: 6260
		public FrameworkScroller[] scrollerControllers;

		// Token: 0x04001875 RID: 6261
		public List<string>[] usedCategories;

		// Token: 0x04001876 RID: 6262
		public Dictionary<string, List<TradeEntry>>[] objectCategories;

		// Token: 0x04001877 RID: 6263
		public Dictionary<string, bool>[] categoryCollapsed;

		// Token: 0x04001878 RID: 6264
		public static int[][] NumberSelected = new int[2][];

		// Token: 0x04001879 RID: 6265
		public Dictionary<global::XRL.World.GameObject, int> ObjectSide = new Dictionary<global::XRL.World.GameObject, int>();

		// Token: 0x0400187A RID: 6266
		public Dictionary<global::XRL.World.GameObject, int> ObjectIndex = new Dictionary<global::XRL.World.GameObject, int>();

		// Token: 0x0400187B RID: 6267
		public FrameworkSearchInput searchInput;

		// Token: 0x0400187C RID: 6268
		public UITextSkin detailsLeftText;

		// Token: 0x0400187D RID: 6269
		public UITextSkin detailsRightText;

		// Token: 0x0400187E RID: 6270
		public HashSet<global::XRL.World.GameObject> importantWarnedObjects = new HashSet<global::XRL.World.GameObject>();

		// Token: 0x0400187F RID: 6271
		private string searchText = "";

		// Token: 0x04001880 RID: 6272
		public global::UnityEngine.GameObject dragIndicator;

		// Token: 0x04001881 RID: 6273
		public UITextSkin dragIndicatorText;

		// Token: 0x04001882 RID: 6274
		public UITextSkin[] titleText;

		// Token: 0x04001883 RID: 6275
		public static global::XRL.World.GameObject Trader;

		// Token: 0x04001884 RID: 6276
		public static float CostMultiple;

		// Token: 0x04001885 RID: 6277
		public UIThreeColorProperties[] traderIcons;

		// Token: 0x04001886 RID: 6278
		public UITextSkin[] traderNames;

		// Token: 0x04001887 RID: 6279
		private TradeEntry searcher = new TradeEntry("");

		// Token: 0x04001888 RID: 6280
		private List<TradeEntry> filteredEntries = new List<TradeEntry>();

		// Token: 0x04001889 RID: 6281
		public MonoBehaviour[] componentsToEnableOnResize;

		// Token: 0x0400188A RID: 6282
		public int resizeFrame;

		// Token: 0x0400188B RID: 6283
		public int layoutGroupFrame;

		// Token: 0x0400188C RID: 6284
		public double lastStageScale;

		// Token: 0x0400188D RID: 6285
		public int lastWidth = int.MinValue;

		// Token: 0x0400188E RID: 6286
		public RectTransform flowCheck;

		// Token: 0x0400188F RID: 6287
		public TradeScreen.SortMode sortMode = TradeScreen.SortMode.Category;

		// Token: 0x04001890 RID: 6288
		public static readonly MenuOption SET_FILTER = new MenuOption
		{
			Id = "SET_FILTER",
			KeyDescription = "Filter",
			InputCommand = "CmdFilter",
			Description = "Filter"
		};

		// Token: 0x04001891 RID: 6289
		public static readonly MenuOption TOGGLE_SORT = new MenuOption
		{
			Id = "TOGGLE_SORT",
			KeyDescription = "Toggle Sort",
			InputCommand = "Toggle",
			Description = "toggle sort"
		};

		// Token: 0x04001892 RID: 6290
		public static readonly MenuOption OFFER_TRADE = new MenuOption
		{
			Id = "OFFER_TRADE",
			KeyDescription = "Offer",
			InputCommand = "CmdTradeOffer",
			Description = "offer"
		};

		// Token: 0x04001893 RID: 6291
		public static readonly MenuOption ADD_ONE = new MenuOption
		{
			Id = "ADD_ONE",
			KeyDescription = "Add One",
			InputCommand = "CmdTradeAdd",
			Description = "add one"
		};

		// Token: 0x04001894 RID: 6292
		public static readonly MenuOption REMOVE_ONE = new MenuOption
		{
			Id = "REMOVE_ONE",
			KeyDescription = "Remove One",
			InputCommand = "CmdTradeRemove",
			Description = "remove one"
		};

		// Token: 0x04001895 RID: 6293
		public static readonly MenuOption TOGGLE_ALL = new MenuOption
		{
			Id = "TOGGLE_ALL",
			KeyDescription = "Toggle All",
			InputCommand = "CmdTradeToggleAll",
			Description = "toggle all"
		};

		// Token: 0x04001896 RID: 6294
		public static readonly MenuOption VENDOR_ACTIONS = new MenuOption
		{
			Id = "VENDOR_ACTIONS",
			KeyDescription = "Vendor Actions",
			InputCommand = "CmdVendorActions",
			Description = "vendor actions"
		};

		// Token: 0x04001897 RID: 6295
		public static List<MenuOption> defaultMenuOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Cancel",
				InputCommand = "Cancel",
				Description = "Close Menu"
			},
			new MenuOption
			{
				InputCommand = "NavigationXYAxis",
				Description = "navigate",
				disabled = true
			},
			TradeScreen.TOGGLE_SORT,
			TradeScreen.SET_FILTER
		};

		// Token: 0x04001898 RID: 6296
		public static List<MenuOption> getItemMenuOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Cancel",
				InputCommand = "Cancel",
				Description = "Close Menu"
			},
			new MenuOption
			{
				InputCommand = "NavigationXYAxis",
				Description = "navigate",
				disabled = true
			},
			TradeScreen.TOGGLE_SORT,
			TradeScreen.SET_FILTER,
			TradeScreen.VENDOR_ACTIONS,
			TradeScreen.ADD_ONE,
			TradeScreen.REMOVE_ONE,
			TradeScreen.TOGGLE_ALL
		};

		// Token: 0x04001899 RID: 6297
		public UITextSkin[] freeDramsLabels;

		// Token: 0x0400189A RID: 6298
		public UITextSkin[] totalLabels;

		// Token: 0x0400189B RID: 6299
		private NavigationContext lastContext;

		// Token: 0x020018D8 RID: 6360
		public class Context : NavigationContext
		{
		}

		// Token: 0x020018D9 RID: 6361
		public enum SortMode
		{
			// Token: 0x040063C3 RID: 25539
			AZ,
			// Token: 0x040063C4 RID: 25540
			Category
		}
	}
}
