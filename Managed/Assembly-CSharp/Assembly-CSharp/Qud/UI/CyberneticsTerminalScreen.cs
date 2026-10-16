using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using XRL;
using XRL.CharacterBuilds.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000436 RID: 1078
	[UIView("CyberneticsTerminalScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "CyberneticsTerminal", UICanvasHost = 1)]
	public class CyberneticsTerminalScreen : SingletonWindowBase<CyberneticsTerminalScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x000D3CE2 File Offset: 0x000D1EE2
		public bool breakBackButton
		{
			get
			{
				return this.lastWidth <= this.breakpointBackButtonWidth;
			}
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000D3CF8 File Offset: 0x000D1EF8
		public static async Task<bool> ShowCyberneticsTerminal(CyberneticsTerminal terminal)
		{
			return await SingletonWindowBase<CyberneticsTerminalScreen>.instance._ShowCyberneticsTerminal(terminal);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000D3D3C File Offset: 0x000D1F3C
		public static async Task<bool> ShowGenericTerminal(GenericTerminal terminal)
		{
			return await SingletonWindowBase<CyberneticsTerminalScreen>.instance._ShowGenericTerminal(terminal);
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000D3D80 File Offset: 0x000D1F80
		public async Task<bool> _ShowCyberneticsTerminal(CyberneticsTerminal terminal)
		{
			this.genericTerminal = null;
			this.cyberneticsTerminal = terminal;
			this.displayScroller.ScrollOnSelection = new Func<bool>(this.ShouldScrollToSelection);
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetCanceled();
			}
			this.completionSource = new TaskCompletionSource<bool>();
			await The.UiContext;
			ControlManager.ResetInput(false, true);
			this.FooterText = "";
			CyberneticsTerminalScreen.initReady = false;
			await APIDispatch.RunAndWaitAsync(delegate
			{
				this.cyberneticsTerminal.CurrentScreen.BeforeRender(null, ref this.FooterText);
			});
			this.Show();
			CyberneticsTerminalScreen.initReady = true;
			bool info = await this.completionSource.Task;
			this.DisableNavContext(true);
			await The.UiContext;
			this.Hide();
			return info;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000D3DCC File Offset: 0x000D1FCC
		public async Task<bool> _ShowGenericTerminal(GenericTerminal terminal)
		{
			this.genericTerminal = terminal;
			this.cyberneticsTerminal = null;
			this.displayScroller.ScrollOnSelection = new Func<bool>(this.ShouldScrollToSelection);
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetCanceled();
			}
			this.completionSource = new TaskCompletionSource<bool>();
			await The.UiContext;
			ControlManager.ResetInput(false, true);
			this.FooterText = "";
			CyberneticsTerminalScreen.initReady = false;
			await APIDispatch.RunAndWaitAsync(delegate
			{
				this.genericTerminal.currentScreen.BeforeRender(null, ref this.FooterText);
			});
			this.Show();
			CyberneticsTerminalScreen.initReady = true;
			bool info = await this.completionSource.Task;
			this.DisableNavContext(true);
			await The.UiContext;
			this.Hide();
			return info;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000D3E17 File Offset: 0x000D2017
		public bool ShouldScrollToSelection()
		{
			return !ScrollViewCalcs.GetScrollViewCalcs(this.displayScroller.GetPrefabForIndex(this.displayScroller.selectedPosition).transform as RectTransform, this._svc).isAnyInView;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000D3E4C File Offset: 0x000D204C
		public void Exit()
		{
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource == null)
			{
				return;
			}
			taskCompletionSource.TrySetResult(false);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000D3E60 File Offset: 0x000D2060
		public void Update()
		{
			if (this.globalContext.IsActive(true))
			{
				NavigationContext activeContext = NavigationController.instance.activeContext;
				bool flag = activeContext != null && activeContext.IsInside(this.displayScroller.GetNavigationContext());
				float width = base.rectTransform.rect.width;
				if (flag != this.wasInScroller || this.lastWidth != width)
				{
					this.wasInScroller = flag;
					this.lastWidth = width;
					if (this.safeArea != null)
					{
						this.safeArea.offsetMin = new Vector2((float)(this.breakBackButton ? 10 : 150), this.safeArea.offsetMin.y);
						this.safeArea.offsetMax = new Vector2((float)(this.breakBackButton ? (-10) : (-150)), this.safeArea.offsetMax.y);
					}
					this.UpdateMenuBars();
				}
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000D3F4F File Offset: 0x000D214F
		public void ControllerChanged()
		{
			this.UpdateMenuBars();
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000D3F58 File Offset: 0x000D2158
		public void SetupContext()
		{
			this.globalContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.globalContext.buttonHandlers.Set(InputButtonTypes.CancelButton, global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit)));
			this.globalContext.commandHandlers = new Dictionary<string, Action> { 
			{
				CyberneticsTerminalScreen.BACK_BUTTON.InputCommand,
				global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit))
			} };
			this.midHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.midHorizNav.contexts.Clear();
			this.midHorizNav.contexts.Add(this.vertNav);
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Clear();
			this.vertNav.contexts.Add(this.displayScroller.GetNavigationContext());
			this.vertNav.contexts.Add(this.hotkeyBar.GetNavigationContext());
			this.midHorizNav.Setup();
			this.midHorizNav.parentContext = this.globalContext;
			this.displayScroller.scrollContext.wraps = false;
			this.vertNav.wraps = false;
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000D4087 File Offset: 0x000D2287
		public static MenuOption BACK_BUTTON
		{
			get
			{
				return EmbarkBuilderOverlayWindow.BackMenuOption;
			}
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000D4090 File Offset: 0x000D2290
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
				Description = "accept"
			});
			this.keyMenuOptions.Add(new MenuOption
			{
				InputCommand = "Cancel",
				Description = "quit"
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

		// Token: 0x060028E2 RID: 10466 RVA: 0x000D41C0 File Offset: 0x000D23C0
		public void HandleMenuOption(FrameworkDataElement data)
		{
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000D41C4 File Offset: 0x000D23C4
		public async Task HandleTextComplete()
		{
			CyberneticsTerminal cyberneticsTerminal = this.cyberneticsTerminal;
			TerminalScreen lastScreen = ((cyberneticsTerminal != null) ? cyberneticsTerminal.CurrentScreen : null);
			await APIDispatch.RunAndWaitAsync(delegate
			{
				CyberneticsTerminal cyberneticsTerminal3 = this.cyberneticsTerminal;
				if (cyberneticsTerminal3 == null)
				{
					return;
				}
				TerminalScreen currentScreen = cyberneticsTerminal3.CurrentScreen;
				if (currentScreen == null)
				{
					return;
				}
				currentScreen.TextComplete();
			});
			CyberneticsTerminal cyberneticsTerminal2 = this.cyberneticsTerminal;
			if (((cyberneticsTerminal2 != null) ? cyberneticsTerminal2.CurrentScreen : null) != lastScreen)
			{
				this.Show();
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000D4207 File Offset: 0x000D2407
		public IEnumerable<FrameworkDataElement> GetMenuItems()
		{
			CyberneticsTerminalLineData cyberneticsTerminalLineData = new CyberneticsTerminalLineData();
			CyberneticsTerminal cyberneticsTerminal = this.cyberneticsTerminal;
			object obj;
			if (cyberneticsTerminal == null)
			{
				obj = null;
			}
			else
			{
				TerminalScreen currentScreen = cyberneticsTerminal.CurrentScreen;
				obj = ((currentScreen != null) ? currentScreen.RenderedTextForModernUI : null);
			}
			object obj2;
			if ((obj2 = obj) == null)
			{
				GenericTerminal genericTerminal = this.genericTerminal;
				if (genericTerminal == null)
				{
					obj2 = null;
				}
				else
				{
					GenericTerminalScreen currentScreen2 = genericTerminal.currentScreen;
					obj2 = ((currentScreen2 != null) ? currentScreen2.RenderedTextForModernUI : null);
				}
			}
			cyberneticsTerminalLineData.Text = obj2;
			cyberneticsTerminalLineData.OptionID = -1;
			cyberneticsTerminalLineData.screen = this;
			yield return cyberneticsTerminalLineData;
			if (this.cyberneticsTerminal != null)
			{
				int num;
				for (int o = 0; o < this.cyberneticsTerminal.CurrentScreen.Options.Count; o = num + 1)
				{
					yield return new CyberneticsTerminalLineData
					{
						Text = this.cyberneticsTerminal.CurrentScreen.Options[o],
						screen = this,
						OptionID = o
					};
					num = o;
				}
			}
			if (this.genericTerminal != null)
			{
				int num;
				for (int o = 0; o < this.genericTerminal.currentScreen.Options.Count; o = num + 1)
				{
					yield return new CyberneticsTerminalLineData
					{
						Text = this.genericTerminal.currentScreen.Options[o],
						screen = this,
						OptionID = o
					};
					num = o;
				}
			}
			yield break;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000D4218 File Offset: 0x000D2418
		public override void Show()
		{
			base.Show();
			this.footerTextSkin.SetText(this.FooterText);
			this.displayScroller.scrollContext.wraps = false;
			ControlManager.ResetInput(false, false);
			List<FrameworkDataElement> list = this.GetMenuItems().ToList<FrameworkDataElement>();
			for (int i = 0; i < list.Count - 1; i++)
			{
				((CyberneticsTerminalLineData)list[i]).nextCursorData = (CyberneticsTerminalLineData)list[i + 1];
			}
			this.displayScroller.BeforeShow(null, list);
			if (list.Count > 0)
			{
				((CyberneticsTerminalLineData)list[0]).row.currentCursor = true;
			}
			this.displayScroller.scrollContext.selectedPosition = 1;
			this.displayScroller.onSelected.RemoveAllListeners();
			this.displayScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelect));
			this.displayScroller.onHighlight.RemoveAllListeners();
			this.UpdateMenuBars();
			this.SetupContext();
			this.EnableNavContext();
			this.displayScroller.scrollContext.ActivateAndEnable();
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000D4330 File Offset: 0x000D2530
		public async void HandleSelect(FrameworkDataElement element)
		{
			CyberneticsTerminalLineData cat = element as CyberneticsTerminalLineData;
			if (cat != null)
			{
				if (cat.CursorDone)
				{
					if (this.cyberneticsTerminal != null)
					{
						this.cyberneticsTerminal.Selected = cat.OptionID;
						if (this.cyberneticsTerminal.Selected > -1)
						{
							await APIDispatch.RunAndWaitAsync(delegate
							{
								this.cyberneticsTerminal.CurrentScreen.Activate();
							});
							if (this.cyberneticsTerminal.CurrentScreen == null)
							{
								TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
								if (taskCompletionSource != null)
								{
									taskCompletionSource.TrySetResult(false);
								}
							}
							else
							{
								this.FooterText = "";
								CyberneticsTerminalScreen.initReady = false;
								await APIDispatch.RunAndWaitAsync(delegate
								{
									this.cyberneticsTerminal.CurrentScreen.BeforeRender(null, ref this.FooterText);
								});
								this.Show();
								CyberneticsTerminalScreen.initReady = true;
							}
						}
					}
					if (this.genericTerminal != null)
					{
						this.genericTerminal.nSelected = cat.OptionID;
						if (this.genericTerminal.nSelected > -1)
						{
							await APIDispatch.RunAndWaitAsync(delegate
							{
								this.genericTerminal.currentScreen.Activate();
							});
							if (this.genericTerminal.currentScreen == null)
							{
								TaskCompletionSource<bool> taskCompletionSource2 = this.completionSource;
								if (taskCompletionSource2 != null)
								{
									taskCompletionSource2.TrySetResult(false);
								}
							}
							else
							{
								this.FooterText = "";
								CyberneticsTerminalScreen.initReady = false;
								await APIDispatch.RunAndWaitAsync(delegate
								{
									this.genericTerminal.currentScreen.BeforeRender(null, ref this.FooterText);
								});
								this.Show();
								CyberneticsTerminalScreen.initReady = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000D436F File Offset: 0x000D256F
		public override void Hide()
		{
			base.Hide();
			this.DisableNavContext(true);
			ControlManager.ResetInput(false, true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000D4391 File Offset: 0x000D2591
		public void EnableNavContext()
		{
			this.globalContext.disabled = false;
			this.displayScroller.GetNavigationContext().ActivateAndEnable();
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000D43B0 File Offset: 0x000D25B0
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

		// Token: 0x0400148F RID: 5263
		public FrameworkScroller hotkeyBar;

		// Token: 0x04001490 RID: 5264
		public FrameworkScroller displayScroller;

		// Token: 0x04001491 RID: 5265
		public RectTransform safeArea;

		// Token: 0x04001492 RID: 5266
		public EmbarkBuilderModuleBackButton backButton;

		// Token: 0x04001493 RID: 5267
		public TaskCompletionSource<bool> completionSource;

		// Token: 0x04001494 RID: 5268
		public NavigationContext globalContext = new NavigationContext();

		// Token: 0x04001495 RID: 5269
		public ScrollContext<NavigationContext> midHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001496 RID: 5270
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001497 RID: 5271
		public Dictionary<string, bool> categoryExpanded = new Dictionary<string, bool>();

		// Token: 0x04001498 RID: 5272
		public float lastWidth;

		// Token: 0x04001499 RID: 5273
		public float breakpointBackButtonWidth;

		// Token: 0x0400149A RID: 5274
		public string FooterText = "";

		// Token: 0x0400149B RID: 5275
		private GenericTerminal genericTerminal;

		// Token: 0x0400149C RID: 5276
		private CyberneticsTerminal cyberneticsTerminal;

		// Token: 0x0400149D RID: 5277
		public static bool initReady;

		// Token: 0x0400149E RID: 5278
		private ScrollViewCalcs _svc = new ScrollViewCalcs();

		// Token: 0x0400149F RID: 5279
		public bool wasInScroller;

		// Token: 0x040014A0 RID: 5280
		public List<MenuOption> keyMenuOptions = new List<MenuOption>();

		// Token: 0x040014A1 RID: 5281
		public UITextSkin footerTextSkin;
	}
}
