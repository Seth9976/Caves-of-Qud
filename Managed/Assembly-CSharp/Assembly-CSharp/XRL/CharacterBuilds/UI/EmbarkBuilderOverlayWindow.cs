using System;
using System.Collections.Generic;
using Qud.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.UI
{
	// Token: 0x020005C1 RID: 1473
	[UIView("EmbarkBuilder:Overlay", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/Overlay", UICanvasHost = 1)]
	public class EmbarkBuilderOverlayWindow : WindowBase, ControlManager.IControllerChangedEvent
	{
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x001342B6 File Offset: 0x001324B6
		public bool breakKeyDescriptions
		{
			get
			{
				return this._lastHeight <= (float)this.keyDescriptionsBreakpoint;
			}
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x001342CA File Offset: 0x001324CA
		public virtual void Back()
		{
			this.builder.back();
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x001342D7 File Offset: 0x001324D7
		public virtual void Next()
		{
			this.builder.advance(false, true, false);
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x001342E8 File Offset: 0x001324E8
		public void BeforeShowWithWindow(EmbarkBuilder builder, EmbarkBuilderModuleWindowDescriptor windowDescriptor)
		{
			this.builder = builder;
			this.currentWindowDescriptor = windowDescriptor;
			this.globalContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.globalContext.buttonHandlers.Set(InputButtonTypes.CancelButton, XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Back)));
			this.globalContext.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			Dictionary<InputAxisTypes, Action> axisHandlers = this.globalContext.axisHandlers;
			InputAxisTypes inputAxisTypes = InputAxisTypes.NavigationPageXAxis;
			Action action = new Action(this.Back);
			axisHandlers.Set(inputAxisTypes, XRL.UI.Framework.Event.Helpers.Handle(XRL.UI.Framework.Event.Helpers.Axis(new Action(this.Next), action, null)));
			if (Media.sizeClass < Media.SizeClass.Medium)
			{
				GameObject gameObject = this.backButtonLg;
				if (gameObject != null)
				{
					gameObject.gameObject.SetActive(false);
				}
				GameObject gameObject2 = this.backButtonSm;
				if (gameObject2 != null)
				{
					gameObject2.gameObject.SetActive(true);
				}
				this.backButton = this.backButtonSm.GetComponent<EmbarkBuilderModuleBackButton>();
				if (this.backButton.navigationContext == null)
				{
					this.backButton.Awake();
				}
				this.backButton.navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
				this.backButton.navigationContext.buttonHandlers.Set(InputButtonTypes.AcceptButton, XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Back)));
			}
			else
			{
				GameObject gameObject3 = this.backButtonLg;
				if (gameObject3 != null)
				{
					gameObject3.gameObject.SetActive(true);
				}
				GameObject gameObject4 = this.backButtonSm;
				if (gameObject4 != null)
				{
					gameObject4.gameObject.SetActive(false);
				}
				this.backButton = this.backButtonLg.GetComponent<EmbarkBuilderModuleBackButton>();
				if (this.backButton.navigationContext == null)
				{
					this.backButton.Awake();
				}
				this.backButton.navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
				this.backButton.navigationContext.buttonHandlers.Set(InputButtonTypes.AcceptButton, XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Back)));
			}
			if (Media.sizeClass < Media.SizeClass.Medium)
			{
				GameObject gameObject5 = this.nextButtonLg;
				if (gameObject5 != null)
				{
					gameObject5.gameObject.SetActive(false);
				}
				GameObject gameObject6 = this.nextButtonSm;
				if (gameObject6 != null)
				{
					gameObject6.gameObject.SetActive(true);
				}
				this.nextButton = this.nextButtonSm.GetComponent<EmbarkBuilderModuleBackButton>();
				if (this.nextButton.navigationContext == null)
				{
					this.nextButton.Awake();
				}
				this.nextButton.navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
				this.nextButton.navigationContext.buttonHandlers.Set(InputButtonTypes.AcceptButton, XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Next)));
			}
			else
			{
				GameObject gameObject7 = this.nextButtonLg;
				if (gameObject7 != null)
				{
					gameObject7.gameObject.SetActive(true);
				}
				GameObject gameObject8 = this.nextButtonSm;
				if (gameObject8 != null)
				{
					gameObject8.gameObject.SetActive(false);
				}
				this.nextButton = this.nextButtonLg.GetComponent<EmbarkBuilderModuleBackButton>();
				if (this.nextButton.navigationContext == null)
				{
					this.nextButton.Awake();
				}
				this.nextButton.navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
				this.nextButton.navigationContext.buttonHandlers.Set(InputButtonTypes.AcceptButton, XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Next)));
			}
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.midHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.UpdateBreadcrumbs();
			this.UpdateMenuBars(windowDescriptor);
			base.transform.SetAsLastSibling();
			this.WireNavContexts();
			this.UpdateSafeArea();
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x00134630 File Offset: 0x00132830
		public void UpdateBreadcrumbs()
		{
			if (this.builder == null)
			{
				return;
			}
			if (this.builder.SkippingUIUpdates)
			{
				return;
			}
			List<ChoiceWithColorIcon> list = new List<ChoiceWithColorIcon>(this.builder.GetBreadcrumbs());
			this.hasCrumbs = list.Count > 0;
			if (this.hasCrumbs)
			{
				this.breadcrumbs.scrollContext.parentContext = this.vertNav;
				this.breadcrumbs.BeforeShow(null, list);
				for (int i = 0; i < this.breadcrumbs.scrollContext.length; i++)
				{
					this.breadcrumbs.GetPrefabForIndex(i).GetComponent<TitledIconButton>().TitleText.gameObject.SetActive(true);
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(base.rectTransform);
				Canvas.ForceUpdateCanvases();
				bool flag = false;
				int num = 0;
				while (!flag && num < this.breadcrumbs.scrollContext.length)
				{
					RectTransform component = this.breadcrumbs.GetPrefabForIndex(num).GetComponent<TitledIconButton>().TitleText.GetComponent<RectTransform>();
					float height = component.rect.height;
					float minHeight = LayoutUtility.GetMinHeight(component);
					if (height > minHeight)
					{
						flag = true;
					}
					num++;
				}
				for (int j = 0; j < this.breadcrumbs.scrollContext.length; j++)
				{
					this.breadcrumbs.GetPrefabForIndex(j).GetComponent<TitledIconButton>().TitleText.gameObject.SetActive(!flag);
				}
				this.breadcrumbs.onSelected.RemoveAllListeners();
				this.breadcrumbs.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.builder.ShowFromCrumb));
				if (!this.breadcrumbs.gameObject.activeInHierarchy)
				{
					this.breadcrumbs.gameObject.SetActive(true);
					return;
				}
			}
			else
			{
				this.breadcrumbs.gameObject.SetActive(false);
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x001347F9 File Offset: 0x001329F9
		public void UpdateSafeArea()
		{
			this.safeArea == null;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x00134808 File Offset: 0x00132A08
		public bool breakNavButtons
		{
			get
			{
				return base.rectTransform.rect.width < (float)this.navButtonBreakpoint;
			}
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x00134834 File Offset: 0x00132A34
		public void WireNavContexts()
		{
			this.vertNav.contexts.Clear();
			this.midHorizNav.contexts.Clear();
			if (!this.breakNavButtons)
			{
				this.midHorizNav.contexts.Add(this.backButton.navigationContext);
			}
			if (this.hasCrumbs)
			{
				this.vertNav.contexts.Add(this.breadcrumbs.scrollContext);
			}
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = this.currentWindowDescriptor;
			NavigationContext navigationContext;
			if (embarkBuilderModuleWindowDescriptor == null)
			{
				navigationContext = null;
			}
			else
			{
				AbstractBuilderModuleWindowBase window = embarkBuilderModuleWindowDescriptor.getWindow();
				navigationContext = ((window != null) ? window.GetNavigationContext() : null);
			}
			NavigationContext navigationContext2 = navigationContext;
			if (navigationContext2 != null)
			{
				if (this.breakNavButtons)
				{
					this.vertNav.contexts.Add(navigationContext2);
				}
				else
				{
					this.midHorizNav.contexts.Add(navigationContext2);
				}
			}
			if (this.midHorizNav.contexts.Count > 0 && !this.breakNavButtons)
			{
				this.vertNav.contexts.Add(this.midHorizNav);
			}
			this.midHorizNav.contexts.Add(this.nextButton.navigationContext);
			this.vertNav.contexts.Add(this.menuBar.scrollContext);
			this.vertNav.Setup();
			this.vertNav.parentContext = this.globalContext;
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x00134978 File Offset: 0x00132B78
		public IEnumerable<MenuOption> GetKeyMenuBar()
		{
			if (this.breakNavButtons)
			{
				yield return EmbarkBuilderOverlayWindow.BackMenuOption;
				yield return EmbarkBuilderOverlayWindow.NextMenuOption;
			}
			yield break;
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x00134988 File Offset: 0x00132B88
		public void UpdateMenuBars()
		{
			if (this.builder != null && this.builder.SkippingUIUpdates)
			{
				return;
			}
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = this.currentWindowDescriptor;
			bool? flag;
			if (embarkBuilderModuleWindowDescriptor == null)
			{
				flag = null;
			}
			else
			{
				AbstractBuilderModuleWindowBase window = embarkBuilderModuleWindowDescriptor.getWindow();
				flag = ((window != null) ? new bool?(window.isActiveAndEnabled) : null);
			}
			bool? flag2 = flag;
			if (flag2.GetValueOrDefault())
			{
				this.UpdateMenuBars(this.currentWindowDescriptor);
			}
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x001349FC File Offset: 0x00132BFC
		public void UpdateMenuBars(EmbarkBuilderModuleWindowDescriptor windowDescriptor)
		{
			if (this.builder != null && this.builder.SkippingUIUpdates)
			{
				return;
			}
			AbstractBuilderModuleWindowBase abstractBuilderModuleWindowBase = ((windowDescriptor != null) ? windowDescriptor.getWindow() : null);
			if (abstractBuilderModuleWindowBase != null)
			{
				List<MenuOption> list = new List<MenuOption>(this.GetKeyMenuBar());
				list.AddRange(abstractBuilderModuleWindowBase.GetKeyMenuBar());
				this.globalContext.commandHandlers = new Dictionary<string, Action>();
				list.ForEach(delegate(MenuOption option)
				{
					if (!string.IsNullOrEmpty(option.InputCommand))
					{
						this.globalContext.commandHandlers.Add(option.InputCommand, delegate
						{
							this.HandleMenuOption(option);
						});
					}
				});
				HorizontalMenuScroller horizontalMenuScroller = this.menuBar;
				if (horizontalMenuScroller != null)
				{
					horizontalMenuScroller.BeforeShow(windowDescriptor, list);
				}
				this.menuBar.onSelected.RemoveAllListeners();
				this.menuBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
				this.legendBar.gameObject.SetActive(!this.breakKeyDescriptions);
				if (!this.breakKeyDescriptions)
				{
					this.legendBar.BeforeShow(windowDescriptor, abstractBuilderModuleWindowBase.GetKeyLegend());
					this.legendBar.scrollContext.disabled = true;
				}
				this.backButton.menuOption = EmbarkBuilderOverlayWindow.BackMenuOption;
				this.backButton.ForceUpdate();
				this.nextButton.menuOption = EmbarkBuilderOverlayWindow.NextMenuOption;
				this.nextButton.navigationContext.disabled = abstractBuilderModuleWindowBase.DataErrors() != null;
				this.nextButton.ForceUpdate();
			}
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x00134B4C File Offset: 0x00132D4C
		public void HandleMenuOption(FrameworkDataElement option)
		{
			if (option.Id == "Back")
			{
				this.Back();
				return;
			}
			if (option.Id == "Next")
			{
				this.Next();
				return;
			}
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = this.currentWindowDescriptor;
			if (embarkBuilderModuleWindowDescriptor == null)
			{
				return;
			}
			AbstractBuilderModuleWindowBase window = embarkBuilderModuleWindowDescriptor.getWindow();
			if (window == null)
			{
				return;
			}
			window.HandleMenuOption(option);
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x00134BA8 File Offset: 0x00132DA8
		public void Update()
		{
			if (!base.canvas.enabled)
			{
				base.gameObject.SetActive(false);
			}
			if (base.rectTransform.rect.width != this._lastWidth || base.rectTransform.rect.height != this._lastHeight)
			{
				this._lastWidth = base.rectTransform.rect.width;
				this._lastHeight = base.rectTransform.rect.height;
				this.UpdateBreadcrumbs();
				this.UpdateMenuBars(this.currentWindowDescriptor);
				this.WireNavContexts();
				this.backButton.gameObject.SetActive(!this.breakNavButtons);
				this.nextButton.gameObject.SetActive(!this.breakNavButtons);
				this.UpdateSafeArea();
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x00134C89 File Offset: 0x00132E89
		public void ControllerChanged()
		{
			this.UpdateMenuBars();
		}

		// Token: 0x04001EF3 RID: 7923
		public EmbarkBuilderModuleBackButton backButton;

		// Token: 0x04001EF4 RID: 7924
		public EmbarkBuilderModuleBackButton nextButton;

		// Token: 0x04001EF5 RID: 7925
		public HorizontalIconScroller breadcrumbs;

		// Token: 0x04001EF6 RID: 7926
		public NavigationContext globalContext = new NavigationContext();

		// Token: 0x04001EF7 RID: 7927
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001EF8 RID: 7928
		public ScrollContext<NavigationContext> midHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001EF9 RID: 7929
		public EmbarkBuilder builder;

		// Token: 0x04001EFA RID: 7930
		public HorizontalMenuScroller menuBar;

		// Token: 0x04001EFB RID: 7931
		public HorizontalMenuScroller legendBar;

		// Token: 0x04001EFC RID: 7932
		public RectTransform safeArea;

		// Token: 0x04001EFD RID: 7933
		public int navButtonBreakpoint = 900;

		// Token: 0x04001EFE RID: 7934
		public int keyDescriptionsBreakpoint = 640;

		// Token: 0x04001EFF RID: 7935
		public EmbarkBuilderModuleWindowDescriptor currentWindowDescriptor;

		// Token: 0x04001F00 RID: 7936
		public GameObject nextButtonLg;

		// Token: 0x04001F01 RID: 7937
		public GameObject nextButtonSm;

		// Token: 0x04001F02 RID: 7938
		public GameObject backButtonSm;

		// Token: 0x04001F03 RID: 7939
		public GameObject backButtonLg;

		// Token: 0x04001F04 RID: 7940
		public bool hasCrumbs;

		// Token: 0x04001F05 RID: 7941
		public static MenuOption BackMenuOption = new MenuOption
		{
			Id = "Back",
			InputCommand = "Cancel",
			KeyDescription = "Esc",
			Description = "Back"
		};

		// Token: 0x04001F06 RID: 7942
		public static MenuOption NextMenuOption = new MenuOption
		{
			Id = "Next",
			InputCommand = "Page Right",
			KeyDescription = "End",
			Description = "Next"
		};

		// Token: 0x04001F07 RID: 7943
		public float _lastWidth;

		// Token: 0x04001F08 RID: 7944
		public float _lastHeight;
	}
}
