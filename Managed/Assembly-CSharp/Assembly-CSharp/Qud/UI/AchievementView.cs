using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XRL;
using XRL.CharacterBuilds.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200042D RID: 1069
	[UIView("AchievementView", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "AchievementView", UICanvasHost = 1)]
	public class AchievementView : SingletonWindowBase<AchievementView>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x06002873 RID: 10355 RVA: 0x000D1E60 File Offset: 0x000D0060
		public void SetupContext()
		{
			this.GlobalNavContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.GlobalNavContext.buttonHandlers.Set(InputButtonTypes.CancelButton, Event.Helpers.Handle(new Action(this.Exit)));
			this.HorizontalNavigation.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.HorizontalNavigation.contexts.Clear();
			this.HorizontalNavigation.contexts.Add(this.Back.navigationContext);
			this.HorizontalNavigation.contexts.Add(this.SavesScroller.GetNavigationContext());
			this.HorizontalNavigation.Setup();
			this.HorizontalNavigation.parentContext = this.GlobalNavContext;
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000D1F10 File Offset: 0x000D0110
		public override void Show()
		{
			base.Show();
			EmbarkBuilderModuleBackButton back = this.Back;
			if (back != null)
			{
				back.gameObject.SetActive(true);
			}
			if (this.Back.navigationContext == null)
			{
				this.Back.Awake();
			}
			this.Back.navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.Back.navigationContext.buttonHandlers.Set(InputButtonTypes.AcceptButton, Event.Helpers.Handle(new Action(this.Exit)));
			Dictionary<string, AchievementInfo> achievements = AchievementManager.State.Achievements;
			if (this.Data == null)
			{
				this.Data = new List<FrameworkDataElement>(achievements.Count);
			}
			this.Data.Clear();
			int num = 0;
			foreach (AchievementInfo achievementInfo in from x in achievements.Values
				orderby x.Achieved descending, x.TimeStamp descending
				select x)
			{
				if (achievementInfo.Hidden && !achievementInfo.Achieved)
				{
					num++;
				}
				else
				{
					this.Data.Add(new AchievementInfoData(achievementInfo));
				}
			}
			if (num > 0)
			{
				this.Data.Add(new HiddenAchievementData(num));
			}
			this.SavesScroller.scrollContext.wraps = true;
			this.SavesScroller.BeforeShow(null, this.Data);
			if (this.SelectFirst)
			{
				this.SelectFirst = false;
				this.SavesScroller.scrollContext.selectedPosition = 0;
			}
			else if (this.SavesScroller.scrollContext.selectedPosition >= this.Data.Count)
			{
				this.SavesScroller.scrollContext.selectedPosition = Math.Max(this.Data.Count - 1, 0);
			}
			this.SetupContext();
			this.EnableNavContext();
			this.UpdateMenuBars();
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000D2114 File Offset: 0x000D0314
		public async Task<bool> ShowMenuAsync()
		{
			this.MenuClosed.TrySetCanceled();
			this.MenuClosed = new TaskCompletionSource<bool>();
			await The.UiContext;
			UIManager.showWindow("AchievementView", false);
			return await this.MenuClosed.Task;
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000D2157 File Offset: 0x000D0357
		public override void Hide()
		{
			base.Hide();
			this.DisableNavContext(true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000D2172 File Offset: 0x000D0372
		public void EnableNavContext()
		{
			this.GlobalNavContext.disabled = false;
			this.SavesScroller.GetNavigationContext().ActivateAndEnable();
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000D2191 File Offset: 0x000D0391
		public void DisableNavContext(bool deactivate = true)
		{
			if (deactivate)
			{
				NavigationContext activeContext = NavigationController.instance.activeContext;
				if (activeContext != null && activeContext.IsInside(this.GlobalNavContext))
				{
					NavigationController.instance.activeContext = null;
				}
			}
			this.GlobalNavContext.disabled = true;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000D21CB File Offset: 0x000D03CB
		public void Exit()
		{
			this.MenuClosed.TrySetResult(true);
			this.Hide();
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000D21E0 File Offset: 0x000D03E0
		public void UpdateMenuBars()
		{
			if (this.GlobalNavContext.IsActive(true))
			{
				List<MenuOption> list = new List<MenuOption>();
				list.Add(new MenuOption
				{
					InputCommand = "NavigationXYAxis",
					Description = "navigate"
				});
				this.HotkeyBar.GetNavigationContext().disabled = true;
				this.HotkeyBar.BeforeShow(null, list);
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000D2240 File Offset: 0x000D0440
		public void Update()
		{
			NavigationContext activeContext = NavigationController.instance.activeContext;
			if ((activeContext != null && activeContext.IsInside(this.SavesScroller.GetNavigationContext())) != this.wasInScroller)
			{
				this.UpdateMenuBars();
			}
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000D2271 File Offset: 0x000D0471
		public void ControllerChanged()
		{
			this.UpdateMenuBars();
		}

		// Token: 0x04001437 RID: 5175
		protected List<FrameworkDataElement> Data;

		// Token: 0x04001438 RID: 5176
		public FrameworkScroller HotkeyBar;

		// Token: 0x04001439 RID: 5177
		public FrameworkScroller SavesScroller;

		// Token: 0x0400143A RID: 5178
		public EmbarkBuilderModuleBackButton Back;

		// Token: 0x0400143B RID: 5179
		public NavigationContext GlobalNavContext = new NavigationContext();

		// Token: 0x0400143C RID: 5180
		public ScrollContext<NavigationContext> HorizontalNavigation = new ScrollContext<NavigationContext>();

		// Token: 0x0400143D RID: 5181
		private bool SelectFirst = true;

		// Token: 0x0400143E RID: 5182
		private TaskCompletionSource<bool> MenuClosed = new TaskCompletionSource<bool>();

		// Token: 0x0400143F RID: 5183
		public bool wasInScroller;
	}
}
