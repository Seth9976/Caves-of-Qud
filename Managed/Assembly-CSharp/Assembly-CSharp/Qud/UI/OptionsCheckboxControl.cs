using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000446 RID: 1094
	[ExecuteInEditMode]
	public class OptionsCheckboxControl : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x06002992 RID: 10642 RVA: 0x000D98BC File Offset: 0x000D7ABC
		public void SetupContexts(ScrollChildContext scontext)
		{
			if (this.context != null && this.context.IsActive(true))
			{
				ScrollChildContext scrollChildContext = this.context.parentContext as ScrollChildContext;
				if (scrollChildContext != null && scrollChildContext.index != scontext.index)
				{
					this.context = new OptionsCheckboxControl.Context();
					goto IL_007C;
				}
			}
			OptionsCheckboxControl.Context context = NavigationController.instance.activeContext as OptionsCheckboxControl.Context;
			if (context != null)
			{
				ScrollChildContext scrollChildContext2 = context.parentContext as ScrollChildContext;
				if (scrollChildContext2 != null && scrollChildContext2.index == scontext.index)
				{
					this.context = context;
				}
			}
			IL_007C:
			if (scontext != null)
			{
				OptionsCheckboxControl.Context context2;
				if ((context2 = this.context) == null)
				{
					context2 = (this.context = new OptionsCheckboxControl.Context());
				}
				scontext.proxyTo = context2;
				this.context.parentContext = scontext;
				OptionsCheckboxControl.Context context3 = this.context;
				if (context3.menuOptionDescriptions == null)
				{
					context3.menuOptionDescriptions = new List<MenuOption> { OptionsCheckboxControl.TOGGLE_OPTION };
				}
				context3 = this.context;
				if (context3.buttonHandlers == null)
				{
					context3.buttonHandlers = new Dictionary<InputButtonTypes, Action> { 
					{
						InputButtonTypes.AcceptButton,
						new Action(this.DoToggle)
					} };
				}
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000D99C8 File Offset: 0x000D7BC8
		public void DoToggle()
		{
			this.data.Value = !this.data.Value;
			this.Render();
			Options.SetOption(this.data.Id, this.data.Value);
			NavigationController.currentEvent.Handle();
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000D9A19 File Offset: 0x000D7C19
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000D9A24 File Offset: 0x000D7C24
		public void setData(FrameworkDataElement data)
		{
			OptionsCheckboxRow optionsCheckboxRow = data as OptionsCheckboxRow;
			if (optionsCheckboxRow != null)
			{
				this.data = optionsCheckboxRow;
				this.Render();
				return;
			}
			this.data = null;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000D9A50 File Offset: 0x000D7C50
		public void Render()
		{
			bool? flag = this.wasSelected;
			bool flag2 = true;
			if ((flag.GetValueOrDefault() == flag2) & (flag != null))
			{
				this.text.SetText((this.data.Value ? "[{{W|■}}] " : "[ ] ") + "{{W|" + this.data.Title + "}}");
			}
			else
			{
				this.text.SetText((this.data.Value ? "[■] " : "[ ] ") + this.data.Title);
			}
			this.data.ValueChangedSinceLastObserved(this);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000D9B00 File Offset: 0x000D7D00
		public void Update()
		{
			if (this.context != null)
			{
				OptionsCheckboxRow optionsCheckboxRow = this.data;
				if (optionsCheckboxRow == null || !optionsCheckboxRow.ValueChangedSinceLastObserved(this))
				{
					bool? flag = this.wasSelected;
					bool flag2 = this.context.IsActive(true);
					if ((flag.GetValueOrDefault() == flag2) & (flag != null))
					{
						return;
					}
				}
				this.wasSelected = new bool?(this.context.IsActive(true));
				this.Render();
			}
		}

		// Token: 0x04001576 RID: 5494
		public FrameworkContext frameworkContext;

		// Token: 0x04001577 RID: 5495
		public OptionsCheckboxRow data;

		// Token: 0x04001578 RID: 5496
		public UITextSkin text;

		// Token: 0x04001579 RID: 5497
		public bool selectedMode;

		// Token: 0x0400157A RID: 5498
		private OptionsCheckboxControl.Context context;

		// Token: 0x0400157B RID: 5499
		public static MenuOption TOGGLE_OPTION = new MenuOption
		{
			InputCommand = "Accept",
			Description = "Toggle Option",
			disabled = true
		};

		// Token: 0x0400157C RID: 5500
		protected const string EMPTY_CHECK = "[ ] ";

		// Token: 0x0400157D RID: 5501
		protected const string CHECKED = "[■] ";

		// Token: 0x0400157E RID: 5502
		protected const string CHECKED_ACTIVE = "[{{W|■}}] ";

		// Token: 0x0400157F RID: 5503
		private bool? wasSelected;

		// Token: 0x02001850 RID: 6224
		private class Context : NavigationContext
		{
		}
	}
}
