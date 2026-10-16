using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036A RID: 874
	internal class DataControlImageButton : ImageButton, IDataControlButton, IButtonControl
	{
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x000521C6 File Offset: 0x000503C6
		// (set) Token: 0x060020D9 RID: 8409 RVA: 0x000521CE File Offset: 0x000503CE
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

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x00052011 File Offset: 0x00050211
		// (set) Token: 0x060020DB RID: 8411 RVA: 0x00052024 File Offset: 0x00050224
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

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x00008B4A File Offset: 0x00006D4A
		public ButtonType ButtonType
		{
			get
			{
				return ButtonType.Image;
			}
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000521D8 File Offset: 0x000503D8
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

		// Token: 0x060020DE RID: 8414 RVA: 0x00052220 File Offset: 0x00050420
		protected override PostBackOptions GetPostBackOptions()
		{
			IPostBackContainer postBackContainer = this.Container as IPostBackContainer;
			if (postBackContainer != null)
			{
				return postBackContainer.GetPostBackOptions(this);
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x040018AA RID: 6314
		private Control _container;
	}
}
