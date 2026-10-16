using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Security;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides user interface (UI) elements for logging in to a Web site.</summary>
	// Token: 0x020003C2 RID: 962
	[Bindable(false)]
	[DefaultEvent("Authenticate")]
	[Designer("System.Web.UI.Design.WebControls.LoginDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Login : CompositeControl, IRenderOuterTable
	{
		/// <summary>Gets or sets the amount of padding inside the borders of the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>The amount of space (in pixels) between the contents of a <see cref="T:System.Web.UI.WebControls.Login" /> control and the <see cref="T:System.Web.UI.WebControls.Login" /> control's border. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Login.BorderPadding" /> property is set to a value less than -1.</exception>
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x000659E8 File Offset: 0x00063BE8
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x00065A11 File Offset: 0x00063C11
		[DefaultValue(1)]
		public virtual int BorderPadding
		{
			get
			{
				object obj = this.ViewState["BorderPadding"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("BorderPadding", "< -1");
				}
				this.ViewState["BorderPadding"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Remember Me check box.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the <see cref="T:System.Web.UI.WebControls.Login" /> control's Remember Me check box.</returns>
		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x00065A3D File Offset: 0x00063C3D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public TableItemStyle CheckBoxStyle
		{
			get
			{
				if (this.checkBoxStyle == null)
				{
					this.checkBoxStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.checkBoxStyle).TrackViewState();
					}
				}
				return this.checkBoxStyle;
			}
		}

		/// <summary>Gets the location of an image to display next to the link to a registration page for new users.</summary>
		/// <returns>The URL of the image to display. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x060027ED RID: 10221 RVA: 0x00065A6C File Offset: 0x00063C6C
		// (set) Token: 0x060027EE RID: 10222 RVA: 0x00065A99 File Offset: 0x00063C99
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string CreateUserIconUrl
		{
			get
			{
				object obj = this.ViewState["CreateUserIconUrl"];
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
					this.ViewState.Remove("CreateUserIconUrl");
					return;
				}
				this.ViewState["CreateUserIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of a link to a registration page for new users.</summary>
		/// <returns>The text of the link to the new-user registration page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x00065AC0 File Offset: 0x00063CC0
		// (set) Token: 0x060027F0 RID: 10224 RVA: 0x00065AED File Offset: 0x00063CED
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string CreateUserText
		{
			get
			{
				object obj = this.ViewState["CreateUserText"];
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
					this.ViewState.Remove("CreateUserText");
					return;
				}
				this.ViewState["CreateUserText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the new-user registration page.</summary>
		/// <returns>The URL of the new-user registration page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x00065B14 File Offset: 0x00063D14
		// (set) Token: 0x060027F2 RID: 10226 RVA: 0x00065B41 File Offset: 0x00063D41
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string CreateUserUrl
		{
			get
			{
				object obj = this.ViewState["CreateUserUrl"];
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
					this.ViewState.Remove("CreateUserUrl");
					return;
				}
				this.ViewState["CreateUserUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the page displayed to the user when a login attempt is successful.</summary>
		/// <returns>The URL of the page the user is redirected to when a login attempt is successful. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x00065B68 File Offset: 0x00063D68
		// (set) Token: 0x060027F4 RID: 10228 RVA: 0x00065B95 File Offset: 0x00063D95
		[UrlProperty]
		[Themeable(false)]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string DestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["DestinationPageUrl"];
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
					this.ViewState.Remove("DestinationPageUrl");
					return;
				}
				this.ViewState["DestinationPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to display a check box to enable the user to control whether a persistent cookie is sent to their browser.</summary>
		/// <returns>true to display the check box; otherwise, false. The default is true.</returns>
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x00065BBC File Offset: 0x00063DBC
		// (set) Token: 0x060027F6 RID: 10230 RVA: 0x00065BE5 File Offset: 0x00063DE5
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool DisplayRememberMe
		{
			get
			{
				object obj = this.ViewState["DisplayRememberMe"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["DisplayRememberMe"] = value;
			}
		}

		/// <summary>Gets or sets the action that occurs when a login attempt fails.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LoginFailureAction" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.LoginFailureAction.Refresh" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.LoginFailureAction" /> enumeration values.</exception>
		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x00065C00 File Offset: 0x00063E00
		// (set) Token: 0x060027F8 RID: 10232 RVA: 0x00065C29 File Offset: 0x00063E29
		[global::System.MonoTODO("RedirectToLoginPage not yet implemented in FormsAuthentication")]
		[Themeable(false)]
		[DefaultValue(LoginFailureAction.Refresh)]
		public virtual LoginFailureAction FailureAction
		{
			get
			{
				object obj = this.ViewState["FailureAction"];
				if (obj != null)
				{
					return (LoginFailureAction)obj;
				}
				return LoginFailureAction.Refresh;
			}
			set
			{
				if (value < LoginFailureAction.Refresh || value > LoginFailureAction.RedirectToLoginPage)
				{
					throw new ArgumentOutOfRangeException("FailureAction");
				}
				this.ViewState["FailureAction"] = (int)value;
			}
		}

		/// <summary>Gets or sets the text displayed when a login attempt fails.</summary>
		/// <returns>The text to display to the user when a login attempt fails. The default is "Your login attempt has failed. Please try again." </returns>
		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x00065C54 File Offset: 0x00063E54
		// (set) Token: 0x060027FA RID: 10234 RVA: 0x00065C86 File Offset: 0x00063E86
		[Localizable(true)]
		public virtual string FailureText
		{
			get
			{
				object obj = this.ViewState["FailureText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Your login attempt was not successful. Please try again.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("FailureText");
					return;
				}
				this.ViewState["FailureText"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of error text in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of error text.</returns>
		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x00065CAD File Offset: 0x00063EAD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle FailureTextStyle
		{
			get
			{
				if (this.failureTextStyle == null)
				{
					this.failureTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.failureTextStyle).TrackViewState();
					}
				}
				return this.failureTextStyle;
			}
		}

		/// <summary>Gets the location of an image to display next to the link to the login Help page.</summary>
		/// <returns>The URL of the image to display. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x00065CDC File Offset: 0x00063EDC
		// (set) Token: 0x060027FD RID: 10237 RVA: 0x0004E8D1 File Offset: 0x0004CAD1
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string HelpPageIconUrl
		{
			get
			{
				object obj = this.ViewState["HelpPageIconUrl"];
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
					this.ViewState.Remove("HelpPageIconUrl");
					return;
				}
				this.ViewState["HelpPageIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of a link to the login Help page.</summary>
		/// <returns>The text of the link to the login Help page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x00065D0C File Offset: 0x00063F0C
		// (set) Token: 0x060027FF RID: 10239 RVA: 0x0004E925 File Offset: 0x0004CB25
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string HelpPageText
		{
			get
			{
				object obj = this.ViewState["HelpPageText"];
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
					this.ViewState.Remove("HelpPageText");
					return;
				}
				this.ViewState["HelpPageText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the login Help page.</summary>
		/// <returns>The URL of the login Help page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x00065D3C File Offset: 0x00063F3C
		// (set) Token: 0x06002801 RID: 10241 RVA: 0x0004E979 File Offset: 0x0004CB79
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string HelpPageUrl
		{
			get
			{
				object obj = this.ViewState["HelpPageUrl"];
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
					this.ViewState.Remove("HelpPageUrl");
					return;
				}
				this.ViewState["HelpPageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of hyperlinks in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of hyperlinks.</returns>
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x00065D69 File Offset: 0x00063F69
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public TableItemStyle HyperLinkStyle
		{
			get
			{
				if (this.hyperLinkStyle == null)
				{
					this.hyperLinkStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.hyperLinkStyle).TrackViewState();
					}
				}
				return this.hyperLinkStyle;
			}
		}

		/// <summary>Gets or sets login instruction text for the user.</summary>
		/// <returns>The login instruction text to display to the user. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x00065D98 File Offset: 0x00063F98
		// (set) Token: 0x06002804 RID: 10244 RVA: 0x0004E9FD File Offset: 0x0004CBFD
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string InstructionText
		{
			get
			{
				object obj = this.ViewState["InstructionText"];
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
					this.ViewState.Remove("InstructionText");
					return;
				}
				this.ViewState["InstructionText"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that defines the settings for instruction text in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style settings of the <see cref="T:System.Web.UI.WebControls.Login" /> control instruction text.</returns>
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002805 RID: 10245 RVA: 0x00065DC5 File Offset: 0x00063FC5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle InstructionTextStyle
		{
			get
			{
				if (this.instructionTextStyle == null)
				{
					this.instructionTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.instructionTextStyle).TrackViewState();
					}
				}
				return this.instructionTextStyle;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that defines the settings for <see cref="T:System.Web.UI.WebControls.Login" /> control labels.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that defines the style settings of the <see cref="T:System.Web.UI.WebControls.Login" /> control labels.</returns>
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x00065DF3 File Offset: 0x00063FF3
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle LabelStyle
		{
			get
			{
				if (this.labelStyle == null)
				{
					this.labelStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.labelStyle).TrackViewState();
					}
				}
				return this.labelStyle;
			}
		}

		/// <summary>Gets or sets the template used to display the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.Login" /> control. The default value is null.</returns>
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x00065E21 File Offset: 0x00064021
		// (set) Token: 0x06002808 RID: 10248 RVA: 0x00065E29 File Offset: 0x00064029
		[Browsable(false)]
		[TemplateContainer(typeof(Login))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate LayoutTemplate
		{
			get
			{
				return this.layoutTemplate;
			}
			set
			{
				this.layoutTemplate = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to use for the login button.</summary>
		/// <returns>The URL of the image used for the login button. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x00065E34 File Offset: 0x00064034
		// (set) Token: 0x0600280A RID: 10250 RVA: 0x00065E61 File Offset: 0x00064061
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string LoginButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["LoginButtonImageUrl"];
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
					this.ViewState.Remove("LoginButtonImageUrl");
					return;
				}
				this.ViewState["LoginButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that allows you to set the appearance of the login button in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the login button.</returns>
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x00065E88 File Offset: 0x00064088
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style LoginButtonStyle
		{
			get
			{
				if (this.logonButtonStyle == null)
				{
					this.logonButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.logonButtonStyle).TrackViewState();
					}
				}
				return this.logonButtonStyle;
			}
		}

		/// <summary>Gets or sets the text for the <see cref="T:System.Web.UI.WebControls.Login" /> control's login button.</summary>
		/// <returns>The text used for the <see cref="T:System.Web.UI.WebControls.Login" /> control's login button. The default is "Login".</returns>
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x00065EB8 File Offset: 0x000640B8
		// (set) Token: 0x0600280D RID: 10253 RVA: 0x00065EEA File Offset: 0x000640EA
		[Localizable(true)]
		public virtual string LoginButtonText
		{
			get
			{
				object obj = this.ViewState["LoginButtonText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Log In");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LoginButtonText");
					return;
				}
				this.ViewState["LoginButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button to use when rendering the <see cref="T:System.Web.UI.WebControls.Login" /> button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Login.LoginButtonType" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration value. </exception>
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x00065F14 File Offset: 0x00064114
		// (set) Token: 0x0600280F RID: 10255 RVA: 0x00065F3D File Offset: 0x0006413D
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType LoginButtonType
		{
			get
			{
				object obj = this.ViewState["LoginButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				if (value < ButtonType.Button || value > ButtonType.Link)
				{
					throw new ArgumentOutOfRangeException("LoginButtonType");
				}
				this.ViewState["LoginButtonType"] = (int)value;
			}
		}

		/// <summary>Gets or sets the name of the membership data provider used by the control.</summary>
		/// <returns>The name of the membership data provider used by the control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x00065F68 File Offset: 0x00064168
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x00065F95 File Offset: 0x00064195
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string MembershipProvider
		{
			get
			{
				object obj = this.ViewState["MembershipProvider"];
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
					this.ViewState.Remove("MembershipProvider");
					return;
				}
				this.ViewState["MembershipProvider"] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the position of the elements of the <see cref="T:System.Web.UI.WebControls.Login" /> control on the page.</summary>
		/// <returns>One the <see cref="T:System.Web.UI.WebControls.Orientation" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.Orientation.Vertical" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Login.Orientation" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.Orientation" /> enumeration value. </exception>
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x00065FBC File Offset: 0x000641BC
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x00065FE5 File Offset: 0x000641E5
		[DefaultValue(Orientation.Vertical)]
		public virtual Orientation Orientation
		{
			get
			{
				object obj = this.ViewState["Orientation"];
				if (obj != null)
				{
					return (Orientation)obj;
				}
				return Orientation.Vertical;
			}
			set
			{
				if (value < Orientation.Horizontal || value > Orientation.Vertical)
				{
					throw new ArgumentOutOfRangeException("Orientation");
				}
				this.ViewState["Orientation"] = (int)value;
			}
		}

		/// <summary>Gets the password entered by the user.</summary>
		/// <returns>The password entered by the user. The default is null.</returns>
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x00066010 File Offset: 0x00064210
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string Password
		{
			get
			{
				if (this._password == null)
				{
					return string.Empty;
				}
				return this._password;
			}
		}

		/// <summary>Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box.</summary>
		/// <returns>The text of the label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box. The default is "Password:".</returns>
		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x00066028 File Offset: 0x00064228
		// (set) Token: 0x06002816 RID: 10262 RVA: 0x0004ED8E File Offset: 0x0004CF8E
		[Localizable(true)]
		public virtual string PasswordLabelText
		{
			get
			{
				object obj = this.ViewState["PasswordLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "Password:";
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("PasswordLabelText");
					return;
				}
				this.ViewState["PasswordLabelText"] = value;
			}
		}

		/// <summary>Gets the location of an image to display next to the link to the password recovery page.</summary>
		/// <returns>The URL of the image to display. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x00066058 File Offset: 0x00064258
		// (set) Token: 0x06002818 RID: 10264 RVA: 0x00066085 File Offset: 0x00064285
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string PasswordRecoveryIconUrl
		{
			get
			{
				object obj = this.ViewState["PasswordRecoveryIconUrl"];
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
					this.ViewState.Remove("PasswordRecoveryIconUrl");
					return;
				}
				this.ViewState["PasswordRecoveryIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of a link to the password recovery page.</summary>
		/// <returns>The text of the link to the password recovery page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x000660AC File Offset: 0x000642AC
		// (set) Token: 0x0600281A RID: 10266 RVA: 0x000660D9 File Offset: 0x000642D9
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string PasswordRecoveryText
		{
			get
			{
				object obj = this.ViewState["PasswordRecoveryText"];
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
					this.ViewState.Remove("PasswordRecoveryText");
					return;
				}
				this.ViewState["PasswordRecoveryText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the password recovery page.</summary>
		/// <returns>The URL of the password recovery page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x00066100 File Offset: 0x00064300
		// (set) Token: 0x0600281C RID: 10268 RVA: 0x0006612D File Offset: 0x0006432D
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string PasswordRecoveryUrl
		{
			get
			{
				object obj = this.ViewState["PasswordRecoveryUrl"];
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
					this.ViewState.Remove("PasswordRecoveryUrl");
					return;
				}
				this.ViewState["PasswordRecoveryUrl"] = value;
			}
		}

		/// <summary>Gets or sets the error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the password field is left blank.</summary>
		/// <returns>The error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the password field is left blank. The default is "Password." </returns>
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x00066154 File Offset: 0x00064354
		// (set) Token: 0x0600281E RID: 10270 RVA: 0x0004EE9A File Offset: 0x0004D09A
		[Localizable(true)]
		public virtual string PasswordRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["PasswordRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Password is required.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("PasswordRequiredErrorMessage");
					return;
				}
				this.ViewState["PasswordRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control encloses rendered HTML in a table element in order to apply inline styles.</summary>
		/// <returns>true if the control encloses rendered HTML in a table element; otherwise, false. The default is true.</returns>
		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x00066186 File Offset: 0x00064386
		// (set) Token: 0x06002820 RID: 10272 RVA: 0x0006618E File Offset: 0x0006438E
		[DefaultValue(true)]
		public virtual bool RenderOuterTable
		{
			get
			{
				return this.renderOuterTable;
			}
			set
			{
				this.renderOuterTable = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to send a persistent authentication cookie to the user's browser.</summary>
		/// <returns>true to send a persistent authentication cookie; otherwise, false. The default value is false.</returns>
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x00066198 File Offset: 0x00064398
		// (set) Token: 0x06002822 RID: 10274 RVA: 0x000661C1 File Offset: 0x000643C1
		[DefaultValue(false)]
		[Themeable(false)]
		public virtual bool RememberMeSet
		{
			get
			{
				object obj = this.ViewState["RememberMeSet"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["RememberMeSet"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label for the Remember Me check box.</summary>
		/// <returns>The text of the label for the Remember Me check box. The default is "Remember me next time." </returns>
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x000661DC File Offset: 0x000643DC
		// (set) Token: 0x06002824 RID: 10276 RVA: 0x0006620E File Offset: 0x0006440E
		[Localizable(true)]
		public virtual string RememberMeText
		{
			get
			{
				object obj = this.ViewState["RememberMeText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Remember me next time.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("RememberMeText");
					return;
				}
				this.ViewState["RememberMeText"] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to a <see cref="T:System.Web.UI.WebControls.Login" /> control. This property is used primarily by control developers.</summary>
		/// <returns>Always returns <see cref="F:System.Web.UI.HtmlTextWriterTag.Table" />.</returns>
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x0004A624 File Offset: 0x00048824
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of text boxes in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that contains properties that define the appearance of text boxes.</returns>
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x00066235 File Offset: 0x00064435
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style TextBoxStyle
		{
			get
			{
				if (this.textBoxStyle == null)
				{
					this.textBoxStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.textBoxStyle).TrackViewState();
					}
				}
				return this.textBoxStyle;
			}
		}

		/// <summary>Specifies the position of each label relative to its associated text box for the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.LoginTextLayout.TextOnLeft" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration values.</exception>
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x00066264 File Offset: 0x00064464
		// (set) Token: 0x06002828 RID: 10280 RVA: 0x0006628D File Offset: 0x0006448D
		[DefaultValue(LoginTextLayout.TextOnLeft)]
		public virtual LoginTextLayout TextLayout
		{
			get
			{
				object obj = this.ViewState["TextLayout"];
				if (obj != null)
				{
					return (LoginTextLayout)obj;
				}
				return LoginTextLayout.TextOnLeft;
			}
			set
			{
				if (value < LoginTextLayout.TextOnLeft || value > LoginTextLayout.TextOnTop)
				{
					throw new ArgumentOutOfRangeException("TextLayout");
				}
				this.ViewState["TextLayout"] = (int)value;
			}
		}

		/// <summary>Gets or sets the title of the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>The title of the <see cref="T:System.Web.UI.WebControls.Login" /> control. The default is "Login". </returns>
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000662B8 File Offset: 0x000644B8
		// (set) Token: 0x0600282A RID: 10282 RVA: 0x0004D91E File Offset: 0x0004BB1E
		[Localizable(true)]
		public virtual string TitleText
		{
			get
			{
				object obj = this.ViewState["TitleText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Log In");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("TitleText");
					return;
				}
				this.ViewState["TitleText"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of the title text in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of title text.</returns>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x000662EA File Offset: 0x000644EA
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle TitleTextStyle
		{
			get
			{
				if (this.titleTextStyle == null)
				{
					this.titleTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.titleTextStyle).TrackViewState();
					}
				}
				return this.titleTextStyle;
			}
		}

		/// <summary>Gets the user name entered by the user.</summary>
		/// <returns>The user name entered by the user. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x00066318 File Offset: 0x00064518
		// (set) Token: 0x0600282D RID: 10285 RVA: 0x0004F14D File Offset: 0x0004D34D
		[DefaultValue("")]
		public virtual string UserName
		{
			get
			{
				object obj = this.ViewState["UserName"];
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
					this.ViewState.Remove("UserName");
					return;
				}
				this.ViewState["UserName"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.Login.UserName" /> text box.</summary>
		/// <returns>The text of the label for the <see cref="P:System.Web.UI.WebControls.Login.UserName" /> text box. The default is "User Name:".</returns>
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x00066348 File Offset: 0x00064548
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x0004F1A6 File Offset: 0x0004D3A6
		[Localizable(true)]
		public virtual string UserNameLabelText
		{
			get
			{
				object obj = this.ViewState["UserNameLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("User Name:");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("UserNameLabelText");
					return;
				}
				this.ViewState["UserNameLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the user name field is left blank.</summary>
		/// <returns>The error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the user name field is left blank. The default is "User Name." </returns>
		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x0006637C File Offset: 0x0006457C
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x0004F202 File Offset: 0x0004D402
		[Localizable(true)]
		public virtual string UserNameRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["UserNameRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("User Name is required.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("UserNameRequiredErrorMessage");
					return;
				}
				this.ViewState["UserNameRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages associated with validators used by the <see cref="T:System.Web.UI.WebControls.Login" /> control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> containing the style settings.</returns>
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x000663AE File Offset: 0x000645AE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style ValidatorTextStyle
		{
			get
			{
				if (this.validatorTextStyle == null)
				{
					this.validatorTextStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.validatorTextStyle).TrackViewState();
					}
				}
				return this.validatorTextStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether to show the <see cref="T:System.Web.UI.WebControls.Login" /> control after the user is authenticated.</summary>
		/// <returns>false if the <see cref="T:System.Web.UI.WebControls.Login" /> control should be hidden when the user is authenticated; otherwise, true. The default is true.</returns>
		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x000663DC File Offset: 0x000645DC
		// (set) Token: 0x06002834 RID: 10292 RVA: 0x00066405 File Offset: 0x00064605
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool VisibleWhenLoggedIn
		{
			get
			{
				object obj = this.ViewState["VisibleWhenLoggedIn"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["VisibleWhenLoggedIn"] = value;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x0006641D File Offset: 0x0006461D
		private Login.LoginContainer LoginTemplateContainer
		{
			get
			{
				if (this.container == null)
				{
					this.container = new Login.LoginContainer(this);
				}
				return this.container;
			}
		}

		/// <summary>Creates the individual controls that make up the <see cref="T:System.Web.UI.WebControls.Login" /> control and associates event handlers with their events.</summary>
		// Token: 0x06002836 RID: 10294 RVA: 0x0006643C File Offset: 0x0006463C
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			ITemplate template = this.LayoutTemplate;
			if (template == null)
			{
				template = new Login.LoginTemplate(this);
			}
			this.LoginTemplateContainer.InstantiateTemplate(template);
			this.Controls.Add(this.container);
			IEditableTextControl editableTextControl = this.container.UserNameTextBox as IEditableTextControl;
			if (editableTextControl == null)
			{
				throw new HttpException("LayoutTemplate does not contain an IEditableTextControl with ID UserName for the username.");
			}
			editableTextControl.Text = this.UserName;
			editableTextControl.TextChanged += this.UserName_TextChanged;
			editableTextControl = this.container.PasswordTextBox as IEditableTextControl;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.Password_TextChanged;
				ICheckBoxControl checkBoxControl = this.container.RememberMeCheckBox as ICheckBoxControl;
				if (checkBoxControl != null)
				{
					checkBoxControl.CheckedChanged += this.RememberMe_CheckedChanged;
				}
				return;
			}
			throw new HttpException("LayoutTemplate does not contain an IEditableTextControl with ID Password for the password.");
		}

		/// <param name="savedState">The <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> to load.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.PageStatePersister.ViewState" />.</exception>
		// Token: 0x06002837 RID: 10295 RVA: 0x0006651C File Offset: 0x0006471C
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.LoginButtonStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.LabelStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.TextBoxStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.HyperLinkStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.InstructionTextStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.TitleTextStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.CheckBoxStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.FailureTextStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.ValidatorTextStyle).LoadViewState(array[9]);
			}
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000665F1 File Offset: 0x000647F1
		private bool HasOnAuthenticateHandler()
		{
			return base.Events[Login.authenticateEvent] != null;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.Authenticate" /> event to authenticate the user.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /> that contains the event data. </param>
		// Token: 0x06002839 RID: 10297 RVA: 0x00066608 File Offset: 0x00064808
		protected virtual void OnAuthenticate(AuthenticateEventArgs e)
		{
			AuthenticateEventHandler authenticateEventHandler = (AuthenticateEventHandler)base.Events[Login.authenticateEvent];
			if (authenticateEventHandler != null)
			{
				authenticateEventHandler(this, e);
			}
		}

		/// <summary>Determines whether to pass an event up the page's user interface (UI) server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> containing the data for the event. </param>
		// Token: 0x0600283A RID: 10298 RVA: 0x00066638 File Offset: 0x00064838
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs e2 = e as CommandEventArgs;
			if (e2 != null && string.Equals(e2.CommandName, Login.LoginButtonCommandName, StringComparison.InvariantCultureIgnoreCase))
			{
				if (!this.AuthenticateUser())
				{
					ITextControl failureTextLiteral = this.LoginTemplateContainer.FailureTextLiteral;
					if (failureTextLiteral != null)
					{
						failureTextLiteral.Text = this.FailureText;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.LoggedIn" /> event after the user logs in to the Web site and has been authenticated.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600283B RID: 10299 RVA: 0x00066688 File Offset: 0x00064888
		protected virtual void OnLoggedIn(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Login.loggedInEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.LoggingIn" /> event when a user submits login information but before the authentication takes place.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> containing the event data.</param>
		// Token: 0x0600283C RID: 10300 RVA: 0x000666B8 File Offset: 0x000648B8
		protected virtual void OnLoggingIn(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[Login.loggingInEvent];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.LoginError" /> event when a login attempt fails.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600283D RID: 10301 RVA: 0x000666E8 File Offset: 0x000648E8
		protected virtual void OnLoginError(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Login.loginErrorEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x0600283E RID: 10302 RVA: 0x0003EF60 File Offset: 0x0003D160
		[global::System.MonoTODO("overriden for ?")]
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the login form using the specified HTML writer.</summary>
		/// <param name="writer">The HMTL writer.</param>
		// Token: 0x0600283F RID: 10303 RVA: 0x00066718 File Offset: 0x00064918
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.VerifyInlinePropertiesNotSet();
			if (!this.VisibleWhenLoggedIn && !this.IsDefaultLoginPage() && this.IsLoggedIn())
			{
				return;
			}
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			this.EnsureChildControls();
			foreach (object obj in this.styles)
			{
				object[] array = (object[])obj;
				((WebControl)array[0]).ApplyStyle((Style)array[1]);
			}
			this.RenderContents(writer);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000667BC File Offset: 0x000649BC
		protected override object SaveViewState()
		{
			object[] array = new object[10];
			array[0] = base.SaveViewState();
			if (this.logonButtonStyle != null)
			{
				array[1] = ((IStateManager)this.logonButtonStyle).SaveViewState();
			}
			if (this.labelStyle != null)
			{
				array[2] = ((IStateManager)this.labelStyle).SaveViewState();
			}
			if (this.textBoxStyle != null)
			{
				array[3] = ((IStateManager)this.textBoxStyle).SaveViewState();
			}
			if (this.hyperLinkStyle != null)
			{
				array[4] = ((IStateManager)this.hyperLinkStyle).SaveViewState();
			}
			if (this.instructionTextStyle != null)
			{
				array[5] = ((IStateManager)this.instructionTextStyle).SaveViewState();
			}
			if (this.titleTextStyle != null)
			{
				array[6] = ((IStateManager)this.titleTextStyle).SaveViewState();
			}
			if (this.checkBoxStyle != null)
			{
				array[7] = ((IStateManager)this.checkBoxStyle).SaveViewState();
			}
			if (this.failureTextStyle != null)
			{
				array[8] = ((IStateManager)this.failureTextStyle).SaveViewState();
			}
			if (this.validatorTextStyle != null)
			{
				array[9] = ((IStateManager)this.validatorTextStyle).SaveViewState();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[0] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the state of the <see cref="T:System.Web.UI.WebControls.Login" /> control.</param>
		// Token: 0x06002841 RID: 10305 RVA: 0x0004FA84 File Offset: 0x0004DC84
		[global::System.MonoTODO("for design-time usage - no more details available")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			base.SetDesignModeState(data);
		}

		/// <summary>Overrides the base <see cref="M:System.Web.UI.Control.TrackViewState" /> method.</summary>
		// Token: 0x06002842 RID: 10306 RVA: 0x000668B8 File Offset: 0x00064AB8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.logonButtonStyle != null)
			{
				((IStateManager)this.logonButtonStyle).TrackViewState();
			}
			if (this.labelStyle != null)
			{
				((IStateManager)this.labelStyle).TrackViewState();
			}
			if (this.textBoxStyle != null)
			{
				((IStateManager)this.textBoxStyle).TrackViewState();
			}
			if (this.hyperLinkStyle != null)
			{
				((IStateManager)this.hyperLinkStyle).TrackViewState();
			}
			if (this.instructionTextStyle != null)
			{
				((IStateManager)this.instructionTextStyle).TrackViewState();
			}
			if (this.titleTextStyle != null)
			{
				((IStateManager)this.titleTextStyle).TrackViewState();
			}
			if (this.checkBoxStyle != null)
			{
				((IStateManager)this.checkBoxStyle).TrackViewState();
			}
			if (this.failureTextStyle != null)
			{
				((IStateManager)this.failureTextStyle).TrackViewState();
			}
			if (this.validatorTextStyle != null)
			{
				((IStateManager)this.validatorTextStyle).TrackViewState();
			}
		}

		/// <summary>Occurs when a user is authenticated.</summary>
		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06002843 RID: 10307 RVA: 0x00066976 File Offset: 0x00064B76
		// (remove) Token: 0x06002844 RID: 10308 RVA: 0x00066989 File Offset: 0x00064B89
		public event AuthenticateEventHandler Authenticate
		{
			add
			{
				base.Events.AddHandler(Login.authenticateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.authenticateEvent, value);
			}
		}

		/// <summary>Occurs when the user logs in to the Web site and has been authenticated.</summary>
		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x06002845 RID: 10309 RVA: 0x0006699C File Offset: 0x00064B9C
		// (remove) Token: 0x06002846 RID: 10310 RVA: 0x000669AF File Offset: 0x00064BAF
		public event EventHandler LoggedIn
		{
			add
			{
				base.Events.AddHandler(Login.loggedInEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.loggedInEvent, value);
			}
		}

		/// <summary>Occurs when a user submits login information, before authentication takes place.</summary>
		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06002847 RID: 10311 RVA: 0x000669C2 File Offset: 0x00064BC2
		// (remove) Token: 0x06002848 RID: 10312 RVA: 0x000669D5 File Offset: 0x00064BD5
		public event LoginCancelEventHandler LoggingIn
		{
			add
			{
				base.Events.AddHandler(Login.loggingInEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.loggingInEvent, value);
			}
		}

		/// <summary>Occurs when a login error is detected.</summary>
		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06002849 RID: 10313 RVA: 0x000669E8 File Offset: 0x00064BE8
		// (remove) Token: 0x0600284A RID: 10314 RVA: 0x000669FB File Offset: 0x00064BFB
		public event EventHandler LoginError
		{
			add
			{
				base.Events.AddHandler(Login.loginErrorEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.loginErrorEvent, value);
			}
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x00066A0E File Offset: 0x00064C0E
		internal void RegisterApplyStyle(WebControl control, Style style)
		{
			this.styles.Add(new object[] { control, style });
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x00066A2C File Offset: 0x00064C2C
		private bool AuthenticateUser()
		{
			if (!this.Page.IsValid)
			{
				return true;
			}
			LoginCancelEventArgs e = new LoginCancelEventArgs();
			this.OnLoggingIn(e);
			if (e.Cancel)
			{
				return true;
			}
			AuthenticateEventArgs e2 = new AuthenticateEventArgs();
			if (!this.HasOnAuthenticateHandler())
			{
				string membershipProvider = this.MembershipProvider;
				MembershipProvider membershipProvider2 = ((membershipProvider.Length == 0) ? Membership.Provider : Membership.Providers[membershipProvider]);
				if (membershipProvider2 == null)
				{
					throw new HttpException(global::Locale.GetText("No provider named '{0}' could be found.", new object[] { membershipProvider }));
				}
				e2.Authenticated = membershipProvider2.ValidateUser(this.UserName, this.Password);
			}
			this.OnAuthenticate(e2);
			if (e2.Authenticated)
			{
				FormsAuthentication.SetAuthCookie(this.UserName, this.RememberMeSet);
				this.OnLoggedIn(EventArgs.Empty);
				string destinationPageUrl = this.DestinationPageUrl;
				if (this.Page.Request.Path.StartsWith(FormsAuthentication.LoginUrl, StringComparison.InvariantCultureIgnoreCase))
				{
					if (!string.IsNullOrEmpty(FormsAuthentication.ReturnUrl))
					{
						this.Redirect(FormsAuthentication.ReturnUrl);
					}
					else if (!string.IsNullOrEmpty(this.DestinationPageUrl))
					{
						this.Redirect(destinationPageUrl);
					}
					else if (!string.IsNullOrEmpty(FormsAuthentication.DefaultUrl))
					{
						this.Redirect(FormsAuthentication.DefaultUrl);
					}
					else if (destinationPageUrl.Length == 0)
					{
						this.Refresh();
					}
				}
				else if (!string.IsNullOrEmpty(this.DestinationPageUrl))
				{
					this.Redirect(destinationPageUrl);
				}
				else
				{
					this.Refresh();
				}
				return true;
			}
			this.OnLoginError(EventArgs.Empty);
			if (this.FailureAction == LoginFailureAction.RedirectToLoginPage)
			{
				FormsAuthentication.RedirectToLoginPage();
			}
			return false;
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x00066BAC File Offset: 0x00064DAC
		[global::System.MonoTODO]
		private void LoginClick(object sender, CommandEventArgs e)
		{
			base.RaiseBubbleEvent(sender, e);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x00066BB8 File Offset: 0x00064DB8
		private bool IsDefaultLoginPage()
		{
			if (this.Page == null || this.Page.Request == null)
			{
				return false;
			}
			string loginUrl = FormsAuthentication.LoginUrl;
			if (loginUrl == null)
			{
				return false;
			}
			string absolutePath = this.Page.Request.Url.AbsolutePath;
			return string.Compare(loginUrl, 0, absolutePath, absolutePath.Length - loginUrl.Length, loginUrl.Length, true, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x00066C21 File Offset: 0x00064E21
		private bool IsLoggedIn()
		{
			return this.Page != null && this.Page.Request != null && this.Page.Request.IsAuthenticated;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x00066C4A File Offset: 0x00064E4A
		private void Redirect(string url)
		{
			if (this.Page != null && this.Page.Response != null)
			{
				this.Page.Response.Redirect(url);
			}
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00066C72 File Offset: 0x00064E72
		private void Refresh()
		{
			if (this.Page != null && this.Page.Response != null)
			{
				this.Page.Response.Redirect(this.Page.Request.RawUrl);
			}
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00066CA9 File Offset: 0x00064EA9
		private void UserName_TextChanged(object sender, EventArgs e)
		{
			this.UserName = ((ITextControl)sender).Text;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00066CBC File Offset: 0x00064EBC
		private void Password_TextChanged(object sender, EventArgs e)
		{
			this._password = ((ITextControl)sender).Text;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x00066CCF File Offset: 0x00064ECF
		private void RememberMe_CheckedChanged(object sender, EventArgs e)
		{
			this.RememberMeSet = ((ICheckBoxControl)sender).Checked;
		}

		/// <summary>Represents the command name associated with the login button.</summary>
		// Token: 0x04001A68 RID: 6760
		public static readonly string LoginButtonCommandName = "Login";

		// Token: 0x04001A69 RID: 6761
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001A6A RID: 6762
		private static readonly object loggedInEvent = new object();

		// Token: 0x04001A6B RID: 6763
		private static readonly object loggingInEvent = new object();

		// Token: 0x04001A6C RID: 6764
		private static readonly object loginErrorEvent = new object();

		// Token: 0x04001A6D RID: 6765
		private TableItemStyle checkBoxStyle;

		// Token: 0x04001A6E RID: 6766
		private TableItemStyle failureTextStyle;

		// Token: 0x04001A6F RID: 6767
		private TableItemStyle hyperLinkStyle;

		// Token: 0x04001A70 RID: 6768
		private TableItemStyle instructionTextStyle;

		// Token: 0x04001A71 RID: 6769
		private TableItemStyle labelStyle;

		// Token: 0x04001A72 RID: 6770
		private Style logonButtonStyle;

		// Token: 0x04001A73 RID: 6771
		private Style textBoxStyle;

		// Token: 0x04001A74 RID: 6772
		private TableItemStyle titleTextStyle;

		// Token: 0x04001A75 RID: 6773
		private Style validatorTextStyle;

		// Token: 0x04001A76 RID: 6774
		private ArrayList styles = new ArrayList();

		// Token: 0x04001A77 RID: 6775
		private ITemplate layoutTemplate;

		// Token: 0x04001A78 RID: 6776
		private Login.LoginContainer container;

		// Token: 0x04001A79 RID: 6777
		private string _password;

		// Token: 0x04001A7A RID: 6778
		private bool renderOuterTable = true;

		// Token: 0x020003C3 RID: 963
		private sealed class LoginContainer : Control
		{
			// Token: 0x06002856 RID: 10326 RVA: 0x00066D16 File Offset: 0x00064F16
			public LoginContainer(Login owner)
			{
				this._owner = owner;
				this.renderOuterTable = this._owner.RenderOuterTable;
				if (this.renderOuterTable)
				{
					this.InitTable();
				}
			}

			// Token: 0x17000CE6 RID: 3302
			// (get) Token: 0x06002857 RID: 10327 RVA: 0x00066D44 File Offset: 0x00064F44
			// (set) Token: 0x06002858 RID: 10328 RVA: 0x00066D51 File Offset: 0x00064F51
			public override string ID
			{
				get
				{
					return this._owner.ID;
				}
				set
				{
					this._owner.ID = value;
				}
			}

			// Token: 0x17000CE7 RID: 3303
			// (get) Token: 0x06002859 RID: 10329 RVA: 0x00066D5F File Offset: 0x00064F5F
			public override string ClientID
			{
				get
				{
					return this._owner.ClientID;
				}
			}

			// Token: 0x0600285A RID: 10330 RVA: 0x00066D6C File Offset: 0x00064F6C
			public void InstantiateTemplate(ITemplate template)
			{
				if (!this.renderOuterTable)
				{
					template.InstantiateIn(this);
					return;
				}
				template.InstantiateIn(this._containerCell);
			}

			// Token: 0x0600285B RID: 10331 RVA: 0x00066D8C File Offset: 0x00064F8C
			private void InitTable()
			{
				this._table = new Table();
				this._containerCell = new TableCell();
				TableRow tableRow = new TableRow();
				tableRow.Cells.Add(this._containerCell);
				this._table.Rows.Add(tableRow);
				this.Controls.AddAt(0, this._table);
			}

			// Token: 0x0600285C RID: 10332 RVA: 0x00066DEC File Offset: 0x00064FEC
			protected internal override void Render(HtmlTextWriter writer)
			{
				if (this._table != null)
				{
					this._table.CellSpacing = 0;
					this._table.CellPadding = this._owner.BorderPadding;
					this._table.ApplyStyle(this._owner.ControlStyle);
					this._table.Attributes.CopyFrom(this._owner.Attributes);
				}
				base.Render(writer);
			}

			// Token: 0x17000CE8 RID: 3304
			// (get) Token: 0x0600285D RID: 10333 RVA: 0x00066E5B File Offset: 0x0006505B
			public Control UserNameTextBox
			{
				get
				{
					return this.FindControl("UserName");
				}
			}

			// Token: 0x17000CE9 RID: 3305
			// (get) Token: 0x0600285E RID: 10334 RVA: 0x000502E1 File Offset: 0x0004E4E1
			public Control PasswordTextBox
			{
				get
				{
					return this.FindControl("Password");
				}
			}

			// Token: 0x17000CEA RID: 3306
			// (get) Token: 0x0600285F RID: 10335 RVA: 0x00066E68 File Offset: 0x00065068
			public Control RememberMeCheckBox
			{
				get
				{
					return this.FindControl("RememberMe");
				}
			}

			// Token: 0x17000CEB RID: 3307
			// (get) Token: 0x06002860 RID: 10336 RVA: 0x0004AF0D File Offset: 0x0004910D
			public ITextControl FailureTextLiteral
			{
				get
				{
					return this.FindControl("FailureText") as ITextControl;
				}
			}

			// Token: 0x04001A7B RID: 6779
			private readonly Login _owner;

			// Token: 0x04001A7C RID: 6780
			private bool renderOuterTable;

			// Token: 0x04001A7D RID: 6781
			private Table _table;

			// Token: 0x04001A7E RID: 6782
			private TableCell _containerCell;
		}

		// Token: 0x020003C4 RID: 964
		private sealed class LoginTemplate : WebControl, ITemplate
		{
			// Token: 0x06002861 RID: 10337 RVA: 0x00066E75 File Offset: 0x00065075
			public LoginTemplate(Login login)
			{
				this._login = login;
			}

			// Token: 0x06002862 RID: 10338 RVA: 0x00066E84 File Offset: 0x00065084
			void ITemplate.InstantiateIn(Control container)
			{
				LiteralControl literalControl = new LiteralControl(this._login.TitleText);
				LiteralControl literalControl2 = new LiteralControl(this._login.InstructionText);
				TextBox textBox = new TextBox();
				textBox.ID = "UserName";
				textBox.Text = this._login.UserName;
				this._login.RegisterApplyStyle(textBox, this._login.TextBoxStyle);
				Label label = new Label();
				label.ID = "UserNameLabel";
				label.AssociatedControlID = "UserName";
				label.Text = this._login.UserNameLabelText;
				RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
				requiredFieldValidator.ID = "UserNameRequired";
				requiredFieldValidator.ControlToValidate = "UserName";
				requiredFieldValidator.ErrorMessage = this._login.UserNameRequiredErrorMessage;
				requiredFieldValidator.ToolTip = this._login.UserNameRequiredErrorMessage;
				requiredFieldValidator.Text = "*";
				requiredFieldValidator.ValidationGroup = this._login.ID;
				this._login.RegisterApplyStyle(requiredFieldValidator, this._login.ValidatorTextStyle);
				TextBox textBox2 = new TextBox();
				textBox2.ID = "Password";
				textBox2.TextMode = TextBoxMode.Password;
				this._login.RegisterApplyStyle(textBox2, this._login.TextBoxStyle);
				Label label2 = new Label();
				label2.ID = "PasswordLabel";
				label2.AssociatedControlID = "PasswordLabel";
				label2.Text = this._login.PasswordLabelText;
				RequiredFieldValidator requiredFieldValidator2 = new RequiredFieldValidator();
				requiredFieldValidator2.ID = "PasswordRequired";
				requiredFieldValidator2.ControlToValidate = "Password";
				requiredFieldValidator2.ErrorMessage = this._login.PasswordRequiredErrorMessage;
				requiredFieldValidator2.ToolTip = this._login.PasswordRequiredErrorMessage;
				requiredFieldValidator2.Text = "*";
				requiredFieldValidator2.ValidationGroup = this._login.ID;
				this._login.RegisterApplyStyle(requiredFieldValidator2, this._login.ValidatorTextStyle);
				bool flag = this._login == null || this._login.DisplayRememberMe;
				CheckBox checkBox;
				if (flag)
				{
					checkBox = new CheckBox();
					checkBox.ID = "RememberMe";
					checkBox.Checked = this._login.RememberMeSet;
					checkBox.Text = this._login.RememberMeText;
					this._login.RegisterApplyStyle(checkBox, this._login.CheckBoxStyle);
				}
				else
				{
					checkBox = null;
				}
				Literal literal = new Literal();
				literal.ID = "FailureText";
				literal.EnableViewState = false;
				WebControl webControl = null;
				switch (this._login.LoginButtonType)
				{
				case ButtonType.Button:
					webControl = new Button();
					webControl.ID = "LoginButton";
					break;
				case ButtonType.Image:
					webControl = new ImageButton();
					webControl.ID = "LoginImageButton";
					break;
				case ButtonType.Link:
					webControl = new LinkButton();
					webControl.ID = "LoginLinkButton";
					break;
				}
				this._login.RegisterApplyStyle(webControl, this._login.LoginButtonStyle);
				webControl.ID = "LoginButton";
				((IButtonControl)webControl).Text = this._login.LoginButtonText;
				((IButtonControl)webControl).CommandName = Login.LoginButtonCommandName;
				((IButtonControl)webControl).Command += this._login.LoginClick;
				((IButtonControl)webControl).ValidationGroup = this._login.ID;
				Table table = new Table();
				table.CellPadding = 0;
				table.Rows.Add(this.CreateRow(this.CreateCell(literalControl, null, this._login.TitleTextStyle, HorizontalAlign.Center)));
				if (this._login.InstructionText.Length > 0)
				{
					table.Rows.Add(this.CreateRow(this.CreateCell(literalControl2, null, this._login.instructionTextStyle, HorizontalAlign.Center)));
				}
				if (this._login.Orientation == Orientation.Horizontal)
				{
					TableRow tableRow = new TableRow();
					TableRow tableRow2 = new TableRow();
					if (this._login.TextLayout == LoginTextLayout.TextOnTop)
					{
						tableRow.Cells.Add(this.CreateCell(label, null, this._login.LabelStyle));
					}
					else
					{
						tableRow2.Cells.Add(this.CreateCell(label, null, this._login.LabelStyle));
					}
					tableRow2.Cells.Add(this.CreateCell(textBox, requiredFieldValidator, null));
					if (this._login.TextLayout == LoginTextLayout.TextOnTop)
					{
						tableRow.Cells.Add(this.CreateCell(label2, null, this._login.LabelStyle));
					}
					else
					{
						tableRow2.Cells.Add(this.CreateCell(label2, null, this._login.LabelStyle));
					}
					tableRow2.Cells.Add(this.CreateCell(textBox2, requiredFieldValidator2, null));
					if (flag)
					{
						tableRow2.Cells.Add(this.CreateCell(checkBox, null, null));
					}
					tableRow2.Cells.Add(this.CreateCell(webControl, null, null));
					if (tableRow.Cells.Count > 0)
					{
						table.Rows.Add(tableRow);
					}
					table.Rows.Add(tableRow2);
				}
				else
				{
					if (this._login.TextLayout == LoginTextLayout.TextOnLeft)
					{
						table.Rows.Add(this.CreateRow(label, textBox, requiredFieldValidator, this._login.LabelStyle));
					}
					else
					{
						table.Rows.Add(this.CreateRow(label, null, null, this._login.LabelStyle));
						table.Rows.Add(this.CreateRow(null, textBox, requiredFieldValidator, null));
					}
					if (this._login.TextLayout == LoginTextLayout.TextOnLeft)
					{
						table.Rows.Add(this.CreateRow(label2, textBox2, requiredFieldValidator2, this._login.LabelStyle));
					}
					else
					{
						table.Rows.Add(this.CreateRow(label2, null, null, this._login.LabelStyle));
						table.Rows.Add(this.CreateRow(null, textBox2, requiredFieldValidator2, null));
					}
					if (flag)
					{
						table.Rows.Add(this.CreateRow(this.CreateCell(checkBox, null, null)));
					}
					table.Rows.Add(this.CreateRow(this.CreateCell(webControl, null, null, HorizontalAlign.Right)));
				}
				if (this._login.FailureTextStyle.ForeColor.IsEmpty)
				{
					this._login.FailureTextStyle.ForeColor = Color.Red;
				}
				table.Rows.Add(this.CreateRow(this.CreateCell(literal, null, this._login.FailureTextStyle)));
				TableCell tableCell = new TableCell();
				this._login.RegisterApplyStyle(tableCell, this._login.HyperLinkStyle);
				if (this.AddLink(this._login.CreateUserUrl, this._login.CreateUserText, this._login.CreateUserIconUrl, tableCell, this._login.HyperLinkStyle))
				{
					if (this._login.Orientation == Orientation.Vertical)
					{
						tableCell.Controls.Add(new LiteralControl("<br/>"));
					}
					else
					{
						tableCell.Controls.Add(new LiteralControl(" "));
					}
				}
				if (this.AddLink(this._login.PasswordRecoveryUrl, this._login.PasswordRecoveryText, this._login.PasswordRecoveryIconUrl, tableCell, this._login.HyperLinkStyle))
				{
					if (this._login.Orientation == Orientation.Vertical)
					{
						tableCell.Controls.Add(new LiteralControl("<br/>"));
					}
					else
					{
						tableCell.Controls.Add(new LiteralControl(" "));
					}
				}
				this.AddLink(this._login.HelpPageUrl, this._login.HelpPageText, this._login.HelpPageIconUrl, tableCell, this._login.HyperLinkStyle);
				table.Rows.Add(this.CreateRow(tableCell));
				this.FixTableColumnSpans(table);
				container.Controls.Add(table);
			}

			// Token: 0x06002863 RID: 10339 RVA: 0x0006766F File Offset: 0x0006586F
			private TableRow CreateRow(TableCell cell)
			{
				return new TableRow
				{
					Cells = { cell }
				};
			}

			// Token: 0x06002864 RID: 10340 RVA: 0x00067684 File Offset: 0x00065884
			private TableRow CreateRow(Control c0, Control c1, Control c2, Style s)
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				TableCell tableCell2 = new TableCell();
				if (c0 != null)
				{
					tableCell.Controls.Add(c0);
					tableRow.Controls.Add(tableCell);
				}
				if (s != null)
				{
					tableCell.ApplyStyle(s);
				}
				if (c1 != null && c2 != null)
				{
					tableCell2.Controls.Add(c1);
					tableCell2.Controls.Add(c2);
					tableCell.HorizontalAlign = HorizontalAlign.Right;
					tableRow.Controls.Add(tableCell2);
				}
				return tableRow;
			}

			// Token: 0x06002865 RID: 10341 RVA: 0x000676FC File Offset: 0x000658FC
			private TableCell CreateCell(Control c0, Control c1, Style s, HorizontalAlign align)
			{
				TableCell tableCell = this.CreateCell(c0, c1, s);
				tableCell.HorizontalAlign = align;
				return tableCell;
			}

			// Token: 0x06002866 RID: 10342 RVA: 0x00067710 File Offset: 0x00065910
			private TableCell CreateCell(Control c0, Control c1, Style s)
			{
				TableCell tableCell = new TableCell();
				if (s != null)
				{
					tableCell.ApplyStyle(s);
				}
				tableCell.Controls.Add(c0);
				if (c1 != null)
				{
					tableCell.Controls.Add(c1);
				}
				return tableCell;
			}

			// Token: 0x06002867 RID: 10343 RVA: 0x0006774C File Offset: 0x0006594C
			private bool AddLink(string pageUrl, string linkText, string linkIcon, WebControl container, Style style)
			{
				bool flag = false;
				if (linkIcon.Length > 0)
				{
					Image image = new Image();
					image.ImageUrl = linkIcon;
					container.Controls.Add(image);
					flag = true;
				}
				if (linkText.Length > 0)
				{
					HyperLink hyperLink = new HyperLink();
					hyperLink.NavigateUrl = pageUrl;
					hyperLink.Text = linkText;
					this._login.RegisterApplyStyle(hyperLink, style);
					container.Controls.Add(hyperLink);
					flag = true;
				}
				return flag;
			}

			// Token: 0x06002868 RID: 10344 RVA: 0x000677BC File Offset: 0x000659BC
			private void FixTableColumnSpans(Table table)
			{
				int num = 0;
				for (int i = 0; i < table.Rows.Count; i++)
				{
					if (num < table.Rows[i].Cells.Count)
					{
						num = table.Rows[i].Cells.Count;
					}
				}
				for (int j = 0; j < table.Rows.Count; j++)
				{
					if (table.Rows[j].Cells.Count == 1 && num > 1)
					{
						table.Rows[j].Cells[0].ColumnSpan = num;
					}
				}
			}

			// Token: 0x04001A7F RID: 6783
			private readonly Login _login;
		}
	}
}
