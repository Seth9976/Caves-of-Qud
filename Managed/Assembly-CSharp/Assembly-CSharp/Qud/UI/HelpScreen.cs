using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using XRL;
using XRL.CharacterBuilds.UI;
using XRL.Help;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200043A RID: 1082
	[UIView("HelpScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "HelpScreen", UICanvasHost = 1)]
	public class HelpScreen : SingletonWindowBase<HelpScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x000D4E73 File Offset: 0x000D3073
		public bool breakBackButton
		{
			get
			{
				return this.lastWidth <= this.breakpointBackButtonWidth;
			}
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000D4E88 File Offset: 0x000D3088
		public async Task<bool> HelpMenu(string startPage = null)
		{
			this.menuItems = The.Manual.Pages.Select<KeyValuePair<string, XRLManualPage>, HelpDataRow>(delegate(KeyValuePair<string, XRLManualPage> kv)
			{
				this.categoryExpanded.TryAdd(kv.Key, true);
				return new HelpDataRow
				{
					CategoryId = kv.Key,
					Collapsed = false,
					Description = kv.Key,
					HelpText = kv.Value.GetData(true)
				};
			}).ToList<HelpDataRow>();
			this.helpScroller.ScrollOnSelection = new Func<bool>(this.ShouldScrollToSelection);
			this.SelectFirst = true;
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetCanceled();
			}
			this.completionSource = new TaskCompletionSource<bool>();
			await The.UiContext;
			ControlManager.ResetInput(false, true);
			this.Show();
			if (!string.IsNullOrEmpty(startPage))
			{
				this.helpScroller.scrollContext.SelectIndex(this.menuItems.IndexOf(this.menuItems.Where<HelpDataRow>((HelpDataRow i) => i.CategoryId == startPage).First<HelpDataRow>()));
			}
			bool info = await this.completionSource.Task;
			this.DisableNavContext(true);
			await The.UiContext;
			this.Hide();
			return info;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000D4ED3 File Offset: 0x000D30D3
		public bool ShouldScrollToSelection()
		{
			return !ScrollViewCalcs.GetScrollViewCalcs(this.helpScroller.GetPrefabForIndex(this.helpScroller.selectedPosition).transform as RectTransform, this._svc).isAnyInView;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000D4F08 File Offset: 0x000D3108
		public void Exit()
		{
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource == null)
			{
				return;
			}
			taskCompletionSource.TrySetResult(false);
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000D4F1C File Offset: 0x000D311C
		public void Update()
		{
			if (this.globalContext.IsActive(true))
			{
				NavigationContext activeContext = NavigationController.instance.activeContext;
				bool flag = activeContext != null && activeContext.IsInside(this.helpScroller.GetNavigationContext());
				float width = base.rectTransform.rect.width;
				if (flag != this.wasInScroller || this.lastWidth != width)
				{
					this.wasInScroller = flag;
					this.lastWidth = width;
					this.backButton.gameObject.SetActive(!this.breakBackButton);
					this.safeArea.offsetMin = new Vector2((float)(this.breakBackButton ? 10 : 150), this.safeArea.offsetMin.y);
					this.safeArea.offsetMax = new Vector2((float)(this.breakBackButton ? (-10) : (-150)), this.safeArea.offsetMax.y);
					this.UpdateMenuBars();
				}
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000D5016 File Offset: 0x000D3216
		public void ControllerChanged()
		{
			this.UpdateMenuBars();
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000D5020 File Offset: 0x000D3220
		public void SetupContext()
		{
			this.globalContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.globalContext.buttonHandlers.Set(InputButtonTypes.CancelButton, global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit)));
			this.globalContext.commandHandlers = new Dictionary<string, Action> { 
			{
				HelpScreen.BACK_BUTTON.InputCommand,
				global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit))
			} };
			this.midHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.midHorizNav.contexts.Clear();
			this.midHorizNav.contexts.Add(this.backButton.navigationContext);
			this.midHorizNav.contexts.Add(this.categoryScroller.GetNavigationContext());
			this.midHorizNav.contexts.Add(this.vertNav);
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Clear();
			this.vertNav.contexts.Add(this.helpScroller.GetNavigationContext());
			this.vertNav.contexts.Add(this.hotkeyBar.GetNavigationContext());
			this.midHorizNav.Setup();
			this.midHorizNav.parentContext = this.globalContext;
			this.helpScroller.scrollContext.wraps = false;
			this.vertNav.wraps = false;
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x000D5185 File Offset: 0x000D3385
		public static MenuOption BACK_BUTTON
		{
			get
			{
				return EmbarkBuilderOverlayWindow.BackMenuOption;
			}
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000D518C File Offset: 0x000D338C
		public void UpdateMenuBars()
		{
			this.keyMenuOptions.Clear();
			this.keyMenuOptions.Add(new MenuOption
			{
				InputCommand = "NavigationXYAxis",
				Description = "navigate"
			});
			this.keyMenuOptions.Add(new MenuOption
			{
				InputCommand = "Accept",
				Description = "Toggle Visibility"
			});
			this.hotkeyBar.BeforeShow(null, this.keyMenuOptions);
			this.hotkeyBar.GetNavigationContext().disabled = false;
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
			foreach (NavigationContext navigationContext in this.hotkeyBar.scrollContext.contexts.GetRange(0, 2))
			{
				navigationContext.disabled = true;
			}
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000D5294 File Offset: 0x000D3494
		public void HandleMenuOption(FrameworkDataElement data)
		{
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000D5296 File Offset: 0x000D3496
		public IEnumerable<FrameworkDataElement> GetMenuItems()
		{
			foreach (HelpDataRow helpDataRow in this.menuItems)
			{
				helpDataRow.Collapsed = !this.categoryExpanded[helpDataRow.CategoryId];
				yield return helpDataRow;
			}
			List<HelpDataRow>.Enumerator enumerator = default(List<HelpDataRow>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000D52A6 File Offset: 0x000D34A6
		public IEnumerable<FrameworkDataElement> GetCategoryItems()
		{
			return this.GetMenuItems();
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000D52B0 File Offset: 0x000D34B0
		public override void Show()
		{
			base.Show();
			EmbarkBuilderModuleBackButton embarkBuilderModuleBackButton = this.backButton;
			if (embarkBuilderModuleBackButton != null)
			{
				embarkBuilderModuleBackButton.gameObject.SetActive(true);
			}
			if (this.backButton.navigationContext == null)
			{
				this.backButton.Awake();
			}
			this.backButton.navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.backButton.navigationContext.buttonHandlers.Set(InputButtonTypes.AcceptButton, global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit)));
			this.helpScroller.scrollContext.wraps = false;
			this.helpScroller.BeforeShow(null, this.GetMenuItems());
			this.categoryScroller.scrollContext.wraps = true;
			this.categoryScroller.BeforeShow(null, this.GetCategoryItems());
			foreach (FrameworkUnityScrollChild frameworkUnityScrollChild in this.categoryScroller.selectionClones)
			{
				FrameworkHoverable component = frameworkUnityScrollChild.GetComponent<FrameworkHoverable>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			if (this.SelectFirst)
			{
				this.SelectFirst = false;
				this.helpScroller.scrollContext.selectedPosition = 0;
			}
			this.helpScroller.onSelected.RemoveAllListeners();
			this.helpScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelect));
			this.helpScroller.onHighlight.RemoveAllListeners();
			this.helpScroller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlight));
			this.categoryScroller.onSelected.RemoveAllListeners();
			this.categoryScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectLeft));
			this.categoryScroller.onHighlight.RemoveAllListeners();
			this.categoryScroller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightLeft));
			this.UpdateMenuBars();
			this.SetupContext();
			this.EnableNavContext();
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000D54AC File Offset: 0x000D36AC
		public void HandleSelect(FrameworkDataElement element)
		{
			HelpDataRow helpDataRow = element as HelpDataRow;
			if (helpDataRow != null)
			{
				this.categoryExpanded[helpDataRow.CategoryId] = !this.categoryExpanded[helpDataRow.CategoryId];
				this.Show();
				this.helpScroller.scrollContext.SelectIndex(this.GetMenuItems().ToList<FrameworkDataElement>().FindIndex((FrameworkDataElement s) => s == element));
			}
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000D552C File Offset: 0x000D372C
		public void HandleHighlight(FrameworkDataElement element)
		{
			this.lastSelectedElement = element;
			string catId = null;
			HelpDataRow helpDataRow = element as HelpDataRow;
			if (helpDataRow != null)
			{
				catId = helpDataRow.CategoryId;
			}
			if (catId != null)
			{
				this.categoryScroller.selectedPosition = (this.categoryScroller.scrollContext.selectedPosition = this.GetCategoryItems().ToList<FrameworkDataElement>().FindIndex(delegate(FrameworkDataElement s)
				{
					HelpDataRow helpDataRow2 = s as HelpDataRow;
					return ((helpDataRow2 != null) ? helpDataRow2.CategoryId : null) == catId;
				}));
			}
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000D55A8 File Offset: 0x000D37A8
		public void HandleSelectLeft(FrameworkDataElement element)
		{
			HelpDataRow cat = element as HelpDataRow;
			if (cat != null)
			{
				this.categoryExpanded[cat.CategoryId] = true;
				this.Show();
				int num = this.GetMenuItems().ToList<FrameworkDataElement>().FindIndex(delegate(FrameworkDataElement s)
				{
					HelpDataRow helpDataRow = s as HelpDataRow;
					return ((helpDataRow != null) ? helpDataRow.CategoryId : null) == cat.CategoryId;
				});
				this.helpScroller.scrollContext.GetContextAt(num).Activate(false);
				this.helpScroller.ScrollSelectedIntoView();
				ScrollViewCalcs.ScrollToTopOfRect(this.helpScroller.GetPrefabForIndex(num).GetComponent<RectTransform>(), null);
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000D5644 File Offset: 0x000D3844
		public void HandleHighlightLeft(FrameworkDataElement element)
		{
			HelpDataRow cat = element as HelpDataRow;
			if (cat != null)
			{
				HelpDataRow helpDataRow = this.GetMenuItems().Skip<FrameworkDataElement>(this.helpScroller.selectedPosition).FirstOrDefault<FrameworkDataElement>() as HelpDataRow;
				if (((helpDataRow != null) ? helpDataRow.CategoryId : null) != cat.CategoryId)
				{
					int num = this.GetMenuItems().ToList<FrameworkDataElement>().FindIndex(delegate(FrameworkDataElement s)
					{
						HelpDataRow helpDataRow2 = s as HelpDataRow;
						return ((helpDataRow2 != null) ? helpDataRow2.CategoryId : null) == cat.CategoryId;
					});
					this.helpScroller.selectedPosition = (this.helpScroller.scrollContext.selectedPosition = num);
					this.helpScroller.ScrollSelectedIntoView();
					ScrollViewCalcs.ScrollToTopOfRect(this.helpScroller.GetPrefabForIndex(num).GetComponent<RectTransform>(), null);
				}
			}
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000D570B File Offset: 0x000D390B
		public override void Hide()
		{
			base.Hide();
			this.DisableNavContext(true);
			ControlManager.ResetInput(false, true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000D572D File Offset: 0x000D392D
		public void EnableNavContext()
		{
			this.globalContext.disabled = false;
			this.helpScroller.GetNavigationContext().ActivateAndEnable();
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000D574C File Offset: 0x000D394C
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

		// Token: 0x040014BF RID: 5311
		public FrameworkScroller hotkeyBar;

		// Token: 0x040014C0 RID: 5312
		public FrameworkScroller helpScroller;

		// Token: 0x040014C1 RID: 5313
		public FrameworkScroller categoryScroller;

		// Token: 0x040014C2 RID: 5314
		public RectTransform safeArea;

		// Token: 0x040014C3 RID: 5315
		public EmbarkBuilderModuleBackButton backButton;

		// Token: 0x040014C4 RID: 5316
		public TaskCompletionSource<bool> completionSource;

		// Token: 0x040014C5 RID: 5317
		public NavigationContext globalContext = new NavigationContext();

		// Token: 0x040014C6 RID: 5318
		public ScrollContext<NavigationContext> midHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x040014C7 RID: 5319
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x040014C8 RID: 5320
		public List<HelpDataRow> menuItems = new List<HelpDataRow>();

		// Token: 0x040014C9 RID: 5321
		public Dictionary<string, bool> categoryExpanded = new Dictionary<string, bool>();

		// Token: 0x040014CA RID: 5322
		public float lastWidth;

		// Token: 0x040014CB RID: 5323
		public float breakpointBackButtonWidth;

		// Token: 0x040014CC RID: 5324
		private ScrollViewCalcs _svc = new ScrollViewCalcs();

		// Token: 0x040014CD RID: 5325
		public bool wasInScroller;

		// Token: 0x040014CE RID: 5326
		private bool SelectFirst = true;

		// Token: 0x040014CF RID: 5327
		public List<MenuOption> keyMenuOptions = new List<MenuOption>();

		// Token: 0x040014D0 RID: 5328
		public FrameworkDataElement lastSelectedElement;
	}
}
