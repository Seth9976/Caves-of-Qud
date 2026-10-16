using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	/// <summary>Detects the user's authentication state and toggles the state of a link to log in to or log out of a Web site.</summary>
	// Token: 0x020003C6 RID: 966
	[Designer("System.Web.UI.Design.WebControls.LoginStatusDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("LoggingOut")]
	[Bindable(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LoginStatus : CompositeControl
	{
		/// <summary>Gets or sets the URL of the image used for the login link.</summary>
		/// <returns>A string containing the URL of the image used for the login link. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x00067994 File Offset: 0x00065B94
		// (set) Token: 0x06002875 RID: 10357 RVA: 0x000679C1 File Offset: 0x00065BC1
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string LoginImageUrl
		{
			get
			{
				object obj = this.ViewState["LoginImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LoginImageUrl");
					return;
				}
				this.ViewState["LoginImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text used for the login link.</summary>
		/// <returns>A string displayed as the login link. The default is "Login".</returns>
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x000679E8 File Offset: 0x00065BE8
		// (set) Token: 0x06002877 RID: 10359 RVA: 0x00067A1A File Offset: 0x00065C1A
		[Localizable(true)]
		public virtual string LoginText
		{
			get
			{
				object obj = this.ViewState["LoginText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Login");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LoginText");
					return;
				}
				this.ViewState["LoginText"] = value;
			}
		}

		/// <summary>Gets or sets a value that determines the action taken when a user logs out of a Web site with the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LogoutAction" /> values. The default is <see cref="F:System.Web.UI.WebControls.LogoutAction.Refresh" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to an invalid <see cref="T:System.Web.UI.WebControls.LogoutAction" /> value. </exception>
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x00067A44 File Offset: 0x00065C44
		// (set) Token: 0x06002879 RID: 10361 RVA: 0x00067A6D File Offset: 0x00065C6D
		[Themeable(false)]
		[DefaultValue(LogoutAction.Refresh)]
		public virtual LogoutAction LogoutAction
		{
			get
			{
				object obj = this.ViewState["LogoutAction"];
				if (obj != null)
				{
					return (LogoutAction)obj;
				}
				return LogoutAction.Refresh;
			}
			set
			{
				if (value < LogoutAction.Refresh || value > LogoutAction.RedirectToLoginPage)
				{
					throw new ArgumentOutOfRangeException("LogoutAction");
				}
				this.ViewState["LogoutAction"] = (int)value;
			}
		}

		/// <summary>Gets or sets the URL of the image used for the logout button.</summary>
		/// <returns>A string containing the URL of the image used for the logout link. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x00067A98 File Offset: 0x00065C98
		// (set) Token: 0x0600287B RID: 10363 RVA: 0x00067AC5 File Offset: 0x00065CC5
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string LogoutImageUrl
		{
			get
			{
				object obj = this.ViewState["LogoutImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LogoutImageUrl");
					return;
				}
				this.ViewState["LogoutImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the logout page.</summary>
		/// <returns>A string containing the URL of the logout page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x00067AEC File Offset: 0x00065CEC
		// (set) Token: 0x0600287D RID: 10365 RVA: 0x00067B19 File Offset: 0x00065D19
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[Themeable(false)]
		public virtual string LogoutPageUrl
		{
			get
			{
				object obj = this.ViewState["LogoutPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LogoutPageUrl");
					return;
				}
				this.ViewState["LogoutPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text used for the logout link.</summary>
		/// <returns>A string displayed as the logout link. The default is "Logout".</returns>
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x00067B40 File Offset: 0x00065D40
		// (set) Token: 0x0600287F RID: 10367 RVA: 0x00067B72 File Offset: 0x00065D72
		[Localizable(true)]
		public virtual string LogoutText
		{
			get
			{
				object obj = this.ViewState["LogoutText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Logout");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LogoutText");
					return;
				}
				this.ViewState["LogoutText"] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
		/// <returns>Always returns <see cref="F:System.Web.UI.HtmlTextWriterTag.A" />.</returns>
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		/// <summary>Creates the child controls that make up the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
		// Token: 0x06002881 RID: 10369 RVA: 0x00067B9C File Offset: 0x00065D9C
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this.logoutLinkButton = new LinkButton();
			this.logoutLinkButton.CausesValidation = false;
			this.logoutLinkButton.Command += this.LogoutClick;
			this.logoutImageButton = new ImageButton();
			this.logoutImageButton.CausesValidation = false;
			this.logoutImageButton.Command += this.LogoutClick;
			this.loginLinkButton = new LinkButton();
			this.loginLinkButton.CausesValidation = false;
			this.loginLinkButton.Command += this.LoginClick;
			this.loginImageButton = new ImageButton();
			this.loginImageButton.CausesValidation = false;
			this.loginImageButton.Command += this.LoginClick;
			this.Controls.Add(this.logoutLinkButton);
			this.Controls.Add(this.logoutImageButton);
			this.Controls.Add(this.loginLinkButton);
			this.Controls.Add(this.loginImageButton);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginStatus.LoggedOut" /> event after the user clicks the logout link and logout processing is complete.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002882 RID: 10370 RVA: 0x00067CB0 File Offset: 0x00065EB0
		protected virtual void OnLoggedOut(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginStatus.loggedOutEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginStatus.LoggingOut" /> event when a user clicks the logout link on the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> that contains event data.</param>
		// Token: 0x06002883 RID: 10371 RVA: 0x00067CE0 File Offset: 0x00065EE0
		protected virtual void OnLoggingOut(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[LoginStatus.loggingOutEvent];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		/// <summary>Determines whether a user is logged in, and gets the URL of the login page.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> containing the event data. </param>
		// Token: 0x06002884 RID: 10372 RVA: 0x0003EF60 File Offset: 0x0003D160
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.LoginName" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> control.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x06002885 RID: 10373 RVA: 0x00067D0E File Offset: 0x00065F0E
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (writer == null)
			{
				return;
			}
			this.RenderContents(writer);
		}

		/// <summary>Renders the contents of the control to the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x06002886 RID: 10374 RVA: 0x00067D1C File Offset: 0x00065F1C
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (writer == null)
			{
				return;
			}
			this.EnsureChildControls();
			bool flag = false;
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
				flag = this.Page.Request.IsAuthenticated;
			}
			bool flag2 = this.LogoutImageUrl.Length > 0;
			this.logoutLinkButton.Visible = flag && !flag2;
			this.logoutImageButton.Visible = flag && flag2;
			bool flag3 = this.LoginImageUrl.Length > 0;
			this.loginLinkButton.Visible = !flag && !flag3;
			this.loginImageButton.Visible = !flag && flag3;
			if (this.logoutLinkButton.Visible)
			{
				this.logoutLinkButton.Text = this.LogoutText;
				this.logoutLinkButton.CssClass = this.CssClass;
				this.logoutLinkButton.Render(writer);
				return;
			}
			if (this.logoutImageButton.Visible)
			{
				this.logoutImageButton.AlternateText = this.LogoutText;
				this.logoutImageButton.CssClass = this.CssClass;
				this.logoutImageButton.ImageUrl = this.LogoutImageUrl;
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.logoutImageButton.UniqueID);
				this.logoutImageButton.Render(writer);
				return;
			}
			if (this.loginLinkButton.Visible)
			{
				this.loginLinkButton.Text = this.LoginText;
				this.loginLinkButton.CssClass = this.CssClass;
				this.loginLinkButton.Render(writer);
				return;
			}
			if (this.loginImageButton.Visible)
			{
				this.loginImageButton.AlternateText = this.LoginText;
				this.loginImageButton.CssClass = this.CssClass;
				this.loginImageButton.ImageUrl = this.LoginImageUrl;
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.loginImageButton.UniqueID);
				this.loginImageButton.Render(writer);
			}
		}

		/// <summary>Overrides the base <see cref="M:System.Web.UI.Control.SetDesignModeState(System.Collections.IDictionary)" /> method. </summary>
		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the state of the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</param>
		// Token: 0x06002887 RID: 10375 RVA: 0x0004FA84 File Offset: 0x0004DC84
		[global::System.MonoTODO("for design-time usage - no more details available")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			base.SetDesignModeState(data);
		}

		/// <summary>Raised after the user clicks the logout link and the logout process is complete.</summary>
		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06002888 RID: 10376 RVA: 0x00067EF8 File Offset: 0x000660F8
		// (remove) Token: 0x06002889 RID: 10377 RVA: 0x00067F0B File Offset: 0x0006610B
		public event EventHandler LoggedOut
		{
			add
			{
				base.Events.AddHandler(LoginStatus.loggedOutEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginStatus.loggedOutEvent, value);
			}
		}

		/// <summary>Raised when the user clicks the logout button.</summary>
		// Token: 0x140000AD RID: 173
		// (add) Token: 0x0600288A RID: 10378 RVA: 0x00067F1E File Offset: 0x0006611E
		// (remove) Token: 0x0600288B RID: 10379 RVA: 0x00067F31 File Offset: 0x00066131
		public event LoginCancelEventHandler LoggingOut
		{
			add
			{
				base.Events.AddHandler(LoginStatus.loggingOutEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginStatus.loggingOutEvent, value);
			}
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x00067F44 File Offset: 0x00066144
		private void LogoutClick(object sender, CommandEventArgs e)
		{
			LoginCancelEventArgs e2 = new LoginCancelEventArgs(false);
			this.OnLoggingOut(e2);
			if (e2.Cancel)
			{
				return;
			}
			FormsAuthentication.SignOut();
			this.OnLoggedOut(e);
			switch (this.LogoutAction)
			{
			case LogoutAction.Refresh:
				HttpContext.Current.Response.Redirect(this.Page.Request.Url.AbsoluteUri);
				return;
			case LogoutAction.Redirect:
			{
				string text = this.LogoutPageUrl;
				if (text.Length == 0)
				{
					text = this.Page.Request.Url.AbsoluteUri;
				}
				HttpContext.Current.Response.Redirect(text);
				return;
			}
			case LogoutAction.RedirectToLoginPage:
				FormsAuthentication.RedirectToLoginPage();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x00067FEE File Offset: 0x000661EE
		private void LoginClick(object sender, CommandEventArgs e)
		{
			FormsAuthentication.RedirectToLoginPage();
		}

		// Token: 0x04001A80 RID: 6784
		private static readonly object loggedOutEvent = new object();

		// Token: 0x04001A81 RID: 6785
		private static readonly object loggingOutEvent = new object();

		// Token: 0x04001A82 RID: 6786
		private LinkButton logoutLinkButton;

		// Token: 0x04001A83 RID: 6787
		private ImageButton logoutImageButton;

		// Token: 0x04001A84 RID: 6788
		private LinkButton loginLinkButton;

		// Token: 0x04001A85 RID: 6789
		private ImageButton loginImageButton;
	}
}
