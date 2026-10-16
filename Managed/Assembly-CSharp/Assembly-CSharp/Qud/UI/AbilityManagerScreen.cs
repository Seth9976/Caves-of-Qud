using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Parts;

namespace Qud.UI
{
	// Token: 0x0200042A RID: 1066
	[UIView("AbilityManagerScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "AbilityManagerScreen", UICanvasHost = 1)]
	public class AbilityManagerScreen : SingletonWindowBase<AbilityManagerScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x06002856 RID: 10326 RVA: 0x000D0E58 File Offset: 0x000CF058
		public void SetupContext()
		{
			this.vertNav.parentContext = this.navigationContext;
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Add(this.leftSideScroller.scrollContext);
			AbilityManagerScreen.Context context = this.navigationContext;
			if (context.buttonHandlers == null)
			{
				context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.CancelButton,
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Cancel))
				} };
			}
			context = this.navigationContext;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>
				{
					{
						AbilityManagerScreen.TOGGLE_SORT.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(AbilityManagerScreen.TOGGLE_SORT);
						})
					},
					{
						AbilityManagerScreen.FILTER_ITEMS.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(AbilityManagerScreen.FILTER_ITEMS);
						})
					}
				};
			}
			AbilityManagerScreen.TOGGLE_SORT.Description = AbilityManagerScreen.sortModeDescription;
			this.vertNav.wraps = true;
			this.vertNav.Setup();
			this.leftSideScroller.scrollContext.parentContext = this.vertNav;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000D0F68 File Offset: 0x000CF168
		public static string sortModeDescription
		{
			get
			{
				return "sort: " + Markup.Color((SingletonWindowBase<AbilityManagerScreen>.instance.sortMode == AbilityManagerScreen.SortMode.Custom) ? "w" : "y", "custom") + "/" + Markup.Color((SingletonWindowBase<AbilityManagerScreen>.instance.sortMode == AbilityManagerScreen.SortMode.Class) ? "w" : "y", "by class");
			}
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000D0FCC File Offset: 0x000CF1CC
		protected async Task<AbilityManagerScreen.Result> showScreen(global::XRL.World.GameObject GO)
		{
			this.skipFrame = 2;
			The.Player.IsRealityDistortionUsable(null, null);
			AbilityManagerLine.dragging = false;
			this.menucomplete.TrySetCanceled();
			this.menucomplete = new TaskCompletionSource<AbilityManagerScreen.Result>();
			this.activatedAbilities = GO.ActivatedAbilities;
			AbilityManagerScreen.Result result;
			if (this.activatedAbilities == null)
			{
				MetricsManager.LogWarning("Opening ability manager with no activated abilities part");
				this.Hide();
				await Popup.ShowAsync("You have no activated abilities.", true, true, true, true, false);
				result = default(AbilityManagerScreen.Result);
			}
			else
			{
				GameManager.Instance.PushGameView("AbilityManagerScreen", true);
				try
				{
					MinEvent.UIHold = true;
					await The.UiContext;
					this.searchText = "";
					this.BeforeShow();
					if (this.leftSideItems.Count == 0)
					{
						this.Hide();
						await Popup.ShowAsync("You have no activated abilities.", true, true, true, true, false);
						result = default(AbilityManagerScreen.Result);
					}
					else
					{
						this.Show();
						this.leftSideScroller.scrollContext.ActivateAndEnable();
						result = await this.menucomplete.Task;
					}
				}
				finally
				{
					GameManager.Instance.PopGameView(true);
					MinEvent.UIHold = false;
				}
			}
			return result;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000D1018 File Offset: 0x000CF218
		public static Task<AbilityManagerScreen.Result> OpenAbilityManager(global::XRL.World.GameObject GO)
		{
			return NavigationController.instance.SuspendContextWhile<AbilityManagerScreen.Result>(() => SingletonWindowBase<AbilityManagerScreen>.instance.showScreen(GO));
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000D1048 File Offset: 0x000CF248
		public bool isCollapsed(string key)
		{
			bool flag;
			return this.classCollapsed.TryGetValue(key, out flag) && flag;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000D1068 File Offset: 0x000CF268
		public void BeforeShow()
		{
			AbilityManagerScreen.<>c__DisplayClass22_0 CS$<>8__locals1 = new AbilityManagerScreen.<>c__DisplayClass22_0();
			CS$<>8__locals1.<>4__this = this;
			AbilityManagerLine.dragging = false;
			this.SetupContext();
			CS$<>8__locals1.hotkeySpread = ControlManager.GetHotkeySpread(new List<string> { "Menus", "UINav" });
			this.RealityWeak = The.Player.IsRealityDistortionUsable(null, null);
			if (this.sortMode == AbilityManagerScreen.SortMode.Class)
			{
				this.leftSideItems.Clear();
				AbilityManager.PlayerAbilityLock.EnterReadLock();
				try
				{
					using (Dictionary<string, List<ActivatedAbilityEntry>>.Enumerator enumerator = AbilityManager.PlayerAbilitiesByClass.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, List<ActivatedAbilityEntry>> keyValuePair = enumerator.Current;
							AbilityManagerLineData abilityManagerLineData = new AbilityManagerLineData
							{
								Id = "Category " + keyValuePair.Key,
								category = keyValuePair.Key,
								collapsed = this.isCollapsed(keyValuePair.Key)
							};
							this.leftSideItems.Add(abilityManagerLineData);
							if (!abilityManagerLineData.collapsed)
							{
								foreach (ActivatedAbilityEntry activatedAbilityEntry in keyValuePair.Value)
								{
									using (IEnumerator<string> enumerator3 = CommandBindingManager.GetCommandBindings(activatedAbilityEntry.Command).GetEnumerator())
									{
										string text = null;
										if (enumerator3.MoveNext())
										{
											text = enumerator3.Current;
										}
										this.leftSideItems.Add(new AbilityManagerLineData
										{
											Id = activatedAbilityEntry.ID.ToString(),
											ability = activatedAbilityEntry,
											searchText = activatedAbilityEntry.DisplayName + " " + activatedAbilityEntry.Description,
											realityIsWeak = this.RealityWeak,
											hotkeyDescription = text
										});
									}
								}
							}
						}
						goto IL_01FC;
					}
				}
				finally
				{
					AbilityManager.PlayerAbilityLock.ExitReadLock();
				}
			}
			this.leftSideItems = this.activatedAbilities.GetAbilityListOrderedByPreference().Select<ActivatedAbilityEntry, AbilityManagerLineData>(delegate(ActivatedAbilityEntry abil, int i)
			{
				AbilityManagerLineData abilityManagerLineData2;
				using (IEnumerator<string> enumerator5 = CommandBindingManager.GetCommandBindings(abil.Command).GetEnumerator())
				{
					string text2 = null;
					if (enumerator5.MoveNext())
					{
						text2 = enumerator5.Current;
					}
					abilityManagerLineData2 = new AbilityManagerLineData
					{
						Id = abil.ID.ToString(),
						ability = abil,
						searchText = abil.DisplayName + " " + abil.Description,
						realityIsWeak = CS$<>8__locals1.<>4__this.RealityWeak,
						hotkeyDescription = text2
					};
				}
				return abilityManagerLineData2;
			}).ToList<AbilityManagerLineData>();
			IL_01FC:
			this.FilterItems();
			int j = 0;
			int num = 0;
			while (j < this.filteredItems.Count)
			{
				if (this.filteredItems[j].ability != null)
				{
					this.filteredItems[j].quickKey = CS$<>8__locals1.<BeforeShow>g__getHotkeyChar|0(num++);
				}
				j++;
			}
			this.leftSideScroller.BeforeShow(this.filteredItems);
			this.leftSideScroller.onSelected.RemoveAllListeners();
			this.leftSideScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectLeft));
			this.leftSideScroller.onHighlight.RemoveAllListeners();
			this.leftSideScroller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightLeft));
			this.leftSideScroller.scrollContext.wraps = false;
			foreach (FrameworkUnityScrollChild frameworkUnityScrollChild in this.leftSideScroller.selectionClones)
			{
				frameworkUnityScrollChild.GetComponent<AbilityManagerLine>().screen = this;
			}
			this.UpdateMenuBars();
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000D1404 File Offset: 0x000CF604
		public void Refresh(bool ForceActivate = true)
		{
			if (this.leftSideScroller.selectedPosition < 0 || this.leftSideScroller.selectedPosition >= this.leftSideScroller.choices.Count)
			{
				this.BeforeShow();
				return;
			}
			FrameworkDataElement currentSelected = this.leftSideScroller.choices[this.leftSideScroller.selectedPosition];
			bool flag = ForceActivate || this.leftSideScroller.scrollContext.GetContextAt(this.leftSideScroller.selectedPosition).IsActive(true);
			this.BeforeShow();
			int num = this.leftSideScroller.choices.FindIndex((FrameworkDataElement item) => item.Id == currentSelected.Id);
			if (num != -1)
			{
				this.leftSideScroller.selectedPosition = num;
				if (flag)
				{
					NavigationContext navigationContext = this.leftSideScroller.scrollContext.GetContextAt(num);
					for (;;)
					{
						ProxyNavigationContext proxyNavigationContext = navigationContext as ProxyNavigationContext;
						if (proxyNavigationContext == null || proxyNavigationContext.proxyTo == null)
						{
							break;
						}
						navigationContext = proxyNavigationContext.proxyTo;
					}
					navigationContext.Activate(false);
					this.UpdateMenuBars();
					return;
				}
			}
			else if (ForceActivate)
			{
				this.leftSideScroller.selectedPosition = 0;
				this.leftSideScroller.scrollContext.GetContextAt(0).Activate(false);
				this.UpdateMenuBars();
			}
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000D1538 File Offset: 0x000CF738
		public void UpdateMenuBars()
		{
			this.hotkeyBar.BeforeShow(null, NavigationController.GetMenuOptions(AbilityManagerScreen.defaultMenuOptions));
			this.hotkeyBar.GetNavigationContext().disabled = true;
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000D1598 File Offset: 0x000CF798
		public void HandleMenuOption(FrameworkDataElement element)
		{
			if (element.Id == "Cancel")
			{
				this.Cancel();
			}
			if (element == AbilityManagerScreen.TOGGLE_SORT)
			{
				if (this.sortMode == AbilityManagerScreen.SortMode.Custom)
				{
					this.sortMode = AbilityManagerScreen.SortMode.Class;
				}
				else
				{
					this.sortMode = AbilityManagerScreen.SortMode.Custom;
				}
				this.Refresh(true);
			}
			if (element == AbilityManagerScreen.FILTER_ITEMS)
			{
				this.HandleFilterItems();
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000D15F4 File Offset: 0x000CF7F4
		public async void HandleFilterItems()
		{
			string text = await Popup.AskStringAsync("Search text:", this.searchText, 80, 0, null, true, true, null, false, null);
			if (text != null && text != null)
			{
				this.searchText = text;
				this.FilterItems();
				if (this.filteredItems.Count == 0)
				{
					await Popup.ShowAsync("No activated abilites found for '" + this.searchText + "'", true, true, true, true, false);
					this.searchText = "";
					this.FilterItems();
				}
				this.Refresh(true);
			}
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000D162C File Offset: 0x000CF82C
		public void FilterItems()
		{
			this.filteredItems.Clear();
			if (string.IsNullOrWhiteSpace(this.searchText))
			{
				AbilityManagerScreen.FILTER_ITEMS.Description = "search";
				this.filteredItems.AddRange(this.leftSideItems);
				return;
			}
			AbilityManagerScreen.FILTER_ITEMS.Description = "search: " + Markup.Color("w", this.searchText);
			this.searcher.searchText = this.searchText;
			HashSet<AbilityManagerLineData> hashSet = (from item in Process.ExtractTop<AbilityManagerLineData>(this.searcher, this.leftSideItems, (AbilityManagerLineData i) => i.searchText.ToLower(), null, this.leftSideItems.Count, 50)
				select item.Value).ToHashSet<AbilityManagerLineData>();
			foreach (AbilityManagerLineData abilityManagerLineData in this.leftSideItems)
			{
				if (hashSet.Contains(abilityManagerLineData))
				{
					this.filteredItems.Add(abilityManagerLineData);
				}
			}
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000D1764 File Offset: 0x000CF964
		public override void Hide()
		{
			CursorManager.setStyle(CursorManager.Style.Pointer);
			base.Hide();
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000D1774 File Offset: 0x000CF974
		public void Cancel()
		{
			if (AbilityManagerLine.dragging)
			{
				return;
			}
			ControlManager.ResetInput(false, true);
			this.Hide();
			this.menucomplete.TrySetResult(default(AbilityManagerScreen.Result));
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000D17AB File Offset: 0x000CF9AB
		void ControlManager.IControllerChangedEvent.ControllerChanged()
		{
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000D17B0 File Offset: 0x000CF9B0
		public void HandleSelectLeft(FrameworkDataElement element)
		{
			if (AbilityManagerLine.dragging)
			{
				return;
			}
			AbilityManagerLineData abilityManagerLineData = element as AbilityManagerLineData;
			if (abilityManagerLineData != null)
			{
				if (abilityManagerLineData.category != null)
				{
					this.classCollapsed[abilityManagerLineData.category] = !this.isCollapsed(abilityManagerLineData.category);
					this.BeforeShow();
					return;
				}
				if (!string.IsNullOrEmpty(abilityManagerLineData.ability.NotUsableDescription))
				{
					Popup.Show(abilityManagerLineData.ability.NotUsableDescription, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				this.menucomplete.TrySetResult(new AbilityManagerScreen.Result
				{
					ability = abilityManagerLineData.ability
				});
			}
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000D1850 File Offset: 0x000CFA50
		public void HandleHighlightLeft(FrameworkDataElement element)
		{
			AbilityManagerLineData abilityManagerLineData = element as AbilityManagerLineData;
			if (abilityManagerLineData != null)
			{
				if (abilityManagerLineData.ability != null)
				{
					this.rightSideIcon.FromRenderable(abilityManagerLineData.ability.GetUITile(), true);
					this.rightSideHeaderText.SetText(abilityManagerLineData.ability.DisplayName);
					StringBuilder stringBuilder = new StringBuilder();
					if (!string.IsNullOrEmpty(abilityManagerLineData.ability.Class))
					{
						stringBuilder.AppendColored("y", "Type: ");
						stringBuilder.Append(abilityManagerLineData.ability.Class);
						stringBuilder.Append("\n");
					}
					if (abilityManagerLineData.ability.Cooldown > 0)
					{
						stringBuilder.AppendColored("C", "Cooldown Remaining Turns: ");
						stringBuilder.Append(abilityManagerLineData.ability.CooldownRounds);
						stringBuilder.Append("\n");
					}
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append("\n");
					}
					stringBuilder.Append(abilityManagerLineData.ability.Description);
					this.rightSideDescriptionArea.SetText(stringBuilder.ToString());
					return;
				}
				this.rightSideIcon.FromRenderable(null, true);
				this.rightSideHeaderText.SetText(abilityManagerLineData.category);
				this.rightSideDescriptionArea.SetText("");
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000D1994 File Offset: 0x000CFB94
		public void Update()
		{
			if (!this.PickHide && AbilityManagerScreen.hideWhenShown.Contains(GameManager.Instance.CurrentGameView))
			{
				if (base.canvas.enabled)
				{
					base.canvas.enabled = false;
				}
				this.PickHide = true;
			}
			else if (this.PickHide && GameManager.Instance.CurrentGameView == "AbilityManagerScreen")
			{
				if (!base.canvas.enabled)
				{
					base.canvas.enabled = true;
				}
				this.PickHide = false;
			}
			if (base.isCurrentWindow() && this.navigationContext.IsActive(true) && this.skipFrame <= 0)
			{
				if (this.filteredItems != null)
				{
					foreach (AbilityManagerLineData abilityManagerLineData in this.filteredItems)
					{
						if (ControlManager.isCharDown(abilityManagerLineData.quickKey))
						{
							ControlManager.ConsumeCurrentInput();
							this.HandleSelectLeft(abilityManagerLineData);
						}
					}
				}
				if (this.lastContext != NavigationController.instance.activeContext)
				{
					this.lastContext = NavigationController.instance.activeContext;
					this.UpdateMenuBars();
				}
			}
			if (this.skipFrame > 0)
			{
				this.skipFrame--;
			}
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000D1AE4 File Offset: 0x000CFCE4
		public void MoveItem(AbilityManagerLineData data, int direction)
		{
			if (AbilityManagerLine.dragging)
			{
				return;
			}
			if (this.sortMode != AbilityManagerScreen.SortMode.Custom)
			{
				return;
			}
			int num = this.leftSideScroller.choices.IndexOf(data);
			if (num == -1)
			{
				return;
			}
			int num2 = num + direction;
			if (num2 < 0 || num2 >= this.leftSideScroller.choices.Count)
			{
				return;
			}
			List<string> list = this.leftSideScroller.choices.Where<FrameworkDataElement>((FrameworkDataElement item) => item != data).Select<FrameworkDataElement, string>(delegate(FrameworkDataElement item)
			{
				AbilityManagerLineData abilityManagerLineData = item as AbilityManagerLineData;
				if (abilityManagerLineData == null)
				{
					return null;
				}
				return abilityManagerLineData.ability.Command;
			}).ToList<string>();
			list.Insert(num2, data.ability.Command);
			ActivatedAbilities.PreferenceOrder = list;
			this.BeforeShow();
			this.leftSideScroller.scrollContext.contexts[num2].Activate(false);
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000D1BCB File Offset: 0x000CFDCB
		public void SerializeUnityOrdering()
		{
			if (this.sortMode != AbilityManagerScreen.SortMode.Custom)
			{
				return;
			}
			ActivatedAbilities.PreferenceOrder = this.<SerializeUnityOrdering>g__CurrentOrdering|45_0();
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000D1BE1 File Offset: 0x000CFDE1
		public IEnumerator ShowNextCycle()
		{
			yield return 0;
			yield return 0;
			this.BeforeShow();
			yield break;
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000D1BF0 File Offset: 0x000CFDF0
		public static async Task HandleRebindAsync(ActivatedAbilityEntry ability, string layerToResetTo = null)
		{
			await The.UiContext;
			try
			{
				AbilityManagerScreen.<>c__DisplayClass47_0 CS$<>8__locals1 = new AbilityManagerScreen.<>c__DisplayClass47_0();
				CS$<>8__locals1.currentControllerType = ControlManager.InputDeviceType.Keyboard;
				Task popupTask;
				List<string> rebind;
				using (CancellationTokenSource cancelTokenSource = new CancellationTokenSource())
				{
					popupTask = Popup.ShowKeybindAsync("Press the keyboard key to bind to {{w|" + ability.DisplayName + "}}", cancelTokenSource.Token);
					rebind = await ControlManager.SuspendControlsWhile<List<string>>(() => CommandBindingManager.GetRebindAsync(CS$<>8__locals1.currentControllerType, false, InputActionType.Button));
					cancelTokenSource.Cancel();
					await popupTask;
					if (rebind == null)
					{
						return;
					}
					if (CommandBindingManager.CommandUsesBinding(ability.Command, rebind))
					{
						return;
					}
					List<GameCommand> list = CommandBindingManager.GetCommandsWithBinding(rebind, CommandBindingManager.DynamicLayerConflictChecker()).ToList<GameCommand>();
					IEnumerable<GameCommand> commandsWithBinding = CommandBindingManager.GetCommandsWithBinding(rebind, null);
					if (commandsWithBinding.Any<GameCommand>((GameCommand b) => b.ID == "CmdSystemMenu"))
					{
						await Popup.ShowAsync(CommandBindingManager.GetBindingDisplayString(rebind) + " is already bound to the system menu.", true, true, true, true, false);
						return;
					}
					if (commandsWithBinding.Any<GameCommand>((GameCommand b) => b.ID == "CmdAbilities"))
					{
						await Popup.ShowAsync(CommandBindingManager.GetBindingDisplayString(rebind) + " is already bound to the ability picker.", true, true, true, true, false);
						return;
					}
					GameCommand gameCommand = list.Find((GameCommand command) => !command.CanRemoveBinding(CS$<>8__locals1.currentControllerType));
					if (gameCommand != null)
					{
						await KeybindsScreen.RequiredConflictBind(CommandBindingManager.GetBindingDisplayString(rebind), gameCommand.DisplayText);
						return;
					}
					if (list.Count > 0)
					{
						TaskAwaiter<bool> taskAwaiter = KeybindsScreen.ConfirmDynamicConflictBind(CommandBindingManager.GetBindingDisplayString(rebind), list, ability.DisplayName).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<bool> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<bool>);
						}
						if (!taskAwaiter.GetResult())
						{
							return;
						}
					}
					AbilityManager.PlayerAbilityLock.EnterReadLock();
					try
					{
						foreach (ActivatedAbilityEntry activatedAbilityEntry in AbilityManager.PlayerAbilities)
						{
							string command2 = activatedAbilityEntry.Command;
							if (command2 != null && command2 != ability.Command && CommandBindingManager.CommandUsesBinding(command2, rebind))
							{
								CommandBindingManager.ReplaceCommandBindingIndex(command2, 0, new List<string>(), CS$<>8__locals1.currentControllerType);
							}
						}
					}
					finally
					{
						AbilityManager.PlayerAbilityLock.ExitReadLock();
					}
					CommandBindingManager.ReplaceCommandBindingIndex(ability.Command, 0, rebind, CS$<>8__locals1.currentControllerType);
					CommandBindingManager.InitializeInputManager(false, false, null);
					CommandBindingManager.SaveCurrentKeymap();
					if (layerToResetTo != null)
					{
						GameManager.Instance.SetActiveLayersForNavCategory("Menu", true);
					}
					await Task.Yield();
					MetricsManager.LogKeybinding(ability.Command);
					ControlManager.ResetInput(false, true);
					AbilityBar.markDirty();
				}
				CS$<>8__locals1 = null;
				CancellationTokenSource cancelTokenSource = null;
				popupTask = null;
				rebind = null;
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("HandleRebind", ex);
			}
			finally
			{
			}
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000D1C3C File Offset: 0x000CFE3C
		public static async Task<bool> HandleRemoveBindAsync(ActivatedAbilityEntry abilityEntry)
		{
			AbilityManagerScreen.<>c__DisplayClass48_0 CS$<>8__locals1 = new AbilityManagerScreen.<>c__DisplayClass48_0();
			CS$<>8__locals1.abilityEntry = abilityEntry;
			await The.UiContext;
			return await NavigationController.instance.SuspendContextWhile<bool>(delegate
			{
				AbilityManagerScreen.<>c__DisplayClass48_0.<<HandleRemoveBindAsync>b__0>d <<HandleRemoveBindAsync>b__0>d;
				<<HandleRemoveBindAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<HandleRemoveBindAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<HandleRemoveBindAsync>b__0>d.<>1__state = -1;
				<<HandleRemoveBindAsync>b__0>d.<>t__builder.Start<AbilityManagerScreen.<>c__DisplayClass48_0.<<HandleRemoveBindAsync>b__0>d>(ref <<HandleRemoveBindAsync>b__0>d);
				return <<HandleRemoveBindAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000D1E40 File Offset: 0x000D0040
		[CompilerGenerated]
		private IEnumerable<string> <SerializeUnityOrdering>g__CurrentOrdering|45_0()
		{
			int num2;
			for (int x = 0; x < this.leftSideScroller.childRoot.transform.childCount; x = num2 + 1)
			{
				Transform element = this.leftSideScroller.childRoot.transform.GetChild(x);
				int num = this.leftSideScroller.selectionClones.FindIndex((FrameworkUnityScrollChild i) => i.transform == element);
				if (num != -1)
				{
					AbilityManagerLineData abilityManagerLineData = this.leftSideScroller.choices[num] as AbilityManagerLineData;
					if (abilityManagerLineData != null)
					{
						yield return abilityManagerLineData.ability.Command;
					}
				}
				num2 = x;
			}
			yield break;
		}

		// Token: 0x04001418 RID: 5144
		protected TaskCompletionSource<AbilityManagerScreen.Result> menucomplete = new TaskCompletionSource<AbilityManagerScreen.Result>();

		// Token: 0x04001419 RID: 5145
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x0400141A RID: 5146
		public AbilityManagerScreen.Context navigationContext = new AbilityManagerScreen.Context
		{
			buttonHandlers = null
		};

		// Token: 0x0400141B RID: 5147
		public FrameworkScroller hotkeyBar;

		// Token: 0x0400141C RID: 5148
		public FrameworkScroller leftSideScroller;

		// Token: 0x0400141D RID: 5149
		protected ActivatedAbilities activatedAbilities;

		// Token: 0x0400141E RID: 5150
		public List<AbilityManagerLineData> leftSideItems = new List<AbilityManagerLineData>();

		// Token: 0x0400141F RID: 5151
		public List<AbilityManagerLineData> filteredItems = new List<AbilityManagerLineData>();

		// Token: 0x04001420 RID: 5152
		public string searchText = "";

		// Token: 0x04001421 RID: 5153
		public UITextSkin rightSideHeaderText;

		// Token: 0x04001422 RID: 5154
		public UITextSkin rightSideDescriptionArea;

		// Token: 0x04001423 RID: 5155
		public UIThreeColorProperties rightSideIcon;

		// Token: 0x04001424 RID: 5156
		public bool RealityWeak;

		// Token: 0x04001425 RID: 5157
		public Dictionary<string, bool> classCollapsed = new Dictionary<string, bool>();

		// Token: 0x04001426 RID: 5158
		public AbilityManagerScreen.SortMode sortMode;

		// Token: 0x04001427 RID: 5159
		public static readonly MenuOption TOGGLE_SORT = new MenuOption
		{
			Id = "TOGGLE_SORT",
			KeyDescription = "Toggle Sort",
			InputCommand = "Toggle",
			Description = "toggle sort"
		};

		// Token: 0x04001428 RID: 5160
		public static readonly MenuOption FILTER_ITEMS = new MenuOption
		{
			Id = "FILTER_ITEMS",
			InputCommand = "CmdFilter",
			Description = "search"
		};

		// Token: 0x04001429 RID: 5161
		public static List<MenuOption> defaultMenuOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Cancel",
				InputCommand = "Cancel",
				Description = "Close Menu"
			},
			new MenuOption
			{
				InputCommand = "NavigationXYAxis",
				Description = "navigate",
				disabled = true
			},
			AbilityManagerScreen.TOGGLE_SORT,
			new MenuOption
			{
				InputCommand = "Accept",
				Description = "Activate Selected Ability",
				disabled = true
			},
			AbilityManagerScreen.FILTER_ITEMS
		};

		// Token: 0x0400142A RID: 5162
		private readonly AbilityManagerLineData searcher = new AbilityManagerLineData();

		// Token: 0x0400142B RID: 5163
		private NavigationContext lastContext;

		// Token: 0x0400142C RID: 5164
		private static List<string> hideWhenShown = new List<string> { "PickDirection", "PickTarget" };

		// Token: 0x0400142D RID: 5165
		private int skipFrame = 2;

		// Token: 0x0400142E RID: 5166
		private bool PickHide;

		// Token: 0x020017E9 RID: 6121
		public class Context : NavigationContext
		{
		}

		// Token: 0x020017EA RID: 6122
		public struct Result
		{
			// Token: 0x0400604E RID: 24654
			public ActivatedAbilityEntry ability;
		}

		// Token: 0x020017EB RID: 6123
		public enum SortMode
		{
			// Token: 0x04006050 RID: 24656
			Custom,
			// Token: 0x04006051 RID: 24657
			Class
		}
	}
}
