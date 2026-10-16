using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Provides programmatic access to the HTML &lt;form&gt; element on the server.</summary>
	// Token: 0x02000255 RID: 597
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlForm : HtmlContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> class.</summary>
		// Token: 0x0600184A RID: 6218 RVA: 0x0003F5AC File Offset: 0x0003D7AC
		public HtmlForm()
			: base("form")
		{
		}

		/// <summary>Gets or sets the action attribute of the HTML form.</summary>
		/// <returns>The action attribute of the HTML form. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0003F5BC File Offset: 0x0003D7BC
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x0003F5E9 File Offset: 0x0003D7E9
		public string Action
		{
			get
			{
				string text = base.Attributes["action"];
				if (string.IsNullOrEmpty(text))
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.Attributes["action"] = null;
					return;
				}
				base.Attributes["action"] = value;
			}
		}

		/// <summary>Gets or sets the child control of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control that causes postback when the ENTER key is pressed.</summary>
		/// <returns>The <see cref="P:System.Web.UI.Control.ID" /> of the button control to display as the default button when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> is loaded. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.InvalidOperationException">The control referenced as the default button is not of the type <see cref="T:System.Web.UI.WebControls.IButtonControl" />.</exception>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0003F616 File Offset: 0x0003D816
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x0003F627 File Offset: 0x0003D827
		[DefaultValue("")]
		public string DefaultButton
		{
			get
			{
				return this._defaultbutton ?? string.Empty;
			}
			set
			{
				this._defaultbutton = value;
			}
		}

		/// <summary>Gets or sets the control on the form to display as the control with input focus when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control is loaded.</summary>
		/// <returns>The <see cref="P:System.Web.UI.Control.ClientID" /> of the control on the form to display as the control with input focus when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> is loaded. The default value is an empty string ("").</returns>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x0003F630 File Offset: 0x0003D830
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x0003F641 File Offset: 0x0003D841
		[DefaultValue("")]
		public string DefaultFocus
		{
			get
			{
				return this._defaultfocus ?? string.Empty;
			}
			set
			{
				this._defaultfocus = value;
			}
		}

		/// <summary>Gets or sets the encoding type a browser uses when posting the form's data to the server.</summary>
		/// <returns>A string that contains the encoding type. The default value is an empty string (""), indicating that the browser's default content type is used.</returns>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0003F64C File Offset: 0x0003D84C
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x0003F674 File Offset: 0x0003D874
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public string Enctype
		{
			get
			{
				string text = base.Attributes["enctype"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("enctype");
					return;
				}
				base.Attributes["enctype"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates how a browser posts form data to the server for processing.</summary>
		/// <returns>A string that indicates how a browser posts form data to the server. The default value is POST.</returns>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0003F69C File Offset: 0x0003D89C
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x0003F6CC File Offset: 0x0003D8CC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public string Method
		{
			get
			{
				string text = base.Attributes["method"];
				if (text == null || text.Length == 0)
				{
					return "post";
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("method");
					return;
				}
				base.Attributes["method"] = value;
			}
		}

		/// <summary>Gets the identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <returns>A string that contains the identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" />.</returns>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0003F6F3 File Offset: 0x0003D8F3
		// (set) Token: 0x06001856 RID: 6230 RVA: 0x00003946 File Offset: 0x00001B46
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public virtual string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether to force controls disabled on the client to submit their values, allowing them to preserve their values after the page posts back to the server. </summary>
		/// <returns>true if controls disabled on the client are forced to submit their values; otherwise, false. The default value is false.</returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0003F6FB File Offset: 0x0003D8FB
		// (set) Token: 0x06001858 RID: 6232 RVA: 0x0003F703 File Offset: 0x0003D903
		[DefaultValue(false)]
		public virtual bool SubmitDisabledControls
		{
			get
			{
				return this.submitdisabledcontrols;
			}
			set
			{
				this.submitdisabledcontrols = value;
			}
		}

		/// <summary>Gets or sets the frame or window in which to render the results of information that is posted to the server.</summary>
		/// <returns>The browser window or frame that displays the results of the information posted to the server. The default is an empty string (""), which refreshes the window or frame with focus. </returns>
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0003F70C File Offset: 0x0003D90C
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x0003F734 File Offset: 0x0003D934
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public string Target
		{
			get
			{
				string text = base.Attributes["target"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("target");
					return;
				}
				base.Attributes["target"] = value;
			}
		}

		/// <summary>Gets the unique programmatic identifier assigned to the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <returns>The unique programmatic identifier assigned to the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</returns>
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0003F75B File Offset: 0x0003D95B
		public override string UniqueID
		{
			get
			{
				if (this.NamingContainer == this.Page)
				{
					return this.ID;
				}
				return "aspnetForm";
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> collection for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control's child server controls.</returns>
		// Token: 0x0600185C RID: 6236 RVA: 0x0003F777 File Offset: 0x0003D977
		[global::System.MonoTODO("why override?")]
		protected override ControlCollection CreateControlCollection()
		{
			return base.CreateControlCollection();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x0600185D RID: 6237 RVA: 0x0003F780 File Offset: 0x0003D980
		protected internal override void OnInit(EventArgs e)
		{
			this.inited = true;
			Page page = this.Page;
			if (page != null)
			{
				page.RegisterViewStateHandler();
				page.RegisterForm(this);
			}
			base.OnInit(e);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0003F7B4 File Offset: 0x0003D9B4
		internal bool DetermineRenderUplevel()
		{
			if (this.isUplevel != null)
			{
				return this.isUplevel.Value;
			}
			this.isUplevel = new bool?(UplevelHelper.IsUplevel(HttpCapabilitiesBase.GetUserAgentForDetection(HttpContext.Current.Request)));
			return this.isUplevel.Value;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600185F RID: 6239 RVA: 0x0003EF60 File Offset: 0x0003D160
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content. </param>
		/// <exception cref="T:System.InvalidOperationException">The control ID set in the <see cref="P:System.Web.UI.HtmlControls.HtmlForm.DefaultButton" /> property is not of the type <see cref="T:System.Web.UI.WebControls.IButtonControl" />.</exception>
		// Token: 0x06001860 RID: 6240 RVA: 0x0003F804 File Offset: 0x0003DA04
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			string text = base.Attributes["action"];
			Page page = this.Page;
			HttpRequest httpRequest = ((page != null) ? page.RequestInternal : null);
			string text4;
			if (string.IsNullOrEmpty(text))
			{
				string text2 = ((httpRequest != null) ? httpRequest.ClientFilePath : null);
				string text3 = ((httpRequest != null) ? httpRequest.CurrentExecutionFilePath : null);
				if (text2 == null)
				{
					text4 = this.Action;
				}
				else if (text2 == text3)
				{
					text4 = UrlUtils.GetFile(text2);
				}
				else
				{
					SessionStateSection sessionStateSection = WebConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;
					bool flag = sessionStateSection != null && sessionStateSection.Cookieless == HttpCookieMode.UseUri;
					string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
					int length = appDomainAppVirtualPath.Length;
					if (length > 1)
					{
						if (flag)
						{
							if (StrUtils.StartsWith(text2, appDomainAppVirtualPath, true))
							{
								text2 = text2.Substring(length + 1);
							}
						}
						else if (StrUtils.StartsWith(text3, appDomainAppVirtualPath, true))
						{
							text3 = text3.Substring(length + 1);
						}
					}
					if (flag)
					{
						Uri uri = new Uri("http://host" + text3);
						string text5 = ((text2[0] == '/') ? "" : "/");
						text4 = new Uri("http://host" + text5 + text2).MakeRelative(uri);
					}
					else
					{
						text4 = text3;
					}
				}
			}
			else
			{
				text4 = text;
			}
			if (httpRequest != null)
			{
				text4 += httpRequest.QueryStringRaw;
			}
			if (httpRequest != null)
			{
				XhtmlConformanceSection xhtmlConformanceSection = WebConfigurationManager.GetSection("system.web/xhtmlConformance") as XhtmlConformanceSection;
				if ((xhtmlConformanceSection == null || xhtmlConformanceSection.Mode != XhtmlConformanceMode.Strict) && base.RenderingCompatibilityLessThan40)
				{
					writer.WriteAttribute("name", this.Name);
				}
			}
			writer.WriteAttribute("method", this.Method);
			if (string.IsNullOrEmpty(text))
			{
				writer.WriteAttribute("action", text4, true);
			}
			if (this.ID == null)
			{
				string clientID = this.ClientID;
			}
			string text6 = ((page != null) ? page.GetSubmitStatements() : null);
			if (!string.IsNullOrEmpty(text6))
			{
				base.Attributes.Remove("onsubmit");
				writer.WriteAttribute("onsubmit", text6);
			}
			string enctype = this.Enctype;
			if (!string.IsNullOrEmpty(enctype))
			{
				writer.WriteAttribute("enctype", enctype);
			}
			string target = this.Target;
			if (!string.IsNullOrEmpty(target))
			{
				writer.WriteAttribute("target", target);
			}
			string defaultButton = this.DefaultButton;
			if (!string.IsNullOrEmpty(defaultButton))
			{
				Control control = this.FindControl(defaultButton);
				if (control == null || !(control is IButtonControl))
				{
					throw new InvalidOperationException(string.Format("The DefaultButton of '{0}' must be the ID of a control of type IButtonControl.", this.ID));
				}
				if (page != null && this.DetermineRenderUplevel())
				{
					writer.WriteAttribute("onkeypress", string.Concat(new string[] { "javascript:return ", page.WebFormScriptReference, ".WebForm_FireDefaultButton(event, '", control.ClientID, "')" }));
				}
			}
			base.Attributes.Remove("method");
			base.Attributes.Remove("enctype");
			base.Attributes.Remove("target");
			base.RenderAttributes(writer);
		}

		/// <summary>Renders the child controls of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		/// <exception cref="T:System.Web.HttpException">The Web page has more than one server-side &lt;form&gt; tag.</exception>
		// Token: 0x06001861 RID: 6241 RVA: 0x0003FAFC File Offset: 0x0003DCFC
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (!this.inited && page != null)
			{
				page.RegisterViewStateHandler();
				page.RegisterForm(this);
			}
			if (page != null)
			{
				page.OnFormRender(writer, this.ClientID);
			}
			base.RenderChildren(writer);
			if (page != null)
			{
				page.OnFormPostRender(writer, this.ClientID);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the form control content.</param>
		// Token: 0x06001862 RID: 6242 RVA: 0x00030026 File Offset: 0x0002E226
		[global::System.MonoTODO("why override?")]
		public override void RenderControl(HtmlTextWriter writer)
		{
			base.RenderControl(writer);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="output">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control is not rendered without a reference to the <see cref="T:System.Web.UI.Page" /> instance.</exception>
		// Token: 0x06001863 RID: 6243 RVA: 0x0003FB4F File Offset: 0x0003DD4F
		protected internal override void Render(HtmlTextWriter output)
		{
			base.Render(output);
		}

		// Token: 0x0400161D RID: 5661
		private bool inited;

		// Token: 0x0400161E RID: 5662
		private string _defaultfocus;

		// Token: 0x0400161F RID: 5663
		private string _defaultbutton;

		// Token: 0x04001620 RID: 5664
		private bool submitdisabledcontrols;

		// Token: 0x04001621 RID: 5665
		private bool? isUplevel;
	}
}
