using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	/// <summary>The event argument passed to the <see cref="E:System.Web.Security.PassportAuthenticationModule.Authenticate" /> event by a <see cref="T:System.Web.Security.PassportAuthenticationModule" />. Since there is already an identity at this point, this is useful mainly for attaching a custom <see cref="T:System.Security.Principal.IPrincipal" /> object to the context using the supplied identity. This class is deprecated.</summary>
	// Token: 0x020004A9 RID: 1193
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportAuthenticationEventArgs : EventArgs
	{
		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the request. This class is deprecated.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the request.</returns>
		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x0008BBFB File Offset: 0x00089DFB
		// (set) Token: 0x060035EE RID: 13806 RVA: 0x0008BC03 File Offset: 0x00089E03
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

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request. This class is deprecated.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x0008BC0C File Offset: 0x00089E0C
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Gets an authenticated Passport identity. This class is deprecated.</summary>
		/// <returns>An authenticated Passport identity.</returns>
		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x0008BC14 File Offset: 0x00089E14
		public PassportIdentity Identity
		{
			get
			{
				return this._Identity;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.PassportAuthenticationEventArgs" /> class. This class is deprecated.</summary>
		/// <param name="identity">The identity object </param>
		/// <param name="context">The context for the event. </param>
		// Token: 0x060035F1 RID: 13809 RVA: 0x0008BC1C File Offset: 0x00089E1C
		public PassportAuthenticationEventArgs(PassportIdentity identity, HttpContext context)
		{
			this._Identity = identity;
			this._Context = context;
		}

		// Token: 0x04001D98 RID: 7576
		private IPrincipal _User;

		// Token: 0x04001D99 RID: 7577
		private HttpContext _Context;

		// Token: 0x04001D9A RID: 7578
		private PassportIdentity _Identity;
	}
}
