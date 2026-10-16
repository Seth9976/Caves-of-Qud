using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleLib.Console;
using UnityEngine.Events;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Parts;

namespace Qud.UI
{
	// Token: 0x02000432 RID: 1074
	[UIView("Book", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "Book", UICanvasHost = 1)]
	public class BookScreen : SingletonWindowBase<BookScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x060028AB RID: 10411 RVA: 0x000D2CEA File Offset: 0x000D0EEA
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
			this.BeforeShow();
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000D2D10 File Offset: 0x000D0F10
		public void SetupContext()
		{
			this.vertNav.parentContext = this.navigationContext;
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Clear();
			this.vertNav.contexts.Add(this.horizNav);
			this.horizNav.parentContext = this.vertNav;
			this.horizNav.contexts.Clear();
			this.horizNav.contexts.Add(this.pageControllers[0].scrollContext);
			this.horizNav.contexts.Add(this.pageControllers[1].scrollContext);
			this.horizNav.SetAxis(InputAxisTypes.NavigationXAxis, false);
			BookScreen.Context context = this.navigationContext;
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
						BookScreen.PREV_PAGE.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(BookScreen.PREV_PAGE);
						})
					},
					{
						BookScreen.NEXT_PAGE.InputCommand,
						global::XRL.UI.Framework.Event.Helpers.Handle(delegate
						{
							this.HandleMenuOption(BookScreen.NEXT_PAGE);
						})
					}
				};
			}
			this.vertNav.Setup();
			this.horizNav.wraps = true;
			this.horizNav.Setup();
			this.vertNav.wraps = true;
			this.vertNav.Setup();
			this.pageControllers[0].scrollContext.parentContext = this.horizNav;
			this.pageControllers[1].scrollContext.parentContext = this.horizNav;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x000D2EC0 File Offset: 0x000D10C0
		public static string sortModeDescription
		{
			get
			{
				return "sort: " + Markup.Color((SingletonWindowBase<BookScreen>.instance.sortMode == BookScreen.SortMode.AZ) ? "w" : "y", "a-z") + "/" + Markup.Color((SingletonWindowBase<BookScreen>.instance.sortMode == BookScreen.SortMode.Category) ? "w" : "y", "by class");
			}
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x000D2F24 File Offset: 0x000D1124
		protected async Task<bool> showScreen(MarkovBook Book, string Sound = "Sounds/Interact/sfx_interact_book_read", Action<int> onShowPage = null, Action<int> afterShowPage = null)
		{
			this.BookID = null;
			this.Book = Book;
			this.onShowPage = onShowPage;
			this.afterShowPage = afterShowPage;
			this.searchText = "";
			this.menucomplete.TrySetCanceled();
			this.menucomplete = new TaskCompletionSource<bool>();
			MinEvent.UIHold = true;
			GameManager.Instance.PushGameView("Book", true);
			SoundManager.PlaySound(Sound, 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			bool flag2;
			try
			{
				await The.UiContext;
				this.BeforeShow();
				this.Show();
				this.titleText.SetText(Book.Title);
				await this.CheckPageCallbacks();
				this.pageControllers[0].scrollContext.ActivateAndEnable();
				bool flag = await this.menucomplete.Task;
				this.Cleanup();
				flag2 = flag;
			}
			finally
			{
				GameManager.Instance.PopGameView(true);
				MinEvent.UIHold = false;
			}
			return flag2;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000D2F88 File Offset: 0x000D1188
		protected async Task<bool> showScreen(string BookID, string Sound = "Sounds/Interact/sfx_interact_book_read", Action<int> onShowPage = null, Action<int> afterShowPage = null)
		{
			this.BookID = BookID;
			this.Book = null;
			this.onShowPage = onShowPage;
			this.afterShowPage = afterShowPage;
			this.searchText = "";
			this.menucomplete.TrySetCanceled();
			this.menucomplete = new TaskCompletionSource<bool>();
			MinEvent.UIHold = true;
			GameManager.Instance.PushGameView("Book", true);
			SoundManager.PlaySound(Sound, 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			bool flag2;
			try
			{
				await The.UiContext;
				this.BeforeShow();
				this.Show();
				this.titleText.SetText(BookUI.Books[BookID].Title);
				await this.CheckPageCallbacks();
				this.pageControllers[0].scrollContext.ActivateAndEnable();
				bool flag = await this.menucomplete.Task;
				this.Cleanup();
				flag2 = flag;
			}
			finally
			{
				GameManager.Instance.PopGameView(true);
				MinEvent.UIHold = false;
			}
			return flag2;
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000D2FEC File Offset: 0x000D11EC
		public static Task<bool> show(string BookID, string Sound = "Sounds/Interact/sfx_interact_book_read", Action<int> OnShowPage = null, Action<int> AfterShowPage = null)
		{
			return NavigationController.instance.SuspendContextWhile<bool>(() => SingletonWindowBase<BookScreen>.instance.showScreen(BookID, Sound, OnShowPage, AfterShowPage));
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000D3034 File Offset: 0x000D1234
		public static Task<bool> show(MarkovBook Book, string Sound = "Sounds/Interact/sfx_interact_book_read", Action<int> OnShowPage = null, Action<int> AfterShowPage = null)
		{
			return NavigationController.instance.SuspendContextWhile<bool>(() => SingletonWindowBase<BookScreen>.instance.showScreen(Book, Sound, OnShowPage, AfterShowPage));
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000D307C File Offset: 0x000D127C
		public void BeforeShow()
		{
			global::XRL.World.Event.PinCurrentPool();
			this.SetupContext();
			ControlManager.GetHotkeySpread(new List<string> { "Menus", "UINav", "Trade", "UI" });
			this.listItems.ForEach(delegate(BookLineData i)
			{
				i.free();
			});
			this.listItems.Clear();
			this.pageControllers[0].onSelected.RemoveAllListeners();
			this.pageControllers[0].onHighlight.RemoveAllListeners();
			this.pageControllers[0].scrollContext.wraps = false;
			this.UpdateMenuBars();
			this.CurrentPage = 0;
			this.UpdateViewFromData();
			global::XRL.World.Event.ResetToPin();
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000D3150 File Offset: 0x000D1350
		public string RenderCurrentPage()
		{
			if (this.Book != null)
			{
				return this.Book.Pages[this.CurrentPage].RenderForModernUI;
			}
			if (this.BookID != null)
			{
				if (!BookUI.Books.ContainsKey(this.BookID))
				{
					BookUI.RenderDynamicBook(this.BookID);
				}
				return BookUI.Books[this.BookID].Pages[this.CurrentPage].RenderForModernUI;
			}
			return "<BookID is null>";
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000D31D4 File Offset: 0x000D13D4
		private void UpdateViewFromData()
		{
			this.pageControllers[0].BeforeShow(new List<BookLineData> { PooledFrameworkDataElement<BookLineData>.next().set(this.RenderCurrentPage()) });
			this.leftPageNumber.SetText((this.CurrentPage + 1).ToString());
			this.rightPageNumber.SetText(this.PageCount.ToString());
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000D3240 File Offset: 0x000D1440
		private async Task CheckPageCallbacks()
		{
			if (this.onShowPage != null)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					this.onShowPage(this.CurrentPage);
				});
			}
			if (this.afterShowPage != null)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					this.afterShowPage(this.CurrentPage);
				});
			}
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000D3283 File Offset: 0x000D1483
		public IEnumerable<FrameworkDataElement> yieldMenuOptions()
		{
			foreach (MenuOption menuOption in BookScreen.getItemMenuOptions)
			{
				yield return menuOption;
			}
			List<MenuOption>.Enumerator enumerator = default(List<MenuOption>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000D328C File Offset: 0x000D148C
		public void UpdateMenuBars()
		{
			this.hotkeyBar.BeforeShow(null, this.yieldMenuOptions());
			this.hotkeyBar.GetNavigationContext().disabled = false;
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000D32E8 File Offset: 0x000D14E8
		private int PageCount
		{
			get
			{
				if (this.Book != null)
				{
					return this.Book.Pages.Count;
				}
				return BookUI.Books[this.BookID].Pages.Count;
			}
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000D3320 File Offset: 0x000D1520
		public async void HandleMenuOption(FrameworkDataElement element)
		{
			if (element.Id == "Cancel")
			{
				this.Cancel();
			}
			if (element == BookScreen.NEXT_PAGE)
			{
				if (this.CurrentPage < this.PageCount - 1)
				{
					this.CurrentPage++;
					this.UpdateViewFromData();
					SoundManager.PlayUISound("Sounds/Interact/sfx_interact_book_pageTurn", 1f, false, false, SoundRequest.SoundEffectType.None);
					await this.CheckPageCallbacks();
				}
			}
			else if (element == BookScreen.PREV_PAGE && this.CurrentPage > 0)
			{
				this.CurrentPage--;
				this.UpdateViewFromData();
				SoundManager.PlayUISound("Sounds/Interact/sfx_interact_book_pageTurn", 1f, false, false, SoundRequest.SoundEffectType.None);
				await this.CheckPageCallbacks();
			}
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000D335F File Offset: 0x000D155F
		public void Cleanup()
		{
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000D3361 File Offset: 0x000D1561
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000D3369 File Offset: 0x000D1569
		public void Cancel()
		{
			ControlManager.ResetInput(false, true);
			this.Hide();
			this.Cleanup();
			this.menucomplete.TrySetResult(true);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000D338B File Offset: 0x000D158B
		void ControlManager.IControllerChangedEvent.ControllerChanged()
		{
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000D338D File Offset: 0x000D158D
		public void Update()
		{
			if (base.isCurrentWindow() && this.navigationContext.IsActive(true) && this.lastContext != NavigationController.instance.activeContext)
			{
				this.lastContext = NavigationController.instance.activeContext;
				this.UpdateMenuBars();
			}
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000D33CD File Offset: 0x000D15CD
		public IEnumerator ShowNextCycle()
		{
			yield return 0;
			yield return 0;
			this.BeforeShow();
			yield break;
		}

		// Token: 0x04001459 RID: 5209
		public const string DEFAULT_OPEN_SOUND = "Sounds/Interact/sfx_interact_book_read";

		// Token: 0x0400145A RID: 5210
		protected TaskCompletionSource<bool> menucomplete = new TaskCompletionSource<bool>();

		// Token: 0x0400145B RID: 5211
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x0400145C RID: 5212
		public ScrollContext<NavigationContext> horizNav = new ScrollContext<NavigationContext>();

		// Token: 0x0400145D RID: 5213
		public FrameworkScroller[] pageControllers;

		// Token: 0x0400145E RID: 5214
		public BookScreen.Context navigationContext = new BookScreen.Context
		{
			buttonHandlers = null
		};

		// Token: 0x0400145F RID: 5215
		public UITextSkin titleText;

		// Token: 0x04001460 RID: 5216
		public FrameworkScroller hotkeyBar;

		// Token: 0x04001461 RID: 5217
		public UITextSkin hotkeyText;

		// Token: 0x04001462 RID: 5218
		public UITextSkin leftPageNumber;

		// Token: 0x04001463 RID: 5219
		public UITextSkin rightPageNumber;

		// Token: 0x04001464 RID: 5220
		private string searchText = "";

		// Token: 0x04001465 RID: 5221
		public MarkovBook Book;

		// Token: 0x04001466 RID: 5222
		public string BookID;

		// Token: 0x04001467 RID: 5223
		public Action<int> onShowPage;

		// Token: 0x04001468 RID: 5224
		public Action<int> afterShowPage;

		// Token: 0x04001469 RID: 5225
		public UIThreeColorProperties[] traderIcons;

		// Token: 0x0400146A RID: 5226
		public UITextSkin[] traderNames;

		// Token: 0x0400146B RID: 5227
		private TradeEntry searcher = new TradeEntry("");

		// Token: 0x0400146C RID: 5228
		private List<BookLineData> listItems = new List<BookLineData>();

		// Token: 0x0400146D RID: 5229
		private int CurrentPage;

		// Token: 0x0400146E RID: 5230
		public BookScreen.SortMode sortMode = BookScreen.SortMode.Category;

		// Token: 0x0400146F RID: 5231
		public static readonly MenuOption PREV_PAGE = new MenuOption
		{
			Id = "PREV_PAGE",
			KeyDescription = "Previous Page",
			InputCommand = "UI:Navigate/left",
			Description = "Previous Page"
		};

		// Token: 0x04001470 RID: 5232
		public static readonly MenuOption NEXT_PAGE = new MenuOption
		{
			Id = "NEXT_PAGE",
			KeyDescription = "Next Page",
			InputCommand = "UI:Navigate/right",
			Description = "Next Page"
		};

		// Token: 0x04001471 RID: 5233
		public static List<MenuOption> getItemMenuOptions = new List<MenuOption>
		{
			BookScreen.PREV_PAGE,
			BookScreen.NEXT_PAGE,
			new MenuOption
			{
				Id = "Cancel",
				InputCommand = "Cancel",
				Description = "Close book"
			}
		};

		// Token: 0x04001472 RID: 5234
		public UITextSkin[] freeDramsLabels;

		// Token: 0x04001473 RID: 5235
		public UITextSkin[] totalLabels;

		// Token: 0x04001474 RID: 5236
		private NavigationContext lastContext;

		// Token: 0x02001803 RID: 6147
		public class Context : NavigationContext
		{
		}

		// Token: 0x02001804 RID: 6148
		public enum SortMode
		{
			// Token: 0x040060AC RID: 24748
			AZ,
			// Token: 0x040060AD RID: 24749
			Category
		}
	}
}
