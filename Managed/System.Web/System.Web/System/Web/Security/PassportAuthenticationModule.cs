using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Provides a wrapper around Passport Authentication services. This class cannot be inherited. This class is deprecated.</summary>
	// Token: 0x020004C2 RID: 1218
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PassportAuthenticationModule : IHttpModule
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Security.PassportAuthenticationModule" /> class. This class is deprecated.</summary>
		// Token: 0x060036D9 RID: 14041 RVA: 0x0008DB47 File Offset: 0x0008BD47
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public PassportAuthenticationModule()
		{
		}

		/// <summary>Raised during authentication. This is a Global.asax event that must be named PassportAuthentication_OnAuthenticate. This class is deprecated</summary>
		// Token: 0x14000106 RID: 262
		// (add) Token: 0x060036DA RID: 14042 RVA: 0x0008DB5A File Offset: 0x0008BD5A
		// (remove) Token: 0x060036DB RID: 14043 RVA: 0x0008DB6D File Offset: 0x0008BD6D
		public event PassportAuthenticationEventHandler Authenticate
		{
			add
			{
				this.events.AddHandler(PassportAuthenticationModule.authenticateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PassportAuthenticationModule.authenticateEvent, value);
			}
		}

		/// <summary>Disposes of the module derived from <see cref="T:System.Web.IHttpModule" /> when called by the <see cref="T:System.Web.HttpRuntime" />. This class is deprecated.</summary>
		// Token: 0x060036DC RID: 14044 RVA: 0x0008DB80 File Offset: 0x0008BD80
		public void Dispose()
		{
			this.events.Dispose();
		}

		/// <summary>Initializes the module derived from <see cref="T:System.Web.IHttpModule" /> when called by the <see cref="T:System.Web.HttpRuntime" />. This class is deprecated</summary>
		/// <param name="app">The <see cref="T:System.Web.HttpApplication" /> module </param>
		// Token: 0x060036DD RID: 14045 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Will we ever implement this? :-)")]
		public void Init(HttpApplication app)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001DE3 RID: 7651
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001DE4 RID: 7652
		private EventHandlerList events = new EventHandlerList();
	}
}
