using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;a&gt; element on the server.</summary>
	// Token: 0x0200024F RID: 591
	[DefaultEvent("ServerClick")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlAnchor : HtmlContainerControl, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> class.</summary>
		// Token: 0x06001804 RID: 6148 RVA: 0x0003ED9C File Offset: 0x0003CF9C
		public HtmlAnchor()
			: base("a")
		{
		}

		/// <summary>Gets or sets the URL target of the link specified in the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> server control.</summary>
		/// <returns>The URL target of the link.</returns>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0003EDAC File Offset: 0x0003CFAC
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x0003EDD4 File Offset: 0x0003CFD4
		[DefaultValue("")]
		[UrlProperty]
		[WebCategory("Action")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public string HRef
		{
			get
			{
				string text = base.Attributes["href"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.Attributes.Remove("href");
					return;
				}
				base.Attributes["href"] = value;
			}
		}

		/// <summary>Gets or sets the bookmark name defined in the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> server control.</summary>
		/// <returns>The bookmark name.</returns>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0003EE04 File Offset: 0x0003D004
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x0003EE2C File Offset: 0x0003D02C
		[DefaultValue("")]
		[WebCategory("Navigation")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public string Name
		{
			get
			{
				string text = base.Attributes["name"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.Attributes.Remove("name");
					return;
				}
				base.Attributes["name"] = value;
			}
		}

		/// <summary>Gets or sets the name of the browser window or frame that displays the contents of the Web page that is linked to when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control is clicked. </summary>
		/// <returns>The browser window or frame that displays the contents of the Web page linked to when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> is clicked. The default is an empty string ("").</returns>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0003EE5C File Offset: 0x0003D05C
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x0003EE84 File Offset: 0x0003D084
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Navigation")]
		public string Target
		{
			get
			{
				string text = base.Attributes["target"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.Attributes.Remove("target");
					return;
				}
				base.Attributes["target"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text displayed when the mouse pointer is placed over the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control.</summary>
		/// <returns>The text displayed when the mouse pointer is placed over the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" />.</returns>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0003EEB4 File Offset: 0x0003D0B4
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x0003EEDC File Offset: 0x0003D0DC
		[WebCategory("Appearance")]
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[DefaultValue("")]
		public string Title
		{
			get
			{
				string text = base.Attributes["title"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.Attributes.Remove("title");
					return;
				}
				base.Attributes["title"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control is clicked. </summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> is clicked; otherwise, false. The default is true.</returns>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0003EF0B File Offset: 0x0003D10B
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[DefaultValue(true)]
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

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0003EF36 File Offset: 0x0003D136
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event and registers client script for generating a postback.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001811 RID: 6161 RVA: 0x0003EF60 File Offset: 0x0003D160
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlAnchor.ServerClick" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x06001812 RID: 6162 RVA: 0x0003EF6C File Offset: 0x0003D16C
		protected virtual void OnServerClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlAnchor.serverClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlAnchor.HRef" /> contains a malformed URL.</exception>
		// Token: 0x06001813 RID: 6163 RVA: 0x0003EF9C File Offset: 0x0003D19C
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if ((EventHandler)base.Events[HtmlAnchor.serverClickEvent] != null)
			{
				PostBackOptions postBackOptions = this.GetPostBackOptions();
				ClientScriptManager clientScript = this.Page.ClientScript;
				clientScript.RegisterForEventValidation(postBackOptions);
				base.Attributes["href"] = clientScript.GetPostBackEventReference(postBackOptions, true);
			}
			else
			{
				string href = this.HRef;
				if (href != string.Empty)
				{
					this.HRef = base.ResolveClientUrl(href);
				}
			}
			base.RenderAttributes(writer);
			base.Attributes.Remove("href");
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control when it posts back to the server. </summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001814 RID: 6164 RVA: 0x0003F02C File Offset: 0x0003D22C
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(EventArgs.Empty);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0003F060 File Offset: 0x0003D260
		private PostBackOptions GetPostBackOptions()
		{
			Page page = this.Page;
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ValidationGroup = null;
			postBackOptions.ActionUrl = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = true;
			postBackOptions.ClientSubmit = true;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Page.RaisePostBackEvent(System.Web.UI.IPostBackEventHandler,System.String)" />.</summary>
		/// <param name="eventArgument">The event arguments.</param>
		// Token: 0x06001816 RID: 6166 RVA: 0x0003F0D7 File Offset: 0x0003D2D7
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control is clicked.</summary>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06001817 RID: 6167 RVA: 0x0003F0E0 File Offset: 0x0003D2E0
		// (remove) Token: 0x06001818 RID: 6168 RVA: 0x0003F0F3 File Offset: 0x0003D2F3
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlAnchor.serverClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlAnchor.serverClickEvent, value);
			}
		}

		// Token: 0x04001619 RID: 5657
		private static readonly object serverClickEvent = new object();
	}
}
