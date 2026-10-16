using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using LaundryBear.PlatformServices;
using Qud.API;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000442 RID: 1090
	[UIView("MainMenu", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "MainMenu", UICanvasHost = 1)]
	public class MainMenu : SingletonWindowBase<MainMenu>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x06002969 RID: 10601 RVA: 0x000D88B4 File Offset: 0x000D6AB4
		public void SetupContext()
		{
			this.globalContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
			{
				this.globalContext.buttonHandlers.Set(InputButtonTypes.CancelButton, global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Quit)));
			}
			this.leftScroller.GetNavigationContext().axisHandlers[InputAxisTypes.NavigationXAxis] = delegate
			{
				this.rightScroller.GetNavigationContext().Activate(false);
			};
			this.rightScroller.GetNavigationContext().axisHandlers[InputAxisTypes.NavigationXAxis] = delegate
			{
				this.leftScroller.GetNavigationContext().Activate(false);
			};
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000D893E File Offset: 0x000D6B3E
		public IEnumerator DoIntroTween(GameObject activeBackground)
		{
			float introTime = 0f;
			LeanTweenType EASE_TYPE = LeanTweenType.easeOutCubic;
			if (activeBackground != null)
			{
				activeBackground.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
				LeanTween.scale(activeBackground, new Vector3(1.01f, 1.01f, 1f), 6f).setEase(EASE_TYPE);
			}
			this.leftFader.alpha = 0f;
			this.logoFader.alpha = 0f;
			this.centerFlyoutGroup.padding.top = -290;
			this.centerTransform.localScale = new Vector3(0f, 1f, 1f);
			while (introTime < 1f)
			{
				introTime += Time.deltaTime;
				yield return null;
			}
			SoundManager.PlayUISound("Sounds/UI/ui_mainMenu_popUp", 1f, false, true, SoundRequest.SoundEffectType.None);
			LeanTween.alpha(this.logoFader.gameObject, 1f, 2f).setEase(EASE_TYPE);
			LeanTween.alpha(this.leftFader.gameObject, 1f, 2f).setEase(EASE_TYPE);
			float flywideStart = 0f;
			while (this.centerTransform.localScale.x < 1f)
			{
				flywideStart += Time.deltaTime;
				this.centerTransform.localScale = new Vector3(Mathf.Lerp(0f, 1f, flywideStart * 5f), 1f, 1f);
				LayoutRebuilder.MarkLayoutForRebuild(this.centerTransform);
				yield return null;
			}
			float flyoutStart = 0f;
			while (this.centerFlyoutGroup.padding.top < 0)
			{
				flyoutStart += Time.deltaTime;
				this.centerFlyoutGroup.padding.top = (int)Mathf.Lerp(-290f, 0f, Easing.SineEaseIn(flyoutStart));
				LayoutRebuilder.MarkLayoutForRebuild(this.centerTransform);
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000D8954 File Offset: 0x000D6B54
		public override void Show()
		{
			TutorialManager.EndTutorial();
			this.versionText.SetText(string.Format("{0}{1}\n{{{{K|build {2}}}}}", XRLGame.MarketingVersion, XRLGame.MarketingPostfix, XRLGame.CoreVersion));
			base.Show();
			string option = Options.GetOption("OptionMainMenuBackground", "");
			GameObject gameObject = null;
			this.backgrounds[0].SetActive(option == "Modern");
			this.backgrounds[1].SetActive(option == "Classic");
			if (option == "Modern")
			{
				gameObject = this.backgrounds[0];
			}
			if (option == "Classic")
			{
				gameObject = this.backgrounds[1];
			}
			if (this.FirstShow)
			{
				this.FirstShow = false;
				base.StartCoroutine(this.DoIntroTween(gameObject));
			}
			else
			{
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				this.leftFader.alpha = 1f;
				this.logoFader.alpha = 1f;
				this.centerFlyoutGroup.padding.top = 0;
				this.centerTransform.localScale = new Vector3(1f, 1f, 1f);
			}
			MainMenu.LeftOptions[1].Enabled = SavesAPI.HasSavedGameInfo();
			Platform currentPlatform = Utilities.GetCurrentPlatform();
			for (int i = MainMenu.LeftOptions.Count - 1; i > -1; i--)
			{
				if ((MainMenu.LeftOptions[i].AllowedPlatforms & currentPlatform) == Platform.None)
				{
					MainMenu.LeftOptions.RemoveAt(i);
				}
			}
			this.leftScroller.scrollContext.wraps = true;
			this.leftScroller.BeforeShow(null, MainMenu.LeftOptions);
			this.leftScroller.onSelected.RemoveAllListeners();
			this.leftScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.SelectedInfo));
			this.leftScroller.scrollContext.parentContext = this.globalContext;
			for (int j = MainMenu.RightOptions.Count - 1; j > -1; j--)
			{
				if ((MainMenu.RightOptions[j].AllowedPlatforms & currentPlatform) == Platform.None)
				{
					MainMenu.RightOptions.RemoveAt(j);
				}
			}
			this.rightScroller.scrollContext.wraps = true;
			this.rightScroller.BeforeShow(null, MainMenu.RightOptions);
			this.rightScroller.onSelected.RemoveAllListeners();
			this.rightScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.SelectedInfo));
			this.rightScroller.scrollContext.parentContext = this.globalContext;
			if (this.SelectFirst)
			{
				this.SelectFirst = false;
				this.leftScroller.scrollContext.selectedPosition = 0;
				this.leftScroller.scrollContext.Activate(false);
			}
			this.SetupContext();
			this.EnableNavContext();
			this.UpdateMenuBars();
			TutorialManager.EndTutorial();
			if (File.Exists(DataManager.DLCPath("OSTEntitlement.txt")) && File.Exists(DataManager.DLCPath("PetPack1Entitlement.txt")))
			{
				MainMenu.HasDromadDeluxeEntitlement = true;
			}
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000D8C54 File Offset: 0x000D6E54
		public async void Reshow()
		{
			await The.UiContext;
			this.EnableNavContext();
			this.leftScroller.scrollContext.ActivateAndEnable();
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000D8C8B File Offset: 0x000D6E8B
		public override void Hide()
		{
			base.Hide();
			this.DisableNavContext(true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000D8CA6 File Offset: 0x000D6EA6
		public void EnableNavContext()
		{
			this.globalContext.disabled = false;
			this.leftScroller.GetNavigationContext().ActivateAndEnable();
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000D8CC8 File Offset: 0x000D6EC8
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
			this.leftScroller.GetNavigationContext().disabled = true;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000D8D20 File Offset: 0x000D6F20
		public async void SelectedInfo(FrameworkDataElement data)
		{
			MainMenuOptionData mainMenuOptionData = data as MainMenuOptionData;
			if (mainMenuOptionData != null)
			{
				if (mainMenuOptionData.Command == "Pick:Redeem Code")
				{
					try
					{
						string text = await Popup.AskStringAsync("Redeem a Code", "", 32, 0, null, false, true, null, false, null);
						if (!string.IsNullOrEmpty(text))
						{
							CodeRedemptionManager.redeemNoProgress(text);
						}
						return;
					}
					catch (Exception ex)
					{
						MetricsManager.LogException("Code Redemption", ex, "game_exception");
						return;
					}
				}
				if (mainMenuOptionData.Command == "Pick:Installed Mod Configuration")
				{
					try
					{
						await NavigationController.instance.SuspendContextWhile<bool>(new Func<Task<bool>>(SingletonWindowBase<ModManagerUI>.instance.ShowMenuAsync));
						return;
					}
					finally
					{
						UIManager.showWindow("MainMenu", false);
						this.Reshow();
					}
				}
				if (mainMenuOptionData.Command == "Pick:Modding Utilities")
				{
					try
					{
						await NavigationController.instance.SuspendContextWhile<bool>(new Func<Task<bool>>(SingletonWindowBase<ModToolkit>.instance.ShowMenuAsync));
						return;
					}
					finally
					{
						UIManager.showWindow("MainMenu", false);
					}
				}
				if (mainMenuOptionData.Command == "Pick:Achievements")
				{
					try
					{
						await NavigationController.instance.SuspendContextWhile<bool>(new Func<Task<bool>>(SingletonWindowBase<AchievementView>.instance.ShowMenuAsync));
						return;
					}
					finally
					{
						UIManager.showWindow("MainMenu", false);
					}
				}
				Keyboard.PushMouseEvent(mainMenuOptionData.Command, null);
			}
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000D8D60 File Offset: 0x000D6F60
		public async Task<XRLGame> ContinueMenu()
		{
			this.SelectFirst = true;
			XRLGame xrlgame;
			for (;;)
			{
				TaskCompletionSource<SaveGameInfo> taskCompletionSource = this.completionSource;
				if (taskCompletionSource != null)
				{
					taskCompletionSource.TrySetCanceled();
				}
				this.completionSource = new TaskCompletionSource<SaveGameInfo>();
				await The.UiContext;
				ControlManager.ResetInput(false, true);
				this.SelectFirst = true;
				this.Show();
				SaveGameInfo info = await this.completionSource.Task;
				this.DisableNavContext(true);
				await The.UiContext;
				if (info == null)
				{
					break;
				}
				try
				{
					if (info.json.SaveVersion < 395)
					{
						await Popup.ShowAsync("That save file looks like it's from an older save format revision (" + info.json.GameVersion + "). Sorry!\n\nYou can probably change to a previous branch in your game client and get it to load if you want to finish it off.", true, true, true, true, false);
						continue;
					}
					TaskAwaiter<bool> taskAwaiter = info.TryRestoreModsAndLoadAsync().GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (!taskAwaiter.GetResult())
					{
						continue;
					}
					this.Hide();
					xrlgame = The.Game;
				}
				catch (Exception)
				{
					continue;
				}
				return xrlgame;
			}
			this.Hide();
			xrlgame = null;
			return xrlgame;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000D8DA4 File Offset: 0x000D6FA4
		public async void Quit()
		{
			TaskAwaiter<DialogResult> taskAwaiter = Popup.ShowYesNoAsync("Are you sure you want to quit?").GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<DialogResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<DialogResult>);
			}
			if (taskAwaiter.GetResult() == DialogResult.Yes)
			{
				GameManager.Instance.Quit();
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000D8DD3 File Offset: 0x000D6FD3
		public void Exit()
		{
			TaskCompletionSource<SaveGameInfo> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetResult(null);
			}
			ControlManager.ResetInput(false, true);
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000D8DF0 File Offset: 0x000D6FF0
		public async void HandleDelete()
		{
			if (NavigationController.instance.activeContext.IsInside(this.leftScroller.GetNavigationContext()))
			{
				SaveInfoData saveInfo = this.saves[this.leftScroller.selectedPosition];
				List<QudMenuItem> list;
				if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
				{
					list = PopupMessage.AcceptCancelButton;
				}
				else
				{
					list = PopupMessage.AcceptCancelButtonWithoutHotkey;
				}
				string text = "{{R|Delete " + saveInfo.SaveGame.Name + "}}";
				TaskAwaiter<QudMenuItem> taskAwaiter = Popup.NewPopupMessageAsync("Are you sure you want to delete the save game for " + saveInfo.SaveGame.Name + "?", list, null, text, null, 1, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<QudMenuItem> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<QudMenuItem>);
				}
				if (taskAwaiter.GetResult().command != "Cancel")
				{
					this.DisableNavContext(true);
					saveInfo.SaveGame.Delete();
					this.Show();
					await Popup.NewPopupMessageAsync("Game Deleted!", PopupMessage.AcceptButton, null, null, null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
				}
				saveInfo = null;
			}
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000D8E28 File Offset: 0x000D7028
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
				KeyDescription = ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false),
				Description = "select"
			});
			if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
			{
				list.Add(new MenuOption
				{
					KeyDescription = ControlManager.getCommandInputDescription("Cancel", true, false, ControlManager.InputDeviceType.Unknown, false),
					Description = "quit"
				});
			}
			this.hotkeyBar.GetNavigationContext().disabled = true;
			this.hotkeyBar.BeforeShow(null, list);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000D8ED8 File Offset: 0x000D70D8
		public void Update()
		{
			if (MainMenu.HasDromadDeluxeEntitlement)
			{
				RectTransform rectTransform = this.dromadDeluxeBadge;
				if ((rectTransform == null || !rectTransform.gameObject.activeSelf) && Options.GetOption("OptionShowCollectionBadges", "") == "Yes")
				{
					this.dromadDeluxeBadge.gameObject.SetActive(true);
					this.dromadDeluxeBadge.localScale = new Vector3(1f, 1f, 1f);
				}
			}
			RectTransform rectTransform2 = this.dromadDeluxeBadge;
			if ((rectTransform2 == null || !rectTransform2.gameObject.activeSelf) && (!MainMenu.HasDromadDeluxeEntitlement || Options.GetOption("OptionShowCollectionBadges", "") != "Yes"))
			{
				this.dromadDeluxeBadge.gameObject.SetActive(false);
			}
			if (!Options.ModernUI)
			{
				this.Hide();
				return;
			}
			NavigationContext activeContext = NavigationController.instance.activeContext;
			bool flag;
			if (activeContext == null || !activeContext.IsInside(this.leftScroller.GetNavigationContext()))
			{
				NavigationContext activeContext2 = NavigationController.instance.activeContext;
				flag = activeContext2 != null && activeContext2.IsInside(this.rightScroller.GetNavigationContext());
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			if (flag2 != this.wasInScroller)
			{
				this.UpdateMenuBars();
				this.wasInScroller = flag2;
			}
			if (flag2)
			{
				MainMenu.LeftOptions.ForEach(delegate(MainMenuOptionData o)
				{
					if (ControlManager.isHotkeyDown(o.Shortcut))
					{
						this.SelectedInfo(o);
					}
				});
				MainMenu.RightOptions.ForEach(delegate(MainMenuOptionData o)
				{
					if (ControlManager.isHotkeyDown(o.Shortcut))
					{
						this.SelectedInfo(o);
					}
				});
			}
			if (this.steamInputWarning && PlatformManager.Steam.Initialized && ControlManager.SuspectSteamInput && !this.steamInputWarning.activeInHierarchy)
			{
				this.steamInputWarning.SetActive(true);
			}
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000D9070 File Offset: 0x000D7270
		public void ControllerChanged()
		{
			this.UpdateMenuBars();
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000D9078 File Offset: 0x000D7278
		private void LateUpdate()
		{
			if (MainMenu.EnsureReselect)
			{
				MainMenu.EnsureReselect = false;
				this.EnableNavContext();
				this.leftScroller.scrollContext.ActivateAndEnable();
			}
		}

		// Token: 0x04001542 RID: 5442
		protected List<SaveInfoData> saves;

		// Token: 0x04001543 RID: 5443
		public Image background;

		// Token: 0x04001544 RID: 5444
		public CanvasGroup logoFader;

		// Token: 0x04001545 RID: 5445
		public CanvasGroup leftFader;

		// Token: 0x04001546 RID: 5446
		public RectTransform centerTransform;

		// Token: 0x04001547 RID: 5447
		public VerticalLayoutGroup centerFlyoutGroup;

		// Token: 0x04001548 RID: 5448
		public FrameworkScroller hotkeyBar;

		// Token: 0x04001549 RID: 5449
		public FrameworkScroller leftScroller;

		// Token: 0x0400154A RID: 5450
		public FrameworkScroller rightScroller;

		// Token: 0x0400154B RID: 5451
		public TaskCompletionSource<SaveGameInfo> completionSource;

		// Token: 0x0400154C RID: 5452
		public NavigationContext globalContext = new NavigationContext();

		// Token: 0x0400154D RID: 5453
		public ScrollContext<NavigationContext> midHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x0400154E RID: 5454
		public UITextSkin versionText;

		// Token: 0x0400154F RID: 5455
		public GameObject[] backgrounds;

		// Token: 0x04001550 RID: 5456
		public static List<MainMenuOptionData> LeftOptions = new List<MainMenuOptionData>
		{
			new MainMenuOptionData
			{
				Text = "New Game",
				Command = "Pick:New Game",
				Shortcut = global::UnityEngine.KeyCode.N
			},
			new MainMenuOptionData
			{
				Text = "Continue",
				Command = "Pick:Continue",
				Shortcut = global::UnityEngine.KeyCode.C
			},
			new MainMenuOptionData
			{
				Text = "Records",
				Command = "Pick:High Scores",
				Shortcut = global::UnityEngine.KeyCode.H
			},
			new MainMenuOptionData
			{
				Text = "Options",
				Command = "Pick:Options",
				Shortcut = global::UnityEngine.KeyCode.O
			},
			new MainMenuOptionData
			{
				Text = "Mods",
				Command = "Pick:Installed Mod Configuration",
				Alert = MainMenuOptionData.AlertMode.ModStatus,
				AllowedPlatforms = Platform.Desktop
			}
		};

		// Token: 0x04001551 RID: 5457
		public static List<MainMenuOptionData> RightOptions = new List<MainMenuOptionData>
		{
			new MainMenuOptionData
			{
				Text = "Redeem Code",
				Command = "Pick:Redeem Code",
				AllowedPlatforms = Platform.Desktop
			},
			new MainMenuOptionData
			{
				Text = "Modding Toolkit",
				Command = "Pick:Modding Utilities",
				Shortcut = global::UnityEngine.KeyCode.M,
				AllowedPlatforms = Platform.Desktop
			},
			new MainMenuOptionData
			{
				Text = "Credits",
				Command = "Pick:Credits"
			},
			new MainMenuOptionData
			{
				Text = "Help",
				Command = "Pick:Help",
				Shortcut = global::UnityEngine.KeyCode.F1
			}
		};

		// Token: 0x04001552 RID: 5458
		public static bool EnsureReselect = false;

		// Token: 0x04001553 RID: 5459
		private bool SelectFirst = true;

		// Token: 0x04001554 RID: 5460
		public bool FirstShow = true;

		// Token: 0x04001555 RID: 5461
		public GameObject steamInputWarning;

		// Token: 0x04001556 RID: 5462
		public static bool HasDromadDeluxeEntitlement = false;

		// Token: 0x04001557 RID: 5463
		public RectTransform dromadDeluxeBadge;

		// Token: 0x04001558 RID: 5464
		public bool wasInScroller;
	}
}
