using System;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000444 RID: 1092
	[ExecuteInEditMode]
	public class OptionsButtonControl : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x06002985 RID: 10629 RVA: 0x000D952C File Offset: 0x000D772C
		public void SetupContexts(ScrollChildContext scontext)
		{
			if (this.context != null && this.context.IsActive(true))
			{
				ScrollChildContext scrollChildContext = this.context.parentContext as ScrollChildContext;
				if (scrollChildContext != null && scrollChildContext.index != scontext.index)
				{
					this.context = new OptionsButtonControl.Context();
					goto IL_007C;
				}
			}
			OptionsButtonControl.Context context = NavigationController.instance.activeContext as OptionsButtonControl.Context;
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
				OptionsButtonControl.Context context2;
				if ((context2 = this.context) == null)
				{
					context2 = (this.context = new OptionsButtonControl.Context());
				}
				scontext.proxyTo = context2;
				this.context.parentContext = scontext;
			}
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000D95E2 File Offset: 0x000D77E2
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000D95EC File Offset: 0x000D77EC
		public void setData(FrameworkDataElement data)
		{
			OptionsMenuButtonRow optionsMenuButtonRow = data as OptionsMenuButtonRow;
			if (optionsMenuButtonRow != null)
			{
				this.data = optionsMenuButtonRow;
				this.Render();
				return;
			}
			this.data = null;
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000D9618 File Offset: 0x000D7818
		public void Render()
		{
			bool? flag = this.wasSelected;
			bool flag2 = true;
			if ((flag.GetValueOrDefault() == flag2) & (flag != null))
			{
				this.text.SetText("{{W|" + this.data.Title + "}}");
				return;
			}
			this.text.SetText(this.data.Title);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000D9680 File Offset: 0x000D7880
		public void Update()
		{
			OptionsMenuButtonRow optionsMenuButtonRow = this.data;
			if (optionsMenuButtonRow == null || !optionsMenuButtonRow.ValueChangedSinceLastObserved(this))
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

		// Token: 0x0400156A RID: 5482
		public FrameworkContext frameworkContext;

		// Token: 0x0400156B RID: 5483
		public OptionsMenuButtonRow data;

		// Token: 0x0400156C RID: 5484
		public UITextSkin text;

		// Token: 0x0400156D RID: 5485
		public bool selectedMode;

		// Token: 0x0400156E RID: 5486
		private OptionsButtonControl.Context context;

		// Token: 0x0400156F RID: 5487
		private bool? wasSelected;

		// Token: 0x0200184E RID: 6222
		private class Context : NavigationContext
		{
		}
	}
}
