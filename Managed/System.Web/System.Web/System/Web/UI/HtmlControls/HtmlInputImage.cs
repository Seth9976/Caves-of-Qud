using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= image&gt; element on the server.</summary>
	// Token: 0x02000261 RID: 609
	[SupportsEventValidation]
	[DefaultEvent("ServerClick")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputImage : HtmlInputControl, IPostBackDataHandler, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> class.</summary>
		// Token: 0x060018D4 RID: 6356 RVA: 0x00040D60 File Offset: 0x0003EF60
		public HtmlInputImage()
			: base("image")
		{
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0003EF0B File Offset: 0x0003D10B
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[WebCategory("Behavior")]
		[WebSysDescription("")]
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

		/// <summary>Gets or sets the alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control in relation to other elements on the Web page.</summary>
		/// <returns>The alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control in relation to other elements on the Web page.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00040D6D File Offset: 0x0003EF6D
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x00040D7A File Offset: 0x0003EF7A
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public string Align
		{
			get
			{
				return this.GetAtt("align");
			}
			set
			{
				this.SetAtt("align", value);
			}
		}

		/// <summary>Gets or sets the alternative text that the browser displays if the image is unavailable or has not been downloaded.</summary>
		/// <returns>The alternative text for the specified image. The default value is an empty string ("").</returns>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00040D88 File Offset: 0x0003EF88
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x00040D95 File Offset: 0x0003EF95
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		public string Alt
		{
			get
			{
				return this.GetAtt("alt");
			}
			set
			{
				this.SetAtt("alt", value);
			}
		}

		/// <summary>Gets or sets the location of the image file.</summary>
		/// <returns>The location of the image file. The default value is an empty string ("").</returns>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00040DA3 File Offset: 0x0003EFA3
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x00040DB0 File Offset: 0x0003EFB0
		[UrlProperty]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[DefaultValue("")]
		public string Src
		{
			get
			{
				return this.GetAtt("src");
			}
			set
			{
				this.SetAtt("src", value);
			}
		}

		/// <summary>Gets or sets the border width for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</summary>
		/// <returns>The border width, in pixels, for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</returns>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00040DC0 File Offset: 0x0003EFC0
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x00040DEE File Offset: 0x0003EFEE
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("-1")]
		public int Border
		{
			get
			{
				string text = base.Attributes["border"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, Helpers.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("border");
					return;
				}
				base.Attributes["border"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00040E24 File Offset: 0x0003F024
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.UniqueID + ".x"];
			string text2 = postCollection[this.UniqueID + ".y"];
			if (text != null && text.Length != 0 && text2 != null && text2.Length != 0)
			{
				this.clicked_x = int.Parse(text, Helpers.InvariantCulture);
				this.clicked_y = int.Parse(text2, Helpers.InvariantCulture);
				this.Page.RegisterRequiresRaiseEvent(this);
				return true;
			}
			return false;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00040EA6 File Offset: 0x0003F0A6
		private void RaisePostBackEventInternal(string eventArgument)
		{
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(new ImageClickEventArgs(this.clicked_x, this.clicked_y));
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00003946 File Offset: 0x00001B46
		private void RaisePostDataChangedEventInternal()
		{
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control causes validation when it posts back to the server. The default value is an empty string (""), indicating that this property is not set. </returns>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0003F11F File Offset: 0x0003D31F
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x0003EF4D File Offset: 0x0003D14D
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

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018E4 RID: 6372 RVA: 0x00040ED8 File Offset: 0x0003F0D8
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x060018E5 RID: 6373 RVA: 0x00040EE2 File Offset: 0x0003F0E2
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEventInternal(eventArgument);
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control that the state of the control has changed.</summary>
		// Token: 0x060018E6 RID: 6374 RVA: 0x00040EEB File Offset: 0x0003F0EB
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputImage.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018E7 RID: 6375 RVA: 0x00040F04 File Offset: 0x0003F104
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputImage.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x060018E8 RID: 6376 RVA: 0x00040F0E File Offset: 0x0003F10E
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Enables the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control to raise events on postback.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x060018E9 RID: 6377 RVA: 0x00040F16 File Offset: 0x0003F116
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060018EA RID: 6378 RVA: 0x00040F20 File Offset: 0x0003F120
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && !base.Disabled)
			{
				page.RegisterRequiresPostBack(this);
				page.RegisterEnabledControl(this);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputImage.ServerClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.ImageClickEventArgs" /> that contains event data. </param>
		// Token: 0x060018EB RID: 6379 RVA: 0x00040F54 File Offset: 0x0003F154
		protected virtual void OnServerClick(ImageClickEventArgs e)
		{
			ImageClickEventHandler imageClickEventHandler = base.Events[HtmlInputImage.ServerClickEvent] as ImageClickEventHandler;
			if (imageClickEventHandler != null)
			{
				imageClickEventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlInputImage.Src" /> property contains a malformed URL.</exception>
		// Token: 0x060018EC RID: 6380 RVA: 0x00040F84 File Offset: 0x0003F184
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			if (this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup))
			{
				ClientScriptManager clientScript = page.ClientScript;
				AttributeCollection attributes = base.Attributes;
				attributes["onclick"] = attributes["onclick"] + clientScript.GetClientValidationEvent(this.ValidationGroup);
			}
			base.PreProcessRelativeReference(writer, "src");
			base.RenderAttributes(writer);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0004100D File Offset: 0x0003F20D
		private void SetAtt(string name, string value)
		{
			if (value == null || value.Length == 0)
			{
				base.Attributes.Remove(name);
				return;
			}
			base.Attributes[name] = value;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00041034 File Offset: 0x0003F234
		private string GetAtt(string name)
		{
			string text = base.Attributes[name];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		/// <summary>Occurs on the server when the user clicks an <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</summary>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060018EF RID: 6383 RVA: 0x00041058 File Offset: 0x0003F258
		// (remove) Token: 0x060018F0 RID: 6384 RVA: 0x00041058 File Offset: 0x0003F258
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event ImageClickEventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlInputImage.ServerClickEvent, value);
			}
			remove
			{
				base.Events.AddHandler(HtmlInputImage.ServerClickEvent, value);
			}
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0004106B File Offset: 0x0003F26B
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlInputImage()
		{
			HtmlInputImage.ServerClickEvent = new object();
		}

		// Token: 0x04001632 RID: 5682
		private int clicked_x;

		// Token: 0x04001633 RID: 5683
		private int clicked_y;
	}
}
