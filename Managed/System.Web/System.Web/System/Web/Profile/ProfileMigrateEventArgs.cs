using System;

namespace System.Web.Profile
{
	/// <summary>Provides data for the <see cref="E:System.Web.Profile.ProfileModule.MigrateAnonymous" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
	// Token: 0x02000506 RID: 1286
	public sealed class ProfileMigrateEventArgs : EventArgs
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileMigrateEventArgs" /> class.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
		/// <param name="anonymousId">The anonymous identifier being migrated from.</param>
		// Token: 0x0600393A RID: 14650 RVA: 0x00098140 File Offset: 0x00096340
		public ProfileMigrateEventArgs(HttpContext context, string anonymousId)
		{
			this.context = context;
			this.anonymousId = anonymousId;
		}

		/// <summary>Gets the anonymous identifier for the anonymous profile from which to migrate profile property values.</summary>
		/// <returns>The anonymous identifier for the anonymous profile from which to migrate profile property values.</returns>
		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x00098156 File Offset: 0x00096356
		public string AnonymousID
		{
			get
			{
				return this.anonymousId;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x0600393C RID: 14652 RVA: 0x0009815E File Offset: 0x0009635E
		public HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x04001F24 RID: 7972
		private HttpContext context;

		// Token: 0x04001F25 RID: 7973
		private string anonymousId;
	}
}
