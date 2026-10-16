using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000368 RID: 872
	[SupportsEventValidation]
	internal class DataControlButton : Button, IDataControlButton, IButtonControl
	{
		// Token: 0x060020C0 RID: 8384 RVA: 0x00051FA0 File Offset: 0x000501A0
		public static IDataControlButton CreateButton(ButtonType type, Control container, string text, string image, string command, string commandArg, bool allowCallback)
		{
			IDataControlButton dataControlButton;
			if (type != ButtonType.Image)
			{
				if (type == ButtonType.Link)
				{
					dataControlButton = new DataControlLinkButton();
				}
				else
				{
					dataControlButton = new DataControlButton();
				}
			}
			else
			{
				dataControlButton = new DataControlImageButton();
				dataControlButton.ImageUrl = image;
			}
			dataControlButton.Container = container;
			dataControlButton.CommandName = command;
			dataControlButton.CommandArgument = commandArg;
			dataControlButton.Text = text;
			dataControlButton.CausesValidation = false;
			dataControlButton.AllowCallback = allowCallback;
			return dataControlButton;
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x00052000 File Offset: 0x00050200
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x00052008 File Offset: 0x00050208
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

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0000EE63 File Offset: 0x0000D063
		// (set) Token: 0x060020C4 RID: 8388 RVA: 0x00003946 File Offset: 0x00001B46
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

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x00052011 File Offset: 0x00050211
		// (set) Token: 0x060020C6 RID: 8390 RVA: 0x00052024 File Offset: 0x00050224
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

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x00008A4D File Offset: 0x00006C4D
		public ButtonType ButtonType
		{
			get
			{
				return ButtonType.Button;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x060020C9 RID: 8393 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override bool UseSubmitBehavior
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0005203C File Offset: 0x0005023C
		internal override string GetClientScriptEventReference()
		{
			if (this.AllowCallback)
			{
				ICallbackContainer callbackContainer = this.Container as ICallbackContainer;
				if (callbackContainer != null)
				{
					return callbackContainer.GetCallbackScript(this, base.CommandName + "$" + base.CommandArgument);
				}
			}
			return base.GetClientScriptEventReference();
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00052084 File Offset: 0x00050284
		protected override PostBackOptions GetPostBackOptions()
		{
			IPostBackContainer postBackContainer = this.Container as IPostBackContainer;
			if (postBackContainer != null)
			{
				return postBackContainer.GetPostBackOptions(this);
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x040018A8 RID: 6312
		private Control _container;
	}
}
