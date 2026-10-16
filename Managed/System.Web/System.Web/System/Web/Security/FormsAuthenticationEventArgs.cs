using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	/// <summary>Provides data for the FormsAuthentication_OnAuthenticate event. This class cannot be inherited.</summary>
	// Token: 0x020004A6 RID: 1190
	public sealed class FormsAuthenticationEventArgs : EventArgs
	{
		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</returns>
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x0008BBD3 File Offset: 0x00089DD3
		// (set) Token: 0x060035E2 RID: 13794 RVA: 0x0008BBDB File Offset: 0x00089DDB
		public IPrincipal User
		{
			get
			{
				return this._User;
			}
			[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
			set
			{
				this._User = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x0008BBE4 File Offset: 0x00089DE4
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationEventArgs" /> class.</summary>
		/// <param name="context">The context for the event. </param>
		// Token: 0x060035E4 RID: 13796 RVA: 0x0008BBEC File Offset: 0x00089DEC
		public FormsAuthenticationEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x04001D96 RID: 7574
		private IPrincipal _User;

		// Token: 0x04001D97 RID: 7575
		private HttpContext _Context;
	}
}
