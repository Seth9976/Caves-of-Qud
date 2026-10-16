using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;button&gt; tag on the server.</summary>
	// Token: 0x02000250 RID: 592
	[DefaultEvent("ServerClick")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlButton : HtmlContainerControl, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> class.</summary>
		// Token: 0x0600181A RID: 6170 RVA: 0x0003F112 File Offset: 0x0003D312
		public HtmlButton()
			: base("button")
		{
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0003EF0B File Offset: 0x0003D10B
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				return this.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control causes validation when it posts back to the server. The default value is an empty string ("") indicating that this property is not set.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0003F11F File Offset: 0x0003D31F
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", "");
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The event arguments.</param>
		// Token: 0x0600181F RID: 6175 RVA: 0x0003F136 File Offset: 0x0003D336
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control when it posts back to the server. </summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001820 RID: 6176 RVA: 0x0003F13F File Offset: 0x0003D33F
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event and registers client script for generating a postback.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06001821 RID: 6177 RVA: 0x0003EF60 File Offset: 0x0003D160
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlButton.ServerClick" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001822 RID: 6178 RVA: 0x0003F174 File Offset: 0x0003D374
		protected virtual void OnServerClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlButton.ServerClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x06001823 RID: 6179 RVA: 0x0003F1A4 File Offset: 0x0003D3A4
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null && base.Events[HtmlButton.ServerClickEvent] != null)
			{
				PostBackOptions postBackOptions = this.GetPostBackOptions();
				AttributeCollection attributes = base.Attributes;
				attributes["onclick"] = attributes["onclick"] + page.ClientScript.GetPostBackEventReference(postBackOptions, true);
				writer.WriteAttribute("language", "javascript");
			}
			base.RenderAttributes(writer);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0003F21C File Offset: 0x0003D41C
		private PostBackOptions GetPostBackOptions()
		{
			Page page = this.Page;
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ValidationGroup = null;
			postBackOptions.ActionUrl = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = true;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Occurs when the user clicks an <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control on the client Web page.</summary>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06001825 RID: 6181 RVA: 0x0003F293 File Offset: 0x0003D493
		// (remove) Token: 0x06001826 RID: 6182 RVA: 0x0003F2A6 File Offset: 0x0003D4A6
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlButton.ServerClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlButton.ServerClickEvent, value);
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0003F2B9 File Offset: 0x0003D4B9
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlButton()
		{
			HtmlButton.ServerClickEvent = new object();
		}
	}
}
