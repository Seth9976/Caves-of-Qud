using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security
{
	/// <summary>Sets the identity of the user for an ASP.NET application when forms authentication is enabled. This class cannot be inherited.</summary>
	// Token: 0x020004BA RID: 1210
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class FormsAuthenticationModule : IHttpModule
	{
		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06003672 RID: 13938 RVA: 0x0008C9A7 File Offset: 0x0008ABA7
		internal static bool FormsAuthRequired
		{
			get
			{
				return FormsAuthenticationModule._fAuthRequired;
			}
		}

		/// <summary>Occurs when the application authenticates the current request.</summary>
		// Token: 0x14000104 RID: 260
		// (add) Token: 0x06003673 RID: 13939 RVA: 0x0008C9AE File Offset: 0x0008ABAE
		// (remove) Token: 0x06003674 RID: 13940 RVA: 0x0008C9C1 File Offset: 0x0008ABC1
		public event FormsAuthenticationEventHandler Authenticate
		{
			add
			{
				this.events.AddHandler(FormsAuthenticationModule.authenticateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(FormsAuthenticationModule.authenticateEvent, value);
			}
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x0008C9D4 File Offset: 0x0008ABD4
		private void InitConfig(HttpContext context)
		{
			if (this.isConfigInitialized)
			{
				return;
			}
			this._config = (AuthenticationSection)WebConfigurationManager.GetSection("system.web/authentication");
			if (!FormsAuthenticationModule._fAuthChecked)
			{
				FormsAuthenticationModule._fAuthRequired = this._config.Mode == AuthenticationMode.Forms;
				FormsAuthenticationModule._fAuthChecked = true;
			}
			this.isConfigInitialized = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationModule" /> class. </summary>
		// Token: 0x06003676 RID: 13942 RVA: 0x0008CA26 File Offset: 0x0008AC26
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public FormsAuthenticationModule()
		{
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.FormsAuthenticationModule" />.</summary>
		// Token: 0x06003677 RID: 13943 RVA: 0x00003946 File Offset: 0x00001B46
		public void Dispose()
		{
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.FormsAuthenticationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003678 RID: 13944 RVA: 0x0008CA39 File Offset: 0x0008AC39
		public void Init(HttpApplication app)
		{
			app.AuthenticateRequest += this.OnAuthenticateRequest;
			app.EndRequest += this.OnEndRequest;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x0008CA60 File Offset: 0x0008AC60
		private void OnAuthenticateRequest(object sender, EventArgs args)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			this.InitConfig(context);
			if (this._config == null || this._config.Mode != AuthenticationMode.Forms)
			{
				return;
			}
			string name = this._config.Forms.Name;
			string path = this._config.Forms.Path;
			string text = this._config.Forms.LoginUrl;
			bool slidingExpiration = this._config.Forms.SlidingExpiration;
			if (!VirtualPathUtility.IsRooted(text))
			{
				text = "~/" + text;
			}
			string text2 = string.Empty;
			string text3 = null;
			try
			{
				text2 = context.Request.PhysicalPath;
				text3 = context.Request.MapPath(text);
			}
			catch
			{
			}
			context.SkipAuthorization = string.Compare(text2, text3, RuntimeHelpers.CaseInsensitive, Helpers.InvariantCulture) == 0;
			string filePath = context.Request.FilePath;
			if (filePath.Length > 15 && string.CompareOrdinal("WebResource.axd", 0, filePath, filePath.Length - 15, 15) == 0)
			{
				context.SkipAuthorization = true;
			}
			FormsAuthenticationEventArgs e = new FormsAuthenticationEventArgs(context);
			FormsAuthenticationEventHandler formsAuthenticationEventHandler = this.events[FormsAuthenticationModule.authenticateEvent] as FormsAuthenticationEventHandler;
			if (formsAuthenticationEventHandler != null)
			{
				formsAuthenticationEventHandler(this, e);
			}
			bool flag = context.User == null;
			if (e.User != null || !flag)
			{
				if (flag)
				{
					context.User = e.User;
				}
				return;
			}
			HttpCookie httpCookie = context.Request.Cookies[name];
			if (httpCookie == null || (httpCookie.Expires != DateTime.MinValue && httpCookie.Expires < DateTime.Now))
			{
				return;
			}
			FormsAuthenticationTicket formsAuthenticationTicket = null;
			try
			{
				formsAuthenticationTicket = FormsAuthentication.Decrypt(httpCookie.Value);
			}
			catch (ArgumentException)
			{
				return;
			}
			if (formsAuthenticationTicket == null || formsAuthenticationTicket.Expired)
			{
				return;
			}
			FormsAuthenticationTicket formsAuthenticationTicket2 = formsAuthenticationTicket;
			if (slidingExpiration)
			{
				formsAuthenticationTicket = FormsAuthentication.RenewTicketIfOld(formsAuthenticationTicket);
			}
			context.User = new GenericPrincipal(new FormsIdentity(formsAuthenticationTicket), new string[0]);
			if (httpCookie.Expires == DateTime.MinValue && formsAuthenticationTicket2 == formsAuthenticationTicket)
			{
				return;
			}
			httpCookie.Value = FormsAuthentication.Encrypt(formsAuthenticationTicket);
			httpCookie.Path = path;
			if (formsAuthenticationTicket.IsPersistent)
			{
				httpCookie.Expires = formsAuthenticationTicket.Expiration;
			}
			context.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x0008CCCC File Offset: 0x0008AECC
		private void OnEndRequest(object sender, EventArgs args)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			if (context.Response.StatusCode != 401 || context.Request.QueryString["ReturnUrl"] != null)
			{
				return;
			}
			if (context.Response.StatusCode == 401 && context.Response.SuppressFormsAuthenticationRedirect)
			{
				return;
			}
			this.InitConfig(context);
			string loginUrl = this._config.Forms.LoginUrl;
			if (this._config == null || this._config.Mode != AuthenticationMode.Forms)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(UrlUtils.Combine(context.Request.ApplicationPath, loginUrl));
			stringBuilder.AppendFormat("?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
			context.Response.Redirect(stringBuilder.ToString(), false);
		}

		// Token: 0x04001DC5 RID: 7621
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001DC6 RID: 7622
		private static bool _fAuthChecked;

		// Token: 0x04001DC7 RID: 7623
		private static bool _fAuthRequired;

		// Token: 0x04001DC8 RID: 7624
		private AuthenticationSection _config;

		// Token: 0x04001DC9 RID: 7625
		private bool isConfigInitialized;

		// Token: 0x04001DCA RID: 7626
		private EventHandlerList events = new EventHandlerList();
	}
}
