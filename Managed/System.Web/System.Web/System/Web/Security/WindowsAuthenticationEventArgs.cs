using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	/// <summary>Provides data for the WindowsAuthentication_OnAuthenticate event. This class cannot be inherited.</summary>
	// Token: 0x020004AD RID: 1197
	public sealed class WindowsAuthenticationEventArgs : EventArgs
	{
		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</returns>
		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x0008BC5A File Offset: 0x00089E5A
		// (set) Token: 0x060035FF RID: 13823 RVA: 0x0008BC62 File Offset: 0x00089E62
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
		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x0008BC6B File Offset: 0x00089E6B
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Gets the Windows identity passed to the <see cref="T:System.Web.Security.WindowsAuthenticationEventArgs" /> constructor.</summary>
		/// <returns>The Windows identity passed to the <see cref="T:System.Web.Security.WindowsAuthenticationEventArgs" /> constructor.</returns>
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x0008BC73 File Offset: 0x00089E73
		public WindowsIdentity Identity
		{
			get
			{
				return this._Identity;
			}
		}

		/// <summary>Initializes a newly created instance of the <see cref="T:System.Web.Security.WindowsAuthenticationEventArgs" /> class.</summary>
		/// <param name="identity">The Windows identity object. </param>
		/// <param name="context">The context for the event. </param>
		// Token: 0x06003602 RID: 13826 RVA: 0x0008BC7B File Offset: 0x00089E7B
		public WindowsAuthenticationEventArgs(WindowsIdentity identity, HttpContext context)
		{
			this._Identity = identity;
			this._Context = context;
		}

		// Token: 0x04001D9D RID: 7581
		private IPrincipal _User;

		// Token: 0x04001D9E RID: 7582
		private HttpContext _Context;

		// Token: 0x04001D9F RID: 7583
		private WindowsIdentity _Identity;
	}
}
