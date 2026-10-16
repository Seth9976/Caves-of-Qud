using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Qud.API;
using UnityEngine.UI;
using XRL.CharacterBuilds.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000433 RID: 1075
	[UIView("Credits", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "Credits", UICanvasHost = 1)]
	public class Credits : SingletonWindowBase<Credits>
	{
		// Token: 0x060028C6 RID: 10438 RVA: 0x000D3558 File Offset: 0x000D1758
		public void SetupContext()
		{
			this.globalContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.globalContext.buttonHandlers.Set(InputButtonTypes.CancelButton, Event.Helpers.Handle(new Action(this.Exit)));
			this.midHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.midHorizNav.contexts.Clear();
			this.midHorizNav.contexts.Add(this.backButton.navigationContext);
			this.midHorizNav.contexts.Add(this.creditsScroller.GetNavigationContext());
			this.midHorizNav.Setup();
			this.midHorizNav.parentContext = this.globalContext;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000D3608 File Offset: 0x000D1808
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
			this.backButton.navigationContext.buttonHandlers.Set(InputButtonTypes.AcceptButton, Event.Helpers.Handle(new Action(this.Exit)));
			Task<List<SaveGameInfo>> savedGameInfo = SavesAPI.GetSavedGameInfo();
			Task.WaitAll(new Task[] { savedGameInfo });
			this.saves = savedGameInfo.Result.Select<SaveGameInfo, SaveInfoData>((SaveGameInfo info) => new SaveInfoData
			{
				SaveGame = info
			}).ToList<SaveInfoData>();
			this.creditsScroller.scrollContext.wraps = true;
			this.creditsScroller.BeforeShow(null, this.saves);
			foreach (SaveManagementRow saveManagementRow in this.creditsScroller.selectionClones.Select<FrameworkUnityScrollChild, SaveManagementRow>((FrameworkUnityScrollChild s) => s.GetComponent<SaveManagementRow>()))
			{
				saveManagementRow != null;
			}
			if (this.SelectFirst)
			{
				this.SelectFirst = false;
				this.creditsScroller.scrollContext.selectedPosition = 0;
			}
			else if (this.creditsScroller.scrollContext.selectedPosition >= this.saves.Count)
			{
				this.creditsScroller.scrollContext.selectedPosition = Math.Max(this.saves.Count - 1, 0);
			}
			this.creditsScroller.onSelected.RemoveAllListeners();
			this.SetupContext();
			this.EnableNavContext();
			this.UpdateMenuBars();
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000D37E0 File Offset: 0x000D19E0
		public override void Hide()
		{
			base.Hide();
			this.DisableNavContext(true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000D37FB File Offset: 0x000D19FB
		public void EnableNavContext()
		{
			this.globalContext.disabled = false;
			this.creditsScroller.GetNavigationContext().ActivateAndEnable();
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000D381A File Offset: 0x000D1A1A
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

		// Token: 0x060028CB RID: 10443 RVA: 0x000D3854 File Offset: 0x000D1A54
		public void Exit()
		{
			UIManager.showWindow("MainMenu", false);
			this.Hide();
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000D3868 File Offset: 0x000D1A68
		public void UpdateMenuBars()
		{
			List<MenuOption> list = new List<MenuOption>();
			list.Add(new MenuOption
			{
				InputCommand = "NavigationXYAxis",
				Description = "navigate"
			});
			list.Add(new MenuOption
			{
				KeyDescription = "space",
				Description = "select"
			});
			this.hotkeyBar.GetNavigationContext().disabled = true;
			this.hotkeyBar.BeforeShow(null, list);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000D38DC File Offset: 0x000D1ADC
		public void Update()
		{
			if (this.globalContext.IsActive(true))
			{
				NavigationController instance = NavigationController.instance;
				bool? flag;
				if (instance == null)
				{
					flag = null;
				}
				else
				{
					NavigationContext activeContext = instance.activeContext;
					if (activeContext == null)
					{
						flag = null;
					}
					else
					{
						FrameworkScroller frameworkScroller = this.creditsScroller;
						flag = new bool?(activeContext.IsInside((frameworkScroller != null) ? frameworkScroller.GetNavigationContext() : null));
					}
				}
				bool? flag2 = flag;
				if (flag2.GetValueOrDefault() != this.wasInScroller)
				{
					this.UpdateMenuBars();
				}
			}
		}

		// Token: 0x04001475 RID: 5237
		protected List<SaveInfoData> saves;

		// Token: 0x04001476 RID: 5238
		public Image background;

		// Token: 0x04001477 RID: 5239
		public FrameworkScroller hotkeyBar;

		// Token: 0x04001478 RID: 5240
		public FrameworkScroller creditsScroller;

		// Token: 0x04001479 RID: 5241
		public EmbarkBuilderModuleBackButton backButton;

		// Token: 0x0400147A RID: 5242
		public NavigationContext globalContext = new NavigationContext();

		// Token: 0x0400147B RID: 5243
		public ScrollContext<NavigationContext> midHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x0400147C RID: 5244
		private bool SelectFirst = true;

		// Token: 0x0400147D RID: 5245
		public bool wasInScroller;
	}
}
