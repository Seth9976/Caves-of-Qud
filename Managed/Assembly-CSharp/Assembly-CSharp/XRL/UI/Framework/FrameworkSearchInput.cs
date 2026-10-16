using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x02001512 RID: 5394
	public class FrameworkSearchInput : FrameworkContext
	{
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600D22C RID: 53804 RVA: 0x004E080E File Offset: 0x004DEA0E
		// (set) Token: 0x0600D22D RID: 53805 RVA: 0x004E081B File Offset: 0x004DEA1B
		public new FrameworkSearchInput.SearchContext context
		{
			get
			{
				return base.context as FrameworkSearchInput.SearchContext;
			}
			set
			{
				base.context = value;
			}
		}

		// Token: 0x0600D22E RID: 53806 RVA: 0x004E0824 File Offset: 0x004DEA24
		public void Awake()
		{
			this.context = new FrameworkSearchInput.SearchContext
			{
				buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
				{
					InputButtonTypes.AcceptButton,
					Event.Helpers.Handle(new Action(this.ChangeValue))
				} }
			};
		}

		// Token: 0x0600D22F RID: 53807 RVA: 0x004E0854 File Offset: 0x004DEA54
		public void EnterAndOpen()
		{
			this.context.Activate(false);
			NavigationController.instance.FireInputButtonEvent(InputButtonTypes.AcceptButton, null);
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600D230 RID: 53808 RVA: 0x004E0870 File Offset: 0x004DEA70
		public string SearchText
		{
			get
			{
				return this.context.inputText;
			}
		}

		// Token: 0x0600D231 RID: 53809 RVA: 0x004E0880 File Offset: 0x004DEA80
		public void Update()
		{
			bool flag = this.context.IsActive(true);
			bool flag2 = flag;
			bool? flag3 = this.isActive;
			if (!((flag2 == flag3.GetValueOrDefault()) & (flag3 != null)) || this.context.inputText != this.lastText)
			{
				this.isActive = new bool?(flag);
				this.lastText = this.context.inputText;
				Color color = (flag ? ConsoleLib.Console.ColorUtility.colorFromChar('W') : ConsoleLib.Console.ColorUtility.FromWebColor("4A757E"));
				this.InputBorder.color = (this.SearchIcon.color = color);
				if (string.IsNullOrWhiteSpace(this.lastText))
				{
					this.InputText.SetText("{{K|<search>}}");
					return;
				}
				this.InputText.SetText(this.lastText);
			}
		}

		// Token: 0x0600D232 RID: 53810 RVA: 0x004E094C File Offset: 0x004DEB4C
		public async void ChangeValue()
		{
			string text = await Popup.AskStringAsync(this.PopupTitle, this.context.inputText ?? "", 80, 0, null, true, true, new bool?(false), false, null);
			FrameworkSearchInput.SearchContext context = this.context;
			string text2;
			if ((text2 = text) == null)
			{
				text2 = this.context.inputText ?? "";
			}
			context.inputText = text2;
			this.OnSearchTextChange.Invoke(this.context.inputText);
		}

		// Token: 0x04005348 RID: 21320
		public Image SearchIcon;

		// Token: 0x04005349 RID: 21321
		public Image InputBorder;

		// Token: 0x0400534A RID: 21322
		public UITextSkin InputText;

		// Token: 0x0400534B RID: 21323
		public UnityEvent<string> OnSearchTextChange = new UnityEvent<string>();

		// Token: 0x0400534C RID: 21324
		private bool? isActive;

		// Token: 0x0400534D RID: 21325
		private string lastText;

		// Token: 0x0400534E RID: 21326
		public string PopupTitle = "Enter search text";

		// Token: 0x02001E21 RID: 7713
		public class SearchContext : ScrollChildContext
		{
			// Token: 0x040075A0 RID: 30112
			public string inputText;
		}
	}
}
