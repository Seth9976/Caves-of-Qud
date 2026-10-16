using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiUnity.Common.Extensions;
using FuzzySharp;
using FuzzySharp.Extractor;
using UnityEngine;
using UnityEngine.Events;
using XRL;
using XRL.CharacterBuilds.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000451 RID: 1105
	[UIView("ModernOptionsMenu", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "_manuallyshown", UICanvasHost = 1)]
	public class OptionsScreen : SingletonWindowBase<OptionsScreen>
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000DA6A8 File Offset: 0x000D88A8
		public bool breakBackButton
		{
			get
			{
				return Media.sizeClass < Media.SizeClass.Medium;
			}
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000DA6B4 File Offset: 0x000D88B4
		public async Task<bool> OptionsMenu()
		{
			this.searchText = "";
			GameManager.Instance.PushGameView("ModernOptionsMenu", true);
			this.menuItems = Options.OptionsByCategory.SelectMany<KeyValuePair<string, List<GameOption>>, OptionsDataRow>(delegate(KeyValuePair<string, List<GameOption>> kv)
			{
				OptionsCategoryRow optionsCategoryRow = new OptionsCategoryRow
				{
					Id = kv.Key,
					Title = kv.Key,
					CategoryId = kv.Key,
					SearchWords = kv.Key
				};
				this.categoryExpanded.TryAdd(kv.Key, true);
				return kv.Value.Select<GameOption, OptionsDataRow>(delegate(GameOption option)
				{
					OptionsDataRow optionsDataRow;
					if (option.Type == "Checkbox")
					{
						optionsDataRow = new OptionsCheckboxRow(option);
					}
					else if (option.Type == "Button")
					{
						optionsDataRow = new OptionsMenuButtonRow(option);
					}
					else if (option.Type == "Slider")
					{
						optionsDataRow = new OptionsSliderRow(option);
					}
					else if (option.Type == "Combo" || option.Type == "BigCombo")
					{
						optionsDataRow = new OptionsComboBoxRow(option);
					}
					else
					{
						optionsDataRow = new OptionsUnknownTypeRow(option);
					}
					return optionsDataRow;
				}).Prepend(optionsCategoryRow);
			}).ToList<OptionsDataRow>();
			int num = this.menuItems.FindIndex((OptionsDataRow row) => row.Id == "OptionShowAdvancedOptions");
			if (num >= 0)
			{
				this.advancedOptionsCheck = this.menuItems[num] as OptionsCheckboxRow;
				this.menuItems.RemoveAt(num);
			}
			this.FilterItems();
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetCanceled();
			}
			this.completionSource = new TaskCompletionSource<bool>();
			await The.UiContext;
			ControlManager.ResetInput(false, true);
			this.Show();
			bool info = await this.completionSource.Task;
			this.DisableNavContext(true);
			await The.UiContext;
			this.Hide();
			GameManager.Instance.PopGameView(true);
			return info;
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000DA6F8 File Offset: 0x000D88F8
		public void FilterItems()
		{
			this.filteredMenuItems.Clear();
			if (string.IsNullOrWhiteSpace(this.searchText))
			{
				Dictionary<string, OptionsCategoryRow> dictionary = this.menuItems.OfType<OptionsCategoryRow>().ToDictionary<OptionsCategoryRow, string>((OptionsCategoryRow i) => i.CategoryId);
				using (IEnumerator<IGrouping<string, OptionsDataRow>> enumerator = (from i in this.menuItems
					where i != null && !(i is OptionsCategoryRow) && i.IsEnabled != null && i.IsEnabled()
					select i into s
					group s by s.CategoryId).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IGrouping<string, OptionsDataRow> grouping = enumerator.Current;
						this.categoryExpanded[grouping.Key] = true;
						this.filteredMenuItems.Add(dictionary[grouping.Key]);
						foreach (OptionsDataRow optionsDataRow in grouping)
						{
							if (!(optionsDataRow is OptionsCategoryRow))
							{
								this.filteredMenuItems.Add(optionsDataRow);
							}
						}
					}
					return;
				}
			}
			IEnumerable<ExtractedResult<OptionsDataRow>> enumerable = Process.ExtractTop<OptionsDataRow>(this.searcher, this.menuItems, (OptionsDataRow i) => i.SearchWords.ToLower(), null, this.menuItems.Count, 50);
			Dictionary<string, OptionsCategoryRow> dictionary2 = this.menuItems.OfType<OptionsCategoryRow>().ToDictionary<OptionsCategoryRow, string>((OptionsCategoryRow i) => i.CategoryId);
			foreach (IGrouping<string, ExtractedResult<OptionsDataRow>> grouping2 in from s in enumerable
				where !(s.Value is OptionsCategoryRow)
				group s by s.Value.CategoryId)
			{
				this.categoryExpanded[grouping2.Key] = true;
				this.filteredMenuItems.Add(dictionary2[grouping2.Key]);
				foreach (ExtractedResult<OptionsDataRow> extractedResult in grouping2)
				{
					if (!(extractedResult.Value is OptionsCategoryRow))
					{
						this.filteredMenuItems.Add(extractedResult.Value);
					}
				}
			}
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000DA9BC File Offset: 0x000D8BBC
		public void Exit()
		{
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource == null)
			{
				return;
			}
			taskCompletionSource.TrySetResult(false);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000DA9D0 File Offset: 0x000D8BD0
		public void Update()
		{
			if (this.globalContext.IsActive(true))
			{
				NavigationContext activeContext = NavigationController.instance.activeContext;
				bool flag = activeContext != null && activeContext.IsInside(this.optionsScroller.GetNavigationContext());
				float width = base.rectTransform.rect.width;
				this.CheckRequirements();
				if (flag != this.wasInScroller || this.lastWidth != width)
				{
					this.wasInScroller = flag;
					this.lastWidth = width;
					this.backButton.gameObject.SetActive(!this.breakBackButton);
					this.safeArea.offsetMin = new Vector2((float)(this.breakBackButton ? 25 : 150), this.safeArea.offsetMin.y);
					this.safeArea.offsetMax = new Vector2((float)(this.breakBackButton ? (-25) : (-150)), this.safeArea.offsetMax.y);
					this.UpdateMenuBars();
					return;
				}
				if (this.lastContext != NavigationController.instance.activeContext)
				{
					this.lastContext = NavigationController.instance.activeContext;
					this.UpdateMenuBars();
				}
			}
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000DAAFC File Offset: 0x000D8CFC
		protected void SetAllExpanded(bool state)
		{
			OptionsDataRow currentSelection = this.lastSelectedElement as OptionsDataRow;
			foreach (string text in this.categoryExpanded.Keys.ToArray<string>())
			{
				this.categoryExpanded[text] = state;
			}
			this.Show();
			if (!state)
			{
				currentSelection = this.GetMenuItems().FirstOrDefault<OptionsDataRow>(delegate(OptionsDataRow row)
				{
					string categoryId = row.CategoryId;
					OptionsDataRow currentSelection2 = currentSelection;
					return categoryId == ((currentSelection2 != null) ? currentSelection2.CategoryId : null);
				});
			}
			int num = Math.Max(0, this.GetMenuItems().IndexOf(currentSelection));
			this.optionsScroller.scrollContext.SelectIndex(num);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000DABA4 File Offset: 0x000D8DA4
		protected void CheckRequirements()
		{
			if (!this.globalContext.editMode && Options.ShouldCheckRequirements())
			{
				float contentTop = ScrollViewCalcs.GetScrollViewCalcs(this.optionsScroller.scrollRect.transform as RectTransform, this._scrollCalc).contentTop;
				OptionsDataRow optionsDataRow = this.lastSelectedElement as OptionsDataRow;
				this.FilterItems();
				this.Show();
				ScrollViewCalcs.GetScrollViewCalcs(this.optionsScroller.scrollRect.transform as RectTransform, this._scrollCalc);
				this._scrollCalc.ScrollToContentTop(contentTop);
				ScrollViewCalcs.GetScrollViewCalcs(this.optionsScroller.scrollRect.transform as RectTransform, this._scrollCalc);
				int num = Math.Max(0, this.GetMenuItems().IndexOf(optionsDataRow));
				this.optionsScroller.scrollContext.SelectIndex(num);
				ScrollViewCalcs.GetScrollViewCalcs(this.optionsScroller.scrollRect.transform as RectTransform, this._scrollCalc);
			}
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000DAC9C File Offset: 0x000D8E9C
		public void ExpandAll()
		{
			this.SetAllExpanded(true);
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000DACA5 File Offset: 0x000D8EA5
		public void CollapseAll()
		{
			this.SetAllExpanded(false);
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000DACB0 File Offset: 0x000D8EB0
		public void SetupContext()
		{
			this.globalContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.globalContext.buttonHandlers.Set(InputButtonTypes.CancelButton, global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit)));
			this.globalContext.commandHandlers = new Dictionary<string, Action>
			{
				{
					"CmdFilter",
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.searchInput.EnterAndOpen))
				},
				{
					"V Positive",
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.ExpandAll))
				},
				{
					"V Negative",
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.CollapseAll))
				},
				{
					"CmdHelp",
					global::XRL.UI.Framework.Event.Helpers.Handle(delegate
					{
						this.HandleMenuOption(OptionsScreen.HELP_TEXT);
					})
				}
			};
			this.midHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.midHorizNav.contexts.Clear();
			this.midHorizNav.contexts.Add(this.backButton.navigationContext);
			this.midHorizNav.contexts.Add(this.categoryScroller.GetNavigationContext());
			this.midHorizNav.contexts.Add(this.vertNav);
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Clear();
			this.topHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.topHorizNav.contexts.Clear();
			this.topHorizNav.contexts.Add(this.searchInput.context);
			if (this.advancedOptionsCheck != null)
			{
				this.advancedOptionsScrollProxy.index = 0;
				this.advancedOptionsCheckboxRow.SetupContexts(this.advancedOptionsScrollProxy);
				this.topHorizNav.contexts.Add(this.advancedOptionsScrollProxy);
			}
			this.vertNav.contexts.Add(this.topHorizNav);
			this.searchInput.context.inputText = this.searchText ?? "";
			this.searchInput.OnSearchTextChange.RemoveListener(new UnityAction<string>(this.OnSearchTextChange));
			this.searchInput.OnSearchTextChange.AddListener(new UnityAction<string>(this.OnSearchTextChange));
			this.vertNav.contexts.Add(this.optionsScroller.GetNavigationContext());
			this.midHorizNav.Setup();
			this.midHorizNav.parentContext = this.globalContext;
			this.optionsScroller.scrollContext.wraps = false;
			this.vertNav.wraps = false;
			this.midHorizNav.wraps = false;
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000DAF3A File Offset: 0x000D913A
		public static MenuOption BACK_BUTTON
		{
			get
			{
				return EmbarkBuilderOverlayWindow.BackMenuOption;
			}
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000DAF44 File Offset: 0x000D9144
		public void OnSearchTextChange(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				this.searchText = "";
			}
			else
			{
				OptionsDataRow optionsDataRow = this.searcher;
				this.searchText = text;
				optionsDataRow.SearchWords = text;
			}
			this.FilterItems();
			this.Show();
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000DAF88 File Offset: 0x000D9188
		public void UpdateMenuBars()
		{
			OptionsDataRow optionsDataRow = this.lastSelectedElement as OptionsDataRow;
			if (optionsDataRow != null && !string.IsNullOrEmpty(optionsDataRow.HelpText))
			{
				this.hotkeyBar.BeforeShow(null, NavigationController.GetMenuOptions(OptionsScreen.defaultMenuOptions));
			}
			else
			{
				this.hotkeyBar.BeforeShow(null, NavigationController.GetMenuOptions(OptionsScreen.defaultMenuOptions.Take<MenuOption>(OptionsScreen.defaultMenuOptions.Count - 1)));
			}
			this.hotkeyBar.GetNavigationContext().disabled = true;
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000DB030 File Offset: 0x000D9230
		public async void HandleMenuOption(FrameworkDataElement data)
		{
			if (data == OptionsScreen.COLLAPSE_ALL)
			{
				this.CollapseAll();
			}
			else if (data == OptionsScreen.EXPAND_ALL)
			{
				this.ExpandAll();
			}
			else if (data == OptionsScreen.HELP_TEXT)
			{
				OptionsDataRow optionSelected = this.lastSelectedElement as OptionsDataRow;
				OptionsDataRow optionSelected2 = optionSelected;
				if (!string.IsNullOrEmpty((optionSelected2 != null) ? optionSelected2.HelpText : null))
				{
					await NavigationController.instance.SuspendContextWhile<QudMenuItem>(() => Popup.NewPopupMessageAsync(optionSelected.HelpText, PopupMessage.AnyKey, null, "Option: " + optionSelected.Title, null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null));
				}
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000DB06F File Offset: 0x000D926F
		public IEnumerable<OptionsDataRow> GetMenuItems()
		{
			foreach (OptionsDataRow optionsDataRow in this.filteredMenuItems)
			{
				OptionsCategoryRow optionsCategoryRow = optionsDataRow as OptionsCategoryRow;
				if (optionsCategoryRow != null)
				{
					optionsCategoryRow.categoryExpanded = this.categoryExpanded[optionsCategoryRow.Id];
					yield return optionsDataRow;
				}
				else if (this.categoryExpanded[optionsDataRow.CategoryId] && optionsDataRow.IsEnabled())
				{
					OptionsCheckboxRow optionsCheckboxRow = optionsDataRow as OptionsCheckboxRow;
					if (optionsCheckboxRow != null)
					{
						optionsCheckboxRow.Value = Options.GetOption(optionsCheckboxRow.Id, "") == "Yes";
					}
					yield return optionsDataRow;
				}
			}
			List<OptionsDataRow>.Enumerator enumerator = default(List<OptionsDataRow>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000DB07F File Offset: 0x000D927F
		public IEnumerable<OptionsDataRow> GetCategoryItems()
		{
			foreach (OptionsDataRow optionsDataRow in this.filteredMenuItems)
			{
				if (optionsDataRow is OptionsCategoryRow)
				{
					yield return optionsDataRow;
				}
			}
			List<OptionsDataRow>.Enumerator enumerator = default(List<OptionsDataRow>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000DB090 File Offset: 0x000D9290
		public override void Show()
		{
			base.Show();
			this.backButton.gameObject.SetActive(!this.breakBackButton);
			this.safeArea.offsetMin = new Vector2((float)(this.breakBackButton ? 25 : 150), this.safeArea.offsetMin.y);
			this.safeArea.offsetMax = new Vector2((float)(this.breakBackButton ? (-25) : (-150)), this.safeArea.offsetMax.y);
			if (this.backButton.navigationContext == null)
			{
				this.backButton.Awake();
			}
			this.backButton.navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.backButton.navigationContext.buttonHandlers.Set(InputButtonTypes.AcceptButton, global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit)));
			this.optionsScroller.scrollContext.wraps = false;
			this.optionsScroller.BeforeShow(null, this.GetMenuItems());
			this.categoryScroller.scrollContext.wraps = true;
			this.categoryScroller.BeforeShow(null, this.GetCategoryItems());
			if (this.SelectFirst)
			{
				this.SelectFirst = false;
				this.optionsScroller.scrollContext.selectedPosition = 0;
			}
			this.optionsScroller.onSelected.RemoveAllListeners();
			this.optionsScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelect));
			this.optionsScroller.onHighlight.RemoveAllListeners();
			this.optionsScroller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlight));
			this.categoryScroller.onSelected.RemoveAllListeners();
			this.categoryScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectLeft));
			this.categoryScroller.onHighlight.RemoveAllListeners();
			this.categoryScroller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightLeft));
			this.UpdateMenuBars();
			if (this.advancedOptionsCheck != null)
			{
				this.advancedOptionsCheckboxRow.setData(this.advancedOptionsCheck);
				this.advancedOptionsCheckboxRow.gameObject.SetActive(true);
			}
			else
			{
				this.advancedOptionsCheckboxRow.gameObject.SetActive(false);
			}
			this.SetupContext();
			this.EnableNavContext();
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000DB2DC File Offset: 0x000D94DC
		public async void HandleSelect(FrameworkDataElement element)
		{
			OptionsCategoryRow optionsCategoryRow = element as OptionsCategoryRow;
			if (optionsCategoryRow != null)
			{
				this.categoryExpanded[optionsCategoryRow.CategoryId] = !this.categoryExpanded[optionsCategoryRow.CategoryId];
				this.Show();
				this.optionsScroller.scrollContext.SelectIndex(this.GetMenuItems().ToList<OptionsDataRow>().FindIndex((OptionsDataRow s) => s == element));
			}
			OptionsMenuButtonRow optionsMenuButtonRow = element as OptionsMenuButtonRow;
			if (optionsMenuButtonRow != null)
			{
				Task<bool> task = optionsMenuButtonRow.OnClick.Invoke(null, null) as Task<bool>;
				if (task != null)
				{
					await task;
				}
			}
			this.Show();
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000DB31C File Offset: 0x000D951C
		public void HandleHighlight(FrameworkDataElement element)
		{
			this.lastSelectedElement = element;
			string catId = null;
			OptionsDataRow optionsDataRow = element as OptionsDataRow;
			if (optionsDataRow != null)
			{
				catId = optionsDataRow.CategoryId;
			}
			if (catId != null)
			{
				this.categoryScroller.selectedPosition = (this.categoryScroller.scrollContext.selectedPosition = this.GetCategoryItems().ToList<OptionsDataRow>().FindIndex((OptionsDataRow s) => ((s != null) ? s.CategoryId : null) == catId));
			}
			this.UpdateMenuBars();
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000DB39C File Offset: 0x000D959C
		public void HandleSelectLeft(FrameworkDataElement element)
		{
			OptionsCategoryRow cat = element as OptionsCategoryRow;
			if (cat != null)
			{
				if (!this.categoryExpanded[cat.CategoryId])
				{
					this.categoryExpanded[cat.CategoryId] = true;
					this.Show();
				}
				int num = this.GetMenuItems().ToList<OptionsDataRow>().FindIndex(delegate(OptionsDataRow s)
				{
					OptionsCategoryRow optionsCategoryRow = s as OptionsCategoryRow;
					return ((optionsCategoryRow != null) ? optionsCategoryRow.CategoryId : null) == cat.CategoryId;
				});
				this.optionsScroller.scrollContext.SelectIndex(num);
				this.optionsScroller.selectedPosition = num;
				this.optionsScroller.ScrollSelectedToTop();
			}
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000DB43D File Offset: 0x000D963D
		public void HandleHighlightLeft(FrameworkDataElement element)
		{
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000DB43F File Offset: 0x000D963F
		public override void Hide()
		{
			base.Hide();
			this.DisableNavContext(true);
			base.gameObject.SetActive(false);
			ControlManager.ResetInput(false, true);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000DB461 File Offset: 0x000D9661
		public void EnableNavContext()
		{
			this.globalContext.disabled = false;
			this.optionsScroller.GetNavigationContext().ActivateAndEnable();
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000DB480 File Offset: 0x000D9680
		public void DisableNavContext(bool deactivate = true)
		{
			if (deactivate)
			{
				NavigationContext activeContext = NavigationController.instance.activeContext;
				if (activeContext != null && activeContext.IsInside(this.globalContext))
				{
					NavigationController.instance.activeContext = null;
				}
			}
			this.globalContext.disabled = true;
		}

		// Token: 0x0400159C RID: 5532
		public FrameworkScroller hotkeyBar;

		// Token: 0x0400159D RID: 5533
		public FrameworkScroller optionsScroller;

		// Token: 0x0400159E RID: 5534
		public FrameworkScroller categoryScroller;

		// Token: 0x0400159F RID: 5535
		public FrameworkSearchInput searchInput;

		// Token: 0x040015A0 RID: 5536
		public RectTransform safeArea;

		// Token: 0x040015A1 RID: 5537
		public EmbarkBuilderModuleBackButton backButton;

		// Token: 0x040015A2 RID: 5538
		public TaskCompletionSource<bool> completionSource;

		// Token: 0x040015A3 RID: 5539
		public OptionsScreen.NavContext globalContext = new OptionsScreen.NavContext();

		// Token: 0x040015A4 RID: 5540
		public ScrollContext<NavigationContext> topHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x040015A5 RID: 5541
		public ScrollContext<NavigationContext> midHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x040015A6 RID: 5542
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x040015A7 RID: 5543
		public List<OptionsDataRow> menuItems = new List<OptionsDataRow>();

		// Token: 0x040015A8 RID: 5544
		public List<OptionsDataRow> filteredMenuItems = new List<OptionsDataRow>();

		// Token: 0x040015A9 RID: 5545
		public Dictionary<string, bool> categoryExpanded = new Dictionary<string, bool>();

		// Token: 0x040015AA RID: 5546
		public ScrollChildContext advancedOptionsScrollProxy = new ScrollChildContext();

		// Token: 0x040015AB RID: 5547
		public OptionsRow advancedOptionsCheckboxRow;

		// Token: 0x040015AC RID: 5548
		public OptionsCheckboxRow advancedOptionsCheck;

		// Token: 0x040015AD RID: 5549
		public float lastWidth;

		// Token: 0x040015AE RID: 5550
		public float breakpointBackButtonWidth;

		// Token: 0x040015AF RID: 5551
		public bool wasInScroller;

		// Token: 0x040015B0 RID: 5552
		public NavigationContext lastContext;

		// Token: 0x040015B1 RID: 5553
		private ScrollViewCalcs _scrollCalc = new ScrollViewCalcs();

		// Token: 0x040015B2 RID: 5554
		private bool SelectFirst = true;

		// Token: 0x040015B3 RID: 5555
		public static readonly MenuOption COLLAPSE_ALL = new MenuOption
		{
			InputCommand = "V Negative",
			Description = "Collapse All",
			disabled = false
		};

		// Token: 0x040015B4 RID: 5556
		public static readonly MenuOption EXPAND_ALL = new MenuOption
		{
			InputCommand = "V Positive",
			Description = "Expand All",
			disabled = false
		};

		// Token: 0x040015B5 RID: 5557
		public static readonly MenuOption HELP_TEXT = new MenuOption
		{
			InputCommand = "CmdHelp",
			Description = "Help",
			disabled = false
		};

		// Token: 0x040015B6 RID: 5558
		public static List<MenuOption> defaultMenuOptions = new List<MenuOption>
		{
			new MenuOption
			{
				InputCommand = "NavigationXYAxis",
				Description = "navigate",
				disabled = true
			},
			OptionsScreen.COLLAPSE_ALL,
			OptionsScreen.EXPAND_ALL,
			new MenuOption
			{
				InputCommand = "Accept",
				Description = "Select",
				disabled = true
			},
			OptionsScreen.HELP_TEXT
		};

		// Token: 0x040015B7 RID: 5559
		public string searchText;

		// Token: 0x040015B8 RID: 5560
		private OptionsCategoryRow searcher = new OptionsCategoryRow();

		// Token: 0x040015B9 RID: 5561
		public FrameworkDataElement lastSelectedElement;

		// Token: 0x02001854 RID: 6228
		public class NavContext : NavigationContext
		{
			// Token: 0x040061FD RID: 25085
			public bool editMode;
		}
	}
}
