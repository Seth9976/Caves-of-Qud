using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Web.Security
{
	/// <summary>Ensures that an authentication object is present in the context. This class cannot be inherited.</summary>
	// Token: 0x020004B7 RID: 1207
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DefaultAuthenticationModule : IHttpModule
	{
		/// <summary>Occurs after the request has been authenticated.</summary>
		// Token: 0x14000103 RID: 259
		// (add) Token: 0x0600363F RID: 13887 RVA: 0x0008C015 File Offset: 0x0008A215
		// (remove) Token: 0x06003640 RID: 13888 RVA: 0x0008C028 File Offset: 0x0008A228
		public event DefaultAuthenticationEventHandler Authenticate
		{
			add
			{
				this.events.AddHandler(DefaultAuthenticationModule.authenticateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(DefaultAuthenticationModule.authenticateEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.DefaultAuthenticationModule" /> class. </summary>
		// Token: 0x06003641 RID: 13889 RVA: 0x0008C03B File Offset: 0x0008A23B
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public DefaultAuthenticationModule()
		{
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.DefaultAuthenticationModule" />.</summary>
		// Token: 0x06003642 RID: 13890 RVA: 0x00003946 File Offset: 0x00001B46
		public void Dispose()
		{
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.DefaultAuthenticationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003643 RID: 13891 RVA: 0x0008C04E File Offset: 0x0008A24E
		public void Init(HttpApplication app)
		{
			app.DefaultAuthentication += this.OnDefaultAuthentication;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x0008C064 File Offset: 0x0008A264
		private void OnDefaultAuthentication(object sender, EventArgs args)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			DefaultAuthenticationEventHandler defaultAuthenticationEventHandler = this.events[DefaultAuthenticationModule.authenticateEvent] as DefaultAuthenticationEventHandler;
			if (context.User == null && defaultAuthenticationEventHandler != null)
			{
				defaultAuthenticationEventHandler(this, new DefaultAuthenticationEventArgs(context));
			}
			if (context.User == null)
			{
				context.User = DefaultAuthenticationModule.generic_principal;
			}
			Thread.CurrentPrincipal = context.User;
		}

		// Token: 0x04001DB0 RID: 7600
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001DB1 RID: 7601
		private static IPrincipal generic_principal = new GenericPrincipal(new GenericIdentity("", ""), new string[0]);

		// Token: 0x04001DB2 RID: 7602
		private EventHandlerList events = new EventHandlerList();
	}
}
