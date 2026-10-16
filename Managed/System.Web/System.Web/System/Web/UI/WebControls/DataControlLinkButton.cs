using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000369 RID: 873
	internal class DataControlLinkButton : LinkButton, IDataControlButton, IButtonControl
	{
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x000520B6 File Offset: 0x000502B6
		// (set) Token: 0x060020CE RID: 8398 RVA: 0x000520BE File Offset: 0x000502BE
		public Control Container
		{
			get
			{
				return this._container;
			}
			set
			{
				this._container = value;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x0000EE63 File Offset: 0x0000D063
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x00003946 File Offset: 0x00001B46
		public string ImageUrl
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00052011 File Offset: 0x00050211
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x00052024 File Offset: 0x00050224
		public bool AllowCallback
		{
			get
			{
				return this.ViewState.GetBool("AllowCallback", true);
			}
			set
			{
				this.ViewState["AllowCallback"] = value;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0003392E File Offset: 0x00031B2E
		public ButtonType ButtonType
		{
			get
			{
				return ButtonType.Link;
			}
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000520C8 File Offset: 0x000502C8
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.EnsureForeColor();
			if (this.AllowCallback)
			{
				ICallbackContainer callbackContainer = this.Container as ICallbackContainer;
				if (callbackContainer != null)
				{
					this.OnClientClick = ClientScriptManager.EnsureEndsWithSemicolon(this.OnClientClick) + callbackContainer.GetCallbackScript(this, base.CommandName + "$" + base.CommandArgument);
				}
			}
			base.Render(writer);
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x0005212C File Offset: 0x0005032C
		private void EnsureForeColor()
		{
			if (this.ForeColor != Color.Empty)
			{
				return;
			}
			for (Control control = this.Parent; control != null; control = control.Parent)
			{
				WebControl webControl = control as WebControl;
				if (webControl != null && webControl.ForeColor != Color.Empty)
				{
					this.ForeColor = webControl.ForeColor;
					return;
				}
				if (control == this.Container)
				{
					break;
				}
			}
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00052194 File Offset: 0x00050394
		protected override PostBackOptions GetPostBackOptions()
		{
			IPostBackContainer postBackContainer = this.Container as IPostBackContainer;
			if (postBackContainer != null)
			{
				return postBackContainer.GetPostBackOptions(this);
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x040018A9 RID: 6313
		private Control _container;
	}
}
