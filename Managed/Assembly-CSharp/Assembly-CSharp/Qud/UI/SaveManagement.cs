using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Qud.API;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.CharacterBuilds.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000458 RID: 1112
	[UIView("SaveManagement", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "SaveManagement", UICanvasHost = 1)]
	public class SaveManagement : SingletonWindowBase<SaveManagement>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x06002A15 RID: 10773 RVA: 0x000DD454 File Offset: 0x000DB654
		public void SetupContext()
		{
			this.navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.navigationContext.buttonHandlers.Set(InputButtonTypes.CancelButton, Event.Helpers.Handle(new Action(this.Exit)));
			this.midHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.midHorizNav.contexts.Clear();
			this.midHorizNav.contexts.Add(this.backButton.navigationContext);
			this.midHorizNav.contexts.Add(this.savesScroller.GetNavigationContext());
			this.midHorizNav.Setup();
			this.midHorizNav.parentContext = this.navigationContext;
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000DD504 File Offset: 0x000DB704
		public override void Show()
		{
			Task<List<SaveGameInfo>> savedGameInfo = SavesAPI.GetSavedGameInfo();
			Task.WaitAll(new Task[] { savedGameInfo });
			List<SaveGameInfo> result = savedGameInfo.Result;
			List<SaveInfoData> list = SaveManagement.SaveGameInfosToUIElements(result);
			this.saves = list;
			if (result.Count == 0)
			{
				this.completionSource.TrySetResult(null);
				this.Exit();
				return;
			}
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
			this.savesScroller.scrollContext.wraps = true;
			this.savesScroller.BeforeShow(null, list);
			foreach (SaveManagementRow saveManagementRow in this.savesScroller.selectionClones.Select<FrameworkUnityScrollChild, SaveManagementRow>((FrameworkUnityScrollChild s) => s.GetComponent<SaveManagementRow>()))
			{
				if (saveManagementRow != null)
				{
					saveManagementRow.deleteButton.context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
					{
						InputButtonTypes.AcceptButton,
						Event.Helpers.Handle(new Action(this.HandleDelete))
					} };
					saveManagementRow.context.context.commandHandlers = new Dictionary<string, Action> { 
					{
						"CmdDelete",
						Event.Helpers.Handle(new Action(this.HandleDelete))
					} };
				}
			}
			if (this.SelectFirst)
			{
				this.SelectFirst = false;
				this.savesScroller.scrollContext.selectedPosition = 0;
			}
			else if (this.savesScroller.scrollContext.selectedPosition >= result.Count)
			{
				this.savesScroller.scrollContext.selectedPosition = Math.Max(result.Count - 1, 0);
			}
			this.savesScroller.onSelected.RemoveAllListeners();
			this.savesScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.SelectedInfo));
			this.SetupContext();
			this.EnableNavContext();
			this.UpdateMenuBars();
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000DD74C File Offset: 0x000DB94C
		public override void Hide()
		{
			base.Hide();
			this.DisableNavContext(true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000DD767 File Offset: 0x000DB967
		public void EnableNavContext()
		{
			this.navigationContext.disabled = false;
			this.savesScroller.GetNavigationContext().ActivateAndEnable();
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000DD786 File Offset: 0x000DB986
		public void DisableNavContext(bool deactivate = true)
		{
			if (deactivate)
			{
				NavigationContext activeContext = NavigationController.instance.activeContext;
				if (activeContext != null && activeContext.IsInside(this.navigationContext))
				{
					NavigationController.instance.activeContext = null;
				}
			}
			this.navigationContext.disabled = true;
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000DD7C0 File Offset: 0x000DB9C0
		public void SelectedInfo(FrameworkDataElement data)
		{
			SaveInfoData saveInfoData = data as SaveInfoData;
			if (saveInfoData != null)
			{
				TaskCompletionSource<SaveGameInfo> taskCompletionSource = this.completionSource;
				if (taskCompletionSource == null)
				{
					return;
				}
				taskCompletionSource.TrySetResult(saveInfoData.SaveGame);
			}
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000DD7F0 File Offset: 0x000DB9F0
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
					SaveGameJSON json = info.json;
					if (json != null && json.SaveVersion < 395)
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

		// Token: 0x06002A1C RID: 10780 RVA: 0x000DD833 File Offset: 0x000DBA33
		public void Exit()
		{
			TaskCompletionSource<SaveGameInfo> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetResult(null);
			}
			ControlManager.ResetInput(false, true);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000DD850 File Offset: 0x000DBA50
		public async void HandleDelete()
		{
			if (NavigationController.instance.activeContext.IsInside(this.savesScroller.GetNavigationContext()))
			{
				SaveInfoData saveInfo = this.saves[this.savesScroller.selectedPosition];
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
					this.EnableNavContext();
					await Popup.NewPopupMessageAsync("Game Deleted!", PopupMessage.AcceptButton, null, null, null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
					this.saves = SaveManagement.SaveGameInfosToUIElements(await SavesAPI.GetSavedGameInfo());
					if (this.saves.Count == 0)
					{
						this.Exit();
					}
					else
					{
						this.Show();
					}
				}
				saveInfo = null;
			}
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000DD888 File Offset: 0x000DBA88
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
			list.Add(new MenuOption
			{
				KeyDescription = ControlManager.getCommandInputDescription("CmdDelete", true, false, ControlManager.InputDeviceType.Unknown, false),
				Description = "delete"
			});
			this.hotkeyBar.GetNavigationContext().disabled = true;
			this.hotkeyBar.BeforeShow(null, list);
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000DD92E File Offset: 0x000DBB2E
		public void Update()
		{
			if (this.navigationContext.IsActive(true))
			{
				NavigationContext activeContext = NavigationController.instance.activeContext;
				if ((activeContext != null && activeContext.IsInside(this.savesScroller.GetNavigationContext())) != this.wasInScroller)
				{
					this.UpdateMenuBars();
				}
			}
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000DD96D File Offset: 0x000DBB6D
		public void ControllerChanged()
		{
			this.UpdateMenuBars();
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000DD975 File Offset: 0x000DBB75
		public static List<SaveInfoData> SaveGameInfosToUIElements(List<SaveGameInfo> saveGameInfos)
		{
			return saveGameInfos.Select<SaveGameInfo, SaveInfoData>((SaveGameInfo info) => new SaveInfoData
			{
				SaveGame = info
			}).ToList<SaveInfoData>();
		}

		// Token: 0x04001605 RID: 5637
		protected List<SaveInfoData> saves;

		// Token: 0x04001606 RID: 5638
		public Image background;

		// Token: 0x04001607 RID: 5639
		public FrameworkScroller hotkeyBar;

		// Token: 0x04001608 RID: 5640
		public FrameworkScroller savesScroller;

		// Token: 0x04001609 RID: 5641
		public EmbarkBuilderModuleBackButton backButton;

		// Token: 0x0400160A RID: 5642
		public TaskCompletionSource<SaveGameInfo> completionSource;

		// Token: 0x0400160B RID: 5643
		public NavigationContext navigationContext = new NavigationContext();

		// Token: 0x0400160C RID: 5644
		public ScrollContext<NavigationContext> midHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x0400160D RID: 5645
		private bool SelectFirst = true;

		// Token: 0x0400160E RID: 5646
		public bool wasInScroller;
	}
}
