using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using XRL;
using XRL.CharacterBuilds.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000441 RID: 1089
	[UIView("Keybinds", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "Keybinds", UICanvasHost = 1)]
	public class KeybindsScreen : SingletonWindowBase<KeybindsScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x000D756B File Offset: 0x000D576B
		public bool breakBackButton
		{
			get
			{
				return Media.sizeClass < Media.SizeClass.Medium;
			}
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000D7576 File Offset: 0x000D5776
		public void getCurrentActiveControl()
		{
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				this.currentControllerType = ControlManager.InputDeviceType.Gamepad;
				ControlManager.instance.controllerFontType = ControlManager.ControllerFontType.XBox;
				return;
			}
			this.currentControllerType = ControlManager.InputDeviceType.Keyboard;
			ControlManager.instance.controllerFontType = ControlManager.ControllerFontType.Keyboard;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000D75A8 File Offset: 0x000D57A8
		public static async Task<bool> ShowKeybindsClick()
		{
			bool flag;
			if (!Options.ModernUI)
			{
				KeyMappingUI.Show();
				flag = false;
			}
			else
			{
				flag = await SingletonWindowBase<KeybindsScreen>.instance.KeybindsMenu();
			}
			return flag;
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000D75E4 File Offset: 0x000D57E4
		public async Task<bool> KeybindsMenu()
		{
			this.searchText = "";
			await The.UiContext;
			this.getCurrentActiveControl();
			this.QueryKeybinds();
			this.SelectFirst = true;
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetCanceled();
			}
			this.completionSource = new TaskCompletionSource<bool>();
			ControlManager.ResetInput(false, true);
			this.Show();
			bool info = await this.completionSource.Task;
			this.DisableNavContext(true);
			await The.UiContext;
			this.Hide();
			return info;
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000D7628 File Offset: 0x000D5828
		public async void Exit()
		{
			if (this.bChangesMade)
			{
				DialogResult dialogResult = await Popup.ShowYesNoCancelAsync("Would you like to save your changes?");
				if (dialogResult == DialogResult.Cancel)
				{
					return;
				}
				if (dialogResult == DialogResult.Yes)
				{
					CommandBindingManager.SaveCurrentKeymap();
					this.bChangesMade = false;
				}
				else if (dialogResult == DialogResult.No)
				{
					CommandBindingManager.LoadCurrentKeymap(false);
					this.bChangesMade = false;
				}
			}
			TaskCompletionSource<bool> taskCompletionSource = this.completionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetResult(false);
			}
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000D7660 File Offset: 0x000D5860
		public void Update()
		{
			if (this.globalContext.IsActive(true))
			{
				NavigationContext activeContext = NavigationController.instance.activeContext;
				bool flag = activeContext != null && activeContext.IsInside(this.keybindsScroller.GetNavigationContext());
				float width = base.rectTransform.rect.width;
				if (flag != this.wasInScroller || this.lastWidth != width)
				{
					this.wasInScroller = flag;
					this.lastWidth = width;
					this.backButton.gameObject.SetActive(!this.breakBackButton);
					this.safeArea.offsetMin = new Vector2((float)(this.breakBackButton ? 5 : 150), this.safeArea.offsetMin.y);
					this.safeArea.offsetMax = new Vector2((float)(this.breakBackButton ? (-150) : (-150)), this.safeArea.offsetMax.y);
					this.UpdateMenuBars();
				}
			}
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000D775C File Offset: 0x000D595C
		public void ControllerChanged()
		{
			this.UpdateMenuBars();
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000D7764 File Offset: 0x000D5964
		public void SetupContext()
		{
			this.globalContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.globalContext.buttonHandlers.Set(InputButtonTypes.CancelButton, global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit)));
			this.globalContext.commandHandlers = new Dictionary<string, Action>
			{
				{
					"CmdFilter",
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.searchInput.EnterAndOpen))
				},
				{
					KeybindsScreen.BACK_BUTTON.InputCommand,
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit))
				},
				{
					KeybindsScreen.REMOVE_BIND.InputCommand,
					global::XRL.UI.Framework.Event.Helpers.Handle(delegate
					{
						this.HandleMenuOption(KeybindsScreen.REMOVE_BIND);
					})
				},
				{
					KeybindsScreen.RESTORE_DEFAULTS.InputCommand,
					global::XRL.UI.Framework.Event.Helpers.Handle(delegate
					{
						this.HandleMenuOption(KeybindsScreen.RESTORE_DEFAULTS);
					})
				}
			};
			this.midHorizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			this.midHorizNav.contexts.Clear();
			this.midHorizNav.contexts.Add(this.backButton.navigationContext);
			this.midHorizNav.contexts.Add(this.categoryScroller.GetNavigationContext());
			this.midHorizNav.contexts.Add(this.vertNav);
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Clear();
			this.vertNav.contexts.Add(this.searchInput.context);
			this.searchInput.OnSearchTextChange.RemoveListener(new UnityAction<string>(this.OnSearchTextChange));
			this.searchInput.OnSearchTextChange.AddListener(new UnityAction<string>(this.OnSearchTextChange));
			if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
			{
				this.vertNav.contexts.Add(this.inputTypeContext.RequireContext<NavigationContext>());
				this.inputTypeContext.context.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.AcceptButton,
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.SelectInputType))
				} };
			}
			this.vertNav.contexts.Add(this.keybindsScroller.GetNavigationContext());
			this.vertNav.contexts.Add(this.hotkeyBar.GetNavigationContext());
			this.midHorizNav.Setup();
			this.midHorizNav.parentContext = this.globalContext;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000D79B1 File Offset: 0x000D5BB1
		public static MenuOption BACK_BUTTON
		{
			get
			{
				return EmbarkBuilderOverlayWindow.BackMenuOption;
			}
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000D79B8 File Offset: 0x000D5BB8
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
				Description = "select"
			});
			this.keyMenuOptions.Add(KeybindsScreen.REMOVE_BIND);
			if (this.breakBackButton)
			{
				this.keyMenuOptions.Add(KeybindsScreen.BACK_BUTTON);
			}
			this.keyMenuOptions.Add(KeybindsScreen.RESTORE_DEFAULTS);
			this.hotkeyBar.BeforeShow(null, this.keyMenuOptions);
			this.hotkeyBar.GetNavigationContext().disabled = false;
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
			foreach (NavigationContext navigationContext in this.hotkeyBar.scrollContext.contexts.GetRange(0, 3))
			{
				navigationContext.disabled = true;
			}
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000D7AF8 File Offset: 0x000D5CF8
		public async void HandleMenuOption(FrameworkDataElement data)
		{
			if (data == KeybindsScreen.RESTORE_DEFAULTS)
			{
				TaskAwaiter<DialogResult> taskAwaiter = Popup.ShowYesNoAsync("Are you sure you want to override your bindings with the default?").GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<DialogResult> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<DialogResult>);
				}
				if (taskAwaiter.GetResult() == DialogResult.Yes)
				{
					this.bChangesMade = true;
					await CommandBindingManager.RestoreDefaults();
				}
				this.QueryKeybinds();
				this.Show();
			}
			else if (data == KeybindsScreen.REMOVE_BIND)
			{
				int selectedPosition = this.keybindsScroller.scrollContext.selectedPosition;
				if (selectedPosition != -1)
				{
					KeybindDataRow row = this.keybindsScroller.scrollContext.data[selectedPosition] as KeybindDataRow;
					if (row != null)
					{
						ScrollContext<int, KeybindRow.KeybindRowSubContext> rowScroller = this.keybindsScroller.GetPrefabForIndex(selectedPosition).GetComponent<KeybindRow>().subscroller;
						GameCommand gameCommand = CommandBindingManager.CommandsByID[row.KeyId];
						if (!gameCommand.CanRemoveBinding(this.currentControllerType))
						{
							await Popup.ShowAsync("Can not remove the last binding for " + Markup.Color("C", gameCommand.DisplayText) + ".", true, true, true, true, false);
							return;
						}
						TaskAwaiter<DialogResult> taskAwaiter = Popup.ShowYesNoAsync("Are you sure you want to clear the binding for {{C|" + row.KeyDescription + "}}?").GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<DialogResult> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<DialogResult>);
						}
						if (taskAwaiter.GetResult() == DialogResult.Yes)
						{
							this.bChangesMade = true;
							CommandBindingManager.ReplaceCommandBindingIndex(row.KeyId, rowScroller.selectedPosition, new List<string>(), this.currentControllerType);
							CommandBindingManager.InitializeInputManager(false, false, null);
						}
						this.QueryKeybinds();
						this.Show();
						rowScroller = null;
					}
					row = null;
				}
			}
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000D7B38 File Offset: 0x000D5D38
		public void FilterItems()
		{
			this.filteredItems.Clear();
			if (string.IsNullOrWhiteSpace(this.searchText))
			{
				this.searchText = "";
				this.filteredItems.AddRange(this.menuItems);
				return;
			}
			this.searcher.SearchWords = this.searchText;
			IEnumerable<ExtractedResult<KeybindDataRow>> enumerable = Process.ExtractTop<KeybindDataRow>(this.searcher, this.menuItems.OfType<KeybindDataRow>(), (KeybindDataRow i) => i.SearchWords.ToLower(), null, this.menuItems.Count, 50);
			Dictionary<string, KeybindCategoryRow> dictionary = this.menuItems.OfType<KeybindCategoryRow>().ToDictionary<KeybindCategoryRow, string>((KeybindCategoryRow i) => i.CategoryId);
			foreach (IGrouping<string, ExtractedResult<KeybindDataRow>> grouping in from s in enumerable
				group s by s.Value.CategoryId)
			{
				this.categoryExpanded[grouping.Key] = true;
				this.filteredItems.Add(dictionary[grouping.Key]);
				this.filteredItems.AddRange(grouping.Select<ExtractedResult<KeybindDataRow>, KeybindDataRow>((ExtractedResult<KeybindDataRow> match) => match.Value));
			}
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000D7CB0 File Offset: 0x000D5EB0
		public void OnSearchTextChange(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				this.searchText = "";
			}
			else
			{
				this.searchText = text;
			}
			this.FilterItems();
			this.Show();
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000D7CDA File Offset: 0x000D5EDA
		public IEnumerable<FrameworkDataElement> GetMenuItems()
		{
			foreach (FrameworkDataElement frameworkDataElement in this.filteredItems)
			{
				KeybindDataRow keybindDataRow = frameworkDataElement as KeybindDataRow;
				if (keybindDataRow != null)
				{
					if (this.categoryExpanded[keybindDataRow.CategoryId])
					{
						yield return frameworkDataElement;
					}
				}
				else
				{
					KeybindCategoryRow keybindCategoryRow = frameworkDataElement as KeybindCategoryRow;
					if (keybindCategoryRow != null)
					{
						keybindCategoryRow.Collapsed = !this.categoryExpanded[keybindCategoryRow.CategoryId];
						yield return frameworkDataElement;
					}
				}
			}
			List<FrameworkDataElement>.Enumerator enumerator = default(List<FrameworkDataElement>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000D7CEA File Offset: 0x000D5EEA
		public IEnumerable<FrameworkDataElement> GetCategoryItems()
		{
			foreach (FrameworkDataElement frameworkDataElement in this.filteredItems)
			{
				KeybindCategoryRow keybindCategoryRow = frameworkDataElement as KeybindCategoryRow;
				if (keybindCategoryRow != null)
				{
					keybindCategoryRow.Collapsed = !this.categoryExpanded[keybindCategoryRow.CategoryId];
					yield return frameworkDataElement;
				}
			}
			List<FrameworkDataElement>.Enumerator enumerator = default(List<FrameworkDataElement>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000D7CFC File Offset: 0x000D5EFC
		public override void Show()
		{
			base.Show();
			EmbarkBuilderModuleBackButton embarkBuilderModuleBackButton = this.backButton;
			if (embarkBuilderModuleBackButton != null)
			{
				embarkBuilderModuleBackButton.gameObject.SetActive(!this.breakBackButton);
			}
			this.safeArea.offsetMin = new Vector2((float)(this.breakBackButton ? 5 : 150), this.safeArea.offsetMin.y);
			this.safeArea.offsetMax = new Vector2((float)(this.breakBackButton ? (-150) : (-150)), this.safeArea.offsetMax.y);
			if (this.backButton.navigationContext == null)
			{
				this.backButton.Awake();
			}
			this.backButton.navigationContext.buttonHandlers = new Dictionary<InputButtonTypes, Action>();
			this.backButton.navigationContext.buttonHandlers.Set(InputButtonTypes.AcceptButton, global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Exit)));
			this.keybindsScroller.scrollContext.wraps = false;
			this.keybindsScroller.BeforeShow(null, this.GetMenuItems());
			this.categoryScroller.scrollContext.wraps = true;
			this.categoryScroller.BeforeShow(null, this.GetCategoryItems());
			foreach (KeybindRow keybindRow in this.keybindsScroller.selectionClones.Select<FrameworkUnityScrollChild, KeybindRow>((FrameworkUnityScrollChild s) => s.GetComponent<KeybindRow>()))
			{
				keybindRow.GetNavigationContext();
				if (keybindRow != null)
				{
					keybindRow.onRebind.RemoveAllListeners();
					keybindRow.onRebind.AddListener(new UnityAction<KeybindDataRow, int, KeybindRow>(this.HandleRebind));
				}
			}
			if (this.SelectFirst)
			{
				this.SelectFirst = false;
				this.keybindsScroller.scrollContext.selectedPosition = 1;
			}
			this.keybindsScroller.onSelected.RemoveAllListeners();
			this.keybindsScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelect));
			this.keybindsScroller.onHighlight.RemoveAllListeners();
			this.keybindsScroller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlight));
			this.categoryScroller.onSelected.RemoveAllListeners();
			this.categoryScroller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectLeft));
			this.categoryScroller.onHighlight.RemoveAllListeners();
			this.categoryScroller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightLeft));
			this.UpdateMenuBars();
			this.SetupContext();
			this.EnableNavContext();
			foreach (AbstractScrollContext abstractScrollContext in this.GetGridSiblings())
			{
				abstractScrollContext.gridSiblings = new Func<IEnumerable<AbstractScrollContext>>(this.GetGridSiblings);
			}
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000D7FEC File Offset: 0x000D61EC
		public void HandleSelect(FrameworkDataElement element)
		{
			KeybindCategoryRow keybindCategoryRow = element as KeybindCategoryRow;
			if (keybindCategoryRow != null)
			{
				this.categoryExpanded[keybindCategoryRow.CategoryId] = !this.categoryExpanded[keybindCategoryRow.CategoryId];
				this.Show();
				this.keybindsScroller.scrollContext.SelectIndex(this.GetMenuItems().ToList<FrameworkDataElement>().FindIndex((FrameworkDataElement s) => s == element));
			}
			foreach (AbstractScrollContext abstractScrollContext in this.GetGridSiblings())
			{
				abstractScrollContext.gridSiblings = new Func<IEnumerable<AbstractScrollContext>>(this.GetGridSiblings);
			}
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000D80B8 File Offset: 0x000D62B8
		public void HandleHighlight(FrameworkDataElement element)
		{
			this.lastSelectedElement = element;
			string catId = null;
			KeybindCategoryRow keybindCategoryRow = element as KeybindCategoryRow;
			if (keybindCategoryRow != null)
			{
				catId = keybindCategoryRow.CategoryId;
			}
			else
			{
				KeybindDataRow keybindDataRow = element as KeybindDataRow;
				if (keybindDataRow != null)
				{
					catId = keybindDataRow.CategoryId;
				}
			}
			if (catId != null)
			{
				this.categoryScroller.selectedPosition = (this.categoryScroller.scrollContext.selectedPosition = this.GetCategoryItems().ToList<FrameworkDataElement>().FindIndex(delegate(FrameworkDataElement s)
				{
					KeybindCategoryRow keybindCategoryRow2 = s as KeybindCategoryRow;
					return ((keybindCategoryRow2 != null) ? keybindCategoryRow2.CategoryId : null) == catId;
				}));
			}
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000D814C File Offset: 0x000D634C
		public void HandleSelectLeft(FrameworkDataElement element)
		{
			KeybindCategoryRow cat = element as KeybindCategoryRow;
			if (cat != null)
			{
				this.categoryExpanded[cat.CategoryId] = true;
				this.Show();
				int num = this.GetMenuItems().ToList<FrameworkDataElement>().FindIndex(delegate(FrameworkDataElement s)
				{
					KeybindCategoryRow keybindCategoryRow = s as KeybindCategoryRow;
					return ((keybindCategoryRow != null) ? keybindCategoryRow.CategoryId : null) == cat.CategoryId;
				});
				this.keybindsScroller.scrollContext.GetContextAt(num).Activate(false);
				this.keybindsScroller.ScrollSelectedIntoView();
				ScrollViewCalcs.ScrollToTopOfRect(this.keybindsScroller.GetPrefabForIndex(num).GetComponent<RectTransform>(), null);
			}
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000D81E8 File Offset: 0x000D63E8
		public void HandleHighlightLeft(FrameworkDataElement element)
		{
			KeybindCategoryRow cat = element as KeybindCategoryRow;
			if (cat != null)
			{
				FrameworkDataElement frameworkDataElement = this.GetMenuItems().Skip<FrameworkDataElement>(this.keybindsScroller.selectedPosition).FirstOrDefault<FrameworkDataElement>();
				KeybindCategoryRow keybindCategoryRow = frameworkDataElement as KeybindCategoryRow;
				string text;
				if (keybindCategoryRow == null)
				{
					KeybindDataRow keybindDataRow = frameworkDataElement as KeybindDataRow;
					text = ((keybindDataRow != null) ? keybindDataRow.CategoryId : null);
				}
				else
				{
					text = keybindCategoryRow.CategoryId;
				}
				if (text != cat.CategoryId)
				{
					int num = this.GetMenuItems().ToList<FrameworkDataElement>().FindIndex(delegate(FrameworkDataElement s)
					{
						KeybindCategoryRow keybindCategoryRow2 = s as KeybindCategoryRow;
						return ((keybindCategoryRow2 != null) ? keybindCategoryRow2.CategoryId : null) == cat.CategoryId;
					});
					this.keybindsScroller.selectedPosition = (this.keybindsScroller.scrollContext.selectedPosition = num);
					this.keybindsScroller.ScrollSelectedIntoView();
					ScrollViewCalcs.ScrollToTopOfRect(this.keybindsScroller.GetPrefabForIndex(num).GetComponent<RectTransform>(), null);
				}
			}
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000D82C8 File Offset: 0x000D64C8
		public override void Hide()
		{
			base.Hide();
			this.DisableNavContext(true);
			base.gameObject.SetActive(false);
			ControlManager.ResetInput(false, true);
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000D82EA File Offset: 0x000D64EA
		public void EnableNavContext()
		{
			this.globalContext.disabled = false;
			this.keybindsScroller.GetNavigationContext().ActivateAndEnable();
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000D8309 File Offset: 0x000D6509
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

		// Token: 0x0600295B RID: 10587 RVA: 0x000D8344 File Offset: 0x000D6544
		public async void HandleRebind(KeybindDataRow binding, int bindIndex, KeybindRow unityRow)
		{
			KeybindsScreen.<>c__DisplayClass51_0 CS$<>8__locals1 = new KeybindsScreen.<>c__DisplayClass51_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.binding = binding;
			CS$<>8__locals1.bindIndex = bindIndex;
			KeybindBox keybindBox;
			switch (CS$<>8__locals1.bindIndex)
			{
			case 0:
				keybindBox = unityRow.box1;
				break;
			case 1:
				keybindBox = unityRow.box2;
				break;
			case 2:
				keybindBox = unityRow.box3;
				break;
			case 3:
				keybindBox = unityRow.box4;
				break;
			default:
				keybindBox = unityRow.box1;
				break;
			}
			KeybindBox box = keybindBox;
			box.editMode = true;
			box.Update();
			await NavigationController.instance.SuspendContextWhile<bool>(delegate
			{
				KeybindsScreen.<>c__DisplayClass51_0.<<HandleRebind>b__0>d <<HandleRebind>b__0>d;
				<<HandleRebind>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<HandleRebind>b__0>d.<>4__this = CS$<>8__locals1;
				<<HandleRebind>b__0>d.<>1__state = -1;
				<<HandleRebind>b__0>d.<>t__builder.Start<KeybindsScreen.<>c__DisplayClass51_0.<<HandleRebind>b__0>d>(ref <<HandleRebind>b__0>d);
				return <<HandleRebind>b__0>d.<>t__builder.Task;
			});
			GameManager.Instance.SetActiveLayersForNavCategory("Keybind", true);
			box.editMode = false;
			box = null;
			this.QueryKeybinds();
			this.Show();
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000D8394 File Offset: 0x000D6594
		private async Task HandleRebindAsync(KeybindDataRow binding, int bindIndex)
		{
			bool flag = await KeybindsScreen.HandleRebindAsync(CommandBindingManager.CommandsByID[binding.KeyId], bindIndex, this.currentControllerType);
			this.bChangesMade = this.bChangesMade || flag;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000D83E8 File Offset: 0x000D65E8
		public static async Task<bool> HandleRebindAsync(GameCommand command, int bindIndex, ControlManager.InputDeviceType deviceType)
		{
			bool flag;
			try
			{
				await The.UiContext;
				bool allowGamepadAlt = deviceType == ControlManager.InputDeviceType.Gamepad && command.ID != "GamepadAlt";
				List<string> rebind = await ControlManager.SuspendControlsWhile<List<string>>(() => CommandBindingManager.GetRebindAsync(deviceType, allowGamepadAlt, command.Type));
				if (rebind == null)
				{
					flag = false;
				}
				else if (CommandBindingManager.CommandUsesBinding(command, rebind))
				{
					flag = false;
				}
				else
				{
					List<GameCommand> conflicts = CommandBindingManager.GetCommandsWithBinding(rebind, CommandBindingManager.ConflictChecker(command)).ToList<GameCommand>();
					string bindingDisplayString = CommandBindingManager.GetBindingDisplayString(rebind);
					GameCommand gameCommand = conflicts.Find((GameCommand command) => !command.CanRemoveBinding(deviceType));
					if (gameCommand != null)
					{
						await KeybindsScreen.RequiredConflictBind(bindingDisplayString, gameCommand.DisplayText);
						flag = false;
					}
					else
					{
						if (conflicts.Count > 0)
						{
							TaskAwaiter<bool> taskAwaiter = KeybindsScreen.ConfirmConflictBind(bindingDisplayString, conflicts, command.DisplayText).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								await taskAwaiter;
								TaskAwaiter<bool> taskAwaiter2;
								taskAwaiter = taskAwaiter2;
								taskAwaiter2 = default(TaskAwaiter<bool>);
							}
							if (!taskAwaiter.GetResult())
							{
								return false;
							}
							foreach (GameCommand gameCommand2 in conflicts)
							{
								CommandBindingManager.RemoveCommandBinding(gameCommand2.ID, rebind);
							}
						}
						CommandBindingManager.ReplaceCommandBindingIndex(command.ID, bindIndex, rebind, deviceType);
						CommandBindingManager.InitializeInputManager(false, true, null);
						await Task.Yield();
						MetricsManager.LogKeybinding(command);
						while (CommandBindingManager.CommandBindings[command.ID].IsPressed(false))
						{
							await Task.Yield();
						}
						ControlManager.ResetInput(false, true);
						flag = true;
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("HandleRebind", ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000D843B File Offset: 0x000D663B
		public IEnumerable<AbstractScrollContext> GetGridSiblings()
		{
			foreach (NavigationContext navigationContext in this.keybindsScroller.scrollContext.contexts)
			{
				AbstractScrollContext abstractScrollContext = ((ScrollChildContext)navigationContext).proxyTo as AbstractScrollContext;
				if (abstractScrollContext != null)
				{
					yield return abstractScrollContext;
				}
			}
			List<NavigationContext>.Enumerator enumerator = default(List<NavigationContext>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000D844C File Offset: 0x000D664C
		public async void SelectInputType()
		{
			int num = await Popup.PickOptionAsync("Select Controller", null, "", this.ControlTypeDisplayName.Values.ToArray<string>(), null, null, null, null, 0, 60, 0, -1, false, true, false, true);
			if (num >= 0)
			{
				this.currentControllerType = this.ControlTypeDisplayName.Keys.ElementAt<ControlManager.InputDeviceType>(num);
				this.QueryKeybinds();
				this.Show();
			}
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000D8484 File Offset: 0x000D6684
		public static async Task<bool> ConfirmConflictBind(string KeyDescription, List<GameCommand> Conflicts, string NewCommand)
		{
			int index = -1;
			string text = Conflicts.Aggregate("", delegate(string carry, GameCommand command)
			{
				int index2 = index;
				index = index2 + 1;
				if (index == 0)
				{
					return Markup.Color("C", command.DisplayText);
				}
				if (index == Conflicts.Count - 1)
				{
					return carry + " and " + Markup.Color("C", command.DisplayText);
				}
				return carry + ", " + Markup.Color("C", command.DisplayText);
			});
			TaskAwaiter<DialogResult> taskAwaiter = Popup.ShowYesNoAsync(string.Concat(new string[]
			{
				Markup.Color("W", KeyDescription),
				" is already bound to ",
				text,
				".\r\n\r\nDo you want to bind it to ",
				Markup.Color("C", NewCommand),
				" instead?"
			})).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<DialogResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<DialogResult>);
			}
			return taskAwaiter.GetResult() == DialogResult.Yes;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000D84D8 File Offset: 0x000D66D8
		public static async Task<bool> ConfirmDynamicConflictBind(string KeyDescription, List<GameCommand> Conflicts, string NewCommand)
		{
			int index = -1;
			string text = Conflicts.Aggregate("", delegate(string carry, GameCommand command)
			{
				int index2 = index;
				index = index2 + 1;
				if (index == 0)
				{
					return Markup.Color("C", command.DisplayText);
				}
				if (index == Conflicts.Count - 1)
				{
					return carry + " and " + Markup.Color("C", command.DisplayText);
				}
				return carry + ", " + Markup.Color("C", command.DisplayText);
			});
			TaskAwaiter<DialogResult> taskAwaiter = Popup.ShowYesNoAsync(string.Concat(new string[]
			{
				Markup.Color("W", KeyDescription),
				" is already bound to ",
				text,
				".\r\n\r\nDo you want to bind it to ",
				Markup.Color("C", NewCommand),
				" anyway?"
			})).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<DialogResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<DialogResult>);
			}
			return taskAwaiter.GetResult() == DialogResult.Yes;
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000D852C File Offset: 0x000D672C
		public static async Task RequiredConflictBind(string KeyDescription, string CommandName)
		{
			await Popup.ShowAsync(string.Concat(new string[]
			{
				Markup.Color("W", KeyDescription),
				" is already bound to ",
				Markup.Color("C", CommandName),
				".  This is a required bind and can't be removed.\r\n\r\nChoose a new bind for ",
				Markup.Color("C", CommandName),
				" first, and then rebind ",
				Markup.Color("W", KeyDescription),
				"."
			}), true, true, true, true, false);
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x000D8577 File Offset: 0x000D6777
		public InputDevice currentGamepad
		{
			get
			{
				return Gamepad.current;
			}
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000D8580 File Offset: 0x000D6780
		public void QueryKeybinds()
		{
			this.menuItems.Clear();
			bool flag = this.currentControllerType == ControlManager.InputDeviceType.Keyboard;
			if (flag)
			{
				this.inputTypeText.SetText("{{C|Configuring Controller:}} {{c|Keyboard && Mouse}}");
			}
			else
			{
				UITextSkin uitextSkin = this.inputTypeText;
				string text = "{{C|Configuring Controller:}} {{c|";
				InputDevice currentGamepad = this.currentGamepad;
				uitextSkin.SetText(text + (((currentGamepad != null) ? currentGamepad.name : null) ?? "<no controller detected>") + "}}");
			}
			foreach (string text2 in CommandBindingManager.CategoriesInOrder)
			{
				if (!Application.isConsolePlatform || !text2.Contains("mouse", StringComparison.InvariantCultureIgnoreCase))
				{
					this.menuItems.Add(new KeybindCategoryRow
					{
						CategoryId = text2,
						CategoryDescription = text2
					});
					if (!this.categoryExpanded.ContainsKey(text2))
					{
						this.categoryExpanded[text2] = true;
					}
					foreach (GameCommand gameCommand in CommandBindingManager.CommandsByCategory[text2])
					{
						if ((gameCommand.Type == InputActionType.Button && !(gameCommand.ID == "GamepadAlt")) || !flag)
						{
							string text3;
							string text4;
							string text5;
							string text6;
							CommandBindingManager.GetCommandBindings(gameCommand.ID, this.currentControllerType, out text3, out text4, out text5, out text6);
							this.menuItems.Add(new KeybindDataRow
							{
								CategoryId = gameCommand.Category,
								KeyId = gameCommand.ID,
								KeyDescription = gameCommand.DisplayText,
								SearchWords = text2 + " " + gameCommand.DisplayText,
								Bind1 = text3,
								Bind2 = text4,
								Bind3 = text5,
								Bind4 = text6
							});
						}
					}
				}
			}
			this.FilterItems();
		}

		// Token: 0x04001525 RID: 5413
		public FrameworkContext inputTypeContext;

		// Token: 0x04001526 RID: 5414
		public UITextSkin inputTypeText;

		// Token: 0x04001527 RID: 5415
		public Image background;

		// Token: 0x04001528 RID: 5416
		public FrameworkScroller hotkeyBar;

		// Token: 0x04001529 RID: 5417
		public VisibleWindowScroller keybindsScroller;

		// Token: 0x0400152A RID: 5418
		public FrameworkScroller categoryScroller;

		// Token: 0x0400152B RID: 5419
		public FrameworkSearchInput searchInput;

		// Token: 0x0400152C RID: 5420
		public RectTransform safeArea;

		// Token: 0x0400152D RID: 5421
		public EmbarkBuilderModuleBackButton backButton;

		// Token: 0x0400152E RID: 5422
		public TaskCompletionSource<bool> completionSource;

		// Token: 0x0400152F RID: 5423
		public NavigationContext globalContext = new NavigationContext();

		// Token: 0x04001530 RID: 5424
		public ScrollContext<NavigationContext> midHorizNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001531 RID: 5425
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001532 RID: 5426
		public List<FrameworkDataElement> menuItems = new List<FrameworkDataElement>();

		// Token: 0x04001533 RID: 5427
		public List<FrameworkDataElement> filteredItems = new List<FrameworkDataElement>();

		// Token: 0x04001534 RID: 5428
		public Dictionary<string, bool> categoryExpanded = new Dictionary<string, bool>();

		// Token: 0x04001535 RID: 5429
		public float lastWidth;

		// Token: 0x04001536 RID: 5430
		public float breakpointBackButtonWidth;

		// Token: 0x04001537 RID: 5431
		public bool wasInScroller;

		// Token: 0x04001538 RID: 5432
		private bool SelectFirst = true;

		// Token: 0x04001539 RID: 5433
		public static readonly MenuOption REMOVE_BIND = new MenuOption
		{
			Id = "REMOVE_BIND",
			InputCommand = "CmdDelete",
			Description = "remove keybind"
		};

		// Token: 0x0400153A RID: 5434
		public static readonly MenuOption RESTORE_DEFAULTS = new MenuOption
		{
			Id = "RESTORE_DEFAULTS",
			InputCommand = "V Positive",
			Description = "restore defaults"
		};

		// Token: 0x0400153B RID: 5435
		public List<MenuOption> keyMenuOptions = new List<MenuOption>();

		// Token: 0x0400153C RID: 5436
		public string searchText;

		// Token: 0x0400153D RID: 5437
		private KeybindDataRow searcher = new KeybindDataRow();

		// Token: 0x0400153E RID: 5438
		public FrameworkDataElement lastSelectedElement;

		// Token: 0x0400153F RID: 5439
		public bool bChangesMade;

		// Token: 0x04001540 RID: 5440
		public Dictionary<ControlManager.InputDeviceType, string> ControlTypeDisplayName = new Dictionary<ControlManager.InputDeviceType, string>
		{
			{
				ControlManager.InputDeviceType.Keyboard,
				"Keyboard && Mouse"
			},
			{
				ControlManager.InputDeviceType.Gamepad,
				"Gamepad"
			}
		};

		// Token: 0x04001541 RID: 5441
		public ControlManager.InputDeviceType currentControllerType = ControlManager.InputDeviceType.Keyboard;
	}
}
