using System;

namespace System.Web.Profile
{
	/// <summary>Provides data for the <see cref="E:System.Web.Profile.ProfileModule.Personalize" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
	// Token: 0x020004FD RID: 1277
	public sealed class ProfileEventArgs : EventArgs
	{
		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x00097344 File Offset: 0x00095544
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Gets or sets the user profile for the current request.</summary>
		/// <returns>The user profile to use for the current request. The default is null.</returns>
		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x0009734C File Offset: 0x0009554C
		// (set) Token: 0x060038E8 RID: 14568 RVA: 0x00097354 File Offset: 0x00095554
		public ProfileBase Profile
		{
			get
			{
				return this._Profile;
			}
			set
			{
				this._Profile = value;
			}
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileEventArgs" /> class.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
		// Token: 0x060038E9 RID: 14569 RVA: 0x0009735D File Offset: 0x0009555D
		public ProfileEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x04001F0B RID: 7947
		private HttpContext _Context;

		// Token: 0x04001F0C RID: 7948
		private ProfileBase _Profile;
	}
}
