using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000478 RID: 1144
	[UIView("StatusScreensScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "StatusScreens", UICanvasHost = 1)]
	public class StatusScreensScreen : SingletonWindowBase<StatusScreensScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x06002B94 RID: 11156 RVA: 0x000E9030 File Offset: 0x000E7230
		public static bool prewarm()
		{
			StatusScreensScreen instance = SingletonWindowBase<StatusScreensScreen>.instance;
			if (instance != null)
			{
				instance._prewarm();
			}
			return true;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000E9044 File Offset: 0x000E7244
		public bool _prewarm()
		{
			StatusScreensScreen.GO = null;
			MinEvent.UIHold = true;
			try
			{
				for (int i = 0; i < this.Screens.Count; i++)
				{
					Transform transform = this.Screens[i];
					IStatusScreen statusScreen = ((transform != null) ? transform.GetComponent<IStatusScreen>() : null);
					if (statusScreen != null)
					{
						statusScreen.Prewarm();
					}
				}
			}
			finally
			{
				MinEvent.UIHold = false;
				GameManager.Instance.PopGameView(false);
			}
			this.menuOptionScroller.DisableFirstLateUpdate();
			this.filterBar.DisableFirstLateUpdate();
			this.screenTabs.DisableFirstLateUpdate();
			return true;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000E90DC File Offset: 0x000E72DC
		public static Task<ScreenReturn> show(int StartingScreen, global::XRL.World.GameObject GO)
		{
			SingletonWindowBase<StatusScreensScreen>.instance.CurrentScreen = StartingScreen % SingletonWindowBase<StatusScreensScreen>.instance.Screens.Count;
			StatusScreensScreen.GO = GO;
			SoundManager.PlayUISound("Sounds/UI/ui_popup_open", 1f, false, true, SoundRequest.SoundEffectType.None);
			return NavigationController.instance.SuspendContextWhile<ScreenReturn>(() => SingletonWindowBase<StatusScreensScreen>.instance.showScreen(StartingScreen, GO));
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000E9150 File Offset: 0x000E7350
		protected async Task<ScreenReturn> showScreen(int StartingScreen, global::XRL.World.GameObject GO)
		{
			this.CurrentScreen = StartingScreen % this.Screens.Count;
			StatusScreensScreen.GO = GO;
			MinEvent.UIHold = true;
			GameManager.Instance.PushGameView("StatusScreensScreen", true);
			ScreenReturn screenReturn2;
			try
			{
				await The.UiContext;
				if (this.globalLayoutElement != null)
				{
					string option = Options.GetOption("OptionStatusScreenSize", "");
					if (option == "Standard")
					{
						this.globalLayoutElement.preferredHeight = 900f;
						this.globalLayoutElement.preferredWidth = 1615f;
						this.globalLayoutElement.flexibleHeight = 0f;
						this.globalLayoutElement.flexibleWidth = 0f;
					}
					else if (option == "Full Height")
					{
						RectTransform rectTransform = UIManager.mainCanvas.transform as RectTransform;
						this.globalLayoutElement.preferredHeight = rectTransform.rect.height;
						this.globalLayoutElement.preferredWidth = Math.Min(rectTransform.rect.width, (float)((int)((double)rectTransform.rect.height * 1.8)));
						this.globalLayoutElement.flexibleHeight = 0f;
						this.globalLayoutElement.flexibleWidth = 0f;
					}
					else if (option == "Full")
					{
						this.globalLayoutElement.preferredHeight = 0f;
						this.globalLayoutElement.preferredWidth = 0f;
						this.globalLayoutElement.flexibleHeight = 1f;
						this.globalLayoutElement.flexibleWidth = 1f;
					}
				}
				float num = 1f + (float)Convert.ToInt32(Options.GetOption("OptionCharacterSheetAdditionalScale", "")) * 0.01f;
				this.scaleTarget.localScale = new Vector3(num, num, 1f);
				this.categoryBarLayoutGroup.enabled = true;
				this.categoryBarLayoutFrames = 3;
				this.UpdateViewFromData();
				this.menucomplete = new TaskCompletionSource<ScreenReturn>();
				ScreenReturn screenReturn = await this.menucomplete.Task;
				this.Cleanup();
				screenReturn2 = screenReturn;
			}
			finally
			{
				MinEvent.UIHold = false;
				GameManager.Instance.PopGameView(false);
			}
			return screenReturn2;
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000E91A3 File Offset: 0x000E73A3
		public void SetPage(int page)
		{
			this.CurrentScreen = page;
			this.UpdateActiveScreen();
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000E91B2 File Offset: 0x000E73B2
		public void NextPage()
		{
			InventoryAndEquipmentStatusScreen.EnteringFromEquipmentSide = true;
			this.CurrentScreen++;
			if (this.CurrentScreen >= this.Screens.Count)
			{
				this.CurrentScreen = 0;
			}
			this.UpdateActiveScreen();
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000E91E8 File Offset: 0x000E73E8
		public void PrevPage()
		{
			InventoryAndEquipmentStatusScreen.EnteringFromEquipmentSide = false;
			this.CurrentScreen--;
			if (this.CurrentScreen < 0)
			{
				this.CurrentScreen = this.Screens.Count - 1;
			}
			this.UpdateActiveScreen();
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000E9220 File Offset: 0x000E7420
		public void OnCloseButton()
		{
			if (this.navigationContext.IsActive(true))
			{
				this.Exit();
			}
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000E9238 File Offset: 0x000E7438
		public void Exit()
		{
			foreach (Transform transform in this.Screens)
			{
				if (transform != null)
				{
					transform.GetComponent<IStatusScreen>().Exit();
				}
			}
			SoundManager.PlayUISound("Sounds/UI/ui_popup_close", 1f, false, true, SoundRequest.SoundEffectType.None);
			this.menucomplete.TrySetResult(ScreenReturn.Exit);
			this.freeTabs();
			this.Hide();
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000E92C4 File Offset: 0x000E74C4
		public void UpdateActiveScreen()
		{
			this.filterText = "";
			foreach (Transform transform in this.Screens)
			{
				if (transform != null)
				{
					transform.GetComponent<IStatusScreen>().HideScreen();
				}
			}
			int num = this.CurrentScreen % this.Screens.Count;
			Transform transform2 = this.Screens[num];
			this.activeScreen = ((transform2 != null) ? transform2.GetComponent<IStatusScreen>() : null);
			if (this.activeScreen != null)
			{
				GameManager.Instance.GetViewData("StatusScreensScreen").NavCategory = this.activeScreen.GetNavigationCategory();
			}
			IStatusScreen statusScreen = this.activeScreen;
			bool flag = ((statusScreen != null) ? statusScreen.WantsCategoryBar() : this.categoryBar.gameObject.activeInHierarchy);
			if (this.categoryBar.activeSelf != flag)
			{
				this.categoryBar.gameObject.SetActive(flag);
				this.categoryBarLayoutFrames = 3;
				this.categoryBarLayoutGroup.enabled = true;
			}
			IStatusScreen statusScreen2 = this.activeScreen;
			if ((statusScreen2 != null) ? statusScreen2.WantsCategoryBar() : this.categoryBar.gameObject.activeInHierarchy)
			{
				if (!this.vertNav.contexts.Contains(this.filterBar.scrollContext))
				{
					this.vertNav.contexts.Insert(1, this.filterBar.scrollContext);
					this.vertNav.Setup();
				}
			}
			else if (this.vertNav.contexts.Contains(this.filterBar.scrollContext))
			{
				this.vertNav.contexts.Remove(this.filterBar.scrollContext);
				this.vertNav.Setup();
			}
			this.menuOptionSizeMinder.forceReset = true;
			this.filterBar.SingleCategoryOnly = false;
			this.filterBar.ResetFilters();
			List<MenuOption> menuOptionDescriptions = this.screenGlobalContext.menuOptionDescriptions;
			if (menuOptionDescriptions != null)
			{
				menuOptionDescriptions.Clear();
			}
			Dictionary<InputAxisTypes, Action> axisHandlers = this.screenGlobalContext.axisHandlers;
			if (axisHandlers != null)
			{
				axisHandlers.Clear();
			}
			Dictionary<InputButtonTypes, Action> buttonHandlers = this.screenGlobalContext.buttonHandlers;
			if (buttonHandlers != null)
			{
				buttonHandlers.Clear();
			}
			Dictionary<string, Action> commandHandlers = this.screenGlobalContext.commandHandlers;
			if (commandHandlers != null)
			{
				commandHandlers.Clear();
			}
			IStatusScreen statusScreen3 = this.activeScreen;
			NavigationContext navigationContext = ((statusScreen3 != null) ? statusScreen3.ShowScreen(StatusScreensScreen.GO, this) : null);
			IStatusScreen statusScreen4 = this.activeScreen;
			if (statusScreen4 != null)
			{
				statusScreen4.CheckLayoutFrame();
			}
			if (navigationContext == null)
			{
				string text = "Selected active screen ";
				IStatusScreen statusScreen5 = this.activeScreen;
				MetricsManager.LogError(text + (((statusScreen5 != null) ? statusScreen5.GetTabString() : null) ?? "(null)") + " didn't return a navigation context! You probably need to override ShowScreen and return one.");
			}
			this.horizNav.contexts.Clear();
			this.horizNav.contexts.Add(navigationContext);
			this.horizNav.Setup();
			navigationContext.ActivateAndEnable();
			this.SetButtons();
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000E95A0 File Offset: 0x000E77A0
		private void SetButtons()
		{
			this.freeTabs();
			this.tabs.AddRange(from s in this.Screens
				where s != null
				select PooledFrameworkDataElement<ButtonBar.ButtonBarButtonData>.next().set(s.GetComponent<IStatusScreen>().GetTabString().ToUpper(), s.GetComponent<IStatusScreen>().GetTabIcon(), (this.CurrentScreen % this.Screens.Count == s.GetSiblingIndex()) ? ButtonBar.ButtonBarButtonData.HighlightState.Highlighted : ButtonBar.ButtonBarButtonData.HighlightState.NotHighlighted, null));
			for (int i = 0; i < this.tabs.Count; i++)
			{
				int s = i;
				this.tabs[i].onSelect = delegate
				{
					this.CurrentScreen = s;
					this.UpdateActiveScreen();
				};
			}
			this.screenTabs.SetButtons(this.tabs);
			for (int j = 0; j < this.tabs.Count; j++)
			{
				ButtonBar.ButtonBarButtonData buttonBarButtonData = this.tabs[j];
				ControlId.Assign((buttonBarButtonData != null) ? buttonBarButtonData.button.gameObject : null, string.Format("StatusScreensScreenTab:{0}", j), false);
			}
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000E969C File Offset: 0x000E789C
		private void freeTabs()
		{
			for (int i = 0; i < this.tabs.Count; i++)
			{
				this.tabs[i].free();
			}
			this.tabs.Clear();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000E96DC File Offset: 0x000E78DC
		public void SelectTab(FrameworkDataElement el)
		{
			ButtonBar.ButtonBarButtonData buttonBarButtonData = el as ButtonBar.ButtonBarButtonData;
			if (buttonBarButtonData != null)
			{
				buttonBarButtonData.onSelect();
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000E96FE File Offset: 0x000E78FE
		public void OnSearchTextChange(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				this.filterText = "";
			}
			else
			{
				this.filterText = text;
			}
			this.OnFilterUpdated();
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000E9722 File Offset: 0x000E7922
		public void OnFilterUpdated()
		{
			IStatusScreen statusScreen = this.activeScreen;
			if (statusScreen == null)
			{
				return;
			}
			statusScreen.FilterUpdated(this.filterText);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000E973C File Offset: 0x000E793C
		public void SetupContexts()
		{
			this.filterBar.SetupContext(true);
			this.filterBar.OnSearchTextChange.RemoveAllListeners();
			this.filterBar.OnSearchTextChange.AddListener(new UnityAction<string>(this.OnSearchTextChange));
			this.filterBar.filtersUpdated = new Action(this.OnFilterUpdated);
			NavigationContext navigationContext = this.navigationContext;
			if (navigationContext.axisHandlers == null)
			{
				navigationContext.axisHandlers = new Dictionary<InputAxisTypes, Action> { 
				{
					InputAxisTypes.NavigationPageXAxis,
					global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.NextPage), new Action(this.PrevPage), null))
				} };
			}
			navigationContext = this.navigationContext;
			if (navigationContext.buttonHandlers == null)
			{
				navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.CancelButton,
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit))
				} };
			}
			navigationContext = this.navigationContext;
			if (navigationContext.commandHandlers == null)
			{
				navigationContext.commandHandlers = new Dictionary<string, Action>
				{
					{
						StatusScreensScreen.SET_FILTER.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.filterBar.searchInput.EnterAndOpen();
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
			this.screenTabs.onSelected.RemoveAllListeners();
			this.screenTabs.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.SelectTab));
			this.screenTabs.SetupContext(InputAxisTypes.NavigationXAxis);
			this.screenGlobalContext.parentContext = this.navigationContext;
			this.horizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.vertNav.parentContext = this.screenGlobalContext;
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Clear();
			this.vertNav.contexts.Add(this.screenTabs.scrollContext);
			this.vertNav.contexts.Add(this.horizNav);
			this.bottomHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.bottomHorizNav.contexts.Clear();
			this.bottomHorizNav.contexts.Add(this.filterBar.searchInput.context);
			this.bottomHorizNav.contexts.Add(this.menuOptionScroller.scrollContext);
			this.vertNav.contexts.Add(this.bottomHorizNav);
			this.screenGlobalContext.Setup();
			this.vertNav.Setup();
			List<Transform> screens = this.Screens;
			if (screens != null)
			{
				screens.ForEach(delegate(Transform s)
				{
					s.GetComponent<IStatusScreen>().PrepareLayoutFrame();
				});
			}
			this.UpdateActiveScreen();
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000E99F0 File Offset: 0x000E7BF0
		public void UpdateViewFromData()
		{
			global::XRL.World.Event.PinCurrentPool();
			this.SetupContexts();
			this.SetButtons();
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000E9A03 File Offset: 0x000E7C03
		public void Cleanup()
		{
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000E9A05 File Offset: 0x000E7C05
		void ControlManager.IControllerChangedEvent.ControllerChanged()
		{
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000E9A07 File Offset: 0x000E7C07
		public void BackgroundClicked()
		{
			if (this.navigationContext.IsActive(true))
			{
				this.Exit();
			}
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000E9A1D File Offset: 0x000E7C1D
		public virtual void HandleMenuOption(FrameworkDataElement data)
		{
			IStatusScreen statusScreen = this.activeScreen;
			if (statusScreen == null)
			{
				return;
			}
			statusScreen.HandleMenuOption(data);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000E9A30 File Offset: 0x000E7C30
		public virtual void Update()
		{
			if (this.categoryBarLayoutGroup.enabled)
			{
				this.categoryBarLayoutFrames--;
				if (this.categoryBarLayoutFrames <= 0)
				{
					this.categoryBarLayoutGroup.enabled = false;
				}
			}
			if (!this.PickHide && StatusScreensScreen.hideWhenShown.Contains(GameManager.Instance.CurrentGameView))
			{
				if (base.canvas.enabled)
				{
					base.canvas.enabled = false;
				}
				this.PickHide = true;
			}
			else if (this.PickHide && GameManager.Instance.CurrentGameView == "StatusScreensScreen")
			{
				if (!base.canvas.enabled)
				{
					base.canvas.enabled = true;
				}
				this.PickHide = false;
			}
			if (this.navigationContext.IsActive(true))
			{
				if (this.previousContext != NavigationController.instance.activeContext || ControlManager.activeControllerType != this.previousType)
				{
					this.previousContext = NavigationController.instance.activeContext;
					this.previousType = ControlManager.activeControllerType;
					if (this.previousContext != null)
					{
						this.updateMenuBar = true;
					}
				}
				if (this.updateMenuBar)
				{
					this.menuOptionScroller.BeforeShow(null, NavigationController.GetMenuOptions(this.defaultMenuOptionOrder));
					this.menuOptionScroller.onSelected.RemoveAllListeners();
					this.menuOptionScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
					this.updateMenuBar = false;
					this.previousContext = NavigationController.instance.activeContext;
				}
			}
		}

		// Token: 0x040017BC RID: 6076
		public NavigationContext navigationContext = new NavigationContext();

		// Token: 0x040017BD RID: 6077
		public NavigationContext screenGlobalContext = new NavigationContext();

		// Token: 0x040017BE RID: 6078
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x040017BF RID: 6079
		public ScrollContext<NavigationContext> horizNav = new ScrollContext<NavigationContext>();

		// Token: 0x040017C0 RID: 6080
		public ScrollContext<NavigationContext> bottomHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x040017C1 RID: 6081
		public List<Transform> Screens;

		// Token: 0x040017C2 RID: 6082
		public ButtonBar screenTabs;

		// Token: 0x040017C3 RID: 6083
		public FilterBar filterBar;

		// Token: 0x040017C4 RID: 6084
		public global::UnityEngine.GameObject categoryBar;

		// Token: 0x040017C5 RID: 6085
		public NavigationContext categoryBarContext;

		// Token: 0x040017C6 RID: 6086
		public FrameworkScroller menuOptionScroller;

		// Token: 0x040017C7 RID: 6087
		public RememberMaxSize menuOptionSizeMinder;

		// Token: 0x040017C8 RID: 6088
		public Transform scaleTarget;

		// Token: 0x040017C9 RID: 6089
		public LayoutElement globalLayoutElement;

		// Token: 0x040017CA RID: 6090
		protected TaskCompletionSource<ScreenReturn> menucomplete = new TaskCompletionSource<ScreenReturn>();

		// Token: 0x040017CB RID: 6091
		public static global::XRL.World.GameObject GO;

		// Token: 0x040017CC RID: 6092
		public int CurrentScreen;

		// Token: 0x040017CD RID: 6093
		private IStatusScreen activeScreen;

		// Token: 0x040017CE RID: 6094
		public int categoryBarLayoutFrames = 3;

		// Token: 0x040017CF RID: 6095
		public VerticalLayoutGroup categoryBarLayoutGroup;

		// Token: 0x040017D0 RID: 6096
		private List<ButtonBar.ButtonBarButtonData> tabs = new List<ButtonBar.ButtonBarButtonData>(8);

		// Token: 0x040017D1 RID: 6097
		public static readonly MenuOption SET_FILTER = new MenuOption
		{
			Id = "SET_FILTER",
			KeyDescription = "Filter",
			InputCommand = "CmdFilter",
			Description = "Filter"
		};

		// Token: 0x040017D2 RID: 6098
		private string filterText = "";

		// Token: 0x040017D3 RID: 6099
		public List<MenuOption> defaultMenuOptionOrder = new List<MenuOption>
		{
			new MenuOption
			{
				InputCommand = "NavigationXYAxis",
				Description = "navigation",
				disabled = true
			},
			new MenuOption
			{
				InputCommand = "Accept",
				Description = "Accept",
				disabled = true
			}
		};

		// Token: 0x040017D4 RID: 6100
		public bool updateMenuBar;

		// Token: 0x040017D5 RID: 6101
		private ControlManager.InputDeviceType previousType;

		// Token: 0x040017D6 RID: 6102
		private NavigationContext previousContext;

		// Token: 0x040017D7 RID: 6103
		private static List<string> hideWhenShown = new List<string> { "PickDirection", "PickTarget" };

		// Token: 0x040017D8 RID: 6104
		private bool PickHide;

		// Token: 0x020018B9 RID: 6329
		public enum STATUS_SCREEN_ORDINAL
		{
			// Token: 0x04006359 RID: 25433
			TINKERING = 3
		}
	}
}
