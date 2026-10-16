using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Manages a <see cref="T:System.Web.Security.RolePrincipal" /> instance for the current user. This class cannot be inherited.</summary>
	// Token: 0x020004C4 RID: 1220
	public sealed class RoleManagerModule : IHttpModule
	{
		/// <summary>A global application event that is raised when the <see cref="T:System.Web.Security.RoleManagerModule" /> is ready to create a <see cref="T:System.Web.Security.RolePrincipal" /> that represents the current user.</summary>
		// Token: 0x14000107 RID: 263
		// (add) Token: 0x0600371A RID: 14106 RVA: 0x0008DE29 File Offset: 0x0008C029
		// (remove) Token: 0x0600371B RID: 14107 RVA: 0x0008DE3C File Offset: 0x0008C03C
		public event RoleManagerEventHandler GetRoles
		{
			add
			{
				this.events.AddHandler(RoleManagerModule.getRolesEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(RoleManagerModule.getRolesEvent, value);
			}
		}

		/// <summary>Called by the HTTP runtime to dispose of the role-manager module.</summary>
		// Token: 0x0600371C RID: 14108 RVA: 0x00003946 File Offset: 0x00001B46
		public void Dispose()
		{
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x0008DE50 File Offset: 0x0008C050
		private void ClearCookie(HttpApplication app, string cookieName)
		{
			HttpCookie httpCookie = new HttpCookie(this._config.CookieName, "");
			httpCookie.Path = this._config.CookiePath;
			httpCookie.Expires = DateTime.MinValue;
			httpCookie.Domain = this._config.Domain;
			httpCookie.Secure = this._config.CookieRequireSSL;
			app.Response.SetCookie(httpCookie);
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x0008DEC0 File Offset: 0x0008C0C0
		private void OnPostAuthenticateRequest(object sender, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			if (this._config == null || !this._config.Enabled)
			{
				return;
			}
			RoleManagerEventHandler roleManagerEventHandler = this.events[RoleManagerModule.getRolesEvent] as RoleManagerEventHandler;
			if (roleManagerEventHandler != null)
			{
				RoleManagerEventArgs e = new RoleManagerEventArgs(httpApplication.Context);
				roleManagerEventHandler(this, e);
				if (e.RolesPopulated)
				{
					return;
				}
			}
			HttpCookie httpCookie = httpApplication.Request.Cookies[this._config.CookieName];
			IIdentity identity = httpApplication.Context.User.Identity;
			RolePrincipal rolePrincipal;
			if (httpApplication.Request.IsAuthenticated)
			{
				if (httpCookie != null)
				{
					if (!this._config.CacheRolesInCookie)
					{
						httpCookie = null;
					}
					else if (this._config.CookieRequireSSL && !httpApplication.Request.IsSecureConnection)
					{
						httpCookie = null;
						this.ClearCookie(httpApplication, this._config.CookieName);
					}
				}
				if (httpCookie == null || string.IsNullOrEmpty(httpCookie.Value))
				{
					rolePrincipal = new RolePrincipal(identity);
				}
				else
				{
					rolePrincipal = new RolePrincipal(identity, httpCookie.Value);
				}
			}
			else
			{
				if (httpCookie != null)
				{
					this.ClearCookie(httpApplication, this._config.CookieName);
				}
				rolePrincipal = new RolePrincipal(identity);
			}
			httpApplication.Context.User = rolePrincipal;
			Thread.CurrentPrincipal = rolePrincipal;
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x0008DFFC File Offset: 0x0008C1FC
		private void OnEndRequest(object sender, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			if (this._config == null || !this._config.Enabled || !this._config.CacheRolesInCookie)
			{
				return;
			}
			if (!httpApplication.Request.IsAuthenticated)
			{
				return;
			}
			if (this._config.CookieRequireSSL && !httpApplication.Request.IsSecureConnection)
			{
				return;
			}
			RolePrincipal rolePrincipal = httpApplication.Context.User as RolePrincipal;
			if (rolePrincipal == null)
			{
				return;
			}
			if (!rolePrincipal.CachedListChanged)
			{
				return;
			}
			string text = rolePrincipal.ToEncryptedTicket();
			if (text == null || text.Length > 4096)
			{
				this.ClearCookie(httpApplication, this._config.CookieName);
				return;
			}
			HttpCookie httpCookie = new HttpCookie(this._config.CookieName, text);
			httpCookie.HttpOnly = true;
			if (!string.IsNullOrEmpty(this._config.Domain))
			{
				httpCookie.Domain = this._config.Domain;
			}
			if (this._config.CookieRequireSSL)
			{
				httpCookie.Secure = true;
			}
			if (this._config.CookiePath.Length > 1)
			{
				httpCookie.Path = this._config.CookiePath;
			}
			httpApplication.Response.SetCookie(httpCookie);
		}

		/// <summary>Associates the role manager with the specified application.</summary>
		/// <param name="app">The <see cref="T:System.Web.HttpApplication" /> to associate the <see cref="T:System.Web.Security.RoleManagerModule" /> with.</param>
		// Token: 0x06003720 RID: 14112 RVA: 0x0008E124 File Offset: 0x0008C324
		public void Init(HttpApplication app)
		{
			this._config = (RoleManagerSection)WebConfigurationManager.GetSection("system.web/roleManager");
			app.PostAuthenticateRequest += this.OnPostAuthenticateRequest;
			app.EndRequest += this.OnEndRequest;
		}

		// Token: 0x04001DE5 RID: 7653
		private static readonly object getRolesEvent = new object();

		// Token: 0x04001DE6 RID: 7654
		private RoleManagerSection _config;

		// Token: 0x04001DE7 RID: 7655
		private EventHandlerList events = new EventHandlerList();
	}
}
