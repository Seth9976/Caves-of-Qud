using System;

namespace System.Web.Profile
{
	/// <summary>Provides data for the <see cref="E:System.Web.Profile.ProfileModule.ProfileAutoSaving" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
	// Token: 0x02000502 RID: 1282
	public sealed class ProfileAutoSaveEventArgs : EventArgs
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileAutoSaveEventArgs" /> class.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
		// Token: 0x06003905 RID: 14597 RVA: 0x00097681 File Offset: 0x00095881
		public ProfileAutoSaveEventArgs(HttpContext context)
		{
			this.context = context;
			this.continueWithProfileAutoSave = true;
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x00097697 File Offset: 0x00095897
		public HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Profile.ProfileModule" /> will automatically save the user profile.</summary>
		/// <returns>true if the <see cref="T:System.Web.Profile.ProfileModule" /> will automatically save the user profile; otherwise, false. The default is true.</returns>
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x0009769F File Offset: 0x0009589F
		// (set) Token: 0x06003908 RID: 14600 RVA: 0x000976A7 File Offset: 0x000958A7
		public bool ContinueWithProfileAutoSave
		{
			get
			{
				return this.continueWithProfileAutoSave;
			}
			set
			{
				this.continueWithProfileAutoSave = value;
			}
		}

		// Token: 0x04001F18 RID: 7960
		private HttpContext context;

		// Token: 0x04001F19 RID: 7961
		private bool continueWithProfileAutoSave;
	}
}
