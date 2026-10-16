using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.ModelBinding;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;
using Unity;

namespace System.Web.UI
{
	/// <summary>Represents an .aspx file, also known as a Web Forms page, requested from a server that hosts an ASP.NET Web application.</summary>
	// Token: 0x02000207 RID: 519
	[ToolboxItem(false)]
	[Designer("Microsoft.VisualStudio.Web.WebForms.WebFormDesigner, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.WebFormCodeDomSerializer, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.TypeCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("Load")]
	[DesignerCategory("ASPXCodeBehind")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Page : TemplateControl, IHttpHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Page" /> class.</summary>
		// Token: 0x06001444 RID: 5188 RVA: 0x0003456C File Offset: 0x0003276C
		public Page()
		{
			this.scriptManager = new ClientScriptManager(this);
			this.Page = this;
			this.ID = "__Page";
			PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
			if (pagesSection != null)
			{
				this.asyncTimeout = pagesSection.AsyncTimeout;
				this.viewStateEncryptionMode = pagesSection.ViewStateEncryptionMode;
				this._viewState = pagesSection.EnableViewState;
				this._viewStateMac = pagesSection.EnableViewStateMac;
			}
			else
			{
				this.asyncTimeout = TimeSpan.FromSeconds(45.0);
				this.viewStateEncryptionMode = ViewStateEncryptionMode.Auto;
				this._viewState = true;
			}
			this.ViewStateMode = ViewStateMode.Enabled;
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpApplicationState" /> object for the current Web request.</summary>
		/// <returns>The current data in the <see cref="T:System.Web.HttpApplicationState" /> class.</returns>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00034619 File Offset: 0x00032819
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpApplicationState Application
		{
			get
			{
				return this._application;
			}
		}

		/// <summary>Sets a value indicating whether the page can be executed on a single-threaded apartment (STA) thread.</summary>
		/// <returns>true if the page supports Active Server Pages (ASP) code; otherwise, false. The default is false.</returns>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x00003946 File Offset: 0x00001B46
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected bool AspCompatMode
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Sets a value indicating whether the page output is buffered.</summary>
		/// <returns>true if page output is buffered; otherwise, false. The default is true.</returns>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00034621 File Offset: 0x00032821
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x0003462E File Offset: 0x0003282E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool Buffer
		{
			get
			{
				return this.Response.BufferOutput;
			}
			set
			{
				this.Response.BufferOutput = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object associated with the application in which the page resides.</summary>
		/// <returns>The <see cref="T:System.Web.Caching.Cache" /> associated with the page's application.</returns>
		/// <exception cref="T:System.Web.HttpException">An instance of <see cref="T:System.Web.Caching.Cache" /> is not created. </exception>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0003463C File Offset: 0x0003283C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Cache Cache
		{
			get
			{
				if (this._cache == null)
				{
					throw new HttpException("Cache is not available.");
				}
				return this._cache;
			}
		}

		/// <summary>Gets or sets a value that allows you to override automatic detection of browser capabilities and to specify how a page is rendered for particular browser clients.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the browser capabilities that you want to override.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00034657 File Offset: 0x00032857
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x0003466D File Offset: 0x0003286D
		[Browsable(false)]
		[WebSysDescription("Value do override the automatic browser detection and force the page to use the specified browser.")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ClientTarget
		{
			get
			{
				if (this.clientTarget != null)
				{
					return this.clientTarget;
				}
				return string.Empty;
			}
			set
			{
				this.clientTarget = value;
				if (value == string.Empty)
				{
					this.clientTarget = null;
				}
			}
		}

		/// <summary>Sets the code page identifier for the current <see cref="T:System.Web.UI.Page" />.</summary>
		/// <returns>An integer that represents the code page identifier for the current <see cref="T:System.Web.UI.Page" />.</returns>
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0003468A File Offset: 0x0003288A
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x0003469C File Offset: 0x0003289C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int CodePage
		{
			get
			{
				return this.Response.ContentEncoding.CodePage;
			}
			set
			{
				this.Response.ContentEncoding = Encoding.GetEncoding(value);
			}
		}

		/// <summary>Sets the HTTP MIME type for the <see cref="T:System.Web.HttpResponse" /> object associated with the page.</summary>
		/// <returns>The HTTP MIME type associated with the current page.</returns>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x000346AF File Offset: 0x000328AF
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x000346BC File Offset: 0x000328BC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string ContentType
		{
			get
			{
				return this.Response.ContentType;
			}
			set
			{
				this.Response.ContentType = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object associated with the page.</summary>
		/// <returns>An <see cref="T:System.Web.HttpContext" /> object that contains information associated with the current page.</returns>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x000346CA File Offset: 0x000328CA
		protected internal override HttpContext Context
		{
			get
			{
				if (this._context == null)
				{
					return HttpContext.Current;
				}
				return this._context;
			}
		}

		/// <summary>Sets the culture ID for the <see cref="T:System.Threading.Thread" /> object associated with the page.</summary>
		/// <returns>A valid culture ID.</returns>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x000346E0 File Offset: 0x000328E0
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x000346F1 File Offset: 0x000328F1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string Culture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.Name;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = this.GetPageCulture(value, Thread.CurrentThread.CurrentCulture);
			}
		}

		/// <summary>Gets or sets a value indicating whether the page validates postback and callback events.</summary>
		/// <returns>true if the page validates postback and callback events; otherwise, false.The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.EnableEventValidation" /> property was set after the page was initialized.</exception>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0003470E File Offset: 0x0003290E
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x00034716 File Offset: 0x00032916
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue("true")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool EnableEventValidation
		{
			get
			{
				return this._eventValidation;
			}
			set
			{
				if (base.IsInited)
				{
					throw new InvalidOperationException("The 'EnableEventValidation' property can be set only in the Page_init, the Page directive or in the <pages> configuration section.");
				}
				this._eventValidation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the page maintains its view state, and the view state of any server controls it contains, when the current page request ends.</summary>
		/// <returns>true if the page maintains its view state; otherwise, false. The default is true.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00034732 File Offset: 0x00032932
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x0003473A File Offset: 0x0003293A
		[Browsable(false)]
		public override bool EnableViewState
		{
			get
			{
				return this._viewState;
			}
			set
			{
				this._viewState = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether ASP.NET should check message authentication codes (MAC) in the page's view state when the page is posted back from the client.</summary>
		/// <returns>true if the view state should be MAC checked and encoded; otherwise, false. The default is true.</returns>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00034743 File Offset: 0x00032943
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x0003474B File Offset: 0x0003294B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EnableViewStateMac
		{
			get
			{
				return this._viewStateMac;
			}
			set
			{
				this._viewStateMac = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00034743 File Offset: 0x00032943
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x0003474B File Offset: 0x0003294B
		internal bool EnableViewStateMacInternal
		{
			get
			{
				return this._viewStateMac;
			}
			set
			{
				this._viewStateMac = value;
			}
		}

		/// <summary>Gets or sets the error page to which the requesting browser is redirected in the event of an unhandled page exception.</summary>
		/// <returns>The error page to which the browser is redirected.</returns>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00034754 File Offset: 0x00032954
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x0003475C File Offset: 0x0003295C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebSysDescription("The URL of a page used for error redirection.")]
		[Browsable(false)]
		public string ErrorPage
		{
			get
			{
				return this._errorPage;
			}
			set
			{
				HttpContext context = this.Context;
				this._errorPage = value;
				if (context != null)
				{
					context.ErrorPage = value;
				}
			}
		}

		/// <summary>Sets an array of files that the current <see cref="T:System.Web.HttpResponse" /> object is dependent upon.</summary>
		/// <returns>The array of files that the current <see cref="T:System.Web.HttpResponse" /> object is dependent upon.</returns>
		// Token: 0x17000674 RID: 1652
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x00034781 File Offset: 0x00032981
		[Obsolete("The recommended alternative is HttpResponse.AddFileDependencies. http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected ArrayList FileDependencies
		{
			set
			{
				if (this.Response != null)
				{
					this.Response.AddFileDependencies(value);
				}
			}
		}

		/// <summary>Gets or sets an identifier for a particular instance of the <see cref="T:System.Web.UI.Page" /> class.</summary>
		/// <returns>The identifier for the instance of the <see cref="T:System.Web.UI.Page" /> class. The default value is '_Page'.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00034797 File Offset: 0x00032997
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x0003479F File Offset: 0x0003299F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		/// <summary>Gets a value that indicates whether the page is being rendered for the first time or is being loaded in response to a postback.</summary>
		/// <returns>true if the page is being loaded in response to a client postback; otherwise, false.</returns>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x000347A8 File Offset: 0x000329A8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPostBack
		{
			get
			{
				return this.isPostBack;
			}
		}

		/// <summary>Gets a value that indicates whether the control in the page that performs postbacks has been registered.</summary>
		/// <returns>true if the control has been registered; otherwise, false.</returns>
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x000347B0 File Offset: 0x000329B0
		public bool IsPostBackEventControlRegistered
		{
			get
			{
				return this.requiresRaiseEvent != null;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.Page" /> object can be reused.</summary>
		/// <returns>false in all cases. </returns>
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00008A4D File Offset: 0x00006C4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether page validation succeeded.</summary>
		/// <returns>true if page validation succeeded; otherwise, false.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.Page.IsValid" /> property is called before validation has occurred.</exception>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x000347BC File Offset: 0x000329BC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsValid
		{
			get
			{
				if (!this.is_validated)
				{
					throw new HttpException(global::Locale.GetText("Page.IsValid cannot be called before validation has taken place. It should be queried in the event handler for a control that has CausesValidation=True and initiated the postback, or after a call to Page.Validate."));
				}
				using (IEnumerator enumerator = this.Validators.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!((IValidator)enumerator.Current).IsValid)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		/// <summary>Gets a list of objects stored in the page context.</summary>
		/// <returns>A reference to an <see cref="T:System.Collections.IDictionary" /> containing objects stored in the page context.</returns>
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00034834 File Offset: 0x00032A34
		[Browsable(false)]
		public IDictionary Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new Hashtable();
				}
				return this.items;
			}
		}

		/// <summary>Sets the locale identifier for the <see cref="T:System.Threading.Thread" /> object associated with the page.</summary>
		/// <returns>The locale identifier to pass to the <see cref="T:System.Threading.Thread" />.</returns>
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0003484F File Offset: 0x00032A4F
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x00034860 File Offset: 0x00032A60
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int LCID
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.LCID;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
			}
		}

		/// <summary>Gets or sets a value indicating whether to return the user to the same position in the client browser after postback. This property replaces the obsolete <see cref="P:System.Web.UI.Page.SmartNavigation" /> property.</summary>
		/// <returns>true if the client position should be maintained; otherwise, false.</returns>
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00034872 File Offset: 0x00032A72
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x0003487A File Offset: 0x00032A7A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool MaintainScrollPositionOnPostBack
		{
			get
			{
				return this._maintainScrollPositionOnPostBack;
			}
			set
			{
				this._maintainScrollPositionOnPostBack = value;
			}
		}

		/// <summary>Gets the adapter that renders the page for the specific requesting browser.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Adapters.PageAdapter" /> that renders the page.</returns>
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00034883 File Offset: 0x00032A83
		public PageAdapter PageAdapter
		{
			get
			{
				return base.Adapter as PageAdapter;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x00034890 File Offset: 0x00032A90
		internal string WebFormScriptReference
		{
			get
			{
				if (this._webFormScriptReference == null)
				{
					this._webFormScriptReference = (this.IsMultiForm ? this.theForm : "window");
				}
				return this._webFormScriptReference;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x000348BC File Offset: 0x00032ABC
		internal string ValidationStartupScript
		{
			get
			{
				if (this._validationStartupScript == null)
				{
					this._validationStartupScript = string.Concat(new string[] { "\n", this.WebFormScriptReference, ".Page_ValidationActive = false;\n", this.WebFormScriptReference, ".ValidatorOnLoad();\n", this.WebFormScriptReference, ".ValidatorOnSubmit = function () {\n\tif (this.Page_ValidationActive) {\n\t\treturn this.ValidatorCommonOnSubmit();\n\t}\n\treturn true;\n};\n" });
				}
				return this._validationStartupScript;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00034923 File Offset: 0x00032B23
		internal string ValidationOnSubmitStatement
		{
			get
			{
				if (this._validationOnSubmitStatement == null)
				{
					this._validationOnSubmitStatement = "if (!" + this.WebFormScriptReference + ".ValidatorOnSubmit()) return false;";
				}
				return this._validationOnSubmitStatement;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0003494E File Offset: 0x00032B4E
		internal string ValidationInitializeScript
		{
			get
			{
				if (this._validationInitializeScript == null)
				{
					this._validationInitializeScript = "WebFormValidation_Initialize(" + this.WebFormScriptReference + ");";
				}
				return this._validationInitializeScript;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x00034979 File Offset: 0x00032B79
		internal IScriptManager ScriptManager
		{
			get
			{
				return (IScriptManager)this.Items[typeof(IScriptManager)];
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00034995 File Offset: 0x00032B95
		internal string theForm
		{
			get
			{
				return "theForm";
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00008A4D File Offset: 0x00006C4D
		internal bool IsMultiForm
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpRequest" /> object for the requested page.</summary>
		/// <returns>The current <see cref="T:System.Web.HttpRequest" /> associated with the page.</returns>
		/// <exception cref="T:System.Web.HttpException">Occurs when the <see cref="T:System.Web.HttpRequest" /> object is not available. </exception>
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0003499C File Offset: 0x00032B9C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpRequest Request
		{
			get
			{
				if (this._request == null)
				{
					throw new HttpException("Request is not available in this context.");
				}
				return this.RequestInternal;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x000349B7 File Offset: 0x00032BB7
		internal HttpRequest RequestInternal
		{
			get
			{
				return this._request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpResponse" /> object associated with the <see cref="T:System.Web.UI.Page" /> object. This object allows you to send HTTP response data to a client and contains information about that response.</summary>
		/// <returns>The current <see cref="T:System.Web.HttpResponse" /> associated with the page.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.HttpResponse" /> object is not available. </exception>
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x000349BF File Offset: 0x00032BBF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpResponse Response
		{
			get
			{
				if (this._response == null)
				{
					throw new HttpException("Response is not available in this context.");
				}
				return this._response;
			}
		}

		/// <summary>Sets the encoding language for the current <see cref="T:System.Web.HttpResponse" /> object.</summary>
		/// <returns>A string that contains the encoding language for the current <see cref="T:System.Web.HttpResponse" />.</returns>
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x000349DA File Offset: 0x00032BDA
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x000349EC File Offset: 0x00032BEC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string ResponseEncoding
		{
			get
			{
				return this.Response.ContentEncoding.WebName;
			}
			set
			{
				this.Response.ContentEncoding = Encoding.GetEncoding(value);
			}
		}

		/// <summary>Gets the Server object, which is an instance of the <see cref="T:System.Web.HttpServerUtility" /> class.</summary>
		/// <returns>The current Server object associated with the page.</returns>
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x000349FF File Offset: 0x00032BFF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpServerUtility Server
		{
			get
			{
				return this.Context.Server;
			}
		}

		/// <summary>Gets the current Session object provided by ASP.NET.</summary>
		/// <returns>The current session-state data.</returns>
		/// <exception cref="T:System.Web.HttpException">Occurs when the session information is set to null. </exception>
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00034A0C File Offset: 0x00032C0C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual HttpSessionState Session
		{
			get
			{
				if (this._session != null)
				{
					return this._session;
				}
				try
				{
					this._session = this.Context.Session;
				}
				catch
				{
				}
				if (this._session == null)
				{
					throw new HttpException("Session state can only be used when enableSessionState is set to true, either in a configuration file or in the Page directive.");
				}
				return this._session;
			}
		}

		/// <summary>Gets or sets a value indicating whether smart navigation is enabled. This property is deprecated.</summary>
		/// <returns>true if smart navigation is enabled; otherwise, false.</returns>
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00034A68 File Offset: 0x00032C68
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x00034A70 File Offset: 0x00032C70
		[Filterable(false)]
		[Obsolete("The recommended alternative is Page.SetFocus and Page.MaintainScrollPositionOnPostBack. http://go.microsoft.com/fwlink/?linkid=14202")]
		[Browsable(false)]
		public bool SmartNavigation
		{
			get
			{
				return this._smartNavigation;
			}
			set
			{
				this._smartNavigation = value;
			}
		}

		/// <summary>Gets or sets the name of the theme that is applied to the page early in the page life cycle.</summary>
		/// <returns>The name of the theme that is applied to the page early in the page life cycle.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.Page.StyleSheetTheme" /> property after the <see cref="M:System.Web.UI.Page.FrameworkInitialize" /> method was called.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Web.UI.Page.StyleSheetTheme" /> is set to an invalid theme name. This exception is thrown when the <see cref="M:System.Web.UI.Page.FrameworkInitialize" /> method is called, not by the property setter.</exception>
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00034A79 File Offset: 0x00032C79
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x00034A81 File Offset: 0x00032C81
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Filterable(false)]
		public virtual string StyleSheetTheme
		{
			get
			{
				return this._styleSheetTheme;
			}
			set
			{
				this._styleSheetTheme = value;
			}
		}

		/// <summary>Gets or sets the name of the page theme.</summary>
		/// <returns>The name of the page theme.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set <see cref="P:System.Web.UI.Page.Theme" /> after the <see cref="E:System.Web.UI.Page.PreInit" /> event has occurred.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Web.UI.Page.Theme" /> is set to an invalid theme name.</exception>
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00034A8A File Offset: 0x00032C8A
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x00034A92 File Offset: 0x00032C92
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string Theme
		{
			get
			{
				return this._theme;
			}
			set
			{
				this._theme = value;
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00034A9C File Offset: 0x00032C9C
		private void InitializeStyleSheet()
		{
			if (this._styleSheetTheme == null)
			{
				PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
				if (pagesSection != null)
				{
					this._styleSheetTheme = pagesSection.StyleSheetTheme;
				}
			}
			if (!string.IsNullOrEmpty(this._styleSheetTheme))
			{
				string text = "~/App_Themes/" + this._styleSheetTheme;
				this._styleSheetPageTheme = BuildManager.CreateInstanceFromVirtualPath(text, typeof(PageTheme)) as PageTheme;
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00034B0C File Offset: 0x00032D0C
		private void InitializeTheme()
		{
			if (this._theme == null)
			{
				PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
				if (pagesSection != null)
				{
					this._theme = pagesSection.Theme;
				}
			}
			if (!string.IsNullOrEmpty(this._theme))
			{
				string text = "~/App_Themes/" + this._theme;
				this._pageTheme = BuildManager.CreateInstanceFromVirtualPath(text, typeof(PageTheme)) as PageTheme;
				if (this._pageTheme != null)
				{
					this._pageTheme.SetPage(this);
				}
			}
		}

		/// <summary>Gets or sets the control in the page that is used to perform postbacks.</summary>
		/// <returns>The control that is used to perform postbacks.</returns>
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00034B8D File Offset: 0x00032D8D
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x00034B95 File Offset: 0x00032D95
		public Control AutoPostBackControl
		{
			get
			{
				return this._autoPostBackControl;
			}
			set
			{
				this._autoPostBackControl = value;
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.Routing.RequestContext.RouteData" /> value of the current <see cref="T:System.Web.Routing.RequestContext" /> instance.</summary>
		/// <returns>The <see cref="P:System.Web.Routing.RequestContext.RouteData" /> value of the current <see cref="T:System.Web.Routing.RequestContext" /> instance.</returns>
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00034BA0 File Offset: 0x00032DA0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RouteData RouteData
		{
			get
			{
				if (this._request == null)
				{
					return null;
				}
				RequestContext requestContext = this._request.RequestContext;
				if (requestContext == null)
				{
					return null;
				}
				return requestContext.RouteData;
			}
		}

		/// <summary>Gets or sets the content of the "description" meta element.</summary>
		/// <returns>The content of the "description" meta element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The page does not have a header control (a head element with the runat attribute set to "server"). </exception>
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00034BCE File Offset: 0x00032DCE
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x00034C0B File Offset: 0x00032E0B
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		public string MetaDescription
		{
			get
			{
				if (this._metaDescription != null)
				{
					return this._metaDescription;
				}
				if (this.htmlHeader != null)
				{
					return this.htmlHeader.Description;
				}
				if (this.frameworkInitialized)
				{
					throw new InvalidOperationException("A server-side head element is required to set this property.");
				}
				return string.Empty;
			}
			set
			{
				if (this.htmlHeader != null)
				{
					this.htmlHeader.Description = value;
					return;
				}
				if (this.frameworkInitialized)
				{
					throw new InvalidOperationException("A server-side head element is required to set this property.");
				}
				this._metaDescription = value;
			}
		}

		/// <summary>Gets or sets the content of the "keywords" meta element.</summary>
		/// <returns>The content of the "keywords" meta element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The page does not have a header control (a head element with the runat attribute set to "server"). </exception>
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00034C3C File Offset: 0x00032E3C
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x00034C79 File Offset: 0x00032E79
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		public string MetaKeywords
		{
			get
			{
				if (this._metaKeywords != null)
				{
					return this._metaDescription;
				}
				if (this.htmlHeader != null)
				{
					return this.htmlHeader.Keywords;
				}
				if (this.frameworkInitialized)
				{
					throw new InvalidOperationException("A server-side head element is required to set this property.");
				}
				return string.Empty;
			}
			set
			{
				if (this.htmlHeader != null)
				{
					this.htmlHeader.Keywords = value;
					return;
				}
				if (this.frameworkInitialized)
				{
					throw new InvalidOperationException("A server-side head element is required to set this property.");
				}
				this._metaKeywords = value;
			}
		}

		/// <summary>Gets or sets the title for the page.</summary>
		/// <returns>The title of the page.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.Title" /> property requires a header control on the page.</exception>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00034CAA File Offset: 0x00032EAA
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x00034CE1 File Offset: 0x00032EE1
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(true)]
		public string Title
		{
			get
			{
				if (this._title != null)
				{
					return this._title;
				}
				if (this.htmlHeader != null && this.htmlHeader.Title != null)
				{
					return this.htmlHeader.Title;
				}
				return string.Empty;
			}
			set
			{
				if (this.htmlHeader != null)
				{
					this.htmlHeader.Title = value;
					return;
				}
				this._title = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</summary>
		/// <returns>Data from the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</returns>
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00034CFF File Offset: 0x00032EFF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TraceContext Trace
		{
			get
			{
				return this.Context.Trace;
			}
		}

		/// <summary>Sets a value indicating whether tracing is enabled for the <see cref="T:System.Web.UI.Page" /> object.</summary>
		/// <returns>true if tracing is enabled for the page; otherwise, false. The default is false.</returns>
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00034D0C File Offset: 0x00032F0C
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x00034D19 File Offset: 0x00032F19
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool TraceEnabled
		{
			get
			{
				return this.Trace.IsEnabled;
			}
			set
			{
				this.Trace.IsEnabled = value;
			}
		}

		/// <summary>Sets the mode in which trace statements are displayed on the page.</summary>
		/// <returns>One of the <see cref="T:System.Web.TraceMode" /> enumeration members.</returns>
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x00034D27 File Offset: 0x00032F27
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x00034D34 File Offset: 0x00032F34
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TraceMode TraceModeValue
		{
			get
			{
				return this.Trace.TraceMode;
			}
			set
			{
				this.Trace.TraceMode = value;
			}
		}

		/// <summary>Sets the level of transaction support for the page.</summary>
		/// <returns>An integer that represents one of the <see cref="T:System.EnterpriseServices.TransactionOption" /> enumeration members.</returns>
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00034D42 File Offset: 0x00032F42
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x00034D4A File Offset: 0x00032F4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected int TransactionMode
		{
			get
			{
				return this._transactionMode;
			}
			set
			{
				this._transactionMode = value;
			}
		}

		/// <summary>Sets the user interface (UI) ID for the <see cref="T:System.Threading.Thread" /> object associated with the page.</summary>
		/// <returns>The UI ID.</returns>
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00034D53 File Offset: 0x00032F53
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x00034D64 File Offset: 0x00032F64
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string UICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.Name;
			}
			set
			{
				Thread.CurrentThread.CurrentUICulture = this.GetPageCulture(value, Thread.CurrentThread.CurrentUICulture);
			}
		}

		/// <summary>Gets information about the user making the page request.</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> that represents the user making the page request.</returns>
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00034D81 File Offset: 0x00032F81
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPrincipal User
		{
			get
			{
				return this.Context.User;
			}
		}

		/// <summary>Gets a collection of all validation controls contained on the requested page.</summary>
		/// <returns>The collection of validation controls.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00034D8E File Offset: 0x00032F8E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ValidatorCollection Validators
		{
			get
			{
				if (this._validators == null)
				{
					this._validators = new ValidatorCollection();
				}
				return this._validators;
			}
		}

		/// <summary>Assigns an identifier to an individual user in the view-state variable associated with the current page.</summary>
		/// <returns>The identifier for the individual user.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.Page.ViewStateUserKey" /> property was accessed too late during page processing. </exception>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00034DA9 File Offset: 0x00032FA9
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x00034DB1 File Offset: 0x00032FB1
		[Browsable(false)]
		[global::System.MonoTODO("Use this when encrypting/decrypting ViewState")]
		public string ViewStateUserKey
		{
			get
			{
				return this.viewStateUserKey;
			}
			set
			{
				this.viewStateUserKey = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.Page" /> object is rendered.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.Page" /> is to be rendered; otherwise, false. The default is true.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00034DBA File Offset: 0x00032FBA
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x00034DC2 File Offset: 0x00032FC2
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00034DCC File Offset: 0x00032FCC
		private CultureInfo GetPageCulture(string culture, CultureInfo deflt)
		{
			if (culture == null)
			{
				return deflt;
			}
			CultureInfo cultureInfo = null;
			if (culture.StartsWith("auto", StringComparison.InvariantCultureIgnoreCase))
			{
				string[] userLanguages = this.Request.UserLanguages;
				try
				{
					if (userLanguages != null && userLanguages.Length != 0)
					{
						cultureInfo = CultureInfo.CreateSpecificCulture(userLanguages[0]);
					}
				}
				catch
				{
				}
				if (cultureInfo == null)
				{
					cultureInfo = deflt;
				}
			}
			else
			{
				cultureInfo = CultureInfo.CreateSpecificCulture(culture);
			}
			return cultureInfo;
		}

		/// <summary>Initiates a request for Active Server Page (ASP) resources. This method is provided for compatibility with legacy ASP applications.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object.</returns>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> with information about the current request. </param>
		/// <param name="cb">The callback method. </param>
		/// <param name="extraData">Any extra data needed to process the request in the same manner as an ASP request. </param>
		// Token: 0x0600149A RID: 5274 RVA: 0x00003A2B File Offset: 0x00001C2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected IAsyncResult AspCompatBeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Terminates a request for Active Server Page (ASP) resources. This method is provided for compatibility with legacy ASP applications.</summary>
		/// <param name="result">The ASP page generated by the request. </param>
		// Token: 0x0600149B RID: 5275 RVA: 0x00003A2B File Offset: 0x00001C2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[global::System.MonoNotSupported("Mono does not support classic ASP compatibility mode.")]
		protected void AspCompatEndProcessRequest(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.HtmlTextWriter" /> object to render the page's content.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriter" /> or <see cref="T:System.Web.UI.Html32TextWriter" />.</returns>
		/// <param name="tw">The <see cref="T:System.IO.TextWriter" /> used to create the <see cref="T:System.Web.UI.HtmlTextWriter" />.</param>
		// Token: 0x0600149C RID: 5276 RVA: 0x00034E30 File Offset: 0x00033030
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual HtmlTextWriter CreateHtmlTextWriter(TextWriter tw)
		{
			if (this.Request.BrowserMightHaveSpecialWriter)
			{
				return this.Request.Browser.CreateHtmlTextWriter(tw);
			}
			return new HtmlTextWriter(tw);
		}

		/// <summary>Performs any initialization of the instance of the <see cref="T:System.Web.UI.Page" /> class that is required by RAD designers. This method is used only at design time.</summary>
		// Token: 0x0600149D RID: 5277 RVA: 0x00034E57 File Offset: 0x00033057
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void DesignerInitialize()
		{
			this.InitRecursive(null);
		}

		/// <summary>Returns a <see cref="T:System.Collections.Specialized.NameValueCollection" /> of data posted back to the page using either a POST or a GET command. </summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the form data. If the postback used the POST command, the form information is returned from the <see cref="P:System.Web.UI.Page.Context" /> object. If the postback used the GET command, the query string information is returned. If the page is being requested for the first time, null is returned.</returns>
		// Token: 0x0600149E RID: 5278 RVA: 0x00034E60 File Offset: 0x00033060
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual NameValueCollection DeterminePostBackMode()
		{
			if (this._context.IsProcessingInclude)
			{
				return null;
			}
			HttpRequest request = this.Request;
			if (request == null)
			{
				return null;
			}
			NameValueCollection nameValueCollection;
			if (string.Compare(this.Request.HttpMethod, "POST", true, Helpers.InvariantCulture) == 0)
			{
				nameValueCollection = request.Form;
			}
			else
			{
				string queryStringRaw = this.Request.QueryStringRaw;
				if (queryStringRaw == null || queryStringRaw.Length == 0)
				{
					return null;
				}
				nameValueCollection = request.QueryString;
			}
			WebROCollection webROCollection = (WebROCollection)nameValueCollection;
			this.allow_load = !webROCollection.GotID;
			if (this.allow_load)
			{
				webROCollection.ID = this.GetTypeHashCode();
			}
			else
			{
				this.allow_load = webROCollection.ID == this.GetTypeHashCode();
			}
			if (nameValueCollection != null && nameValueCollection["__VIEWSTATE"] == null && nameValueCollection["__EVENTTARGET"] == null)
			{
				return null;
			}
			return nameValueCollection;
		}

		/// <summary>Searches the page naming container for a server control with the specified identifier.</summary>
		/// <returns>The specified control, or null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found. </param>
		// Token: 0x0600149F RID: 5279 RVA: 0x00034F30 File Offset: 0x00033130
		public override Control FindControl(string id)
		{
			if (id == this.ID)
			{
				return this;
			}
			return base.FindControl(id);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00034F49 File Offset: 0x00033149
		private Control FindControl(string id, bool decode)
		{
			return this.FindControl(id);
		}

		/// <summary>Gets a reference that can be used in a client event to post back to the server for the specified control and with the specified event arguments.</summary>
		/// <returns>The string that represents the client event.</returns>
		/// <param name="control">The server control that receives the client event postback. </param>
		/// <param name="argument">A <see cref="T:System.String" /> that is passed to <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />. </param>
		// Token: 0x060014A1 RID: 5281 RVA: 0x00034F52 File Offset: 0x00033152
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202")]
		public string GetPostBackClientEvent(Control control, string argument)
		{
			return this.scriptManager.GetPostBackEventReference(control, argument);
		}

		/// <summary>Gets a reference, with javascript: appended to the beginning of it, that can be used in a client event to post back to the server for the specified control and with the specified event arguments.</summary>
		/// <returns>A string representing a JavaScript call to the postback function that includes the target control's ID and event arguments.</returns>
		/// <param name="control">The server control to process the postback. </param>
		/// <param name="argument">The parameter passed to the server control. </param>
		// Token: 0x060014A2 RID: 5282 RVA: 0x00034F61 File Offset: 0x00033161
		[Obsolete("The recommended alternative is ClientScript.GetPostBackClientHyperlink. http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string GetPostBackClientHyperlink(Control control, string argument)
		{
			return this.scriptManager.GetPostBackClientHyperlink(control, argument);
		}

		/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified <see cref="T:System.Web.UI.Control" /> object.</summary>
		/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
		/// <param name="control">The server control to process the postback on the server. </param>
		// Token: 0x060014A3 RID: 5283 RVA: 0x00034F70 File Offset: 0x00033170
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202")]
		public string GetPostBackEventReference(Control control)
		{
			return this.scriptManager.GetPostBackEventReference(control, string.Empty);
		}

		/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified control that handles the postback and a string argument of additional event information. </summary>
		/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
		/// <param name="control">The server control to process the postback. </param>
		/// <param name="argument">The parameter passed to the server control. </param>
		// Token: 0x060014A4 RID: 5284 RVA: 0x00034F52 File Offset: 0x00033152
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202")]
		public string GetPostBackEventReference(Control control, string argument)
		{
			return this.scriptManager.GetPostBackEventReference(control, argument);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00034F83 File Offset: 0x00033183
		internal void RequiresFormScriptDeclaration()
		{
			this.requiresFormScriptDeclaration = true;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00034F8C File Offset: 0x0003318C
		internal void RequiresPostBackScript()
		{
			if (this.requiresPostBackScript)
			{
				return;
			}
			this.ClientScript.RegisterHiddenField("__EVENTTARGET", string.Empty);
			this.ClientScript.RegisterHiddenField("__EVENTARGUMENT", string.Empty);
			this.requiresPostBackScript = true;
			this.RequiresFormScriptDeclaration();
		}

		/// <summary>Retrieves a hash code that is generated by <see cref="T:System.Web.UI.Page" /> objects that are generated at run time. This hash code is unique to the <see cref="T:System.Web.UI.Page" /> object's control hierarchy.</summary>
		/// <returns>The hash code generated at run time. The default is 0.</returns>
		// Token: 0x060014A7 RID: 5287 RVA: 0x00008A4D File Offset: 0x00006C4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int GetTypeHashCode()
		{
			return 0;
		}

		/// <summary>Initializes the output cache for the current page request based on an <see cref="T:System.Web.UI.OutputCacheParameters" /> object.</summary>
		/// <param name="cacheSettings">An <see cref="T:System.Web.UI.OutputCacheParameters" /> that contains the cache settings.</param>
		/// <exception cref="T:System.Web.HttpException">The cache profile was not found.- or -A missing directive or configuration settings profile attribute.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The output cache settings location is invalid. </exception>
		// Token: 0x060014A8 RID: 5288 RVA: 0x00034FDC File Offset: 0x000331DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[global::System.MonoTODO("The following properties of OutputCacheParameters are silently ignored: CacheProfile, SqlDependency")]
		protected internal virtual void InitOutputCache(OutputCacheParameters cacheSettings)
		{
			if (cacheSettings.Enabled)
			{
				this.InitOutputCache(cacheSettings.Duration, cacheSettings.VaryByContentEncoding, cacheSettings.VaryByHeader, cacheSettings.VaryByCustom, cacheSettings.Location, cacheSettings.VaryByParam);
				HttpResponse response = this.Response;
				HttpCachePolicy httpCachePolicy = ((response != null) ? response.Cache : null);
				if (httpCachePolicy != null && cacheSettings.NoStore)
				{
					httpCachePolicy.SetNoStore();
				}
			}
		}

		/// <summary>Initializes the output cache for the current page request.</summary>
		/// <param name="duration">The amount of time that objects stored in the output cache are valid.</param>
		/// <param name="varyByContentEncoding">A semicolon-separated list of character-sets (content encodings) that content from the output cache will vary by.</param>
		/// <param name="varyByHeader">A semicolon-separated list of headers that content from the output cache will vary by.</param>
		/// <param name="varyByCustom">The Vary HTTP header.</param>
		/// <param name="location">One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> values.</param>
		/// <param name="varyByParam">A semicolon-separated list of parameters received by a GET or POST method that content from the output cache will vary by.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An invalid value is specified for <paramref name="location" />. </exception>
		// Token: 0x060014A9 RID: 5289 RVA: 0x00035040 File Offset: 0x00033240
		[EditorBrowsable(EditorBrowsableState.Never)]
		[global::System.MonoTODO("varyByContentEncoding is not currently used")]
		protected virtual void InitOutputCache(int duration, string varyByContentEncoding, string varyByHeader, string varyByCustom, OutputCacheLocation location, string varyByParam)
		{
			if (duration <= 0)
			{
				return;
			}
			HttpResponse response = this.Response;
			HttpCachePolicy cache = response.Cache;
			bool flag = false;
			HttpContext context = this.Context;
			DateTime dateTime = ((context != null) ? context.Timestamp : DateTime.Now);
			switch (location)
			{
			case OutputCacheLocation.Any:
				cache.SetCacheability(HttpCacheability.Public);
				cache.SetMaxAge(new TimeSpan(0, 0, duration));
				cache.SetLastModified(dateTime);
				flag = true;
				break;
			case OutputCacheLocation.Client:
				cache.SetCacheability(HttpCacheability.Private);
				cache.SetMaxAge(new TimeSpan(0, 0, duration));
				cache.SetLastModified(dateTime);
				break;
			case OutputCacheLocation.Downstream:
				cache.SetCacheability(HttpCacheability.Public);
				cache.SetMaxAge(new TimeSpan(0, 0, duration));
				cache.SetLastModified(dateTime);
				break;
			case OutputCacheLocation.Server:
				cache.SetCacheability(HttpCacheability.Server);
				flag = true;
				break;
			}
			if (flag)
			{
				if (varyByCustom != null)
				{
					cache.SetVaryByCustom(varyByCustom);
				}
				if (varyByParam != null && varyByParam.Length > 0)
				{
					foreach (string text in varyByParam.Split(';', StringSplitOptions.None))
					{
						cache.VaryByParams[text.Trim()] = true;
					}
					cache.VaryByParams.IgnoreParams = false;
				}
				else
				{
					cache.VaryByParams.IgnoreParams = true;
				}
				if (varyByHeader != null && varyByHeader.Length > 0)
				{
					foreach (string text2 in varyByHeader.Split(';', StringSplitOptions.None))
					{
						cache.VaryByHeaders[text2.Trim()] = true;
					}
				}
				if (this.PageAdapter != null)
				{
					if (this.PageAdapter.CacheVaryByParams != null)
					{
						foreach (string text3 in this.PageAdapter.CacheVaryByParams)
						{
							cache.VaryByParams[text3] = true;
						}
					}
					if (this.PageAdapter.CacheVaryByHeaders != null)
					{
						foreach (string text4 in this.PageAdapter.CacheVaryByHeaders)
						{
							cache.VaryByHeaders[text4] = true;
						}
					}
				}
			}
			response.IsCached = true;
			cache.Duration = duration;
			cache.SetExpires(dateTime.AddSeconds((double)duration));
		}

		/// <summary>Initializes the output cache for the current page request.</summary>
		/// <param name="duration">The amount of time that objects stored in the output cache are valid. </param>
		/// <param name="varyByHeader">A semicolon-separated list of headers that content from the output cache will vary by. </param>
		/// <param name="varyByCustom">The Vary HTTP header. </param>
		/// <param name="location">One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> values. </param>
		/// <param name="varyByParam">A semicolon-separated list of parameters received by a GET or POST method that content from the output cache will vary by.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An invalid value is specified for <paramref name="location" />. </exception>
		// Token: 0x060014AA RID: 5290 RVA: 0x000352AC File Offset: 0x000334AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void InitOutputCache(int duration, string varyByHeader, string varyByCustom, OutputCacheLocation location, string varyByParam)
		{
			this.InitOutputCache(duration, null, varyByHeader, varyByCustom, location, varyByParam);
		}

		/// <summary>Determines whether the client script block with the specified key is registered with the page.</summary>
		/// <returns>true if the script block is registered; otherwise, false.</returns>
		/// <param name="key">The string key of the client script to search for. </param>
		// Token: 0x060014AB RID: 5291 RVA: 0x000352BC File Offset: 0x000334BC
		[Obsolete("The recommended alternative is ClientScript.IsClientScriptBlockRegistered(string key). http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool IsClientScriptBlockRegistered(string key)
		{
			return this.scriptManager.IsClientScriptBlockRegistered(key);
		}

		/// <summary>Determines whether the client startup script is registered with the <see cref="T:System.Web.UI.Page" /> object.</summary>
		/// <returns>true if the startup script is registered; otherwise, false.</returns>
		/// <param name="key">The string key of the startup script to search for. </param>
		// Token: 0x060014AC RID: 5292 RVA: 0x000352CA File Offset: 0x000334CA
		[Obsolete("The recommended alternative is ClientScript.IsStartupScriptRegistered(string key). http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool IsStartupScriptRegistered(string key)
		{
			return this.scriptManager.IsStartupScriptRegistered(key);
		}

		/// <summary>Retrieves the physical path that a virtual path, either absolute or relative, or an application-relative path maps to.</summary>
		/// <returns>The physical path associated with the virtual path or application-relative path.</returns>
		/// <param name="virtualPath">A <see cref="T:System.String" /> that represents the virtual path. </param>
		// Token: 0x060014AD RID: 5293 RVA: 0x000352D8 File Offset: 0x000334D8
		public string MapPath(string virtualPath)
		{
			return this.Request.MapPath(virtualPath);
		}

		/// <summary>Initializes the <see cref="T:System.Web.UI.HtmlTextWriter" /> object and calls on the child controls of the <see cref="T:System.Web.UI.Page" /> to render.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the page content.</param>
		// Token: 0x060014AE RID: 5294 RVA: 0x000352E8 File Offset: 0x000334E8
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.MaintainScrollPositionOnPostBack)
			{
				this.ClientScript.RegisterWebFormClientScript();
				this.ClientScript.RegisterHiddenField("__SCROLLPOSITIONX", this.Request["__SCROLLPOSITIONX"]);
				this.ClientScript.RegisterHiddenField("__SCROLLPOSITIONY", this.Request["__SCROLLPOSITIONY"]);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("<script type=\"text/javascript\">");
				stringBuilder.AppendLine("//<![CDATA[");
				stringBuilder.AppendLine(this.theForm + ".oldSubmit = " + this.theForm + ".submit;");
				stringBuilder.AppendLine(this.theForm + ".submit = function () { " + this.WebFormScriptReference + ".WebForm_SaveScrollPositionSubmit(); }");
				stringBuilder.AppendLine(this.theForm + ".oldOnSubmit = " + this.theForm + ".onsubmit;");
				stringBuilder.AppendLine(this.theForm + ".onsubmit = function () { " + this.WebFormScriptReference + ".WebForm_SaveScrollPositionOnSubmit(); }");
				if (this.IsPostBack)
				{
					stringBuilder.AppendLine(this.theForm + ".oldOnLoad = window.onload;");
					stringBuilder.AppendLine("window.onload = function () { " + this.WebFormScriptReference + ".WebForm_RestoreScrollPosition (); };");
				}
				stringBuilder.AppendLine("//]]>");
				stringBuilder.AppendLine("</script>");
				this.ClientScript.RegisterStartupScript(typeof(Page), "MaintainScrollPositionOnPostBackStartup", stringBuilder.ToString());
			}
			base.Render(writer);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0003546C File Offset: 0x0003366C
		private void RenderPostBackScript(HtmlTextWriter writer, string formUniqueID)
		{
			writer.WriteLine();
			ClientScriptManager.WriteBeginScriptBlock(writer);
			this.RenderClientScriptFormDeclaration(writer, formUniqueID);
			writer.WriteLine(this.WebFormScriptReference + "._form = " + this.theForm + ";");
			writer.WriteLine(this.WebFormScriptReference + ".__doPostBack = function (eventTarget, eventArgument) {");
			writer.WriteLine(string.Concat(new string[] { "\tif(", this.theForm, ".onsubmit && ", this.theForm, ".onsubmit() == false) return;" }));
			writer.WriteLine("\t" + this.theForm + ".__EVENTTARGET.value = eventTarget;");
			writer.WriteLine("\t" + this.theForm + ".__EVENTARGUMENT.value = eventArgument;");
			writer.WriteLine("\t" + this.theForm + ".submit();");
			writer.WriteLine("}");
			ClientScriptManager.WriteEndScriptBlock(writer);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00035564 File Offset: 0x00033764
		private void RenderClientScriptFormDeclaration(HtmlTextWriter writer, string formUniqueID)
		{
			if (this.formScriptDeclarationRendered)
			{
				return;
			}
			if (this.PageAdapter != null)
			{
				writer.WriteLine("\tvar {0} = {1};\n", this.theForm, this.PageAdapter.GetPostBackFormReference(formUniqueID));
			}
			else
			{
				writer.WriteLine("\tvar {0};\n\tif (document.getElementById) {{ {0} = document.getElementById ('{1}'); }}", this.theForm, formUniqueID);
				writer.WriteLine("\telse {{ {0} = document.{1}; }}", this.theForm, formUniqueID);
			}
			this.formScriptDeclarationRendered = true;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000355CC File Offset: 0x000337CC
		internal void OnFormRender(HtmlTextWriter writer, string formUniqueID)
		{
			if (this.renderingForm)
			{
				throw new HttpException("Only 1 HtmlForm is allowed per page.");
			}
			this.renderingForm = true;
			writer.WriteLine();
			if (this.requiresFormScriptDeclaration || (this.scriptManager != null && this.scriptManager.ScriptsPresent) || this.PageAdapter != null)
			{
				ClientScriptManager.WriteBeginScriptBlock(writer);
				this.RenderClientScriptFormDeclaration(writer, formUniqueID);
				ClientScriptManager.WriteEndScriptBlock(writer);
			}
			if (this.handleViewState)
			{
				this.scriptManager.RegisterHiddenField("__VIEWSTATE", this._savedViewState);
			}
			this.scriptManager.WriteHiddenFields(writer);
			if (this.requiresPostBackScript)
			{
				this.RenderPostBackScript(writer, formUniqueID);
				this.postBackScriptRendered = true;
			}
			this.scriptManager.WriteWebFormClientScript(writer);
			this.scriptManager.WriteClientScriptBlocks(writer);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0003568B File Offset: 0x0003388B
		internal IStateFormatter GetFormatter()
		{
			return new ObjectStateFormatter(this);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00035693 File Offset: 0x00033893
		internal string GetSavedViewState()
		{
			return this._savedViewState;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0003569C File Offset: 0x0003389C
		internal void OnFormPostRender(HtmlTextWriter writer, string formUniqueID)
		{
			this.scriptManager.SaveEventValidationState();
			this.scriptManager.WriteExpandoAttributes(writer);
			this.scriptManager.WriteHiddenFields(writer);
			if (!this.postBackScriptRendered && this.requiresPostBackScript)
			{
				this.RenderPostBackScript(writer, formUniqueID);
			}
			this.scriptManager.WriteWebFormClientScript(writer);
			this.scriptManager.WriteArrayDeclares(writer);
			this.scriptManager.WriteStartupScriptBlocks(writer);
			this.renderingForm = false;
			this.postBackScriptRendered = false;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00035718 File Offset: 0x00033918
		private void ProcessPostData(NameValueCollection data, bool second)
		{
			NameValueCollection nameValueCollection = ((this._requestValueCollection == null) ? new NameValueCollection(SecureHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant) : this._requestValueCollection);
			if (data != null && data.Count > 0)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
				foreach (string text in data.AllKeys)
				{
					if (!(text == "__VIEWSTATE") && !(text == "__EVENTTARGET") && !(text == "__EVENTARGUMENT") && !(text == "__EVENTVALIDATION") && !dictionary.ContainsKey(text))
					{
						dictionary.Add(text, text);
						Control control = this.FindControl(text, true);
						if (control != null)
						{
							IPostBackDataHandler postBackDataHandler = control as IPostBackDataHandler;
							IPostBackEventHandler postBackEventHandler = control as IPostBackEventHandler;
							if (postBackDataHandler == null)
							{
								if (postBackEventHandler != null)
								{
									this.formPostedRequiresRaiseEvent = postBackEventHandler;
								}
							}
							else
							{
								if (postBackDataHandler.LoadPostData(text, nameValueCollection))
								{
									if (this.requiresPostDataChanged == null)
									{
										this.requiresPostDataChanged = new List<IPostBackDataHandler>();
									}
									this.requiresPostDataChanged.Add(postBackDataHandler);
								}
								if (this._requiresPostBackCopy != null)
								{
									this._requiresPostBackCopy.Remove(text);
								}
							}
						}
						else if (!second)
						{
							if (this.secondPostData == null)
							{
								this.secondPostData = new NameValueCollection(SecureHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
							}
							this.secondPostData.Add(text, data[text]);
						}
					}
				}
			}
			List<string> list = null;
			if (this._requiresPostBackCopy != null && this._requiresPostBackCopy.Count > 0)
			{
				foreach (string text2 in this._requiresPostBackCopy.ToArray())
				{
					IPostBackDataHandler postBackDataHandler2 = this.FindControl(text2, true) as IPostBackDataHandler;
					if (postBackDataHandler2 != null)
					{
						this._requiresPostBackCopy.Remove(text2);
						if (postBackDataHandler2.LoadPostData(text2, nameValueCollection))
						{
							if (this.requiresPostDataChanged == null)
							{
								this.requiresPostDataChanged = new List<IPostBackDataHandler>();
							}
							this.requiresPostDataChanged.Add(postBackDataHandler2);
						}
					}
					else if (!second)
					{
						if (list == null)
						{
							list = new List<string>();
						}
						list.Add(text2);
					}
				}
			}
			this._requiresPostBackCopy = (second ? null : list);
			if (second)
			{
				this.secondPostData = null;
			}
		}

		/// <summary>Sets the intrinsic server objects of the <see cref="T:System.Web.UI.Page" /> object, such as the <see cref="P:System.Web.UI.Page.Context" />, <see cref="P:System.Web.UI.Page.Request" />, <see cref="P:System.Web.UI.Page.Response" />, and <see cref="P:System.Web.UI.Page.Application" /> properties.</summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, and <see cref="P:System.Web.HttpContext.Session" />) used to service HTTP requests. </param>
		// Token: 0x060014B6 RID: 5302 RVA: 0x00035950 File Offset: 0x00033B50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ProcessRequest(HttpContext context)
		{
			this.SetContext(context);
			if (this.clientTarget != null)
			{
				this.Request.ClientTarget = this.clientTarget;
			}
			base.WireupAutomaticEvents();
			this._appCulture = Thread.CurrentThread.CurrentCulture;
			this._appUICulture = Thread.CurrentThread.CurrentUICulture;
			this.FrameworkInitialize();
			this.frameworkInitialized = true;
			context.ErrorPage = this._errorPage;
			try
			{
				this.InternalProcessRequest();
			}
			catch (ThreadAbortException ex)
			{
				if (FlagEnd.Value != ex.ExceptionState)
				{
					throw;
				}
				Thread.ResetAbort();
			}
			catch (Exception ex2)
			{
				this.ProcessException(ex2);
			}
			finally
			{
				this.ProcessUnload();
			}
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00035A18 File Offset: 0x00033C18
		private void ProcessException(Exception e)
		{
			this.Trace.Warn("Unhandled Exception", e.ToString(), e);
			this._context.AddError(e);
			this.OnError(EventArgs.Empty);
			if (this._context.HasError(e))
			{
				this._context.ClearError(e);
				throw new HttpUnhandledException(null, e);
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00035A78 File Offset: 0x00033C78
		private void ProcessUnload()
		{
			try
			{
				this.RenderTrace();
				base.UnloadRecursive(true);
			}
			catch
			{
			}
			if (!Thread.CurrentThread.CurrentCulture.Equals(this._appCulture))
			{
				Thread.CurrentThread.CurrentCulture = this._appCulture;
			}
			if (!Thread.CurrentThread.CurrentUICulture.Equals(this._appUICulture))
			{
				Thread.CurrentThread.CurrentUICulture = this._appUICulture;
			}
			this._appCulture = null;
			this._appUICulture = null;
		}

		/// <summary>Begins processing an asynchronous page request.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the request.</param>
		/// <param name="callback">The callback method to notify when the process is complete.</param>
		/// <param name="extraData">State data for the asynchronous method.</param>
		// Token: 0x060014B9 RID: 5305 RVA: 0x00035B04 File Offset: 0x00033D04
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected IAsyncResult AsyncPageBeginProcessRequest(HttpContext context, AsyncCallback callback, object extraData)
		{
			this.ProcessRequest(context);
			Page.DummyAsyncResult dummyAsyncResult = new Page.DummyAsyncResult(true, true, extraData);
			if (callback != null)
			{
				callback(dummyAsyncResult);
			}
			return dummyAsyncResult;
		}

		/// <summary>Ends processing an asynchronous page request.</summary>
		/// <param name="result">An <see cref="T:System.IAsyncResult" /> referencing a pending asynchronous request.</param>
		// Token: 0x060014BA RID: 5306 RVA: 0x00003946 File Offset: 0x00001B46
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void AsyncPageEndProcessRequest(IAsyncResult result)
		{
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00035B2C File Offset: 0x00033D2C
		private void InternalProcessRequest()
		{
			if (this.PageAdapter != null)
			{
				this._requestValueCollection = this.PageAdapter.DeterminePostBackMode();
			}
			else
			{
				this._requestValueCollection = this.DeterminePostBackMode();
			}
			if (this._requestValueCollection != null)
			{
				if (!this.isCrossPagePostBack && this._requestValueCollection["__PREVIOUSPAGE"] != null && this._requestValueCollection["__PREVIOUSPAGE"] != this.Request.FilePath)
				{
					this._doLoadPreviousPage = true;
				}
				else
				{
					this.isCallback = this._requestValueCollection["__CALLBACKPARAM"] != null;
					this.isPostBack = true;
				}
				string text = this._requestValueCollection["__LASTFOCUS"];
				if (!string.IsNullOrEmpty(text))
				{
					this._focusedControlID = base.UniqueID2ClientID(text);
				}
			}
			if (!this.isCrossPagePostBack && this._context.PreviousHandler is Page)
			{
				this.previousPage = (Page)this._context.PreviousHandler;
			}
			this.Trace.Write("aspx.page", "Begin PreInit");
			this.OnPreInit(EventArgs.Empty);
			this.Trace.Write("aspx.page", "End PreInit");
			this.InitializeTheme();
			this.ApplyMasterPage();
			this.Trace.Write("aspx.page", "Begin Init");
			this.InitRecursive(null);
			this.Trace.Write("aspx.page", "End Init");
			this.Trace.Write("aspx.page", "Begin InitComplete");
			this.OnInitComplete(EventArgs.Empty);
			this.Trace.Write("aspx.page", "End InitComplete");
			this.renderingForm = false;
			this.RestorePageState();
			this.ProcessPostData();
			this.ProcessRaiseEvents();
			if (this.ProcessLoadComplete())
			{
				return;
			}
			this.RenderPage();
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00035CF8 File Offset: 0x00033EF8
		private void RestorePageState()
		{
			if (this.IsPostBack || this.IsCallback)
			{
				if (this._requestValueCollection != null)
				{
					this.scriptManager.RestoreEventValidationState(this._requestValueCollection["__EVENTVALIDATION"]);
				}
				this.Trace.Write("aspx.page", "Begin LoadViewState");
				this.LoadPageViewState();
				this.Trace.Write("aspx.page", "End LoadViewState");
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00035D68 File Offset: 0x00033F68
		private void ProcessPostData()
		{
			if (this.IsPostBack || this.IsCallback)
			{
				this.Trace.Write("aspx.page", "Begin ProcessPostData");
				this.ProcessPostData(this._requestValueCollection, false);
				this.Trace.Write("aspx.page", "End ProcessPostData");
			}
			this.ProcessLoad();
			if (this.IsPostBack || this.IsCallback)
			{
				this.Trace.Write("aspx.page", "Begin ProcessPostData Second Try");
				this.ProcessPostData(this.secondPostData, true);
				this.Trace.Write("aspx.page", "End ProcessPostData Second Try");
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00035E0C File Offset: 0x0003400C
		private void ProcessLoad()
		{
			this.Trace.Write("aspx.page", "Begin PreLoad");
			this.OnPreLoad(EventArgs.Empty);
			this.Trace.Write("aspx.page", "End PreLoad");
			this.Trace.Write("aspx.page", "Begin Load");
			base.LoadRecursive();
			this.Trace.Write("aspx.page", "End Load");
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00035E80 File Offset: 0x00034080
		private void ProcessRaiseEvents()
		{
			if (this.IsPostBack || this.IsCallback)
			{
				this.Trace.Write("aspx.page", "Begin Raise ChangedEvents");
				this.RaiseChangedEvents();
				this.Trace.Write("aspx.page", "End Raise ChangedEvents");
				this.Trace.Write("aspx.page", "Begin Raise PostBackEvent");
				this.RaisePostBackEvents();
				this.Trace.Write("aspx.page", "End Raise PostBackEvent");
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00035F00 File Offset: 0x00034100
		private bool ProcessLoadComplete()
		{
			this.Trace.Write("aspx.page", "Begin LoadComplete");
			this.OnLoadComplete(EventArgs.Empty);
			this.Trace.Write("aspx.page", "End LoadComplete");
			if (this.IsCrossPagePostBack)
			{
				return true;
			}
			if (this.IsCallback)
			{
				string text = this.ProcessCallbackData();
				HtmlTextWriter htmlTextWriter = new HtmlTextWriter(this.Response.Output);
				htmlTextWriter.Write(text);
				htmlTextWriter.Flush();
				return true;
			}
			this.Trace.Write("aspx.page", "Begin PreRender");
			base.PreRenderRecursiveInternal();
			this.Trace.Write("aspx.page", "End PreRender");
			this.ExecuteRegisteredAsyncTasks();
			this.Trace.Write("aspx.page", "Begin PreRenderComplete");
			this.OnPreRenderComplete(EventArgs.Empty);
			this.Trace.Write("aspx.page", "End PreRenderComplete");
			this.Trace.Write("aspx.page", "Begin SaveViewState");
			this.SavePageViewState();
			this.Trace.Write("aspx.page", "End SaveViewState");
			this.Trace.Write("aspx.page", "Begin SaveStateComplete");
			this.OnSaveStateComplete(EventArgs.Empty);
			this.Trace.Write("aspx.page", "End SaveStateComplete");
			return false;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0003604C File Offset: 0x0003424C
		internal void RenderPage()
		{
			this.scriptManager.ResetEventValidationState();
			this.Trace.Write("aspx.page", "Begin Render");
			HtmlTextWriter htmlTextWriter = this.CreateHtmlTextWriter(this.Response.Output);
			this.RenderControl(htmlTextWriter);
			this.Trace.Write("aspx.page", "End Render");
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x000360A7 File Offset: 0x000342A7
		internal void SetContext(HttpContext context)
		{
			this._context = context;
			this._application = context.Application;
			this._response = context.Response;
			this._request = context.Request;
			this._cache = context.Cache;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000360E0 File Offset: 0x000342E0
		private void RenderTrace()
		{
			TraceManager traceManager = HttpRuntime.TraceManager;
			if ((this.Trace.HaveTrace && !this.Trace.IsEnabled) || (!this.Trace.HaveTrace && !traceManager.Enabled))
			{
				return;
			}
			this.Trace.SaveData();
			if (!this.Trace.HaveTrace && traceManager.Enabled && !traceManager.PageOutput)
			{
				return;
			}
			if (!traceManager.LocalOnly || this.Context.Request.IsLocal)
			{
				HtmlTextWriter htmlTextWriter = new HtmlTextWriter(this.Response.Output);
				this.Trace.Render(htmlTextWriter);
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00036184 File Offset: 0x00034384
		private void RaisePostBackEvents()
		{
			if (this.requiresRaiseEvent != null)
			{
				this.RaisePostBackEvent(this.requiresRaiseEvent, null);
				return;
			}
			if (this.formPostedRequiresRaiseEvent != null)
			{
				this.RaisePostBackEvent(this.formPostedRequiresRaiseEvent, null);
				return;
			}
			NameValueCollection requestValueCollection = this._requestValueCollection;
			if (requestValueCollection == null)
			{
				return;
			}
			string text = requestValueCollection["__EVENTTARGET"];
			if (string.IsNullOrEmpty(text))
			{
				IPostBackEventHandler postBackEventHandler = this.AutoPostBackControl as IPostBackEventHandler;
				if (postBackEventHandler != null)
				{
					this.RaisePostBackEvent(postBackEventHandler, null);
					return;
				}
				if (this.formPostedRequiresRaiseEvent != null)
				{
					this.RaisePostBackEvent(this.formPostedRequiresRaiseEvent, null);
					return;
				}
				this.Validate();
				return;
			}
			else
			{
				IPostBackEventHandler postBackEventHandler = this.FindControl(text, true) as IPostBackEventHandler;
				if (postBackEventHandler == null)
				{
					postBackEventHandler = this.AutoPostBackControl as IPostBackEventHandler;
				}
				if (postBackEventHandler == null)
				{
					return;
				}
				string text2 = requestValueCollection["__EVENTARGUMENT"];
				this.RaisePostBackEvent(postBackEventHandler, text2);
				return;
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00036248 File Offset: 0x00034448
		internal void RaiseChangedEvents()
		{
			if (this.requiresPostDataChanged == null)
			{
				return;
			}
			foreach (IPostBackDataHandler postBackDataHandler in this.requiresPostDataChanged)
			{
				postBackDataHandler.RaisePostDataChangedEvent();
			}
			this.requiresPostDataChanged = null;
		}

		/// <summary>Notifies the server control that caused the postback that it should handle an incoming postback event.</summary>
		/// <param name="sourceControl">The ASP.NET server control that caused the postback. This control must implement the <see cref="T:System.Web.UI.IPostBackEventHandler" /> interface. </param>
		/// <param name="eventArgument">The postback argument. </param>
		// Token: 0x060014C6 RID: 5318 RVA: 0x000362A8 File Offset: 0x000344A8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
		{
			sourceControl.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Declares a value that is declared as an ECMAScript array declaration when the page is rendered.</summary>
		/// <param name="arrayName">The name of the array in which to declare the value. </param>
		/// <param name="arrayValue">The value to place in the array. </param>
		// Token: 0x060014C7 RID: 5319 RVA: 0x000362B1 File Offset: 0x000344B1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.RegisterArrayDeclaration(string arrayName, string arrayValue). http://go.microsoft.com/fwlink/?linkid=14202")]
		public void RegisterArrayDeclaration(string arrayName, string arrayValue)
		{
			this.scriptManager.RegisterArrayDeclaration(arrayName, arrayValue);
		}

		/// <summary>Emits client-side script blocks to the response.</summary>
		/// <param name="key">Unique key that identifies a script block. </param>
		/// <param name="script">Content of script that is sent to the client. </param>
		// Token: 0x060014C8 RID: 5320 RVA: 0x000362C0 File Offset: 0x000344C0
		[Obsolete("The recommended alternative is ClientScript.RegisterClientScriptBlock(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void RegisterClientScriptBlock(string key, string script)
		{
			this.scriptManager.RegisterClientScriptBlock(key, script);
		}

		/// <summary>Allows server controls to automatically register a hidden field on the form. The field will be sent to the <see cref="T:System.Web.UI.Page" /> object when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> server control is rendered.</summary>
		/// <param name="hiddenFieldName">The unique name of the hidden field to be rendered. </param>
		/// <param name="hiddenFieldInitialValue">The value to be emitted in the hidden form. </param>
		// Token: 0x060014C9 RID: 5321 RVA: 0x000362CF File Offset: 0x000344CF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete]
		public virtual void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
		{
			this.scriptManager.RegisterHiddenField(hiddenFieldName, hiddenFieldInitialValue);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented, Used in HtmlForm")]
		internal void RegisterClientScriptFile(string a, string b, string c)
		{
			throw new NotImplementedException();
		}

		/// <summary>Allows a page to access the client OnSubmit event. The script should be a function call to client code registered elsewhere.</summary>
		/// <param name="key">Unique key that identifies a script block. </param>
		/// <param name="script">The client-side script to be sent to the client. </param>
		// Token: 0x060014CB RID: 5323 RVA: 0x000362DE File Offset: 0x000344DE
		[Obsolete("The recommended alternative is ClientScript.RegisterOnSubmitStatement(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RegisterOnSubmitStatement(string key, string script)
		{
			this.scriptManager.RegisterOnSubmitStatement(key, script);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x000362ED File Offset: 0x000344ED
		internal string GetSubmitStatements()
		{
			return this.scriptManager.WriteSubmitStatements();
		}

		/// <summary>Registers a control as one that requires postback handling when the page is posted back to the server. </summary>
		/// <param name="control">The control to be registered. </param>
		/// <exception cref="T:System.Web.HttpException">The control to register does not implement the <see cref="T:System.Web.UI.IPostBackDataHandler" /> interface. </exception>
		// Token: 0x060014CD RID: 5325 RVA: 0x000362FC File Offset: 0x000344FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RegisterRequiresPostBack(Control control)
		{
			if (!(control is IPostBackDataHandler))
			{
				throw new HttpException("The control to register does not implement the IPostBackDataHandler interface.");
			}
			if (this._requiresPostBack == null)
			{
				this._requiresPostBack = new List<string>();
			}
			string uniqueID = control.UniqueID;
			if (this._requiresPostBack.Contains(uniqueID))
			{
				return;
			}
			this._requiresPostBack.Add(uniqueID);
		}

		/// <summary>Registers an ASP.NET server control as one requiring an event to be raised when the control is processed on the <see cref="T:System.Web.UI.Page" /> object.</summary>
		/// <param name="control">The control to register. </param>
		// Token: 0x060014CE RID: 5326 RVA: 0x00036351 File Offset: 0x00034551
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void RegisterRequiresRaiseEvent(IPostBackEventHandler control)
		{
			this.requiresRaiseEvent = control;
		}

		/// <summary>Emits a client-side script block in the page response. </summary>
		/// <param name="key">Unique key that identifies a script block. </param>
		/// <param name="script">Content of script that will be sent to the client. </param>
		// Token: 0x060014CF RID: 5327 RVA: 0x0003635A File Offset: 0x0003455A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.RegisterStartupScript(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual void RegisterStartupScript(string key, string script)
		{
			this.scriptManager.RegisterStartupScript(key, script);
		}

		/// <summary>Causes page view state to be persisted, if called.</summary>
		// Token: 0x060014D0 RID: 5328 RVA: 0x00036369 File Offset: 0x00034569
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RegisterViewStateHandler()
		{
			this.handleViewState = true;
		}

		/// <summary>Saves any view-state and control-state information for the page.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> in which to store the view-state information. </param>
		// Token: 0x060014D1 RID: 5329 RVA: 0x00036374 File Offset: 0x00034574
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void SavePageStateToPersistenceMedium(object state)
		{
			PageStatePersister pageStatePersister = this.PageStatePersister;
			if (pageStatePersister == null)
			{
				return;
			}
			Pair pair = state as Pair;
			if (pair != null)
			{
				pageStatePersister.ViewState = pair.First;
				pageStatePersister.ControlState = pair.Second;
			}
			else
			{
				pageStatePersister.ViewState = state;
			}
			pageStatePersister.Save();
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x000363C0 File Offset: 0x000345C0
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x000363F8 File Offset: 0x000345F8
		internal string RawViewState
		{
			get
			{
				NameValueCollection requestValueCollection = this._requestValueCollection;
				string text;
				if (requestValueCollection == null || (text = requestValueCollection["__VIEWSTATE"]) == null)
				{
					return null;
				}
				if (text == string.Empty)
				{
					return null;
				}
				return text;
			}
			set
			{
				this._savedViewState = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.PageStatePersister" /> object associated with the page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PageStatePersister" /> associated with the page.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00036401 File Offset: 0x00034601
		protected virtual PageStatePersister PageStatePersister
		{
			get
			{
				if (this.page_state_persister == null && this.PageAdapter != null)
				{
					this.page_state_persister = this.PageAdapter.GetStatePersister();
				}
				if (this.page_state_persister == null)
				{
					this.page_state_persister = new HiddenFieldPageStatePersister(this);
				}
				return this.page_state_persister;
			}
		}

		/// <summary>Loads any saved view-state information to the <see cref="T:System.Web.UI.Page" /> object. </summary>
		/// <returns>The saved view state.</returns>
		// Token: 0x060014D5 RID: 5333 RVA: 0x00036440 File Offset: 0x00034640
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual object LoadPageStateFromPersistenceMedium()
		{
			PageStatePersister pageStatePersister = this.PageStatePersister;
			if (pageStatePersister == null)
			{
				return null;
			}
			pageStatePersister.Load();
			return new Pair(pageStatePersister.ViewState, pageStatePersister.ControlState);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00036470 File Offset: 0x00034670
		internal void LoadPageViewState()
		{
			Pair pair = this.LoadPageStateFromPersistenceMedium() as Pair;
			if (pair != null && (this.allow_load || this.isCrossPagePostBack))
			{
				this.LoadPageControlState(pair.Second);
				Pair pair2 = pair.First as Pair;
				if (pair2 != null)
				{
					base.LoadViewStateRecursive(pair2.First);
					this._requiresPostBackCopy = pair2.Second as List<string>;
				}
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x000364D4 File Offset: 0x000346D4
		internal void SavePageViewState()
		{
			if (!this.handleViewState)
			{
				return;
			}
			object obj = this.SavePageControlState();
			Pair pair = null;
			object obj2 = null;
			if (this.EnableViewState && this.ViewStateMode == ViewStateMode.Enabled)
			{
				obj2 = base.SaveViewStateRecursive();
			}
			object obj3 = ((this._requiresPostBack != null && this._requiresPostBack.Count > 0) ? this._requiresPostBack : null);
			if (obj2 != null || obj3 != null)
			{
				pair = new Pair(obj2, obj3);
			}
			Pair pair2 = new Pair();
			pair2.First = pair;
			pair2.Second = obj;
			if (pair2.First == null && pair2.Second == null)
			{
				this.SavePageStateToPersistenceMedium(null);
				return;
			}
			this.SavePageStateToPersistenceMedium(pair2);
		}

		/// <summary>Instructs any validation controls included on the page to validate their assigned information.</summary>
		// Token: 0x060014D8 RID: 5336 RVA: 0x00036574 File Offset: 0x00034774
		public virtual void Validate()
		{
			this.is_validated = true;
			this.ValidateCollection(this._validators);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0003658A File Offset: 0x0003478A
		internal bool AreValidatorsUplevel()
		{
			return this.AreValidatorsUplevel(string.Empty);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00036598 File Offset: 0x00034798
		internal bool AreValidatorsUplevel(string valGroup)
		{
			bool flag = false;
			foreach (object obj in this.Validators)
			{
				BaseValidator baseValidator = ((IValidator)obj) as BaseValidator;
				if (baseValidator != null && !(valGroup != baseValidator.ValidationGroup) && baseValidator.GetRenderUplevel())
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00036610 File Offset: 0x00034810
		private bool ValidateCollection(ValidatorCollection validators)
		{
			if (validators == null || validators.Count == 0)
			{
				return true;
			}
			bool flag = true;
			foreach (object obj in validators)
			{
				IValidator validator = (IValidator)obj;
				validator.Validate();
				if (!validator.IsValid)
				{
					flag = false;
				}
			}
			return flag;
		}

		/// <summary>Confirms that an <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control is rendered for the specified ASP.NET server control at run time.</summary>
		/// <param name="control">The ASP.NET server control that is required in the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control. </param>
		/// <exception cref="T:System.Web.HttpException">The specified server control is not contained between the opening and closing tags of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> server control at run time. </exception>
		/// <exception cref="T:System.ArgumentNullException">The control to verify is null.</exception>
		// Token: 0x060014DC RID: 5340 RVA: 0x0003667C File Offset: 0x0003487C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void VerifyRenderingInServerForm(Control control)
		{
			if (this.Context == null)
			{
				return;
			}
			if (this.IsCallback)
			{
				return;
			}
			if (!this.renderingForm)
			{
				throw new HttpException(string.Concat(new string[]
				{
					"Control '",
					control.ClientID,
					"' of type '",
					control.GetType().Name,
					"' must be placed inside a form tag with runat=server."
				}));
			}
		}

		/// <summary>Initializes the control tree during page generation based on the declarative nature of the page. </summary>
		// Token: 0x060014DD RID: 5341 RVA: 0x000366E3 File Offset: 0x000348E3
		protected override void FrameworkInitialize()
		{
			base.FrameworkInitialize();
			this.InitializeStyleSheet();
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ClientScriptManager" /> object used to manage, register, and add script to the page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ClientScriptManager" /> object.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x000366F1 File Offset: 0x000348F1
		public ClientScriptManager ClientScript
		{
			get
			{
				return this.scriptManager;
			}
		}

		/// <summary>Occurs when page initialization is complete.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060014DF RID: 5343 RVA: 0x000366F9 File Offset: 0x000348F9
		// (remove) Token: 0x060014E0 RID: 5344 RVA: 0x0003671A File Offset: 0x0003491A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler InitComplete
		{
			add
			{
				this.event_mask |= 1;
				base.Events.AddHandler(Page.InitCompleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.InitCompleteEvent, value);
			}
		}

		/// <summary>Occurs at the end of the load stage of the page's life cycle.</summary>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060014E1 RID: 5345 RVA: 0x0003672D File Offset: 0x0003492D
		// (remove) Token: 0x060014E2 RID: 5346 RVA: 0x0003674E File Offset: 0x0003494E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler LoadComplete
		{
			add
			{
				this.event_mask |= 2;
				base.Events.AddHandler(Page.LoadCompleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.LoadCompleteEvent, value);
			}
		}

		/// <summary>Occurs at the beginning of page initialization.</summary>
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060014E3 RID: 5347 RVA: 0x00036761 File Offset: 0x00034961
		// (remove) Token: 0x060014E4 RID: 5348 RVA: 0x00036782 File Offset: 0x00034982
		public event EventHandler PreInit
		{
			add
			{
				this.event_mask |= 4;
				base.Events.AddHandler(Page.PreInitEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.PreInitEvent, value);
			}
		}

		/// <summary>Occurs before the page <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060014E5 RID: 5349 RVA: 0x00036795 File Offset: 0x00034995
		// (remove) Token: 0x060014E6 RID: 5350 RVA: 0x000367B6 File Offset: 0x000349B6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler PreLoad
		{
			add
			{
				this.event_mask |= 8;
				base.Events.AddHandler(Page.PreLoadEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.PreLoadEvent, value);
			}
		}

		/// <summary>Occurs before the page content is rendered.</summary>
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060014E7 RID: 5351 RVA: 0x000367C9 File Offset: 0x000349C9
		// (remove) Token: 0x060014E8 RID: 5352 RVA: 0x000367EB File Offset: 0x000349EB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler PreRenderComplete
		{
			add
			{
				this.event_mask |= 16;
				base.Events.AddHandler(Page.PreRenderCompleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.PreRenderCompleteEvent, value);
			}
		}

		/// <summary>Occurs after the page has completed saving all view state and control state information for the page and controls on the page.</summary>
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060014E9 RID: 5353 RVA: 0x000367FE File Offset: 0x000349FE
		// (remove) Token: 0x060014EA RID: 5354 RVA: 0x00036820 File Offset: 0x00034A20
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler SaveStateComplete
		{
			add
			{
				this.event_mask |= 32;
				base.Events.AddHandler(Page.SaveStateCompleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.SaveStateCompleteEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Page.InitComplete" /> event after page initialization.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060014EB RID: 5355 RVA: 0x00036834 File Offset: 0x00034A34
		protected virtual void OnInitComplete(EventArgs e)
		{
			if ((this.event_mask & 1) != 0)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Page.InitCompleteEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Page.LoadComplete" /> event at the end of the page load stage.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060014EC RID: 5356 RVA: 0x0003686C File Offset: 0x00034A6C
		protected virtual void OnLoadComplete(EventArgs e)
		{
			if ((this.event_mask & 2) != 0)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Page.LoadCompleteEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Page.PreInit" /> event at the beginning of page initialization.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060014ED RID: 5357 RVA: 0x000368A4 File Offset: 0x00034AA4
		protected virtual void OnPreInit(EventArgs e)
		{
			if ((this.event_mask & 4) != 0)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Page.PreInitEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Page.PreLoad" /> event after postback data is loaded into the page server controls but before the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060014EE RID: 5358 RVA: 0x000368DC File Offset: 0x00034ADC
		protected virtual void OnPreLoad(EventArgs e)
		{
			if ((this.event_mask & 8) != 0)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Page.PreLoadEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Page.PreRenderComplete" /> event after the <see cref="M:System.Web.UI.Page.OnPreRenderComplete(System.EventArgs)" /> event and before the page is rendered.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060014EF RID: 5359 RVA: 0x00036914 File Offset: 0x00034B14
		protected virtual void OnPreRenderComplete(EventArgs e)
		{
			if ((this.event_mask & 16) != 0)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Page.PreRenderCompleteEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
			if (this.Form == null)
			{
				return;
			}
			if (!this.Form.DetermineRenderUplevel())
			{
				return;
			}
			string defaultButton = this.Form.DefaultButton;
			if (string.IsNullOrEmpty(this._focusedControlID))
			{
				this._focusedControlID = this.Form.DefaultFocus;
				if (string.IsNullOrEmpty(this._focusedControlID))
				{
					this._focusedControlID = defaultButton;
				}
			}
			if (!string.IsNullOrEmpty(this._focusedControlID))
			{
				this.ClientScript.RegisterWebFormClientScript();
				this.ClientScript.RegisterStartupScript(typeof(Page), "HtmlForm-DefaultButton-StartupScript", string.Concat(new string[] { "\n", this.WebFormScriptReference, ".WebForm_AutoFocus('", this._focusedControlID, "');\n" }), true);
			}
			if (this.Form.SubmitDisabledControls && this._hasEnabledControlArray)
			{
				this.ClientScript.RegisterWebFormClientScript();
				this.ClientScript.RegisterOnSubmitStatement(typeof(Page), "HtmlForm-SubmitDisabledControls-SubmitStatement", this.WebFormScriptReference + ".WebForm_ReEnableControls();");
			}
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00036A54 File Offset: 0x00034C54
		internal void RegisterEnabledControl(Control control)
		{
			if (this.Form == null || !this.Page.Form.SubmitDisabledControls || !this.Page.Form.DetermineRenderUplevel())
			{
				return;
			}
			this._hasEnabledControlArray = true;
			this.Page.ClientScript.RegisterArrayDeclaration("__enabledControlArray", "'" + control.ClientID + "'");
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Page.SaveStateComplete" /> event after the page state has been saved to the persistence medium.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> object containing the event data.</param>
		// Token: 0x060014F1 RID: 5361 RVA: 0x00036AC0 File Offset: 0x00034CC0
		protected virtual void OnSaveStateComplete(EventArgs e)
		{
			if ((this.event_mask & 32) != 0)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Page.SaveStateCompleteEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Gets the HTML form for the page.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> object associated with the page.</returns>
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00036AF9 File Offset: 0x00034CF9
		public HtmlForm Form
		{
			get
			{
				return this._form;
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00036B01 File Offset: 0x00034D01
		internal void RegisterForm(HtmlForm form)
		{
			this._form = form;
		}

		/// <summary>Gets the query string portion of the requested URL.</summary>
		/// <returns>The query string portion of the requested URL.</returns>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00036B0A File Offset: 0x00034D0A
		public string ClientQueryString
		{
			get
			{
				return this.Request.UrlComponents.Query;
			}
		}

		/// <summary>Gets the page that transferred control to the current page.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Page" /> representing the page that transferred control to the current page.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current user is not allowed to access the previous page.-or-ASP.NET routing is in use and the previous page's URL is a routed URL. When ASP.NET checks access permissions, it assumes that the URL is an actual path to a file. Because this is not the case with a routed URL, the check fails.</exception>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00036B1C File Offset: 0x00034D1C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Page PreviousPage
		{
			get
			{
				if (this._doLoadPreviousPage)
				{
					this._doLoadPreviousPage = false;
					this.LoadPreviousPageReference();
				}
				return this.previousPage;
			}
		}

		/// <summary>Gets a value that indicates whether the page request is the result of a callback.</summary>
		/// <returns>true if the page request is the result of a callback; otherwise, false.</returns>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00036B39 File Offset: 0x00034D39
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsCallback
		{
			get
			{
				return this.isCallback;
			}
		}

		/// <summary>Gets a value indicating whether the page is involved in a cross-page postback.</summary>
		/// <returns>true if the page is participating in a cross-page request; otherwise, false.</returns>
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00036B41 File Offset: 0x00034D41
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsCrossPagePostBack
		{
			get
			{
				return this.isCrossPagePostBack;
			}
		}

		/// <summary>Gets the character used to separate control identifiers when building a unique ID for a control on a page.</summary>
		/// <returns>The character used to separate control identifiers. The default is set by the <see cref="T:System.Web.UI.Adapters.PageAdapter" /> instance that renders the page. The <see cref="P:System.Web.UI.Page.IdSeparator" /> is a server-side field and should not be modified.</returns>
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00036B49 File Offset: 0x00034D49
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new virtual char IdSeparator
		{
			get
			{
				return base.IdSeparator;
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00036B54 File Offset: 0x00034D54
		private string ProcessCallbackData()
		{
			ICallbackEventHandler callbackTarget = this.GetCallbackTarget();
			string empty = string.Empty;
			this.ProcessRaiseCallbackEvent(callbackTarget, ref empty);
			return this.ProcessGetCallbackResult(callbackTarget, empty);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00036B80 File Offset: 0x00034D80
		private ICallbackEventHandler GetCallbackTarget()
		{
			string text = this._requestValueCollection["__CALLBACKID"];
			if (text == null || text.Length == 0)
			{
				throw new HttpException("Callback target not provided.");
			}
			ICallbackEventHandler callbackEventHandler = this.FindControl(text, true) as ICallbackEventHandler;
			if (callbackEventHandler == null)
			{
				throw new HttpException(string.Format("Invalid callback target '{0}'.", text));
			}
			return callbackEventHandler;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00036BD8 File Offset: 0x00034DD8
		private void ProcessRaiseCallbackEvent(ICallbackEventHandler target, ref string callbackEventError)
		{
			string text = this._requestValueCollection["__CALLBACKPARAM"];
			try
			{
				target.RaiseCallbackEvent(text);
			}
			catch (Exception ex)
			{
				callbackEventError = "e" + (RuntimeHelpers.DebuggingEnabled ? ex.ToString() : ex.Message);
			}
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00036C34 File Offset: 0x00034E34
		private string ProcessGetCallbackResult(ICallbackEventHandler target, string callbackEventError)
		{
			string callbackResult;
			try
			{
				callbackResult = target.GetCallbackResult();
			}
			catch (Exception ex)
			{
				return "e" + (RuntimeHelpers.DebuggingEnabled ? ex.ToString() : ex.Message);
			}
			string eventValidationStateFormatted = this.ClientScript.GetEventValidationStateFormatted();
			return string.Concat(new string[]
			{
				callbackEventError,
				(eventValidationStateFormatted == null) ? "0" : eventValidationStateFormatted.Length.ToString(),
				"|",
				eventValidationStateFormatted,
				callbackResult
			});
		}

		/// <summary>Gets the document header for the page if the head element is defined with a runat=server in the page declaration.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> containing the page header.</returns>
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00036CC8 File Offset: 0x00034EC8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HtmlHead Header
		{
			get
			{
				return this.htmlHeader;
			}
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00036CD0 File Offset: 0x00034ED0
		internal void SetHeader(HtmlHead header)
		{
			this.htmlHeader = header;
			if (header == null)
			{
				return;
			}
			if (this._title != null)
			{
				this.htmlHeader.Title = this._title;
				this._title = null;
			}
			if (this._metaDescription != null)
			{
				this.htmlHeader.Description = this._metaDescription;
				this._metaDescription = null;
			}
			if (this._metaKeywords != null)
			{
				this.htmlHeader.Keywords = this._metaKeywords;
				this._metaKeywords = null;
			}
		}

		/// <summary>Sets a value indicating whether the page is processed synchronously or asynchronously.</summary>
		/// <returns>true if the page is processed asynchronously; otherwise, false.</returns>
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00036D48 File Offset: 0x00034F48
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x00036D50 File Offset: 0x00034F50
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected bool AsyncMode
		{
			get
			{
				return this.asyncMode;
			}
			set
			{
				this.asyncMode = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time-out interval used when processing asynchronous tasks.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that contains the allowed time interval for completion of the asynchronous task. The default time interval is 45 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The property was set to a negative value.</exception>
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00036D59 File Offset: 0x00034F59
		// (set) Token: 0x06001502 RID: 5378 RVA: 0x00036D61 File Offset: 0x00034F61
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public TimeSpan AsyncTimeout
		{
			get
			{
				return this.asyncTimeout;
			}
			set
			{
				this.asyncTimeout = value;
			}
		}

		/// <summary>Gets a value indicating whether the page is processed asynchronously.</summary>
		/// <returns>true if the page is in asynchronous mode; otherwise, false;</returns>
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00036D6A File Offset: 0x00034F6A
		public bool IsAsync
		{
			get
			{
				return this.AsyncMode;
			}
		}

		/// <summary>Gets a unique suffix to append to the file path for caching browsers.</summary>
		/// <returns>A unique suffix appended to the file path. The default is "__ufps=" plus a unique 6-digit number.</returns>
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00036D74 File Offset: 0x00034F74
		protected internal virtual string UniqueFilePathSuffix
		{
			get
			{
				if (string.IsNullOrEmpty(this.uniqueFilePathSuffix))
				{
					this.uniqueFilePathSuffix = "__ufps=" + base.AppRelativeVirtualPath.GetHashCode().ToString("x");
				}
				return this.uniqueFilePathSuffix;
			}
		}

		/// <summary>Gets or sets the maximum length for the page's state field.</summary>
		/// <returns>The maximum length, in bytes, for the page's state field. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.Page.MaxPageStateFieldLength" /> property is not equal to -1 or a positive number.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.MaxPageStateFieldLength" /> property was set after the page was initialized.</exception>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00036DBC File Offset: 0x00034FBC
		// (set) Token: 0x06001506 RID: 5382 RVA: 0x00036DC4 File Offset: 0x00034FC4
		[global::System.MonoTODO("Actually use the value in code.")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MaxPageStateFieldLength
		{
			get
			{
				return this.maxPageStateFieldLength;
			}
			set
			{
				this.maxPageStateFieldLength = value;
			}
		}

		/// <summary>Registers beginning and ending event handler delegates that do not require state information for an asynchronous page.</summary>
		/// <param name="beginHandler">The delegate for the <see cref="T:System.Web.BeginEventHandler" /> method.</param>
		/// <param name="endHandler">The delegate for the <see cref="T:System.Web.EndEventHandler" /> method.</param>
		/// <exception cref="T:System.InvalidOperationException">The &lt;async&gt; page directive is not set to true.- or -The <see cref="M:System.Web.UI.Page.AddOnPreRenderCompleteAsync(System.Web.BeginEventHandler,System.Web.EndEventHandler)" /> method is called after the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Web.UI.PageAsyncTask.BeginHandler" /> or <see cref="P:System.Web.UI.PageAsyncTask.EndHandler" /> is null. </exception>
		// Token: 0x06001507 RID: 5383 RVA: 0x00036DCD File Offset: 0x00034FCD
		public void AddOnPreRenderCompleteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler)
		{
			this.AddOnPreRenderCompleteAsync(beginHandler, endHandler, null);
		}

		/// <summary>Registers beginning and ending  event handler delegates for an asynchronous page.</summary>
		/// <param name="beginHandler">The delegate for the <see cref="T:System.Web.BeginEventHandler" /> method.</param>
		/// <param name="endHandler">The delegate for the <see cref="T:System.Web.EndEventHandler" /> method.</param>
		/// <param name="state">An object containing state information for the event handlers.</param>
		/// <exception cref="T:System.InvalidOperationException">The &lt;async&gt; page directive is not set to true.- or -The <see cref="M:System.Web.UI.Page.AddOnPreRenderCompleteAsync(System.Web.BeginEventHandler,System.Web.EndEventHandler)" /> method is called after the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Web.UI.PageAsyncTask.BeginHandler" /> or <see cref="P:System.Web.UI.PageAsyncTask.EndHandler" /> is null. </exception>
		// Token: 0x06001508 RID: 5384 RVA: 0x00036DD8 File Offset: 0x00034FD8
		public void AddOnPreRenderCompleteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			if (!this.IsAsync)
			{
				throw new InvalidOperationException("AddOnPreRenderCompleteAsync called and Page.IsAsync == false");
			}
			if (base.IsPrerendered)
			{
				throw new InvalidOperationException("AddOnPreRenderCompleteAsync can only be called before and during PreRender.");
			}
			if (beginHandler == null)
			{
				throw new ArgumentNullException("beginHandler");
			}
			if (endHandler == null)
			{
				throw new ArgumentNullException("endHandler");
			}
			this.RegisterAsyncTask(new PageAsyncTask(beginHandler, endHandler, null, state, false));
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00036E37 File Offset: 0x00035037
		private List<PageAsyncTask> ParallelTasks
		{
			get
			{
				if (this.parallelTasks == null)
				{
					this.parallelTasks = new List<PageAsyncTask>();
				}
				return this.parallelTasks;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00036E52 File Offset: 0x00035052
		private List<PageAsyncTask> SerialTasks
		{
			get
			{
				if (this.serialTasks == null)
				{
					this.serialTasks = new List<PageAsyncTask>();
				}
				return this.serialTasks;
			}
		}

		/// <summary>Registers a new asynchronous task with the page.</summary>
		/// <param name="task">A <see cref="T:System.Web.UI.PageAsyncTask" /> that defines the asynchronous task.</param>
		/// <exception cref="T:System.ArgumentNullException">The asynchronous task is null. </exception>
		// Token: 0x0600150B RID: 5387 RVA: 0x00036E6D File Offset: 0x0003506D
		public void RegisterAsyncTask(PageAsyncTask task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			if (task.ExecuteInParallel)
			{
				this.ParallelTasks.Add(task);
				return;
			}
			this.SerialTasks.Add(task);
		}

		/// <summary>Starts the execution of an asynchronous task.</summary>
		/// <exception cref="T:System.Web.HttpException">There is an exception in the asynchronous task.</exception>
		// Token: 0x0600150C RID: 5388 RVA: 0x00036EA0 File Offset: 0x000350A0
		public void ExecuteRegisteredAsyncTasks()
		{
			if ((this.parallelTasks == null || this.parallelTasks.Count == 0) && (this.serialTasks == null || this.serialTasks.Count == 0))
			{
				return;
			}
			if (this.parallelTasks != null)
			{
				DateTime now = DateTime.Now;
				List<PageAsyncTask> list = this.parallelTasks;
				this.parallelTasks = null;
				List<IAsyncResult> list2 = new List<IAsyncResult>();
				foreach (PageAsyncTask pageAsyncTask in list)
				{
					IAsyncResult asyncResult = pageAsyncTask.BeginHandler(this, EventArgs.Empty, new AsyncCallback(this.EndAsyncTaskCallback), pageAsyncTask);
					if (asyncResult.CompletedSynchronously)
					{
						pageAsyncTask.EndHandler(asyncResult);
					}
					else
					{
						list2.Add(asyncResult);
					}
				}
				if (list2.Count > 0)
				{
					WaitHandle[] array = new WaitHandle[list2.Count];
					for (int i = 0; i < list2.Count; i++)
					{
						array[i] = list2[i].AsyncWaitHandle;
					}
					if (!WaitHandle.WaitAll(array, this.AsyncTimeout, false))
					{
						for (int i = 0; i < list2.Count; i++)
						{
							if (!list2[i].IsCompleted)
							{
								list[i].TimeoutHandler(list2[i]);
							}
						}
					}
				}
				TimeSpan timeSpan = DateTime.Now - now;
				if (timeSpan <= this.AsyncTimeout)
				{
					this.AsyncTimeout -= timeSpan;
				}
				else
				{
					this.AsyncTimeout = TimeSpan.FromTicks(0L);
				}
			}
			if (this.serialTasks != null)
			{
				List<PageAsyncTask> list3 = this.serialTasks;
				this.serialTasks = null;
				foreach (PageAsyncTask pageAsyncTask2 in list3)
				{
					DateTime now2 = DateTime.Now;
					IAsyncResult asyncResult2 = pageAsyncTask2.BeginHandler(this, EventArgs.Empty, new AsyncCallback(this.EndAsyncTaskCallback), pageAsyncTask2);
					if (asyncResult2.CompletedSynchronously)
					{
						pageAsyncTask2.EndHandler(asyncResult2);
					}
					else if (!asyncResult2.AsyncWaitHandle.WaitOne(this.AsyncTimeout, false) && !asyncResult2.IsCompleted)
					{
						pageAsyncTask2.TimeoutHandler(asyncResult2);
					}
					TimeSpan timeSpan2 = DateTime.Now - now2;
					if (timeSpan2 <= this.AsyncTimeout)
					{
						this.AsyncTimeout -= timeSpan2;
					}
					else
					{
						this.AsyncTimeout = TimeSpan.FromTicks(0L);
					}
				}
			}
			this.AsyncTimeout = TimeSpan.FromSeconds(45.0);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00037160 File Offset: 0x00035360
		private void EndAsyncTaskCallback(IAsyncResult result)
		{
			((PageAsyncTask)result.AsyncState).EndHandler(result);
		}

		/// <summary>Creates a specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object to render the page's content.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders the content of the page.</returns>
		/// <param name="tw">The <see cref="T:System.IO.TextWriter" /> used to create the <see cref="T:System.Web.UI.HtmlTextWriter" />. </param>
		/// <param name="writerType">The type of text writer to create.</param>
		/// <exception cref="T:System.Web.HttpException">The <paramref name="writerType" /> parameter is set to an invalid type.</exception>
		// Token: 0x0600150E RID: 5390 RVA: 0x00037178 File Offset: 0x00035378
		public static HtmlTextWriter CreateHtmlTextWriterFromType(TextWriter tw, Type writerType)
		{
			if (!typeof(HtmlTextWriter).IsAssignableFrom(writerType))
			{
				throw new HttpException(string.Format("Type '{0}' cannot be assigned to HtmlTextWriter", writerType.FullName));
			}
			if (writerType.GetConstructor(new Type[] { typeof(TextWriter) }) == null)
			{
				throw new HttpException(string.Format("Type '{0}' does not have a consturctor that takes a TextWriter as parameter", writerType.FullName));
			}
			return (HtmlTextWriter)Activator.CreateInstance(writerType, new object[] { tw });
		}

		/// <summary>Gets or sets the encryption mode of the view state.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.ViewStateEncryptionMode" /> values. The default value is <see cref="F:System.Web.UI.ViewStateEncryptionMode.Auto" />. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value set is not a member of the <see cref="T:System.Web.UI.ViewStateEncryptionMode" /> enumeration.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.ViewStateEncryptionMode" /> property can be set only in or before the page PreRenderphase in the page life cycle.</exception>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x000371F9 File Offset: 0x000353F9
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x00037201 File Offset: 0x00035401
		[Browsable(false)]
		[DefaultValue("0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ViewStateEncryptionMode ViewStateEncryptionMode
		{
			get
			{
				return this.viewStateEncryptionMode;
			}
			set
			{
				this.viewStateEncryptionMode = value;
			}
		}

		/// <summary>Registers a control with the page as one requiring view-state encryption. </summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.UI.Page.RegisterRequiresViewStateEncryption" /> method must be called before or during the page PreRenderphase in the page life cycle. </exception>
		// Token: 0x06001511 RID: 5393 RVA: 0x0003720A File Offset: 0x0003540A
		public void RegisterRequiresViewStateEncryption()
		{
			this.controlRegisteredForViewStateEncryption = true;
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x00037213 File Offset: 0x00035413
		internal bool NeedViewStateEncryption
		{
			get
			{
				return this.ViewStateEncryptionMode == ViewStateEncryptionMode.Always || (this.ViewStateEncryptionMode == ViewStateEncryptionMode.Auto && this.controlRegisteredForViewStateEncryption);
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00037230 File Offset: 0x00035430
		private void ApplyMasterPage()
		{
			if (this.masterPageFile != null && this.masterPageFile.Length > 0)
			{
				MasterPage master = this.Master;
				if (master != null)
				{
					Dictionary<string, bool> dictionary = new Dictionary<string, bool>(StringComparer.Ordinal);
					MasterPage.ApplyMasterPageRecursive(this.Request.CurrentExecutionFilePath, HostingEnvironment.VirtualPathProvider, master, dictionary);
					master.Page = this;
					this.Controls.Clear();
					this.Controls.Add(master);
				}
			}
		}

		/// <summary>Gets or sets the virtual path of the master page.</summary>
		/// <returns>The virtual path of the master page.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.MasterPageFile" /> property is set after the <see cref="E:System.Web.UI.Page.PreInit" /> event is complete.</exception>
		/// <exception cref="T:System.Web.HttpException">The file specified in the <see cref="P:System.Web.UI.Page.MasterPageFile" /> property does not exist.- or -The page does not have a <see cref="T:System.Web.UI.WebControls.Content" /> control as the top level control.</exception>
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x0003729D File Offset: 0x0003549D
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x000372A5 File Offset: 0x000354A5
		[DefaultValue("")]
		public virtual string MasterPageFile
		{
			get
			{
				return this.masterPageFile;
			}
			set
			{
				this.masterPageFile = value;
				this.masterPage = null;
			}
		}

		/// <summary>Gets the master page that determines the overall look of the page.</summary>
		/// <returns>The <see cref="T:System.Web.UI.MasterPage" /> associated with this page if it exists; otherwise, null. </returns>
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x000372B8 File Offset: 0x000354B8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MasterPage Master
		{
			get
			{
				if (this.Context == null || string.IsNullOrEmpty(this.masterPageFile))
				{
					return null;
				}
				if (this.masterPage == null)
				{
					this.masterPage = MasterPage.CreateMasterPage(this, this.Context, this.masterPageFile, this.contentTemplates);
				}
				return this.masterPage;
			}
		}

		/// <summary>Sets the browser focus to the control with the specified identifier. </summary>
		/// <param name="clientID">The ID of the control to set focus to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="clientID" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Web.UI.Page.SetFocus(System.String)" /> is called when the control is not part of a Web Forms page.- or -<see cref="M:System.Web.UI.Page.SetFocus(System.String)" /> is called after the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
		// Token: 0x06001517 RID: 5399 RVA: 0x00037308 File Offset: 0x00035508
		public void SetFocus(string clientID)
		{
			if (string.IsNullOrEmpty(clientID))
			{
				throw new ArgumentNullException("control");
			}
			if (base.IsPrerendered)
			{
				throw new InvalidOperationException("SetFocus can only be called before and during PreRender.");
			}
			if (this.Form == null)
			{
				throw new InvalidOperationException("A form tag with runat=server must exist on the Page to use SetFocus() or the Focus property.");
			}
			this._focusedControlID = clientID;
		}

		/// <summary>Sets the browser focus to the specified control. </summary>
		/// <param name="control">The control to receive focus.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Web.UI.Page.SetFocus(System.Web.UI.Control)" /> is called when the control is not part of a Web Forms page. - or -<see cref="M:System.Web.UI.Page.SetFocus(System.Web.UI.Control)" /> is called after the <see cref="E:System.Web.UI.Control.PreRender" /> event. </exception>
		// Token: 0x06001518 RID: 5400 RVA: 0x00037355 File Offset: 0x00035555
		public void SetFocus(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			this.SetFocus(control.ClientID);
		}

		/// <summary>Registers a control as one whose control state must be persisted.</summary>
		/// <param name="control">The control to register.</param>
		/// <exception cref="T:System.ArgumentException">The control to register is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.UI.Page.RegisterRequiresControlState(System.Web.UI.Control)" /> method can be called only before or during the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
		// Token: 0x06001519 RID: 5401 RVA: 0x00037374 File Offset: 0x00035574
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RegisterRequiresControlState(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.RequiresControlState(control))
			{
				return;
			}
			if (this.requireStateControls == null)
			{
				this.requireStateControls = new List<Control>();
			}
			this.requireStateControls.Add(control);
			int num = this.requireStateControls.Count - 1;
			if (this._savedControlState == null || num >= this._savedControlState.Length)
			{
				return;
			}
			for (Control control2 = control.Parent; control2 != null; control2 = control2.Parent)
			{
				if (control2.IsChildControlStateCleared)
				{
					return;
				}
			}
			object obj = this._savedControlState[num];
			if (obj != null)
			{
				control.LoadControlState(obj);
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.Control" /> object is registered to participate in control state management.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.UI.Control" /> requires control state; otherwise, false</returns>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to check for a control state requirement.</param>
		// Token: 0x0600151A RID: 5402 RVA: 0x00037409 File Offset: 0x00035609
		public bool RequiresControlState(Control control)
		{
			return this.requireStateControls != null && this.requireStateControls.Contains(control);
		}

		/// <summary>Stops persistence of control state for the specified control.</summary>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> for which to stop persistence of control state.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Web.UI.Control" /> is null.</exception>
		// Token: 0x0600151B RID: 5403 RVA: 0x00037421 File Offset: 0x00035621
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void UnregisterRequiresControlState(Control control)
		{
			if (this.requireStateControls != null)
			{
				this.requireStateControls.Remove(control);
			}
		}

		/// <summary>Returns a collection of control validators for a specified validation group.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ValidatorCollection" /> that contains the control validators for the specified validation group.</returns>
		/// <param name="validationGroup">The validation group to return, or null to return the default validation group.</param>
		// Token: 0x0600151C RID: 5404 RVA: 0x00037438 File Offset: 0x00035638
		public ValidatorCollection GetValidators(string validationGroup)
		{
			if (validationGroup == string.Empty)
			{
				validationGroup = null;
			}
			ValidatorCollection validatorCollection = new ValidatorCollection();
			if (this._validators == null)
			{
				return validatorCollection;
			}
			foreach (object obj in this._validators)
			{
				IValidator validator = (IValidator)obj;
				if (this.BelongsToGroup(validator, validationGroup))
				{
					validatorCollection.Add(validator);
				}
			}
			return validatorCollection;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000374BC File Offset: 0x000356BC
		private bool BelongsToGroup(IValidator v, string validationGroup)
		{
			BaseValidator baseValidator = v as BaseValidator;
			if (validationGroup == null)
			{
				return baseValidator == null || string.IsNullOrEmpty(baseValidator.ValidationGroup);
			}
			return baseValidator != null && baseValidator.ValidationGroup == validationGroup;
		}

		/// <summary>Instructs the validation controls in the specified validation group to validate their assigned information.</summary>
		/// <param name="validationGroup">The validation group name of the controls to validate.</param>
		// Token: 0x0600151E RID: 5406 RVA: 0x000374F5 File Offset: 0x000356F5
		public virtual void Validate(string validationGroup)
		{
			this.is_validated = true;
			this.ValidateCollection(this.GetValidators(validationGroup));
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0003750C File Offset: 0x0003570C
		private object SavePageControlState()
		{
			int num = ((this.requireStateControls == null) ? 0 : this.requireStateControls.Count);
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			object[] array2 = new object[num];
			bool flag = true;
			TraceContext traceContext = ((this.Context != null && this.Context.Trace.IsEnabled) ? this.Context.Trace : null);
			for (int i = 0; i < num; i++)
			{
				Control control = this.requireStateControls[i];
				object obj = (array[i] = control.SaveControlState());
				if (obj != null)
				{
					flag = false;
				}
				if (traceContext != null)
				{
					traceContext.SaveControlState(control, obj);
				}
				ControlAdapter adapter = control.Adapter;
				if (adapter != null)
				{
					array2[i] = adapter.SaveAdapterControlState();
					if (array2[i] != null)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				return null;
			}
			return new Pair(array, array2);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x000375E4 File Offset: 0x000357E4
		private void LoadPageControlState(object data)
		{
			this._savedControlState = null;
			if (data == null)
			{
				return;
			}
			Pair pair = (Pair)data;
			this._savedControlState = (object[])pair.First;
			object[] array = (object[])pair.Second;
			if (this.requireStateControls == null)
			{
				return;
			}
			int num = Math.Min(this.requireStateControls.Count, (this._savedControlState != null) ? this._savedControlState.Length : this.requireStateControls.Count);
			for (int i = 0; i < num; i++)
			{
				Control control = this.requireStateControls[i];
				control.LoadControlState((this._savedControlState != null) ? this._savedControlState[i] : null);
				if (control.Adapter != null)
				{
					control.Adapter.LoadAdapterControlState((array != null) ? array[i] : null);
				}
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000376AC File Offset: 0x000358AC
		private void LoadPreviousPageReference()
		{
			if (this._requestValueCollection != null)
			{
				string text = this._requestValueCollection["__PREVIOUSPAGE"];
				if (text != null)
				{
					IHttpHandler httpHandler = BuildManager.CreateInstanceFromVirtualPath(text, typeof(IHttpHandler)) as IHttpHandler;
					this.previousPage = (Page)httpHandler;
					this.previousPage.isCrossPagePostBack = true;
					this.Server.Execute(httpHandler, null, true, this._context.Request.CurrentExecutionFilePath, null, false, false);
				}
			}
		}

		/// <summary>Called during page initialization to create a collection of content (from content controls) that is handed to a master page, if the current page or master page refers to a master page. </summary>
		/// <param name="templateName">The name of the content template to add.</param>
		/// <param name="template">The content template</param>
		/// <exception cref="T:System.Web.HttpException">A content template with the same name already exists.</exception>
		// Token: 0x06001522 RID: 5410 RVA: 0x00037724 File Offset: 0x00035924
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal void AddContentTemplate(string templateName, ITemplate template)
		{
			if (this.contentTemplates == null)
			{
				this.contentTemplates = new Hashtable();
			}
			this.contentTemplates[templateName] = template;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00037746 File Offset: 0x00035946
		internal PageTheme PageTheme
		{
			get
			{
				return this._pageTheme;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0003774E File Offset: 0x0003594E
		internal PageTheme StyleSheetPageTheme
		{
			get
			{
				return this._styleSheetPageTheme;
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00037756 File Offset: 0x00035956
		internal void PushDataItemContext(object o)
		{
			if (this.dataItemCtx == null)
			{
				this.dataItemCtx = new Stack();
			}
			this.dataItemCtx.Push(o);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00037777 File Offset: 0x00035977
		internal void PopDataItemContext()
		{
			if (this.dataItemCtx == null)
			{
				throw new InvalidOperationException();
			}
			this.dataItemCtx.Pop();
		}

		/// <summary>Gets the data item at the top of the data-binding context stack.</summary>
		/// <returns>The object at the top of the data binding context stack.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no data-binding context for the page.</exception>
		// Token: 0x06001527 RID: 5415 RVA: 0x00037793 File Offset: 0x00035993
		public object GetDataItem()
		{
			if (this.dataItemCtx == null || this.dataItemCtx.Count == 0)
			{
				throw new InvalidOperationException("No data item");
			}
			return this.dataItemCtx.Peek();
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000377C0 File Offset: 0x000359C0
		private void AddStyleSheets(PageTheme theme, ref List<string> links)
		{
			if (theme == null)
			{
				return;
			}
			string[] array = ((theme != null) ? theme.GetStyleSheets() : null);
			if (array == null || array.Length == 0)
			{
				return;
			}
			if (links == null)
			{
				links = new List<string>();
			}
			links.AddRange(array);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001529 RID: 5417 RVA: 0x000377FC File Offset: 0x000359FC
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			List<string> list = null;
			this.AddStyleSheets(this.StyleSheetPageTheme, ref list);
			this.AddStyleSheets(this.PageTheme, ref list);
			if (list == null)
			{
				return;
			}
			HtmlHead header = this.Header;
			if (list != null && header == null)
			{
				throw new InvalidOperationException("Using themed css files requires a header control on the page.");
			}
			ControlCollection controls = header.Controls;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				string text = list[i];
				HtmlLink htmlLink = new HtmlLink();
				htmlLink.Href = text;
				htmlLink.Attributes["type"] = "text/css";
				htmlLink.Attributes["rel"] = "stylesheet";
				controls.AddAt(0, htmlLink);
			}
		}

		/// <summary>Returns a list of physical file names that correspond to a list of virtual file locations.</summary>
		/// <returns>An object containing a list of physical file locations.</returns>
		/// <param name="virtualFileDependencies">A string array of virtual file locations.</param>
		// Token: 0x0600152A RID: 5418 RVA: 0x0000207C File Offset: 0x0000027C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[global::System.MonoDocumentationNote("Not implemented.  Only used by .net aspx parser")]
		protected object GetWrappedFileDependencies(string[] virtualFileDependencies)
		{
			return virtualFileDependencies;
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.Page.Culture" /> and <see cref="P:System.Web.UI.Page.UICulture" /> for the current thread of the page.</summary>
		// Token: 0x0600152B RID: 5419 RVA: 0x00003946 File Offset: 0x00001B46
		[global::System.MonoDocumentationNote("Does nothing.  Used by .net aspx parser")]
		protected virtual void InitializeCulture()
		{
		}

		/// <summary>Adds a list of dependent files that make up the current page. This method is used internally by the ASP.NET page framework and is not intended to be used directly from your code.</summary>
		/// <param name="virtualFileDependencies">An <see cref="T:System.Object" /> containing the list of file names.</param>
		// Token: 0x0600152C RID: 5420 RVA: 0x00003946 File Offset: 0x00001B46
		[global::System.MonoDocumentationNote("Does nothing. Used by .net aspx parser")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal void AddWrappedFileDependencies(object virtualFileDependencies)
		{
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000378B5 File Offset: 0x00035AB5
		// Note: this type is marked as 'beforefieldinit'.
		static Page()
		{
			Page.InitCompleteEvent = new object();
			Page.LoadCompleteEvent = new object();
			Page.PreInitEvent = new object();
			Page.PreLoadEvent = new object();
			Page.PreRenderCompleteEvent = new object();
			Page.SaveStateCompleteEvent = new object();
		}

		/// <summary>Gets the model binding execution context.</summary>
		/// <returns>The model binding execution context. If the model binding execution context is null, a new one is created and returned.</returns>
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelBindingExecutionContext ModelBindingExecutionContext
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the model state dictionary object that contains the state of the model and of model-binding validation.</summary>
		/// <returns>The model state dictionary object.</returns>
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelStateDictionary ModelState
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the query string value is validated.</summary>
		/// <returns>true if query string validation should be skipped (the query string should not be validated); otherwise, false if query string validation should take place as normal. The default is false.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000378F4 File Offset: 0x00035AF4
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool SkipFormActionValidation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether unobtrusive JavaScript is used for client-side validation.</summary>
		/// <returns>true if unobtrusive JavaScript is used; otherwise, false.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00037910 File Offset: 0x00035B10
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public UnobtrusiveValidationMode UnobtrusiveValidationMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return UnobtrusiveValidationMode.None;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether the page checks client input from the browser for potentially dangerous values.</summary>
		/// <returns>A value that indicates whether the page checks client input. The default is <see cref="F:System.Web.UI.ValidateRequestMode.Enabled" />.</returns>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0003792C File Offset: 0x00035B2C
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override ValidateRequestMode ValidateRequestMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ValidateRequestMode.Inherit;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns a name-value collection of data that was posted to the page using either a POST or a GET command, without performing ASP.NET request validation on the request.</summary>
		/// <returns>An object that contains the unvalidated form data.</returns>
		// Token: 0x06001536 RID: 5430 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual NameValueCollection DeterminePostBackModeUnvalidated()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Updates the specified model instance using values from the data-bound control.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="model">The model.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06001537 RID: 5431 RVA: 0x00037948 File Offset: 0x00035B48
		public virtual bool TryUpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Updates the model instance using values from the specified value provider.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="model">The model.</param>
		/// <param name="valueProvider">The value provider.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06001538 RID: 5432 RVA: 0x00037964 File Offset: 0x00035B64
		public virtual bool TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Updates the specified model instance using values from the data-bound control.</summary>
		/// <param name="model">The model.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06001539 RID: 5433 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void UpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the specified model instance using values from a specified value provider.</summary>
		/// <param name="model">The model.</param>
		/// <param name="valueProvider">The value provider.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x0600153A RID: 5434 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void UpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040014A2 RID: 5282
		private bool _eventValidation = true;

		// Token: 0x040014A3 RID: 5283
		private object[] _savedControlState;

		// Token: 0x040014A4 RID: 5284
		private bool _doLoadPreviousPage;

		// Token: 0x040014A5 RID: 5285
		private string _focusedControlID;

		// Token: 0x040014A6 RID: 5286
		private bool _hasEnabledControlArray;

		// Token: 0x040014A7 RID: 5287
		private bool _viewState;

		// Token: 0x040014A8 RID: 5288
		private bool _viewStateMac;

		// Token: 0x040014A9 RID: 5289
		private string _errorPage;

		// Token: 0x040014AA RID: 5290
		private bool is_validated;

		// Token: 0x040014AB RID: 5291
		private bool _smartNavigation;

		// Token: 0x040014AC RID: 5292
		private int _transactionMode;

		// Token: 0x040014AD RID: 5293
		private ValidatorCollection _validators;

		// Token: 0x040014AE RID: 5294
		private bool renderingForm;

		// Token: 0x040014AF RID: 5295
		private string _savedViewState;

		// Token: 0x040014B0 RID: 5296
		private List<string> _requiresPostBack;

		// Token: 0x040014B1 RID: 5297
		private List<string> _requiresPostBackCopy;

		// Token: 0x040014B2 RID: 5298
		private List<IPostBackDataHandler> requiresPostDataChanged;

		// Token: 0x040014B3 RID: 5299
		private IPostBackEventHandler requiresRaiseEvent;

		// Token: 0x040014B4 RID: 5300
		private IPostBackEventHandler formPostedRequiresRaiseEvent;

		// Token: 0x040014B5 RID: 5301
		private NameValueCollection secondPostData;

		// Token: 0x040014B6 RID: 5302
		private bool requiresPostBackScript;

		// Token: 0x040014B7 RID: 5303
		private bool postBackScriptRendered;

		// Token: 0x040014B8 RID: 5304
		private bool requiresFormScriptDeclaration;

		// Token: 0x040014B9 RID: 5305
		private bool formScriptDeclarationRendered;

		// Token: 0x040014BA RID: 5306
		private bool handleViewState;

		// Token: 0x040014BB RID: 5307
		private string viewStateUserKey;

		// Token: 0x040014BC RID: 5308
		private NameValueCollection _requestValueCollection;

		// Token: 0x040014BD RID: 5309
		private string clientTarget;

		// Token: 0x040014BE RID: 5310
		private ClientScriptManager scriptManager;

		// Token: 0x040014BF RID: 5311
		private bool allow_load;

		// Token: 0x040014C0 RID: 5312
		private PageStatePersister page_state_persister;

		// Token: 0x040014C1 RID: 5313
		private CultureInfo _appCulture;

		// Token: 0x040014C2 RID: 5314
		private CultureInfo _appUICulture;

		// Token: 0x040014C3 RID: 5315
		private HttpContext _context;

		// Token: 0x040014C4 RID: 5316
		private HttpApplicationState _application;

		// Token: 0x040014C5 RID: 5317
		private HttpResponse _response;

		// Token: 0x040014C6 RID: 5318
		private HttpRequest _request;

		// Token: 0x040014C7 RID: 5319
		private Cache _cache;

		// Token: 0x040014C8 RID: 5320
		private HttpSessionState _session;

		/// <summary>A string that defines the EVENTARGUMENT hidden field in the rendered page.</summary>
		// Token: 0x040014C9 RID: 5321
		[EditorBrowsable(EditorBrowsableState.Never)]
		public const string postEventArgumentID = "__EVENTARGUMENT";

		/// <summary>A string that defines the EVENTTARGET hidden field in the rendered page.</summary>
		// Token: 0x040014CA RID: 5322
		[EditorBrowsable(EditorBrowsableState.Never)]
		public const string postEventSourceID = "__EVENTTARGET";

		// Token: 0x040014CB RID: 5323
		private const string ScrollPositionXID = "__SCROLLPOSITIONX";

		// Token: 0x040014CC RID: 5324
		private const string ScrollPositionYID = "__SCROLLPOSITIONY";

		// Token: 0x040014CD RID: 5325
		private const string EnabledControlArrayID = "__enabledControlArray";

		// Token: 0x040014CE RID: 5326
		internal const string LastFocusID = "__LASTFOCUS";

		// Token: 0x040014CF RID: 5327
		internal const string CallbackArgumentID = "__CALLBACKPARAM";

		// Token: 0x040014D0 RID: 5328
		internal const string CallbackSourceID = "__CALLBACKID";

		// Token: 0x040014D1 RID: 5329
		internal const string PreviousPageID = "__PREVIOUSPAGE";

		// Token: 0x040014D2 RID: 5330
		private int maxPageStateFieldLength = -1;

		// Token: 0x040014D3 RID: 5331
		private string uniqueFilePathSuffix;

		// Token: 0x040014D4 RID: 5332
		private HtmlHead htmlHeader;

		// Token: 0x040014D5 RID: 5333
		private MasterPage masterPage;

		// Token: 0x040014D6 RID: 5334
		private string masterPageFile;

		// Token: 0x040014D7 RID: 5335
		private Page previousPage;

		// Token: 0x040014D8 RID: 5336
		private bool isCrossPagePostBack;

		// Token: 0x040014D9 RID: 5337
		private bool isPostBack;

		// Token: 0x040014DA RID: 5338
		private bool isCallback;

		// Token: 0x040014DB RID: 5339
		private List<Control> requireStateControls;

		// Token: 0x040014DC RID: 5340
		private HtmlForm _form;

		// Token: 0x040014DD RID: 5341
		private string _title;

		// Token: 0x040014DE RID: 5342
		private string _theme;

		// Token: 0x040014DF RID: 5343
		private string _styleSheetTheme;

		// Token: 0x040014E0 RID: 5344
		private string _metaDescription;

		// Token: 0x040014E1 RID: 5345
		private string _metaKeywords;

		// Token: 0x040014E2 RID: 5346
		private Control _autoPostBackControl;

		// Token: 0x040014E3 RID: 5347
		private bool frameworkInitialized;

		// Token: 0x040014E4 RID: 5348
		private Hashtable items;

		// Token: 0x040014E5 RID: 5349
		private bool _maintainScrollPositionOnPostBack;

		// Token: 0x040014E6 RID: 5350
		private bool asyncMode;

		// Token: 0x040014E7 RID: 5351
		private TimeSpan asyncTimeout;

		// Token: 0x040014E8 RID: 5352
		private const double DefaultAsyncTimeout = 45.0;

		// Token: 0x040014E9 RID: 5353
		private List<PageAsyncTask> parallelTasks;

		// Token: 0x040014EA RID: 5354
		private List<PageAsyncTask> serialTasks;

		// Token: 0x040014EB RID: 5355
		private ViewStateEncryptionMode viewStateEncryptionMode;

		// Token: 0x040014EC RID: 5356
		private bool controlRegisteredForViewStateEncryption;

		// Token: 0x040014ED RID: 5357
		private string _validationStartupScript;

		// Token: 0x040014EE RID: 5358
		private string _validationOnSubmitStatement;

		// Token: 0x040014EF RID: 5359
		private string _validationInitializeScript;

		// Token: 0x040014F0 RID: 5360
		private string _webFormScriptReference;

		// Token: 0x040014F7 RID: 5367
		private int event_mask;

		// Token: 0x040014F8 RID: 5368
		private const int initcomplete_mask = 1;

		// Token: 0x040014F9 RID: 5369
		private const int loadcomplete_mask = 2;

		// Token: 0x040014FA RID: 5370
		private const int preinit_mask = 4;

		// Token: 0x040014FB RID: 5371
		private const int preload_mask = 8;

		// Token: 0x040014FC RID: 5372
		private const int prerendercomplete_mask = 16;

		// Token: 0x040014FD RID: 5373
		private const int savestatecomplete_mask = 32;

		// Token: 0x040014FE RID: 5374
		private Hashtable contentTemplates;

		// Token: 0x040014FF RID: 5375
		private PageTheme _pageTheme;

		// Token: 0x04001500 RID: 5376
		private PageTheme _styleSheetPageTheme;

		// Token: 0x04001501 RID: 5377
		private Stack dataItemCtx;

		// Token: 0x02000208 RID: 520
		// (Invoke) Token: 0x0600153C RID: 5436
		private delegate void ProcessRequestDelegate(HttpContext context);

		// Token: 0x02000209 RID: 521
		private sealed class DummyAsyncResult : IAsyncResult
		{
			// Token: 0x0600153F RID: 5439 RVA: 0x0003797F File Offset: 0x00035B7F
			public DummyAsyncResult(bool isCompleted, bool completedSynchronously, object state)
			{
				this.isCompleted = isCompleted;
				this.completedSynchronously = completedSynchronously;
				this.state = state;
				if (isCompleted)
				{
					this.asyncWaitHandle = new ManualResetEvent(true);
					return;
				}
				this.asyncWaitHandle = new ManualResetEvent(false);
			}

			// Token: 0x170006B8 RID: 1720
			// (get) Token: 0x06001540 RID: 5440 RVA: 0x000379B8 File Offset: 0x00035BB8
			public object AsyncState
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170006B9 RID: 1721
			// (get) Token: 0x06001541 RID: 5441 RVA: 0x000379C0 File Offset: 0x00035BC0
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return this.asyncWaitHandle;
				}
			}

			// Token: 0x170006BA RID: 1722
			// (get) Token: 0x06001542 RID: 5442 RVA: 0x000379C8 File Offset: 0x00035BC8
			public bool CompletedSynchronously
			{
				get
				{
					return this.completedSynchronously;
				}
			}

			// Token: 0x170006BB RID: 1723
			// (get) Token: 0x06001543 RID: 5443 RVA: 0x000379D0 File Offset: 0x00035BD0
			public bool IsCompleted
			{
				get
				{
					return this.isCompleted;
				}
			}

			// Token: 0x04001502 RID: 5378
			private readonly object state;

			// Token: 0x04001503 RID: 5379
			private readonly WaitHandle asyncWaitHandle;

			// Token: 0x04001504 RID: 5380
			private readonly bool completedSynchronously;

			// Token: 0x04001505 RID: 5381
			private readonly bool isCompleted;
		}
	}
}
