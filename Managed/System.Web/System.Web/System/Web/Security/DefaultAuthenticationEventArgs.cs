using System;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Provides data for the DefaultAuthentication_OnAuthenticate event. This class cannot be inherited.</summary>
	// Token: 0x020004B6 RID: 1206
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DefaultAuthenticationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.DefaultAuthenticationEventArgs" /> class.</summary>
		/// <param name="context">The context for the event. </param>
		// Token: 0x0600363D RID: 13885 RVA: 0x0008BFF0 File Offset: 0x0008A1F0
		public DefaultAuthenticationEventArgs(HttpContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this._context = context;
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x0600363E RID: 13886 RVA: 0x0008C00D File Offset: 0x0008A20D
		public HttpContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x04001DAF RID: 7599
		private HttpContext _context;
	}
}
