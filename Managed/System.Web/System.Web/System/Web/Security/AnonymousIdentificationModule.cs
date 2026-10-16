using System;
using System.ComponentModel;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Manages anonymous identifiers for the ASP.NET application.</summary>
	// Token: 0x020004B3 RID: 1203
	public sealed class AnonymousIdentificationModule : IHttpModule
	{
		/// <summary>Occurs when a new anonymous identifier is created.</summary>
		// Token: 0x14000102 RID: 258
		// (add) Token: 0x06003631 RID: 13873 RVA: 0x0008BCC1 File Offset: 0x00089EC1
		// (remove) Token: 0x06003632 RID: 13874 RVA: 0x0008BCD4 File Offset: 0x00089ED4
		public event AnonymousIdentificationEventHandler Creating
		{
			add
			{
				this.events.AddHandler(AnonymousIdentificationModule.creatingEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(AnonymousIdentificationModule.creatingEvent, value);
			}
		}

		/// <summary>Clears the anonymous cookie or identifier associated with a session.</summary>
		/// <exception cref="T:System.NotSupportedException">Calling <see cref="M:System.Web.Security.AnonymousIdentificationModule.ClearAnonymousIdentifier" /> when the anonymous identification is not enabled.-or-The user for the current request is anonymous.</exception>
		// Token: 0x06003633 RID: 13875 RVA: 0x0008BCE7 File Offset: 0x00089EE7
		public static void ClearAnonymousIdentifier()
		{
			if (AnonymousIdentificationModule.Config == null || !AnonymousIdentificationModule.Config.Enabled)
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.AnonymousIdentificationModule" />.</summary>
		// Token: 0x06003634 RID: 13876 RVA: 0x0008BD02 File Offset: 0x00089F02
		public void Dispose()
		{
			this.app.PostAuthenticateRequest -= this.OnEnter;
			this.app = null;
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.AnonymousIdentificationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003635 RID: 13877 RVA: 0x0008BD22 File Offset: 0x00089F22
		public void Init(HttpApplication app)
		{
			this.app = app;
			app.PostAuthenticateRequest += this.OnEnter;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x0008BD40 File Offset: 0x00089F40
		[global::System.MonoTODO("cookieless userid")]
		private void OnEnter(object source, EventArgs eventArgs)
		{
			if (!AnonymousIdentificationModule.Enabled)
			{
				return;
			}
			string text = null;
			HttpCookie httpCookie = this.app.Request.Cookies[AnonymousIdentificationModule.Config.CookieName];
			if (httpCookie != null && (httpCookie.Expires == DateTime.MinValue || httpCookie.Expires > DateTime.Now))
			{
				try
				{
					text = Encoding.Unicode.GetString(Convert.FromBase64String(httpCookie.Value));
				}
				catch
				{
				}
			}
			if (text == null)
			{
				AnonymousIdentificationEventHandler anonymousIdentificationEventHandler = this.events[AnonymousIdentificationModule.creatingEvent] as AnonymousIdentificationEventHandler;
				if (anonymousIdentificationEventHandler != null)
				{
					AnonymousIdentificationEventArgs e = new AnonymousIdentificationEventArgs(HttpContext.Current);
					anonymousIdentificationEventHandler(this, e);
					text = e.AnonymousID;
				}
				if (text == null)
				{
					text = Guid.NewGuid().ToString();
				}
				HttpCookie httpCookie2 = new HttpCookie(AnonymousIdentificationModule.Config.CookieName);
				httpCookie2.Path = this.app.Request.ApplicationPath;
				httpCookie2.Expires = DateTime.Now + AnonymousIdentificationModule.Config.CookieTimeout;
				httpCookie2.Value = Convert.ToBase64String(Encoding.Unicode.GetBytes(text));
				this.app.Response.AppendCookie(httpCookie2);
			}
			this.app.Request.AnonymousID = text;
		}

		/// <summary>Gets a value indicating whether anonymous identification is enabled for the ASP.NET application.</summary>
		/// <returns>true if anonymous identification is enabled for the ASP.NET application; otherwise, false. The default is false.</returns>
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x0008BE98 File Offset: 0x0008A098
		public static bool Enabled
		{
			get
			{
				return AnonymousIdentificationModule.Config != null && AnonymousIdentificationModule.Config.Enabled;
			}
		}

		// Token: 0x04001DA6 RID: 7590
		private static readonly object creatingEvent = new object();

		// Token: 0x04001DA7 RID: 7591
		private HttpApplication app;

		// Token: 0x04001DA8 RID: 7592
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x04001DA9 RID: 7593
		private static AnonymousIdentificationSection Config = (AnonymousIdentificationSection)WebConfigurationManager.GetSection("system.web/anonymousIdentification");
	}
}
