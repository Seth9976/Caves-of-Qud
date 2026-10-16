using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000457 RID: 1111
	[UIView("PickGameObject", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "PickGameObject", UICanvasHost = 1)]
	public class PickGameObjectScreen : SingletonWindowBase<PickGameObjectScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x060029F7 RID: 10743 RVA: 0x000DC3B4 File Offset: 0x000DA5B4
		public bool isCollapsed(string category)
		{
			bool flag;
			if (!this.categoryCollapsed.TryGetValue(category, out flag))
			{
				return PickGameObjectScreen.CategoryPriority != null;
			}
			return flag;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000DC3DC File Offset: 0x000DA5DC
		public void SetupContext()
		{
			this.vertNav.parentContext = this.navigationContext;
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Add(this.itemScrollerController.scrollContext);
			this.vertNav.contexts.Add(this.hotkeyBar.scrollContext);
			PickGameObjectScreen.Context context = this.navigationContext;
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
						PickGameObjectScreen.TOGGLE_SORT.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(PickGameObjectScreen.TOGGLE_SORT);
						})
					},
					{
						PickGameObjectScreen.TAKE_ALL.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(PickGameObjectScreen.TAKE_ALL);
						})
					},
					{
						PickGameObjectScreen.STORE_ITEM.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(PickGameObjectScreen.STORE_ITEM);
						})
					}
				};
			}
			PickGameObjectScreen.TOGGLE_SORT.Description = PickGameObjectScreen.sortModeDescription;
			this.vertNav.wraps = true;
			this.vertNav.Setup();
			this.itemScrollerController.scrollContext.parentContext = this.vertNav;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x000DC528 File Offset: 0x000DA728
		public static string sortModeDescription
		{
			get
			{
				return "sort: " + Markup.Color((SingletonWindowBase<PickGameObjectScreen>.instance.sortMode == PickGameObjectScreen.SortMode.AZ) ? "w" : "y", PickGameObjectScreen.PreserveOrder ? "list" : "a-z") + "/" + Markup.Color((SingletonWindowBase<PickGameObjectScreen>.instance.sortMode == PickGameObjectScreen.SortMode.Category) ? "w" : "y", "by class");
			}
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000DC598 File Offset: 0x000DA798
		protected async Task<PickGameObjectScreen.Result> showScreen(IList<global::XRL.World.GameObject> Items, string Title, PickItem.PickItemDialogStyle Style, global::XRL.World.GameObject Actor, global::XRL.World.GameObject Container, Cell Cell, bool PreserveOrder, string CategoryPriority = null, bool ShowContext = false, bool NotePlayerOwned = false, bool Reentry = false)
		{
			The.Player.IsRealityDistortionUsable(null, null);
			PickGameObjectScreen.RequestInterfaceExit = false;
			this.style = Style;
			PickGameObjectScreen.Actor = Actor;
			PickGameObjectScreen.Container = Container;
			PickGameObjectScreen.Cell = Cell;
			PickGameObjectScreen.PreserveOrder = PreserveOrder;
			PickGameObjectScreen.CategoryPriority = ((CategoryPriority != null) ? CategoryPriority.CachedCommaExpansion() : null);
			PickGameObjectScreen.ShowContext = ShowContext;
			PickGameObjectScreen.NotePlayerOwned = NotePlayerOwned;
			this.menucomplete.TrySetCanceled();
			this.menucomplete = new TaskCompletionSource<PickGameObjectScreen.Result>();
			this.rawGameObjects.Clear();
			this.rawGameObjects.AddRange(Items);
			PickGameObjectScreen.Result result2;
			try
			{
				MinEvent.UIHold = true;
				await The.UiContext;
				GameManager.Instance.PushGameView("PickGameObject", true);
				if (!Reentry)
				{
					if (this.categoryCollapsed == null)
					{
						this.categoryCollapsed = new Dictionary<string, bool>();
					}
					this.categoryCollapsed.Clear();
					if (CategoryPriority != null)
					{
						foreach (string text in PickGameObjectScreen.CategoryPriority)
						{
							this.categoryCollapsed.Add(text, false);
						}
					}
				}
				UITextSkin uitextSkin = this.titleText;
				if (uitextSkin != null)
				{
					uitextSkin.SetText(Title);
				}
				this.Show();
				this.BeforeShow(Reentry);
				this.itemScrollerController.scrollContext.ActivateAndEnable();
				PickGameObjectScreen.Result result = await this.menucomplete.Task;
				result.requestInterfaceExit = PickGameObjectScreen.RequestInterfaceExit;
				result2 = result;
			}
			finally
			{
				this.Hide();
				GameManager.Instance.PopGameView(true);
				MinEvent.UIHold = false;
			}
			return result2;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000DC63C File Offset: 0x000DA83C
		public static Task<PickGameObjectScreen.Result> show(IList<global::XRL.World.GameObject> Items, string Title, PickItem.PickItemDialogStyle style, global::XRL.World.GameObject Actor, global::XRL.World.GameObject Container, Cell Cell, bool PreserveOrder, string CategoryPriority, bool ShowContext, bool NotePlayerOwned, bool Reentry)
		{
			return NavigationController.instance.SuspendContextWhile<PickGameObjectScreen.Result>(() => SingletonWindowBase<PickGameObjectScreen>.instance.showScreen(Items, Title, style, Actor, Container, Cell, PreserveOrder, CategoryPriority, ShowContext, NotePlayerOwned, Reentry));
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000DC6BC File Offset: 0x000DA8BC
		public void UpdateViewFromData(bool Reentry)
		{
			int selectedPosition = this.itemScrollerController.selectedPosition;
			if (this.listItems == null)
			{
				this.listItems = new List<PickGameObjectLineData>();
			}
			this.listItems.ForEach(delegate(PickGameObjectLineData i)
			{
				i.free();
			});
			this.listItems.Clear();
			int num = 0;
			this.nameCache.Clear();
			this.categoryCache.Clear();
			if (this.sortMode == PickGameObjectScreen.SortMode.Category)
			{
				this.usedCategories.Clear();
				foreach (global::XRL.World.GameObject gameObject in this.rawGameObjects)
				{
					if (!this.categoryCache.ContainsKey(gameObject))
					{
						this.categoryCache.Add(gameObject, gameObject.GetInventoryCategory(false));
					}
				}
				foreach (KeyValuePair<string, List<global::XRL.World.GameObject>> keyValuePair in this.objectCategories)
				{
					keyValuePair.Value.Clear();
				}
				foreach (global::XRL.World.GameObject gameObject2 in this.rawGameObjects)
				{
					string inventoryCategory = gameObject2.GetInventoryCategory(false);
					if (!this.objectCategories.ContainsKey(inventoryCategory))
					{
						this.objectCategories.Add(inventoryCategory, new List<global::XRL.World.GameObject>());
					}
					this.objectCategories[inventoryCategory].Add(gameObject2);
					if (!this.usedCategories.Contains(inventoryCategory))
					{
						this.usedCategories.Add(inventoryCategory);
					}
				}
				if (!PickGameObjectScreen.PreserveOrder)
				{
					this.usedCategories.Sort();
				}
			}
			if (this.sortMode == PickGameObjectScreen.SortMode.Category)
			{
				num = 0;
				using (List<string>.Enumerator enumerator3 = this.usedCategories.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						string text = enumerator3.Current;
						this.listItems.Add(PooledFrameworkDataElement<PickGameObjectLineData>.next().set(PickGameObjectLineDataType.Category, PickGameObjectLineDataStyle.Interact, null, text, this.isCollapsed(text), false, this.getHotkeyChar(num), this.getHotkeyDescription(num)));
						num++;
						if (!this.isCollapsed(text))
						{
							if (!PickGameObjectScreen.PreserveOrder)
							{
								this.objectCategories[text].Sort((global::XRL.World.GameObject a, global::XRL.World.GameObject b) => this.categoryCache[a].CompareTo(this.categoryCache[b]));
							}
							foreach (global::XRL.World.GameObject gameObject3 in this.objectCategories[text])
							{
								this.listItems.Add(PooledFrameworkDataElement<PickGameObjectLineData>.next().set(PickGameObjectLineDataType.Item, PickGameObjectLineDataStyle.Interact, gameObject3, text, false, true, this.getHotkeyChar(num), this.getHotkeyDescription(num)));
								num++;
							}
						}
					}
					goto IL_038F;
				}
			}
			if (this.sortMode == PickGameObjectScreen.SortMode.AZ)
			{
				foreach (global::XRL.World.GameObject gameObject4 in this.rawGameObjects)
				{
					this.nameCache.Add(gameObject4, gameObject4.GetCachedDisplayNameForSort());
				}
				num = 0;
				if (!PickGameObjectScreen.PreserveOrder)
				{
					this.rawGameObjects.Sort((global::XRL.World.GameObject a, global::XRL.World.GameObject b) => this.nameCache[a].CompareTo(this.nameCache[b]));
				}
				foreach (global::XRL.World.GameObject gameObject5 in this.rawGameObjects)
				{
					this.listItems.Add(PooledFrameworkDataElement<PickGameObjectLineData>.next().set(PickGameObjectLineDataType.Item, PickGameObjectLineDataStyle.Interact, gameObject5, null, false, false, this.getHotkeyChar(num), this.getHotkeyDescription(num)));
					num++;
				}
			}
			IL_038F:
			this.itemScrollerController.BeforeShow(this.listItems);
			if (Reentry)
			{
				int num2 = Math.Max(0, Math.Min(selectedPosition, this.listItems.Count - 1));
				this.itemScrollerController.selectedPosition = num2;
				this.itemScrollerController.scrollContext.SelectIndex(num2);
				this.itemScrollerController.ScrollSelectedIntoView();
			}
			else if (this.sortMode == PickGameObjectScreen.SortMode.Category && this.listItems.Count > 0)
			{
				int num3 = this.listItems.FindIndex((PickGameObjectLineData i) => i.go != null);
				num3 = Math.Max(0, Math.Min(num3, this.listItems.Count - 1));
				this.itemScrollerController.selectedPosition = num3;
				this.itemScrollerController.scrollContext.SelectIndex(num3);
				this.itemScrollerController.ScrollSelectedIntoView();
			}
			foreach (FrameworkUnityScrollChild frameworkUnityScrollChild in this.itemScrollerController.selectionClones)
			{
				frameworkUnityScrollChild.GetComponent<PickGameObjectLine>().screen = this;
			}
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000DCBE8 File Offset: 0x000DADE8
		private char getHotkeyChar(int n)
		{
			if (this.hotkeySpread.Count <= n)
			{
				return '\0';
			}
			return Keyboard.ConvertKeycodeToLowercaseChar(this.hotkeySpread[n]);
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000DCC0C File Offset: 0x000DAE0C
		private string getHotkeyDescription(int n)
		{
			if (this.hotkeySpread.Count <= n)
			{
				return "";
			}
			return Keyboard.ConvertKeycodeToLowercaseChar(this.hotkeySpread[n]).ToString();
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000DCC48 File Offset: 0x000DAE48
		public void BeforeShow(bool Reentry)
		{
			global::XRL.World.Event.PinCurrentPool();
			this.SetupContext();
			this.hotkeySpread.Clear();
			this.hotkeySpread.AddRange(ControlManager.GetHotkeySpread(new List<string> { "Menus", "UINav" }));
			this.itemScrollerController.onSelected.RemoveAllListeners();
			this.itemScrollerController.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectItem));
			this.itemScrollerController.onHighlight.RemoveAllListeners();
			this.itemScrollerController.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightObject));
			this.itemScrollerController.scrollContext.wraps = false;
			this.UpdateMenuBars();
			this.UpdateViewFromData(Reentry);
			FastLayoutRebuilder.RefreshLayoutGroupsImmediateAndRecursiveCached(base.gameObject);
			global::XRL.World.Event.ResetToPin();
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000DCD1C File Offset: 0x000DAF1C
		public IEnumerable<FrameworkDataElement> yieldMenuOptions()
		{
			foreach (MenuOption menuOption in PickGameObjectScreen.defaultMenuOptions)
			{
				yield return menuOption;
			}
			List<MenuOption>.Enumerator enumerator = default(List<MenuOption>.Enumerator);
			if (this.style == PickItem.PickItemDialogStyle.GetItemDialog)
			{
				yield return PickGameObjectScreen.TAKE_ALL;
			}
			if (this.style == PickItem.PickItemDialogStyle.GetItemDialog && PickGameObjectScreen.Actor != null && PickGameObjectScreen.Container != null)
			{
				yield return PickGameObjectScreen.STORE_ITEM;
			}
			if (NavigationController.instance.activeContext is PickGameObjectLine.Context)
			{
				foreach (MenuOption menuOption2 in NavigationController.instance.activeContext.menuOptionDescriptions)
				{
					yield return menuOption2;
				}
				enumerator = default(List<MenuOption>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000DCD2C File Offset: 0x000DAF2C
		public void UpdateMenuBars()
		{
			this.hotkeyBar.BeforeShow(null, this.yieldMenuOptions());
			this.hotkeyBar.GetNavigationContext().disabled = false;
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000DCD88 File Offset: 0x000DAF88
		public async void HandleMenuOption(FrameworkDataElement element)
		{
			if (FrameworkUnityController.instance.DebugContextActivity)
			{
				Debug.LogError("UI:DEBUG TAKE_ALL Initialized");
			}
			if (element == PickGameObjectScreen.TAKE_ALL)
			{
				if (TutorialManager.AllowCommand("Take All"))
				{
					bool doClose = false;
					await APIDispatch.RunAndWaitAsync(delegate
					{
						if (PickItem.TakeAll(PickGameObjectScreen.Actor, PickGameObjectScreen.Container, PickGameObjectScreen.Cell, this.rawGameObjects, ref PickGameObjectScreen.RequestInterfaceExit))
						{
							doClose = true;
							return;
						}
					});
					if (FrameworkUnityController.instance.DebugContextActivity)
					{
						Debug.LogError("UI:DEBUG TAKE_ALL Complete");
					}
					if (doClose)
					{
						this.menucomplete.TrySetResult(default(PickGameObjectScreen.Result));
					}
				}
			}
			else if (element == PickGameObjectScreen.STORE_ITEM)
			{
				this.menucomplete.TrySetResult(new PickGameObjectScreen.Result
				{
					wantToStore = true
				});
			}
			else
			{
				if (element.Id == "Cancel")
				{
					this.Cancel();
				}
				if (element == PickGameObjectScreen.TOGGLE_SORT)
				{
					if (this.sortMode == PickGameObjectScreen.SortMode.AZ)
					{
						this.sortMode = PickGameObjectScreen.SortMode.Category;
					}
					else
					{
						this.sortMode = PickGameObjectScreen.SortMode.AZ;
					}
					this.BeforeShow(false);
				}
			}
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000DCDC8 File Offset: 0x000DAFC8
		public override void Hide()
		{
			this.navigationContext.disabled = true;
			this.listItems.ForEach(delegate(PickGameObjectLineData i)
			{
				i.free();
			});
			this.listItems.Clear();
			base.Hide();
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000DCE1C File Offset: 0x000DB01C
		public void Cancel()
		{
			ControlManager.ResetInput(false, true);
			this.Hide();
			this.menucomplete.TrySetResult(default(PickGameObjectScreen.Result));
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000DCE4B File Offset: 0x000DB04B
		void ControlManager.IControllerChangedEvent.ControllerChanged()
		{
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000DCE50 File Offset: 0x000DB050
		public void HandleVAxis(int? val)
		{
			int? num = val;
			int num2 = 0;
			if ((num.GetValueOrDefault() == num2) & (num != null))
			{
				return;
			}
			PickGameObjectLineData pickGameObjectLineData = this.listItems[this.itemScrollerController.selectedPosition];
			foreach (string text in this.usedCategories)
			{
				Dictionary<string, bool> dictionary = this.categoryCollapsed;
				string text2 = text;
				num = val;
				num2 = 0;
				dictionary[text2] = (num.GetValueOrDefault() < num2) & (num != null);
			}
			this.BeforeShow(false);
			this.ReselectAfterCategoryStateChange(pickGameObjectLineData);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000DCF00 File Offset: 0x000DB100
		public void HandleXAxis(int? val)
		{
			int? num = val;
			int num2 = 0;
			if ((num.GetValueOrDefault() == num2) & (num != null))
			{
				return;
			}
			PickGameObjectLineData pickGameObjectLineData = this.listItems[this.itemScrollerController.selectedPosition];
			if (pickGameObjectLineData.category != null)
			{
				Dictionary<string, bool> dictionary = this.categoryCollapsed;
				string category = pickGameObjectLineData.category;
				num = val;
				num2 = 0;
				dictionary[category] = (num.GetValueOrDefault() < num2) & (num != null);
			}
			this.BeforeShow(false);
			this.ReselectAfterCategoryStateChange(pickGameObjectLineData);
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000DCF7C File Offset: 0x000DB17C
		public void ReselectAfterCategoryStateChange(PickGameObjectLineData item)
		{
			int num = -1;
			if (item.go != null)
			{
				num = this.listItems.FindIndex((PickGameObjectLineData i) => i.go == item.go);
			}
			if (num == -1)
			{
				num = this.listItems.FindIndex((PickGameObjectLineData i) => i.category == item.category);
			}
			num = Math.Max(0, Math.Min(num, this.listItems.Count - 1));
			this.itemScrollerController.selectedPosition = num;
			this.itemScrollerController.ScrollSelectedIntoView();
			this.itemScrollerController.scrollContext.GetContextAt(num).Activate(false);
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000DD022 File Offset: 0x000DB222
		public void BackgroundClicked()
		{
			if (this.navigationContext.IsActive(true))
			{
				this.Cancel();
			}
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000DD038 File Offset: 0x000DB238
		public void HandleSelectItem(FrameworkDataElement element)
		{
			PickGameObjectLineData pickGameObjectLineData = element as PickGameObjectLineData;
			if (pickGameObjectLineData != null)
			{
				if (pickGameObjectLineData.type == PickGameObjectLineDataType.Category)
				{
					this.categoryCollapsed[pickGameObjectLineData.category] = !this.isCollapsed(pickGameObjectLineData.category);
					this.BeforeShow(false);
					return;
				}
				this.menucomplete.TrySetResult(new PickGameObjectScreen.Result
				{
					item = pickGameObjectLineData.go
				});
			}
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000DD0A2 File Offset: 0x000DB2A2
		public void HandleHighlightObject(FrameworkDataElement element)
		{
			PickGameObjectLineData pickGameObjectLineData = element as PickGameObjectLineData;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000DD0AC File Offset: 0x000DB2AC
		public void Update()
		{
			CanvasGroup canvasGroup = base.canvasGroup;
			if (canvasGroup != null && canvasGroup.alpha == (float)1 && GameManager.Instance.CurrentGameView == "PickTarget")
			{
				this.hiddenFromTargetPicker = true;
				base.canvasGroup.alpha = 0f;
			}
			if (this.hiddenFromTargetPicker && GameManager.Instance.CurrentGameView != "PickTarget")
			{
				base.canvasGroup.alpha = 1f;
			}
			if (base.isCurrentWindow() && this.navigationContext.IsActive(true))
			{
				if (this.listItems != null)
				{
					foreach (PickGameObjectLineData pickGameObjectLineData in this.listItems)
					{
						if (ControlManager.isCharDown(pickGameObjectLineData.quickKey))
						{
							this.HandleSelectItem(pickGameObjectLineData);
							return;
						}
					}
				}
				if (this.lastContext != NavigationController.instance.activeContext)
				{
					this.UpdateMenuBars();
					this.lastContext = NavigationController.instance.activeContext;
				}
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000DD1CC File Offset: 0x000DB3CC
		public void MoveItem(PickGameObjectLineData data, int direction)
		{
		}

		// Token: 0x040015E4 RID: 5604
		protected TaskCompletionSource<PickGameObjectScreen.Result> menucomplete = new TaskCompletionSource<PickGameObjectScreen.Result>();

		// Token: 0x040015E5 RID: 5605
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x040015E6 RID: 5606
		public PickGameObjectScreen.Context navigationContext = new PickGameObjectScreen.Context
		{
			buttonHandlers = null
		};

		// Token: 0x040015E7 RID: 5607
		public PickItem.PickItemDialogStyle style;

		// Token: 0x040015E8 RID: 5608
		public List<global::XRL.World.GameObject> rawGameObjects = new List<global::XRL.World.GameObject>();

		// Token: 0x040015E9 RID: 5609
		public List<PickGameObjectLineData> listItems = new List<PickGameObjectLineData>();

		// Token: 0x040015EA RID: 5610
		public FrameworkScroller hotkeyBar;

		// Token: 0x040015EB RID: 5611
		public FrameworkScroller itemScrollerController;

		// Token: 0x040015EC RID: 5612
		public List<string> usedCategories = new List<string>();

		// Token: 0x040015ED RID: 5613
		public Dictionary<string, List<global::XRL.World.GameObject>> objectCategories = new Dictionary<string, List<global::XRL.World.GameObject>>();

		// Token: 0x040015EE RID: 5614
		public Dictionary<string, bool> categoryCollapsed = new Dictionary<string, bool>();

		// Token: 0x040015EF RID: 5615
		public Dictionary<global::XRL.World.GameObject, string> nameCache = new Dictionary<global::XRL.World.GameObject, string>();

		// Token: 0x040015F0 RID: 5616
		public Dictionary<global::XRL.World.GameObject, string> categoryCache = new Dictionary<global::XRL.World.GameObject, string>();

		// Token: 0x040015F1 RID: 5617
		public UITextSkin titleText;

		// Token: 0x040015F2 RID: 5618
		public static bool RequestInterfaceExit;

		// Token: 0x040015F3 RID: 5619
		public static global::XRL.World.GameObject Actor;

		// Token: 0x040015F4 RID: 5620
		public static global::XRL.World.GameObject Container;

		// Token: 0x040015F5 RID: 5621
		public static Cell Cell;

		// Token: 0x040015F6 RID: 5622
		public static bool PreserveOrder;

		// Token: 0x040015F7 RID: 5623
		public static bool ShowContext;

		// Token: 0x040015F8 RID: 5624
		public static bool NotePlayerOwned;

		// Token: 0x040015F9 RID: 5625
		public static List<string> CategoryPriority;

		// Token: 0x040015FA RID: 5626
		private List<global::UnityEngine.KeyCode> hotkeySpread = new List<global::UnityEngine.KeyCode>();

		// Token: 0x040015FB RID: 5627
		public bool DoUpdate;

		// Token: 0x040015FC RID: 5628
		public LayoutGroup targetLayoutGroup;

		// Token: 0x040015FD RID: 5629
		public PickGameObjectScreen.SortMode sortMode = PickGameObjectScreen.SortMode.Category;

		// Token: 0x040015FE RID: 5630
		public static readonly MenuOption TOGGLE_SORT = new MenuOption
		{
			Id = "TOGGLE_SORT",
			KeyDescription = "Toggle Sort",
			InputCommand = "Page Left",
			Description = "toggle sort"
		};

		// Token: 0x040015FF RID: 5631
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
			PickGameObjectScreen.TOGGLE_SORT
		};

		// Token: 0x04001600 RID: 5632
		public static List<MenuOption> getItemMenuOptions = new List<MenuOption>
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
			PickGameObjectScreen.TOGGLE_SORT
		};

		// Token: 0x04001601 RID: 5633
		public static readonly MenuOption TAKE_ALL = new MenuOption
		{
			Id = "TAKE_ALL",
			KeyDescription = "Take All",
			InputCommand = "Take All",
			Description = "take all"
		};

		// Token: 0x04001602 RID: 5634
		public static readonly MenuOption STORE_ITEM = new MenuOption
		{
			Id = "STORE_ITEMS",
			KeyDescription = "Store Items",
			InputCommand = "Store Items",
			Description = "store an item"
		};

		// Token: 0x04001603 RID: 5635
		private NavigationContext lastContext;

		// Token: 0x04001604 RID: 5636
		private bool hiddenFromTargetPicker;

		// Token: 0x02001862 RID: 6242
		public class Context : NavigationContext
		{
		}

		// Token: 0x02001863 RID: 6243
		public struct Result
		{
			// Token: 0x04006228 RID: 25128
			public global::XRL.World.GameObject item;

			// Token: 0x04006229 RID: 25129
			public bool wantToStore;

			// Token: 0x0400622A RID: 25130
			public bool requestInterfaceExit;
		}

		// Token: 0x02001864 RID: 6244
		public enum SortMode
		{
			// Token: 0x0400622C RID: 25132
			AZ,
			// Token: 0x0400622D RID: 25133
			Category
		}
	}
}
