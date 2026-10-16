using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.CharacterBuilds;
using XRL.CharacterBuilds.UI;
using XRL.Core;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200043B RID: 1083
	[UIView("HighScoresScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "HighScoresScreen", UICanvasHost = 1)]
	public class HighScoresScreen : SingletonWindowBase<HighScoresScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x0600291C RID: 10524 RVA: 0x000D57F0 File Offset: 0x000D39F0
		public HighScoresScreen()
		{
			this.WrappedHandleDelete = global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.HandleDelete));
			this.WrappedHandleRevisit = global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.HandleRevisit));
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x000D588C File Offset: 0x000D3A8C
		public Scoreboard2 scoreboard
		{
			get
			{
				return Scores.Scoreboard;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000D5893 File Offset: 0x000D3A93
		public bool breakBackButton
		{
			get
			{
				return this.lastWidth <= this.breakpointBackButtonWidth;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x000D58A6 File Offset: 0x000D3AA6
		public bool breakRightFiller
		{
			get
			{
				return this.lastWidth <= this.breakpointRightFiller;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000D58B9 File Offset: 0x000D3AB9
		public bool isLocalMode
		{
			get
			{
				return this.currentMode == HighScoresScreen.Modes.Local;
			}
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000D58C4 File Offset: 0x000D3AC4
		public void SetupContext()
		{
			this.globalContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.globalContext.buttonHandlers.Set(InputButtonTypes.CancelButton, global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit)));
			this.globalContext.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			if (this.currentMode == HighScoresScreen.Modes.Daily || this.currentMode == HighScoresScreen.Modes.DailyFriends)
			{
				this.globalContext.axisHandlers.Set(InputAxisTypes.NavigationPageXAxis, global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.NextPage), new Action(this.PrevPage), null)));
			}
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Clear();
			this.vertNav.contexts.Add(this.midHorizNav);
			this.vertNav.contexts.Add(this.hotkeyBar.GetNavigationContext());
			this.vertNav.parentContext = this.globalContext;
			this.midHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.vertNav.axisHandlers.Set(InputAxisTypes.NavigationXAxis, this.midHorizNav.axisHandlers[InputAxisTypes.NavigationXAxis]);
			this.midHorizNav.contexts.Clear();
			if (!this.breakBackButton)
			{
				this.midHorizNav.contexts.Add(this.backButton.navigationContext);
			}
			this.midHorizNav.contexts.Add(this.leftSideScroller.GetNavigationContext());
			this.midHorizNav.contexts.Add(this.scoresScroller.GetNavigationContext());
			this.vertNav.Setup();
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000D5A5B File Offset: 0x000D3C5B
		public void NextPage()
		{
			this.HandleMenuOption(HighScoresScreen.NEXT_DAY);
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000D5A68 File Offset: 0x000D3C68
		public void PrevPage()
		{
			this.HandleMenuOption(HighScoresScreen.PREVIOUS_DAY);
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000D5A78 File Offset: 0x000D3C78
		public void PostSetupScore(FrameworkUnityScrollChild child, ScrollChildContext context, FrameworkDataElement element, int index)
		{
			HighScoresRow component = child.GetComponent<HighScoresRow>();
			if (component != null)
			{
				NavigationContext navigationContext = component.deleteButton.context;
				if (navigationContext.buttonHandlers == null)
				{
					navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
				}
				component.deleteButton.context.buttonHandlers.Clear();
				component.deleteButton.context.buttonHandlers.Add(InputButtonTypes.AcceptButton, this.WrappedHandleDelete);
				navigationContext = component.revisitCodaButton.context;
				if (navigationContext.buttonHandlers == null)
				{
					navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
				}
				component.revisitCodaButton.context.buttonHandlers.Clear();
				component.revisitCodaButton.context.buttonHandlers.Add(InputButtonTypes.AcceptButton, this.WrappedHandleRevisit);
				component.context.context.commandHandlers = new Dictionary<string, Action>
				{
					{ "U Negative", this.WrappedHandleDelete },
					{ "CmdDelete", this.WrappedHandleDelete },
					{ "CmdInsert", this.WrappedHandleRevisit }
				};
			}
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000D5B7C File Offset: 0x000D3D7C
		public void HandleMenuOption(FrameworkDataElement option)
		{
			MenuOption menuOption = option as MenuOption;
			if (menuOption != null)
			{
				if (menuOption == HighScoresScreen.BACK_BUTTON)
				{
					this.Exit();
				}
				if (menuOption == HighScoresScreen.ACHIEVEMENTS && this.currentMode != HighScoresScreen.Modes.Achievements)
				{
					this.currentMode = HighScoresScreen.Modes.Achievements;
					this.SelectFirst = true;
					this.Show();
				}
				if (menuOption == HighScoresScreen.LOCAL_SCORES && this.currentMode != HighScoresScreen.Modes.Local)
				{
					this.currentMode = HighScoresScreen.Modes.Local;
					this.SelectFirst = true;
					this.Show();
				}
				if (menuOption == HighScoresScreen.GLOBAL_DAILY && this.currentMode != HighScoresScreen.Modes.Daily)
				{
					this.currentMode = HighScoresScreen.Modes.Daily;
					this.SelectFirst = true;
					this.leaderboardID = LeaderboardManager.GetDailyID(this.daysAgo = 0);
					this.friendsOnly = false;
					this.Show();
				}
				if (menuOption == HighScoresScreen.FRIENDS_DAILY && this.currentMode != HighScoresScreen.Modes.DailyFriends)
				{
					this.currentMode = HighScoresScreen.Modes.DailyFriends;
					this.SelectFirst = true;
					this.leaderboardID = LeaderboardManager.GetDailyID(this.daysAgo = 0);
					this.friendsOnly = true;
					this.Show();
				}
				if (menuOption == HighScoresScreen.PREVIOUS_DAY)
				{
					int num = this.daysAgo + 1;
					this.daysAgo = num;
					this.leaderboardID = LeaderboardManager.GetDailyID(num);
					this.Show();
				}
				if (menuOption == HighScoresScreen.NEXT_DAY)
				{
					this.daysAgo = Math.Max(0, this.daysAgo - 1);
					this.leaderboardID = LeaderboardManager.GetDailyID(this.daysAgo);
					this.Show();
				}
			}
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000D5CCC File Offset: 0x000D3ECC
		public override void Show()
		{
			if (this.currentMode == HighScoresScreen.Modes.Achievements)
			{
				if (this.scoresScroller.selectionPrefab != this.AchivementRowPrefab)
				{
					this.scoresScroller.ChangePrefabs(this.AchivementRowPrefab, this.SpacerPrefab);
				}
				this.selection.Clear();
				this.achSelection.Clear();
				Dictionary<string, AchievementInfo> achievements = AchievementManager.State.Achievements;
				if (this.achSelection == null)
				{
					this.achSelection = new List<FrameworkDataElement>(achievements.Count);
				}
				this.achSelection.Clear();
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
						this.achSelection.Add(new AchievementInfoData(achievementInfo));
					}
				}
				if (num > 0)
				{
					this.achSelection.Add(new HiddenAchievementData(num));
				}
				this.titleText.SetText("{{W|ACHIEVEMENTS}}");
			}
			else if (this.currentMode == HighScoresScreen.Modes.Local)
			{
				if (this.scoresScroller.selectionPrefab != this.LocalHighScoresRowPrefab)
				{
					this.scoresScroller.ChangePrefabs(this.LocalHighScoresRowPrefab, this.SpacerPrefab);
				}
				this.selection.Clear();
				this.selection.AddRange(this.scoreboard.Scores.Select<ScoreEntry2, HighScoresDataElement>((ScoreEntry2 s) => new HighScoresDataElement
				{
					entry = s
				}));
				this.titleText.SetText("{{W|ENDED RUNS}}");
			}
			else
			{
				if (this.scoresScroller.selectionPrefab != this.SteamHighScoresRowPrefab)
				{
					this.scoresScroller.ChangePrefabs(this.SteamHighScoresRowPrefab, null);
				}
				this.selection.Clear();
				this.selection.Add(new HighScoresDataElement
				{
					message = "Fetching leaderboard"
				});
				this.cancelLast.Cancel();
				this.cancelLast = new CancellationTokenSource();
				this.titleText.SetText("{{W|" + LeaderboardManager.GetLeaderboardName(this.leaderboardID).ToUpper() + (this.friendsOnly ? " (friends only)" : "") + "}}");
				this.FetchLeaderboard(this.cancelLast.Token);
			}
			HighScoresScreen.leftSideMenuOptions.Clear();
			HighScoresScreen.leftSideMenuOptions.Add(HighScoresScreen.LOCAL_SCORES);
			if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console || !PlatformManager.AreLeaderboardsOffline())
			{
				HighScoresScreen.leftSideMenuOptions.Add(HighScoresScreen.GLOBAL_DAILY);
				HighScoresScreen.leftSideMenuOptions.Add(HighScoresScreen.FRIENDS_DAILY);
			}
			HighScoresScreen.leftSideMenuOptions.Add(HighScoresScreen.ACHIEVEMENTS);
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
			this.scores.Clear();
			this.scores.AddRange(this.selection);
			this.scoresScroller.scrollContext.wraps = true;
			this.scoresScroller.PostSetup.RemoveAllListeners();
			this.scoresScroller.PostSetup.AddListener(new UnityAction<FrameworkUnityScrollChild, ScrollChildContext, FrameworkDataElement, int>(this.PostSetupScore));
			this.leftSideScroller.BeforeShow(null, HighScoresScreen.leftSideMenuOptions);
			this.leftSideScroller.onSelected.RemoveAllListeners();
			this.leftSideScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
			this.scoresScroller.onSelected.RemoveAllListeners();
			this.scoresScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.SelectedInfo));
			this.SetupContext();
			this.UpdateMenuBars();
			if (this.SelectFirst)
			{
				this.scoresScroller.scrollContext.selectedPosition = 0;
			}
			FrameworkScroller frameworkScroller = this.scoresScroller;
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = null;
			IEnumerable<FrameworkDataElement> enumerable2;
			if (this.currentMode != HighScoresScreen.Modes.Achievements)
			{
				IEnumerable<FrameworkDataElement> enumerable = this.scores;
				enumerable2 = enumerable;
			}
			else
			{
				IEnumerable<FrameworkDataElement> enumerable = this.achSelection;
				enumerable2 = enumerable;
			}
			frameworkScroller.BeforeShow(embarkBuilderModuleWindowDescriptor, enumerable2);
			if (this.SelectFirst)
			{
				this.SelectFirst = false;
				this.scoresScroller.scrollContext.selectedPosition = 0;
			}
			this.scoresScroller.ScrollSelectedIntoView();
			Canvas.ForceUpdateCanvases();
			Canvas.ForceUpdateCanvases();
			this.EnableNavContext();
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000D61B8 File Offset: 0x000D43B8
		public async void FetchLeaderboard(CancellationToken ct)
		{
			try
			{
				await Task.Delay(100);
				if (!ct.IsCancellationRequested)
				{
					LeaderboardScores leaderboardScores = await LeaderboardManager.GetLeaderboardAsync(this.leaderboardID, 0, 9999, this.friendsOnly, new CancellationToken?(ct));
					if (ct.IsCancellationRequested || leaderboardScores == null || leaderboardScores.GetResult() == LeaderboardScores.Result.Cancelled)
					{
						this.scores.Clear();
						this.scores.Add(new HighScoresDataElement
						{
							message = "{{R|The game is currently running offline.}}"
						});
						this.scoresScroller.BeforeShow(null, this.scores);
					}
					else if (leaderboardScores.GetResult() == LeaderboardScores.Result.Failure)
					{
						Popup.ShowYesNo("An error has occurred, would you like to try again?", "Sounds/UI/ui_notification", true, DialogResult.Yes, delegate(DialogResult result)
						{
							switch (result)
							{
							case DialogResult.Yes:
							case DialogResult.OK:
								this.FetchLeaderboard(ct);
								return;
							}
							PlatformManager.SetOfflineMode(true);
						});
					}
					else
					{
						this.scores.Clear();
						for (int i = 0; i < leaderboardScores.GetEntryCount(); i++)
						{
							this.scores.Add(new HighScoresDataElement
							{
								leaderboardEntry = leaderboardScores.GetLeaderboardEntry(i)
							});
						}
						int num = Math.Max(0, this.scores.FindIndex((HighScoresDataElement s) => s.leaderboardEntry.isCurrentUser));
						if (this.scores.Count == 0)
						{
							this.scores.Add(new HighScoresDataElement
							{
								message = "Empty leaderboard"
							});
						}
						this.scoresScroller.BeforeShow(null, this.scores);
						Canvas.ForceUpdateCanvases();
						(this.scoresScroller as VisibleWindowScroller).ScrollIndexIntoView(num);
						this.scoresScroller.scrollContext.SelectIndex(num);
					}
				}
			}
			catch (Exception ex)
			{
				if (!ct.IsCancellationRequested)
				{
					this.scores.Clear();
					if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
					{
						this.scores.Add(new HighScoresDataElement
						{
							message = "{{R|Error: \n" + ex.StackTrace + "}}"
						});
					}
					else
					{
						this.scores.Add(new HighScoresDataElement
						{
							message = "{{R|An error has occurred.}}"
						});
						Popup.ShowYesNo("An error has occurred, would you like to try again?", "Sounds/UI/ui_notification", true, DialogResult.Yes, delegate(DialogResult result)
						{
							switch (result)
							{
							case DialogResult.Yes:
							case DialogResult.OK:
								this.FetchLeaderboard(ct);
								return;
							}
							PlatformManager.SetOfflineMode(true);
						});
					}
					this.scoresScroller.BeforeShow(null, this.scores);
					Canvas.ForceUpdateCanvases();
				}
			}
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000D61F7 File Offset: 0x000D43F7
		public override void Hide()
		{
			base.Hide();
			this.DisableNavContext(true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000D6214 File Offset: 0x000D4414
		public void EnableNavContext()
		{
			this.globalContext.disabled = false;
			List<HighScoresDataElement> list = this.scores;
			if (list != null && list.Count > 0)
			{
				this.scoresScroller.GetNavigationContext().Activate(false);
				return;
			}
			NavigationContext navigationContext = this.midHorizNav.contexts[0];
			if (navigationContext == null)
			{
				return;
			}
			navigationContext.ActivateAndEnable();
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000D6273 File Offset: 0x000D4473
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

		// Token: 0x0600292B RID: 10539 RVA: 0x000D62B0 File Offset: 0x000D44B0
		public async void SelectedInfo(FrameworkDataElement data)
		{
			HighScoresDataElement highScoresDataElement = data as HighScoresDataElement;
			if (highScoresDataElement != null && this.currentMode == HighScoresScreen.Modes.Local)
			{
				await Popup.ShowAsync(highScoresDataElement.entry.Details, true, true, true, true, false);
			}
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000D62F0 File Offset: 0x000D44F0
		public async Task<XRLGame> ShowScreen()
		{
			this.SelectFirst = true;
			TaskCompletionSource<XRLGame> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetCanceled();
			}
			this.completionSource = new TaskCompletionSource<XRLGame>();
			await The.UiContext;
			ControlManager.ResetInput(false, true);
			this.Show();
			XRLGame xrlgame = await this.completionSource.Task;
			this.DisableNavContext(true);
			this.Hide();
			return xrlgame;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000D6333 File Offset: 0x000D4533
		public void Exit()
		{
			TaskCompletionSource<XRLGame> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetResult(null);
			}
			ControlManager.ResetInput(false, true);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000D6350 File Offset: 0x000D4550
		public async void HandleRevisit()
		{
			if (NavigationController.instance.activeContext.IsInside(this.scoresScroller.GetNavigationContext()))
			{
				HighScoresScreen.<>c__DisplayClass61_0 CS$<>8__locals1 = new HighScoresScreen.<>c__DisplayClass61_0();
				CS$<>8__locals1.score = this.scores[this.scoresScroller.selectedPosition];
				try
				{
					CS$<>8__locals1.game = null;
					await NavigationController.instance.SuspendContextWhile(delegate
					{
						HighScoresScreen.<>c__DisplayClass61_0.<<HandleRevisit>b__0>d <<HandleRevisit>b__0>d;
						<<HandleRevisit>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<HandleRevisit>b__0>d.<>4__this = CS$<>8__locals1;
						<<HandleRevisit>b__0>d.<>1__state = -1;
						<<HandleRevisit>b__0>d.<>t__builder.Start<HighScoresScreen.<>c__DisplayClass61_0.<<HandleRevisit>b__0>d>(ref <<HandleRevisit>b__0>d);
						return <<HandleRevisit>b__0>d.<>t__builder.Task;
					});
					this.completionSource.TrySetResult(CS$<>8__locals1.game);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("HandleRevisit coda", ex, "game_exception");
				}
				CS$<>8__locals1 = null;
			}
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000D6388 File Offset: 0x000D4588
		public async void HandleDelete()
		{
			if (NavigationController.instance.activeContext.IsInside(this.scoresScroller.GetNavigationContext()))
			{
				HighScoresDataElement score = this.scores[this.scoresScroller.selectedPosition];
				TaskAwaiter<DialogResult> taskAwaiter = Popup.ShowYesNoAsync("Are you sure you want to delete this?\n\n" + score.entry.Details.Split(new char[] { '\n' }, 2)[0]).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<DialogResult> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<DialogResult>);
				}
				if (taskAwaiter.GetResult() == DialogResult.Yes)
				{
					try
					{
						HighScoresDataElement highScoresDataElement = score;
						if (highScoresDataElement != null)
						{
							ScoreEntry2 entry = highScoresDataElement.entry;
							if (entry != null)
							{
								entry.DeleteCoda();
							}
						}
					}
					catch (Exception ex)
					{
						MetricsManager.LogException("HandleDelete coda save", ex, "game_exception");
					}
					this.scoreboard.Scores.Remove(score.entry);
					this.scoreboard.Save();
					this.Show();
				}
				score = null;
			}
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000D63C0 File Offset: 0x000D45C0
		public void UpdateMenuBars()
		{
			List<MenuOption> list = new List<MenuOption>();
			if (this.breakBackButton)
			{
				list.Add(HighScoresScreen.BACK_BUTTON);
			}
			list.Add(new MenuOption
			{
				InputCommand = "NavigationXYAxis",
				Description = "navigate",
				disabled = true
			});
			if (this.currentMode == HighScoresScreen.Modes.Local)
			{
				list.Add(new MenuOption
				{
					KeyDescription = ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false),
					Description = "select",
					disabled = true
				});
			}
			if (this.currentMode == HighScoresScreen.Modes.Daily || this.currentMode == HighScoresScreen.Modes.DailyFriends)
			{
				list.Add(HighScoresScreen.PREVIOUS_DAY);
				if (this.daysAgo > 0)
				{
					list.Add(HighScoresScreen.NEXT_DAY);
				}
			}
			this.hotkeyBar.GetNavigationContext().disabled = false;
			this.hotkeyBar.BeforeShow(null, list);
			Canvas.ForceUpdateCanvases();
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000D649C File Offset: 0x000D469C
		public void Update()
		{
			if (this.globalContext.IsActive(true))
			{
				NavigationContext activeContext = NavigationController.instance.activeContext;
				bool flag = activeContext != null && activeContext.IsInside(this.scoresScroller.GetNavigationContext());
				float width = base.rectTransform.rect.width;
				if (flag != this.wasInScroller || this.lastWidth != width)
				{
					this.lastWidth = width;
					this.backButton.gameObject.SetActive(!this.breakBackButton);
					this.rightFiller.gameObject.SetActive(!this.breakRightFiller);
					this.safeArea.offsetMin = new Vector2((float)(this.breakBackButton ? 10 : 150), this.safeArea.offsetMin.y);
					this.safeArea.offsetMax = new Vector2((float)(this.breakBackButton ? (-10) : (-150)), this.safeArea.offsetMax.y);
					this.UpdateMenuBars();
				}
			}
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000D65A6 File Offset: 0x000D47A6
		public void ControllerChanged()
		{
			this.UpdateMenuBars();
		}

		// Token: 0x040014D1 RID: 5329
		public RectTransform safeArea;

		// Token: 0x040014D2 RID: 5330
		public RectTransform rightFiller;

		// Token: 0x040014D3 RID: 5331
		public Image background;

		// Token: 0x040014D4 RID: 5332
		public FrameworkScroller hotkeyBar;

		// Token: 0x040014D5 RID: 5333
		public FrameworkScroller scoresScroller;

		// Token: 0x040014D6 RID: 5334
		public FrameworkScroller leftSideScroller;

		// Token: 0x040014D7 RID: 5335
		public UITextSkin titleText;

		// Token: 0x040014D8 RID: 5336
		public EmbarkBuilderModuleBackButton backButton;

		// Token: 0x040014D9 RID: 5337
		public TaskCompletionSource<XRLGame> completionSource;

		// Token: 0x040014DA RID: 5338
		public NavigationContext globalContext = new NavigationContext();

		// Token: 0x040014DB RID: 5339
		public ScrollContext<NavigationContext> midHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x040014DC RID: 5340
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x040014DD RID: 5341
		public List<HighScoresDataElement> scores = new List<HighScoresDataElement>();

		// Token: 0x040014DE RID: 5342
		public FrameworkUnityScrollChild LocalHighScoresRowPrefab;

		// Token: 0x040014DF RID: 5343
		public FrameworkUnityScrollChild SteamHighScoresRowPrefab;

		// Token: 0x040014E0 RID: 5344
		public FrameworkUnityScrollChild AchivementRowPrefab;

		// Token: 0x040014E1 RID: 5345
		public GameObject SpacerPrefab;

		// Token: 0x040014E2 RID: 5346
		public float lastWidth;

		// Token: 0x040014E3 RID: 5347
		public float breakpointBackButtonWidth;

		// Token: 0x040014E4 RID: 5348
		public float breakpointRightFiller;

		// Token: 0x040014E5 RID: 5349
		public HighScoresScreen.Modes currentMode = HighScoresScreen.Modes.Local;

		// Token: 0x040014E6 RID: 5350
		private bool SelectFirst = true;

		// Token: 0x040014E7 RID: 5351
		public readonly Action WrappedHandleDelete;

		// Token: 0x040014E8 RID: 5352
		public readonly Action WrappedHandleRevisit;

		// Token: 0x040014E9 RID: 5353
		public static readonly MenuOption ACHIEVEMENTS = new MenuOption
		{
			Id = "ACH",
			InputCommand = null,
			Description = "Achievements"
		};

		// Token: 0x040014EA RID: 5354
		public static readonly MenuOption LOCAL_SCORES = new MenuOption
		{
			Id = "LOCAL_SCORES",
			InputCommand = null,
			Description = "Ended Runs"
		};

		// Token: 0x040014EB RID: 5355
		public static readonly MenuOption GLOBAL_DAILY = new MenuOption
		{
			Id = "GLOBAL_DAILY",
			InputCommand = null,
			Description = "Daily (global)"
		};

		// Token: 0x040014EC RID: 5356
		public static readonly MenuOption FRIENDS_DAILY = new MenuOption
		{
			Id = "FRIENDS_DAILY",
			InputCommand = null,
			Description = "Daily (friends)"
		};

		// Token: 0x040014ED RID: 5357
		public static List<MenuOption> leftSideMenuOptions = new List<MenuOption>
		{
			HighScoresScreen.LOCAL_SCORES,
			HighScoresScreen.GLOBAL_DAILY,
			HighScoresScreen.FRIENDS_DAILY,
			HighScoresScreen.ACHIEVEMENTS
		};

		// Token: 0x040014EE RID: 5358
		public static readonly MenuOption PREVIOUS_DAY = new MenuOption
		{
			Id = "PREVIOUS_DAY",
			InputCommand = "Page Left",
			Description = "Previous Day"
		};

		// Token: 0x040014EF RID: 5359
		public static readonly MenuOption NEXT_DAY = new MenuOption
		{
			Id = "NEXT_DAY",
			InputCommand = "Page Right",
			Description = "Next Day"
		};

		// Token: 0x040014F0 RID: 5360
		public static readonly MenuOption BACK_BUTTON = KeybindsScreen.BACK_BUTTON;

		// Token: 0x040014F1 RID: 5361
		public int daysAgo;

		// Token: 0x040014F2 RID: 5362
		public string leaderboardID;

		// Token: 0x040014F3 RID: 5363
		public bool friendsOnly;

		// Token: 0x040014F4 RID: 5364
		private List<HighScoresDataElement> selection = new List<HighScoresDataElement>();

		// Token: 0x040014F5 RID: 5365
		private List<FrameworkDataElement> achSelection = new List<FrameworkDataElement>();

		// Token: 0x040014F6 RID: 5366
		private CancellationTokenSource cancelLast = new CancellationTokenSource();

		// Token: 0x040014F7 RID: 5367
		public bool wasInScroller;

		// Token: 0x02001822 RID: 6178
		public enum Modes
		{
			// Token: 0x04006132 RID: 24882
			Achievements,
			// Token: 0x04006133 RID: 24883
			Local,
			// Token: 0x04006134 RID: 24884
			Daily,
			// Token: 0x04006135 RID: 24885
			DailyFriends
		}
	}
}
