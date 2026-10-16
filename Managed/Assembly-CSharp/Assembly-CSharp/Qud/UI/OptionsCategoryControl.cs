using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000445 RID: 1093
	[ExecuteInEditMode]
	public class OptionsCategoryControl : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x0600298B RID: 10635 RVA: 0x000D96F0 File Offset: 0x000D78F0
		public void SetupContexts(ScrollChildContext scontext)
		{
			if (this.context != null && this.context.IsActive(true))
			{
				ScrollChildContext scrollChildContext = this.context.parentContext as ScrollChildContext;
				if (scrollChildContext != null && scrollChildContext.index != scontext.index)
				{
					this.context = new OptionsCategoryControl.Context();
					goto IL_007C;
				}
			}
			OptionsCategoryControl.Context context = NavigationController.instance.activeContext as OptionsCategoryControl.Context;
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
				OptionsCategoryControl.Context context2;
				if ((context2 = this.context) == null)
				{
					context2 = (this.context = new OptionsCategoryControl.Context());
				}
				scontext.proxyTo = context2;
				OptionsCategoryControl.Context context3 = this.context;
				if (context3.menuOptionDescriptions == null)
				{
					context3.menuOptionDescriptions = new List<MenuOption> { OptionsCategoryControl.TOGGLE_OPTION };
				}
				this.context.parentContext = scontext;
			}
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000D97CE File Offset: 0x000D79CE
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000D97D8 File Offset: 0x000D79D8
		public void setData(FrameworkDataElement data)
		{
			OptionsCategoryRow optionsCategoryRow = data as OptionsCategoryRow;
			if (optionsCategoryRow != null)
			{
				this.data = optionsCategoryRow;
				this.Render();
				return;
			}
			this.data = null;
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000D9804 File Offset: 0x000D7A04
		public void Render()
		{
			this.title.SetText("{{C|" + this.data.Title.ToUpper() + "}}");
			if (this.data.categoryExpanded)
			{
				this.categoryExpander.SetText("{{C|[-]}}");
				return;
			}
			this.categoryExpander.SetText("{{C|[+]}}");
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000D986C File Offset: 0x000D7A6C
		public void Update()
		{
			OptionsCategoryRow optionsCategoryRow = this.data;
			if (optionsCategoryRow != null && optionsCategoryRow.ValueChangedSinceLastObserved(this))
			{
				this.Render();
			}
		}

		// Token: 0x04001570 RID: 5488
		public OptionsCategoryRow data;

		// Token: 0x04001571 RID: 5489
		public UITextSkin categoryExpander;

		// Token: 0x04001572 RID: 5490
		public UITextSkin title;

		// Token: 0x04001573 RID: 5491
		private OptionsCategoryControl.Context context;

		// Token: 0x04001574 RID: 5492
		public bool selectedMode;

		// Token: 0x04001575 RID: 5493
		public static MenuOption TOGGLE_OPTION = new MenuOption
		{
			InputCommand = "Accept",
			Description = "Toggle Visibilty",
			disabled = true
		};

		// Token: 0x0200184F RID: 6223
		private class Context : NavigationContext
		{
		}
	}
}
