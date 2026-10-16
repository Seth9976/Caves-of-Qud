using System;

namespace System.Web.Security
{
	/// <summary>Provides event data for the <see cref="E:System.Web.Security.RoleManagerModule.GetRoles" /> event of the <see cref="T:System.Web.Security.RoleManagerModule" /> class.</summary>
	// Token: 0x020004AB RID: 1195
	public sealed class RoleManagerEventArgs : EventArgs
	{
		/// <summary>Gets or sets a value indicating whether role information has been applied to the current user.</summary>
		/// <returns>true if role information has been applied to the current user; otherwise, false.</returns>
		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060035F6 RID: 13814 RVA: 0x0008BC32 File Offset: 0x00089E32
		// (set) Token: 0x060035F7 RID: 13815 RVA: 0x0008BC3A File Offset: 0x00089E3A
		public bool RolesPopulated
		{
			get
			{
				return this._RolesPopulated;
			}
			set
			{
				this._RolesPopulated = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x0008BC43 File Offset: 0x00089E43
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Security.RoleManagerEventArgs" /> class and sets the <see cref="P:System.Web.Security.RoleManagerEventArgs.Context" /> property to the specified <see cref="T:System.Web.HttpContext" />.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
		// Token: 0x060035F9 RID: 13817 RVA: 0x0008BC4B File Offset: 0x00089E4B
		public RoleManagerEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x04001D9B RID: 7579
		private HttpContext _Context;

		// Token: 0x04001D9C RID: 7580
		private bool _RolesPopulated;
	}
}
