using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Sets the identity of the user for an ASP.NET application when Windows authentication is enabled. This class cannot be inherited.</summary>
	// Token: 0x020004D0 RID: 1232
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WindowsAuthenticationModule : IHttpModule
	{
		/// <summary>Occurs when the application authenticates the current request.</summary>
		// Token: 0x14000108 RID: 264
		// (add) Token: 0x060037FF RID: 14335 RVA: 0x00094AD3 File Offset: 0x00092CD3
		// (remove) Token: 0x06003800 RID: 14336 RVA: 0x00094AE6 File Offset: 0x00092CE6
		public event WindowsAuthenticationEventHandler Authenticate
		{
			add
			{
				this.events.AddHandler(WindowsAuthenticationModule.authenticateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(WindowsAuthenticationModule.authenticateEvent, value);
			}
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Security.WindowsAuthenticationModule" /> class.</summary>
		// Token: 0x06003801 RID: 14337 RVA: 0x00094AF9 File Offset: 0x00092CF9
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public WindowsAuthenticationModule()
		{
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.WindowsAuthenticationModule" />.</summary>
		// Token: 0x06003802 RID: 14338 RVA: 0x00094B0C File Offset: 0x00092D0C
		public void Dispose()
		{
			this.events.Dispose();
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.WindowsAuthenticationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003803 RID: 14339 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public void Init(HttpApplication app)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E2A RID: 7722
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001E2B RID: 7723
		private EventHandlerList events = new EventHandlerList();
	}
}
