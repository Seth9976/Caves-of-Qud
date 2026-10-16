using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Events;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x0200042F RID: 1071
	[UIView("PopupAskNumberScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "AskNumber", UICanvasHost = 1)]
	public class AskNumberScreen : SingletonWindowBase<AskNumberScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x06002888 RID: 10376 RVA: 0x000D2634 File Offset: 0x000D0834
		public void SetupContext()
		{
			AskNumberScreen.Context context = this.navigationContext;
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
						"Cancel",
						global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Cancel))
					},
					{
						"Accept",
						global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.Accept))
					}
				};
			}
			context = this.navigationContext;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			this.navigationContext.axisHandlers.Set(InputAxisTypes.NavigationXAxis, global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.DoPageUp), new Action(this.DoPageDown), null)));
			this.navigationContext.axisHandlers.Set(InputAxisTypes.NavigationYAxis, global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.DoScrollDown), new Action(this.DoScrollUp), null)));
			this.navigationContext.axisHandlers.Set(InputAxisTypes.NavigationPageYAxis, global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.DoPageDown), new Action(this.DoPageUp), null)));
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000D277B File Offset: 0x000D097B
		public void DoScrollUp()
		{
			this.Current++;
			this.UpdateViewFromData();
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000D2791 File Offset: 0x000D0991
		public void DoScrollDown()
		{
			this.Current--;
			this.UpdateViewFromData();
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000D27A7 File Offset: 0x000D09A7
		public void DoPageDown()
		{
			this.Current = this.Min;
			this.UpdateViewFromData();
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000D27BB File Offset: 0x000D09BB
		public void DoPageUp()
		{
			this.Current = this.Max;
			this.UpdateViewFromData();
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000D27D0 File Offset: 0x000D09D0
		protected async Task<int?> showScreen(string Message, int Start = 0, int Min = 0, int Max = 2147483647)
		{
			int? num2;
			try
			{
				await The.UiContext;
				this.Current = Start;
				this.messageText.SetText(Message);
				this.numberText.SetText(Start.ToString());
				this.Min = Min;
				this.Max = Max;
				this.menucomplete.TrySetCanceled();
				this.menucomplete = new TaskCompletionSource<int?>();
				MinEvent.UIHold = true;
				this.BeforeShow();
				this.Show();
				int? num = await this.menucomplete.Task;
				this.Cleanup();
				num2 = num;
			}
			finally
			{
				MinEvent.UIHold = false;
			}
			return num2;
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000D2834 File Offset: 0x000D0A34
		public static async Task<int?> show(string Message, int Start = 0, int Min = 0, int Max = 2147483647)
		{
			return await NavigationController.instance.SuspendContextWhile<int?>(() => SingletonWindowBase<AskNumberScreen>.instance.showScreen(Message, Start, Min, Max));
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000D288F File Offset: 0x000D0A8F
		public void BeforeShow()
		{
			global::XRL.World.Event.PinCurrentPool();
			this.SetupContext();
			this.UpdateMenuBars();
			this.UpdateTitleBars();
			this.UpdateViewFromData();
			this.navigationContext.ActivateAndEnable();
			global::XRL.World.Event.ResetToPin();
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000D28C0 File Offset: 0x000D0AC0
		private void UpdateViewFromData()
		{
			if (this.Current < this.Min)
			{
				this.Current = this.Min;
			}
			if (this.Current > this.Max)
			{
				this.Current = this.Max;
			}
			this.numberText.SetText(this.Current.ToString());
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000D2918 File Offset: 0x000D0B18
		public void UpdateTitleBars()
		{
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000D291A File Offset: 0x000D0B1A
		public IEnumerable<FrameworkDataElement> yieldMenuOptions()
		{
			foreach (MenuOption menuOption in AskNumberScreen.getItemMenuOptions)
			{
				yield return menuOption;
			}
			List<MenuOption>.Enumerator enumerator = default(List<MenuOption>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000D2924 File Offset: 0x000D0B24
		public void UpdateMenuBars()
		{
			this.hotkeyBar.BeforeShow(null, this.yieldMenuOptions());
			this.hotkeyBar.GetNavigationContext().disabled = false;
			this.hotkeyBar.onSelected.RemoveAllListeners();
			this.hotkeyBar.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleMenuOption));
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000D2980 File Offset: 0x000D0B80
		public void HandleMenuOption(FrameworkDataElement element)
		{
			if (element.Id == "Accept")
			{
				this.Accept();
			}
			if (element.Id == "Cancel")
			{
				this.Cancel();
			}
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000D29B2 File Offset: 0x000D0BB2
		public void Cleanup()
		{
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000D29B4 File Offset: 0x000D0BB4
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000D29BC File Offset: 0x000D0BBC
		public void Accept()
		{
			int num = Convert.ToInt32(this.numberText.text);
			ControlManager.ResetInput(false, true);
			this.Hide();
			this.Cleanup();
			this.menucomplete.TrySetResult(new int?(num));
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000D2A00 File Offset: 0x000D0C00
		public void Cancel()
		{
			ControlManager.ResetInput(false, true);
			this.Hide();
			this.Cleanup();
			this.menucomplete.TrySetResult(null);
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000D2A35 File Offset: 0x000D0C35
		void ControlManager.IControllerChangedEvent.ControllerChanged()
		{
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000D2A37 File Offset: 0x000D0C37
		public void HandleVAxis(int? val)
		{
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000D2A39 File Offset: 0x000D0C39
		public void HandleXAxis(int? val)
		{
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000D2A3B File Offset: 0x000D0C3B
		public void HandleHighlightObject(FrameworkDataElement element)
		{
			TradeLineData tradeLineData = element as TradeLineData;
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000D2A44 File Offset: 0x000D0C44
		public void Update()
		{
			if (base.isCurrentWindow() && this.navigationContext.IsActive(true) && this.lastContext != NavigationController.instance.activeContext)
			{
				this.lastContext = NavigationController.instance.activeContext;
				this.UpdateMenuBars();
			}
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000D2A84 File Offset: 0x000D0C84
		public void MoveItem(TradeLineData data, int direction)
		{
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000D2A86 File Offset: 0x000D0C86
		public IEnumerator ShowNextCycle()
		{
			yield return 0;
			yield return 0;
			this.BeforeShow();
			yield break;
		}

		// Token: 0x04001448 RID: 5192
		protected TaskCompletionSource<int?> menucomplete = new TaskCompletionSource<int?>();

		// Token: 0x04001449 RID: 5193
		public AskNumberScreen.Context navigationContext = new AskNumberScreen.Context();

		// Token: 0x0400144A RID: 5194
		public FrameworkScroller hotkeyBar;

		// Token: 0x0400144B RID: 5195
		public UITextSkin numberText;

		// Token: 0x0400144C RID: 5196
		public UITextSkin messageText;

		// Token: 0x0400144D RID: 5197
		public int Current;

		// Token: 0x0400144E RID: 5198
		public int Min;

		// Token: 0x0400144F RID: 5199
		public int Max;

		// Token: 0x04001450 RID: 5200
		public static List<MenuOption> getItemMenuOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Accept"
			},
			new MenuOption
			{
				Id = "Cancel",
				InputCommand = "Cancel",
				Description = "Cancel"
			}
		};

		// Token: 0x04001451 RID: 5201
		public UITextSkin[] freeDramsLabels;

		// Token: 0x04001452 RID: 5202
		public UITextSkin[] totalLabels;

		// Token: 0x04001453 RID: 5203
		private NavigationContext lastContext;

		// Token: 0x020017FC RID: 6140
		public class Context : NavigationContext
		{
		}
	}
}
